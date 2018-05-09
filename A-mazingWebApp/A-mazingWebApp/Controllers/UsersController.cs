using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using A_mazingWebApp.Models;
using MazeModel;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace A_mazingWebApp.Controllers
{
    public class UsersController : ApiController
    {
        private string ComputeHash(string input)
        {
            SHA1 sha = SHA1.Create();
            byte[] buffer = Encoding.ASCII.GetBytes(input);
            byte[] hash = sha.ComputeHash(buffer);
            string hash64 = Convert.ToBase64String(hash);
            return hash64;
        }

        private UsersContext db = new UsersContext();

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/username/password
        [ResponseType(typeof(JObject))]
        public JObject GetUser(string id, string password)
        {
            User user = db.Users.Find(id);
            if (user == null || user.Password != ComputeHash(password))
            {
                //return NotFound;
                return null;
            }

            JObject jsonUser = JObject.Parse(user.ToJSON());

            //return user;
            return jsonUser;
        }
        [ActionName("UpdateScores")]
        [Route("Users/{userName}/{condition}")]
        public IHttpActionResult GetUpdateScores(string userName, string condition)
        {
            User user = db.Users.Where(e => e.Username == userName).FirstOrDefault();
            switch (condition)
            {
                case "win":
                    user.Wins++;
                    break;
                case "lost":
                    user.Losses++;
                    break;
                default:
                    break;
            }
            db.Entry(user).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(userName))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(userName +" "+condition);
        }


        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(string id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Username)
            {
                return BadRequest();
            }

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        [ResponseType(typeof(User))]
        public string PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return "Bad Request";
            }

            if (!UserExists(user.Username))
            {
                user.Password = ComputeHash(user.Password);
                db.Users.Add(user);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    if (UserExists(user.Username))
                    {
                        return "User Already Exists";
                    }
                    else
                    {
                        throw;
                    }
                }
                return "Success";
            }
            return "User exist";
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(string id)
        {
            return db.Users.Count(e => e.Username == id) > 0;
        }
    }
}