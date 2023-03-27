namespace Hful.Iam.Api.Dto.Authorization
{
    public class LoginRequestDto
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Code { get; set; }
    }
}
