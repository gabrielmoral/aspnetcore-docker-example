using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Api.Domain;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IValuesProcessor _procesor;

        public ValuesController(IValuesProcessor procesor)
        {
            _procesor = procesor;
        }
        
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return _procesor.Get(id).Value;
        }

        // POST api/values
        [HttpPost]
        public void Post(ValueType value)
        {
            _procesor.Save(value);
        }
    }
}