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
        public Dictionary<int, string> plateDays = new Dictionary<int, string>() {

            {1, "Monday"}, {2, "Monday"},
            {3, "Tuesday"}, {4, "Tuesday"},
            {5, "Wednesday"}, {6, "Wednesday"},
            {7, "Thursday"}, {8, "Thursday"},
            {9, "Friday"}, {0, "Friday"}
        };

        public TimeSpan restriction_1;
        public TimeSpan restriction_2;
        public TimeSpan restriction_3;
        public TimeSpan restriction_4;

        // GET: PicoyPlaca
        public ActionResult PicoyPlacaVerifier()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PicoyPlacaVerifier(InputsPicoyPlaca inputsPicoYPlaca)
        {
            //Try catch Error Handler
            try
            {
                //Convert string to Datetime the date provided by the user 
                DateTime dayPicked = DateTime.ParseExact(inputsPicoYPlaca.date, "yyyy-MM-dd", CultureInfo.InvariantCulture);

                //Set the Hours of Pico y Placa
                SetHoursPicoyPlaca();

                //Method to verify if the inputs are correct 
                if (InputsValidation(inputsPicoYPlaca.plateNumber, dayPicked))
                {
                    //Retrieve last digit of the Plate provided
                    int numberToVerify = int.Parse(
                                                    inputsPicoYPlaca.plateNumber
                                                    .Substring(inputsPicoYPlaca.plateNumber.Length - 1)
                                                   );
                    //Retrieve the day of the week that the last digit correspond
                    string dayOfWeekPlate = plateDays
                                                .Where(x => x.Key == numberToVerify)
                                                .Select(x => x.Value)
                                                .FirstOrDefault();

                    //Method to validate if the Plate is able to be on road or not.
                    bool response = MainValidationPlate(dayOfWeekPlate, dayPicked, inputsPicoYPlaca.time);

                    if (response)
                    {
                        ViewBag.Message = "You are able to road with the inputs provided.";
                        return View();
                    }
                    else
                    {
                        ViewBag.Message = "You are not able to road with the inputs provided.";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Message = "Please, provide a valid Plate Number or Date.";
                    return View();
                }
            }
            catch
            {
                ViewBag.Message = "Something went wrong. Please, try again later!";
                return View();
            }
        }

        public void SetHoursPicoyPlaca()
        {
            //Hours of restriction expressed on hours, minutes and seconds.
            restriction_1 = new TimeSpan(7, 0, 0);
            restriction_2 = new TimeSpan(9, 30, 0);
            restriction_3 = new TimeSpan(16, 0, 0);
            restriction_4 = new TimeSpan(19, 30, 0);
        }

        public bool InputsValidation(string plateNumber, DateTime dayPicked)
        {
            //Try catch Error Handler
            try
            {
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

        //Main Method to Verify if the car can be on road
        public bool MainValidationPlate(string dayOfWeekPlate, DateTime dateProvided, TimeSpan timeProvided)
        {
            //Try catch Error Handler
            try
            {
                //Obtain the Name of the Day received as input
                string dayPicked = dateProvided.DayOfWeek.ToString();
                
                //Verify if the date selected by the user is the same day of the restriction of the Plate
                if (dayPicked != dayOfWeekPlate)
                {
                    return true;
                }
                else
                {
                    //If the time provided by the user is between the restriction of the Day it will return that the car cannot be on road, otherwise it will be able to be on road
                    if ((timeProvided >= restriction_1 && timeProvided <= restriction_2) || (timeProvided >= restriction_3 && timeProvided <= restriction_4))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

    }
}