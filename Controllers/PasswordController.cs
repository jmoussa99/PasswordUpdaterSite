using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PasswordUpdaterFramework.Models;
using PasswordUpdaterFramework.Services;

namespace PasswordUpdaterFramework.Controllers
{
    public class PasswordController : Controller
    {
        private readonly OnBaseService _onBaseService = new OnBaseService();

        [HttpGet]
        public ActionResult Index()
        {
            return View(new PasswordResetModel());
        }

        [HttpPost]
        public ActionResult Index(PasswordResetModel model)
        {
            if(model.NewPassword.Length < 8)
            {
                ViewBag.Message = "Password needs to be at least 8 characters long.";
                return View(model);
            }
            if(!string.Equals((model.NewPassword),(model.repeatPassword)))
            {
                ViewBag.Message = "Passwords do not match.";
                return View(model);
            }
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.NewPassword))
            {
                ViewBag.Message = "Username and password are required.";
                return View(model);
            }

            var success = _onBaseService.ChangeUserPassword(model.Username, model.NewPassword, out var message);
            ViewBag.Message = message;
            return View(model);
        }
    }
}