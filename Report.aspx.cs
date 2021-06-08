using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSettingsData("1");
            PerMill();                
            LoadGrid();
            CreditDebit();
            LoadData();
        }
    }
    private decimal deposit = (decimal)0.0;
    private decimal mill = (decimal)0.0;
    private decimal expense = (decimal)0.0;
    private decimal netPayment = (decimal)0.0;
    private decimal credit = (decimal)0.0;
    private void LoadSettingsData(string id)
    {
        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT Id, FromDate, Todate FROM Settings WHERE Id='" + id + "'");
        if (dt.Rows.Count > 0)
        {
            hdnFromDate.Value = Convert.ToDateTime(dt.Rows[0]["FromDate"]).ToString("yyyy-MM-dd");
            hdnToDate.Value = Convert.ToDateTime(dt.Rows[0]["Todate"]).ToString("yyyy-MM-dd");

            ltrMonth.Text = Convert.ToDateTime(dt.Rows[0]["FromDate"]).ToString("MMMM")+" - " + Convert.ToDateTime(dt.Rows[0]["FromDate"]).Year.ToString();
            

        }
    }
   
    private void LoadGrid()
    {
        DataTable dt1 = new DataTable();

        dt1.Columns.Add(new DataColumn("Name", typeof(string)));
        dt1.Columns.Add(new DataColumn("Deposit", typeof(string)));
        dt1.Columns.Add(new DataColumn("Mill", typeof(string)));
        dt1.Columns.Add(new DataColumn("Expense", typeof(string)));
        dt1.Columns.Add(new DataColumn("NetPay", typeof(string)));

        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT Name FROM Member Where Status='Active'");
        foreach (DataRow dr in dt.Rows)
        {
            string name = dr["Name"].ToString();
            DataTable dtAmount = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT ISNULL(Sum(Amount),0) as TotalAmount FROM Deposit Where Name='" + name + "' AND (Date >= '" + hdnFromDate.Value + "') AND  (Date <= '" + hdnToDate.Value + "')");
            decimal totalDeposit = (decimal)0.0;
            if (dtAmount.Rows.Count > 0)
            {
                totalDeposit = Convert.ToDecimal(dtAmount.Rows[0]["TotalAmount"]);
            }
            DataTable dtMill = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT ISNULL(Sum(Mill),0) as TotalMill FROM MillEntry Where Name='" + name + "' AND (Date >= '" + hdnFromDate.Value + "') AND  (Date <= '" + hdnToDate.Value + "')");
            decimal totalMill = (decimal)0.0;
            if (dtMill.Rows.Count > 0)
            {
                totalMill = Convert.ToDecimal(dtMill.Rows[0]["TotalMill"]);
            }
            decimal totalExpenses = totalMill * Convert.ToDecimal(hdnPermil.Value);
            decimal balance = totalDeposit - totalExpenses;

            DataRow dr1 = dt1.NewRow();
            dr1["Name"] = name;
            dr1["Deposit"] = totalDeposit.ToString("0.00");
            dr1["Mill"] = totalMill.ToString();
            dr1["Expense"] = totalExpenses.ToString("0.00");
            dr1["NetPay"] = balance.ToString("0.00");
            dt1.Rows.Add(dr1);
            
        }

        gvTotalReport.DataSource = dt1;
        gvTotalReport.DataBind();
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
            credit = totalExpenses;
        }

        decimal perMill = (decimal)0.0;
        if (totalExpenses > 0 && totalMil > 0)
        {
            perMill = totalExpenses / totalMil;
        }
        
        hdnPermil.Value = perMill.ToString();
        lblPermil.Text = "Meal Rate: " + perMill.ToString("0.00") + " ৳";
        lblMill.Text = "Total Meals: " + totalMil.ToString();
    }

    private void CreditDebit()
    {
        
        lblDebit.Text = totalDeposit.ToString("0.00") + " ৳";      
        lblCredit.Text = credit.ToString("0.00") + " ৳";  
        lblBalance.Text = (totalDeposit - credit).ToString("0.00") + " ৳";      
              
    }

    private decimal totalDeposit = (decimal)0.0;
    private decimal totalMill = (decimal)0.0;
    private decimal totalExpense = (decimal)0.0;
    private decimal totalNetPay = (decimal)0.0;
    protected void gvTotalReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            totalDeposit += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Deposit"));
            totalMill += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Mill"));
            totalExpense += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Expense"));
            totalNetPay += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NetPay"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "Total: ";
            e.Row.Cells[2].Text = Convert.ToString(totalDeposit)+ " ৳";
            e.Row.Cells[3].Text = Convert.ToString(totalMill);
            e.Row.Cells[4].Text = Convert.ToString(totalExpense) + " ৳";
            e.Row.Cells[5].Text = Convert.ToString(totalNetPay) + " ৳";
        }
    }
    private void LoadData()
    {
        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT Id,ExpensesBy, ExpDate, Amount FROM Expenses Where (ExpDate >= '" + hdnFromDate.Value + "') AND  (ExpDate <= '" + hdnToDate.Value + "') Order By ExpDate desc");
        gvMillHistory.DataSource = dt;
        gvMillHistory.EmptyDataText = "No Data Found!";
        gvMillHistory.DataBind();
    }
    protected void gvMillHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMillHistory.PageIndex = e.NewPageIndex;
        LoadData();
        gvMillHistory.PageIndex = e.NewPageIndex;
    }
    private decimal total = (decimal)0.0;
    protected void gvMillHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amount"));
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[2].Text = "Total: ";
            e.Row.Cells[3].Text = Convert.ToString(total);
        }
    }

}