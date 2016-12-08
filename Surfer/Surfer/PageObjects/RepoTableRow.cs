using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace Surfer.PageObjects
{
    public class RepoTableRow : TableRow
    {
        public string Row { get; }
        public string Name { get; }
        public int StorageLocationsNumber { get; }
        public string TreeExpander { get; }

        public RepoTableRow(IWebDriver driver, IWebElement table, string row, int storageLocationsNumber=0) : base(driver, table)
        {
            Row = row;
            Name = CssSelectors.RepoTableNameCell;
            StorageLocationsNumber = storageLocationsNumber;
            TreeExpander = CssSelectors.RepoTableTreeExpanderCell;
            Actions = CssSelectors.RepoTableActionsCell;
        }
    }
}
