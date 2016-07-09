using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;
namespace Model.Dao
{
    public class ProductCategoryDao
    {
        OnlineShopDbContext db = null;
        public ProductCategoryDao()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<ProductCategory> ListAll(string searchString, int page, int pageSize)
        {
            IQueryable<ProductCategory> model = db.ProductCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }
            return db.ProductCategories.Where(x => x.Status == true).OrderBy(x => x.DisplayOrder).ToPagedList(page, pageSize);
           // return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }

        //public object ListAll()
        //{
        //    throw new NotImplementedException();
        //}
        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.ToList();
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
    }
}
