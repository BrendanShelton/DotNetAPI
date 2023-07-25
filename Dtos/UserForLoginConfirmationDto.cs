namespace DotnetAPI.Dtos
{
    public partial class UserForRegistrationDto
    {
        public byte[] PasswordHash {get; set;} = new byte[0];
        public byte[] PasswordSalt {get; set;} = new byte[0];        
    }
}