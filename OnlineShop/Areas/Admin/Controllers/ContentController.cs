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
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ContentDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new ContentDao();
            var content = dao.GetByID(id);
            SetViewBag(content.CategoryID);
            return View(content);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Content content)
        {
            var dao = new ContentDao();
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                content.ModifiedBy = session.UserName;
                var culture = Session[CommonConstants.CurrentCulture];
                content.Language = culture.ToString();
                var result = dao.Edit(content);
                if (result)
                {
                    SetAlert("Cập nhật tin tức thành công!", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật tin tức không thành công!");
                }
                return RedirectToAction("Index");
            }
            SetViewBag(content.CategoryID);
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Content model)
        {
            if (ModelState.IsValid)
            {
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                model.CreatedBy = session.UserName;
                var culture = Session[CommonConstants.CurrentCulture];
                model.Language = culture.ToString();
                new ContentDao().Create(model);
                return RedirectToAction("Index");
            }
            SetViewBag();
            return View();
        }
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ContentDao().Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult ChangeStatus(long id)
        {
            var result = new ContentDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }
    }
}