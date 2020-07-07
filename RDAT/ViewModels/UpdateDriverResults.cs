using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        
        public List<DrugResult> DrugResults { get; set; }
        
        public List<AlcoholResult> AlcoholResults { get; set; }

        public bool Alcohol_Show { get; set; }
        
        public bool Drug_Show { get; set; }

    }

    public class DrugResult
    {
        public int Drug_Specimen_Id { get; set; }

        public int Drug_TestingLogID { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Drug_Test_Date { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Drug_Results_Date { get; set; }

        public string Drug_Reported_Result { get; set; }

        public int BatchID { get; set; }
    }

    public class AlcoholResult
    {
        public int Alcohol_Specimen_Id { get; set; }

        public int Alcohol_TestingLogID { get; set; }


        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Alcohol_Test_Date { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? Alcohol_Results_Date { get; set; }
           

        public string Alcohol_Reported_Result { get; set; }

        public int BatchID { get; set; }
    }

}
