using Microsoft.AspNetCore.Mvc;
using Lab1.Models;
using Lab1.DTO;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuysController : ControllerBase
    {
        private readonly Context _context;
        private readonly ILogger<BuysController> _logger;

        public BuysController(Context context, ILogger<BuysController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuyResponseDTO>>> GetBuys()
        {
            var buys = await _context.Buys.Include(b => b.BuyProducts).ThenInclude(bp => bp.Product).ToListAsync();
            var buyDtos = buys.Select(b => new BuyResponseDTO
            {
                Id = b.Id,
                ProveedorId = b.ProveedorId,
                Total = b.Total,
                FechaInicio = b.FechaInicio,
                FechaPago = b.FechaPago,
                Pagado = b.Pagado,
                Entregado = b.Entregado,
                FechaCreacion = b.FechaCreacion,
                Productos = b.BuyProducts.Select(bp => new ProductResponseDTO
                {
                    Id = bp.Product.Id,
                    Name = bp.Product.Name,
                    Description = bp.Product.Description,
                    Price = bp.Product.Price,
                    CategoryId = bp.Product.CategoryId
                }).ToList()
            }).ToList();

            return Ok(buyDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BuyResponseDTO>> GetBuy(int id)
        {
            var buy = await _context.Buys.Include(b => b.BuyProducts).ThenInclude(bp => bp.Product).FirstOrDefaultAsync(b => b.Id == id);
            if (buy == null)
            {
                return NotFound();
            }

            var buyDto = new BuyResponseDTO
            {
                Id = buy.Id,
                ProveedorId = buy.ProveedorId,
                Total = buy.Total,
                FechaInicio = buy.FechaInicio,
                FechaPago = buy.FechaPago,
                Pagado = buy.Pagado,
                Entregado = buy.Entregado,
                FechaCreacion = buy.FechaCreacion,
                Productos = buy.BuyProducts.Select(bp => new ProductResponseDTO
                {
                    Id = bp.Product.Id,
                    Name = bp.Product.Name,
                    Description = bp.Product.Description,
                    Price = bp.Product.Price,
                    CategoryId = bp.Product.CategoryId
                }).ToList()
            };

            return Ok(buyDto);
        }

        [HttpPost]
        public async Task<ActionResult<Buy>> CreateBuy(BuyDTO buyDto)
        {
            _logger.LogInformation("Iniciando CreateBuy");

            if (buyDto == null)
            {
                _logger.LogError("El objeto BuyDTO es nulo.");
                return BadRequest("El objeto BuyDTO es nulo.");
            }

            if (buyDto.ProductoIds == null || !buyDto.ProductoIds.Any())
            {
                _logger.LogError("Los productos son requeridos.");
                return BadRequest("Los productos son requeridos.");
            }

            try
            {
                var buy = new Buy
                {
                    ProveedorId = buyDto.ProveedorId,
                    Total = buyDto.Total,
                    FechaInicio = buyDto.FechaInicio,
                    FechaPago = buyDto.FechaPago,
                    Pagado = buyDto.Pagado,
                    Entregado = buyDto.Entregado,
                    BuyProducts = buyDto.ProductoIds.Select(productId => new BuyProduct { ProductId = productId }).ToList()
                };

                _logger.LogInformation("Validando la compra");
                buy.Validate();

                _logger.LogInformation("Agregando la compra a la base de datos");
                _context.Buys.Add(buy);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Compra creada exitosamente");
                return CreatedAtAction(nameof(GetBuy), new { id = buy.Id }, buy);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Error al guardar los cambios en la base de datos");
                return BadRequest(new { message = dbEx.Message, details = dbEx.InnerException?.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la compra");
                return BadRequest(new { message = ex.Message, details = ex.StackTrace });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBuy(int id, BuyDTO updatedBuyDto)
        {
            var buy = await _context.Buys.Include(b => b.BuyProducts).FirstOrDefaultAsync(b => b.Id == id);
            if (buy == null)
            {
                return NotFound();
            }

            try
            {
                buy.ProveedorId = updatedBuyDto.ProveedorId;
                buy.Total = updatedBuyDto.Total;
                buy.FechaInicio = updatedBuyDto.FechaInicio;
                buy.FechaPago = updatedBuyDto.FechaPago;
                buy.Pagado = updatedBuyDto.Pagado;
                buy.Entregado = updatedBuyDto.Entregado;
                buy.BuyProducts = updatedBuyDto.ProductoIds.Select(productId => new BuyProduct { ProductId = productId }).ToList();

                buy.Validate();
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuy(int id)
        {
            var buy = await _context.Buys.FindAsync(id);
            if (buy == null)
            {
                return NotFound();
            }

            _context.Buys.Remove(buy);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}