<%@ Page Title="Meal Entry" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Mill-Entry.aspx.cs" Inherits="Mill_Entry" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">

   <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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

             <asp:HiddenField ID="hdnFromDate" runat="server" />
                <asp:HiddenField ID="hdnToDate" runat="server" />
            <div class="row">
                <div class="col-md-6">

                    <div class="form-horizontal">
                        <div class="Card-header">
                            <strong>Meal Entry</strong>
                        </div>
                        <div class="Card-Body">
                            <div class="form-group">

                                <div class="col-md-12">
                                    <asp:Label ID="msgLabel" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Member Name</label>
                                <div class="col-md-9">
                                    <asp:DropDownList ID="ddlMember" AppendDataBoundItems="true" DataValueField="Name" DataTextField="Name" CssClass="form-control" runat="server">
                                        <asp:ListItem Value="0">---Select---</asp:ListItem>
                                       
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlMember" InitialValue="0"
                                        CssClass="text-danger" ErrorMessage="Select Member." />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Date</label>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="tbxDate" AutoPostBack="true" OnTextChanged="tbxDate_TextChanged" CssClass="form-control" />
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                        Enabled="True" TargetControlID="tbxDate"></ajax:CalendarExtender>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxDate" CssClass="text-danger" ErrorMessage="Enter Date." />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Meal</label>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="tbxMill" CssClass="form-control" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxMill" CssClass="text-danger" ErrorMessage="Enter Mill." />
                                    <ajax:FilteredTextBoxExtender ID="txtOpBalance_FilteredTextBoxExtender"
                                        runat="server" Enabled="True" FilterType="Custom, Numbers" ValidChars="0123456789." TargetControlID="tbxMill"></ajax:FilteredTextBoxExtender>

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

                <div class="col-md-6">
                    <div class="form-horizontal">
                        <div class="Card-header">
                            <strong>Meal History</strong>
                        </div>
                        <div class="Card-Body">
                            <div class="table-responsive">
                                <asp:GridView ID="gvMillHistory" DataKeyNames="ID" ShowFooter="true" OnRowDataBound="gvMillHistory_RowDataBound" OnSelectedIndexChanged="gvMillHistory_SelectedIndexChanged" runat="server" CssClass="table table-bordered table-striped"
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
                                                <%#Convert.ToDateTime(Eval("Date")).ToString("dd-MMM-yyyy") %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Mill" HeaderText="Meal" SortExpression="Mill">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select" ImageUrl="Content/Images/btnedit.gif" Text="Select" />
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Center" />
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

