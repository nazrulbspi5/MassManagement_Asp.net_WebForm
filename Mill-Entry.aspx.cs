using System;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mill_Entry : System.Web.UI.Page
{
    private string monthWithYear = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string name = User.Identity.Name;
            if (name=="")
            {
                Response.Redirect("~/Account/Login?ReturnUrl=Mill-Entry");
            }
            if (!User.IsInRole("admin") && !User.IsInRole("systemAdmin"))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Account/Login");
            }
            int dateM = DateTime.Now.Month;
            string dateY = DateTime.Now.Year.ToString();
            string month = GetMonth(dateM);

            monthWithYear =  month + "-" + dateY;

            tbxDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            LoadSettingsData("1");
            LoadMember();
            LoadData();
            PerMill();
        }
    }
    private void LoadSettingsData(string id)
    {
        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT Id, FromDate, Todate FROM Settings WHERE Id='" + id + "'");
        if (dt.Rows.Count > 0)
        {
            hdnFromDate.Value = Convert.ToDateTime(dt.Rows[0]["FromDate"]).ToString("yyyy-MM-dd");
            hdnToDate.Value = Convert.ToDateTime(dt.Rows[0]["Todate"]).ToString("yyyy-MM-dd");

        }
    }
    private string GetMonth(int dateM)
    {


        switch (dateM)
        {
            case 1:
                return "Jan";
            case 2:
                return "Feb";
            case 3:
                return "Mar";
            case 4:
                return "Apr";
            case 5:
                return "May";
            case 6:
                return "Jun";
            case 7:
                return "Jul";
            case 8:
                return "Aug";
            case 9:
                return "Sep";
            case 10:
                return "Oct";
            case 11:
                return "Nov";
            default:
                return "Dec";
        }
    }

    private void LoadMember()
    {
        ddlMember.Items.Clear();
        ddlMember.Items.Insert(0, new ListItem("---Select---", "0"));
        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT * FROM Member Where Status='Active'");
        ddlMember.DataSource = dt;
        ddlMember.DataBind();
        if (ddlMember.Items.FindByText("---Select---") != null)
        {
            ddlMember.Items.FindByText("---Select---").Selected = true;
        }
    }

    private void PerMill()
    {

        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT ISNULL(Sum(Mill),0) as TotalMill FROM MillEntry Where (Date >= '" + hdnFromDate.Value + "') AND  (Date <= '" + hdnToDate.Value + "')");
        decimal totalMil = 0;
        if (dt.Rows.Count > 0)
        {
            totalMil = Convert.ToDecimal(dt.Rows[0]["TotalMill"]);

        }
        DataTable dataTable = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT ISNULL(Sum(Amount),0) as TotalAmount FROM Expenses Where (ExpDate >= '" + hdnFromDate.Value + "') AND  (ExpDate <= '" + hdnToDate.Value + "')");
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
        lblPermil.Text = "Meal Rate: " + perMill.ToString("0.00") + " ৳";
        lblMill.Text = "Total Meals: " + totalMil.ToString();
    }


    private void LoadData()
    {
        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT Id,Name, Date, Mill FROM MillEntry Where Date='" + Convert.ToDateTime(tbxDate.Text) + "' AND (Date >= '" + hdnFromDate.Value + "') AND  (Date <= '" + hdnToDate.Value + "')");
        gvMillHistory.DataSource = dt;
        gvMillHistory.EmptyDataText = "No Data Found!";
        gvMillHistory.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnSave.Text != "Update")
            {
                DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("Select Id From MillEntry Where Name='" + ddlMember.SelectedValue + "' AND Date='" + Convert.ToDateTime(tbxDate.Text) + "'");
                if (dt.Rows.Count > 0)
                {
                    msgLabel.Text = "Your meal already saved!";
                    msgLabel.Attributes.Add("class", "msg_warning");
                }
                else
                {
                    string query = "INSERT INTO MillEntry (Name,Date,Mill) VALUES('" + ddlMember.SelectedValue + "','" + Convert.ToDateTime(tbxDate.Text) + "','" + Convert.ToDecimal(tbxMill.Text) + "')";
                    DatabaseGateway.DatabaseManager.GetInstance().ExecuteCommand(query);
                    msgLabel.Text = "Meal Saved.";
                    msgLabel.Attributes.Add("class", "msg_success");
                    tbxMill.Text = "";
                    ddlMember.SelectedValue = "0";
                }

            }
            else
            {
                string query = "UPDATE MillEntry SET Name='" + ddlMember.SelectedValue + "',Date='" + Convert.ToDateTime(tbxDate.Text) + "',Mill='" + Convert.ToDecimal(tbxMill.Text) + "' Where ID='" + Session["Id"] + "'";
                DatabaseGateway.DatabaseManager.GetInstance().ExecuteCommand(query);
                msgLabel.Text = "Meal Updated.";
                msgLabel.Attributes.Add("class", "msg_success");
                tbxMill.Text = "";
                ddlMember.SelectedValue = "0";
                btnSave.Text = "Save";
                ddlMember.Enabled = true;
                tbxDate.Enabled = true;
            }

        }
        catch (Exception er)
        {
            msgLabel.Text = er.ToString();
            msgLabel.Attributes.Add("class", "msg_error");
        }
        finally
        {
            LoadData();
            PerMill();
        }
    }

    protected void gvMillHistory_SelectedIndexChanged(object sender, EventArgs e)
    {
        int index = Convert.ToInt32(gvMillHistory.SelectedIndex);
        Label lblItemName = gvMillHistory.Rows[index].FindControl("Label1") as Label;
        EditMode(lblItemName.Text);
    }

    private void EditMode(string id)
    {
        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT Name, Date, Mill FROM MillEntry WHERE Id='" + id + "'");
        if (dt.Rows.Count > 0)
        {
            ddlMember.SelectedValue = dt.Rows[0]["Name"].ToString();
            tbxDate.Text = Convert.ToDateTime(dt.Rows[0]["Date"]).ToString("dd-MMM-yyyy");
            tbxMill.Text = dt.Rows[0]["Mill"].ToString();
            btnSave.Text = "Update";
            msgLabel.Text = "Edit Mode Activated!";
            msgLabel.Attributes.Add("class", "msg_info");
            Session["Id"] = id;
            ddlMember.Enabled = false;
            tbxDate.Enabled = false;
        }


    }

    protected void tbxDate_TextChanged(object sender, EventArgs e)
    {
        LoadData();
        PerMill();
    }
    private decimal total = (decimal)0.0;
    protected void gvMillHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Mill"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total Meals: ";
            e.Row.Cells[3].Text = Convert.ToString(total);
        }
    }
}