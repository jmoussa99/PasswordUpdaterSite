using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hyland.Unity;
using System.Configuration;

namespace PasswordUpdaterFramework.Services
{
    public class OnBaseService
    {
        private readonly string appServerUrl = System.Configuration.ConfigurationManager.AppSettings["OnBase.AppServerUrl"];
        private readonly string username = System.Configuration.ConfigurationManager.AppSettings["OnBase.Username"];
        private readonly string password = System.Configuration.ConfigurationManager.AppSettings["OnBase.Password"];
        private readonly string dataSource = System.Configuration.ConfigurationManager.AppSettings["OnBase.DataSource"];

        private Application Connect()
        {
            var authProps = Application.CreateOnBaseAuthenticationProperties(appServerUrl, username, password, dataSource);
            authProps.LicenseType = LicenseType.Default;
            return Application.Connect(authProps);
        }

        public bool ChangeUserPassword(string userName, string newPassword, out string message)
        {
            try
            {
                using (var app = Connect())
                {
                    var user = app.Core.GetUser(userName);
                    if (user == null)
                    {
                        message = "User not found.";
                        return false;
                    }

                    if(user.IsLocked()) app.Core.UserAdministration.UnlockUserAccount(user);
                    var userProps = app.Core.UserAdministration.CreateEditableUserProperties();

                    userProps.UserName = userName;
                    userProps.Password = newPassword;
                    userProps.ChangePasswordAtLogin = false;

                    app.Core.UserAdministration.ChangeUserProperties(user, userProps);
                    message = "Password updated successfully.";
                    return true;
                }
            }
            catch (Exception ex)
            {
                message = $"Error: {ex.Message}";
                return false;
            }
        }
    }
}