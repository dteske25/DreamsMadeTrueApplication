﻿using System.Threading.Tasks;
using DreamsMadeTrue.Engines.Client.Dtos;

namespace DreamsMadeTrue.Engines.Client.Interfaces
{
    public interface IUserEngine
    {
        Task<UserDto> FindUserById(string id);
        Task<UserDto> FindUserByEmail(string email);
        Task<UserDto> FindUserByUsername(string username);
        Task<UserDto> CreateUser(UserDto userInfo, string password);
        Task<string> GenerateEmailConfirmation(UserDto userInfo);
        Task<SignInResultDto> SignInUser(UserDto userInfo);
        Task<SignInResultDto> PasswordSignInUser(UserDto userInfo, string password);
        Task<SignInResultDto> ConfirmEmail(string userId, string code);
    }
}
