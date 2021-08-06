using System;
using System.Collections.Generic;
using System.Text;

namespace labirinto
{
    class Pilha<Type>
    {
        // Um TAD Pilha teria os atributos com visibilidade interna (encapsulados). 
        private Type[] elementos;
        private int topo;
        // para inicializar os atributos teremos o contrutor
        public Pilha(int tam)
        {
            this.elementos = new Type[tam];
            this.topo = -1; // pilha esta vazia
        }
        public void push(Type elemento)
        {
            // se a pilha esta cheia gera uma excecao
            if (this.isFull())
                throw new Exception("Pilha cheia.");

            this.topo++;
            elementos[topo] = elemento;
        }
        public Type pop()
        {
            // se a pilha estiver vazia gera uma excecao
            if (this.isEmpty())
                throw new Exception("Pilha vazia.");

            Type ch = elementos[topo];
            topo--;
            return ch;
        }
        public bool isEmpty()
        {
            return topo == -1;
        }
        public bool isFull()
        {
            return topo >= elementos.Length - 1;
        }
        public Type GetUltimoColocado()
        {
            //pego e mando o ultimo colocado da lista
            return elementos[topo];
        }

        public int Size()
        {
            return topo;
        }
    }
}
