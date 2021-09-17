using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AcheruFriends.Data;

namespace AcheruFriends
{
    public partial class ExportConDataController : ExportController
    {
        private readonly ConDataContext context;

        public ExportConDataController(ConDataContext context)
        {
            this.context = context;
        }
        [HttpGet("/export/ConData/genders/csv")]
        [HttpGet("/export/ConData/genders/csv(fileName='{fileName}')")]
        public FileStreamResult ExportGendersToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.Genders, Request.Query), fileName);
        }

        [HttpGet("/export/ConData/genders/excel")]
        [HttpGet("/export/ConData/genders/excel(fileName='{fileName}')")]
        public FileStreamResult ExportGendersToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.Genders, Request.Query), fileName);
        }
        [HttpGet("/export/ConData/myfriends/csv")]
        [HttpGet("/export/ConData/myfriends/csv(fileName='{fileName}')")]
        public FileStreamResult ExportMyFriendsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(context.MyFriends, Request.Query), fileName);
        }

        [HttpGet("/export/ConData/myfriends/excel")]
        [HttpGet("/export/ConData/myfriends/excel(fileName='{fileName}')")]
        public FileStreamResult ExportMyFriendsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(context.MyFriends, Request.Query), fileName);
        }
    }
}
