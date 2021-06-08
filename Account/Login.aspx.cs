using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Login : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //string username = "admin";
        //string password = "abcd1234";
        //MembershipUser mu = Membership.GetUser(username);
        //mu.ChangePassword(mu.ResetPassword(), password);
    }
    protected void Login1_LoggedIn(object sender, EventArgs e)
    {
        if (Roles.IsUserInRole(Login1.UserName, "admin") || Roles.IsUserInRole(Login1.UserName, "systemAdmin"))
        {
            string rURL = Request.QueryString["ReturnUrl"];
            if (rURL != null)
            {
                Response.Redirect("~/"+rURL);
            }
            else
            {
                Response.Redirect("~/Mill-Entry", true);
            }
        }

    }

    protected void Login1_OnLoginError(object sender, EventArgs e)
    {
        MembershipUser membershipUser = Membership.GetUser(Login1.UserName);
        if (membershipUser != null)
        {
            bool isLockedOut = membershipUser.IsLockedOut;
            if (isLockedOut == true)
            {
                Login1.FailureText = "Your account has been locked out for security reasons. <br/> Please contact us to unlock.";
                Panel pnlAlert = Login1.FindControl("pnlAlert") as Panel;
                pnlAlert.Visible = true;
            }
            else
            {
                Login1.FailureText = "<b>Wrong credentials!</b><br/> Wrong password have been specified.";
                Panel pnlAlert = Login1.FindControl("pnlAlert") as Panel;
                pnlAlert.Visible = true;
            }
        }
        else
        {
            Login1.FailureText = "<b>Wrong credentials!</b><br/> Wrong user name or password have been specified.";
            Panel pnlAlert = Login1.FindControl("pnlAlert") as Panel;
            pnlAlert.Visible = true;
        }


    }

}