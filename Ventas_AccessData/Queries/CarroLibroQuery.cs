﻿using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ventas_Domain.Queries;

namespace Ventas_AccessData.Queries
{
    public class CarroLibroQuery : ICarroLibroQuery
    {
        private readonly IDbConnection connection;
        private readonly Compiler sklKataCompiler;
        private readonly VentasContext context;
        public CarroLibroQuery(IDbConnection xconnection, Compiler xsklKataCompiler, VentasContext dbContext)
        {
            connection = xconnection;
            sklKataCompiler = xsklKataCompiler;
            context = dbContext;
        }
    }
}
