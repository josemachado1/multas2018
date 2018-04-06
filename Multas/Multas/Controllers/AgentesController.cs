﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Multas.Models;

namespace Multas.Controllers
{
    public class AgentesController : Controller
    {
        //  cria um objeto privado, que representa a base de dados
        private MultasDb db = new MultasDb();

        // GET: Agentes
        public ActionResult Index()
        {
            //(LINQ)db.Agentes.ToList() --> em SQL: Select * FROM Agentes
            //constroi uma lista com os dados de todos os Agentes e envia-a para a View
            return View(db.Agentes.ToList());
        }

        // GET: Agentes/Details/5
        /// <summary>
        /// Apresenta os detalhes do Agente
        /// </summary>
        /// <param name="id">representa a PK que identifica o Agente</param>
        /// <returns></returns>
        public ActionResult Details(int? id)
        {
            //int? - significa que pode ter valores nulos

            //proteger a execuçao do metodo contra a Nao existencia de dados
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //vai procurar a Agente cujo ID foi foenecido
            Agentes agentes = db.Agentes.Find(id);

            //se o Agente NAO for encontrado...
            if (agentes == null)
            {
                return HttpNotFound();
            }

            //envia para a View os dados do Agente
            return View(agentes);
        }

        // GET: Agentes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agentes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nome,Fotografia,Esquadra")] Agentes agentes)
        {
            //ModelState.IsValid --> confronta os dados fornecidos com o modelo se nao respeitar as regras do modelo, rejeita os dados
            if (ModelState.IsValid)
            {
                //adiciona na estrutura os dados, na memoria do servidor, o objeto Agentes
                db.Agentes.Add(agentes);
                //faz 'commit' na BD
                db.SaveChanges();
                //redireciona o utilizador para a pagina de inicio
                return RedirectToAction("Index");
            }

            //devolve os dados do agente à View
            return View(agentes);
        }

        // GET: Agentes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                return HttpNotFound();
            }
            return View(agentes);
        }

        // POST: Agentes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nome,Fotografia,Esquadra")] Agentes agentes)
        {
            if (ModelState.IsValid)
            {
                //atualiza os dados do Agente, na estrutura de dados em memoria
                db.Entry(agentes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agentes);
        }

        // GET: Agentes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agentes agentes = db.Agentes.Find(id);
            if (agentes == null)
            {
                return HttpNotFound();
            }
            return View(agentes);
        }

        // POST: Agentes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //procurar o Agente
            Agentes agentes = db.Agentes.Find(id);
            //remover da memoria
            db.Agentes.Remove(agentes);
            //commit na BD
            db.SaveChanges();
            return RedirectToAction("Index");
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