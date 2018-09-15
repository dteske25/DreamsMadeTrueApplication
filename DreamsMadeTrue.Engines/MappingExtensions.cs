using DreamsMadeTrue.Core.Models;
using DreamsMadeTrue.Engines.Client.Dtos;

namespace DreamsMadeTrue.Engines
{
    public static class MappingExtensions
    {
        public static ApplicationUser ToBaseObj(this UserDto dto)
        {
            var user = new ApplicationUser()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.Username
            };
            if (!string.IsNullOrWhiteSpace(dto.Id))
            {
                user.Id = dto.Id;
            }
            return user;
        }

        public static UserDto ToDto(this ApplicationUser user)
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
