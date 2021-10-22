using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Backend.Model.DTO
{
    public class PerformanceDTO
    {
        #region Hardwares
        public CpuModel CPU { get; set; }
        public GpuModel GPU { get; set; }
        public MoboModel MotherBoard { get; set; }
        public RamModel RAM { get; set; }
        public List<DriveModel> Drives { get; set; }
        #endregion

        #region Softwares
        public OSModel OperatingSystem { get; set; }
        #endregion
    }
}
