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
using System.Xml.Linq;

namespace OS_UI_test
{
    public class BugReport
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
            _webDriver.Quit();

        }

        [Test]
        public void FormEqualsXML()
        {

            bool validation = true;
            var doc = XDocument.Load(@"C:\Users\paula.zenkner\Desktop\Pedro\NFSE_322127_9014419_1_1.XML.xml");
            _webDriver.Url = "https://demo.powerserv.com.br/osfornecedorTeste/Client/notafiscal.html?";
            _webDriver.Navigate();

            
            _webDriver.FindElement(By.Id("Contato_nome")).SendKeys("FRANCIELLY DOS SANTOS");
            _webDriver.FindElement(By.Id("SetorFiscal_email")).SendKeys("COMPRAS.FATURAMENTO@ORSEGUPS.COM.BR");
            _webDriver.FindElement(By.Id("Contato_telefone")).SendKeys("51991347171");
            _webDriver.FindElement(By.Id("SetorFiscal_telefone")).SendKeys("40204411");
            _webDriver.FindElement(By.Id("Nota_numero")).SendKeys("322127");
            _webDriver.FindElement(By.Id("Emissao")).SendKeys(("15022024"));
            _webDriver.FindElement(By.Id("Nota_xml")).SendKeys(@"C:\Users\paula.zenkner\Desktop\Pedro\NFSE_322127_9014419_1_1.XML.xml");
            _webDriver.FindElement(By.Id("Nota_pdf")).SendKeys(@"C:\Users\paula.zenkner\Desktop\Pedro\NFSE7022.PDF.pdf");
            var cnpjReadOnly = _webDriver.FindElement(By.Id("Cnpj"));
            ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].removeAttribute('readonly')", cnpjReadOnly);
            _webDriver.FindElement(By.Id("Cnpj")).SendKeys("08491597000126");
            var emailFornecedorReadOnly = _webDriver.FindElement(By.Id("Fornecedor_email"));
            ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].removeAttribute('readonly')", emailFornecedorReadOnly);
            _webDriver.FindElement(By.Id("Fornecedor_email")).SendKeys("JOILIANE.SA@ORSEGUPS.COM.BR, COMPRAS.FATURAMENTO@ORSEGUPS.COM.BR");
            var razaoSocialReadOnly = _webDriver.FindElement(By.Id("Razaosocial"));
            ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].removeAttribute('readonly')", razaoSocialReadOnly);
            _webDriver.FindElement(By.Id("Razaosocial")).SendKeys("ORSEGUPS MONITORAMENTO ELETRONICO LTDA");
            var emailSolicitanteReadOnly = _webDriver.FindElement(By.Id("Solicitante_email"));
            ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].removeAttribute('readonly')", emailSolicitanteReadOnly);
            _webDriver.FindElement(By.Id("Solicitante_email")).SendKeys("ANA.ARRIAL@HMV.ORG.BR");
            var numSolicitacaoReadOnly = _webDriver.FindElement(By.Id("Solicitacao"));
            ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].removeAttribute('readonly')", numSolicitacaoReadOnly);
            _webDriver.FindElement(By.Id("Solicitacao")).SendKeys("7022");
            var valorNotaReadOnly = _webDriver.FindElement(By.Id("Nota_valor"));
            ((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].removeAttribute('readonly')", valorNotaReadOnly);
            _webDriver.FindElement(By.Id("Nota_valor")).SendKeys("206,723");

         /* String nomeContatoForm = _webDriver.FindElement(By.Id("Contato_nome")).GetAttribute("value").ToString();
            String setorFiscalEmailForm = _webDriver.FindElement(By.Id("SetorFiscal_email")).GetAttribute("value").ToString();
            String contatoTelefoneForm = _webDriver.FindElement(By.Id("Contato_telefone")).GetAttribute("value").ToString();
            String setorFiscalTelefoneForm = _webDriver.FindElement(By.Id("SetorFiscal_telefone")).GetAttribute("value").ToString();
            String notaNumeroForm = _webDriver.FindElement(By.Id("Nota_numero")).GetAttribute("value").ToString();
            String dataEmissaoForm = _webDriver.FindElement(By.Id("Emissao")).GetAttribute("value").ToString();
            String notaXMLForm = _webDriver.FindElement(By.Id("Nota_xml")).GetAttribute("value").ToString();
            String notaPDFForm = _webDriver.FindElement(By.Id("Nota_pdf")).GetAttribute("value").ToString(); */
            String cnpjForm = _webDriver.FindElement(By.Id("Cnpj")).GetAttribute("value").ToString();
            String razaoSocialForm = _webDriver.FindElement(By.Id("Razaosocial")).GetAttribute("value").ToString();
            String valorNotaForm = _webDriver.FindElement(By.Id("Nota_valor")).GetAttribute("value").ToString();
            String fornecedorEmailForm = _webDriver.FindElement(By.Id("Fornecedor_email")).GetAttribute("value").ToString();
            String solicitanteEmailForm = _webDriver.FindElement(By.Id("Solicitante_email")).GetAttribute("value").ToString();
            String solicitacaoForm = _webDriver.FindElement(By.Id("Solicitacao")).GetAttribute("value").ToString();

            Thread.Sleep(5000);

            var cnpjNoXmlElements = doc.Descendants("cpfcnpj");
            if (cnpjNoXmlElements.Any())
            {
                string cnpjNoXml = cnpjNoXmlElements.First().Value; 
                if (cnpjNoXml == cnpjForm)
                {
                    Console.WriteLine("O CNPJ inserido no formulário é igual ao do XML");
                    Console.WriteLine($"CNPJ no Formulário: {cnpjForm}");
                    Console.WriteLine($"CNPJ extraído do XML: {cnpjNoXml}");
                }
                else
                {
                    Console.WriteLine("O CNPJ não confere.");
                    Console.WriteLine($"CNPJ no Formulário: {cnpjForm}");
                    Console.WriteLine($"CNPJ extraído do XML: {cnpjNoXml}");
                    validation = false;
                }
            }
            else
            {
                Console.WriteLine("CNPJ não encontrado no XML.");
                validation = false;
            }

            /*var emailFornecedorNoXML = doc.Descendants("XXXXXXXXX");
            if (emailFornecedorNoXML.Any())
            {
                Console.WriteLine("O Email do Fornecedor inserido no formulário é igual ao do XML");
                Console.WriteLine($"Email do Fornecedor extraído do XML: {emailFornecedorNoXML}");
            }
            else
            {
                Console.WriteLine("O Email do Fornecedor não confere.");
                Console.WriteLine($"Email do Fornecedor extraído do XML: {emailFornecedorNoXML}");
                validation = false;
            }*/
            var razaoSocialNoXMLElements = doc.Descendants("nome_razao_social");
            if (razaoSocialNoXMLElements.Any())
            {
                string razaoSocialNoXML = razaoSocialNoXMLElements.First().Value;
                if (razaoSocialNoXML == razaoSocialForm)
                {
                    Console.WriteLine("A Razao Social é igual ao do XML");
                    Console.WriteLine($"A Razao Social no Form: {razaoSocialForm}");
                    Console.WriteLine($"A Razao Social do XML: {razaoSocialNoXML}");
                }
                else
                {
                    Console.WriteLine("A Razao Social nao confere.");
                    Console.WriteLine($"A Razao Social no Form: {razaoSocialForm}");
                    Console.WriteLine($"A Razao Social extraída do XML: {razaoSocialNoXML}");
                    validation = false;
                }
            }
            else
            {
                Console.WriteLine("CNPJ não encontrado no XML.");
                validation = false;
            }



            /*var emailSolicitanteNoXML = doc.Descendants("XXXXXXX");
            if (emailSolicitanteNoXML.Any())
            {
                Console.WriteLine("O Email do Solicitante inserido no formulário é igual ao do XML");
                Console.WriteLine($"O Email do Solicitante extraído do XML: {emailSolicitanteNoXML}");
            }
            else
            {
                Console.WriteLine("O Email do Solicitante não confere.");
                Console.WriteLine($"O Email do Solicitante extraído do XML: {emailSolicitanteNoXML}");
                validation = false;
            }*/

            var valorNotaNoXMLElements = doc.Descendants("valor_tributavel");
            if (valorNotaNoXMLElements.Any())
            {
                string valorNotaNoXML = valorNotaNoXMLElements.First().Value;
                if (valorNotaNoXML == valorNotaForm)
                {
                    Console.WriteLine("O Valor da Nota é igual ao do XML");
                    Console.WriteLine($"O Valor da Nota no Form: {valorNotaForm}");
                    Console.WriteLine($"O Valor da Nota do XML: {valorNotaNoXML}");
                }
                else
                {
                    Console.WriteLine("O Valor da Nota nao confere.");
                    Console.WriteLine($"O Valor da Nota no Form: {valorNotaForm}");
                    Console.WriteLine($"O Valor da Nota extraída do XML: {valorNotaNoXML}");
                    validation = false;
                }
            }
            else
            {
                Console.WriteLine("Valor da Nota não encontrado no XML.");
                validation = false;
            }

            if (validation == true)
            {
                Console.WriteLine("Todos os campos batem com o XML");
            }
            else
            {
                Console.WriteLine("Um ou mais campos nao batem com o XML");
            }

            Thread.Sleep(5000);

            if (validation == true)
            {
                _webDriver.FindElement(By.Id("LiberarParaEnvio_input")).Click();
                Thread.Sleep(5000);
                _webDriver.FindElement(By.XPath("//div[@id='submit']/input")).Click();
            }
        }
    }
}
