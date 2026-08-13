using DigitalForms.DL.Data;
using DigitalForms.BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using DigitalForms.BL.Models;
using DigitalForms.BL.Data;
using EFCore.BulkExtensions;


namespace DigitalForms.DL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DFSqlContext _context;
        protected readonly s255d01dbDfSharedContext _readcontext;
        public GenericRepository(DFSqlContext context, s255d01dbDfSharedContext readcontext)
        {
            _context = context;
            _readcontext = readcontext;
        }
        public void Add(T entity)
        {           
            _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {           
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);
        }
        public void UpdateRange(IEnumerable<T> entities)
        {

            if (entities.Any())
            {
                entities.ToList().ForEach(e =>
                {
                    _context.Entry(e).State = EntityState.Modified;
                });
                _context.Set<T>().UpdateRange(entities);
            }
        }
        public void AddRange(IEnumerable<T> entities)
        {
            if (entities.Any())
            {
                Type proptype = typeof(T);
                //if (proptype.Name == "Condition")
                //{
                //    _context.BulkInsert(entities, a => a.IncludeGraph = true);
                //    _context.BulkSaveChangesAsync().Wait();
                //}
                //else
                _context.Set<T>().AddRange(entities);
            }
        }
        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
        public IQueryable<T> FindAsync(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }
        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public void Remove(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _context.Set<T>().Remove(entity);            
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            if (entities.Any())
            {
                //bool bulkchilddeletion = false;
                entities.ToList().ForEach(e =>
                {
                    Type proptype = typeof(T);

                    //if (proptype.Name == "Condition") 
                    //{
                    //    T tempdata = (T)(object)e;
                    //    Condition data = (Condition)(object)tempdata;
                    //    ICollection<ConditionDetail> childobj = data.ConditionDetails;

                    //    if (childobj.Any())
                    //    {
                    //        _context.BulkDelete<ConditionDetail>(childobj);
                    //        bulkchilddeletion = true;
                    //    }
                    //}
                    //else
                    //{
                        _context.Entry(e).State = EntityState.Deleted;
                    //}

                });
                //if (bulkchilddeletion)
                //{
                //    _context.BulkDelete<T>(entities);
                //    _context.BulkSaveChangesAsync().Wait();
                //}
                //else
                //{
                    _context.RemoveRange(entities);
                //}
            }
        }
        public void Detach(T entity)
        {
            var entry = _context.Entry(entity);
            if (entry != null)
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}
