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
        private static string MemoryQuery = "SELECT * FROM Win32_PhysicalMemory";
        private static string OperatingSystemQuery = "SELECT * FROM Win32_OperatingSystem";
        private ManagementObjectSearcher MemorySearcher = new ManagementObjectSearcher(MemoryQuery);
        private ManagementObjectSearcher OperatingSystemSearcher = new ManagementObjectSearcher(OperatingSystemQuery);

        public Ram()
        {

        }

        public List<RamDTO> GetDescription()
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

        public RamDTO GetPerformance()
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
    }
}
