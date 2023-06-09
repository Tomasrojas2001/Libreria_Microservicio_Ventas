﻿
using Ventas_Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ventas_AccessData;

namespace Ventas_AccessData.Commands
{
    public class GenericRepository : IGenericRepository
    {
        private readonly VentasContext _context;
        public GenericRepository(VentasContext context)
        {
            this._context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Update<T>(int id, T NewEntity) where T : class
        {
            var entity = _context.Find<T>(id);

            _context.Entry(entity).CurrentValues.SetValues(NewEntity);
            _context.SaveChanges();
        }

        public void Delete<T>(int _id) where T : class
        {
            _context.Remove(_context.Find<T>(_id));
            _context.SaveChanges();
        }

    }
}
