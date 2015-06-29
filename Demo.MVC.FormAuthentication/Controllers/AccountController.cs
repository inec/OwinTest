using Demo.MVC.FormAuthentication.Models;
//using Demo.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;


namespace Demo.MVC.FormAuthentication.Controllers
{
    public class AccountController : Controller
    {

        IAuthenticationManager Authentication
        {
            get { return HttpContext.GetOwinContext().Authentication; }
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (User.Identity.IsAuthenticated)
                ViewBag.Message = "You Dont have enough Permissions, you need to be with elevated privileges to go there. Auth: " + User.Identity.IsAuthenticated;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                           if (model.HasValidUsernameAndPassword) //Check the database
                {
                    var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Name, model.Email),
                        },
                        DefaultAuthenticationTypes.ApplicationCookie,
                        ClaimTypes.Name, ClaimTypes.Role);

                    // if you want roles, just add as many as you want here (for loop maybe?)
                    identity.AddClaim(new Claim(ClaimTypes.Role, "guest"));
                    // tell OWIN the identity provider, optional
                    // identity.AddClaim(new Claim(IdentityProvider, "Simplest Auth"));


                    Authentication.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = model.RememberMe
                    }, identity);

                    return RedirectToAction("index", "home");

  
                



                  /*  List<Claim> claims = GetClaims(); //Get the claims from the headers or db or your user store
                    if (null != claims)
                    {
                        SignIn(claims);

                        return RedirectToLocal(returnUrl);
                    }



                    ModelState.AddModelError("", "Invalid username or password.");*/

                }
                else
                {
                    //No User of that email address
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            else
            {
                //Model not valid
                ModelState.AddModelError("", "The Model is Not valid");
            }
            // If we got this far, something failed, redisplay form
            return View(model);

        }

      


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //
        // POST: /Account/LogOff
      //  [HttpPost]
       // [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}