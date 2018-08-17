using DreamsMadeTrue.Core.Models;
using DreamsMadeTrue.Engines.Client.Dtos;

namespace DreamsMadeTrue.Engines
{
    public static class MappingExtensions
    {
        public static ApplicationUser AsApplicationUser(this UserDto dto)
        {
            return new ApplicationUser
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Username,
                Id = dto.Id
            };
        }

        public static UserDto AsUserDto(this ApplicationUser user)
        {
            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Username = user.UserName,
                Id = user.Id
            };
        }
    }
}
