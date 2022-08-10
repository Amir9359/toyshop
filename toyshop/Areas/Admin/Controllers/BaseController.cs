using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using toyshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using toysite.Models;

namespace Project_Digikala.Areas.Admin.Controllers
{
    
    public class BaseController : Controller
{
        private UserManager<User> UserManager;
        private User _Operator;
        public BaseController(UserManager<User> UserManager )
        {
            this.UserManager = UserManager;
        }
        public User Operator {
            get
            {
                return GetOperator().GetAwaiter().GetResult();
            }
        }
        private async Task<User> GetOperator()
        {
            _Operator=await UserManager.FindByNameAsync("digiAdmin");
                if (await UserManager.CheckPasswordAsync(_Operator, "amL@87*"))
                {
                    return  _Operator;
                    
                }   
                else
                {  
                  return null;
                }

        }

}
}
