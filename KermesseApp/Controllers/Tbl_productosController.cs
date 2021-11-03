using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KermesseApp.Models;

namespace KermesseApp.Controllers
{
    public class Tbl_productosController : Controller
    {
        private KERMESSEEntities db = new KERMESSEEntities();

        // GET: Tbl_producto
        public ActionResult Index()
        { 
            return View();
        }
        public ActionResult VwGuardarProd() //Invokes List View
        {
            return View(); //Returns New Prod Cat View
        }

        [HttpPost]
        public ActionResult GuardarProd(tbl_productos tp) //Method that saves new item
        {

            if (ModelState.IsValid)
            {
                tbl_productos tProd = new tbl_productos();
                tProd.nombre = tp.nombre;
                tProd.desc_presentacion = tp.desc_presentacion;
                tProd.cantidad = tp.cantidad;
                tProd.precio_venta = tp.precio_venta;
                tProd.estado = 1;
                db.tbl_productos.Add(tProd); //Add new item
                db.SaveChanges(); //Saves to model registry 
            }

            ModelState.Clear();
            return View("tbl_catProducto");

        }
    }
}