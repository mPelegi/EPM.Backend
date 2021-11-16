using EPM.Backend.Helpers;
using EPM.Backend.Model.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Text;

namespace EPM.Backend.BLL.Gatherer.Hardware
{
    public class Cpu
    {
        private static string ProcessorQuery = "SELECT * FROM Win32_Processor";
        private ManagementObjectSearcher ProcessorSearcher = new ManagementObjectSearcher(ProcessorQuery);
        private static PerformanceCounter PerformanceCounter = new PerformanceCounter("Processor Information", "% Processor Performance", "_Total");
        private static PerformanceCounter LoadCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        CpuDTO cpuDTO = new CpuDTO();

        public Cpu()
        {
            CpuClockPercentage();
            CpuLoadPercentage();
        }

        public CpuDTO GetDescription()
        {
            CpuDTO retorno = new CpuDTO();

            foreach (ManagementObject obj in ProcessorSearcher.Get())
            {
                retorno.Name = Convert.ToString(obj["Name"]);
                retorno.Manufacturer = Convert.ToString(obj["Manufacturer"]);
                retorno.NumberOfPhysicalCores = Convert.ToString(obj["NumberOfCores"]);
                retorno.NumberOfLogicalCores = Convert.ToString(obj["NumberOfLogicalProcessors"]);
            }

            return retorno;
        }

        public CpuDTO GetPerformance()
        {
            foreach (ManagementObject obj in ProcessorSearcher.Get())
            {
                cpuDTO.MaxClockSpeedMHz = Convert.ToDecimal(obj["MaxClockSpeed"]);
                cpuDTO.MaxClockSpeedGHz = UnitConverter.MHzToGHz((uint)obj["MaxClockSpeed"]);
                cpuDTO.ActualClockSpeedMHz = cpuDTO.MaxClockSpeedMHz * CpuClockPercentage() / 100;
                cpuDTO.ActualClockSpeedGHz = cpuDTO.MaxClockSpeedGHz * CpuClockPercentage() / 100;
                cpuDTO.LoadPercentage = Math.Round(CpuLoadPercentage(), 2);
            }

            return cpuDTO;
        }

        private decimal CpuClockPercentage()
        {
            return (decimal)PerformanceCounter.NextValue();
        }

        private decimal CpuLoadPercentage()
        {
            return (decimal)LoadCounter.NextValue();
        }
    }
}
