using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Models
{
    [Table("TblCustomer")]
    [Bind("CustId,Name,Address,Email,MobileNo,DateOfBirth,Custom")]
    public class Customer
    {
        [Key]
        public int CustId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Custom { get; set; }
    }
}
