using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize] // Alle Anfragen müssen genehmigt werden
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository repo;
        private readonly IMapper mapper;

        public UsersController(IUserRepository repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
        //    var users = await repo.GetUsersAsnyc();
        //    var usersToReturn = mapper.Map<IEnumerable<MemberDto>>(users);
        //    return Ok(usersToReturn);

        var users = await repo.GetMembersAsync();
        return Ok(users);

        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<AppUser>> GetUser(int id)
        // {
        //     return await repo.GetUserByIdAsnyc(id);
        // }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await repo.GetMemberAsync(username);
        }
    }
}