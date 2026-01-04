

namespace ClassProject.Pages;
using ClassProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClassProject.Data;
using System.Data.Entity;

public class ForgotPasswordModel : PageModel
{
    private readonly AppDbContext _context;

    public ForgotPasswordModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty] public string Email { get; set; }
    public string Message { get; set; }

    public async Task OnPostAsync()
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == Email);
        if (user == null)
        {
            Message = "ایمیل مورد نظر یافت نشد.";
            return;
        }

        Message = "لینک بازیابی رمز به ایمیل شما ارسال شد (شبیه‌سازی).";
    }
}

