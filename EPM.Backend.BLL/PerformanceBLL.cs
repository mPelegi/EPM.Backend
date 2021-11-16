using EPM.Backend.BLL.Gatherer.Hardware;
using EPM.Backend.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Backend.BLL
{
    public class PerformanceBLL
    {
        private static PerformanceDTO Performance;
        private static Cpu Cpu;
        private static Gpu Gpu;
        private static Ram Ram;
        private static Drive Drive;


        public PerformanceBLL()
        {
            Performance = new PerformanceDTO();
            Cpu = new Cpu();
            Gpu = new Gpu();
            Ram = new Ram();
            Drive = new Drive();
        }

        public PerformanceDTO GetPerformances()
        {
            Performance.CPU = Cpu.GetPerformance();
            Performance.GPU = Gpu.GetPerformance();
            Performance.RAM = Ram.GetPerformance();
            Performance.Drives = Drive.GetPerformance();

            return Performance;
        }
    }
}
