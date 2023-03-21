using Hful.Core.Mapper;
using Hful.Domain;
using Hful.Iam.Domain;
using Hful.Iam.Dto;

namespace Hful.Iam.Service
{
    internal class PermissionService : IPermissionService
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<Menu> _menuRepsoitory;
        private readonly IAsyncExecutor _asyncExecutor;

        public PermissionService(IObjectMapper objectMapper, IRepository<Menu> menuRepsoitory, IAsyncExecutor asyncExecutor)
        {
            _objectMapper = objectMapper;
            _menuRepsoitory = menuRepsoitory;
            _asyncExecutor = asyncExecutor;
        }

        public async Task<List<MenuDto>> GetMenu(Guid tenantId, Guid userId)
        {
            var data = (await _asyncExecutor.ToListAsync(_menuRepsoitory.AsQueryable().Where(x => x.TenantId == tenantId)));
            return await ToTree(data);
        }

        private async Task<List<MenuDto>> ToTree(List<Menu> menus)
        {
            Dictionary<Guid, MenuDto> parentMap = menus.ToDictionary(x => x.Id, Entity2TreeDto);
            // 根节点
            List<MenuDto> result = new List<MenuDto>();
            // 父节点没有权限或者父节点被删除的节点
            List<MenuDto> alone = new List<MenuDto>();

            foreach (MenuDto item in parentMap.Values)
            {
                if (item.ParentId == null)
                {
                    result.Add(item);
                }
                else if (parentMap.ContainsKey(item.ParentId.Value))
                {
                    MenuDto? parent = parentMap.GetValueOrDefault(item.ParentId.Value);
                    if (parent.Children == null)
                    {
                        parent.Children = new List<MenuDto>();
                    }
                    parent.Children.Add(item);
                }
                else
                {
                    alone.Add(item);
                }
            }

            while (alone.Count != 0)
            {
                alone.RemoveAll(x => parentMap.ContainsKey(x.ParentId.Value));
                // 查询父节点
                HashSet<Guid> id = alone.Select(x => x.ParentId.Value).ToHashSet();
                List<MenuDto> data = (await _asyncExecutor.ToListAsync(_menuRepsoitory.AsQueryable().Where(x => id.Contains(x.Id))))
                        .Select(Entity2TreeDto).ToList();
                // 丢弃没有父节点的节点
                foreach (var item in alone.GroupBy(x => x.ParentId))
                {
                    data.FindAll(x => x.Id == item.Key).ForEach(x => x.Children = item.ToList());
                }
                alone.Clear();
                // 组装新查出来的节点
                foreach (MenuDto item in data)
                {
                    if (item.ParentId == null)
                    {
                        result.Add(item);
                    }
                    else if (parentMap.ContainsKey(item.ParentId.Value))
                    {
                        MenuDto? parent = parentMap.GetValueOrDefault(item.ParentId.Value);
                        if (parent.Children == null)
                        {
                            parent.Children = new List<MenuDto>();
                        }
                        parent.Children.Add(item);
                    }
                    else
                    {
                        alone.Add(item);
                    }
                }
            }
            return result;
        }

        private MenuDto Entity2TreeDto(Menu x)
        {
            return _objectMapper.Map<Menu, MenuDto>(x);
        }
    }
}