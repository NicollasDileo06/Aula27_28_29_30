﻿using System;
using System.Collections.Generic;

namespace Aula27_28_29_30
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Produto p1 = new Produto();
            p1.Codigo = 4;
            p1.Nome = "Gibson";
            p1.Preco = 7500f;
           
            p1.Cadastrar(p1);

           Produto alterado = new Produto();
           alterado.Codigo = 3;
           alterado.Nome = "Ferdinando";
           alterado.Preco = 6800f;
           p1.Alterar(alterado);

            p1.Remover("Gibson");

            List<Produto> lista = p1.Ler();
          
            foreach(Produto item in lista) 
            {
            Console.WriteLine($"R$ {item.Preco} - {item.Nome}");
            }

        }
        
        
    }
}

