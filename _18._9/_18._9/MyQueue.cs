using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18._9
{
    class node
    {
        public node next;
        public object data;
        public node(node next, object data)
        {
            this.next = next;
            this.data = data;
        }
    }

    class MyQueue
    {
        node top;
        node tail;
        /// <summary>
        /// положить в очередь
        /// </summary>
        public void Enqueue(node d)
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
        public object Dequeue()
        {
            if (isEmpty()) throw new InvalidOperationException();
            object result = top.data;
            top = top.next;
            return result;
        }
    }
}
