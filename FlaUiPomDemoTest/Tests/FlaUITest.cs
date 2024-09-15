using FlaUiPomDemoTest.Pages;
using NUnit.Framework;

namespace FlaUiPomDemoTest.Tests
{
    public class FlaUITest : TestBase
    {

        [Test]
        public void ZoteroTest ()
        {
            var zoteroPage = new ZoteroPage(_driver.GetMainWindow("Zotero"));

            zoteroPage.ExportLibrary();


            Assert.Pass();
        }

        [Test]
        public void ZoteroTest1()
        {
            var zoteroPage = new ZoteroPage(_driver.GetMainWindow("Zotero"));

            zoteroPage.ExportLibrary();


            Assert.Pass();
        }


        [Test]
        public void ZoteroTest2()
        {
            var zoteroPage = new ZoteroPage(_driver.GetMainWindow("Zotero"));

            zoteroPage.ExportLibrary();


            Assert.Pass();
        }

        [Test]
        public void NotepadTest()
        {
            var notepadPage = new NotepadPage(_driver.GetMainWindow("WindowsNotepad"));

            notepadPage.SaveAsTxtFile();

            Assert.Pass();
        }

        [Test]
        public void NotepadTest1()
        {
            var notepadPage = new NotepadPage(_driver.GetMainWindow("WindowsNotepad"));

            notepadPage.SaveAsTxtFile();

            Assert.Pass();
        }
    }
}
