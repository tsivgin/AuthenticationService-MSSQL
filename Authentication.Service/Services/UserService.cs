using System.Linq;
using System.Threading.Tasks;
using Authentication.Core.DTOs;
using Authentication.Core.Model;
using Authentication.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SharedLibrary.Dtos;

namespace Authentication.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public UserService(
            IConfiguration configuration,
            UserManager<UserApp> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _config = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<Response<UserAppDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = new UserApp {Email = createUserDto.Email, UserName = createUserDto.UserName};
            try
            {
                var result = await _userManager.CreateAsync(user, createUserDto.Password);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(x => x.Description).ToList();

                    return Response<UserAppDto>.Fail(new ErrorDto(errors, true), 400);
                }
                bool roleExists = await _roleManager.RoleExistsAsync(_config["Roles:User"]);
                
                if (!roleExists)
                {
                    IdentityRole role = new IdentityRole(_config["Roles:User"]);
                    role.NormalizedName = _config["Roles:User"];
                
                    _roleManager.CreateAsync(role).Wait();
                }
                
                //Kullanıcıya ilgili rol ataması yapılır.
                _userManager.AddToRoleAsync(user, _config["Roles:User"]).Wait();
            }
            catch (Exception e)
            {
            }


            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
        }

        public async Task<Response<UserAppDto>> GetUserByNameAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                return Response<UserAppDto>.Fail("UserName not found", 404, true);
            }

            return Response<UserAppDto>.Success(ObjectMapper.Mapper.Map<UserAppDto>(user), 200);
        }

        public async Task<Response<CreateRoleDto>> CreateRole(CreateRoleDto roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName.Role);
            if (!roleExist)
            {
                //create the roles and seed them to the database: Question 1
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName.Role));

                if (roleResult.Succeeded)
                {
                    return Response<CreateRoleDto>.Success(ObjectMapper.Mapper.Map<CreateRoleDto>(roleExist), 200);
                }
                else
                {
                    return Response<CreateRoleDto>.Fail("Not Insert", 400,true);
                }
            }

            return Response<CreateRoleDto>.Fail("Aynı Kayıt var",400,true );
        }

        public async Task<Response<UserRoleMappingDto>> AddUserRoleMapping(UserRoleMappingDto userRoleMappingDto)
        {
            var user = await _userManager.FindByEmailAsync(userRoleMappingDto.Email);
            if(user != null)
            {
                var userRole = await _userManager.GetRolesAsync(user);
                if (userRole.Contains(userRoleMappingDto.RoleName))
                {
                    return Response<UserRoleMappingDto>.Fail("Atama Zaten var", 400,true);
                }
                var result = await _userManager.AddToRoleAsync(user, userRoleMappingDto.RoleName);

                if(result.Succeeded)
                {
                    return Response<UserRoleMappingDto>.Success(ObjectMapper.Mapper.Map<UserRoleMappingDto>(userRoleMappingDto), 200);
                }
                else
                {
                    return Response<UserRoleMappingDto>.Fail("Not Insert", 400,true);
                }
            }
            return Response<UserRoleMappingDto>.Fail("Geçersiz Email", 400,true);

        }
        public async Task<Response<List<string>>> GetUserRoles(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            if (user == null) return Response<List<string>>.Fail("Geçersiz Email", 400, true);
            var userRole = await _userManager.GetRolesAsync(user);
            return Response<List<string>>.Success(ObjectMapper.Mapper.Map<List<string>>(userRole), 200);

        }
        public async Task<Response<UserRoleMappingDto>> RemoveUserFromRole(UserRoleMappingDto userRoleMappingDto)
        {
            var user = await _userManager.FindByEmailAsync(userRoleMappingDto.Email);
            if(user != null)
            {
                
                var result = await _userManager.RemoveFromRoleAsync(user, userRoleMappingDto.RoleName);

                if(result.Succeeded)
                {
                    return Response<UserRoleMappingDto>.Success(ObjectMapper.Mapper.Map<UserRoleMappingDto>(userRoleMappingDto), 200);
                }
                else
                {
                    return Response<UserRoleMappingDto>.Fail("Not Delete", 400,true);
                }
            }
            return Response<UserRoleMappingDto>.Fail("Geçersiz Email", 400,true);

        }
    }
}