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

        public long Insert(Category category)
        {
            //Xử lý alias
            if (string.IsNullOrEmpty(category.MetaTitle))
            {
                category.MetaTitle = StringHelper.ToUnsignString(category.Name);
            }
            category.CreatedDate = DateTime.Now;
            category.CreatedBy = USER_SESSION;
            db.Categories.Add(category);
            db.SaveChanges();
            return category.ID;
        }
        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
    }
}
