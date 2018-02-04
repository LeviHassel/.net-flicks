using CoreTemplate.Accessors.Database;
using CoreTemplate.Accessors.Models.EF.Base;
using Microsoft.EntityFrameworkCore;

namespace CoreTemplate.Accessors.Accessors.Base
{
    public abstract class EntityAccessor<TEntity> where TEntity : Entity
    {
        protected readonly CoreTemplateContext _db;

        public EntityAccessor(CoreTemplateContext db)
        {
            //Disable global tracking: https://docs.microsoft.com/en-us/ef/core/querying/tracking
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            _db = db;
        }
    }
}
