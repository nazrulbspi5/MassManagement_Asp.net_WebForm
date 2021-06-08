﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Expenses : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string name = User.Identity.Name;
            if (name == "")
            {
                Response.Redirect("~/Account/Login?ReturnUrl=Expenses");
            }
            if (!User.IsInRole("admin") && !User.IsInRole("systemAdmin"))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Account/Login");
            }
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

    private void LoadData()
    {
        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT Id,ExpensesBy, ExpDate, Amount FROM Expenses Where Id<>0 AND  (ExpDate >= '" + hdnFromDate.Value + "') AND  (ExpDate <= '" + hdnToDate.Value + "') Order By ExpDate desc");
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
                string query = "INSERT INTO Expenses (ExpensesBy,ExpDate,Amount) VALUES('" + ddlMember.SelectedValue + "','" + Convert.ToDateTime(tbxDate.Text) + "','" + Convert.ToDecimal(tbxAmount.Text) + "')";
                DatabaseGateway.DatabaseManager.GetInstance().ExecuteCommand(query);
                msgLabel.Text = "Expense Saved.";
                msgLabel.Attributes.Add("class", "msg_success");
                tbxAmount.Text = "";
                ddlMember.SelectedValue = "0";
               

            }
            else
            {
                string query = "UPDATE Expenses SET ExpensesBy='" + ddlMember.SelectedValue + "',ExpDate='" + Convert.ToDateTime(tbxDate.Text) + "',Amount='" + Convert.ToDecimal(tbxAmount.Text) + "' Where ID='" + Session["Id"] + "'";
                DatabaseGateway.DatabaseManager.GetInstance().ExecuteCommand(query);
                msgLabel.Text = "Expense Updated.";
                msgLabel.Attributes.Add("class", "msg_success");
                tbxAmount.Text = "";
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
        DataTable dt = DatabaseGateway.DatabaseManager.GetInstance().GetDataTable("SELECT ExpensesBy, ExpDate, Amount FROM Expenses WHERE Id='" + id + "'");
        if (dt.Rows.Count > 0)
        {
            ddlMember.SelectedValue = dt.Rows[0]["ExpensesBy"].ToString();
            tbxDate.Text = Convert.ToDateTime(dt.Rows[0]["ExpDate"]).ToString("dd-MMM-yyyy");
            tbxAmount.Text = dt.Rows[0]["Amount"].ToString();
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

    protected void gvMillHistory_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        Label lblItemCode = gvMillHistory.Rows[index].FindControl("Label1") as Label;

        string query = "DELETE FROM Expenses WHERE Id = '" + lblItemCode.Text + "'";
        DatabaseGateway.DatabaseManager.GetInstance().ExecuteCommand(query);
        msgLabel.Text = "Data Deleted!";
        msgLabel.Attributes.Add("class", "msg_success");
        LoadData();
    }
}