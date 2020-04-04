using System;
using System.Collections.Generic;
using System.Text;
using Xadrez.tabuleiro;
using Xadrez.tabuleiro.Enum;

namespace Xadrez.JogodeXadrez
{
    class PartidadeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public bool Terminada { get; private set; }
        private int Turno;
        private Cor JogadorAtual;
        

        public PartidadeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Terminada = false;
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQteMovimentos();
            Peca pecaCapturada=Tab.RetirarPeca(destino);
            Tab.colocarPeca(p, destino);
        }

        private void ColocarPecas() 
        {
            Tab.colocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('c', 1).ToPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('c', 2).ToPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('d', 2).ToPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('e', 2).ToPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Branco), new PosicaoXadrez('e', 1).ToPosicao());
            Tab.colocarPeca(new Rei(Tab, Cor.Branco), new PosicaoXadrez('d', 1).ToPosicao());

            Tab.colocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('c', 7).ToPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('c', 8).ToPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('d', 7).ToPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('e', 7).ToPosicao());
            Tab.colocarPeca(new Torre(Tab, Cor.Preta), new PosicaoXadrez('e', 8).ToPosicao());
            Tab.colocarPeca(new Rei(Tab, Cor.Preta), new PosicaoXadrez('d', 8).ToPosicao());

        }
    }
}
