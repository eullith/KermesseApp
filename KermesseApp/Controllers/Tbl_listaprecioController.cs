using KermesseApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace KermesseApp.Controllers
{
    public class Tbl_listaprecioController : Controller
    {
        private KERMESSEEntities db = new KERMESSEEntities();

        // GET: Tbl_listaprecio

        //View tables methods
        public ActionResult tbl_listaprecio()
        {
            return View(db.tbl_listaprecio.Where(model => model.estado != 3));
        }

        public ActionResult tbl_listaprecio_det(int id)
        {
            var detalles = db.tbl_listaprecio_det.Where(model => model.id_listaprecio_det == id);
            return View(detalles);
        }
       
        //View Add Item to Table Page
        public ActionResult VwGuardarListaPrecio()
        {
            ViewBag.id_kermesse = new SelectList(db.tbl_kermesse.Where(model => model.estado != 3), "id_kermesse", "nombre");
            ViewBag.id_producto = new SelectList(db.tbl_productos.Where(model => model.estado != 3), "id_productos", "nombre");
            return View();
        }

        //Save data into Lista Precio table Method
        [HttpPost]
        public ActionResult GuardarListaPrecio(tbl_listaprecio lp)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    tbl_listaprecio lPrec = new tbl_listaprecio();
                    lPrec.id_kermesse = lp.id_kermesse;
                    lPrec.nombre = lp.nombre;
                    lPrec.descripcion = lp.descripcion;
                    lPrec.estado = lp.estado;
                    db.tbl_listaprecio.Add(lPrec);
                    db.SaveChanges();
                    foreach(var lpd in lp.listaprecio_det)
                    {
                        tbl_listaprecio_det lpDet = new tbl_listaprecio_det();
                        lpDet.id_producto = lpd.id_producto;
                        lpDet.precio_venta = lpd.precio_venta;
                        lpDet.id_listaprecio = lpd.id_listaprecio;
                        db.tbl_listaprecio_det.Add(lpDet);
                    }
                    db.SaveChanges();
                }
                ModelState.Clear();
                return RedirectToAction("tbl_Listaprecio");
            }
            catch(Exception)
            {
                return View("VwGuardarListaPrecio");
            }
        }

        //Save data into Lista Precio table Method
        [HttpPost]
        public ActionResult GuardarListaPrecDetalle(tbl_listaprecio lp)
        {
            int idList = lp.id_listaprecio;

            try
            {
                tbl_listaprecio lPrec = new tbl_listaprecio();
                lPrec.id_listaprecio = lp.id_listaprecio;
                foreach (var lpd in lp.listaprecio_det)
                {
                    tbl_listaprecio_det lpDet = new tbl_listaprecio_det();
                    lpDet.id_producto = lpd.id_producto;
                    lpDet.precio_venta = lpd.precio_venta;
                    lpDet.id_listaprecio = lpd.id_listaprecio;
                    db.tbl_listaprecio_det.Add(lpDet);
                }
                db.SaveChanges();

                ModelState.Clear();
                return RedirectToAction("tbl_listaprecio_det", "Tbl_listaprecio", new { id = idList });
            }
            catch(Exception)
            {
                return RedirectToAction("tbl_listaprecio_det", "Tbl_listaprecio", new { id = idList });
                throw;
            }
        }

        //Edit Methods
        public ActionResult EditarListaPrecio(int id)
        {
            tbl_listaprecio lp = db.tbl_listaprecio.Find(id);
            if(lp == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.id_kermesse = new SelectList(db.tbl_kermesse.Where(model => model.estado != 3), "id_kermesse", "nombre");
                return View(lp);
            }
        }

        public ActionResult EditarListaPrecioDet(int id)
        {
            tbl_listaprecio_det lpd = db.tbl_listaprecio_det.Find(id);
            if (lpd == null)
            {
                return HttpNotFound();
            }
            else
            {
                ViewBag.id_listaprecio = new SelectList(db.tbl_listaprecio.Where(model => model.estado != 3), "id_listaprecio", "nombre");
                ViewBag.id_producto = new SelectList(db.tbl_productos.Where(model => model.estado != 3), "id_producto", "nombre");

                return View(lpd);
            }
        }

        [HttpPost]
        public ActionResult ActualizarListaPrecio(tbl_listaprecio lp)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    lp.estado = 2;
                    db.Entry(lp).State = EntityState.Modified;
                    db.SaveChanges();
                }

                ViewBag.id_kermesse = new SelectList(db.tbl_kermesse.Where(model => model.estado != 3), "id_kermesse", "nombre");
                return RedirectToAction("tbl_Listaprecio");
            }
            catch (Exception)
            {
                return RedirectToAction("tbl_Listaprecio");
                throw;
            }
        }

        [HttpPost]
        public ActionResult ActualizarListaPrecioDet(tbl_listaprecio_det lpd)
        {
            int idList = lpd.id_listaprecio;

            try
            {
                if(ModelState.IsValid)
                {
                    db.Entry(lpd).State = EntityState.Modified;
                    db.SaveChanges();
                }
                ViewBag.id_producto = new SelectList(db.tbl_productos.Where(model => model.estado != 3), "id_producto", "nombre");
                return RedirectToAction("tbl_listaprecio_det", "Tbl_listaprecio", new { id = idList });
            }
            catch(Exception)
            {
                return RedirectToAction("tbl_listaprecio_det", "Tbl_listaprecio", new { id = idList });
                throw;
            }
        }

        //Delete Methods
        public ActionResult EliminarListaPrecio(int id)
        {
            tbl_listaprecio lp = new tbl_listaprecio();
            lp = db.tbl_listaprecio.Find(id);
            this.EliminarLPrecLogic(lp);
            return RedirectToAction("tbl_Listaprecio");
        }

        [HttpPost]
        public ActionResult EliminarLPrecLogic(tbl_listaprecio lp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    lp.estado = 3;
                    db.Entry(lp).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction("tbl_Listaprecio");
            }
            catch (Exception)
            {
                return View();
                throw;
            }
        }

        public ActionResult EliminarListaPrecioDet(int id)
        {
            tbl_listaprecio_det lpd = new tbl_listaprecio_det();
            lpd = db.tbl_listaprecio_det.Find(id);
            int idListaprecio = lpd.id_listaprecio;
            db.tbl_listaprecio_det.Remove(lpd);
            db.SaveChanges();

            var list = db.tbl_listaprecio_det.Where(model => model.id_listaprecio == idListaprecio);
            return View("tbl_Listaprecio", list);
        }

        //Filter Method
        public ActionResult FiltrarListaPrecio(String cadena)
        {
            if (String.IsNullOrEmpty(cadena))
            {
                var list = db.tbl_listaprecio.Where(model => model.estado != 3);
                return View("tbl_Listaprecio", list);  //Retorna la vista de lista precio
            }
            else
            {
                var listaFiltrada = db.tbl_listaprecio.Where(x => x.nombre.Contains(cadena) || x.descripcion.Contains(cadena));
                return View("tbl_Listaprecio", listaFiltrada);  //Retorna la vista filtrada de lista precio
            }
        }

        public ActionResult FiltrarListaPrecioDet(String cadena, int id)
        {
            if (String.IsNullOrEmpty(cadena))
            {
                var list = db.tbl_listaprecio_det.Where(model => model.id_listaprecio == id);
                return View("tbl_Listaprecio_Det", list);
            }
            else
            {
                var listaFiltrada = db.tbl_listaprecio_det.Where(model => model.tbl_productos.nombre.Contains(cadena) && model.id_listaprecio == id);
                return View("tbl_Listaprecio_Det", listaFiltrada);
            }
        }
    }
}