using System;
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            InserirDados();
        }

        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();

            // Formas de rastrear uma entidade para posteriormente armazenar na base de dados
            db.Produtos.Add(produto);
            // db.Set<Produto>().Add(produto);
            // db.Entry(produto).State = EntityState.Added;
            // db.Add(produto); // adiciona uma pequena sobrecarga de processamento

            // Salva as alterações na base de dados
            var registros = db.SaveChanges(); // retorna a quantidade de registros afetados

            Console.WriteLine($"Total Registro(s): {registros}");
        }
    }
}
