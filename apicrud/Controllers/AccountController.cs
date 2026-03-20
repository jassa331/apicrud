using apicrud.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AccountController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly ClientDbcontext _context;

    public AccountController(IConfiguration configuration, ClientDbcontext context)
    {
        _configuration = configuration;
        _context = context;
    }

    [HttpGet]
    public IActionResult Register() => View();

    //[HttpPost]
    public IActionResult Register(login model)
    {
        string encryptedPassword = EncryptionHelper.Encrypt(model.Password);
        model.Password = encryptedPassword;
        _context.Userss.Add(model);
     
       
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
            }

        _context.Userss.Add(model);
        try
        {
            _context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex.InnerException?.Message);
        }

        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult Login() => View();

    [HttpPost]
    public IActionResult Login(login model)
    {
        string failure;
        int fail = 0;

        if (HttpContext.Request.Cookies.TryGetValue("Login", out failure))
        {
            int.TryParse(failure, out fail);
        }

        if (fail >= 3)
        {
            return View("Failure"); 
        }

        var user = _context.Userss.FirstOrDefault(x => x.Email == model.Email);

        if (user != null)
        {
            string decryptedPassword = EncryptionHelper.Decrypt(user.Password);
            if (model.Password == decryptedPassword)
            {
                Response.Cookies.Delete("Login");


                var token = GenerateJwtToken(user.Email);
                Response.Cookies.Append("JWToken", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false,
                    Expires = DateTimeOffset.UtcNow.AddSeconds(150000)
                });

                return RedirectToAction("Index", "infoes");
            }
        }

        
        fail++;

        CookieOptions options = new CookieOptions
        {
            Expires = DateTime.Now.AddSeconds(10)
        };

        HttpContext.Response.Cookies.Append("Login", fail.ToString(), options);

        return View("Login");
    }



//ViewBag.Error = "Invalid login";
//        return View(model);
//    }

    [HttpPost]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("JWToken");
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    private string GenerateJwtToken(string email)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
