using bugList.Auth.Models;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace bugList.Auth
{
    //need to add log in Register, and SignUpOrSignIn
    public class FirebaseAuthService : IFirebaseAuthService
    {
        private const string FIREBASE_SIGN_IN_BASE_URL =
            "https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=";
        private const string FIREBASE_SIGN_UP_BASE_URL =
            "https://identitytoolkit.googleapis.com/v1/accounts:signUp?key=";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _firebaseApiKey;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public Task<FirebaseUser> Login(Credentials credentials)
        {
            var url = FIREBASE_SIGN_IN_BASE_URL + _firebaseApiKey;
            //return await SignUpOrSignIn(credentials.Email, credentials.Password, url);
            throw new System.NotImplementedException();
        }

        public Task<FirebaseUser> Register(Registration registration)
        {
            throw new System.NotImplementedException();
        }
    }
}
