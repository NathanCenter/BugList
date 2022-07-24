using bugList.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Firebase.Auth;
using bugList.Models;
using System;
using FirebaseAdmin.Auth;

namespace bugList.Auth
{
    public class UserProfileController : Controller
    {
        private readonly IUserProfileRepository _userProfileRepository;
        FirebaseAuthProvider auth;
        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
            auth = new FirebaseAuthProvider(
                           new FirebaseConfig("AIzaSyCbhyotYFGag5vCve17fHTqqxGsPQSpLU8"));
        }
        // GET: UserProfileController
        //log in page

        //https://arno-waegemans.medium.com/firebase-authentication-for-asp-net-core-mvc-defd6135c632

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("_UserToken");
            return RedirectToAction("SignIn");
        }


        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserProfile UserProfile)
        {
            //create the user
            await auth.CreateUserWithEmailAndPasswordAsync(UserProfile.Email, UserProfile.Password,UserProfile.FirebaseId);


            
            //log in the new user
            var fbAuthLink = await auth
                            .SignInWithEmailAndPasswordAsync(UserProfile.Email, UserProfile.Password);
            string token = fbAuthLink.FirebaseToken;
            string localId = fbAuthLink.User.LocalId;
            //var authData = auth.LinkAccountsAsync(UserProfile.Email, UserProfile.Password, UserProfile.FirebaseId);
            Console.WriteLine(token);
            //saving the token in a session variable
            if (token != null)
            {
                
                HttpContext.Session.SetString("_UserToken", token);
                _userProfileRepository.Add(UserProfile,localId);
                

                return RedirectToAction("Index", "ProjectList");
               
            }
            else
            {
                return View();
            }
        }

        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(UserProfile userprofile)
        {
            //log in the user
            var fbAuthLink = await auth
                            .SignInWithEmailAndPasswordAsync(userprofile.Email, userprofile.Password);
            string token = fbAuthLink.FirebaseToken;
            string localId = fbAuthLink.User.LocalId;
            //saving the token in a session variable
            if (token != null)
            {
                HttpContext.Session.SetString("_UserToken", token);

                return RedirectToAction("Index", "ProjectList");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Index()
        {
            return View();


        }

        // GET: UserProfileController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserProfileController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserProfileController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
