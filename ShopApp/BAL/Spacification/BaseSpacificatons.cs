using DAL.Entities;
using System.Linq.Expressions;

namespace BLL.Spacification
{
    public class BaseSpacificatons<TEntity> : ISpacification<TEntity> where TEntity : BaseEntity
    {
        public Expression<Func<TEntity, bool>> Predicate { get; set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; set; }

        //will Use This Constructor If There Is no Criteria (meaning With Get All)
        public BaseSpacificatons()
        {
            //declare Reference From Includes when Call this Class
            Includes = new List<Expression<Func<TEntity, object>>>();
        }


        //Will Use this Ctor IF There are Criteria Existing Ex : Get by Id=>Criteria will Sent at Ctor then Set at Reference 
        public BaseSpacificatons(Expression<Func<TEntity, bool>> criteria)
        {
            Predicate = criteria;
            Includes = new List<Expression<Func<TEntity, object>>>();
        }
    }

}
