using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DnsClient;
using Growthx_assignment.Models;
using Growthx_assignment.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;
using MongoDB.Bson;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Growthx_assignment.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IMongoCollection<Admin> _admins;
        private readonly IMongoCollection<Assignment> _assignments;
        private readonly IMongoCollection<User> _users;
        private string loginId = string.Empty;
        public UserController(mongoDbService mongoDbService)
        {
            _admins = mongoDbService.Database?.GetCollection<Admin>("admin");
            _assignments = mongoDbService.Database?.GetCollection<Assignment>("assignment");
            _users = mongoDbService.Database?.GetCollection<User>("user");

        }
        //        - `POST /register` - Register a new user.
        //- `POST /login` - User login.
        //- `POST /upload` - Upload an assignment.
        //- `GET /admins`- fetch all admins

        [HttpPost("Register")]
        public async Task<ActionResult> register(User user)
        {


            await _users.InsertOneAsync(user);

            return Ok(user);

        }

        [HttpPost("login")]
        public async Task<ActionResult> login(string username, string password)
        {

            var filter = new BsonDocument {
                                                { "username", username },
                                                { "password", password }
                                            };

            User user = await _users.Find(filter).FirstOrDefaultAsync();
            if (user is not null)
            {
                loginId = user.Id;
            }
            //db.admin.find({username : "admin",password : "admin"})
            return user is not null ? Ok("Login Success") : Ok("Incorrect User");

        }
        //upload an assignment
        [HttpPost("uploadAssignment")]
        public async Task<ActionResult> uploadAssignment(Assignment assignment)
        {
            assignment.Userid = loginId;
            await _assignments.InsertOneAsync(assignment);
            return Ok(assignment);

        }

        //getall adminids
        [HttpGet("GetAll")]
        public async Task<IEnumerable<Admin>> Getall()
        {
            return await _admins.Find(FilterDefinition<Admin>.Empty).ToListAsync();
        }


    }
}

