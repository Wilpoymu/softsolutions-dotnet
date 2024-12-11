
using Lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private readonly Context _context;

        public ProviderController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Provider>> GetProvideres()
        {
            return _context.Providers.ToList();
        }

        [HttpPost]
        public ActionResult<Provider> AddProvider(Provider provider)
        {
            _context.Providers.Add(provider);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetProvideres), new { id = provider.Id }, provider);
        }
    }
}