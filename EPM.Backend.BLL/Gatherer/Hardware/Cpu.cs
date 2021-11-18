using EPM.Backend.Helpers;
using EPM.Backend.Model.DTO;
using System;
using System.Diagnostics;
using System.Management;

namespace EPM.Backend.BLL.Gatherer.Hardware
{
    public class Cpu
    {
        private static Cpu Instance;
        private static string ProcessorQuery;
        private ManagementObjectSearcher ProcessorSearcher;
        private static PerformanceCounter PerformanceCounter;
        private static PerformanceCounter LoadCounter;

        private Cpu()
        {
            Initialize();
            CpuClockPercentage();
            CpuLoadPercentage();
        }

        public static Cpu GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Cpu();
            }

            return Instance;
        }

        private void Initialize()
        {
            ProcessorQuery = "SELECT * FROM Win32_Processor";
            ProcessorSearcher = new ManagementObjectSearcher(ProcessorQuery);
            PerformanceCounter = new PerformanceCounter("Processor Information", "% Processor Performance", "_Total");
            LoadCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }

        public CpuDTO GetDescription()
        {
            try
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
            catch (Exception e)
            {
                return null;
            }
            
        }

        public CpuDTO GetPerformance()
        {
            try
            {
                CpuDTO retorno = new CpuDTO();

                foreach (ManagementObject obj in ProcessorSearcher.Get())
                {
                    retorno.MaxClockSpeedMHz = Convert.ToDecimal(obj["MaxClockSpeed"]);
                    retorno.MaxClockSpeedGHz = UnitConverter.MHzToGHz((uint)obj["MaxClockSpeed"]);
                    retorno.ActualClockSpeedMHz = retorno.MaxClockSpeedMHz * CpuClockPercentage() / 100;
                    retorno.ActualClockSpeedGHz = retorno.MaxClockSpeedGHz * CpuClockPercentage() / 100;
                    retorno.LoadPercentage = Math.Round(CpuLoadPercentage(), 2);
                }

                return retorno;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        private decimal CpuClockPercentage()
        {
            try
            {
                return (decimal)PerformanceCounter.NextValue();
            }
            catch (Exception e)
            {
                return 0;
            }
            
        }

        private decimal CpuLoadPercentage()
        {
            try
            {
                return (decimal)LoadCounter.NextValue();
            }
            catch (Exception e)
            {
                return 0;
            }
            
        }
    }
}
