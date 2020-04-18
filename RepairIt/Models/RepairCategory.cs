using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RepairIt.Models
{
    public class RepairCategory
    {
        [Key]
        public int Cat_Id { get; set; }

        [Display(Name ="Category Name")]
        public string Cat_Name { get; set; }

        public List<CategoryTask> Cat_DefTasks { get; set; }
    }
}