using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mill_History : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            int dateM = DateTime.Now.Month;
            string dateY = DateTime.Now.Year.ToString();
            string month = GetMonth(dateM);
           
            tbxFormDate.Text = "01" + "-" + month + "-" + dateY;

            tbxToDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
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
        ddlMember.Items.Insert(0, new ListItem("---All---", "0"));
        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT * FROM Member Where Status='Active'");
        ddlMember.DataSource = dt;
        ddlMember.DataBind();
        if (ddlMember.Items.FindByText("---All---") != null)
        {
            ddlMember.Items.FindByText("---All---").Selected = true;
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
        string criteria = "";
        if (ddlMember.SelectedValue != "0")
        {
            criteria = "AND (Name='" + ddlMember.SelectedValue + "')";
        }
        string formDate = "";
        if (tbxFormDate.Text != "")
        {
            try
            {
                formDate = Convert.ToDateTime(tbxFormDate.Text).ToString("yyyy-MM-dd");
                criteria += " AND  (Date >= '" + formDate + "')";
            }
            catch (Exception)
            {

                throw;
            }

        }
        string toDate = "";
        if (tbxToDate.Text != "")
        {
            try
            {
                toDate = Convert.ToDateTime(tbxToDate.Text).ToString("yyyy-MM-dd");
                criteria += " AND  (Date <= '" + toDate + "')";
            }
            catch (Exception)
            {

                throw;
            }

        }
        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT Id,Name, Date, Mill FROM MillEntry Where Id<>0" + criteria + "Order By Date DESC");
        gvMillHistory.DataSource = dt;
        gvMillHistory.EmptyDataText = "No Data Found!";
        gvMillHistory.DataBind();
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

    protected void btnShow_Click(object sender, EventArgs e)
    {
        LoadData();
    }

    protected void ddlMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }


    protected void gvMillHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMillHistory.PageIndex = e.NewPageIndex;
        LoadData();
        gvMillHistory.PageIndex = e.NewPageIndex;
    }
}