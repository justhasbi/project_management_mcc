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
    public class ActivitiesController : BaseController<Activity, ActivityRepository, int>
    {

        private readonly ActivityRepository activityRepository;

        public ActivitiesController(ActivityRepository activityRepository) : base(activityRepository)
        {
            this.activityRepository = activityRepository;
        }
        
        [Authorize(Roles = "Manager, Human Resource")]
        [HttpPost("CreateMultipleActivity")]
        public ActionResult InsertMultipleActivity(CreateListActivityVM createListActivityVM)
        {
            try
            {
                activityRepository.CreateMultipleActivity(createListActivityVM);
                return Ok(new
                {
                    status = HttpStatusCode.OK,
                    Message = "Success"
                });
            }
            catch
            {
                return BadRequest(new
                {
                    status = HttpStatusCode.BadRequest,
                    Message = "Failed"
                });
            }
        }

        [Authorize]
        [HttpPut("UpdateActivityStatus")]
        public ActionResult UpdateActivityStatus(UpdateStatusVM updateStatusVM)
        {
            try
            {
                activityRepository.UpdateActivityStatus(updateStatusVM);
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

        
        [HttpGet("GetByProject/{id}")]
        public ActionResult GetByProject(int id)
        {
            try
            {
                var data = activityRepository.GetByProject(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                return NotFound(e);
            }
        }
    }
}
