using bugList.Models;
using System.Collections.Generic;

namespace bugList.Repositories
{
    public interface IUserProfileRepository
    {
        void Add(UserProfile userProfile);
        UserProfile GetByFirebaseUserId(string firebaseUserId);
        UserProfile GetById(int id);
        List<UserProfile> GetAll();
        void Delete(int id);
        void Edit(UserProfile userProfile);
    }
}
