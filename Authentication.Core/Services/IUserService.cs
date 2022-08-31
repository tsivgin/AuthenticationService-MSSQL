using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Authentication.Core.DTOs;

namespace Authentication.Core.Services
{
    public interface IUserService
    {
        Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto);

        Task<Response<UserAppDto>> GetUserByNameAsync(string userName);
        Task<Response<CreateRoleDto>> CreateRole(CreateRoleDto roleName);
        Task<Response<UserRoleMappingDto>> AddUserRoleMapping(UserRoleMappingDto userRoleMappingDto);
        Task<Response<List<string>>> GetUserRoles(string userName);
        Task<Response<UserRoleMappingDto>> RemoveUserFromRole(UserRoleMappingDto userRoleMappingDto);
    }
}