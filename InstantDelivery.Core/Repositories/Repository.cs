using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Validation;
using System.Linq;

namespace InstantDelivery.Core.Repositories
{
    public partial class Repository<T> where T : EntityObject
    {
        private readonly InstantDeliveryContext context;
        private IDbSet<T> entities;
        private string errorMessage = string.Empty;   
    }

    public partial class Repository<T> where T : EntityObject
    {
        protected virtual IQueryable<T> Table => this.Entities;
        protected IDbSet<T> Entities => entities ?? (entities = context.Set<T>());
        public int Total => entities.Count();

        public Repository()
        {
            this.context = new InstantDeliveryContext();
            int a = 0;
        }

        public IList<T> GetAll()
        {
            return entities.ToList();
        }

        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        public IList<T> Page(int pageNumber, int pageSize)
        {
            return Entities.OrderBy(e => e.EntityKey)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize).ToList();
        }

        public void Reload(T entity)
        {
            context.Entry(entity).Reload();
        }

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                this.Entities.Add(entity);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (
                    var validationError in
                        dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    errorMessage += $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}" +
                                    Environment.NewLine;
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (
                    var validationError in
                        dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    errorMessage += Environment.NewLine +
                                    $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                }

                throw new Exception(errorMessage, dbEx);
            }
        }

        public void Remove(T entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                this.Entities.Remove(entity);
                this.context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (
                    var validationError in
                        dbEx.EntityValidationErrors.SelectMany(validationErrors => validationErrors.ValidationErrors))
                {
                    errorMessage += Environment.NewLine +
                                    $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}";
                }
                throw new Exception(errorMessage, dbEx);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}