using EPM.Backend.Model.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;

namespace EPM.Backend.BLL.Gatherer.Hardware
{
    public class Gpu
    {
        private static Gpu Instance;
        private static string VideoControllerQuery;
        private ManagementObjectSearcher VideoControllerSearcher;
        private static List<PerformanceCounter> GpuCounters;

        private Gpu()
        {
            Initialize();
            SetGpuCounters();
            GpuLoadPercentage();
        }

        public static Gpu GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Gpu();
            }

            return Instance;
        }

        private void Initialize()
        {
            VideoControllerQuery = "SELECT * FROM Win32_VideoController";
            VideoControllerSearcher = new ManagementObjectSearcher(VideoControllerQuery);
            GpuCounters = new List<PerformanceCounter>();
        }

        public GpuDTO GetDescription()
        {
            try
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
            catch (Exception e)
            {
                return null;
            }
            
        }

        public GpuDTO GetPerformance()
        {
            try
            {
                GpuDTO retorno = new GpuDTO();

                retorno.LoadPercentage = Math.Round(GpuLoadPercentage(), 2);

                return retorno;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }

        private void SetGpuCounters()
        {
            try
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
            catch (Exception e)
            {
                return;
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
