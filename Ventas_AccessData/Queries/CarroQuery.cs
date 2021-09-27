﻿using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Ventas_Domain.DTOs;
using Ventas_Domain.DTOs.CarroDtos;
using Ventas_Domain.DTOs.CarroLibroDtos;
using Ventas_Domain.DTOs.VentasDtos;
using Ventas_Domain.Entities;
using Ventas_Domain.Queries;

namespace Ventas_AccessData.Queries
{
    public class CarroQuery : ICarroQuery
    {
        private readonly IDbConnection connection;
        private readonly Compiler sklKataCompiler;
        private readonly VentasContext context;
        public CarroQuery(IDbConnection xconnection, Compiler xsklKataCompiler, VentasContext dbContext)
        {
            connection = xconnection;
            sklKataCompiler = xsklKataCompiler;
            context = dbContext;
        }

        public ResponseCarroCompleto GetCarroCompletoQuery(int Usuarioid)
        {
            var db = new QueryFactory(connection, sklKataCompiler);

            var Carro = db.Query("Carro")
                        .Select("Id", "Valor", "Activo", "UsuarioId")
                        .Where("UsuarioId", "=", Usuarioid).FirstOrDefault<Carro>();

            var ListaVentas = db.Query("Ventas")
                .Select("Fecha", "Comprobante", "estado", "CarroId")
                .Where("CarroId", "=", Carro.Id).Get<RequestVenta>().ToList();

            var ListaCarroLibro = db.Query("CarroLibro").Select("Id", "CarroId", "LibroId").Where("CarroId", "=", Carro.Id).Get<GetCarroLibroByCarroId>().ToList();

            return new ResponseCarroCompleto
            {
                Id = Carro.Id,
                Valor = Carro.Valor,
                Activo = Carro.Activo,
                Usuarioid = Carro.Usuarioid,
                Ventas = ListaVentas,
                CarroLibros = ListaCarroLibro
            };
        }

    

        public bool VerificarCarroActivo(int UsuarioId)
        {
           
            var Carro = context.Carro.Where(w => w.Activo == true).Where(x => x.Usuarioid == UsuarioId).Select(c => c).FirstOrDefault();
            

            if (Carro != null)
            {                
                return true;
            }
            else
            {
                return false;
            }
        }

        public void UpdateCarroActivoQuery(int usuarioId)
        {
            Carro Carro = context.Carro.Where(c => c.Usuarioid == usuarioId).Where(c => c.Activo == true).FirstOrDefault();
            Carro.Activo = false;
            context.Update(Carro);
            context.SaveChanges();
        }
    }
}