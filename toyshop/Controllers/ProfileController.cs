
﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using toyshop.Models;
using toyshop.Models.Profile;
 
﻿using Microsoft.AspNetCore.Mvc;
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 using Microsoft.AspNetCore.Cors.Infrastructure;
 using toysite.Models;

 namespace toyshop.Controllers
{
    public class ProfileController : Controller
    {
 
        private IAddressRepository _AddressRepository;
        private UserManager<User> _UserManager;
        public ProfileController(IAddressRepository AddressRepository, UserManager<User> UserManager)
        {
            _UserManager = UserManager;
            _AddressRepository = AddressRepository;
        }
 
        public IActionResult Index()
        {
            return View();
        }
 
        [HttpPost]
        public async Task<IActionResult> save(string province, string city, string address, string tel)
        {
            var customer = await _UserManager.FindByNameAsync(User.Identity.Name);
            await _AddressRepository.Add(new Address
            {
                Shahr = city,
                Ostan = province,
                Location = address,
                Phone = tel,
                Customer = customer
            });
            await _AddressRepository.save();
            return RedirectToAction("Index" , "Home");
        }

        public async Task<IActionResult> Edite(string CustomerId , int AddresId, string province, string city, string address, string tel)
        {
            var newAddress = new Address()
            {
                Id = AddresId,
                Location = address,
                Shahr = city,
                Ostan = province,
                Phone = tel
            };
            await _AddressRepository.EditeProfile(newAddress, CustomerId);
            await _AddressRepository.save();
            return RedirectToAction("Index", "Home");
        }

    }
}
