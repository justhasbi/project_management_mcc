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
                return NotFound(new {
                    data,
                    message = "Data Not Found",
                });
            }

            return Ok(new {
                data,
                message = "Success",
            });
        }
        
        [HttpGet("{key}")]
        public ActionResult GetById(K key)
        {
            var data = repository.GetById(key);
            if (data == null)
            {
                return NotFound(new
                {
                    data,
                    message = "Data Not Found",
                });
            }

            return Ok(new
            {
                data,
                message = "Success",
            });
        }

        [HttpPost]
        public ActionResult Insert(E entity)
        {
            try
            {
                repository.Insert(entity);
                return Ok(new
                {
                    data = entity,
                    message = "Success Insert Data"
                });
            }
            catch
            {
                return BadRequest(new
                {
                    data = entity,
                    message = "Error Insert Data"
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

        [HttpDelete]
        public ActionResult Delete(K key)
        {
            try
            {
                repository.Delete(key);
                return Ok("Success Delete Data");
            }
            catch
            {
                return BadRequest("Data Not Found");
            }
        }
    }
}
