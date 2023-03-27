namespace Hful.Iam.Dto
{
    public class MenuDto
    {
        public List<MenuDto> Children { get; set; }

        public Guid? ParentId { get; set; }

        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
