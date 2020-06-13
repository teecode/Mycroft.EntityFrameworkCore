namespace Mycroft.EntityFrameworkCore.Core.Utility.Security
{
    public class PasswordOptions
    {
        public int RequiredLength { get; set; } = 10;
        public int RequiredUniqueChars { get; set; } = 4;
        public bool RequireDigit { get; set; } = true;
        public bool RequireLowercase { get; set; } = true;
        public bool RequireNonAlphanumeric { get; set; } = true;
        public bool RequireUppercase { get; set; } = true;
    }
}