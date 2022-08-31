namespace PTW.Domain.Storage.Common.Repositories
{
    using PTW.Domain.Storage.Common.Context;
    using PTW.Domain.Storage.Common.Entities;
    using Microsoft.EntityFrameworkCore;

    public class Repository<TEntity> where TEntity : Entity
    {
        protected readonly IPTWDbContext _ptwDbContext;

        protected Repository(IPTWDbContext ptwDbContext)
        {
            _ptwDbContext = ptwDbContext;
        }

        protected IQueryable<TEntity> AllNoTrackedOf<TEntity>() where TEntity : Entity
        {
            return _ptwDbContext.Set<TEntity>().Where(x => x.DeletedOn == null).AsNoTracking();
        }

        public void Insert(TEntity entity)
        {
            _ptwDbContext.Set<TEntity>().Add(entity);
        }

        public void InsertMany(IEnumerable<TEntity> entities)
        {
            _ptwDbContext.Set<TEntity>().AddRange(entities);
        }

        public void Update(TEntity existingEntity, TEntity entity)
        {
            _ptwDbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
        }

        public void SaveChanges()
        {
            _ptwDbContext.SaveChanges();
        }
    }
}
