using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string name = Page.User.Identity.Name;
            if (Page.User.IsInRole("admin"))
            {
                liMillENtry.Visible = true;
                liDeposit.Visible = true;
                liExpense.Visible = true;
               
            }
            else if (Page.User.IsInRole("systemAdmin"))
            {
                liMillENtry.Visible = true;
                liDeposit.Visible = true;
                liExpense.Visible = true;               
                liMember.Visible = true;
                liMaintenance.Visible = true;
            }
           
        }

    }

    protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/Report");

    }
}