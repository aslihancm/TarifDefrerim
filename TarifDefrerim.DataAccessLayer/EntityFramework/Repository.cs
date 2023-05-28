﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TarifDefrerim.DataAccessLayer.Abstract;
using TarifDefrerim.Entity;

namespace TarifDefrerim.DataAccessLayer.EntityFramework
{
    public class Repository<T> :RepositoryBase, IRepository<T> where T:class
    {
        //private DatabaseContext db = new DatabaseContext(); //Singleton Pattern

        private DatabaseContext db;
        private DbSet<T> _objectSet;
        public Repository()
        {
            db=RepositoryBase.CreateContext();
           // db.Categories.Find(1); //i=>i.Id==1
            _objectSet = db.Set<T>();
        }

        //Okuma işlemi 3 farklı yolla yapılıyor
        //1.Bütün liste
        //2.HErhangi bir yapıya göre listele
        //Tek bir tane alanı bul
        public List<T> List()
        {
            return _objectSet.ToList();
        }
        //public List<T> List(Exception<Func<T,bool>> filter)
        //{
        //    return _objectSet.Where(filter).ToList();
        //}
        public T Find(Exception<Func<T, bool>> filter)
        {
            return _objectSet.Find(filter);
            //return _objectSet.FirstOrDefault(filter); diyebiliriz.
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);
            return Save();
        }
        public int Update(T obj)
        {
        
            return Save();
        }
        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }
        public int Save()
        {

            return db.SaveChanges();
        }

        public List<T> List(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public T Find(Expression<Func<T, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}