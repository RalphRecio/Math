using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.Json;

namespace Math.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpGet]
        public IActionResult Compute2() 
        {

            List<Vrble> values = new List<Vrble>();
            List<Formulas> fomls = new List<Formulas>();

            String[] forms = { "@x+@y" , "@x-@y" , "@z*@p"};

            values.Add(new Vrble
            {
                Name = "@x",
                Value = 1,
            });

            values.Add(new Vrble
            {
                Name = "@y",
                Value = 2,
            });

            values.Add(new Vrble
            {
                Name = "@p",
                Value = 10,
            });

            values.Add(new Vrble
            {
                Name = "@z",
                Value = 10,
            });


            foreach (string fr in forms) {

                string frr = fr;

                foreach (var val in values)
                {
                    string finalFormula = frr.Replace(val.Name.ToString(), val.Value.ToString());
                    frr = finalFormula;
                }


                var result = new DataTable().Compute(frr, null) ?? 0;
                Console.WriteLine(result);

                fomls.Add(new Formulas
                {
                    formla = frr,
                    result = double.Parse(result.ToString()!)
                });

            }
            string formula = "@x+@y";
            return Ok(fomls);
        }
    }


    public class Vrble 
    {
        public string Name { get; set; }
        public double Value { get; set; }
    }

    public class Formulas
    {
        public string formla { get; set; }
        public double? result { get; set; }
    }

}
