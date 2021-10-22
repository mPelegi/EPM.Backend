using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Backend.Model
{
    public class RamModel
    {
        #region Description
        public string PartNumber { get; set; }
        public string ClockSpeed { get; set; }
        public string Manufacturer { get; set; }
        public string Capacity { get; set; }
        public string Tag { get; set; }
        #endregion

        #region Performance
        public decimal FreeMemoryMB { get; set; }
        public decimal FreeMemoryGB { get; set; }
        public decimal TotalMemoryMB { get; set; }
        public decimal TotalMemoryGB { get; set; }
        #endregion
    }
}
