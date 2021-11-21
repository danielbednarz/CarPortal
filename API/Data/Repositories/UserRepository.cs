using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MainDatabaseContext _context;
        private readonly IMapper _mapper;

        public UserRepository(MainDatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MemberDto> GetMemberAsync(string username)
        {
            return await _context.Users
                .Where(x => x.UserName == username)
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<PagedList<MemberDto>> GetMembersAsync(UserParameters userParams)
        {
            var query = _context.Users.Where(x => x.UserName != userParams.CurrentUsername).AsQueryable();

            if(userParams?.BrandId != 0)
            {
                query = query.Where(x => x.BrandId == userParams.BrandId);
            }

            if(userParams?.ModelId != 0)
            {
                query = query.Where(x => x.ModelId == userParams.ModelId);
            }

            query = query.Where(x => x.EnginePower >= userParams.MinEnginePower && x.EnginePower <= userParams.MaxEnginePower);

            query = userParams.OrderBy switch
            {
                "EnginePowerDesc" => query.OrderByDescending(x => x.EnginePower),
                "EnginePowerAsc" => query.OrderBy(x => x.EnginePower),
                "ProductionDateDesc" => query.OrderByDescending(x => x.ProductionDate),
                "ProductionDateAsc" => query.OrderBy(x => x.ProductionDate),
                "CreateDateAsc" => query.OrderBy(x => x.CreateDate),
                _ => query.OrderByDescending(x => x.CreateDate)
            };

            return await PagedList<MemberDto>.CreateAsync(query.ProjectTo<MemberDto>(_mapper.ConfigurationProvider).AsNoTracking(), userParams.PageNumber, userParams.PageSize);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users
                .Include(x => x.Photos)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
