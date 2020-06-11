using NUnit.Framework;
using ProtonMail.Infrastructure;
using ProtonMail.ProtonMailPages;
using System;

namespace ProtonMail.ProtonMailTests
{
    public class FolderTests : TestBase
    {
        private LoginPage _loginPage;
        private WelcomeModalPage _welcomeModalPage;
        private LeftMenuPage _leftMenuPage;
        private FoldersAndLabelsPage _foldersAndLabelsPage;
        string newFolderName = "Create New Folder Test";
        string editedFolderName = "New Folder name";

        [OneTimeSetUp]
        public void BeforeTests()
        {
            _loginPage = new LoginPage(Driver);
            _welcomeModalPage = new WelcomeModalPage(Driver);
            _leftMenuPage = new LeftMenuPage(Driver);
            _foldersAndLabelsPage = new FoldersAndLabelsPage(Driver);

            _loginPage.NavigateToProtonMailAndLogin();
            _welcomeModalPage.CloseWelcomeModal();
            _leftMenuPage.NavigateToFoldersAndLabels();
            _foldersAndLabelsPage.DeleteAllFolders();
        }

        [Test]
        public void CreateAndEditFolder()
        {   
            _foldersAndLabelsPage.CreateNewFolder(newFolderName)
                .VerifyLastFolder(newFolderName);
            _foldersAndLabelsPage.ChangeFolderName(editedFolderName)
                .VerifyLastFolder(editedFolderName);
            _foldersAndLabelsPage.DeleteAllFolders();
        }

        [Test]
        public void FolderLimits()
        {
            _foldersAndLabelsPage.CreateMaxFolderAndCheckLimitMessage(3);
            _foldersAndLabelsPage.DeleteAllFolders();
        }

        [Test]
        public void FolderWithExistingName()
        {
            _foldersAndLabelsPage.CreateFoldersWithSameNameAndCheckAlert(newFolderName);
            _foldersAndLabelsPage.DeleteAllFolders();
        }

        [OneTimeTearDown]
        public void AfterTest()
        {          
            Driver.Close();
        }
    }
}
