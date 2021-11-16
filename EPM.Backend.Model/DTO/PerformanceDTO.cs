using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Backend.Model.DTO
{
    public class PerformanceDTO
    {
        #region Hardwares
        public CpuDTO CPU { get; set; }
        public GpuDTO GPU { get; set; }
        public RamDTO RAM { get; set; }
        public List<DriveDTO> Drives { get; set; }
        #endregion
    }
}
