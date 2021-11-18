using EPM.Backend.BLL.Gatherer.Hardware;
using EPM.Backend.BLL.Gatherer.Software;
using EPM.Backend.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Backend.BLL.Facade
{
    public class GathererFacade
    {
        private static DescriptionDTO Description;
        private static PerformanceDTO Performance;
        protected Cpu Cpu;
        protected Gpu Gpu;
        protected Ram Ram;
        protected Drive Drive;
        protected Mobo Mobo;
        protected Os Os;

        public GathererFacade(Cpu cpu, Gpu gpu, Ram ram, Drive drive, Mobo mobo, Os os)
        {
            Cpu = cpu;
            Gpu = gpu;
            Ram = ram;
            Drive = drive;
            Mobo = mobo;
            Os = os;

            Description = new DescriptionDTO();
            Performance = new PerformanceDTO();
        }

        public DescriptionDTO GetDescriptions()
        {
            Description.CPU = Cpu.GetDescription();
            Description.GPU = Gpu.GetDescription();
            Description.MotherBoard = Mobo.GetDescription();
            Description.RAMS = Ram.GetDescription();
            Description.Drives = Drive.GetDescription();
            Description.OperatingSystem = Os.GetDescription();

            return Description;
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
