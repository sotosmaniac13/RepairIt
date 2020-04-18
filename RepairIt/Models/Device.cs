using System.ComponentModel.DataAnnotations;

namespace RepairIt.Models
{
    public class Device
    {
        [Key]
        public int Dev_Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Dev_Name { get; set; }

        [Display(Name = "Device Owner")]
        public virtual Client Dev_Owner { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Notes")]
        public string Dev_Notes { get; set; }
    }
}