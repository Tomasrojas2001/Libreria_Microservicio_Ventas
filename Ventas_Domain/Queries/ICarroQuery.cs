﻿using System;
using System.Collections.Generic;
using System.Text;
using Ventas_Domain.DTOs;
using Ventas_Domain.DTOs.CarroDtos;
using Ventas_Domain.Entities;

namespace Ventas_Domain.Queries
{
    public interface ICarroQuery
    {
       ResponseCarroCompleto GetCarroCompletoQuery(int Usuarioid);
        bool VerificarCarroActivo(int usuarioId);
        void UpdateCarroActivoQuery(int usuarioId);
    }
}