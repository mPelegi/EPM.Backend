using EPM.Backend.Model.DTO;
using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace EPM.Backend.BLL.Gatherer.Software
{
    public class Os
    {
        private static Os Instance;
        private static string OperatingSystemQuery;
        private ManagementObjectSearcher OperatingSystemSearcher;

        private Os()
        {
            Initialize();
        }

        public static Os GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Os();
            }

            return Instance;
        }

        private void Initialize()
        {
            OperatingSystemQuery = "SELECT * FROM Win32_OperatingSystem";
            OperatingSystemSearcher = new ManagementObjectSearcher(OperatingSystemQuery);

        }

        public OsDTO GetDescription()
        {
            try
            {
                OsDTO retorno = new OsDTO();

                foreach (ManagementObject obj in OperatingSystemSearcher.Get())
                {
                    retorno.Name = Convert.ToString(obj["Caption"]);
                    retorno.Manufacturer = Convert.ToString(obj["Manufacturer"]);
                    retorno.CsName = Convert.ToString(obj["CSName"]);
                    retorno.RegisteredUser = Convert.ToString(obj["RegisteredUser"]);
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
