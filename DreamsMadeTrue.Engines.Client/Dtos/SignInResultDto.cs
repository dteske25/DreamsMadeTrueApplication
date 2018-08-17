namespace DreamsMadeTrue.Engines.Client.Dtos
{
    public class SignInResultDto
    {
        public bool Succeeded { get; set; }
        public UserDto UserInfo { get; set; }
        public string Token { get; set; }
    }
}
