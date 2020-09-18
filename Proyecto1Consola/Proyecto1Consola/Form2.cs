using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto1Consola
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        #region variables

        private bool Poblamiento = true;
        private LectorSintaxis LectorSintactico;
        private Color kCommentarioColor = Color.LightGreen;
        private List<string> Tokens = new List<string>();

        private List<string> CodigoEscrito = new List<string>();

        #region Estructurasintaxis
        struct WordAndPosition
        {
            public string Word;
            public int Position;
            public int Length;
            public override string ToString()
            {
                string s = "Word = " + Word + ", Position = " + Position + ", Length = " + Length + "\n";
                return s;
            }
        };

        WordAndPosition[] TheBuffer = new WordAndPosition[4000];

        private bool TestComment(string s)
        {
            string testString = s.Trim();
            if ((testString.Length >= 2) &&
                 (testString[0] == '/') &&
                 (testString[1] == '/')
                )
                return true;

            return false;
        }
        private int ParseLine(string s)
        {
            TheBuffer.Initialize();
            int count = 0;
            Regex r = new Regex(@"\w+|[^A-Za-z0-9_ \f\t\v]", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match m;

            for (m = r.Match(s); m.Success; m = m.NextMatch())
            {
                TheBuffer[count].Word = m.Value;
                TheBuffer[count].Position = m.Index;
                TheBuffer[count].Length = m.Length;
                count++;
            }


            return count;
        }
        private Color MostrarColor(string s)
        {
            Color Color = Color.Black;

            if (LectorSintactico.IsFuncion(s))
            {
                Color = Color.Red;
            }

            if (LectorSintactico.IsLlave(s))
            {
                Color = Color.Blue;
            }

            if (LectorSintactico.IsSeparador(s))
            {
                Color = Color.DarkOrange;
            }


            return Color;
        }
        private void CrearSintaxisPorCadaLinea()
        {
            int Start = richTextBox1.SelectionStart;
            int Length = richTextBox1.SelectionLength;


            int pos = Start;
            while ((pos > 0) && (richTextBox1.Text[pos - 1] != '\n'))
                pos--;

            int pos2 = Start;
            while ((pos2 < richTextBox1.Text.Length) &&
                    (richTextBox1.Text[pos2] != '\n'))
                pos2++;

            string s = richTextBox1.Text.Substring(pos, pos2 - pos);
            if (TestComment(s) == true)
            {
                richTextBox1.Select(pos, pos2 - pos);
                richTextBox1.SelectionColor = kCommentarioColor;
            }
            else
            {
                string previousWord = "";
                int count = ParseLine(s);
                for (int i = 0; i < count; i++)
                {
                    WordAndPosition wp = TheBuffer[i];

                    if (wp.Word == "/" && previousWord == "/")
                    {

                        int posCommentStart = wp.Position - 1;
                        int posCommentEnd = pos2;
                        while (wp.Word != "\n" && i < count)
                        {
                            wp = TheBuffer[i];
                            i++;
                        }

                        i--;
                        posCommentEnd = pos2;
                        richTextBox1.Select(posCommentStart + pos, posCommentEnd - (posCommentStart + pos));
                        richTextBox1.SelectionColor = this.kCommentarioColor;

                    }
                    else
                    {

                        Color c = MostrarColor(wp.Word);
                        richTextBox1.Select(wp.Position + pos, wp.Length);
                        richTextBox1.SelectionColor = c;
                    }

                    previousWord = wp.Word;

                }
            }

            if (Start >= 0)
                richTextBox1.Select(Start, Length);


        }

        private void CrearSintaxisColorAllText(string s)
        {
            Poblamiento = true;

            int CurrentSelectionStart = richTextBox1.SelectionStart;
            int CurrentSelectionLength = richTextBox1.SelectionLength;

            int count = ParseLine(s);
            string previousWord = "";
            for (int i = 0; i < count; i++)
            {
                WordAndPosition wp = TheBuffer[i];


                if (wp.Word == "/" && previousWord == "/")
                {

                    int posCommentStart = wp.Position - 1;
                    int posCommentEnd = i;
                    while (wp.Word != "\n" && i < count)
                    {
                        wp = TheBuffer[i];
                        i++;
                    }

                    i--;

                    posCommentEnd = wp.Position;
                    richTextBox1.Select(posCommentStart, posCommentEnd - posCommentStart);
                    richTextBox1.SelectionColor = this.kCommentarioColor;

                }
                else
                {

                    Color c = MostrarColor(wp.Word);
                    richTextBox1.Select(wp.Position, wp.Length);
                    richTextBox1.SelectionColor = c;
                }

                previousWord = wp.Word;
            }

            if (CurrentSelectionStart >= 0)
                richTextBox1.Select(CurrentSelectionStart, CurrentSelectionLength);

            Poblamiento = false;

        }
        #endregion

        #region Inicializadores

        private delegate void ANALIZADOR_SEMANTICO_SINTACTICO__();

        private void GetCodigoEscrito()
        {
            try
            {
                string[] data = richTextBox1.Lines;
                CodigoEscrito = new List<string>();
                CodigoEscrito.AddRange(data);
            }
            catch { }
        }

        private void CodigoInicio()
        {
            string[] codigo = new string[]
            {
                "espacio ChumpeDesktop",
                "{",
                "     usar sistema;",
                "     usar sistema.coleccion;",
                "     usar sistema.componentes;",

                "     publico clase chumpe",
                "     {",
                "       chumpe_inicio()",
                "       {",
                "       }",
                "     }",
                "}"
            };
            richTextBox1.Lines = codigo;
        }

        #endregion

        #region eventos

        private void Form2_Load(object sender, EventArgs e)
        {
            LectorSintactico = new LectorSintaxis("chumpe.syntax");
            CodigoInicio();
            this.CrearSintaxisColorAllText(richTextBox1.Text);
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            this.Text = "CHUMPE++ V" + version;
            this.richTextBox1.AcceptsTab = true;
            tabControl1.Visible = false;
            richTextBox1.Select();
            richTextBox1.DetectUrls = true;
        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (Poblamiento)
                return;

            ColorSyntaxEditor.FlickerFreeRichEditTextBox._Paint = false;
            CrearSintaxisPorCadaLinea();
            ColorSyntaxEditor.FlickerFreeRichEditTextBox._Paint = true;
        }
        

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = true;
        }

        private void cerrarProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.Visible = false;
        }

        private void crearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            abrirarchivo();
        }

        public void abrirarchivo()
        {

            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.Title = "Lenguajes                                                                     Abrir Archivo                                                                       ";
                ofd.ShowDialog();
                // ofd.Filter = "Archivos ed#(*.ed)|*.ed";
                if (File.Exists(ofd.FileName))
                {
                    using (Stream stream = ofd.OpenFile())
                    {
                        //MessageBox.Show("archivo encontrado:  "+ofd.FileName);
                        leerarchivo(ofd.FileName);
              ///          nomarchivox = ofd.FileName;

               //         txt_direccion.Text = ofd.FileName;
                        tabControl1.Visible = true;
                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("El archivo no se abrio correctamente");

             //   tabla_errorres.addliste(2);
            }

        }

        public void leerarchivo(string nomarchivo)
        {
            StreamReader reader = new StreamReader(nomarchivo, System.Text.Encoding.Default);
            //string read = reader.ReadLine();
            string texto;
            // while (read != null)
            //{
            texto = reader.ReadToEnd();
            // read = read + "\n";

            reader.Close();

            richTextBox1.Text = texto;
            // read =reader.ReadLine();

            //}


        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
