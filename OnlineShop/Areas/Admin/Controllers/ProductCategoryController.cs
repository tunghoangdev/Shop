
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using Model.EF;
using PagedList;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductCategoryDao();
            var model = dao.ListAll(searchString, page, pageSize);

            ViewBag.SearchString = searchString;

            return View(model);
        }
    }
}