﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReservaHotel.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class cti_reservacionEntities : DbContext
    {
        public cti_reservacionEntities()
            : base("name=cti_reservacionEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<cliente> cliente { get; set; }
        public virtual DbSet<habitacion> habitacion { get; set; }
        public virtual DbSet<pago> pago { get; set; }
        public virtual DbSet<reservacion> reservacion { get; set; }
        public virtual DbSet<recibo> recibo { get; set; }
    }
}
