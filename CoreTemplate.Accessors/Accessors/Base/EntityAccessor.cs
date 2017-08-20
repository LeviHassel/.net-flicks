using CoreTemplate.Accessors.Models.EF.Base;

namespace CoreTemplate.Accessors.Accessors.Base
{
  public class EntityAccessor<TEntity> where TEntity : Entity
  {
    protected readonly CoreTemplateContext _db;

    public EntityAccessor(CoreTemplateContext db)
    {
      _db = db;
    }
  }
}
