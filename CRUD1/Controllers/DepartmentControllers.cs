using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
//using WebAPI.Models;



namespace CRUD1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentControllers : ControllerBase
    {
        private readonly IConfiguration _configuration;
        

        public DepartmentControllers(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        
        [HttpGet(Name = "GetDepartment")]
        public JsonResult Get()
        {
            string query = @"
                        select DepartmentId, DepartmentName from dbo.Department";
            DataTable table = new DataTable();
            string sqlDataSrc = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSrc))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();
                }
                return new JsonResult(table);
            }
            
        }
        [HttpPut]
        public JsonResult Put(Department dep)
        {
            string query = @"
                        update dbo.Department set
                        DepartmentName = '" + dep.DepartmentName + @"'
                        where DepartmentId = " + dep.DepartmentId + @"";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();

                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader=myCommand.ExecuteReader();
                    table.Load(myReader); ;
                    myReader.Close();
                    myCon.Close();

                }
            }
            return new JsonResult("Updated");
        }




    }
}
    


