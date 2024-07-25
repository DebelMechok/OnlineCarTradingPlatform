using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using EcommerseProject.Data;
using EcommerseProject.Models;
using EcommerseProject.Identity;

namespace EcommerseProject.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly EcommerseProject.Data.CarDealershipContext _context;

        public DashboardModel(EcommerseProject.Data.CarDealershipContext context)
        {
            _context = context;
        }

        public IList<ApplicationUser> ApplicationUsers { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ApplicationUsers = await _context.Users.OrderBy(x=>x.UserName).ToListAsync();
        }
    }
}
