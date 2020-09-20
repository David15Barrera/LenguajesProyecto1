using Proyecto1Consola.Clases;
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
        private Color comentario = Color.Red;
        private List<string> Tokens = new List<string>();

        private List<string> CodigoEscrito = new List<string>();

        public static List<string> ListaErrores = new List<string>();
        #endregion

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
            Color Color = Color.Orange;

            if (LectorSintactico.IsFuncion(s))
            {
                Color = Color.Green;
            }

            if (LectorSintactico.IsLlave(s))
            {
                Color = Color.Pink;
            }

            if (LectorSintactico.IsSeparador(s))
            {
                Color = Color.Blue;
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
                richTextBox1.SelectionColor = comentario;
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
                        richTextBox1.SelectionColor = this.comentario;

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
                    richTextBox1.SelectionColor = this.comentario;

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
                "Entero = 10",
                "Cadena cadena = Hola mundo",

            };
            richTextBox1.Lines = codigo;
        }

        #endregion

        #region eventos


        private void Form2_Load(object sender, EventArgs e)
        {
            LectorSintactico = new LectorSintaxis("Palabras.syntax");
            CodigoInicio();
            this.CrearSintaxisColorAllText(richTextBox1.Text);
            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;
            this.Text = "Lenguajes Formlaes IDE" + version;
            this.richTextBox1.AcceptsTab = true;
           // tabControl1.Visible = false;
          //  richTextBox1.Select();
          //  richTextBox1.DetectUrls = true;
        }

        private void richTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            if (Poblamiento)
                return;

            ColorSyntaxEditor.FlickerFreeRichEditTextBox._Paint = false;
            CrearSintaxisPorCadaLinea();
            ColorSyntaxEditor.FlickerFreeRichEditTextBox._Paint = true;
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Tab:
                    var d = richTextBox1.SelectionTabs;
                    break;
            }
        }
        private void ANALIZAR_COMPILAR()
        {
            Compilador compilador = new Compilador();
            AnalizadorSemantico semantico = new AnalizadorSemantico();
            List<string> CodigoComputado = new List<string>();
            toolErrorSintaxis.Text = "";
            try
            {
                toolProgreso.Increment(20);
                //toolnotificaciones.Text = "Analizando codigo... (20%)";
                List<string> Codigo = new List<string>();
                Codigo.AddRange(richTextBox1.Lines);
                semantico.SetCodigoAnalizar(Codigo);
                semantico.Computar(out CodigoComputado);
                toolProgreso.Increment(30);
                List<string> Errores = semantico.MostrarErrores();
                if (Errores.Count != 0)
                {
                    toolErrorSintaxis.Text = "Error al compilar... ";
                    ErroresDeTokens.ListaErrores = Errores;
                    ErroresDeTokens FrmError = new ErroresDeTokens();
                    FrmError.Show();
                //    toolnotificaciones.Text = "sin notificaciones...";
                    toolProgreso.Increment(100);
                    return;
                }
                string direccion = Archivos.Direccion;
                if (direccion == null || direccion == "")
                {
                    GetCodigoEscrito();
                    Archivos.Guardar(CodigoEscrito);
                    direccion = Archivos.Direccion;
                }

                string[] trozo_direccion = direccion.Split(new string[] { "\\", ".cs" }, StringSplitOptions.RemoveEmptyEntries);
                string nombre = trozo_direccion[trozo_direccion.Length - 1];
                if (nombre == "" || string.IsNullOrEmpty(nombre))
                    nombre = "Lenguajes";
              //  toolnotificaciones.Text = "Compilando... (60%)";
                toolProgreso.Increment(60);

                var d = compilador.CheckCodigoAcompilar(CodigoComputado);
                var k = compilador.GenerarCodigoCsharp(d, "__IL_SISTEMA_INT");
                List<string> ILerr = new List<string>();
                bool compilado = compilador.CompilarCodigo(k, nombre + ".exe", out ILerr);

                if (compilado)
                {
                    Process p = new Process();
                    ProcessStartInfo psi = new ProcessStartInfo(System.IO.Directory.GetCurrentDirectory() + @"\" + nombre + ".exe");
                    p.StartInfo = psi;
                    p.Start();
                    toolErrorSintaxis.Text = "EL trabajo se a copilado ";
                //    toolnotificaciones.Text = "sin notificaciones...";
                }
                else
                {
                    if (ILerr.Count >= 1)
                    {
                        ErroresDeTokens.ListaErrores = ILerr;
                        ErroresDeTokens FrmError = new ErroresDeTokens();
                        FrmError.Show();


                    }
                    toolErrorSintaxis.Text = "Compilacion exitosa pero contiene un error de compilacion";
                }
                toolProgreso.Increment(100);

            }
            catch (Exception ex)
            {
                toolProgreso.Increment(0);
                MessageBox.Show(ex.Message);
            }
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            if (Archivos.Direccion != null) Archivos.Direccion = null;
        }

        private void cerrarProyectoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // tabControl1.Visible = false;
        }

        private void crearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //  abrirarchivo();
            try
            {
                string[] datos = Archivos.AbrirArchivo();
                if (datos != null)
                {
                    richTextBox1.Lines = datos;
                }
            }
            catch { }
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
                       // tabControl1.Visible = true;
                    }

                }
            }
            catch (Exception)
            {

                MessageBox.Show("El archivo no se abrio correctamente");

               // tabla_errorres.addliste(2);
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

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
           // toolnotificaciones.Text = "Compilando espere..";
            toolProgreso.Style = ProgressBarStyle.Continuous;
            toolProgreso.Overflow = ToolStripItemOverflow.Always;
            toolProgreso.Increment(10);
            System.Threading.Thread hilo =
                new System.Threading.Thread(delegate ()
                {
                    ANALIZADOR_SEMANTICO_SINTACTICO__ analizador = new ANALIZADOR_SEMANTICO_SINTACTICO__(ANALIZAR_COMPILAR);
                    this.Invoke(analizador);
                });
            hilo.Start();

        }

//Codigo para la utilizacion de los codigos de la misma
        private void informacionToolStripMenuItem_Click(object sender, EventArgs e)
        {

            MessageBox.Show("David Enrique Lux Barrera 201931344");
        }

        private void guardarComoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCodigoEscrito();
            Archivos.Guardar(CodigoEscrito, true);
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCodigoEscrito();
            Archivos.Guardar(CodigoEscrito);
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetCodigoEscrito();
            Archivos.Guardar(CodigoEscrito);
            Application.Exit();
        }

        private void richTextBox1_TextChanged_2(object sender, EventArgs e)
        {

        }
    }
}