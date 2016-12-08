using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Surfer.Enums;

namespace Surfer.Models
{
    public class StorageLocationModel
    {
        public string DataPath { get; }
        public string MetadataPath { get; }
        public string Username { get; }
        public string Size { get; }
        public string SizeUnit { get; }
        public StorageLocationsType Type { get; }

        public StorageLocationModel(string datapath, string metadatapath, string size, string sizeunit)
        {
            DataPath = datapath;
            MetadataPath = metadatapath;
            Size = size;
            SizeUnit = sizeunit;
            Type = StorageLocationsType.Local;
        }

        public StorageLocationModel(string networkpath, string username, string password, string size, string sizeunit)
        {
            DataPath = networkpath;
            MetadataPath = networkpath;
            Username = username;
            Size = size;
            SizeUnit = sizeunit;
            Type = StorageLocationsType.Network;
        }

        public StorageLocationModel(string datapath, string metadatapath, string size, string sizeunit, StorageLocationsType type)
        {
            DataPath = datapath;
            MetadataPath = metadatapath;
            Size = size;
            SizeUnit = sizeunit;
            Type = type;
        }
    }
}
