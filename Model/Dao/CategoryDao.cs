using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Common;

namespace Model.Dao
{
    public class CategoryDao
    {
        public static string USER_SESSION = "USER_SESSION";
        OnlineShopDbContext db = null;
        public CategoryDao()
        {
            db = new OnlineShopDbContext();
        }
        public IEnumerable<Category> ListAllPaging(string searchString, int page , int pageSize)
        {
            IEnumerable<Category> model = db.Categories;
            if (!string.IsNullOrEmpty(searchString)) {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public IEnumerable<Category> ListAllPaging(int page, int pageSize)
        {
            IQueryable<Category> model = db.Categories;
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        // Them moi
        public long Insert(Category category)
        {
            //Xử lý alias
            if (string.IsNullOrEmpty(category.MetaTitle))
            {
                category.MetaTitle = StringHelper.ToUnsignString(category.Name);
            }
            category.CreatedDate = DateTime.Now;
            category.ParentID = category.ParentID;
            db.Categories.Add(category);
            db.SaveChanges();
            return category.ID;
        }

        // thay doi trang thaithe loai
        public bool ChangeStatus(long id)
        {
            var cat = db.Categories.Find(id);
            cat.Status = !cat.Status;
            db.SaveChanges();
            return cat.Status;
        }
        // xoa the loai
        public bool Delete(int id)
        {
            try
            {
                var cat = db.Categories.Find(id);
                db.Categories.Remove(cat);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }

        public bool Edit(Category cat)
        {
            try
            {
                var cate = db.Categories.Find(cat.ID);
                cate.Name = cat.Name;
                if (string.IsNullOrEmpty(cate.MetaTitle))
                {
                    cate.MetaTitle = StringHelper.ToUnsignString(cate.Name);
                }
                else {
                    cate.MetaTitle = cat.MetaTitle;
                }
                cate.MetaDescriptions = cat.MetaDescriptions;
                cate.MetaKeywords = cat.MetaKeywords;
                cate.ParentID = cat.ParentID;
                cate.ModifiedDate = DateTime.Now;
                cate.Status = cat.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
        public Category GetByID(long id)
        {
            return db.Categories.Find(id);
        }

    }
}
