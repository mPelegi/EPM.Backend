using EPM.Backend.BLL.Gatherer.Hardware;
using EPM.Backend.BLL.Gatherer.Software;
using EPM.Backend.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EPM.Backend.BLL
{
    public class DescriptionBLL
    {
        private static DescriptionDTO Description;
        private static Cpu Cpu;
        private static Gpu Gpu;
        private static Ram Ram;
        private static Drive Drive;
        private static Mobo Mobo;
        private static Os Os;

        public DescriptionBLL()
        {
            Description = new DescriptionDTO();
            Cpu = new Cpu();
            Gpu = new Gpu();
            Ram = new Ram();
            Drive = new Drive();
            Mobo = new Mobo();
            Os = new Os();
        }

        public DescriptionDTO GetDescriptions()
        {
            Description.CPU = Cpu.GetDescription();
            Description.GPU = Gpu.GetDescription();
            Description.MotherBoard = Mobo.GetDescription();
            Description.RAMs = Ram.GetDescription();
            Description.Drives = Drive.GetDescription();
            Description.OperatingSystem = Os.GetDescription();

            return Description;
        }
    }
}
