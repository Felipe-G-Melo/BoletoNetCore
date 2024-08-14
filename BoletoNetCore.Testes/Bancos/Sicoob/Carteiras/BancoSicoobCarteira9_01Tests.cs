using System;
using NUnit.Framework;

namespace BoletoNetCore.Testes
{
    [TestFixture]
    [Category("Sicoob Carteira 9 Var 01")]
    public class BancoSicoobCarteira901Tests
    {
        readonly IBanco _banco;
        public BancoSicoobCarteira901Tests()
        {
            var contaBancaria = new ContaBancaria
            {
                Agencia = "4277",
                DigitoAgencia = "3",
                Conta = "6498",
                DigitoConta = "0",
                CarteiraPadrao = "9",
                VariacaoCarteiraPadrao = "01",
                TipoCarteiraPadrao = TipoCarteira.CarteiraCobrancaSimples,
                TipoFormaCadastramento = TipoFormaCadastramento.ComRegistro,
                TipoImpressaoBoleto = TipoImpressaoBoleto.Empresa
            };
            _banco = Banco.Instancia(Bancos.Sicoob);
            _banco.Beneficiario = TestUtils.GerarBeneficiario("17227", "8", "", contaBancaria);
            _banco.FormataBeneficiario();
        }

        [Test]
        public void Sicoob_9_01_REM240()
        {
            TestUtils.TestarHomologacao(_banco, TipoArquivo.CNAB240, nameof(BancoSicoobCarteira901Tests), 5, false, "?", 223344);
        }

        [TestCase(300, "1", "BO123456D", "1", "0000001-9", "75691697400000300009427701017227800000019001", "75699.42779 01017.227800 00000.190017 1 69740000030000", 2016, 11, 10)]
        [TestCase(300, "2", "BO123456D", "1", "0000002-6", "75691711100000300009427701017227800000026001", "75699.42779 01017.227800 00000.260018 1 71110000030000", 2017, 03, 27)]
        [TestCase(400, "3", "BO123456D", "1", "0000003-3", "75691714200000400009427701017227800000033001", "75699.42779 01017.227800 00000.330019 1 71420000040000", 2017, 04, 27)]
        [TestCase(500, "4", "BO123456F", "3", "0000004-0", "75693703000000500009427701017227800000040001", "75699.42779 01017.227800 00000.400010 3 70300000050000", 2017, 01, 05)]
        [TestCase(900, "5", "BO123456F", "8", "0000005-8", "75698729500000900009427701017227800000058001", "75699.42779 01017.227800 00000.580019 8 72950000090000", 2017, 09, 27)]
        [TestCase(600, "6", "BO123456G", "3", "0000006-5", "75693706300000600009427701017227800000065001", "75699.42779 01017.227800 00000.650010 3 70630000060000", 2017, 2, 07)]
        [TestCase(4011.24, "12349", "BO123456F", "2", "0012349-2", "75692702200004011249427701017227800123492001", "75699.42779 01017.227800 01234.920013 2 70220000401124", 2016, 12, 28)]
        [TestCase(700, "8", "BO123456B", "3", "0000008-0", "75693709500000700009427701017227800000080001", "75699.42779 01017.227800 00000.800011 3 70950000070000", 2017, 3, 11)]
        [TestCase(409, "9", "BO123456E", "7", "0000009-7", "75697700200000409009427701017227800000097001", "75699.42779 01017.227800 00000.970012 7 70020000040900", 2016, 12, 08)]
        public void Deve_criar_boleto_sicoob_9_01_com_digito_verificador_valido(decimal valorTitulo, string nossoNumero, string numeroDocumento, string digitoVerificador, string nossoNumeroFormatado, string codigoDeBarras, string linhaDigitavel, params int[] anoMesDia)
        {
            //Ambiente
            var boleto = new Boleto(_banco)
            {
                DataVencimento = new DateTime(anoMesDia[0], anoMesDia[1], anoMesDia[2]),
                ValorTitulo = valorTitulo,
                NossoNumero = nossoNumero,
                NumeroDocumento = numeroDocumento,
                EspecieDocumento = TipoEspecieDocumento.DM,
                Pagador = TestUtils.GerarPagador()
            };

            //Ação
            boleto.ValidarDados();

            //Assertivas
            Assert.That(boleto.CodigoBarra.DigitoVerificador, Is.EqualTo(digitoVerificador), $"Dígito Verificador diferente de {digitoVerificador}");
        }

