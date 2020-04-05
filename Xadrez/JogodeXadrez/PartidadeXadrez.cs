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
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturas;
        public bool Xeque { get; private set; }

        public PartidadeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Xeque = false;
            Terminada = false;
            Pecas = new HashSet<Peca>();
            Capturas = new HashSet<Peca>();
            ColocarPecas();
        }
        private Cor Adversária(Cor cor)
        {
            if (cor == Cor.Branco)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branco;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in PecasemJogo(cor))
            {
                if(x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaemCheque(Cor cor)
        {
            Peca R = rei(cor);
            if(R==null)
            {
                throw new TabuleiroException("Não existe Rei no Tabuleiro");
            }
            foreach (Peca x in PecasemJogo(Adversária(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha,R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool EstaemXequemate(Cor cor)
        {
            if(!EstaemCheque(cor))
            {
                return false;
            }

            foreach(Peca x in PecasemJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i=0; i<Tab.Linhas;i++)
                {
                    for (int j=0;j<Tab.Colunas;j++)
                    {
                        if(mat[i,j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool estaemXeque = EstaemCheque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if(!estaemXeque)
                            {
                                return false;
                            }
                        }
                    }
                }

            }
            return true;
        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada=ExecutaMovimento(origem, destino);
            if (EstaemCheque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em cheque");
            }
            if (EstaemCheque(Adversária(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (EstaemXequemate(Adversária(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }
        }
        private void MudaJogador()
        {
            if (JogadorAtual==Cor.Branco)
            {
                JogadorAtual = Cor.Preta;
            }
            else 
            {
                JogadorAtual = Cor.Branco;
            }
        }

        public void ValidarPosicaodeOrigem(Posicao posicao)
        {
            if (Tab.Peca(posicao) == null)
            {
                throw new TabuleiroException("Não existe peça na posição escolhida");
            }
            if (JogadorAtual != Tab.Peca(posicao).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua! ");
            }
            if (!Tab.Peca(posicao).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("A peça não possui movimentos disponíveis ");
            }
        }

        public void validarPosicaodeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de Destino Invalida");
            }
        }
        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQteMovimentos();
            Peca pecaCapturada=Tab.RetirarPeca(destino);
            Tab.colocarPeca(p, destino);
            if(pecaCapturada!=null)
            {
                Capturas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarQteMovimentos();
            if(pecaCapturada!=null)
            {
                Tab.colocarPeca(pecaCapturada, destino);
                Capturas.Remove(pecaCapturada);
            }
            Tab.colocarPeca(p, origem);
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Capturas)
            {
                if (x.Cor==cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> PecasemJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }
        private void ColocarPecas() 
        {
            ColocarNovaPeca('a', 1, new Torre(Tab, Cor.Branco));
            ColocarNovaPeca('b', 1, new Cavalo(Tab, Cor.Branco));
            ColocarNovaPeca('c', 1, new Bispo(Tab, Cor.Branco));
            ColocarNovaPeca('d', 1, new Rainha(Tab, Cor.Branco));
            ColocarNovaPeca('e', 1, new Rei(Tab, Cor.Branco));
            ColocarNovaPeca('f', 1, new Bispo(Tab, Cor.Branco));
            ColocarNovaPeca('g', 1, new Cavalo(Tab, Cor.Branco));
            ColocarNovaPeca('h', 1, new Torre(Tab, Cor.Branco));
            ColocarNovaPeca('a', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('b', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('c', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('d', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('e', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('f', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('g', 2, new Peao(Tab, Cor.Branco));
            ColocarNovaPeca('h', 2, new Peao(Tab, Cor.Branco));

            ColocarNovaPeca('a', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(Tab, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(Tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rainha(Tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(Tab, Cor.Preta));
            ColocarNovaPeca('f', 8, new Bispo(Tab, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(Tab, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(Tab, Cor.Preta));
            ColocarNovaPeca('b', 7, new Peao(Tab, Cor.Preta));
            ColocarNovaPeca('c', 7, new Peao(Tab, Cor.Preta));
            ColocarNovaPeca('d', 7, new Peao(Tab, Cor.Preta));
            ColocarNovaPeca('e', 7, new Peao(Tab, Cor.Preta));
            ColocarNovaPeca('f', 7, new Peao(Tab, Cor.Preta));
            ColocarNovaPeca('g', 7, new Peao(Tab, Cor.Preta));
            ColocarNovaPeca('h', 7, new Peao(Tab, Cor.Preta));



        }
    }
}
