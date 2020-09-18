using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1Consola
{
    public partial class Form1 : Form
    {

        private delegate void Run__();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loading();
        }

        private void loading()
        {

            new System.Threading.Thread(delegate ()
            {
                Random rand = new Random();
                System.Threading.Thread.Sleep(rand.Next(2500, 5000));
                Run__ des = new Run__(Running);
                this.Invoke(des);

            }).Start();
        }

        private void Running()
        {
            Form2 desk = new Form2();
            desk.Show();
            this.Hide();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
