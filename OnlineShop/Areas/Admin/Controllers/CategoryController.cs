using Model.Dao;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CategoryDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }
        // tao moi
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            SetViewBag();
            if (ModelState.IsValid)
            {
                var currentCulture = Session[CommonConstants.CurrentCulture];
                model.Language = currentCulture.ToString();
                 var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreatedBy = session.UserName;
                var id = new CategoryDao().Insert(model);
                if (id > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", StaticResources.Resources.InsertCategoryFailed);
                }
            }
            return View(model);
        }
        // Chinh sua
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new CategoryDao();
            var cat = dao.GetByID(id);
            SetViewBag(cat.ParentID);
            return View(cat);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Category cat)
        {
            var dao = new CategoryDao();
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                cat.ModifiedBy = session.UserName;
                var culture = Session[CommonConstants.CurrentCulture];
                cat.Language = culture.ToString();
                var result = dao.Edit(cat);
                if (result)
                {
                    SetAlert("Cập nhật thể loại thành công!", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thể loại không thành công!");
                }
                return RedirectToAction("Index");
            }
            SetViewBag(cat.ParentID);
            return View();
        }
        // Phuong thuc xoa the loai
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new CategoryDao().Delete(id);
            return RedirectToAction("Index");
        }
        // Thay doi trang thai
        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new CategoryDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDao();
            ViewBag.ParentID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
    }
}