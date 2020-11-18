using System;
using System.Reflection;
using System.Diagnostics;
using System.Media;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1Consola.Clases
{
   public class LectorSintaxis
	{
		private string Archivo;
		private ArrayList Llaves = new ArrayList();
		private ArrayList Funciones = new ArrayList();
		private ArrayList Comentario = new ArrayList();
		private ArrayList Separadores = new ArrayList();
		private ArrayList trueoflasee = new ArrayList();
		private ArrayList cadena = new ArrayList();
		private ArrayList Entero = new ArrayList();
		private ArrayList Caracter = new ArrayList();
		private ArrayList Decimal = new ArrayList();

		public ArrayList GetLlaves() { return this.Llaves; }

		public ArrayList GetFunciones() { return this.Funciones; }

		public ArrayList GetComentarios() { return this.Comentario; }

		public ArrayList GetSeparadores() { return this.Separadores; }

		public ArrayList Gettrueoflasee() { return this.trueoflasee; }

		public ArrayList Getcadena() { return this.cadena; }

		public ArrayList GetCaracter() { return this.Caracter; }

		public ArrayList GetEntero() { return this.Entero; }
		public ArrayList GetDecimal() { return this.Decimal; }

		public LectorSintaxis(string archivo)
		{
			FileStream fs = new FileStream(archivo, FileMode.Open, FileAccess.Read);
			StreamReader sr = new StreamReader(fs);
			Archivo = sr.ReadToEnd();
			sr.Close();
			fs.Close();
			FiltrarArreglo();
		}

		public void FiltrarArreglo()
		{
			StringReader sr = new StringReader(Archivo);
			string SigLinea;

			SigLinea = sr.ReadLine();
			SigLinea = SigLinea.Trim();

			while (SigLinea != null)
			{
				if (SigLinea == "[FUNCIONES]")
				{
					SigLinea = sr.ReadLine();
					if (SigLinea != null)
						SigLinea = SigLinea.Trim();
					while (SigLinea != null && SigLinea[0] != '['
						)
					{
						Funciones.Add(SigLinea);
						Funciones.Add(SigLinea.ToUpper());
						SigLinea = "";
						while (SigLinea != null && SigLinea == "")
						{
							SigLinea = sr.ReadLine();
							if (SigLinea != null)
								SigLinea = SigLinea.Trim();
						}
					}
				}

				if (SigLinea == "[CLAVES]")
				{

					SigLinea = sr.ReadLine();
					if (SigLinea != null)
						SigLinea = SigLinea.Trim();
					while (SigLinea != null && SigLinea[0] != '['
						)
					{
						Llaves.Add(SigLinea);
						Llaves.Add(SigLinea.ToUpper());
						SigLinea = "";
						while (SigLinea != null && SigLinea == "")
						{
							SigLinea = sr.ReadLine();
							if (SigLinea != null)
								SigLinea = SigLinea.Trim();
						}

					}
				}

				if (SigLinea == "[COMENTARIO]")
				{

					SigLinea = sr.ReadLine();
					if (SigLinea != null)
						SigLinea = SigLinea.Trim();
					while (SigLinea != null && SigLinea[0] != '['
						)
					{
						Comentario.Add(SigLinea);

						SigLinea = "";
						while (SigLinea != null && SigLinea == "")
						{
							SigLinea = sr.ReadLine();
							if (SigLinea != null)
								SigLinea = SigLinea.Trim();
						}

					}
				}

				if (SigLinea == "[SEPARADORES]")
				{

					SigLinea = sr.ReadLine();
					if (SigLinea != null)
						SigLinea = SigLinea.Trim();
					while (SigLinea != null && SigLinea[0] != '['
						)
					{
						Separadores.Add(@SigLinea);

						SigLinea = "";
						while (SigLinea != null && SigLinea == "")
						{
							SigLinea = sr.ReadLine();
							if (SigLinea != null)
								SigLinea = SigLinea.Trim();
						}

					}
				}
				if (SigLinea == "[trueoflasee]")
				{

					SigLinea = sr.ReadLine();
					if (SigLinea != null)
						SigLinea = SigLinea.Trim();
					while (SigLinea != null && SigLinea[0] != '['
						)
					{
						trueoflasee.Add(SigLinea);
						trueoflasee.Add(SigLinea.ToUpper());
						SigLinea = "";
						while (SigLinea != null && SigLinea == "")
						{
							SigLinea = sr.ReadLine();
							if (SigLinea != null)
								SigLinea = SigLinea.Trim();
						}

					}
				}
				if (SigLinea == "[CADENA]")
				{

					SigLinea = sr.ReadLine();
					if (SigLinea != null)
						SigLinea = SigLinea.Trim();
					while (SigLinea != null && SigLinea[0] != '['
						)
					{
						cadena.Add(SigLinea);
						cadena.Add(SigLinea.ToUpper());
						SigLinea = "";
						while (SigLinea != null && SigLinea == "")
						{
							SigLinea = sr.ReadLine();
							if (SigLinea != null)
								SigLinea = SigLinea.Trim();
						}

					}
				}
				if (SigLinea == "[ENTERO]")
				{

					SigLinea = sr.ReadLine();
					if (SigLinea != null)
						SigLinea = SigLinea.Trim();
					while (SigLinea != null && SigLinea[0] != '['
						)
					{
						Entero.Add(SigLinea);
						Entero.Add(SigLinea.ToUpper());
						SigLinea = "";
						while (SigLinea != null && SigLinea == "")
						{
							SigLinea = sr.ReadLine();
							if (SigLinea != null)
								SigLinea = SigLinea.Trim();
						}

					}
				}
				if (SigLinea == "[CARACTER]")
				{

					SigLinea = sr.ReadLine();
					if (SigLinea != null)
						SigLinea = SigLinea.Trim();
					while (SigLinea != null && SigLinea[0] != '['
						)
					{
						Caracter.Add(SigLinea);
						Caracter.Add(SigLinea.ToUpper());
						SigLinea = "";
						while (SigLinea != null && SigLinea == "")
						{
							SigLinea = sr.ReadLine();
							if (SigLinea != null)
								SigLinea = SigLinea.Trim();
						}

					}
				}
				if (SigLinea == "[DECIMAL]")
				{

					SigLinea = sr.ReadLine();
					if (SigLinea != null)
						SigLinea = SigLinea.Trim();
					while (SigLinea != null && SigLinea[0] != '['
						)
					{
						Decimal.Add(SigLinea);
						Decimal.Add(SigLinea.ToUpper());
						SigLinea = "";
						while (SigLinea != null && SigLinea == "")
						{
							SigLinea = sr.ReadLine();
							if (SigLinea != null)
								SigLinea = SigLinea.Trim();
						}

					}
				}


				if (SigLinea != null && SigLinea.Length > 0 && SigLinea[0] == '[')
				{
				}
				else
				{
					SigLinea = sr.ReadLine();
					if (SigLinea != null)
						SigLinea = SigLinea.Trim();
				}
			}

			Llaves.Sort();
			Funciones.Sort();
			Comentario.Sort();
			Separadores.Sort();
			trueoflasee.Sort();
			cadena.Sort();
			Entero.Sort();
			Caracter.Sort();
			Decimal.Sort();
		}

		public bool IsLlave(string s)
		{
			int index = Llaves.BinarySearch(s);
			if (index >= 0)
				return true;

			return false;
		}

		public bool IsFuncion(string s)
		{
			int index = Funciones.BinarySearch(s);
			if (index >= 0)
				return true;

			return false;
		}

		public bool IsCommentario(string s)
		{
			int index = Comentario.BinarySearch(s);
			if (index >= 0)
				return true;

			return false;
		}

		public bool IsSeparador(string s)
		{

			int index = Separadores.BinarySearch(s);
			if (index >= 0)
				return true;

			return false;
		}
		public bool Istrueoflasee(string s)
		{

			int index = trueoflasee.BinarySearch(s);
			if (index >= 0)
				return true;

			return false;
		}
		public bool Iscadena(string s)
		{

			int index = cadena.BinarySearch(s);
			if (index >= 0)
				return true;

			return false;
		}
		public bool IsEntero(string s)
		{

			int index = Entero.BinarySearch(s);
			if (index >= 0)
				return true;

			return false;
		}
		public bool IsCaracter(string s)
		{

			int index = Caracter.BinarySearch(s);
			if (index >= 0)
				return true;

			return false;
		}
		public bool IsDecimal(string s)
		{

			int index = Decimal.BinarySearch(s);
			if (index >= 0)
				return true;

			return false;
		}

	}
}
