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
        private static Drive Instance;
        private static string DiskDriveQuery;
        private ManagementObjectSearcher DiskDriveSearcher;
        private DriveInfo[] AllDrives = DriveInfo.GetDrives();

        private Drive()
        {
            Initialize();
        }

        public static Drive GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Drive();
            }

            return Instance;
        }

        private void Initialize()
        {
            DiskDriveQuery = "SELECT * FROM Win32_DiskDrive";
            DiskDriveSearcher = new ManagementObjectSearcher(DiskDriveQuery);
        }        

        public List<DriveDTO> GetDescription()
        {
            try
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
            catch (Exception e)
            {
                return null;
            }
            
        }

        public List<DriveDTO> GetPerformance()
        {
            try
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
            catch (Exception e)
            {
                return null;
            }
            
        }
    }
}
