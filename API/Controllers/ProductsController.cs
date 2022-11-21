using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Core.Entities;
using Dapper;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly StoreContext _context;
        public ProductsController (StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult GetProduct()
        {
            try
            {
                using (var db = _context.ConnectSkinet())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    var data = db.Query<Product>("[dbo].[GetProducts]", param: parameters, commandType: System.Data.CommandType.StoredProcedure).ToList();
                    var resultdata = new
                    {
                        Data = data,
                        success = true
                    };
                    return new JsonResult  (new  { Data = resultdata });
                }
            }
            catch (Exception ex)
            {
                var resultdata = new
                {
                    success = false,
                    ErrorMessage = ex.Message
                };
                return new JsonResult (new { Data = resultdata});
            }
        }

        [HttpGet("{Id}")]
        public ActionResult GetProduct(int Id)
        {
            try
            {
                using (var db = _context.ConnectSkinet())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Id", Id);
                    var data = db.Query<Product>("[dbo].[GetProductById]", param: parameters, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                    var resultdata = new
                    {
                        Data = data,
                        success = true
                    };
                    return new JsonResult  (new  { Data = resultdata });
                }
            }
            catch (Exception ex)
            {
                var resultdata = new
                {
                    success = false,
                    ErrorMessage = ex.Message
                };
                return new JsonResult (new { Data = resultdata});
            }
        }
    }
}