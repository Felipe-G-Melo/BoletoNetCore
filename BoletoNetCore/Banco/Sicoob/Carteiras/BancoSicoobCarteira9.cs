using System;

namespace BoletoNetCore
{
    [CarteiraCodigo("9/01")]
    internal class BancoSicoobCarteira9: ICarteira<BancoSicoob>
    {
        internal static Lazy<ICarteira<BancoSicoob>> Instance { get; } = new Lazy<ICarteira<BancoSicoob>>(() => new BancoSicoobCarteira9());

        private BancoSicoobCarteira9()
        {

        }

        public string FormataCodigoBarraCampoLivre(Boleto boleto)
        {
            return "                         ";
        }

        public void FormataNossoNumero(Boleto boleto)
        {
        }
    }
}
