using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDAT.Models;

namespace RDAT.ViewModels
{
    public class UpdateDriverResults
    {
        public int Driver_Id { get; set; }

        public string Driver_Name { get; set; }

        public string Company_Name { get; set; }

        public string Driver_UniqueID { get; set; }

        public string Drug_Specimen_Id { get; set; }

        public int Drug_TestingLogID { get; set; }

        public DateTime? Drug_Test_Date { get; set; }
        
        public DateTime? Drug_Results_Date { get; set; }

        public string Drug_Reported_Result { get; set; }

        public DateTime? Alcohol_Test_Date { get; set; }
        
        public DateTime? Alcohol_Results_Date { get; set; }

        public string Alcohol_Specimen_Id { get; set; }
        
        public int Alcohol_TestingLogID { get; set; }

        public string Alcohol_Reported_Result { get; set; }

        public bool Alcohol_Show { get; set; }
        
        public bool Drug_Show { get; set; }

    }
}
