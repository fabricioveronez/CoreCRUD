using CoreCRUD.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCRUD.Test.Integration.Data
{
    public static class DataHelper
    {

        private static Stack<string> ids = new Stack<string>();



        public static void StartDatabase()
        {

            MongoUrl url = new MongoUrl("mongodb://fabricioveronez:123456@ds113835.mlab.com:13835/core-crud");
            MongoClient client = new MongoClient(url);
            IMongoDatabase db = client.GetDatabase("core-crud");
            IMongoCollection<Produto> produtoCollection = db.GetCollection<Produto>("Produto");

            Produto produto;

            produto = new Produto()
            {
                Nome = "Monitor Alienware Free-sync De 24.5' Aw2518hf",
                Preco = 2000,
                Categorias = new string[] { "Monitor", "Informática" },
                Descricao = "Projetado para os mais entusiastas jogadores, veja os jogos ganharem vida no Monitor Alienware de 24.5 polegadas para jogos com iluminação AlienFX personalizado, taxas de atualização nativa ultrarrápida de 240 Hz, tempo de resposta de 1 ms e tecnologia AMD FreeSync com recursos gráficos inovadores."
            };
            produtoCollection.InsertOne(produto);
            ids.Push(produto.Id.ToString());

            produto = new Produto()
            {
                Nome = "Game FIFA 18 - Xbox 360",
                Preco = 199.99,
                Categorias = new string[] { "Games", "XBox", "Esportes" },
                Descricao = ""
            };
            produtoCollection.InsertOne(produto);
            ids.Push(produto.Id.ToString());

            produto = new Produto()
            {
                Nome = "Smartphone Asus Zenfone 4 Selfie Dual Chip Android 7 Tela 5.5' Snapdragon 64GB 4G Câmera Traseira 16MP Dual Frontal 20MP + 8MP - Dourado",
                Preco = 1499.99,
                Categorias = new string[] { "Zenfone", "Celulares" },
                Descricao = "O novo ZenFone 4 Selfie chegou para redefinir tudo o que você já viu sobre o fenômeno do autorretrato. Ele é ideal para quem quer tirar, não penas selfies, mas também wefies perfeitas! - Não conhece o novo fenômeno das wefies? São as selfies com várias pessoas! O sistema de câmera dupla frontal do ZenFone 4 Selfie tem lentes wide angle de 120° que permitem que você enquadre muitos amigos na sua selfie. E claro, com a incrível qualidade de imagem da câmera de 20 MP e flash frontal. Já o design fino, com cores metálicas, além de bonito e sofisticado, é perfeito para que você segure e capture momentos incríveis com muita facilidade e segurança. "
            };
            produtoCollection.InsertOne(produto);
            ids.Push(produto.Id.ToString());

            produto = new Produto()
            {
                Nome = "Box DVD Trilogia Robocop (3 DVDs)",
                Preco = 47.88,
                Categorias = new string[] { "DVD", "Filmes" },
                Descricao = "Os 3 filmes deste clássico que marcou as décadas de 80 e 90. Relembre esta história emocionante do policial Alez Murphy que se tornou Robocop: O Policial do Futuro."
            };
            produtoCollection.InsertOne(produto);
            ids.Push(produto.Id.ToString());

            produto = new Produto()
            {
                Nome = "Cooktop Brastemp Gourmand 4 Bocas Vitroceramico Preto Indução Eletrico 220v(Bdj62)",
                Preco = 2999.29,
                Categorias = new string[] { "Cooktop", "Eletrodomésticos" },
                Descricao = "Cooktop Brastemp Com Tecnologia De Indução De Última Geração E Design Sofisticado, Tem Maior Eficiência Energética (-40 De Consumo). Possui Timer Com Programação Individual Para Cada Boca Proporcionando Controle Preciso Para Receitas Sempre No Ponto Perfeito Com O Desligamento Automático Ao Final Do Tempo De Cocção."
            };
            produtoCollection.InsertOne(produto);
            ids.Push(produto.Id.ToString());

            produto = new Produto()
            {
                Nome = "Notebook 2 em 1 Dell Inspiron I11-3168-A10 Intel Pentium 4GB 500GB Tela LED 11,6 Windows 10 - Cinza",
                Preco = 1899.99,
                Categorias = new string[] { "Notebook", "Informática" },
                Descricao = "Quanto mais você aprende, mais a Cortana se torna mais útil para você. Conte com a Cortana para ajudar você a encontrar coisas, realizar tarefas, definir lembretes e trabalhar em seus dispositivos de forma mais produtiva."
            };
            produtoCollection.InsertOne(produto);
            ids.Push(produto.Id.ToString());

            produto = new Produto()
            {
                Nome = "Home Theater Blu-Ray 3D Samsung HT-F4505 500W 5.1 Canais Full HD HDMI USB Rádio FM",
                Preco = 699.99,
                Categorias = new string[] { "Home Theater Blu-Ray" },
                Descricao = "O Home Theater possui diversas portas para facilitar a forma como você aprecia conteúdos. Reproduza o áudio por meio das entradas DLNA e USB. Jogue ou assista a programação em alta definição com a conexão HDMI ARC."
            };
            produtoCollection.InsertOne(produto);
            ids.Push(produto.Id.ToString());

            produto = new Produto()
            {
                Nome = "Sofá 3 Lugares Laguna Assento Retrátil e Encosto Reclinável em Suede Amassado Nogal",
                Preco = 650,
                Categorias = new string[] { "Sofá 3 Lugares", "Sofá" },
                Descricao = "O Sofá Laguna tem tudo a ver com a sua casa. Com 3 lugares, assentos retráteis, encostos reclináveis se encaixa perfeitamente em qualquer ambiente. Além de possuir design diferenciado, praticidade e conforto, o Sofá Laguna é 100% fibra e com madeira de eucalipto de áreas de reflorestamento. Uma combinação perfeita para quem procura sintonia entre qualidade e bom gosto. Aproveite!"
            };
            produtoCollection.InsertOne(produto);
            ids.Push(produto.Id.ToString());
        }

        public static string GetValidId()
        {
            MongoUrl url = new MongoUrl("mongodb://fabricioveronez:123456@ds113835.mlab.com:13835/core-crud");
            MongoClient client = new MongoClient(url);
            IMongoDatabase db = client.GetDatabase("core-crud");
            IMongoCollection<Produto> produtoCollection = db.GetCollection<Produto>("Produto");

            return produtoCollection.Find(FilterDefinition<Produto>.Empty).FirstOrDefault().Id;

            /// return ids.Pop();
        }

        public static string GetRamdonId()
        {
            return ObjectId.GenerateNewId().ToString();
        }

    }
}
