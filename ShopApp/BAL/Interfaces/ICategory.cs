using DAL.Entities;

namespace BLL.Interfaces
{
    public interface ICategory : IGenericRepository<Category>
    {

        //1)Update Category
        public void UpdateAsync(Category category);


    }
}
