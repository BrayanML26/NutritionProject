using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;
using System.Web.Security;

namespace NutritionProject.Controllers
{
    public class AccessController : Controller
    {
        // GET: Access
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuarios obj)
        {
            int resultado;
            string mensaje = string.Empty;

            ViewData["Name_"] = string.IsNullOrEmpty(obj.Name_) ? "" : obj.Name_;
            ViewData["Email"] = string.IsNullOrEmpty(obj.Email) ? "" : obj.Email;
            ViewData["Username"] = string.IsNullOrEmpty(obj.Username) ? "" : obj.Username;

            resultado = new CN_Usuarios().Registrar(obj, out mensaje);

            if(resultado > 0)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Access");
            }
            else
            {
                ViewBag.Error = mensaje;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Index(string username, string password)
        {
            Usuarios oUsuario = null;

            oUsuario = new CN_Usuarios().Listar().Where(item => item.Username == username && item.Password_ == password).FirstOrDefault();

            if (oUsuario == null)
            {
                ViewBag.Error = "Username/Password incorrectos";
                return View();
            }
            else
            {
                FormsAuthentication.SetAuthCookie(oUsuario.Username, false);

                Session["Usuario"] = oUsuario;

                ViewBag.Error = null;
                return RedirectToAction("Index", "Recipe");
            }
        }

        public ActionResult CerrarSesion()
        {
            Session["Usuario"] = null;

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Access");
        }
    }
}