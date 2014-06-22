using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _18._9
{
    /// <summary>
    /// №18.9 Подсчитать количество элементов на k-ом уровне двоичного дерева (корень считать узлом 1-го уровня).
    /// Выполнил Вакулин А.С., ФКН, 2 группа
    /// </summary>
    public partial class Form1 : Form
    {
        bool drawing = false;
        Graphics g;
        MyTree myTree;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int L = textBox1.Lines.Count();
            for (int i = 0; i < L; i++)
            {
                if (textBox1.Lines[i] != "")
                {
                    int k = Convert.ToInt32(textBox1.Lines[i]);
                    myTree.Insert(ref myTree.top, k, 200, 40);
                }
            }
            MyDraw();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = CreateGraphics();
            myTree = new MyTree(ClientRectangle.Width, ClientRectangle.Height);
        }
        public void MyDraw()
        {
            myTree.Draw();
            g.DrawImage(myTree.bitmap, ClientRectangle);
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            MyDraw();
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            myTree.DeSelect(myTree.top);
            myTree.selectNode = myTree.FindNode(myTree.top, e.X, e.Y);
            drawing = myTree.selectNode != null;
            if (drawing)
                myTree.selectNode.visit = true;
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (drawing)
                myTree.Delta(myTree.selectNode, myTree.selectNode.x - e.X, myTree.selectNode.y - e.Y);
            else
            {
                myTree.DeSelect(myTree.top);
                myTree.selectNode = myTree.FindNode(myTree.top, e.X, e.Y);
                if (myTree.selectNode != null)
                    myTree.selectNode.visit = true;
            }
            MyDraw();
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            drawing = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string N = textBox2.Text;
            int n = Convert.ToInt32(N);
            Random rnd = new Random();
            textBox1.Text = " ";
            for (int i = 0; i < n; i++)
            {
                int k = rnd.Next(100);
                textBox1.Text += Convert.ToString(k) + Convert.ToChar(13) + Convert.ToChar(10);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string N = textBox3.Text;
            int n = Convert.ToInt32(N);
            int m = myTree.CountLevel(n);
            label2.Text = Convert.ToString(m);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32(textBox3.Text);
            int g = myTree.CountOnLevel(n);
            label2.Text = Convert.ToString(g);
        }
    }
}
