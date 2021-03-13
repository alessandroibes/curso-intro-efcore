using System;
using System.Collections.Generic;
using System.Linq;
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
            //InserirDadosEmMassa2();
            //ConsultarDados();
            //CadastrarPedido();
            //ConsultarPedidoCarregamentoAdiantado();
            //AtualizarDados();
            //AtualizarDadosDesconectado();
            //AtualizarDadosDesconectado2();
            //RemoverRegistro();
            RemoverRegistroDesconectado();
        }

        private static void RemoverRegistroDesconectado()
        {
            using var db = new Data.ApplicationContext();
            var cliente = new Cliente() { Id = 3 };

            db.Clientes.Remove(cliente);
            //db.Remove(cliente);
            //db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }

        private static void RemoverRegistro()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(2);

            db.Clientes.Remove(cliente);
            //db.Remove(cliente);
            //db.Entry(cliente).State = EntityState.Deleted;

            db.SaveChanges();
        }

        private static void AtualizarDadosDesconectado2()
        {
            using var db = new Data.ApplicationContext();
            var cliente = new Cliente
            {
                Id = 1
            };
            
            var clienteDesconectado = new
            {
                Nome = "Cliente Desconectado Passo 2",
                Telefone = "666666666"
            };

            db.Attach(cliente); // Começa a rastrear o objeto
            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);
            db.SaveChanges();
        }

        private static void AtualizarDadosDesconectado()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(1);
            var clienteDesconectado = new
            {
                Nome = "Cliente Desconectado",
                Telefone = "555555555"
            };

            db.Entry(cliente).CurrentValues.SetValues(clienteDesconectado);
            db.SaveChanges();
        }

        private static void AtualizarDados()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(1);
            cliente.Nome = "Cliente Alterado Passo 2";

            //db.Clientes.Update(cliente); // Isso irá indicar que eu estou alterando todos os dados do cliente, o que não é verdade
            db.SaveChanges();
        }

        private static void ConsultarPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db.Pedidos
                            .Include(p => p.Itens)
                                .ThenInclude(p => p.Produto)
                            .ToList();

            Console.WriteLine(pedidos.Count);
        }

        private static void CadastrarPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido()
            {
                ClienteId = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10,
                    }
                }
            };

            db.Pedidos.Add(pedido);
            db.SaveChanges();
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            //var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            var consultaPorMetodo = db.Clientes.AsNoTracking().Where(p => p.Id > 0).ToList();
            foreach (var cliente in consultaPorMetodo)
            {
                Console.WriteLine($"Consultando Cliente: {cliente.Id}");

                // Busca os dados na memória, mas se for usado AsNoTracking() ao trazer os dados, 
                // o método Find() irá buscar na base de dados e não na memória. 
                // Esse é o único método que busca os dados em memória.
                db.Clientes.Find(cliente.Id);

                //db.Clientes.FirstOrDefault(p => p.Id == cliente.Id); // Sempre busca os dados na base de dados
            }
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
