using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RepairIt.Models
{
    public class Client
    {
        [Key]
        public int Cl_Id { get; set; }

        [Display(Name = "FirstName")]
        public string Cl_FirstName { get; set; }

        [Display(Name = "LastName")]
        public string Cl_LastName { get; set; }

        [Display(Name = "Phone")]
        public string Cl_Phone { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Notes")]
        public string Cl_Notes { get; set; }

        [Display(Name = "Client's Devices")]
        public List<Device> Cl_Devices { get; set; }
    }
}