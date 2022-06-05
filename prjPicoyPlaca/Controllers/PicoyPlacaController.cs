﻿using prjPicoyPlaca.Models;
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
                    return View();
                }
                else
                {
                    ViewBag.Message = "Please, provide a valid Plate Number or Date.";
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
            //Try catch Error Handler
            try
            {
                DateTime dayPicked = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime today = DateTime.Today;

                //Validation of invalid Plate Number. If the plate number is invalid, it won't validate anything else.
                if (plateNumber.Equals("0000"))
                {
                    return false;
                }

                //Validation of invalid Date (Dates above today's date)
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