using EPM.Backend.Model.DTO;
using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace EPM.Backend.BLL.Gatherer.Hardware
{
    public class Mobo
    {
        private static string MotherboardQuery = "SELECT * FROM Win32_ComputerSystem";
        private ManagementObjectSearcher MotherboardSearcher = new ManagementObjectSearcher(MotherboardQuery);

        public Mobo()
        {

        }

        public MoboDTO GetDescription()
        {
            MoboDTO retorno = new MoboDTO();

            foreach (ManagementObject obj in MotherboardSearcher.Get())
            {
                retorno.Name = Convert.ToString(obj["Model"]);
                retorno.Manufacturer = Convert.ToString(obj["Manufacturer"]);
            }

            return retorno;
        }
    }
}
