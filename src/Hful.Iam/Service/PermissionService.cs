using Hful.Core.Context;
using Hful.Core.Mapper;
using Hful.Domain;
using Hful.Domain.Iam;
using Hful.Iam.Domain;
using Hful.Iam.Dto;

namespace Hful.Iam.Service
{
    internal class PermissionService : IPermissionService
    {
        private readonly IObjectMapper _objectMapper;
        private readonly IRepository<Menu> _menuRepsoitory;
        private readonly IAsyncExecutor _asyncExecutor;
        private readonly ICurrentUser _currentUser;
        private readonly IRepository<RolePermission> _rolePermissionRepository;
        private readonly IRepository<UserPermission> _userPermissionRepository;

        public PermissionService(IObjectMapper objectMapper,
            IRepository<Menu> menuRepsoitory,
            IAsyncExecutor asyncExecutor,
            ICurrentUser currentUser,
            IRepository<RolePermission> rolePermissionRepository,
            IRepository<UserPermission> userPermissionRepository)
        {
            _objectMapper = objectMapper;
            _menuRepsoitory = menuRepsoitory;
            _asyncExecutor = asyncExecutor;
            _currentUser = currentUser;
            _rolePermissionRepository = rolePermissionRepository;
            _userPermissionRepository = userPermissionRepository;
        }

        public async Task<List<MenuDto>> GetMenuAsync(Guid? tenantId, Guid userId)
        {
            var querable = _menuRepsoitory.AsQueryable();
            if (tenantId != null)
            {
                querable = querable.Where(x => x.TenantId == tenantId);
            }

            if (!_currentUser.IsSuperAdmin)
            {
                var roleIds = _currentUser.RoleIds;
                var rolePermissionIds = (await _asyncExecutor.ToListAsync(_rolePermissionRepository.AsQueryable().Where(x => roleIds.Contains(x.RoleId))))
                    .Select(x => x.PermissionId);

                var userPermissionIds = (await _asyncExecutor.ToListAsync(_userPermissionRepository.AsQueryable().Where(x => x.UserId == _currentUser.Id)))
                    .Select(x => x.PermissionId);

                var permissionIds = new HashSet<Guid>(rolePermissionIds);
                permissionIds.UnionWith(userPermissionIds);

                querable = querable.Where(x => permissionIds.Contains(x.Id));
            }

            var data = (await _asyncExecutor.ToListAsync(querable));
            return await ToTree(data);
        }

        public async Task<List<string>> GetPermissionAsync(Guid? tenantId, Guid userId)
        {
            var menu = await GetMenuAsync(tenantId, userId);
            Stack<MenuDto> stack = new Stack<MenuDto>(menu);
            List<string> permissions = new List<string>();

            while (stack.Count > 0)
            {
                var data = stack.Pop();
                permissions.Add(data.Code);
                if (data.Children?.Count > 0)
                {
                    foreach (var item in data.Children)
                        stack.Push(item);
                }
            }

            return permissions;
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
                else if (parentMap.TryGetValue(item.ParentId.Value, out var parent))
                {
                    parent.Children ??= new List<MenuDto>();
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
                    else if (parentMap.TryGetValue(item.ParentId.Value, out var parent))
                    {
                        parent.Children ??= new List<MenuDto>();
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