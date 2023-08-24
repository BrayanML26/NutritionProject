using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaDatos;
using CapaEntidad;
using CapaNegocio;

namespace NutritionProject.Controllers
{
    public class RecipeController : Controller
    {
        // GET: Recipe
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DetalleReceta(int idreceta = 0)
        {
            Recetas oReceta = new Recetas();

            oReceta = new CN_Recetas().Listar().Where(r => r.Recipe_Id == idreceta).FirstOrDefault();

            if(oReceta != null)
            {
                oReceta.Image_ = oReceta.Image_;
            }
            return View(oReceta);
        }


        #region RECETAS
        [HttpGet]
        public JsonResult ListarRecetas()
        {
            List<Recetas> oLista = new List<Recetas>();

            oLista = new CN_Recetas().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarRecetas(Recetas obj)
        {
            object resultado;
            string mensaje = string.Empty;

            if (obj.Recipe_Id == 0)
            {
                resultado = new CN_Recetas().Registrar(obj, out mensaje);
            }
            else
            {
                resultado = ("HAHAHA");//new CN_Recetas().Modificar(obj, out mensaje);
            }
            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region TIPO DIETA
        [HttpGet]
        public JsonResult ListarTipoDieta()
        {
            List<TipoDieta> oLista = new List<TipoDieta>();

            oLista = new CN_TipoDieta().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region LISTAR ALIMENTOS POR DIETA
        [HttpPost]
        public JsonResult ListarAlimentosPorDieta(int idtipodieta)
        {
            List<Alimentos> oLista = new List<Alimentos>();

            oLista = new CN_Alimentos().ListarAlimentosPorDieta(idtipodieta);

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region LISTAR RECETA 
        [HttpPost]
        public JsonResult ListarReceta(int idtipodieta, int idalimento, string busqueda)
        {
            List<Recetas> oLista = new List<Recetas>();

            oLista = new CN_Recetas().Listar().Select(r => new Recetas()
            {
                Recipe_Id = r.Recipe_Id,
                oUserId = r.oUserId,
                Name_ = r.Name_,
                Description_ = r.Description_,
                oDiet_Type_Id = r.oDiet_Type_Id,
                Time_Preparation = r.Time_Preparation,
                Servings = r.Servings,
                Image_ = r.Image_,
                oFood_Id = r.oFood_Id,
                Date_ = r.Date_,
                Steps = r.Steps,
                Ingredients = r.Ingredients
                //Datos = r.Datos
            }).Where(r =>
                r.oDiet_Type_Id.Diet_Type_Id == (idtipodieta == 0 ? r.oDiet_Type_Id.Diet_Type_Id : idtipodieta) &&
                r.oFood_Id.Food_Id == (idalimento == 0 ? r.oFood_Id.Food_Id : idalimento) &&
                (string.IsNullOrEmpty(busqueda) ||
                r.Name_.Contains(busqueda) ||
                r.Ingredients.Contains(busqueda))
            ).ToList();

            var jsonresult = Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
            jsonresult.MaxJsonLength = int.MaxValue;

            return jsonresult;

        }
        #endregion

        #region CATEGORIA ALIMENTOS
        [HttpGet]
        public JsonResult ListarCategoriaAlimentos()
        {
            List<CategoriaAlimentos> oLista = new List<CategoriaAlimentos>();

            oLista = new CN_CategoriaAlimentos().Listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region AGREGAR FAVORITO
        [HttpPost]
        public JsonResult AgregarFavorito(int idreceta)
        {
            int idusuario = ((Usuarios)Session["Usuario"]).UserId;

            bool existe = new CN_Favoritos().ExisteFavorito(idreceta, idusuario);

            bool respuesta = false;

            string mensaje = string.Empty;

            if (existe)
            {
                mensaje = "La receta ya existe en favoritos";
            }
            else
            {
                respuesta = true;
            }

            return Json(new { respuesta = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region CANTIDAD FAVORITO
        [HttpGet]
        public JsonResult CantidadFavorito()
        {
            int idusuario = ((Usuarios)Session["Usuario"]).UserId;

            int cantidad = new CN_Favoritos().CantidadFavorito(idusuario);

            return Json(new { cantidad = cantidad }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        
    }
}