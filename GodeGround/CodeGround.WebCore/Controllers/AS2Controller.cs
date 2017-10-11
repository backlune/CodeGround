using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CodeGround.WebCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CodeGround.WebCore.Controllers
{
   [Route("api/[controller]")]
   public class AS2Controller : Controller
   {
      //// GET: api/values
      //[HttpGet]
      //[SwaggerResponse(200, Type = typeof(string))]
      //public IActionResult Get()
      //{
      //   return Content("value1");
      //}

      //[HttpGet]
      //[Produces(typeof(string))]
      //public IActionResult Get2()
      //{
      //   return Content("value1");
      //}


      [HttpGet("GetJson")]
      [SwaggerResponse(typeof(string))]
      //[SwaggerResponse(200, typeof())]
      public IActionResult GetJson()
      {
         return Json("value1");
      }

      [HttpGet("Get2Json")]
      [Produces(typeof(string))]
      public IActionResult Get2Json()
      {
         return Json("value1");
      }

      // GET api/values/5
      [HttpGet("{id}")]
      [SwaggerResponse(typeof(AS2Data))]
      //[Produces(typeof(AS2Data))]
      public IActionResult Get(int id)
      {
         return Json(new AS2Data {Value1 = 5, Value2 = "value", Value3 = 99.75});
      }

      //// GET api/values/5
      //[HttpGet("{id}")]
      ////[SwaggerResponse(200, Type = typeof(AS2Data))]
      //[Produces(typeof(AS2Data))]
      //public IActionResult Get2(int id)
      //{
      //   return Json(new AS2Data { Value1 = 5, Value2 = "value", Value3 = 99.75 });
      //}

      //get api/as2/get2?id=2%name=test

      // POST api/as2/mdn
      [HttpPost("mdn")]
      public ActionResult Mdn([FromBody]AS2Data value)
      {
         try
         {
            string as2From = Request.Headers["AS2-FROM"];
            CreateCallNode(Request.Headers, value.Value2, "AS2MDN");
            dynamic m_pComApi = GetApi();
            m_pComApi.CallComponent(componentId: "{4C685EC2-2468-4CFA-824C-8B6A89367426}", eventParameter: as2From, callType: 1);

            return Ok();
         }
         catch (Exception ex)
         {
            return BadRequest();
         }
      }

      private void CreateCallNode(IHeaderDictionary requestHeaders, string value, string as2mdn)
      {
         throw new NotImplementedException();
      }

      private object GetApi()
      {
         throw new NotImplementedException();
      }

      // POST api/values
      [HttpPost("message")]
      public void Message([FromBody]string value)
      {
      }

      // PUT api/values/5
      [HttpPut("{id}")]
      public void Put(int id, [FromBody]string value)
      {
      }

      // DELETE api/values/5
      [HttpDelete("{id}")]
      public void Delete(int id)
      {
      }
   }
}
