﻿using ASP_MVC_Website.Models;
using ASP_MVC_Website.ViewModel;
using ATAB_CMS_Website.Models.User_BL;
using Repository_UnitOfWork_DAL.EF_Data_Model;
using Repository_UnitOfWork_DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASP_MVC_Website.Controllers
{
    public class NewsController : Controller
    {
        private INewsBusiness _businessNews;
        private IUserBusiness _businessUser;

        public NewsController()
        {
            _businessNews = new NewsBusiness(new ModelStateWrapper(this.ModelState), new UnitOfWork());
            _businessUser = new UserBusiness(new UnitOfWork());
        }        

        // GET: /News/

        public ActionResult Index()
        {
            return View(_businessNews.ListNews());
        }

        // GET: /News/Details/5

        public ActionResult Details(int id = 0)
        {
            NewsUserViewModel news = _businessNews.DetailsNews(id); 
            if (news == null)
            {
                return HttpNotFound();
            }

            return View(news);
        }

        // GET: /News/Create

        public ActionResult Create()
        {
            ViewData["User"] = new SelectList(_businessUser.ListUser(), "UserId", "UserName");
            return View();
        }

        // POST: /News/Create

        [HttpPost]
        public ActionResult Create(News news)
        {
            if (ModelState.IsValid)
            {
                _businessNews.CreateNews(news);
                return RedirectToAction("Index");
            }

            ViewData["User"] = new SelectList(_businessUser.ListUser(), "UserId", "UserName");

            return View(news);
        }

        // GET: /News/Edit/5

        public ActionResult Edit(int id = 0)
        {
            News news = _businessNews.EditNews(id); 
            if (news == null)
            {
                return HttpNotFound();
            }

            ViewData["User"] = new SelectList(_businessUser.ListUser(), "UserId", "UserName");

            return View(news);
        }

        // POST: /News/Edit/5

        [HttpPost]
        public ActionResult Edit(News news)
        {
            if (ModelState.IsValid)
            {
                _businessNews.EditNews(news);
                return RedirectToAction("Index");
            }

            ViewData["User"] = new SelectList(_businessUser.ListUser(), "UserId", "UserName");

            return View(news);
        }

        // GET: /News/Delete/5

        public ActionResult Delete(int id = 0)
        {
            NewsUserViewModel news = _businessNews.DeleteNews(id);
            if (news == null)
            {
                return HttpNotFound();
            }

            return View(news);
        }

        // POST: /News/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _businessNews.DeleteNewsConfirm(id);           
            return RedirectToAction("Index");
        }
    }
}