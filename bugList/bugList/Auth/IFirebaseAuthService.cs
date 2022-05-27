using bugList.Auth.Models;
using System.Threading.Tasks;
namespace bugList.Auth
{
    public interface IFirebaseAuthService
    {
        Task<FirebaseUser> Login(Credentials credentials);
        Task<FirebaseUser> Register(Registration registration);
    }
}
