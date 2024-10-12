using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Growthx_assignment.Models;
using Growthx_assignment.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Growthx_assignment.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        private readonly IMongoCollection<Admin> _admins;
        private readonly IMongoCollection<Assignment> _assignments;
        public AdminController(mongoDbService mongoDbService)
        {
            _admins = mongoDbService.Database?.GetCollection<Admin>("admin");
            _assignments = mongoDbService.Database?.GetCollection<Assignment>("assignment");

        }

        //first register
        [HttpPost("Register")]
        public async Task<ActionResult> register(Admin admin)
        {
            

            await _admins.InsertOneAsync(admin);

            return Ok(admin);

        }

        [HttpPost("login")]
        public async Task<ActionResult> login(string username, string password)
        {
           
                var filter = new BsonDocument {
                                                { "username", username },
                                                { "password", password }
                                            };
           
            var admin = await _admins.Find(filter).FirstOrDefaultAsync();
            //db.admin.find({username : "admin",password : "admin"})
            return admin is not null ? Ok("Login Success") : Ok("Incorrect User");
             
        }
        //getall adminids
        [HttpGet("GetAll")]
        public async Task<IEnumerable<Admin>> Getall()
        {
            return await _admins.Find(FilterDefinition<Admin>.Empty).ToListAsync();
        }

        //[HttpGet("assignments")]
        [HttpGet("GetAssignments_Admin")]
        public async Task<ActionResult> assignmentstoAdmin(string adminId)
        {
            List<Assignment> assignments = await _assignments.Find(x => x.AdminId == adminId).ToListAsync();
            return Ok(assignments);

        }

        [HttpPost("assignments/{assigmentid}/accept")]
        public async Task<ActionResult> assignment_accept(string assigmentid)
        {
            var assign_admin = await _assignments.Find(x => x.Id == assigmentid).FirstOrDefaultAsync();
            assign_admin.Status = "Accepted";

            return Ok(assign_admin);
        }

        [HttpPost("assignments/{assigmentid}/rejected")]
        public async Task<ActionResult> assignment_reject(string assigmentid)
        {

            var assign_admin = await _assignments.Find(x => x.Id == assigmentid).FirstOrDefaultAsync();
            assign_admin.Status = "Rejected";

            return Ok(assign_admin);
        }



    }
}

