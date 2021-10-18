using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using project_management_mcc.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace project_management_mcc.Base
{
    /**
     * E = Entity
     * R = Repository
     * K = Key
     */
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<E, R, K> : ControllerBase
        where E : class
        where R : IRepository<E, K>
    {
        private readonly R repository;

        public BaseController(R repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var data = repository.Get();
            if (data == null)
            {
                return NotFound(data);
            }

            return Ok(data);
        }
        
        [HttpGet("{key}")]
        public ActionResult GetById(K key)
        {
            var data = repository.GetById(key);
            if (data == null)
            {
                return NotFound(new { mewssage = "Data Not Found" });
            }
            return Ok(data);
        }

        [HttpPost]
        public ActionResult Insert(E entity)
        {
            try
            {
                var response = repository.Insert(entity);
                return Ok("Success Insert Data");
            }
            catch (Exception e)
            {
                return BadRequest(new { 
                    message = e,
                });
            }
        }

        [HttpPut]
        public ActionResult Update(E entity)
        {
            try
            {
                repository.Update(entity);
                return Ok("Success Update Data");
            }
            catch
            {
                return BadRequest("Data Not Found");
            }
        }

        [HttpDelete("{key}")]
        public ActionResult Delete(K key)
        {
            try
            {
                repository.Delete(key);
                return Ok("Success Delete Data");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
