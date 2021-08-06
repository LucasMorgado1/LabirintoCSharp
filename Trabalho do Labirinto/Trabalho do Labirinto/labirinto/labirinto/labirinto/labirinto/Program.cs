/*
Entrega de trabalho
Nós,

Larissa Teixeira Araujo
Lucas Fernando Soares Morgado de Souza

declaramos que todas as respostas são fruto de nosso próprio trabalho,
não copiamos respostas de colegas externos à equipe,
não disponibilizamos nossas respostas para colegas externos à equipe e
não realizamos quaisquer outras atividades desonestas para nos beneficiar
ou prejudicar outros.
*/

using System;

namespace labirinto
{
    class Program
    {
        static void Main(string[] args)
        {

            string[] lines = System.IO.File.ReadAllLines("tabuleiro.txt");
            int[] pontoinicial = new int[] { 0, 0 };
            int[] pontofinal = new int[] { 0, 0 };
            int[,] matriz = new int[lines[0].Split(' ').Length, lines[0].Split(' ').Length];

            Console.WriteLine("Guia:\n  0 = Em branco \n -1 = Parede \n -2 = Ponto de Inicio (A) \n -3 = Ponto Final (B) \n");
            Console.WriteLine("AVISO: o programa ira funcionar apenas em caso do arredor do mapa ser todo por -1\n");

            for (int y = 0; y < lines.Length; y++)
            {
                string[] letras = lines[y].Split(" ");

                for (int x = 0; x < lines.Length; x++)
                {
                    matriz[y, x] = int.Parse(letras[x]);
                    //aqui eh onde marcamos de forma automatica onde sera o ponto inicial do tabuleiro
                    if (matriz[y, x] == -2)
                    {
                        pontoinicial = new int[] { y, x };
                    }
                    //e aqui onde sera o ponto final do tabuleiro
                    if (matriz[y, x] == -3)
                    {
                        pontofinal = new int[] { y, x };
                    }
                }
            }
            //aqui eh onde imprimimos o tabuleiro (padrao e finalizado)
            Console.WriteLine("Labirinto padrao: \n");
            ImprimeLabirinto(matriz);

            int [] []  caminho =AchaCaminho(matriz, pontoinicial, pontofinal);
            Console.WriteLine();

            Console.WriteLine("Melhor caminho do ponto A ate o ponto B: \n");
            ImprimeResultado(matriz, caminho);
        }

