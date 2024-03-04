using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using FluentAssertions;

namespace OS_UI_test
{
    public class DatesTests
{

        private IWebDriver _webDriver;
        [SetUp]
        public void Setup()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            _webDriver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            _webDriver.Close();
            _webDriver.Quit();
            _webDriver.Dispose();

        }

        [Test]
        public void IssueDateForNextMonth_ReturnInvalidMessage()
        {
            var issueDate = DateTime.Today.AddMonths(1);
            //gera via API a url
            _webDriver.Url = "https://demo.powerserv.com.br/osfornecedorTeste/Client/notafiscal.html?55743cc0393b1cb4b8b37d09ae48d097";
            _webDriver.Navigate();

            _webDriver.FindElement(By.Id("Contato_nome")).SendKeys("NomeContato");
            _webDriver.FindElement(By.Id("SetorFiscal_email")).SendKeys("email@email.com");
            _webDriver.FindElement(By.Id("Contato_telefone")).SendKeys("51991347171");
            _webDriver.FindElement(By.Id("SetorFiscal_telefone")).SendKeys("51991347171");
            _webDriver.FindElement(By.Id("Nota_numero")).SendKeys("2024/01");
            _webDriver.FindElement(By.Id("Emissao")).SendKeys(issueDate.ToString("dd/mm/yyyy"));
            _webDriver.FindElement(By.XPath("//div[@id='submit']/input")).Click();

            Thread.Sleep(5000);
            var variavelDaMensagem = _webDriver.FindElement(By.XPath("//div[@id='panel31_column3_container']//span")).Text;

            variavelDaMensagem.Should().Be("Prezado Usuário, o envio de notas não é mais permitido, tente novamente no próximo periodo fiscal que inicia em 01/2");
        }

        [Test]
        public void IssueDateForMonthBefore_ReturnInvalidMessage()
        {
            var issueDate = DateTime.Today.AddMonths(-1);



            _webDriver.Url = "https://demo.powerserv.com.br/osfornecedorTeste/Client/notafiscal.html?55743cc0393b1cb4b8b37d09ae48d097";
            _webDriver.Navigate();

            _webDriver.FindElement(By.Id("Contato_nome")).SendKeys("NomeContato");
            _webDriver.FindElement(By.Id("SetorFiscal_email")).SendKeys("email@email.com");
            _webDriver.FindElement(By.Id("Contato_telefone")).SendKeys("51991347171");
            _webDriver.FindElement(By.Id("SetorFiscal_telefone")).SendKeys("51991347171");
            _webDriver.FindElement(By.Id("Nota_numero")).SendKeys("2024/01");
            _webDriver.FindElement(By.Id("Emissao")).SendKeys(issueDate.ToString("dd/mm/yyyy"));
            _webDriver.FindElement(By.XPath("//div[@id='submit']/input")).Click();

            Thread.Sleep(5000);
            var variavelDaMensagem = _webDriver.FindElement(By.XPath("//div[@id='panel31_column3_container']//span")).Text;

            variavelDaMensagem.Should().Be("Prezado Usuário, o envio de notas não é mais permitido, pois passou do periodo de 4 dias uteis após a emissão");
        }
    }
}