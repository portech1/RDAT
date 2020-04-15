using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RDAT.Models
{
    public class CreateBatch
    {
        [Key]
        public int id { get; set; }
        
        public int ActivePool { get; set; }

        public int DrugPercentage { get; set; }

        [DisplayName("Drug Test Date")]
        public DateTime DrugTestDate { get; set; }

        public int AlcoholPercentage { get; set; }

        [DisplayName("Alcohol Test Date")]
        public DateTime AlcoholTestDate { get; set; }


    }
}
