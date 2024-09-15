using FlaUiPomDemoTest.Driver;
using NUnit.Framework;
using System;

namespace FlaUiPomDemoTest.Tests
{
    public class TestBase
    {
        public FlaUIAppDriver _driver;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Console.WriteLine("Setting up tests");
        }

        [SetUp]
        public void SetUp()
        {
            _driver = new FlaUIAppDriver();
            _driver.LaunchApp("Zotero", false);
            _driver.LaunchApp("WindowsNotepad", false);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.CloseApp("WindowsNotepad");
            _driver.CloseApp("Zotero");
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Console.WriteLine("Tests completed");
        }
    }
}
