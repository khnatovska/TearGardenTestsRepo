using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartSel
{
    public class CssSelestors
    {
        public static string addNewRepositoryBtn = "#newMainRepository";
        public static string addNewRepositoryBtnText = "#newMainRepository > span";
        public static string addNewRepositoryDialogCreatelBtn = "#createRepoButton";
        public static string addNewRepositoryDialogCancelBtn = "#createRepoCancel";
        public static string addNewRepositoryDialogRepoNameLabel = "label.control-label[for=\"Name\"";
        public static string addNewRepositoryDialogRepoName = "#RepoName";
        public static string addNewRepositoryDialogConcurOperationsLabel = "label.control-label[for=\"MaxConcurrentOperations\"";
        public static string addNewRepositoryDialogConcurOperations = "#MaximumConcurrentOperations";
        public static string addNewRepositoryDialogAddStorageLocationBtn = "#popup1 div.popup-inner-content nav.navbar-actions span";
        public static string addStorageLocationDialogSaveBtn = "#editFileSpecOK";
        public static string addStorageLocationDialogCancelBtn = "#editFileSpecCancel";
        public static string addStorageLocationDialogLocalPathRadio = "#addOnlocalDisk";
        public static string addStorageLocationDialogDataPathLabel = "label.control-label[for=\"DataPath\"";
        public static string addStorageLocationDialogDataPath = "#FileSpecDataPath";
        public static string addStorageLocationDialogMetadataPathLabel = "label.control-label[for=\"MetadataPath\"";
        public static string addStorageLocationDialogMetadataPath = "#FileSpecMetadataPath";
        public static string addStorageLocationDialogSharedPathRadio = "#addOnCifsShare";
        public static string addStorageLocationDialogUNCPathLabel = "label.control-label[for=\"UncPath\"";
        public static string addStorageLocationDialogUNCPath = "#UncPath";
        public static string addStorageLocationDialogUsernameLabel = "label.control-label[for=\"NetworkUserName\"";
        public static string addStorageLocationDialogUsername = "#FileSpecNetworkUserName";
        public static string addStorageLocationDialogPasswordLabel = "label.control-label[for=\"NetworkPassword\"";
        public static string addStorageLocationDialogPassword = "#FileSpecNetworkPassword";
        public static string addStorageLocationDialogSize = "#FileSpecSize";
        public static string addStorageLocationDialogSizeControlArrowUp = "#popup2 button.bootstrap-touchspin-up";
        public static string addStorageLocationDialogSizeControlArrowDown = "#popup2 button.bootstrap-touchspin-down";
        public static string addStorageLocationDialogSizeUnitSelectBtn = "#dropdown-wrapper-FileSpecSizeUnit > span.dropdown-multiselect-button";
        public static string addStorageLocationDialogSizeUnitDefaultValue = "#dropdown-wrapper-FileSpecSizeUnit span.dropdown-inner-text.cutText";
        public static string addStorageLocationDialogSizeUnitDropdown = "#dropdown-visual-input-FileSpecSizeUnit";
        public static string addStorageLocationDialogSizeUnitDropdownOption1 = "#dropdown-menu-FileSpecSizeUnit > ul > li:nth-child(1) > label";
        public static string addStorageLocationDialogSizeUnitDropdownOption2 = "#dropdown-menu-FileSpecSizeUnit > ul > li:nth-child(2) > label";


        public static string uiDialogLevelOneTitle = "#ui-id-3";
        public static string uiDialogLevelTwoTitle = "#ui-id-4";

        public static string uiDialogMessageBoxHeader = "#msgbox-message-header";
        public static string uiDialogMessageBoxMessage = "#msgbox-message";
        public static string uiDialogMessageBoxDefaultBtn = "#dialog div.messageboxButtonsContainer button.default";
    }
}
