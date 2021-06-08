<%@ Page Title="Report" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <style>
        th, td, thead, tbody {
            border: 1px solid #ddd;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="hdnPermil" runat="server" />
    <div class="row">
        <div class="col-md-12">

            <div class="form-horizontal">
                <div class="Card-header">
                    <strong>
                        <asp:Literal ID="ltrMonth" runat="server"></asp:Literal></strong>
                </div>
                <div class="Card-Body">
                    <div class="form-group">
                        <div class="col-md-6">
                            <div class="alert alert-success">
                                <asp:Label ID="lblPermil" Style="font-size: 20px; font-weight: bold" runat="server"></asp:Label>
                            </div>

                        </div>
                        <div class="col-md-6">
                            <div class="alert alert-success">
                                <asp:Label ID="lblMill" Style="font-size: 20px; font-weight: bold" runat="server"></asp:Label>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
         <asp:HiddenField ID="hdnFromDate" runat="server" />
                <asp:HiddenField ID="hdnToDate" runat="server" />
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="form-horizontal">
                <div class="Card-header">
                    <strong>Total Report</strong>
                </div>
                <div class="Card-Body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvTotalReport" ShowFooter="true" OnRowDataBound="gvTotalReport_RowDataBound" runat="server" CssClass="table table-bordered table-striped"
                            AutoGenerateColumns="False" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="#SL">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <%--<asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                       <%#Convert.ToDateTime(Eval("Date")).ToString("dd-MMM-yyyy") %>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Deposit" HeaderText="Deposit" SortExpression="Deposit">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Mill" HeaderText="Meals" SortExpression="Mill">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Expense" HeaderText="Expense" SortExpression="Expense">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NetPay" HeaderText="Net Payment" SortExpression="NetPay">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>

                            </Columns>
                            <FooterStyle HorizontalAlign="Center" Font-Bold="True"></FooterStyle>
                            <HeaderStyle BackColor="#4f8a10" ForeColor="White" />
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">

            <div class="form-horizontal">
                <div class="Card-header">
                    <strong>Debit/Credit</strong>
                </div>
                <div class="Card-Body">
                    <table class="table">
                        <thead>
                            <tr>
                                <th>Debit</th>
                                <th>Credit</th>
                                <th>Balance</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDebit" runat="server"></asp:Label>

                                </td>
                                <td>
                                    <asp:Label ID="lblCredit" runat="server"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblBalance" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
         <div class="col-md-6">
                    <div class="form-horizontal">
                        <div class="Card-header">
                            <strong>Expense History</strong>
                        </div>
                        <div class="Card-Body">
                            <div class="table-responsive">
                                <asp:GridView ID="gvMillHistory" AllowPaging="true" OnPageIndexChanging="gvMillHistory_PageIndexChanging" PageSize="10" DataKeyNames="ID" ShowFooter="true" OnRowDataBound="gvMillHistory_RowDataBound" runat="server" CssClass="table table-bordered table-striped"
                                    AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#SL">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:Label ID="Label1" Visible="false" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <%#Convert.ToDateTime(Eval("ExpDate")).ToString("dd-MMM-yyyy") %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ExpensesBy" HeaderText="Expenses By" SortExpression="ExpensesBy">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>

                                        
                                    </Columns>
                                    <FooterStyle HorizontalAlign="Center" Font-Bold="True"></FooterStyle>
                                </asp:GridView>

                            </div>
                        </div>
                    </div>
                </div>
    </div>
</asp:Content>

