using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aula27_28_29_30
{
    public class Produto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public float Preco { get; set; }
        private const string PATH = "Datebase/Produto.csv";

        public Produto()
        {
            string pasta = PATH.Split('/')[0];
          if(!Directory.Exists(pasta))
          {
              Directory.CreateDirectory(pasta);
          }
        }

    /// <summary>
    /// Cadastra um produto
    /// </summary>
    /// <param name="prod">Objeto</param>

         public void Cadastrar(Produto prod)
         {
            var linha = new string[] { PrepararLinha(prod) };
            File.AppendAllLines(PATH, linha);
         }

         /// <summary>
         /// Lê o csv 
         /// </summary>
         /// <returns>Lista de produtos</returns>

         public List<Produto> Ler()
         {
             //criamos uma lista que servira como nosso retorno
             List<Produto> produtos = new List<Produto>();

             // Lemos o arquivo e transformamos em um array de linhas 
             // [0] = codigo=1;nome=Gibson;preco=4500
             // [1] = codigo=1;nome=Fender;preco=4500
             string[] linhas = File.ReadAllLines(PATH);

             foreach(string linha in linhas ){
                 
                 // Separamos os dados de cada linha com Split
                 // [0] = codigo=1
                 // [1] = nome=Gibson
                 // [2] = preco=7500
                string[] dado = linha.Split(";");

                 // Criamos instâncias de produtos para serem colocados na lista
                 Produto p = new Produto();
                 p.Codigo = Int32.Parse( Separar(dado[0] ));
                 p.Nome = Separar(dado[1]);
                 p.Preco = float.Parse( Separar(dado[2]));

                produtos.Add(p);
             }

             produtos = produtos.OrderBy(y => y.Nome).ToList();

             return produtos;
         } 

         /// <summary>
         /// Remove uma ou mais linhas que contenham o termo
         /// </summary>
         /// <param name="_termo">termo para ser buscado</param>


        public void Remover(string _termo){

            // criamos uma lista que servirá como uma espécie de backup para as linhas do csv
            List<string> linhas = new List<string>();

            //Ultilizamos a biblioteca StreamReader para ler nosso .csv
            using(StreamReader arquivo = new StreamReader (PATH))
            {
             string linha;
             while((linha = arquivo.ReadLine()) != null)
             {
             linhas.Add(linha);
             }
            }

             //Removemos as linhas que tiveram o termo passado como argumento
             //codigo=1;nome=Tagima;preco=7500
             //Tagima 
         linhas.RemoveAll(l => l.Contains(_termo ));

             //Reescrevemos nosso csv do zero
         ReescreverCSV(linhas);
        }

         public void Alterar(Produto produtoAlterado){

              // criamos uma lista que servirá como uma espécie de backup para as linhas do csv
            List<string> linhas = new List<string>();

            //Ultilizamos a biblioteca StreamReader para ler nosso .csv
            using(StreamReader arquivo = new StreamReader (PATH))
            {
             string linha;
             while((linha = arquivo.ReadLine()) != null)
             {
             linhas.Add(linha);
             }
            }

         // 0
         //codigo=3;nome=Squire;preco=7500
         //codigo=2;nome=Squire;preco=7500
         // 0              1
         //codigo = //codigo=3;nome=Squire;preco=7500
         //linhas.RemoveAll(x => x.Split(";")[0].Contains(_produtoAlterado.Codigo.ToString()));
         linhas.RemoveAll(z => z.Split(";")[0].Split("=")[1] == produtoAlterado.Codigo.ToString());

         linhas.Add( PrepararLinha( produtoAlterado ) );

         ReescreverCSV(linhas);
         }  

        /// <summary>
        /// Reescreve o CSV
        /// </summary>
        /// <param name="lines">Lista de linhas</param>

         private void ReescreverCSV(List<string> lines){
             
              using(StreamWriter output = new StreamWriter(PATH))
            {
                foreach(string ln in lines)
                {
                    output.Write(ln + "\n");
                }
            }

         }  

         
        
          public List<Produto> Filtrar(string _nome)
          {
              return Ler().FindAll(x => x.Nome == _nome);
          }

         private string Separar(string _coluna)
         {
             //0      1
             //nome =  Gibson
             return _coluna.Split("=")[1];
         }

       // 1;Celular;600

       public string PrepararLinha(Produto p){
           return $"codigo={p.Codigo};nome={p.Nome};preco={p.Preco}";
       }

    }
}
