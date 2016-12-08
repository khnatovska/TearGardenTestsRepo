using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Surfer
{
    public class CssSelectors
    {
        //Repo page selectors
        public static string NewRepoBtn = "#newMainRepository > span";
        public static string RepoTable = "#dvmRepositoriesGrid";
        public static string RepoTableRow = "#dvmRepositoriesGrid tr";
        public static string RepoTableTreeExpanderCell = "td[aria-describedby='dvmRepositoriesGrid_Icon'] div.tree-wrap";
        public static string RepoTableNameCell = "td[aria-describedby='dvmRepositoriesGrid_Name'] span";
        public static string RepoTableActionsCell = "td[aria-describedby='dvmRepositoriesGrid_actions']";

        //Repo dialog selectors
        public static string RepoDialogRepoNameLabel = "label.control-label[for=\"Name\"";
        public static string RepoDialogRepoName = "#RepoName";
        public static string RepoDialogConcurOperationsLabel = "label.control-label[for=\"MaxConcurrentOperations\"";
        public static string RepoDialogConcurOperations = "#MaximumConcurrentOperations";
        public static string RepoDialogCommentsLabel = "label.control-label[for=\"Comment\"";
        public static string RepoDialogComments = "#RepoComments";
        public static string RepoDialogAddStorageLocationBtn = "#popup1 div.popup-inner-content nav.navbar-actions span";
        public static string RepoDialogStorageLocationsTable = "#fileSpecificationsGrid";
        public static string RepoDialogStorageLocationsTableRow = "#fileSpecificationsGrid tr";
        public static string RepoDialogStorageLocationsTableDataPathCell =
            "td[aria-describedby='fileSpecificationsGrid_DataPath']";
        public static string RepoDialogStorageLocationsTableMetadataPathCell =
            "td[aria-describedby='fileSpecificationsGrid_MetadataPath']";
        public static string RepoDialogStorageLocationsTableSizeCell =
            "td[aria-describedby='fileSpecificationsGrid_SizeString']";
        public static string RepoDialogStorageLocationsTableActionsCell =
           "td[aria-describedby='fileSpecificationsGrid_actions']";
        public static string RepoDialogStorageLocationsTableActionsBtn =
           "td[aria-describedby='fileSpecificationsGrid_actions'] button";
        public static string RepoDialogStorageLocationsTableActionsDropdown =
           "td[aria-describedby='fileSpecificationsGrid_actions'] ul";
        public static string RepoDialogStorageLocationsTableNoData =
            "#gview_fileSpecificationsGrid div.no-data-to-display";
        public static string RepoDialogCreateBtn = "#createRepoButton";
        public static string RepoDialogCancelBtn = "#createRepoCancel";

        //Storage Location Dialog selectors
        public static string StorageLocationDialogLocalPathRadio = "#addOnlocalDisk";
        public static string StorageLocationDialogDataPathLabel = "label.control-label[for=\"DataPath\"";
        public static string StorageLocationDialogDataPath = "#FileSpecDataPath";
        public static string StorageLocationDialogMetadataPathLabel = "label.control-label[for=\"MetadataPath\"";
        public static string StorageLocationDialogMetadataPath = "#FileSpecMetadataPath";
        public static string StorageLocationDialogNetworkPathRadio = "#addOnCifsShare";
        public static string StorageLocationDialogUNCPathLabel = "label.control-label[for=\"UncPath\"";
        public static string StorageLocationDialogUNCPath = "#UncPath";
        public static string StorageLocationDialogUsernameLabel = "label.control-label[for=\"NetworkUserName\"";
        public static string StorageLocationDialogUsername = "#FileSpecNetworkUserName";
        public static string StorageLocationDialogPasswordLabel = "label.control-label[for=\"NetworkPassword\"";
        public static string StorageLocationDialogPassword = "#FileSpecNetworkPassword";
        public static string StorageLocationDialogSizeUnitSelectBtn = "#dropdown-wrapper-FileSpecSizeUnit > span.dropdown-multiselect-button";
        public static string StorageLocationDialogSizeUnitValue = "#dropdown-wrapper-FileSpecSizeUnit span.dropdown-inner-text.cutText";
        public static string StorageLocationDialogSizeUnitDropdown = "#dropdown-menu-FileSpecSizeUnit";
        public static string StorageLocationDialogSize = "#FileSpecSize";
        public static string StorageLocationDialogSizeControlUp = "#popup2 button.bootstrap-touchspin-up";
        public static string StorageLocationDialogSizeControlDown = "#popup2 button.bootstrap-touchspin-down";
        public static string StorageLocationDialogDetailsToggle = "#toggleDetailsLink";
        public static string StorageLocationDialogDetailsCashingPolicyDropdown = "#dropdown-visual-input-FileSpecCachingPolicy";
        public static string StorageLocationDialogDetailsBytesPerSectorDropdown = "#dropdown-visual-input-FileSpecBytesPerSector";
        public static string StorageLocationDialogDetailsAvrgBytesPerSector = "#FileSpecAverageBytesPerRecord";
        public static string StorageLocationDialogSaveBtn = "#editFileSpecOK";
        public static string StorageLocationDialogCancelBtn = "#editFileSpecCancel";

        //UI widget level one selectors
        public static string uiDialogLevelOne = "div.ui-dialog[aria-labelledby=\"ui-id-3\"";
        public static string uiDialogLevelOneTitle = "#ui-id-3";

        //UI widget level two selectors
        public static string uiDialogLevelTwo = "div.ui-dialog[aria-labelledby=\"ui-id-4\"";
        public static string uiDialogLevelTwoTitle = "#ui-id-4";

        //UI widget message box selectors
        public static string uiDialogMessageBox = "div.ui-dialog[aria-labelledby=\"ui-id-2\"";
        public static string uiDialogMessageBoxHeader = "#msgbox-message-header";
        public static string uiDialogMessageBoxMessage = "#msgbox-message";
        public static string uiDialogMessageBoxMessageParagraph = "#msgbox-message > p";
        public static string uiDialogMessageBoxDefaultBtn = "#dialog div.messageboxButtonsContainer button.default";


        //relative selectors for DropdownMenu
        public static string DropdownMenuOptions = "div > ul > li > a";
        public static string DropdownMenuBtn = "button";
        public static string DropdownMenuDropdown = "ul.dropdown-menu";

        

        public static string GetChild(string selector, int number)
        {
            var child = string.Format(":nth-child({0})", number);
            return selector + child;
        }
        
        
    }
}
