using System.Collections.Generic;
using DreamsMadeTrue.Core.Enums;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;

namespace DreamsMadeTrue.Core.Models
{
    public class ApplicationUser : IdentityUser, IMongoObject
    {
        public ApplicationUser() : base()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<UserTypes> Roles { get; set; }
        public IEnumerable<UserToken> Tokens { get; set; }
    }

    public class UserToken
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
