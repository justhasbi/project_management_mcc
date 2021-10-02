﻿using project_management_mcc.Context;
using project_management_mcc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_management_mcc.Repositories.Data
{
    public class JobRepostitory : GeneralRepository<MyContext, Job, int>
    {
        public JobRepostitory(MyContext context) : base(context)
        {
        }
    }
}
