﻿using ProyectoWeb_Jueves.Entidades;

namespace ProyectoWeb_Jueves.Services
{
    public interface IProductoModel
    {
        ProductoRespuesta? ConsultarProductos();
    }
}