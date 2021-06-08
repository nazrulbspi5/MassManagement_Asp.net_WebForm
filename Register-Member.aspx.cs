using System;
using System.Data;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class Register_Member : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string name = User.Identity.Name;
            if (name == "")
            {
                Response.Redirect("~/Account/Login?ReturnUrl=Register-Member");
            }
            if (!User.IsInRole("systemAdmin"))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Account/Login");
            }
            LoadData();
        }
    }
    [WebMethod]
    public static bool IsUserNameAvailable(string userName)
    {
        try
        {
            return Membership.GetUser(userName) == null;
        }
        catch
        {
            return false;
        }
    }
    private void LoadData()
    {
        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT Id,Name,Status FROM Member");
        gvMember.DataSource = dt;
        gvMember.EmptyDataText = "No Data Found!";
        gvMember.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text != "Update")
            {
                DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("Select Id From Member Where Name='" + tbxName.Text.Trim() + "'");
                if (dt.Rows.Count > 0)
                {
                    msgLabel.Text = "Member name already exist.!";
                    msgLabel.Attributes.Add("class", "msg_warning");
                }
                else
                {
                    DatabaseGateway.DatabaseManager.GetInstance().ExecuteCommand("INSERT INTO Member (Name,Mobile,Email,Address,Status) VALUES('" + tbxName.Text.Trim() + "','"+tbxMobile.Text+"','"+tbxEmail.Text+"','"+tbxAddress.Text+"','"+ddlstatus.SelectedValue+"')");
                    msgLabel.Text = "Member Name Save Successfully.";
                    msgLabel.Attributes.Add("class", "msg_success");
                }
                
            }
            else
            {
                DatabaseGateway.DatabaseManager.GetInstance().ExecuteCommand("Update Member SET Name='"+tbxName.Text.Trim()+ "',Mobile='"+tbxMobile.Text+ "',Email='"+tbxEmail.Text+ "',Address='" + tbxAddress.Text+"',Status='" + ddlstatus.SelectedValue+"' Where Id='" + Session["Id"] + "'");
                msgLabel.Text = "Member Name Updated Successfully.";
                msgLabel.Attributes.Add("class", "msg_success");
            }
        }
        catch (Exception er)
        {

            msgLabel.Text = "ERROR "+er.ToString();
            msgLabel.Attributes.Add("class", "msg_warning");
        }
        finally
        {
            tbxName.Text = "";
            LoadData();
        }
    }

    protected void gvMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = Convert.ToInt32(gvMember.SelectedIndex);
        Label lblItemName = gvMember.Rows[index].FindControl("Label1") as Label;
        EditMode(lblItemName.Text);

    }

    private void EditMode(string id)
    {
        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT Name,Mobile,Email,Address,Status FROM Member WHERE Id='" + id + "'");
        if (dt.Rows.Count > 0)
        {
            tbxName.Text = dt.Rows[0]["Name"].ToString();
            tbxMobile.Text = dt.Rows[0]["Mobile"].ToString();
            tbxEmail.Text = dt.Rows[0]["Email"].ToString();
            tbxAddress.Text = dt.Rows[0]["Address"].ToString();
            ddlstatus.SelectedValue = dt.Rows[0]["Status"].ToString();

            btnSave.Text = "Update";
            msgLabel.Text = "Edit Mode Activated!";
            msgLabel.Attributes.Add("class", "msg_info");
            Session["Id"] = id;
            
        }

    }

    protected void gvMember_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.RowIndex);
            Label lblItemCode = gvMember.Rows[index].FindControl("Label1") as Label;

            DatabaseGateway.DatabaseManager.GetInstance().ExecuteCommand("DELETE FROM Member Where Id='" + lblItemCode.Text + "'");
            msgLabel.Attributes.Add("class", "msg_success");
            msgLabel.Text = "Delete successfully....";
            LoadData();
        }
        catch (Exception ex)
        {
            msgLabel.Attributes.Add("class", "msg_error");
            msgLabel.Text = "ERROR: " + ex.Message.ToString();
        }
    }
}