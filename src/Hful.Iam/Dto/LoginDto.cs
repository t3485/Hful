namespace Hful.Iam.Dto
{
    public class LoginDto
    {
        public bool Status { get; internal set; }

        public UserDto? User { get; internal set; }
    }
}
