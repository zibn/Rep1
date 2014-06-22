using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18._9
{
    class MyQueue<Type>
    {
        node top;
        node tail;

        public int count = 0;

        class node
        {
            public node next;
            public Type data;
            public node(node next, Type data)
            {
                this.next = next;
                this.data = data;
            }
        }

        /// <summary>
        /// положить в очередь
        /// </summary>
        public void Enqueue(Type d)
        {
            node q = new node(null, d);
            if (isEmpty())
            {
                top = tail = q;
            }
            else
            {
                tail.next = q;
                tail = q;
            }
            count++;
        }
        /// <summary>
        /// проверка на пустоту
        /// </summary>
        public bool isEmpty()
        {
            return top == null;
        }
        /// <summary>
        /// удалить из очереди
        /// </summary>
        public Type Dequeue()
        {
            if (isEmpty()) throw new InvalidOperationException();
            Type result = top.data;
            top = top.next;
            count--;
            return result;
        }
    }
}