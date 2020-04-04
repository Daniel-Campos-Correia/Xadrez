using System;
using Xadrez.tabuleiro;
using Xadrez.JogodeXadrez;
using Xadrez.tabuleiro.Enum;



namespace Xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            PosicaoXadrez pos = new PosicaoXadrez('a', 1);
            Console.WriteLine(pos);
            Console.WriteLine(pos.ToPosicao());

            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);
                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
                tab.colocarPeca(new Torre(tab, Cor.Preta), new Posicao(3, 0));
                
                tab.colocarPeca(new Torre(tab, Cor.Branco), new Posicao(1, 0));
                tab.colocarPeca(new Torre(tab, Cor.Branco), new Posicao(2, 3));
                tab.colocarPeca(new Torre(tab, Cor.Branco), new Posicao(4, 0));

                Tela.ImprimirTabuleiro(tab);
            }
            catch(TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
