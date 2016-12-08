using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using Surfer.Enums;
using Surfer.Models;

namespace Surfer.PageObjects
{
    public class StorageLocationTableRow : TableRow
    {
        public string Row { get; }
        public string DataPath { get; }
        public string MetadataPath { get; }
        public string Size { get; }

        public StorageLocationTableRow(IWebDriver driver, IWebElement table, string row) : base(driver, table)
        {
            Row = row;
            DataPath = CssSelectors.RepoDialogStorageLocationsTableDataPathCell;
            MetadataPath = CssSelectors.RepoDialogStorageLocationsTableMetadataPathCell;
            Size = CssSelectors.RepoDialogStorageLocationsTableSizeCell;
            Actions = CssSelectors.RepoDialogStorageLocationsTableActionsCell;
        }
        
        public void DataPathEquals(string expectedPath)
        {
            var row = Table.FindByCss(Row);
            var datapath = row.FindByCss(DataPath);
            Assert.AreEqual(expectedPath, datapath.Text);
        }

        public void MetadataPathEquals(string expectedPath)
        {
            var row = Table.FindByCss(Row);
            var metadatapath = row.FindByCss(MetadataPath);
            Assert.AreEqual(expectedPath, metadatapath.Text);
        }

        public void SizeInGbEquals(string expectedSize)
        {
            var row = Table.FindByCss(Row);
            var size = row.FindByCss(Size);
            Assert.AreEqual(expectedSize+" GB", size.Text);
        }
        
        public DropdownMenu GetActionsDropdown()
        {
            Driver.WaitForAjax();
            return new DropdownMenu(Driver, Row);
        }

        public string GetDataPath()
        {
            var row = Table.FindByCss(Row);
            return row.FindByCss(DataPath).Text;
        }

        public string GetMetadataPath()
        {
            var row = Table.FindByCss(Row);
            return row.FindByCss(MetadataPath).Text;
        }

        public string GetSize()
        {
            var row = Table.FindByCss(Row);
            var size = row.FindByCss(Size).Text;
            return size.Split(' ')[0];
        }

        public string GetSizeUnit()
        {
            var row = Table.FindByCss(Row);
            var size = row.FindByCss(Size).Text;
            return size.Split(' ')[1];
        }

        public StorageLocationsType GetStorageLocationType()
        {
            var datapath = GetDataPath();
            if (datapath.StartsWith("\\"))
            {
                return StorageLocationsType.Network;
            }
            return StorageLocationsType.Local;
        }

        public StorageLocationModel PrepareStorageLocationModel()
        {
            return new StorageLocationModel(GetDataPath(), GetMetadataPath(), GetSize(), GetSizeUnit(), GetStorageLocationType());
        }
    }
}