        [TestCase(300, "1", "BO123456D", "1", "0000001-9", "75691697400000300009427701017227800000019001", "75699.42779 01017.227800 00000.190017 1 69740000030000", 2016, 11, 10)]
        [TestCase(300, "2", "BO123456D", "1", "0000002-6", "75691711100000300009427701017227800000026001", "75699.42779 01017.227800 00000.260018 1 71110000030000", 2017, 03, 27)]
        [TestCase(400, "3", "BO123456D", "1", "0000003-3", "75691714200000400009427701017227800000033001", "75699.42779 01017.227800 00000.330019 1 71420000040000", 2017, 04, 27)]
        [TestCase(500, "4", "BO123456F", "3", "0000004-0", "75693703000000500009427701017227800000040001", "75699.42779 01017.227800 00000.400010 3 70300000050000", 2017, 01, 05)]
        [TestCase(900, "5", "BO123456F", "8", "0000005-8", "75698729500000900009427701017227800000058001", "75699.42779 01017.227800 00000.580019 8 72950000090000", 2017, 09, 27)]
        [TestCase(600, "6", "BO123456G", "3", "0000006-5", "75693706300000600009427701017227800000065001", "75699.42779 01017.227800 00000.650010 3 70630000060000", 2017, 2, 07)]
        [TestCase(4011.24, "12349", "BO123456F", "2", "0012349-2", "75692702200004011249427701017227800123492001", "75699.42779 01017.227800 01234.920013 2 70220000401124", 2016, 12, 28)]
        [TestCase(700, "8", "BO123456B", "3", "0000008-0", "75693709500000700009427701017227800000080001", "75699.42779 01017.227800 00000.800011 3 70950000070000", 2017, 3, 11)]
        [TestCase(409, "9", "BO123456E", "7", "0000009-7", "75697700200000409009427701017227800000097001", "75699.42779 01017.227800 00000.970012 7 70020000040900", 2016, 12, 08)]
        public void Deve_criar_boleto_sicoob_9_01_com_nosso_numero_formatado_valido(decimal valorTitulo, string nossoNumero, string numeroDocumento, string digitoVerificador, string nossoNumeroFormatado, string codigoDeBarras, string linhaDigitavel, params int[] anoMesDia)
        {
            //Ambiente
            var boleto = new Boleto(_banco)
            {
                DataVencimento = new DateTime(anoMesDia[0], anoMesDia[1], anoMesDia[2]),
                ValorTitulo = valorTitulo,
                NossoNumero = nossoNumero,
                NumeroDocumento = numeroDocumento,
                EspecieDocumento = TipoEspecieDocumento.DM,
                Pagador = TestUtils.GerarPagador()
            };

            //Ação
            boleto.ValidarDados();

            //Assertivas 
            Assert.That(boleto.NossoNumeroFormatado, Is.EqualTo(nossoNumeroFormatado), "Nosso número inválido");
        }

