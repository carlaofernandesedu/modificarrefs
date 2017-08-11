using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsReferencias
{
    class Program
    {
        static void Main(string[] args)
        {
            SubstituirReferencias();
        }


        static void SubstituirReferencias()
        {
            try
            {
                string pathbase = System.Reflection.Assembly.GetExecutingAssembly().Location;
                TratamentoArquivo arquivo = new TratamentoArquivo(new ConsoleLog());
                var nomesArquivos = Path.Combine(Path.GetDirectoryName(pathbase), "arquivosui.config");
                arquivo.ObterLista(nomesArquivos);
                var listadepara = Path.Combine(Path.GetDirectoryName(pathbase), "deparaui.config");
                arquivo.ObterListaDePara(listadepara);
                arquivo.ProcessarArquivos();
                Console.ReadLine();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
