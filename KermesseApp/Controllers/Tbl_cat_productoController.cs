using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KermesseApp.Models;
using Microsoft.Reporting.WebForms;

namespace KermesseApp.Controllers
{
    public class Tbl_cat_productoController : Controller
    {
        private KERMESSEEntities db = new KERMESSEEntities();

        // GET: Tbl_cat_producto
        public ActionResult tbl_catProducto() //Invokes List View
        {
            return View(db.tbl_cat_producto.ToList());
        }

        public ActionResult VwGuardarCatProd() //Invokes List View
        {
            return View(); //Returns New Prod Cat View
        }

        [HttpPost]
        public ActionResult GuardarCatProd(tbl_cat_producto tcp) //Method that saves new item
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tbl_cat_producto tcProd = new tbl_cat_producto();
                    tcProd.nombre = tcp.nombre;
                    tcProd.descripcion = tcp.descripcion;
                    tcProd.estado = 1;
                    db.tbl_cat_producto.Add(tcProd); //Add new item
                    db.SaveChanges(); //Saves to model registry 
                }
                ModelState.Clear();
                return RedirectToAction("tbl_catProducto");
            }
            catch
            {
               return RedirectToAction("tbl_catProducto");
            }
           
        }

        public ActionResult EliminarCatProd(int id)
        {
            tbl_cat_producto tcp = new tbl_cat_producto();
            tcp = db.tbl_cat_producto.Find(id);
            db.tbl_cat_producto.Remove(tcp);

            db.SaveChanges();
            var list = db.tbl_cat_producto.ToList();

            return View("tbl_catProducto", list);
        }

        public ActionResult VerCatProd(int id)
        {
            var catProd = db.tbl_cat_producto.Where(x => x.id_cat_producto == id).First();
            return View(catProd);
        }

        public ActionResult EditarCatProd(int? id)
        {
            tbl_cat_producto tcp = db.tbl_cat_producto.Find(id);
            if (tcp == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(tcp);
            }
        }

        [HttpPost]
        public ActionResult ActualizarCatProd(tbl_cat_producto tcp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tcp).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("tbl_catProducto");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        
        public ActionResult FiltrarCatProd(String cadena)
        {
            if(String.IsNullOrEmpty(cadena))
            {
                var list = db.tbl_cat_producto.ToList();
                return View("tbl_catProducto", list);
            }
            else
            {
                var listaFiltrada = db.tbl_cat_producto.Where(x => x.nombre.Contains(cadena) || x.descripcion.Contains(cadena));
                return View("tbl_catProducto", listaFiltrada);
            }
        }

        public ActionResult VerRptCatProd(String tipo, String cadena)
        {
            LocalReport rpt = new LocalReport();
            string mt, enc, f;
            string[] s;
            Warning[] w;

            //cadena = formCollection.GetValue("cadena").AttemptedValue;

            string ruta = Path.Combine(Server.MapPath("~/Reportes"), "rptCatProducto.rdlc");
            rpt.ReportPath = ruta;

            var listaFiltrada = from a in db.tbl_cat_producto select a; //method to gather all db info

            if(!string.IsNullOrEmpty(cadena))
            {
                //List<tbl_productos> listaFiltrada = new List<tbl_productos>();
                listaFiltrada = listaFiltrada.Where(x => x.nombre.Contains(cadena) || x.descripcion.Contains(cadena));
            }

            ReportDataSource rd = new ReportDataSource("dsCatProducto", listaFiltrada);
            rpt.DataSources.Add(rd);
            //tipo = "PDF";
            var b = rpt.Render(tipo, null, out mt, out enc, out f, out s, out w);
            return new FileContentResult(b, mt);
        }
    }
}