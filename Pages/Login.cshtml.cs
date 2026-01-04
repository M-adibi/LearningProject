
namespace ClassProject.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using ClassProject.Data;
using System.Data.Entity;

public class LoginModel : PageModel
{
    private readonly AppDbContext _context;

    public LoginModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty] public string Username { get; set; }
    [BindProperty] public string Password { get; set; }
    [BindProperty] public bool RememberMe { get; set; }
    public string Message { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Username == null || Password == null)
        {
            Message = "لطفاً همه فیلدها را پر کنید.";
            return Page();
        }

        // جستجوی کاربر در دیتابیس (اینجا رمز بدون هش‌کردن چک می‌شود؛ در عمل باید هش شود)
        var user = await _context.Users
            .SingleOrDefaultAsync(u => u.Username == Username && u.PasswordHash == Password);

        if (user == null)
        {
            Message = "نام کاربری یا رمز عبور اشتباه است.";
            return Page();
        }

        // ساخت ClaimsIdentity و ثبت کوکی برای کاربر
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Username) };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
            new AuthenticationProperties { IsPersistent = RememberMe });

        return RedirectToPage("/Index");
    }
}