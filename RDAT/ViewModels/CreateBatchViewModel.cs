using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RDAT.Models;

namespace RDAT.ViewModels
{
    public class CreateBatchViewModel
    {
        public CreateBatch batchRequest { get; set; }

        public List<Driver> drivers { get; set; }
    }
}