        public static void ImprimeLabirinto(int[,] matriz)
        {
            for (int y = 0; y < matriz.GetLength(0); y++)
            {
                for (int x = 0; x < matriz.GetLength(1); x++)
                {
                    //aqui eh a logica simples de impressao de matriz
                    if (matriz[y, x] >= 0 && matriz[y, x] < 10)
                        Console.Write(" ");
                    Console.Write(" " + matriz[y, x]);
                }
                Console.WriteLine(" ");
            }
        }
        public static void ImprimeResultado(int[,] matriz, int [] [] caminho)
        {
            //aqui imprimimos o tabuleiro novamente
            for (int y = 0; y < matriz.GetLength(0); y++)
            {
                for (int x = 0; x < matriz.GetLength(1); x++)
                {
                    for (int i = 0; i < caminho.Length; i++)
                    {
                        //porem, se o X e o Y do caminho (onde tem a logica do melhor caminho) for igual ao X e Y dos for`s
                        //colocamos em verde os numeros, para melhor entendimento e impressao do tabuleiro e do caminho
                        if (caminho[i][0] == x && caminho [i][1] == y)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                    }
                    if (matriz[y, x] >= 0 && matriz[y, x] < 10)
                        Console.Write(" ");
                    Console.Write(" " + matriz[y, x]);
                    //no fim resetamos a cor, para que o tabuleiro inteiro nao saia todo verde
                    Console.ResetColor();
                }

                Console.WriteLine(" ");
            }
        }
        //Essa eh funcao principal, onde fazemos todo o calculo e onde somamos os numeros conforme vai andando no tabuleiro
        public static int [] [] AchaCaminho(int[,] matriz, int[] pontoinicial, int[] pontofinal)
        {
            Fila<int[]> fila = new Fila<int[]>(matriz.Length);
            Pilha<int[]> pilha = new Pilha<int[]>(matriz.Length);

            int x = pontoinicial[0];
            int y = pontoinicial[1];
            //indo pra cima do ponto inicial
            if (matriz[y, x - 1] == 0)
            {
                matriz[y, x - 1] = 1;
                fila.enqueue(new int[] { x - 1, y });
            }
            //indo pra baixo do ponto inicial
            if (matriz[y, x + 1] == 0)
            {
                matriz[y, x + 1] = 1;
                fila.enqueue(new int[] { x + 1, y });
            }
            //indo pra esquerda do ponto inicial
            if (matriz[y - 1, x] == 0)
            {
                matriz[y - 1, x] = 1;
                fila.enqueue(new int[] { x, y - 1 });
            }
            //indo pra direita do ponto inicial
            if (matriz[y + 1, x] == 0)
            {
                matriz[y + 1, x] = 1;
                fila.enqueue(new int[] { x, y + 1 });
            }

            //criamos um vetor de direcoes, para automatizar o processo atraves do resto do tabuleiro
            int[] emX = { 1, -1, 0, 0 };
            int[] emY = { 0, 0, 1, -1 };

            while (!fila.isEmpty())
            {
                //posicao atual eh referente ao ultimo numero que foi colocado no tabuleiro, por exemplo um 4, no proximo sera posAtual + 1, resultando 5
                int[] posAtual = fila.dequeue();

                //aqui comparamos se na novaPosicao o espaco esta livre, se estiver, colocamos a posAtual, somando um nele
                for (int i = 0; i < emX.Length; i++) {
                    int[] novaPos = { posAtual[0] + emX[i], posAtual[1] + emY[i] };
                    if (matriz[novaPos[1], novaPos[0]] == 0)
                    {
                        matriz[novaPos[1], novaPos[0]] = matriz[posAtual[1], posAtual[0]] + 1;
                        //depois de colocar nas posicoes, damos enqueue para colocar dentro da Fila
                        fila.enqueue(new int[] { novaPos[0], novaPos[1] });
                    }
                }
            }
            //aqui criamos um vetor chamado menornumero, onde sera usado para comparacao
            int[] menornumero = new int[] { 0, 0, 999 };


            for (int i = 0; i < emX.Length; i++)
            {
                //para fazer o caminho inverso, indo do ponto final pro ponto inicial, vamos comparando com os numeros ao redor
                //caso seja menor, pegamos ele e assim continua, comparando e atribuindo
                if (matriz[pontofinal[1] + emY[i], pontofinal[0] + emX[i]] < menornumero[2] && matriz[pontofinal[1] + emY[i], pontofinal[0] + emX[i]] > -1)
                {
                    menornumero[0] = pontofinal[0] + emX[i];
                    menornumero[1] = pontofinal[1] + emY[i];
                    menornumero[2] = matriz[pontofinal[1] + emY[i], pontofinal[0] + emX[i]];
                }

            }
            //destino eh referente a posicao dos numeros nas suas coordenadas
            int[] destino = new int[] { menornumero[0], menornumero[1]};
            pilha.push(destino);

            while (matriz[destino[1], destino[0]] != 1 )
            {
                for (int i = 0; i < emX.Length; i++)
                {
                    //para finalizar, aqui eh onde pegamos o caminho mais rapido do ponto B ate o ponto A
                     if (matriz[destino[1] + emY[i], destino[0] + emX[i]] <  matriz[destino[1], destino[0]] && matriz[destino[1] + emY[i], destino[0] + emX[i]] > -1 ) {
                        //entao atribuimos ele na variavel destino
                        destino = new int[] { destino[0] + emX[i], destino[1] + emY[i] };
                        //e colocamos dentro da pilha
                        pilha.push(destino);
                        
                    }
                }
            }
            int index = 0;
            int[][] resultado = new int[pilha.Size() + 1][]; 
            while (!pilha.isEmpty())
            {
                //por ultimo mas nao menos importante, damos pop no resultado, dando entao o melhor caminho de fato, onde usaremos na impressao do resultado
                resultado[index] = pilha.pop();
                index++;
            }
            //retornamos o resultado
            return resultado;
        }  
    }
}
