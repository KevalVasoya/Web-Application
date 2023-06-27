using System;
using System.ComponentModel.DataAnnotations;

namespace CommonModel.DBModel
{
    public class EmployeeDetails
    {      

        public int ID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set ; }
        public int DesignationId { get; set; }
        public string ProfilePicture { get; set; }
        public int Salary { get; set; }
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateofBirth { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }     
    
    }
}
