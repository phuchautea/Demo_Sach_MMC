﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Caching;

namespace Demo_Sach_MMC.Controllers
{
    public class SachController : Controller
    {
        private static MemoryCache _bookCache = new MemoryCache("bookCache");
        MyDataDataContext data = new MyDataDataContext();
        public List<Sach> GetAll()
        {
            var books = _bookCache.Get("bookCache") as List<Sach>;
            if(books == null)
            {
                books = (from tt in data.Saches select tt).ToList();
                _bookCache.Add("bookCache", books, DateTimeOffset.Now.AddSeconds(20));
            }
            return books;
        }
        public ActionResult Index()
        {
            var allBooks = GetAll();
            return View(allBooks);
        }
    }
}