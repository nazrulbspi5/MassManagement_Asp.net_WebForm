using System;
using System.Data;
using System.Web.Security;
using System.Web.UI.WebControls;

public partial class Maintenance : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string name = User.Identity.Name;
            if (name == "")
            {
                Response.Redirect("~/Account/Login?ReturnUrl=Maintenance");
            }
            if (!User.IsInRole("systemAdmin"))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Account/Login");
            }
            LoadSettingsData("1");
            LoadUser();
            LoadDefault();
        }
    }
    private void LoadUser()
    {
        ddlUserName.Items.Clear();
        ddlUserName.Items.Insert(0, new ListItem("---Select---", "0"));
        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT UserName FROM aspnet_Users");
        ddlUserName.DataValueField = "UserName";
        ddlUserName.DataTextField = "UserName";
        ddlUserName.DataSource = dt;
        ddlUserName.DataBind();
        if (ddlUserName.Items.FindByText("---Select---") != null)
        {
            ddlUserName.Items.FindByText("---Select---").Selected = true;
        }
    }
    private void LoadSettingsData(string id)
    {
        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT Id, FromDate, Todate FROM Settings WHERE Id='" + id + "'");
        if (dt.Rows.Count > 0)
        {
            tbxFromDate.Text = Convert.ToDateTime(dt.Rows[0]["FromDate"]).ToString("dd-MMM-yyyy");
            tbxToDate.Text = Convert.ToDateTime(dt.Rows[0]["Todate"]).ToString("dd-MMM-yyyy");

        }
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            string query = @"Truncate Table Deposit Truncate Table Expenses Truncate Table MillEntry";
            DatabaseGateway.DatabaseManager.GetInstance().ExecuteCommand(query);

            lblAlert.Attributes.Add("class", "msg_success");
            lblMsg.Text = "Clear Database Successfuly.";
        }
        catch (Exception er)
        {
            lblAlert.Attributes.Add("class", "msg_warning");
            lblMsg.Text = "ERROR " + er.ToString();
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            string username = ddlUserName.SelectedValue;
            string password = tbxPassword.Text;
            MembershipUser mu = Membership.GetUser(username);
            mu.ChangePassword(mu.ResetPassword(), password);

            msgLabel.Attributes.Add("class", "msg_success");
            msgLabel.Text = "Password Change Successfuly.";
        }
        catch (Exception er)
        {

            msgLabel.Attributes.Add("class", "msg_warning");
            msgLabel.Text = "ERROR " + er.ToString();
        }
        finally
        {
            tbxPassword.Text = "";
            ddlUserName.SelectedValue = "0";
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string query = "UPDATE Settings SET FromDate='" + Convert.ToDateTime(tbxFromDate.Text) + "',Todate='" + Convert.ToDateTime(tbxToDate.Text) + "' Where ID='1'";
            DatabaseGateway.DatabaseManager.GetInstance().ExecuteCommand(query);
            lblMessage.Text = "Updated.";
            lblMessage.Attributes.Add("class", "msg_success");
            LoadSettingsData("1");
        }
        catch (Exception er)
        {
            lblMessage.Text = er.ToString();
            lblMessage.Attributes.Add("class", "msg_error");
        }

    }
    private decimal PerMeal()
    {
        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT ISNULL(Sum(Mill),0) as TotalMill FROM MillEntry Where (Date >= '" + tbxFromDate.Text + "') AND  (Date <= '" + tbxToDate.Text + "')");
        decimal totalMil = 0;
        if (dt.Rows.Count > 0)
        {
            totalMil = Convert.ToDecimal(dt.Rows[0]["TotalMill"]);

        }
        DataTable dataTable = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT ISNULL(Sum(Amount),0) as TotalAmount FROM Expenses Where (ExpDate >= '" + tbxFromDate.Text + "') AND  (ExpDate <= '" + tbxToDate.Text + "')");
        decimal totalExpenses = 0;
        if (dataTable.Rows.Count > 0)
        {
            totalExpenses = Convert.ToDecimal(dataTable.Rows[0]["TotalAmount"]);

        }

        decimal perMill = (decimal)0.0;
        if (totalExpenses > 0 && totalMil > 0)
        {
            perMill = totalExpenses / totalMil;
        }

        return perMill;

    }
    private DataTable GetData()
    {
        DataTable dt1 = new DataTable();

        dt1.Columns.Add(new DataColumn("Name", typeof(string)));
        dt1.Columns.Add(new DataColumn("Email", typeof(string)));
        dt1.Columns.Add(new DataColumn("Deposit", typeof(string)));
        dt1.Columns.Add(new DataColumn("Mill", typeof(string)));
        dt1.Columns.Add(new DataColumn("Expense", typeof(string)));
        dt1.Columns.Add(new DataColumn("NetPay", typeof(string)));

        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT Name,Mobile,Email FROM Member Where Status='Active'");
        foreach (DataRow dr in dt.Rows)
        {
            string name = dr["Name"].ToString();
            string email = dr["Email"].ToString();
            DataTable dtAmount = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT ISNULL(Sum(Amount),0) as TotalAmount FROM Deposit Where Name='" + name + "' AND (Date >= '" + tbxFromDate.Text + "') AND  (Date <= '" + tbxToDate.Text + "')");
            decimal totalDeposit = (decimal)0.0;
            if (dtAmount.Rows.Count > 0)
            {
                totalDeposit = Convert.ToDecimal(dtAmount.Rows[0]["TotalAmount"]);
            }
            DataTable dtMill = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT ISNULL(Sum(Mill),0) as TotalMill FROM MillEntry Where Name='" + name + "' AND (Date >= '" + tbxFromDate.Text + "') AND  (Date <= '" + tbxToDate.Text + "')");
            decimal totalMill = (decimal)0.0;
            if (dtMill.Rows.Count > 0)
            {
                totalMill = Convert.ToDecimal(dtMill.Rows[0]["TotalMill"]);
            }
            decimal totalExpenses = totalMill * PerMeal();
            decimal balance = totalDeposit - totalExpenses;

            DataRow dr1 = dt1.NewRow();
            dr1["Name"] = name;
            dr1["Email"] = email;
            dr1["Deposit"] = totalDeposit.ToString("0.00");
            dr1["Mill"] = totalMill.ToString();
            dr1["Expense"] = totalExpenses.ToString("0.00");
            dr1["NetPay"] = balance.ToString("0.00");
            dt1.Rows.Add(dr1);

        }
        return dt1;


    }
    protected void btnNotify_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = GetData();
            foreach (DataRow item in dt.Rows)
            {
                string subject = "Meal Report of " + Convert.ToDateTime(tbxFromDate.Text).ToString("MMMM") + " - " + Convert.ToDateTime(tbxFromDate.Text).Year.ToString();
                string body = "Dear " + item["Name"].ToString() + ",<br>";
                body += "Meal Report of " + Convert.ToDateTime(tbxFromDate.Text).ToString("MMMM") + " - " + Convert.ToDateTime(tbxFromDate.Text).Year.ToString() + ":<br>";
                body += "<b>Total Meal:</b> " + item["Name"].ToString() + "<br>";
                body += "<b>Total Deposit:</b> " + item["Deposit"].ToString() + " Taka<br>";
                body += "<b>Total Expense:</b> " + item["Expense"].ToString() + " Taka<br>";
                body += "<b>Net Payment:</b> " + item["NetPay"].ToString() + " Taka<br><br>";
                body += "Thanks";

                Notify.SendSimpleMail(item["Email"].ToString(), subject, body);



            }

            lblNotify.Attributes.Add("class", "msg_success");
            lblNotify.Text = "Report Send Successfuly.";
        }
        catch (Exception er)
        {

            lblNotify.Text = er.ToString();
            lblNotify.Attributes.Add("class", "msg_error");
        }

    }
    private void LoadDefault()
    {
        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT ConfigID, DisplayName, DisplayEmail, ReplyToEmail, SMTPServer, Port, SSL, Authentication, UserName, Password, IsEmailSent FROM EmailConfig where ConfigID='1'");
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0]["SSL"].ToString() != "")
                chkSSL.Checked = Convert.ToBoolean(dt.Rows[0]["SSL"].ToString());
            if (dt.Rows[0]["Authentication"].ToString() != "")
                chkAuthentication.Checked = Convert.ToBoolean(dt.Rows[0]["Authentication"].ToString());
            tbxDisplayName.Text = dt.Rows[0]["DisplayName"].ToString();
            tbxDisplayEmail.Text = dt.Rows[0]["DisplayEmail"].ToString();
            tbxReplyToEmail.Text = dt.Rows[0]["ReplyToEmail"].ToString();
            tbxServer.Text = dt.Rows[0]["SMTPServer"].ToString();
            tbxPort.Text = dt.Rows[0]["Port"].ToString();
            tbxUserName.Text = dt.Rows[0]["UserName"].ToString();
            txtPassword.Attributes.Add("value", dt.Rows[0]["Password"].ToString());
            //txtPassword.Text= dt.Rows[0]["Password"].ToString();
            if (chkAuthentication.Checked)
                pnlEmail.Visible = true;
            else
                pnlEmail.Visible = false;
            if (dt.Rows[0]["Password"].ToString() != "")
                chkIsEmailSent.Checked = Convert.ToBoolean(dt.Rows[0]["IsEmailSent"].ToString());
        }
    }
    protected void btnConfig_Click(object sender, EventArgs e)
    {
        try
        {
            bool ssl = false;
            bool auth = false;
            bool isEmail = false;
            if (chkSSL.Checked)
            {
                ssl = true;
            }
            if (chkAuthentication.Checked)
            {
                auth = true;
            }
            if (chkIsEmailSent.Checked)
            {
                isEmail = true;
            }

            string query = "UPDATE EmailConfig SET DisplayName='" + tbxDisplayName.Text + "',DisplayEmail='" + tbxDisplayEmail.Text + "', ReplyToEmail='" + tbxReplyToEmail.Text + "',SMTPServer='" + tbxServer.Text + "',Port='" + tbxPort.Text + "',SSL='" + ssl + "',Authentication='" + auth + "',UserName='" + tbxUserName.Text + "',Password='" + txtPassword.Text + "',IsEmailSent='" + isEmail + "' Where ConfigID='1'";
            DatabaseGateway.DatabaseManager.GetInstance().ExecuteCommand(query);
            lblEmailConfig.Text = "Update Succesfully.";
            lblEmailConfig.Attributes.Add("class", "msg_success");
            LoadDefault();
        }
        catch (Exception er)
        {
            lblEmailConfig.Text = er.ToString();
            lblEmailConfig.Attributes.Add("class", "msg_error");
        }
    }

    protected void chkAuthentication_CheckedChanged(object sender, EventArgs e)
    {
        if (chkAuthentication.Checked)
            pnlEmail.Visible = true;
        else
            pnlEmail.Visible = false;
    }
}