using FlaUI.Core.AutomationElements;

namespace FlaUiPomDemoTest.Pages
{
    public class NotepadPage
    {
        private readonly Window mainWindow;

        public NotepadPage(Window mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        // NotepadPage selectors
        private Button FilePopup => mainWindow.FindFirstDescendant(cf => cf.ByName("File")).AsButton();

        private Button SaveAsButton => mainWindow.FindFirstDescendant(cf => cf.ByAutomationId("41008")).AsButton();



        public void SaveAsTxtFile()
        {
            mainWindow.Focus();
            FilePopup.Click();
            SaveAsButton.Click();
        }
    }
}
