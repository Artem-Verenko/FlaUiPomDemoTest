using FlaUI.Core.AutomationElements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlaUiPomDemoTest.Pages
{
    public class ZoteroPage
    {
        private readonly Window mainWindow;

        public ZoteroPage(Window mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        // Zotero selectors
        private Button FilePopup => mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("fileMenu")).AsButton();


        public void ExportLibrary()
        {
            mainWindow.Focus();
            FilePopup.Click();
        }


    }
}
