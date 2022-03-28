using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prs_server.Model {
    public class Po {
        [Key]
        public Vendor Vendor { get; set; }
        public IEnumerable<Poline>Polines { get; set; }
        public decimal PoTotal { get; set; }
        
       
    
    }
}
