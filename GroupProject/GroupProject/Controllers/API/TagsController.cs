using GroupProject.Models;
using GroupProject.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GroupProject.Controllers.API
{
    public class TagsController : ApiController
    {
        private readonly TagsRepository _tags = new TagsRepository();

        //GET /api/tags
        [HttpGet]
        public IHttpActionResult ShowTags()
        {
            return Ok(_tags.GetTags());
        }

        //POST /api/tags
        [HttpPost]
        public IHttpActionResult CreateTag([FromBody]Tag tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _tags.AddTag(tag.Name);

            return Ok();
        }
    }
}
