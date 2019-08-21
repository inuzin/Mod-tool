using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mod_Tool_test
{
    public class Mod
    {
        public String nome;
        public String[] filepaths;
        public String esp;

        public Mod(string Nome, string[] Filepaths, string Esp)
        {
            nome = Nome;
            filepaths = Filepaths;
            esp = Esp;
        }
    }
}
