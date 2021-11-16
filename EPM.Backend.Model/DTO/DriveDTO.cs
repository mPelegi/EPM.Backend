using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Backend.Model.DTO
{
    public class DriveDTO
    {
        #region Description
        public string Model { get; set; }
        public string InterfaceType { get; set; }
        public string MediaType { get; set; }
        public string Partitions { get; set; }
        public string Status { get; set; }
        #endregion

        #region Performance
        public string LogicalDisk { get; set; }
        public decimal TotalSizeMB { get; set; }
        public decimal TotalSizeGB { get; set; }
        public decimal AvailableSizeMB { get; set; }
        public decimal AvailableSizeGB { get; set; }
        #endregion
    }
}
