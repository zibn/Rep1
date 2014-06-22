using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace _18._9
{
    class MyTree
    {
        public Node top;
        public Node selectNode;
        public Bitmap bitmap;
        const int step = 50;
        const int dh = 1;
        Graphics g;
        Pen myPen;
        SolidBrush myBrush = (SolidBrush)Brushes.White;
        Font myFont;

        public class Node
        {
            public object data;
            public Node left;
            public Node right;
            public int x;
            public int y;
            public bool visit;
            public int count;

            public Node(Node left, Node right, object data, int x, int y)
            {
                this.left = left;
                this.right = right;
                this.data = data;
                this.x = x;
                this.y = y;
                this.visit = false;
                this.count = 1;
            }
        }

        /// <summary>
        /// Снимаем признак посещения узла
        /// </summary>
        public void DeSelect(Node p)
        {
            if (p != null)
            {
                p.visit = false;
                DeSelect(p.left);
                DeSelect(p.right);
            }
        }
        /// <summary>
        /// Смещаем поддерево
        /// </summary>
        public void Delta(Node p, int dx, int dy)
        {
            p.x -= dx; p.y -= dy;
            if (p.left != null)
                Delta(p.left, dx, dy);
            if (p.right != null)
                Delta(p.right, dx, dy);
        }
        /// <summary>
        /// Поиск по координатам 
        /// </summary>
        /// <param name="p">узел</param>
        /// <param name="x">координата</param>
        /// <param name="y">координата</param>
        /// <returns></returns>
        public Node FindNode(Node p, int x, int y)
        {
            Node result = null;
            if (p == null)
                return result;

            if (((p.x - x) * (p.x - x) + (p.y - y) * (p.y - y)) < 100)
                result = p;
            else
            {
                result = FindNode(p.left, x, y);
                if (result == null)
                    result = FindNode(p.right, x, y);
            }
            return result;
        }
        /// <summary>
        /// поиск места и вставка нового узла
        /// </summary>
        public void Insert(ref Node t, int data, int x, int y)
        {
            if (t == null)
                t = new Node(null, null, data, x, y);
            else
                if (data < Convert.ToInt32(t.data))
                    Insert(ref t.left, data, t.x - step, t.y + dh * step);
                else
                    if (data > Convert.ToInt32(t.data))
                        Insert(ref t.right, data, t.x + step, t.y + dh * step);
                    else
                        t.count++;
        }

        public MyTree(int VW, int VH)
        {
            top = null;
            bitmap = new Bitmap(VW, VH);
            myFont = new Font("Courier New", 10, FontStyle.Bold);
        }
        /// <summary>
        /// Рисование дерева(узла)
        /// </summary>
        void DrawNode(Node p)
        {
            int R = 17;
            if (p.left != null)
                g.DrawLine(myPen, p.x, p.y, p.left.x, p.left.y);
            if (p.right != null)
                g.DrawLine(myPen, p.x, p.y, p.right.x, p.right.y);

            if (p.visit)
                myBrush = (SolidBrush)Brushes.AliceBlue;
            else
                myBrush = (SolidBrush)Brushes.Violet;

            g.FillEllipse(myBrush, p.x - R, p.y - R, 2 * R, 2 * R);
            g.DrawEllipse(myPen, p.x - R, p.y - R, 2 * R, 2 * R);
            string s = Convert.ToString(p.data) + ":" + Convert.ToString(p.count);
            SizeF size = g.MeasureString(s, myFont);
            g.DrawString(s, myFont, Brushes.Black,
                p.x - size.Width / 2,
                p.y - size.Height / 2);

            if (p.left != null)
                DrawNode(p.left);
            if (p.right != null)
                DrawNode(p.right);
        }
        /// <summary>
        /// рисование дерева
        /// </summary>
        public void Draw()
        {
            using (g = Graphics.FromImage(bitmap))
            {
                Color cl = Color.FromArgb(255, 255, 255);
                g.Clear(cl);
                myPen = Pens.Black;
                if (top != null)
                    DrawNode(top);
            }
        }

        /// <summary>
        /// метод для вызова метода, который будет осуществлять 
        /// подсчет количества узлов на данном уровне
        /// </summary>
        public int CountLevel(int k)
        {
            Node node = top;
            int i = 1;
            return countLevel(node, i, k);
        }
        /// <summary>
        /// метод осуществляющий подсчет количества узлов на данном уровне
        /// </summary>
        private int countLevel(Node node, int i, int k)
        {
            if (i == k)
                return 1;
            else
            {
                int res = 0;
                if (node.left != null)
                    res += countLevel(node.left, i + 1, k);
                if (node.right != null)
                    res += countLevel(node.right, i + 1, k);
                return res;
            }
        }

        /// <summary>
        /// используя поиск в ширину
        /// </summary>
        public int CountOnLevel(int k)
        {
            MyQueue<Node> q = new MyQueue<Node>();
            q.Enqueue(top);
            int n = 0;
            int count = 0;
            while ((!q.isEmpty()) && (n < k))
            {
                int N = q.count;
                n++;
                for (int i = 0; i < N; i++)
                {
                    Node p = q.Dequeue();
                    if (n == k) count++;
                    if (p.left != null)
                        q.Enqueue(p.left);
                    if (p.right != null)
                        q.Enqueue(p.right);
                }               
            }
            return count;
        }
    }
}
