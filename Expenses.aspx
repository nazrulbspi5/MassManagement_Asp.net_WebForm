<%@ Page Title="Expense Entry" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Expenses.aspx.cs" Inherits="Expenses" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=15.1.4.0, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

            <div class="row">
                <div class="col-md-12">

                    <div class="form-horizontal">
                        <div class="Card-header">
                            <strong>Report</strong>
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
                <div class="col-md-6">

                    <div class="form-horizontal">
                        <div class="Card-header">
                            <strong>Expense Entry</strong>
                        </div>
                        <div class="Card-Body">
                            <div class="form-group">

                                <div class="col-md-12">
                                    <asp:Label ID="msgLabel" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group">
                                <label class="col-md-3 control-label">Expense Date</label>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="tbxDate" AutoPostBack="true" OnTextChanged="tbxDate_TextChanged" CssClass="form-control" />
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                        Enabled="True" TargetControlID="tbxDate"></ajax:CalendarExtender>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxDate" CssClass="text-danger" ErrorMessage="Enter Date." />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Expense By</label>
                                <div class="col-md-9">
                                    <asp:DropDownList ID="ddlMember" AppendDataBoundItems="true" DataTextField="Name" DataValueField="Name" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">---Select---</asp:ListItem>

                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlMember" InitialValue="0"
                                        CssClass="text-danger" ErrorMessage="Select Member." />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Amount</label>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="tbxAmount" CssClass="form-control" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxAmount" CssClass="text-danger" ErrorMessage="Enter Amount." />
                                    <ajax:FilteredTextBoxExtender ID="txtOpBalance_FilteredTextBoxExtender"
                                        runat="server" Enabled="True" FilterType="Custom, Numbers" ValidChars="0123456789." TargetControlID="tbxAmount"></ajax:FilteredTextBoxExtender>

                                </div>
                            </div>

                            <div class="form-group">
                                <div class="col-md-offset-3 col-md-10">
                                    <asp:Button runat="server" Text="Save" CssClass="btn btn-primary" ID="btnSave" OnClick="btnSave_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                 <asp:HiddenField ID="hdnFromDate" runat="server" />
                <asp:HiddenField ID="hdnToDate" runat="server" />
                <div class="col-md-6">
                    <div class="form-horizontal">
                        <div class="Card-header">
                            <strong>Expense History</strong>
                        </div>
                        <div class="Card-Body">
                            <div class="table-responsive">
                                <asp:GridView ID="gvMillHistory" DataKeyNames="ID" ShowFooter="true" OnRowDataBound="gvMillHistory_RowDataBound" OnRowDeleting="gvMillHistory_OnRowDeleting" OnSelectedIndexChanged="gvMillHistory_SelectedIndexChanged" runat="server" CssClass="table table-bordered table-striped"
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

                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select" ImageUrl="Content/Images/btnedit.gif" Text="Select" />
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                               
                                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="Content/Images/btndelete.gif" Text="Delete" ToolTip="Delete" />

                                                <asp:ConfirmButtonExtender TargetControlID="ImageButton2" ID="confBtnDelete" runat="server" DisplayModalPopupID="ModalPopupExtender1"></asp:ConfirmButtonExtender>
                                                <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="ImageButton2"
                                                    PopupControlID="PNL" OkControlID="ButtonOk" CancelControlID="ButtonCancel" BackgroundCssClass="modalBackground" />
                                                <asp:Panel ID="PNL" runat="server" Style="display: none; width: 300px; background-color: White; border-width: 2px; border-color: Black; border-style: solid; padding: 20px;">
                                                    <b style="color: red">Item will be deleted permanently!</b><br />
                                                    <br />
                                                    Are you sure to delete the item from list?
                                                            <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <div style="text-align: right;">
                                                        <asp:Button ID="ButtonOk" CausesValidation="False" CssClass="btn btn-default" runat="server" Text="OK" />
                                                        <asp:Button ID="ButtonCancel" CssClass="btn btn-default" CausesValidation="False" runat="server" Text="Cancel" />
                                                    </div>
                                                </asp:Panel>

                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle HorizontalAlign="Center" Font-Bold="True"></FooterStyle>
                                </asp:GridView>

                            </div>
                        </div>
                    </div>
                </div>
            </div>




       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

