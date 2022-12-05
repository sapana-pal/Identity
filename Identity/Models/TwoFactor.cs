using System.ComponentModel.DataAnnotations;

namespace Identity.Models
{
    public class TwoFactor
    {
        [Required]
        public string TwoFactorCode { get; set; }
    }
}
