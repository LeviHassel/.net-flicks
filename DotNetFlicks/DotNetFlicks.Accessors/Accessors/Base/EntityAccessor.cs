using DotNetFlicks.Accessors.Database;
using DotNetFlicks.Accessors.Models.EF.Base;
using Microsoft.EntityFrameworkCore;

namespace DotNetFlicks.Accessors.Accessors.Base
{
    public abstract class EntityAccessor<TEntity> where TEntity : Entity
    {
        protected readonly DotNetFlicksContext _db;

        public EntityAccessor(DotNetFlicksContext db)
        {
            //Disable global tracking: https://docs.microsoft.com/en-us/ef/core/querying/tracking
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            _db = db;
        }
    }
}
