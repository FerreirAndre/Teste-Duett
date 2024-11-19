using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using UserControl.UI.Contracts;
using UserControl.UI.Models;
using UserControl.UI.Providers;

namespace UserControl.UI.Pages;


public partial class Index
{

    public List<UserVM> Users { get; private set; }
    
    [Inject]
    private IJSRuntime JsRuntime { get; set; }
    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    [Inject]
    public IAuthenticationService AuthenticationService { get; set; }

    protected async override Task OnInitializedAsync()
    {
        await ((ApiAuthenticationStateProvider)AuthenticationStateProvider).GetAuthenticationStateAsync();
        Users = await AuthenticationService.GetUsers();
    }
    
    protected void GoToLogin()
    {
        NavigationManager.NavigateTo("login/");
    }

    protected async Task DeleteUser(string userId)
    {
        bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Are you sure?");
        if (confirmed)
        {
            await AuthenticationService.DeleteAsync(userId);
            NavigationManager.Refresh();
        }
    }

    protected void GoToRegister()
    {
        NavigationManager.NavigateTo("register/");
    }
    
    protected async void GoToLogout()
    {
        await AuthenticationService.Logout();
    }
}