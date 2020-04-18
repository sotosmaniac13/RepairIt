using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairIt.Models
{
    public class Repair
    {
        [Key]
        public int Rep_Id { get; set; }

        public RepairCategory Rep_Category { get; set; }

        [Display(Name = "Client")]
        public virtual Client Rep_Client { get; set; }

        [Display(Name = "Device")]
        public Device Rep_Device { get; set; }

        [Display(Name = "Completed Tasks")]
        public List<CompletedTask> Rep_CompletedTasks { get; set; }

        [Display(Name = "Subject")]
        public string Rep_Subject { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string Rep_Description { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Additional Fixes")]
        public string Rep_AdditionalFixes { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Pending Fixes")]
        public string Rep_PendingFixes { get; set; }

        [Display(Name = "Repair Cost")]
        public decimal Rep_RepairCost { get; set; }

        [Display(Name = "Received On")]
        public DateTime? Rep_ReceivedOn { get; set; }

        [Display(Name = "Completed On")]
        public DateTime? Rep_CompletedOn { get; set; }
    }
}