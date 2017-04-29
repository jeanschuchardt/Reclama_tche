using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReclamaTche.Models
{
    public class Initializer : System.Data.Entity.DropCreateDatabaseAlways<ReclamaDBContext>
    {
        protected override void Seed(ReclamaDBContext context)
        {
            var categorias = new List<Categoria>
            {
                new Categoria { Nome = "Água",Descricao ="Falta d'água, Problemas com esgoto",CategoriaID =1},
                new Categoria { Nome = "Árvore",Descricao ="Árvore caida, galho quebrado",CategoriaID =2},
                new Categoria { Nome = "Energia", Descricao ="Falta de energia",CategoriaID =3},
                new Categoria { Nome = "Lixo", Descricao ="Falta de coleta de lixo, acumulo de lixo",CategoriaID =4 },
                new Categoria { Nome = "Queimada", Descricao="Queimada",CategoriaID =5},
                new Categoria { Nome = "Rua",Descricao ="rua escura, buraco na rua, calçada desnivelada, falto de rampa para cadeirante",CategoriaID =6},
                new Categoria { Nome = "Alagamento",Descricao ="Alagamento",CategoriaID =7},
                new Categoria { Nome = "Pichação",Descricao ="Pichação de monumentos públicos",CategoriaID =8},
                new Categoria { Nome = "Transito",Descricao ="Falta de sinalização",CategoriaID =9},

             };

            categorias.ForEach(s => context.Categorias.Add(s));
            context.SaveChanges();



            var reclamacoes = new List<Reclamacao>
            {

                new Reclamacao
                {
                    Nome = "Buraco",
                    Endereco = "Av. Protásio Alves",
                    Bairro ="Petrópolis",
                    ReleaseDate = new DateTime(2016, 06, 01),
                    Descricao = "Buraco na rua próximo ao nº 1875",
                    Status = Status.ABERTO,
                    CategoriaID = 6,
                    Usuario = "jesus@s2b.com"
                },

                new Reclamacao
                {
                    Nome = "Árvore",
                    Endereco = "Av. Bagé, 73",
                    Bairro ="Petrópolis",
                    ReleaseDate = new DateTime(2016, 06, 06),
                    Descricao = "Arvore quebrada",
                    Status = Status.ENCERRADO,
                    CategoriaID = 2,
                    Usuario = "joao@s2b.com"
                },
                new Reclamacao
                {
                    Nome = "Árvore quebrada",
                    Endereco = "Av. José Bonifácil, 363",
                    Bairro ="Petrópolis",
                    ReleaseDate = new DateTime(2016, 06, 05),
                    Descricao = "Arvore quebrada",
                    Status = Status.RESOLVIDO,
                    CategoriaID = 2,
                    Usuario = "oliveira@s2b.com"
                },

                 new Reclamacao
                {
                    Nome = "Árvore quebrada",
                    Endereco = "Av. José Bonifácil, 363",
                    Bairro ="Bom Fim",
                    ReleaseDate = new DateTime(2016, 06, 05),
                    Descricao = "Arvore quebrada",
                    Status = Status.RESOLVIDO,
                    CategoriaID = 2,
                    Usuario = "oliveira@s2b.com"
                },
                  new Reclamacao
                {
                    Nome = "Acumulo  de lixo",
                    Endereco = "R. Gen. Lima e Silva, 963",
                    Bairro ="Cidade Baixa",
                    ReleaseDate = new DateTime(2016, 05,30 ),
                    Descricao = "Muitas garafas e copos espalhados pela rua acumulo muito maior aos fins de semana",
                    Status = Status.ABERTO,
                    CategoriaID = 4,
                    Usuario = "oliveira@s2b.com"
                },

                       new Reclamacao
                {
                    Nome = "Esgoto a Céu aberto",
                    Endereco = "Rua Senador Annibal Di Primio Beck",
                    Bairro ="Cidade Baixa",
                    ReleaseDate = new DateTime(2016, 05,30 ),
                    Descricao = "Falta de condições mínimas de saneamento básico",
                    Status = Status.ABERTO,
                    CategoriaID = 1,
                    Usuario = "oliveira@s2b.com"
                },

                new Reclamacao
                {
                    Nome = "Pichação do Monumento ao Expedicionário",
                    Endereco = "Av. José Bonifácil, 363",
                    Bairro ="Bom Fim",
                    ReleaseDate = new DateTime(2016, 06,07 ),
                    Descricao = "O arco foi todo rabiscado com uns simbolos estranhos",
                    Status = Status.ABERTO,
                    CategoriaID = 8,
                    Usuario = "huguinho@s2b.com"
                },
                 new Reclamacao
                {
                    Nome = "Alagamento na Goette",
                    Endereco = "Av. Goette, 125",
                    Bairro ="Rio Branco",
                    ReleaseDate = new DateTime(2016, 06,07 ),
                    Descricao = "É cair uma chuva que essa avenida submerge ",
                    Status = Status.ABERTO,
                    CategoriaID = 7,
                    Usuario = "huguinho@s2b.com"
                },
                new Reclamacao
                {
                    Nome = "Sinaleira quebrada",
                    Endereco = "Rua Professor Cristiano Fischer",
                    Bairro ="Petrópolis",
                    ReleaseDate = new DateTime(2016, 06, 08),
                    Descricao = "Devido uma colisão entre dois veículos a cinaleira caiu e quebrou",
                    Status = Status.ABERTO,
                    CategoriaID = 9,
                    Usuario = "huguinho@s2b.com"},

                new Reclamacao {
                   Nome = "Arvore",
                   Endereco = "Av. Bento Gonsalves",
                   Bairro="Agronomia",
                   ReleaseDate = new DateTime(2016, 06, 03),
                   Descricao = "OMG OMG OMG",
                   Status = Status.ENCERRADO,
                   CategoriaID = 2,
                   Usuario = "odin@s2b.com"
                   },
                new Reclamacao
                {
                    Nome = "Buraco",
                    Endereco = "Av. Fernandes Vieira",
                    Bairro = "Bom Fim",
                    ReleaseDate = new DateTime(2016, 05, 27),
                    Descricao = "OMG OMG OMG",
                    Status = Status.ABERTO,
                    CategoriaID = 6,
                    Usuario = "jesus@s2b.com"
                },
                new Reclamacao
                {
                    Nome = "Buraco",
                    Endereco = "Av. Oswaldo Aranha",
                    Bairro = "Bom Fim",
                    ReleaseDate = new DateTime(2016, 05, 19),
                    Descricao = "OMG OMG OMG",
                    Status = Status.ENCERRADO,
                    CategoriaID = 4,
                    Usuario = "jesus@s2b.com"
                },
                new Reclamacao
                {
                    Nome = "Fogo",
                    Endereco = "Av.Gen Lima e silva",
                    Bairro = "Cidade Baixa",
                    ReleaseDate = new DateTime(2016, 05, 20),
                    Descricao = "OMG OMG OMG",
                    Status = Status.ABERTO,
                    CategoriaID = 5,
                    Usuario = "jesus@s2b.com"
                },
                new Reclamacao
                {
                    Nome = "Falta de luz",
                    Endereco = "Av. Joao teles",
                    Bairro = "Bom Fim",
                    ReleaseDate = new DateTime(2016, 06, 07),
                    Descricao = "Poste caido",
                    Status = Status.RESOLVIDO,
                    CategoriaID = 3,
                    Usuario = "huguinho@s2b.com"
                },
                new Reclamacao
                {
                    Nome = "Rua escura",
                    Endereco = "Av. Joao alfredo",
                    Bairro = "Cidade Baixa",
                    ReleaseDate = new DateTime(2014, 06, 08),
                    Descricao = "Não há luz na rua, falta de iluminação pública",
                    Status = Status.RESOLVIDO,
                    CategoriaID = 6,
                    Usuario = "huguinho@s2b.com"
                },

            };

            reclamacoes.ForEach(s => context.Reclamacoes.Add(s));
            context.SaveChanges();


            var t = context.Reclamacoes;
        
            var comentarios = new List<Comentario>
            {
                new Comentario { ReclamacaoID = 1,
                    Body = "Isto é um comentário Isto é um comentário Isto é um comentário Isto é um comentário Isto é um comentário",
                    ReleaseDate = new DateTime(2016,06,01),
                    Usuario = "oliveira@s2b.com"},
                  new Comentario { ReclamacaoID = 1,
                    Body = "Isto é um comentário Isto é um comentário Isto é um comentário Isto é um comentário Isto é um comentário",
                    ReleaseDate = new DateTime(2016,06,01),
                    Usuario = "oliveira@s2b.com"},
                    new Comentario { ReclamacaoID = 1,
                    Body = "Isto é um comentário Isto é um comentário Isto é um comentário Isto é um comentário Isto é um comentário",
                    ReleaseDate = new DateTime(2016,06,01),
                    Usuario = "oliveira@s2b.com"},
                      new Comentario { ReclamacaoID = 2,
                    Body = "Isto é um comentário Isto é um comentário Isto é um comentário Isto é um comentário Isto é um comentário",
                    ReleaseDate = new DateTime(2016,06,01),
                    Usuario = "oliveira@s2b.com"},
                        new Comentario { ReclamacaoID = 2,
                    Body = "Isto é um comentário Isto é um comentário Isto é um comentário Isto é um comentário Isto é um comentário",
                    ReleaseDate = new DateTime(2016,06,01),
                    Usuario = "oliveira@s2b.com"},
                          new Comentario { ReclamacaoID = 3,
                    Body = "Isto é um comentário Isto é um comentário Isto é um comentário Isto é um comentário Isto é um comentário",
                    ReleaseDate = new DateTime(2016,06,01),
                    Usuario = "oliveira@s2b.com"},
                            new Comentario { ReclamacaoID = 4,
                    Body = "Isto é um comentário Isto é um comentário Isto é um comentário Isto é um comentário Isto é um comentário",
                    ReleaseDate = new DateTime(2016,06,01),
                    Usuario = "oliveira@s2b.com"},
                              new Comentario { ReclamacaoID = 4,
                    Body = "Isto é um comentário Isto é um comentário Isto é um comentário Isto é um comentário Isto é um comentário",
                    ReleaseDate = new DateTime(2016,06,01),
                    Usuario = "oliveira@s2b.com"},
                new Comentario { ReclamacaoID = 10, Body = "teste", ReleaseDate = new DateTime(2016,06,01), Usuario = "lucifer@s2b.com"},
                new Comentario { ReclamacaoID = 7, Body = "teste", ReleaseDate = new DateTime(2016,06,01), Usuario = "lucifer@s2b.com"},
                new Comentario { ReclamacaoID = 6, Body = "teste", ReleaseDate = new DateTime(2016,06,01), Usuario = "lucifer@s2b.com"},
                new Comentario { ReclamacaoID = 3, Body = "teste", ReleaseDate = new DateTime(2016,06,01), Usuario = "lucifer@s2b.com"},
                new Comentario { ReclamacaoID = 4, Body = "teste", ReleaseDate = new DateTime(2016,06,01), Usuario = "lucifer@s2b.com"},
                new Comentario { ReclamacaoID = 4, Body = "teste", ReleaseDate = new DateTime(2016,06,01), Usuario = "lucifer@s2b.com"},
                new Comentario { ReclamacaoID = 5, Body = "teste", ReleaseDate = new DateTime(2016,06,01), Usuario = "lucifer@s2b.com"},
                new Comentario { ReclamacaoID = 6, Body = "teste", ReleaseDate = new DateTime(2016,06,01), Usuario = "lucifer@s2b.com"},
                new Comentario { ReclamacaoID = 7, Body = "teste", ReleaseDate = new DateTime(2016,06,01), Usuario = "lucifer@s2b.com"},


            };

            comentarios.ForEach(s => context.Comentarios.Add(s));
            context.SaveChanges();

        }
    }
}