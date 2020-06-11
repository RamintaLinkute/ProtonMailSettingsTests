using NUnit.Framework;
using ProtonMail.Infrastructure;
using ProtonMail.ProtonMailPages;
using ProtonMail.Utilities;

namespace ProtonMail.ProtonMailTests
{ 
    public class LabelTests : TestBase
    {
        private LoginPage _loginPage;
        private WelcomeModalPage _welcomeModalPage;
        private LeftMenuPage _leftMenuPage;
        private FoldersAndLabelsPage _foldersAndLabelsPage;
        string newLabelName = Generator.GenerateRandomString(8);
        string editedLabelName = Generator.GenerateRandomString(5);

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
            _foldersAndLabelsPage.DeleteAllLabels();
        }

        [Test]
        public void CreateAndEditLabel()
        {
            _foldersAndLabelsPage.CreateNewLabel(newLabelName)
                .VerifyLastLabelName(newLabelName)
                .ChangeLabelName(editedLabelName)
                .VerifyLastLabelName(editedLabelName)
                .DeleteAllLabels();
        }

        [Test]
        public void LabelLimits()
        {
            _foldersAndLabelsPage.CreateMaxLabels(20);
            Assert.IsTrue(_foldersAndLabelsPage.AssertLimitAlertWarningAppears(), "Alert is not shown");
            _foldersAndLabelsPage.CloseModal()
                .DeleteAllLabels();
        }

        [Test]
        public void LabelWithExistingName()
        {
            _foldersAndLabelsPage.CreateLabelsWithSameName(newLabelName);
            Assert.IsTrue(_foldersAndLabelsPage.AssertSameNameAlertWarningAppears(), "Alert is not shown");
            _foldersAndLabelsPage.CloseModal()
                .DeleteAllLabels();
        }

        [OneTimeTearDown]
        public void AfterTest()
        {
            Driver.Close();
        }
    }
}
