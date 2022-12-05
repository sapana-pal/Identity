using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Identity.Models
{
    public class Login
    {
        //[Required,EmailAddress]
        //[Display(Name = "Email / Username")]
        //public string Email { get; set; }

        //[Required]
        //public string Password { get; set; }

        //public string ReturnUrl { get; set; }
        //[Display(Name="Remember Me")]
        //public bool Remember { get; set; }

        [Required]
        public string UsernameOrEmail
        {
            get;
            set;
        }
        [Required, DataType(DataType.Password)]
        public string Password
        {
            get;
            set;
        }
        public bool RememberMe
        {
            get;
            set;
        }


        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }

   
}
