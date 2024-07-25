
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using petshop.Data;

using System;
using System.IO;
using Newtonsoft.Json;



namespace petshop.Controllers
{
    [Route("/api/handler")]
    [ApiController]
    public class HandleController : ControllerBase
    {

        private readonly AppDbContext _dbContext;

        public HandleController(AppDbContext userContext)
        {
            _dbContext = userContext;
        }
        [Authorize]
        [HttpGet]
        public ActionResult Handler(int page, int perPage, string? sort, string? search)
        {

            return Ok();
        }


    }
}