namespace DotnetAPI.Dtos
{
    partial class UserForRegistrationDto
    {
        byte[] PasswordHash {get; set;} = new byte[0];
        byte[] PasswordSalt {get; set;} = new byte[0];        
    }
}