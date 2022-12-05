namespace Identity.Controllers
{
    public class RegisterModel
    {
        public string Username { get; internal set; }
        public object Email { get; internal set; }
        public string Password { get; internal set; }
    }
}