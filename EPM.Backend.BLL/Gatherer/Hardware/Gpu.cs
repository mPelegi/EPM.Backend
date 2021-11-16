using EPM.Backend.Model.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Text;

namespace EPM.Backend.BLL.Gatherer.Hardware
{
    public class Gpu
    {
        private static string VideoControllerQuery = "SELECT * FROM Win32_VideoController";
        private ManagementObjectSearcher VideoControllerSearcher = new ManagementObjectSearcher(VideoControllerQuery);
        private static List<PerformanceCounter> GpuCounters = new List<PerformanceCounter>();

        public Gpu()
        {
            SetGpuCounters();
            GpuLoadPercentage();
        }

        public GpuDTO GetDescription()
        {
            GpuDTO retorno = new GpuDTO();

            foreach (ManagementObject obj in VideoControllerSearcher.Get())
            {
                retorno.Name = Convert.ToString(obj["Name"]);
                retorno.Manufacturer = Convert.ToString(obj["AdapterCompatibility"]);
                //Unfortunately, WMI is only able to view up to 4.3gb of video memory, in my case I have 6gb, so I'll have to take it directly from the device name 
                retorno.DedicatedMemoryGB = retorno.Name.Substring(retorno.Name.IndexOf("GB") - 2, 2).Trim();
            }

            return retorno;
        }

        public GpuDTO GetPerformance()
        {
            GpuDTO retorno = new GpuDTO();
            retorno.LoadPercentage = Math.Round(GpuLoadPercentage(), 2);


            return retorno;
        }

        private void SetGpuCounters()
        {
            PerformanceCounterCategory category = new PerformanceCounterCategory("GPU Engine");
            string[] counterNames = category.GetInstanceNames();

            foreach (string counterName in counterNames)
            {
                if (counterName.EndsWith("engtype_3D"))
                {
                    //var teste = category.GetCounters(counterName);
                    foreach (PerformanceCounter counter in category.GetCounters(counterName))
                    {
                        if (counter.CounterName == "Utilization Percentage")
                        {
                            GpuCounters.Add(counter);
                        }
                    }
                }
            }
        }

        private static decimal GpuLoadPercentage()
        {
            decimal result = 0m;
            try
            {
                GpuCounters.ForEach(x =>
                {
                    result += (decimal)x.NextValue();
                });

                return result;
            }
            catch
            {
                return 0m;
            }
        }
    }
}
