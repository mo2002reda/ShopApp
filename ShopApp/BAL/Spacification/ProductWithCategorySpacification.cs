using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Spacification
{
    public class ProductWithCategorySpacification : BaseSpacificatons<Product>
    {
        //To Get All Products 
        public ProductWithCategorySpacification()
        {
            Includes.Add(x => x.category);
        }

        //To Get Product by Id With Include Category
        public ProductWithCategorySpacification(int id) : base(x => x.Id == id)
        {
            Includes.Add(x => x.category);
        }
    }
}
