using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);

        Task<bool> SaveAllAsnyc();

        Task<IEnumerable<AppUser>> GetUsersAsnyc();

        Task<AppUser> GetUserByIdAsnyc(int id);

        Task<AppUser> GetUserByUsernameAsync(string name);
        Task<IEnumerable<MemberDto>> GetMembersAsync();
        Task<MemberDto> GetMemberAsync(string name);

        

    }
}