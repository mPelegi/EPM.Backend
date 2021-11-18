using EPM.Backend.BLL.Facade;
using EPM.Backend.BLL.Gatherer.Hardware;
using EPM.Backend.BLL.Gatherer.Software;
using EPM.Backend.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Backend.BLL
{
    public class MonitorBLL
    {
        private Cpu Cpu;
        private Gpu Gpu;
        private Ram Ram;
        private Drive Drive;
        private Mobo Mobo;
        private Os Os;

        private GathererFacade Gatherer;

        public MonitorBLL()
        {
            Cpu = Cpu.GetInstance();
            Gpu = Gpu.GetInstance();
            Ram = Ram.GetInstance();
            Drive = Drive.GetInstance();
            Mobo = Mobo.GetInstance();
            Os = Os.GetInstance();

            Gatherer = new GathererFacade(Cpu, Gpu, Ram, Drive, Mobo, Os);
        }

        public DescriptionDTO GetDescriptions()
        {
            return Gatherer.GetDescriptions();
        }

        public PerformanceDTO GetPerformances()
        {
            return Gatherer.GetPerformances();
        }
    }
}
