using Proyecto1Consola.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1Consola
{
    public partial class ErroresDeTokens : Form
    {

        public static List<string> ListaErrores = new List<string>();

        public ErroresDeTokens()
        {
            InitializeComponent();
        }

        private void FormMostrarErrores_Load(object sender, EventArgs e)
        {
            this.Text = "Error al compilar ";
            if (ListaErrores.Count != 0)
            {
                for (int i = 0; i < ListaErrores.Count; i++)
                {
                    grilla_error.Rows.Add();
                    grilla_error[0, i].Value = (i + 1);
                    grilla_error[1, i].Value = ListaErrores[i].ToString();
                }
            }

        }

        private void grilla_error_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void ArchivoError()
        {

        }
        public static List<string> Lista = new List<string>();

        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog save_ = new SaveFileDialog();
            save_.Filter = "(.gtE)|*.gtE";
            save_.Title = "Guardar";
            if (save_.ShowDialog() == DialogResult.OK)
            {

               TextWriter sw = new StreamWriter("Errores.gtE");
                int rowcount = grilla_error.Rows.Count;
                for (int i = 0; i < rowcount - 1; i++)
                {
                    sw.WriteLine(grilla_error.Rows[i].Cells[0].Value.ToString() + "\t"
                                 + grilla_error.Rows[i].Cells[1].Value.ToString() + "\t"
    );
                }
                sw.Close();
                MessageBox.Show("Datos Exportados correctamente");

            }
            save_.Dispose();
           // Archivos.Guardar(CodigoEscrito, true);
        }
    }
}
