using EPM.Backend.Helpers;
using EPM.Backend.Model.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Text;

namespace EPM.Backend.BLL.Gatherer.Hardware
{
    public class Drive
    {
        private static string DiskDriveQuery = "SELECT * FROM Win32_DiskDrive";
        private ManagementObjectSearcher DiskDriveSearcher = new ManagementObjectSearcher(DiskDriveQuery);
        private DriveInfo[] AllDrives = DriveInfo.GetDrives();

        public Drive()
        {

        }

        public List<DriveDTO> GetDescription()
        {
            List<DriveDTO> retorno = new List<DriveDTO>();

            string deviceID = null;
            string drivePartition = null;
            foreach (ManagementObject obj in DiskDriveSearcher.Get())
            {
                DriveDTO drive = new DriveDTO();

                drive.Model = Convert.ToString(obj["Model"]);
                drive.InterfaceType = Convert.ToString(obj["InterfaceType"]);
                drive.MediaType = Convert.ToString(obj["MediaType"]);
                drive.Partitions = Convert.ToString(obj["Partitions"]);
                drive.Status = Convert.ToString(obj["Status"]);

                deviceID = Convert.ToString(obj["DeviceID"]).Replace(@"\", "\\");
                drivePartition = "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + deviceID + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition";

                using (ManagementObjectSearcher partitionSearch = new ManagementObjectSearcher(drivePartition))
                {
                    foreach (ManagementObject part in partitionSearch.Get())
                    {
                        drivePartition = "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='" + part["DeviceID"] + "'} WHERE AssocClass = Win32_LogicalDiskToPartition";
                        using (ManagementObjectSearcher logicalpartitionsearch = new ManagementObjectSearcher(drivePartition))
                        {
                            foreach (ManagementObject logicalpartition in logicalpartitionsearch.Get())
                            {
                                drive.LogicalDisk = Convert.ToString(logicalpartition["DeviceID"]);
                            }
                        }
                    }
                }

                retorno.Add(drive);
            }

            return retorno;
        }

        public List<DriveDTO> GetPerformance()
        {
            List<DriveDTO> retorno = new List<DriveDTO>();

            foreach (DriveInfo d in AllDrives)
            {
                DriveDTO drive = new DriveDTO();

                drive.LogicalDisk = d.Name.Replace(@"\", "");

                if (d.IsReady == true)
                {
                    drive.AvailableSizeMB = UnitConverter.ByteToMegabyte((ulong)d.TotalFreeSpace);
                    drive.TotalSizeMB = UnitConverter.ByteToMegabyte((ulong)d.TotalSize);
                    drive.AvailableSizeGB = UnitConverter.ByteToGigabyte((ulong)d.TotalFreeSpace);
                    drive.TotalSizeGB = UnitConverter.ByteToGigabyte((ulong)d.TotalSize);
                }

                retorno.Add(drive);
            }

            return retorno;
        }
    }
}
