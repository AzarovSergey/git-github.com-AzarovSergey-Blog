using System;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using MvcPL.Infrastructure;
using MvcPL.Providers;
using MvcPL.ViewModels;
using MvcPL.Mapper;
using BLL.Interface.Services;
using MvcPL.Models;
using System.Collections.Generic;
using System.Web;


namespace MvcPL.Controllers
{
    public class ErrorController:Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}