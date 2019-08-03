using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracticalTest.Models
{
    [MetadataType(typeof(MetadataEmployee))]
    public partial class TblEmployee
    {
    }

    public class MetadataEmployee
    {

        [Required]
        //[MinLength(5, ErrorMessage = "User Name can't be less than 5 characters")]
        [MaxLength(50, ErrorMessage = "Name can't be more than 50 characters")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Name is not correct format")]
        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        [Required]
        //[MinLength(10, ErrorMessage = "User Address can't be less than 10 characters")]
        [MaxLength(100, ErrorMessage = "Address can't be more than 100 characters")]
        [Display(Name = "Address")]
        public string EmployeeAddress { get; set; }

        [Required]
        //[MinLength(6, ErrorMessage = "User Contact can't be less than 6 characters")]
        [MaxLength(20, ErrorMessage = "Contact can't be more than 12 characters")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^(((\+|00)?880)|0)(\d){10}$", ErrorMessage = "Contact is not correct format")]
        [Display(Name = "Contact")]
        public string EmployeeContactNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Make Date")]
        public System.DateTime MakeDate { get; set; }

        [Required]
        //[MinLength(4, ErrorMessage = "User Password can't be less than 4 characters")]
        [MaxLength(50, ErrorMessage = "Password can't be more than 50 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}