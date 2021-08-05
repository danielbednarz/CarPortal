using System.Collections.Generic;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using API.Interfaces.Repositories;
using API.DTOs;
using AutoMapper;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<MemberDto>>> GetUsers()
        {
            var users = await _userRepository.GetMembersAsync();
            var data = _mapper.Map<List<MemberDto>>(users);

            return Ok(data);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUsers(string username)
        {
            var user = await _userRepository.GetMemberAsync(username);
            var data = _mapper.Map<MemberDto>(user);
            
            return Ok(data);
        }


    }
}
