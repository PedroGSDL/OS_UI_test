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
    public class EmptyFields
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
        public void IsEmptyNomeDoContato()
        {
            _webDriver.Url = "https://demo.powerserv.com.br/osfornecedorTeste/Client/notafiscal.html?55743cc0393b1cb4b8b37d09ae48d097";
            _webDriver.Navigate();
            _webDriver.FindElement(By.Id("SetorFiscal_email")).SendKeys("email@email.com");
            _webDriver.FindElement(By.Id("Contato_telefone")).SendKeys("51991347171");
            _webDriver.FindElement(By.Id("SetorFiscal_telefone")).SendKeys("51991347171");
            _webDriver.FindElement(By.Id("Nota_numero")).SendKeys("2024/01");
            _webDriver.FindElement(By.XPath("//div[@id='submit']/input")).Click();
            Thread.Sleep(5000);
            var variavelDaMensagem = _webDriver.FindElement(By.XPath("//div[@id='panel26_column1_container']//span")).Text;
            variavelDaMensagem.Should().Be("Preencha \"NOME DO CONTATO\".");
        }

        [Test]
        public void IsEmptyEmailSetorFiscal()
        {
            _webDriver.Url = "https://demo.powerserv.com.br/osfornecedorTeste/Client/notafiscal.html?55743cc0393b1cb4b8b37d09ae48d097";
            _webDriver.Navigate();
            _webDriver.FindElement(By.Id("Contato_nome")).SendKeys("NomeContato");
            _webDriver.FindElement(By.Id("Contato_telefone")).SendKeys("51991347171");
            _webDriver.FindElement(By.Id("SetorFiscal_telefone")).SendKeys("51991347171");
            _webDriver.FindElement(By.Id("Nota_numero")).SendKeys("2024/01");
            _webDriver.FindElement(By.XPath("//div[@id='submit']/input")).Click();
            Thread.Sleep(5000);
            var variavelDaMensagem = _webDriver.FindElement(By.XPath("//div[@id='panel26_column2_container']//span")).Text;
            variavelDaMensagem.Should().Be("Preencha \"EMAIL SETOR FISCAL\".");
        }

        [Test]
        public void IsEmptyTelefoneDeContato()
        {
            _webDriver.Url = "https://demo.powerserv.com.br/osfornecedorTeste/Client/notafiscal.html?55743cc0393b1cb4b8b37d09ae48d097";
            _webDriver.Navigate();
            _webDriver.FindElement(By.XPath("//div[@id='submit']/input")).Click();
            Thread.Sleep(5000);
            var variavelDaMensagem = _webDriver.FindElement(By.XPath("//span[@data-notify-text and contains(text(), 'Preencha \"TELEFONE DE CONTATO\".')]")).Text;
            variavelDaMensagem.Should().Be("Preencha \"TELEFONE DE CONTATO\".");
        }

        [Test]
        public void IsEmptyTelefoneSetorFiscal()
        {
            _webDriver.Url = "https://demo.powerserv.com.br/osfornecedorTeste/Client/notafiscal.html?55743cc0393b1cb4b8b37d09ae48d097";
            _webDriver.Navigate();
            _webDriver.FindElement(By.XPath("//div[@id='submit']/input")).Click();
            Thread.Sleep(5000);
            var variavelDaMensagem = _webDriver.FindElement(By.XPath("//span[@data-notify-text and contains(text(), 'Preencha \"TELEFONE SETOR FISCAL\".')]")).Text;
            variavelDaMensagem.Should().Be("Preencha \"TELEFONE SETOR FISCAL\".");
        }

        [Test]
        public void IsNumeroDaNotaEmpty()
        {
            _webDriver.Url = "https://demo.powerserv.com.br/osfornecedorTeste/Client/notafiscal.html?55743cc0393b1cb4b8b37d09ae48d097";
            _webDriver.Navigate();
            _webDriver.FindElement(By.XPath("//div[@id='submit']/input")).Click();
            _webDriver.FindElement(By.Id("Nota_numero")).SendKeys("2024/01");
            Thread.Sleep(5000);
            var variavelDaMensagem = _webDriver.FindElement(By.XPath("//span[@data-notify-text and contains(text(), 'Preencha \"NÚMERO DA NOTA\".')]")).Text;
            variavelDaMensagem.Should().Be("Preencha \"NÚMERO DA NOTA\".");
        }

        [Test]
        public void IsDateFieldEmpty()
        {
            var issueDate = DateTime.Today.AddMonths(1);
            _webDriver.Url = "https://demo.powerserv.com.br/osfornecedorTeste/Client/notafiscal.html?55743cc0393b1cb4b8b37d09ae48d097";
            _webDriver.Navigate().GoToUrl(_webDriver.Url);
            _webDriver.FindElement(By.Id("Emissao")).SendKeys(issueDate.ToString("dd/mm/yyyy"));
            Thread.Sleep(5000);
            IWebElement campoData = _webDriver.FindElement(By.Id("Emissao"));
            string value = campoData.GetAttribute("value");
            Assert.IsFalse(string.IsNullOrEmpty(value));

        }
    }
}
