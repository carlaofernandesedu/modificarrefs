using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsReferencias
{
    public class TratamentoArquivo
    {
        private Log _log;
        public string[] _listaArquivos { get; set; }
        public Dictionary<string,string> _listaDePara { get; set; }
        public TratamentoArquivo(Log log)
        {
            _log = log;
        }
        public string[] Caminhos { get; set; } 
        
        public void ObterLista(string arquivoConfig)
        {
            _log.Escrever(String.Format("Carregando os nomes de arquivos do arquivo {0}", arquivoConfig));
           _listaArquivos = File.ReadAllLines(arquivoConfig);
            _log.Escrever("Lista de arquivos carregado");
        }

        public void ObterListaDePara(string arquivoConfig)
        {
            _log.Escrever(String.Format("Carregando os parametros de DEPARA do arquivo {0}", arquivoConfig));
            using (StreamReader sr = File.OpenText(arquivoConfig))
            {
                _listaDePara = new Dictionary<string, string>();
                string[] registro;
                while (!sr.EndOfStream)
                {
                   registro  = sr.ReadLine().Split('|');
                   _listaDePara.Add(registro[0], registro[1]);
                }
            }
            _log.Escrever("Configuracao DEPARA carregado");
        }

        public void ProcessarArquivos()
        {
            foreach(var item in _listaArquivos)
            {
                CriarArquivoModificado(item);
            }
        }

        private void CriarArquivoModificado(string arquivo)
        {
            _log.Escrever(String.Format("Lendo o arquivo {0}", arquivo));
            var linhasArquivo = File.ReadAllLines(arquivo);
            var total = linhasArquivo.Length - 1;
            for (int i=0;i<= total;i++)
            {
                SubstituirConteudo(i, linhasArquivo[i], ref linhasArquivo);
            }
   
            string diretorio = ConfigurationManager.AppSettings["diretorio"];
            var novonomearq = Path.Combine(diretorio, Path.GetFileName(arquivo));
            _log.Escrever(String.Format("Criando o arquivo {0}", novonomearq));
            StringBuilder sb = new StringBuilder();
            using (var sw = File.CreateText(novonomearq))
            {
                for (var i = 0; i <= total; i++)
                {
                    if (i == total)
                        sw.Write(linhasArquivo[i]);
                    else
                        sw.WriteLine(linhasArquivo[i]);
                }
            }
            _log.Escrever(String.Format("arquivo {0} gerado", novonomearq));

        }

        private void SubstituirConteudo(int indice,string conteudo, ref string[] colecao)
        {

            foreach (var item in _listaDePara.Keys)
            {
                if (conteudo.IndexOf(item) > 0)
                {
                    var novoConteudo = conteudo.Replace(item, _listaDePara[item]);
                    colecao[indice] = novoConteudo;
                    _log.Escrever(String.Format("Linha {0} Substituida de {1} para {2}", indice, conteudo, novoConteudo));
                    break;
                }
            }
        }
    }
}
