using System.ComponentModel.DataAnnotations;

namespace RepairIt.Models
{
    public class CompletedTask
    {
        [Key]
        public int Task_Id { get; set; }

        public CategoryTask Task_DefaultTask { get; set; }

        public bool Task_BoolValue { get; set; }

        public string Task_StringValue { get; set; }
    }
}