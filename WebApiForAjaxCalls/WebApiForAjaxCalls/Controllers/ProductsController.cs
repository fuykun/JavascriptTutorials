using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace WebApiForAjaxCalls.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private static List<Product> _productList;
        public ProductsController()
        {
            _productList ??= new List<Product>
            {
                new(1,"MSI B550 TOMAHAWK",1200,5),
                new(2,"ASUS ROG STRIX B460-H",1571,10),
                new(3,"ASUS TUF B550-PLUS",1786,8),
                new(4,"MSI MEG Z490 GODLIKE",9073,2),
                new(5,"MSI MEG X570 UNIFY",3349,15),
            };
        }

        [HttpGet("GetList")]
        public IEnumerable<Product> GetList()
        {
            return _productList;
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            if (id == 0)
                return BadRequest("Id değeri 0'dan büyük olmalıdır.");
            var product = _productList.FirstOrDefault(x => x.Id == id);
            if (product != null) return Ok(product);
            return NotFound();
        }

        [HttpPost("Insert")]
        public IActionResult Insert(Product product)
        {
            if (_productList.Exists(x => x.Name == product.Name))
                return BadRequest("Bu ürün zaten kayıtlı!");

            var newId = _productList.Last().Id + 1;
            product.Id = newId;

            _productList.Add(product);
            return Ok();
        }
    }
}