        [TestCase(300, "1", "BO123456D", "1", "0000001-9", "75691697400000300009427701017227800000019001", "75699.42779 01017.227800 00000.190017 1 69740000030000", 2016, 11, 10)]
        [TestCase(300, "2", "BO123456D", "1", "0000002-6", "75691711100000300009427701017227800000026001", "75699.42779 01017.227800 00000.260018 1 71110000030000", 2017, 03, 27)]
        [TestCase(400, "3", "BO123456D", "1", "0000003-3", "75691714200000400009427701017227800000033001", "75699.42779 01017.227800 00000.330019 1 71420000040000", 2017, 04, 27)]
        [TestCase(500, "4", "BO123456F", "3", "0000004-0", "75693703000000500009427701017227800000040001", "75699.42779 01017.227800 00000.400010 3 70300000050000", 2017, 01, 05)]
        [TestCase(900, "5", "BO123456F", "8", "0000005-8", "75698729500000900009427701017227800000058001", "75699.42779 01017.227800 00000.580019 8 72950000090000", 2017, 09, 27)]
        [TestCase(600, "6", "BO123456G", "3", "0000006-5", "75693706300000600009427701017227800000065001", "75699.42779 01017.227800 00000.650010 3 70630000060000", 2017, 2, 07)]
        [TestCase(4011.24, "12349", "BO123456F", "2", "0012349-2", "75692702200004011249427701017227800123492001", "75699.42779 01017.227800 01234.920013 2 70220000401124", 2016, 12, 28)]
        [TestCase(700, "8", "BO123456B", "3", "0000008-0", "75693709500000700009427701017227800000080001", "75699.42779 01017.227800 00000.800011 3 70950000070000", 2017, 3, 11)]
        [TestCase(409, "9", "BO123456E", "7", "0000009-7", "75697700200000409009427701017227800000097001", "75699.42779 01017.227800 00000.970012 7 70020000040900", 2016, 12, 08)]
        public void Deve_criar_boleto_sicoob_9_01_com_codigo_de_barras_valido(decimal valorTitulo, string nossoNumero, string numeroDocumento, string digitoVerificador, string nossoNumeroFormatado, string codigoDeBarras, string linhaDigitavel, params int[] anoMesDia)
        {
            //Ambiente
            var boleto = new Boleto(_banco)
            {
                DataVencimento = new DateTime(anoMesDia[0], anoMesDia[1], anoMesDia[2]),
                ValorTitulo = valorTitulo,
                NossoNumero = nossoNumero,
                NumeroDocumento = numeroDocumento,
                EspecieDocumento = TipoEspecieDocumento.DM,
                Pagador = TestUtils.GerarPagador()
            };

            //Ação
            boleto.ValidarDados();

            //Assertivas 
            Assert.That(boleto.CodigoBarra.CodigoDeBarras, Is.EqualTo(codigoDeBarras), "Código de Barra inválido");
        }

        [TestCase(300, "1", "BO123456D", "1", "0000001-9", "75691697400000300009427701017227800000019001", "75699.42779 01017.227800 00000.190017 1 69740000030000", 2016, 11, 10)]
        [TestCase(300, "2", "BO123456D", "1", "0000002-6", "75691711100000300009427701017227800000026001", "75699.42779 01017.227800 00000.260018 1 71110000030000", 2017, 03, 27)]
        [TestCase(400, "3", "BO123456D", "1", "0000003-3", "75691714200000400009427701017227800000033001", "75699.42779 01017.227800 00000.330019 1 71420000040000", 2017, 04, 27)]
        [TestCase(500, "4", "BO123456F", "3", "0000004-0", "75693703000000500009427701017227800000040001", "75699.42779 01017.227800 00000.400010 3 70300000050000", 2017, 01, 05)]
        [TestCase(900, "5", "BO123456F", "8", "0000005-8", "75698729500000900009427701017227800000058001", "75699.42779 01017.227800 00000.580019 8 72950000090000", 2017, 09, 27)]
        [TestCase(600, "6", "BO123456G", "3", "0000006-5", "75693706300000600009427701017227800000065001", "75699.42779 01017.227800 00000.650010 3 70630000060000", 2017, 2, 07)]
        [TestCase(4011.24, "12349", "BO123456F", "2", "0012349-2", "75692702200004011249427701017227800123492001", "75699.42779 01017.227800 01234.920013 2 70220000401124", 2016, 12, 28)]
        [TestCase(700, "8", "BO123456B", "3", "0000008-0", "75693709500000700009427701017227800000080001", "75699.42779 01017.227800 00000.800011 3 70950000070000", 2017, 3, 11)]
        [TestCase(409, "9", "BO123456E", "7", "0000009-7", "75697700200000409009427701017227800000097001", "75699.42779 01017.227800 00000.970012 7 70020000040900", 2016, 12, 08)]
        public void Deve_criar_boleto_sicoob_9_01_com_linha_digitavel_valida(decimal valorTitulo, string nossoNumero, string numeroDocumento, string digitoVerificador, string nossoNumeroFormatado, string codigoDeBarras, string linhaDigitavel, params int[] anoMesDia)
        {
            //Ambiente
            var boleto = new Boleto(_banco)
            {
                DataVencimento = new DateTime(anoMesDia[0], anoMesDia[1], anoMesDia[2]),
                ValorTitulo = valorTitulo,
                NossoNumero = nossoNumero,
                NumeroDocumento = numeroDocumento,
                EspecieDocumento = TipoEspecieDocumento.DM,
                Pagador = TestUtils.GerarPagador()
            };

            //Ação
            boleto.ValidarDados();

            //Assertivas 
            Assert.That(boleto.CodigoBarra.LinhaDigitavel, Is.EqualTo(linhaDigitavel), "Linha digitável inválida");
        }
    }
}