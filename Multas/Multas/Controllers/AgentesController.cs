using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MultasProj.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace MultasProj.Controllers
{
    public class AgentesController : Controller
    {
        // cria um objeto privado, que representa a base de dados
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Agentes
        public ActionResult Index()
        {


            //recuperar os dados pessoais da pessoa que se autenticou
            var dadosPessoais = db.Users.Find(User.Identity.GetUserId());
            //agora, com este objeto, ja posso utilizar os dados pessoais
            //de um utilizador no meu programa
            //por exemplo:
            Session["nomeUtilizador"] = dadosPessoais.NomeProprio + " " + dadosPessoais.Apelido;


            // (LINQ)db.Agente.ToList() --> em SQL: SELECT * FROM Agentes ORDER BY Nome
            // constroi uma lista com os dados de todos os Agentes
            // e envia-a para a View

            var listaAgentes = db.Agentes.ToList().OrderBy( a => a.Nome);

            return View(listaAgentes);
        }

        // GET: Agentes/Details/5
        /// <summary>
        /// Apresenta os detalhes de um Agente
        /// </summary>
        /// <param name="id"> representa a PK que identifica o Agente </param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {

            // int? - significa que pode haver valores nulos

            // protege a execução do método contra a Não existencia de dados
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                //ou nao foi introduzido um ID valido, ou foi introduzido um valor completamente errado
                return RedirectToAction("Index");
            }

            // vai procurar o Agente cujo ID foi fornecido
            Agentes agente = db.Agentes.Find(id);

            // se o Agente NÃO for encontrado...
            if (agente == null)
            {
                // return HttpNotFound();
                return RedirectToAction("Index");
            }

            // envia para a View os dados do Agente
            return View(agente);
        }





        // GET: Agentes/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: Agentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nome,Esquadra,Fotografia")] Agentes agente,
                                   HttpPostedFileBase fileUploadFotografia)
        {

            // determinar o ID do novo Agente
            int novoID = 0;
            //***********************************************
            //proteger a geraçao de um novo ID
            //***********************************************
            //determinar o nº de Agentes na tabela
            if (db.Agentes.Count() == 0)
            {
                novoID = 1;
            }
            else{
             novoID = db.Agentes.Max(a => a.ID) + 1;

            }

           // atribuir o ID ao novo agente
            agente.ID = novoID;
            //********************************************
            //outra hipotse possivel seria utilizar o 
            //try{ }
            //catch(Exception) { }
            //********************************************


            // var. auxiliar
            string nomeFotografia = "Agente_" + novoID + ".jpg";
            string caminhoParaFotografia = Path.Combine(Server.MapPath("~/imagens/"), nomeFotografia); // indica onde a imagem será guardada

            // verificar se chega efetivamente um ficheiro ao servidor
            if (fileUploadFotografia != null)
            {
                // guardar o nome da imagem na BD
                agente.Fotografia = nomeFotografia;
            }
            else
            {
                // não há imagem...
                ModelState.AddModelError("", "Não foi fornecida uma imagem..."); // gera MSG de erro
                return View(agente); // reenvia os dados do 'Agente' para a View
            }

            //    verificar se o ficheiro é realmente uma imagem ---> casa
            //    redimensionar a imagem --> ver em casa

            // ModelState.IsValid --> confronta os dados fornecidos com o modelo
            // se não respeitar as regras do modelo, rejeita os dados
            if (ModelState.IsValid)
            {
                try
                {
                // adiciona na estrutura de dados, na memória do servidor,
                // o objeto Agentes
                db.Agentes.Add(agente);
                // faz 'commit' na BD
                db.SaveChanges();

                // guardar a imagem no disco rígido
                fileUploadFotografia.SaveAs(caminhoParaFotografia);

                // redireciona o utilizador para a página de início
                return RedirectToAction("Index");

                }
                catch (Exception)
                {
                    //gerar uma mensagem de erro para o utilizador 
                    ModelState.AddModelError("", "Ocorreu um erro nao determinado na criaçao do novo Agente...");
                }
               
            }
            //se se chegar aqui, é pq aconteceu um problema...
            // devolve os dados do agente à View
            return View(agente);
        }




        // GET: Agentes/Edit/5
        public ActionResult Edit(int? id)
        {
            // int? - significa que pode haver valores nulos

            // protege a execução do método contra a Não existencia de dados
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                //ou nao foi introduzido um ID valido, ou foi introduzido um valor completamente errado
                return RedirectToAction("Index");
            }

            // vai procurar o Agente cujo ID foi fornecido
            Agentes agente = db.Agentes.Find(id);

            // se o Agente NÃO for encontrado...
            if (agente == null)
            {
                // return HttpNotFound();
                return RedirectToAction("Index");
            }

            return View(agente);
        }





        // POST: Agentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Fotografia,Esquadra")] Agentes agente)
        {
            //falta tratar das imagens como foi feito no Create


            if (ModelState.IsValid)
            {
                // atualiza os dados do Agente, na estrutura de dados em memória
                db.Entry(agente).State = EntityState.Modified;
                // Commit
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agente);
        }




        // GET: Agentes/Delete/5
        /// <summary>
        /// procura os dados de um agente, e mostra-os no ecran
        /// </summary>
        /// <param name="id">identificador do agente a pesquisar</param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                //ou nao foi introduzido um ID valido, ou foi introduzido um valor completamente errado
                return RedirectToAction("Index");
            }

            // vai procurar o Agente cujo ID foi fornecido
            Agentes agente = db.Agentes.Find(id);

            // se o Agente NÃO for encontrado...
            if (agente == null)
            {
                // return HttpNotFound();
                return RedirectToAction("Index");
            }
            return View(agente);
        }



        // POST: Agentes/Delete/5
        /// <summary>
        /// concretiza, torna definitiva (qd possivel) a remoçao de um Agente
        /// </summary>
        /// <param name="id">é o identificador do agente a remover</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // procurar o Agente
            Agentes agente = db.Agentes.Find(id);

            try
            {
            // remover da memória
            db.Agentes.Remove(agente);
            // commit na BD
            db.SaveChanges();
                //redirecionar para a pagina inicial 
            return RedirectToAction("Index");
            }
            catch (Exception)
            {
                //gerar uma mensagem de erro, a ser apresentada ao utilizar
                ModelState.AddModelError("", string.Format("Nao foi possivel remover o Agente '{0}', porque existem {1} multas associadas a ele. ",agente.Nome, agente.ListaDeMultas.Count));
            }
            //reenviar os dados para a View

            return View(agente);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}