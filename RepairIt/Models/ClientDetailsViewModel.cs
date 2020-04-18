using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepairIt.Models
{
    public class ClientDetailsViewModel
    {
        public Client Client { get; set; }
        public IEnumerable<Device> ClientDevices { get; set; }
    }
}