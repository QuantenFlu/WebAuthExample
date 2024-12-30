using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using WebAuthExample.Data.Account;

namespace WebAuthExample.Components.Account.Pages;

public partial class Login : ComponentBase
{
    public string? errorMessage { get; set; }
    [Inject] private SignInManager<ApplicationUser> SignInManager {get; set;}
    [Inject] ILogger<Login> Logger  {get; set;}
    [Inject] IdentityRedirectManager RedirectManager {get; set;}
    [CascadingParameter] private HttpContext HttpContext { get; set; } = default!;
    [SupplyParameterFromForm] private InputModel Input { get; set; } = new();
    [SupplyParameterFromQuery] private string? ReturnUrl { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (HttpMethods.IsGet(HttpContext.Request.Method))
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        }
    }
    
    public async Task LoginUser()
    {
        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        errorMessage = null;
        var result = await SignInManager.PasswordSignInAsync(Input.Username, Input.Password, Input.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            Logger.LogInformation("User logged in.");
            RedirectManager.RedirectTo(ReturnUrl);
        }
        else
        {
            errorMessage = "Error: Invalid login attempt.";
        }
    }
}