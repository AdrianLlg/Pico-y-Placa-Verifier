using prjPicoyPlaca.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjPicoyPlaca.Controllers
{
    public class PicoyPlacaController : Controller
    {
        // GET: PicoyPlaca
        public ActionResult PicoyPlacaVerifier()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PicoyPlacaVerifier(InputsPicoyPlaca inputsPicoYPlaca)
        {
            try
            {
                if (InputsVerifier(inputsPicoYPlaca.plateNumber, inputsPicoYPlaca.date))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Please, provide a valid Date.";
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }


        public bool InputsVerifier(string plateNumber, string date)
        {
            try
            {
                DateTime dayPicked = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime today = DateTime.Today;

                if (dayPicked.Date >= today)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}