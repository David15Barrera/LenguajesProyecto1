using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1Consola.Clases
{
    class Grahviz
    {
        public class TTreeNode
        {
            public string name;
            public double value;
            public TTreeNode left, right;

            // Constructor  to create a single node 
            public TTreeNode(string name, double d)
            {
                this.name = name;
                value = d;
                left = null;
                right = null;
            }

            public override string ToString()
            {
                return string.Format("\"{0}\"", name);
            }
        }

        public class TBinarySTree
        {
            // Implements:

            // count()
            // clear()
            // insert()
            // delete()
            // findSymbol()
            //
            // Usage:
            //
            //  TBinarySTree bt = new TBinarySTree();
            //  bt.insert ("Bill", "3.14");
            //  bt.insert ("John". 2.71");
            //  etc.
            //  node = bt.findSymbol ("Bill");
            //  WriteLine ("Node value = {0}\n", node.value);
            //

            private TTreeNode root;     // Points to the root of the tree
            private int _count = 0;

            public TTreeNode GetRoot()
            {
                return root;
            }



///            Luego un par de metodos para generar la representacion de cada nodo y sus relaciones en lenguaje para Dot

            public static string ToDotGraph(TBinarySTree tree)
            {
                StringBuilder b = new StringBuilder();
                b.Append("digraph G {" + Environment.NewLine);
                b.Append(ToDot(tree.GetRoot()));
                b.Append("}");
                return b.ToString();
            }


            public static string ToDot(TTreeNode node)
            {
                StringBuilder b = new StringBuilder();
                if (node.left != null)
                {
                    b.AppendFormat("{0}->{1}{2}", node.ToString(), node.left.ToString(), Environment.NewLine);
                    b.Append(ToDot(node.left));
                }

                if (node.right != null)
                {
                    b.AppendFormat("{0}->{1}{2}", node.ToString(), node.right.ToString(), Environment.NewLine);
                    b.Append(ToDot(node.right));
                }
                return b.ToString();
            }


        }
    }
}