using System;
using System.Collections.Generic;
using System.Text;

namespace labirinto
{
    class Fila <T> {
        // atributos do TAD Fila
        private T[] elementos;
        private int first, last;
        // construtor da Fila
        public Fila(int tam)
        {
            elementos = new T[tam];
            this.first = this.last = 0;
        }
        // insere um elemento na fila
        public void enqueue(T elemento)
        {
            if (isFull())
                throw new Exception("Fila cheia.");

            this.elementos[this.last++] = elemento;
        }
        // remove um elemento na fila
        public T dequeue()
        {
            if (isEmpty())
                throw new Exception("Fila vazia.");

            return this.elementos[this.first++];
        }
        public bool isEmpty()
        {
            return this.first == this.last;
        }
        public bool isFull()
        {
            return (last + 1) % elementos.Length == first;
        }
    }
}
