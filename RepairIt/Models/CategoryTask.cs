using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RepairIt.Models
{
    public class CategoryTask
    {
        [Key]
        public int CatTask_Id { get; set; }

        public virtual RepairCategory CatTask_CategoryOfThisTask { get; set; }

        [Display(Name = "Task Name")]
        public string CatTask_Name { get; set; }

        [Display(Name = "Task Value Type")]
        public PropertyTypes CatTask_Type { get; set; }
    }

    public enum PropertyTypes
    {
        [Display(Name = "Text")]
        String,
        [Display(Name = "True/False")]
        Boolean
    }
}