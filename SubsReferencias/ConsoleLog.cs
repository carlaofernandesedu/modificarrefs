using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsReferencias
{
    public class ConsoleLog : Log
    {
        public override void Escrever(string conteudo)
        {
            Console.WriteLine(DateTime.Now.ToLongTimeString() + " " + conteudo);
        }
    }
}
