using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private DataContext context;

        private IMapper mapper;

        public UserRepository(DataContext context, IMapper mapper){
            this.context = context;
            this.mapper = mapper;
        }



        public async Task<MemberDto> GetMemberAsync(string name)
        {
            return await context.Users
            .Where(x => x.UserName == name)
            .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MemberDto>> GetMembersAsync()
        {
            return await context.Users.ProjectTo<MemberDto>(mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<AppUser> GetUserByIdAsnyc(int id)
        {
            return await context.Users.Include(x => x.Photos).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<AppUser> GetUserByUsernameAsync(string name)
        {
            return await context.Users
            .Include(x => x.Photos)
            .SingleOrDefaultAsync<AppUser>(x => x.UserName == name);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsnyc()
        {
            return await context.Users.Include(x => x.Photos).ToListAsync();
        }

        public async Task<bool> SaveAllAsnyc()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            context.Entry(user).State = EntityState.Modified;
        }
    }
}