using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabtaba.Domain.Entities.UserEntity;

namespace Tabtaba.Services.Specifications
{
    public class UserSavedContentWithEntriesSpecification :BaseSpecifications<UserSavedContent>
    {
        public UserSavedContentWithEntriesSpecification(string userId)
            : base(x => x.UserId == userId)
        {
           
            AddInclude(x => x.KnowledgeLibrary);
            AddInclude("KnowledgeLibrary.Doctor.User");
            AddOrderByDescending(x => x.SavedAt);
        }
    }
    
}
