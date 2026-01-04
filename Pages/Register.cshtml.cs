
namespace ClassProject.Pages;

using ClassProject.Data;
using ClassProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using ClassProject.Data;
using ClassProject.Models;

public class RegisterModel : PageModel
{
    private readonly AppDbContext _context;

    public RegisterModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty] public string FirstName { get; set; }
    [BindProperty] public string LastName { get; set; }
    [BindProperty] public string Email { get; set; }
    [BindProperty] public string Password { get; set; }
    [BindProperty] public string ConfirmPassword { get; set; }
    public string Message { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Password != ConfirmPassword)
        {
            Message = "رمز عبور با تکرار آن مطابقت ندارد.";
            return Page();
        }
        // ایجاد کاربر جدید و ذخیره در دیتابیس (اینجا صرفاً مثال و بدون هش رمز)
        var user = new User
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Username = Email,       // می‌توان نام‌کاربری را ایمیل قرار داد
            PasswordHash = Password // توجه: در عمل باید هش شود!
        };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return RedirectToPage("/Login");
    }
}

