using EPM.Backend.Model.DTO;
using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace EPM.Backend.BLL.Gatherer.Software
{
    public class Os
    {
        private static string OperatingSystemQuery = "SELECT * FROM Win32_OperatingSystem";
        private ManagementObjectSearcher OperatingSystemSearcher = new ManagementObjectSearcher(OperatingSystemQuery);

        public Os()
        {

        }

        public OsDTO GetDescription()
        {
            OsDTO retorno = new OsDTO();

            foreach (ManagementObject obj in OperatingSystemSearcher.Get())
            {
                retorno.Name = Convert.ToString(obj["Caption"]);
                retorno.Manufacturer = Convert.ToString(obj["Manufacturer"]);
                retorno.CSName = Convert.ToString(obj["CSName"]);
                retorno.RegisteredUser = Convert.ToString(obj["RegisteredUser"]);
            }

            return retorno;
        }
    }
}
