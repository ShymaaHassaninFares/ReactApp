using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Product.Data.Core;
using Product.Domain.Interface;

namespace Product.Data.Repositories.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly IQueryableUnitOfWork _unitOfWork;

        protected DbSet<TEntity> GetSet()
        {
            return _unitOfWork.CreateSet<TEntity>();
        }

        #region Constructor

        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == null) throw new ArgumentNullException(nameof(unitOfWork));
            _unitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository Members

        public IUnitOfWork UnitOfWork => (IUnitOfWork)_unitOfWork;

        public Task<TEntity> GetAsync(object[] keyValues)
        {
            return keyValues != null ? GetSet().FindAsync(keyValues).AsTask() : null;
        }


        public virtual IQueryable<TEntity> GetAll(bool readOnly = true)
        {
            if (readOnly)
                return GetSet().AsNoTracking();
            return GetSet();
        }

        public virtual void Add(TEntity item)
        {
            GetSet().Add(item);
        }

        public virtual void Delete(TEntity item)
        {
            if (item != null)
            {
                _unitOfWork.Attach(item);

                GetSet().Remove(item);
            }
        }

        public virtual void TrackItem(TEntity item)
        {
            if (item != null)
                _unitOfWork.Attach(item);
        }

        public virtual void Update(TEntity item)
        {
            _unitOfWork.SetModified(item);
        }

        public virtual void Update(TEntity item, string[] includedProperties)
        {
            _unitOfWork.SetModified(item, includedProperties);
        }

        #endregion
    }
}
