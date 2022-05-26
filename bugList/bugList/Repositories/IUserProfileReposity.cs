using bugList.Models;
using System.Collections.Generic;

namespace bugList.Repositories
{
    public interface IUserProfileReposity
    {
        UserProfile GetByFirebaseUserId(string firebaseUserId);
        UserProfile GetById(int id);
        List<UserProfile> GetAll();
    }
}
