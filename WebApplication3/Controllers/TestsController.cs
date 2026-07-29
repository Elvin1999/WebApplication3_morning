using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        //private readonly ICalculateService _calculateServise;
        ////DI 
        //public TestsController(ICalculateService calculateServise)
        //{
        //    _calculateServise = calculateServise;



        //    int data = 100;
        //    Console.WriteLine("Test in constructor");
        //}

        //[HttpGet]
        //public ActionResult<string> Get()
        //{
        //    var result = _calculateServise.CalculateSomething(10, 20);
        //    return "Test GET Method : " + result;
        //}


        private readonly ICalculateService _calculateServise1;
        private readonly ICalculateService _calculateServise2;
        //DI 
        public TestsController(ICalculateService calculateServise1, ICalculateService calculateServise2)
        {
            _calculateServise1 = calculateServise1;
            _calculateServise2 = calculateServise2;

            int data = 100;
            Console.WriteLine("Test in constructor");
        }

        [HttpGet]
        public ActionResult<string> Get()
        {
            var result1 = _calculateServise1.CalculateSomething(10,20);
            var result2 = _calculateServise2.CalculateSomething(10,20);
            return "Test GET Method : "+(result1+result2);
        }
    }
}
