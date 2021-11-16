using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Backend.Model.DTO
{
    public class DescriptionDTO
    {
        #region Hardwares
        public CpuDTO CPU { get; set; }
        public GpuDTO GPU { get; set; }
        public MoboDTO MotherBoard { get; set; }
        public List<RamDTO> RAMs { get; set; }
        public List<DriveDTO> Drives { get; set; }
        #endregion

        #region Softwares
        public OsDTO OperatingSystem { get; set; }
        #endregion
    }
}
