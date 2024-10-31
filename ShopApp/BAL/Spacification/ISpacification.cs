using DAL.Entities;
using System.Linq.Expressions;

namespace BLL.Spacification
{
    public interface ISpacification<TEntity> where TEntity : BaseEntity
    {
        //Where Condition signature
        Expression<Func<TEntity, bool>> Predicate { get; set; }

        //List of Includes Signature
        List<Expression<Func<TEntity, object>>> Includes { get; set; }
    }
}
