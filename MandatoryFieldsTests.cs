using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using FluentAssertions;
using System.Security.Cryptography.X509Certificates;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace OS_UI_test
{
    public class MandatoryFieldsTests
    {
        private IWebDriver _webDriver;
        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _webDriver = new ChromeDriver();
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--start-fullscreen");
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Quit();
        }

        [Test]
        public void InvalidDateNumber()
        {
            _webDriver.Url = "https://demo.powerserv.com.br/osfornecedorTeste/Client/notafiscal.html?55743cc0393b1cb4b8b37d09ae48d097";
            _webDriver.Navigate().GoToUrl(_webDriver.Url);
            _webDriver.FindElement(By.Id("Emissao")).SendKeys("23072001");
            Thread.Sleep(5000);
            var inputField = _webDriver.FindElement(By.Id("Emissao"));
            var inputValue = inputField.GetAttribute("value"); 
            string pattern = @"^\d{4}-\d{2}-\d{2}$";
            Assert.IsTrue(Regex.IsMatch(inputValue, pattern), "O valor do campo Data é inválido!"); 
        }

        [Test]
        public void InvalidPhoneNumber()
        {
            _webDriver.Url = "https://demo.powerserv.com.br/osfornecedorTeste/Client/notafiscal.html?55743cc0393b1cb4b8b37d09ae48d097";
            _webDriver.Navigate().GoToUrl(_webDriver.Url);
            _webDriver.FindElement(By.Id("Contato_telefone")).SendKeys("51982162913");
            Thread.Sleep(5000);
            var InputField = _webDriver.FindElement(By.Id("Emissao"));
            var inputVaLue = InputField.GetAttribute("value");
            string pattern = @"^\d{11,14}$";
            Assert.IsTrue(Regex.IsMatch(inputVaLue, pattern));
        }


        
    }
}


