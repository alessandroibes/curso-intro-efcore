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
            //InserirDados();

            //InserirDadosEmMassa();

            InserirDadosEmMassa2();
        }

        private static void InserirDadosEmMassa2()
        {
            var listaClientes = new[]
            {
                new Cliente()
                {
                    Nome = "Teste 1",
                    CEP = "11111111",
                    Cidade = "Cidade Teste 1",
                    Estado = "SE",
                    Telefone = "222222222"
                },
                new Cliente()
                {
                    Nome = "Teste 2",
                    CEP = "33333333",
                    Cidade = "Cidade Teste 2",
                    Estado = "PI",
                    Telefone = "4444444444"
                }
            };

            using var db = new Data.ApplicationContext();
            db.Clientes.AddRange(listaClientes);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registro(s): {registros}");
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto()
            {
                Descricao = "Produto Teste",
                CodigoBarras = "1234567891231",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente()
            {
                Nome = "Alessandro Oliveira",
                CEP = "99999000",
                Cidade = "Fortaleza",
                Estado = "CE",
                Telefone = "999990000"
            };

            using var db = new Data.ApplicationContext();
            db.AddRange(produto, cliente);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registro(s): {registros}");
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
