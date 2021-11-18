using EPM.Backend.Model.DTO;
using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace EPM.Backend.BLL.Gatherer.Hardware
{
    public class Mobo
    {
        private static Mobo Instance;
        private static string MotherboardQuery;
        private ManagementObjectSearcher MotherboardSearcher;

        private Mobo()
        {
            Initialize();
        }

        public static Mobo GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Mobo();
            }

            return Instance;
        }

        private void Initialize()
        {
            MotherboardQuery = "SELECT * FROM Win32_ComputerSystem";
            MotherboardSearcher = new ManagementObjectSearcher(MotherboardQuery);

        }

        public MoboDTO GetDescription()
        {
            try
            {
                MoboDTO retorno = new MoboDTO();

                foreach (ManagementObject obj in MotherboardSearcher.Get())
                {
                    retorno.Name = Convert.ToString(obj["Model"]);
                    retorno.Manufacturer = Convert.ToString(obj["Manufacturer"]);
                }

                return retorno;
            }
            catch (Exception e)
            {
                return null;
            }
            
        }
    }
}
