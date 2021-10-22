using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Backend.Model
{
    public class CpuModel
    {
        #region Description
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public string NumberOfPhysicalCores { get; set; }
        public string NumberOfLogicalCores { get; set; }
        #endregion

        #region Performance
        public decimal MaxClockSpeedGHz { get; set; }
        public decimal ActualClockSpeedGHz { get; set; }
        public decimal MaxClockSpeedMHz { get; set; }
        public decimal ActualClockSpeedMHz { get; set; }
        public decimal LoadPercentage { get; set; }
        #endregion


    }
}
