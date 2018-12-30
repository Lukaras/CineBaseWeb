﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CineBase.Controllers
{
    public class UserController : Controller
    {
	UserManger manager = new UserManger();	

        public IActionResult Index()
        {
            return View();
        }

	public ViewResult Register()
	{
	    return View();
	}

	public ViewResult Login()
	{
	    return View();
	}

	public void Add(UserViewModel model)
	{
	    manager.Add(model);
	}
    }
}