using EPM.Backend.Helpers;
using EPM.Backend.Model.DTO;
using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace EPM.Backend.BLL.Gatherer.Hardware
{
    public class Ram
    {
        private static Ram Instance;
        private static string MemoryQuery;
        private static string OperatingSystemQuery;
        private ManagementObjectSearcher MemorySearcher;
        private ManagementObjectSearcher OperatingSystemSearcher;

        private Ram()
        {
            Initialize();
        }

        public static Ram GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Ram();
            }

            return Instance;
        }

        private void Initialize()
        {
            MemoryQuery = "SELECT * FROM Win32_PhysicalMemory";
            OperatingSystemQuery = "SELECT * FROM Win32_OperatingSystem";
            MemorySearcher = new ManagementObjectSearcher(MemoryQuery);
            OperatingSystemSearcher = new ManagementObjectSearcher(OperatingSystemQuery);
        }

        public List<RamDTO> GetDescription()
        {
            try
            {
                List<RamDTO> retorno = new List<RamDTO>();

                foreach (ManagementObject obj in MemorySearcher.Get())
                {
                    RamDTO ramAux = new RamDTO();

                    ramAux.PartNumber = Convert.ToString(obj["PartNumber"]);
                    ramAux.ClockSpeed = Convert.ToString(obj["ConfiguredClockSpeed"]);
                    ramAux.Manufacturer = Convert.ToString(obj["Manufacturer"]);
                    ramAux.Capacity = UnitConverter.ToConvert((ulong)obj["Capacity"]);
                    ramAux.Tag = Convert.ToString(obj["Tag"]);

                    retorno.Add(ramAux);
                }

                return retorno;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        public RamDTO GetPerformance()
        {
            try
            {
                RamDTO retorno = new RamDTO();

                foreach (ManagementObject obj in OperatingSystemSearcher.Get())
                {
                   retorno.FreeMemoryMB = UnitConverter.KilobyteToMegabyte((ulong)obj["FreePhysicalMemory"]);
                   retorno.TotalMemoryMB = UnitConverter.KilobyteToMegabyte((ulong)obj["TotalVisibleMemorySize"]);
                   retorno.FreeMemoryGB = UnitConverter.KilobyteToGigabyte((ulong)obj["FreePhysicalMemory"]);
                   retorno.TotalMemoryGB = UnitConverter.KilobyteToGigabyte((ulong)obj["TotalVisibleMemorySize"]);
                }

                return retorno;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }
    }
}
