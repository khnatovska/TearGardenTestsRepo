using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surfer.Models
{
    public class RepoModel
    {
        public string Name { get; }
        public string ConcurOperations { get; }
        public string Comments { get; }
        public int StorageLocationsNumber { get; }
        public List<StorageLocationModel> StorageLocations { get; }

        public RepoModel(string name, string operations, string comments, int storageLocationsNumber,
            List<StorageLocationModel> storageLocations)
        {
            Name = name;
            ConcurOperations = operations;
            Comments = comments;
            StorageLocationsNumber = storageLocationsNumber;
            StorageLocations = storageLocations;
        }
    }
}
