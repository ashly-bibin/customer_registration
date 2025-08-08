using System;
using System.Collections.Generic;
using System.Linq;
using New_cusromer_registeration.DAL;
using New_cusromer_registeration.Models;
using System.Web.Http;

namespace New_cusromer_registeration.Controllers
{
    public class customercontroller: ApiController
    {
        CustomerRepos repo = new CustomerRepos();

        public IEnumerable<Customer> Get()
        {
            return repo.GetAll_Customers();
        }

        public IHttpActionResult Get(int id)
        {
            var cust = repo.GetCustomerBy_Id(id);
            if (cust == null) return NotFound();
            return Ok(cust);
        }

        public IHttpActionResult Post([FromBody] Customer cust)
            {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            int newId = repo.Add_Customer(cust);
            cust.CustomerId = newId;
            return CreatedAtRoute("DefaultApi", new { id = newId }, cust);
        }

        public IHttpActionResult Put(int id, [FromBody] Customer cust)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != cust.CustomerId) return BadRequest();
            bool updated = repo.Update_Customer(cust);
            if (!updated) return NotFound();
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }

        public IHttpActionResult Delete(int id)
        {
            bool deleted = repo.Delete_Customer(id);
            if (!deleted) return NotFound();
            return Ok();
        }
    }
}