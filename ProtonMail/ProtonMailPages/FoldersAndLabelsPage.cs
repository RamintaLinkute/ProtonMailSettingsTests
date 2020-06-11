using NUnit.Framework;
using OpenQA.Selenium;
using ProtonMail.Infrastructure;
using ProtonMail.Utilities;
using System;
using System.Collections.Generic;

namespace ProtonMail.ProtonMailPages
{
    public class FoldersAndLabelsPage : PageBase
    {
        public FoldersAndLabelsPage(IWebDriver driver) : base(driver)
        {
        }

        //folders elements
        public IWebElement AddFolderButton => _driver.FindElement(By.XPath("//button[contains(text(),'Add folder')]"));
        public IWebElement SaveButton => _driver.FindElement(By.XPath("//button[contains(text(),'Save')]"));
        public IWebElement EditLastFolderButton => _driver.FindElement(By.XPath("(//section[@data-target-id='folderlist']//button[text()='Edit'])[last()]"));
        public IWebElement LastFolder => _driver.FindElement(By.XPath("(//section[@data-target-id='folderlist']//li)[last()]"));
        public IList<IWebElement> FolderActionDropDown => _driver.FindElements(By.CssSelector("[data-target-id='folderlist'] [data-test-id='dropdown:open']"));
        public IList<IWebElement> FoldersList => _driver.FindElements(By.XPath("//section[@data-target-id='folderlist']//li"));

        //labels elements
        public IList<IWebElement> LabelsList => _driver.FindElements(By.XPath("//section[@data-target-id='labellist']//tbody/tr"));
        public IWebElement AddLabelButton => _driver.FindElement(By.XPath("//button[contains(text(),'Add label')]"));
        public IList<IWebElement> LabelsActionDropDown => _driver.FindElements(By.CssSelector("[data-target-id='labellist'] [data-test-id='dropdown:open']"));
        public IWebElement LastLabel => _driver.FindElement(By.XPath("(//span[@data-test-id='folders/labels:item-name'])[last()]"));
        public IWebElement EditLastLabelButton => _driver.FindElement(By.XPath("(//tr[@data-test-id='folders/labels:item-type:label']//button[@data-test-id='folders/labels:item-edit'])[last()]"));

        //common elements
        public IWebElement NameInput => _driver.FindElement(By.Id("accountName"));
        public IWebElement LimitAlert => _driver.FindElement(By.XPath("//div[contains(@class,'notifications-container')]/div[contains(text(),'limit reached')]"));
        public IWebElement DeleteButton => _driver.FindElement(By.CssSelector("[data-test-id='folders/labels:item-delete']"));
        public IWebElement ConfirmButton => _driver.FindElement(By.XPath("//button[text()='Confirm']"));
        public IWebElement CancelButton => _driver.FindElement(By.XPath("//button[text()='Cancel']"));
        public IWebElement SameNameAlert => _driver.FindElement(By.XPath("//div[contains(@class,'notifications-container')]/div[text() = 'A label or folder with this name already exists']"));

        public FoldersAndLabelsPage CreateNewFolder(string folderName)
        {
            WaitUtils.WaitUntilVisible(AddFolderButton, _driver);
            AddFolderButton.Click();
            WaitUtils.WaitUntilVisible(NameInput, _driver);
            NameInput.SendKeys(folderName);
            SaveButton.Click();
            return this;
        }

        public FoldersAndLabelsPage VerifyLastFolder(string folderName)
        {
            WaitUtils.WaitUntilInvisible(NameInput, _driver);
            var LastFolderTitle = LastFolder.GetAttribute("title");
            Assert.IsTrue(LastFolderTitle.Equals(folderName));
            return this;
        }

        public FoldersAndLabelsPage ChangeFolderName(string folderName)
        {
            EditLastFolderButton.Click();
            WaitUtils.WaitUntilVisible(NameInput, _driver);
            NameInput.Click();
            NameInput.SendKeys(Keys.Control + "a" + Keys.Delete);
            NameInput.SendKeys(folderName);
            SaveButton.Click();
            return this;
        }

        public FoldersAndLabelsPage CreateMaxFolders(int folderLimit)
        {
            int i = 0;
            while (i < folderLimit)
            {
                CreateNewFolder(Convert.ToString(i));
                WaitUtils.WaitUntilInvisible(NameInput, _driver);
                i++;
            }
            CreateNewFolder(Convert.ToString(i));
            return this;
        }

