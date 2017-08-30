using CoreTemplate.Accessors.Models.EF.Base;

namespace CoreTemplate.Accessors.Accessors.Base
{
    public class EntityAccessor<TEntity> where TEntity : Entity
    {
        protected readonly ApplicationDbContext _db;

        public EntityAccessor(ApplicationDbContext db)
        {
            _db = db;
        }
    }
}
