using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PracticalTest.Models
{
    [MetadataType(typeof(MetadataDesignation))]
    public partial class TblDesignation 
    {
    }
    public class MetadataDesignation
    {
        [Required]
        //[MinLength(5, ErrorMessage = "User Designation Name can't be less than 5 characters")]
        [MaxLength(50, ErrorMessage = "Designation Name can't be more than 50 characters")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Name is not correct format")]
        [Display(Name = "Designation Name")]
        public string DesignationName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Make Date")]
        public System.DateTime MakeDate { get; set; }
    }
   
}