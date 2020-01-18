using GroupProject.Models;
using GroupProject.Repositories.ApiRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GroupProject.Controllers.API
{
    public class UsersController : ApiController
    {
        private readonly AdminApiRepository _adminApiRepository = new AdminApiRepository();

        //GET /api/users
        [HttpGet]
        public IHttpActionResult ShowUsers()
        {
            return Ok(_adminApiRepository.GetUsers());
        }

        //public IHttpActionResult GetUserById(int id)
        //{
        //    _adminApiRepository.FindUserById(id);

        //    return Ok();
        //}

        [HttpPut]
        public IHttpActionResult ChangeRole([FromBody]UserRoleViewModel userRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _adminApiRepository.ChangeRoleFromUser(userRole);
            return Ok();

        }

        [HttpDelete]
        public IHttpActionResult Delete(UserRoleViewModel userRole)
        {
            if (userRole == null)
            {
                return BadRequest();

            }

            bool removed = _adminApiRepository.DeleteRoleFromUser(userRole);

            if (!removed)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}








