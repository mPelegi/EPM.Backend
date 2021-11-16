using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Backend.Model.DTO
{
    public class GpuDTO
    {
        #region Description
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string DedicatedMemoryGB { get; set; }
        #endregion

        #region Performance
        public decimal LoadPercentage { get; set; }
        #endregion
    }
}
