using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ErrorTestingController : AppController
    {
        private readonly MainDatabaseContext _context;
        public ErrorTestingController(MainDatabaseContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")]
        public ActionResult<string> GetSecret()
        {
            return "tajny tekst";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var user = _context.Users.Find(-1);

            if (user == null)
            {
                return NotFound();
            }
            else
                return Ok(user);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var uncreatedUser = _context.Users.Find(-34);

            var status = uncreatedUser.ToString();

            return status;
        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("Złe przekierowanie");
        }

    }
}