        public bool AssertLimitAlertWarningAppears()
        {
            WaitUtils.WaitUntilVisible(LimitAlert, _driver);
            return LimitAlert.Displayed;
        }

        public FoldersAndLabelsPage CloseModal()
        {
            CancelButton.Click();
            return this;
        }

        public FoldersAndLabelsPage DeleteAllFolders()
        {
            int i = FoldersList.Count;
            WaitUtils.WaitUntilVisible(AddFolderButton, _driver);
            while (i > 0)
            {
                i--;
                WaitUtils.WaitUntilVisible(FolderActionDropDown[i], _driver);
                FolderActionDropDown[i].Click();
                WaitUtils.WaitUntilVisible(DeleteButton, _driver);
                DeleteButton.Click();
                WaitUtils.WaitUntilVisible(ConfirmButton, _driver);
                ConfirmButton.Click();
                WaitUtils.WaitUntilInvisible(ConfirmButton, _driver);
                if (FoldersList.Count == i + 1)
                {
                    WaitUtils.WaitUntilInvisible(FolderActionDropDown[i], _driver);
                }
            }
            return this;
        }

        public FoldersAndLabelsPage CreateFoldersWithSameName(string folderName)
        {
            CreateNewFolder(folderName);
            WaitUtils.WaitUntilInvisible(NameInput, _driver);
            CreateNewFolder(folderName);
            return this;
        }

        public bool AssertSameNameAlertWarningAppears()
        {
            WaitUtils.WaitUntilVisible(SameNameAlert, _driver);
            return SameNameAlert.Displayed;
        }

        public FoldersAndLabelsPage DeleteAllLabels()
        {
            int i = LabelsList.Count;
            WaitUtils.WaitUntilVisible(AddLabelButton, _driver);
            while (i > 0)
            {
                i--;
                WaitUtils.WaitUntilVisible(LabelsActionDropDown[i], _driver);
                LabelsActionDropDown[i].Click();
                WaitUtils.WaitUntilVisible(DeleteButton, _driver);
                DeleteButton.Click();
                WaitUtils.WaitUntilVisible(ConfirmButton, _driver);
                ConfirmButton.Click();
                WaitUtils.WaitUntilInvisible(ConfirmButton, _driver);
                if (LabelsList.Count == i + 1)
                {
                    WaitUtils.WaitUntilInvisible(LabelsActionDropDown[i], _driver);
                }
            }
            return this;
        }

        public FoldersAndLabelsPage CreateNewLabel(string folderName)
        {
            WaitUtils.WaitUntilVisible(AddLabelButton, _driver);
            AddLabelButton.Click();
            WaitUtils.WaitUntilVisible(NameInput, _driver);
            NameInput.SendKeys(folderName);
            SaveButton.Click();
            return this;
        }

        public FoldersAndLabelsPage CreateMaxLabels(int folderLimit)
        {
            int i = 0;
            while (i < folderLimit)
            {
                CreateNewLabel(Generator.GenerateRandomString(6));
                WaitUtils.WaitUntilInvisible(NameInput, _driver);
                i++;
            }
            CreateNewLabel(Convert.ToString(i));
            return this;
        }

        public FoldersAndLabelsPage VerifyLastLabelName(string labelName)
        {
            WaitUtils.WaitUntilInvisible(NameInput, _driver);
            var LastLabelText = LastLabel.Text;
            Assert.IsTrue(LastLabelText.Equals(labelName));
            return this;
        }

        public FoldersAndLabelsPage ChangeLabelName(string folderName)
        {
            EditLastLabelButton.Click();
            WaitUtils.WaitUntilVisible(NameInput, _driver);
            NameInput.Click();
            NameInput.SendKeys(Keys.Control + "a" + Keys.Delete);
            NameInput.SendKeys(folderName);
            SaveButton.Click();
            return this;
        }

        public FoldersAndLabelsPage CreateLabelsWithSameName(string labelName)
        {
            CreateNewLabel(labelName);
            WaitUtils.WaitUntilInvisible(NameInput, _driver);
            CreateNewLabel(labelName);
            return this;
        }
    }
}
