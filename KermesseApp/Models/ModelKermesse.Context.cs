﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KermesseApp.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KERMESSEEntities : DbContext
    {
        public KERMESSEEntities()
            : base("name=KERMESSEEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<tbl_arqueocaja> tbl_arqueocaja { get; set; }
        public virtual DbSet<tbl_arqueocaja_det> tbl_arqueocaja_det { get; set; }
        public virtual DbSet<tbl_bonos> tbl_bonos { get; set; }
        public virtual DbSet<tbl_cat_gastos> tbl_cat_gastos { get; set; }
        public virtual DbSet<tbl_cat_producto> tbl_cat_producto { get; set; }
        public virtual DbSet<tbl_comunidad> tbl_comunidad { get; set; }
        public virtual DbSet<tbl_denominacion> tbl_denominacion { get; set; }
        public virtual DbSet<tbl_gastos> tbl_gastos { get; set; }
        public virtual DbSet<tbl_ingreso_com> tbl_ingreso_com { get; set; }
        public virtual DbSet<tbl_ingresocom_det> tbl_ingresocom_det { get; set; }
        public virtual DbSet<tbl_kermesse> tbl_kermesse { get; set; }
        public virtual DbSet<tbl_listaprecio> tbl_listaprecio { get; set; }
        public virtual DbSet<tbl_listaprecio_det> tbl_listaprecio_det { get; set; }
        public virtual DbSet<tbl_moneda> tbl_moneda { get; set; }
        public virtual DbSet<tbl_opciones> tbl_opciones { get; set; }
        public virtual DbSet<tbl_parroquia> tbl_parroquia { get; set; }
        public virtual DbSet<tbl_productos> tbl_productos { get; set; }
        public virtual DbSet<tbl_rol> tbl_rol { get; set; }
        public virtual DbSet<tbl_rol_opcion> tbl_rol_opcion { get; set; }
        public virtual DbSet<tbl_rol_usuario> tbl_rol_usuario { get; set; }
        public virtual DbSet<tbl_tasacambio> tbl_tasacambio { get; set; }
        public virtual DbSet<tbl_tasacambio_det> tbl_tasacambio_det { get; set; }
        public virtual DbSet<tbl_usuario> tbl_usuario { get; set; }
    }
}
