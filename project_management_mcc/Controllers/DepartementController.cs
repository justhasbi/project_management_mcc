﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Controllers
{
    public class DepartementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
