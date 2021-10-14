﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_management_mcc.Base;
using project_management_mcc.Models;
using project_management_mcc.Repositories.Data;
using project_management_mcc.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace project_management_mcc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeActivitiesController : BaseController<EmployeeActivity, EmployeeActivityRepository, int>
    {

        private readonly EmployeeActivityRepository employeeActivityRepository;

        public EmployeeActivitiesController(EmployeeActivityRepository repository) : base(repository)
        {
            this.employeeActivityRepository = repository;
        }
        [Authorize(Roles = "Manager, Human Resource")]
        [HttpPost("AssignMultipleEmployee")]
        public ActionResult AssignMultipleEmployee(CreateListAssignEmployeeVM createListAssignEmployeeVM)
        {
            try
            {
                employeeActivityRepository.AssignMultipleEmployee(createListAssignEmployeeVM);
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    Message = "Success"
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    Message = e
                });
            }
        }

        [HttpGet("GetEmployeeActivity/{id}")]
        public ActionResult GetEmployeeActivity(int id) 
        {
            try
            {
                var data = employeeActivityRepository.GetEmployeeActivity(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPost("DeleteEmployeeAssignment")]
        public ActionResult DeleteEmployeeAssignment(EmployeeActivityVM employeeActivityVM)
        {
            try
            {
                var data = employeeActivityRepository.DeleteEmployeeAssignment(employeeActivityVM);
                return Ok("Success Delete Employee Assignment");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
