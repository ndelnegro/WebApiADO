﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoModelo
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        public string? NombreCompleto {  get; set; }
        public Departamento? Departamento { get; set; } 
        public decimal sueldo { get; set; } 
        public string? FechaContrato {  get; set; } 

    }
}
