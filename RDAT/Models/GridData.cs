using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Models
{
    public class GridData {
        
        public string data { get; set; }

        public int itemsCount { get; set; }

    }
}
