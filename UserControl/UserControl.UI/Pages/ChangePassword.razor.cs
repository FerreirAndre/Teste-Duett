using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using UserControl.UI.Contracts;
using UserControl.UI.Models;

namespace UserControl.UI.Pages
{
    public partial class ChangePassword
    {
        public ChangePasswordVM changePasswordModel { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public string Message { get; set; }

        [Inject]
        private IAuthenticationService AuthenticationService { get; set; }


        protected override void OnInitialized()
        {
            changePasswordModel = new ChangePasswordVM();
        }

        private async Task HandleChangePassword()
        {
            var result = await  AuthenticationService.ChangePassword(changePasswordModel.Email,changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);
            if (result)
            {
                NavigationManager.NavigateTo("/");
            }
            Message = "Something went wrong, please try again.";
        }
    }
}