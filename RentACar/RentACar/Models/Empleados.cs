//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RentACar.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Empleados
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public int TandaLabor { get; set; }
        public decimal PorcientoComision { get; set; }
        public System.DateTime FechaIngreso { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public bool Estado { get; set; }
        public bool Deleted { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}