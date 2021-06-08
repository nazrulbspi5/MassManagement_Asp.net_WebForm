<%@ Page Title="Maintenance" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Maintenance.aspx.cs" Inherits="Maintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

    <div class="row">
        <div class="col-md-6">

            <div class="form-horizontal">
                <div class="Card-header">
                    <strong>Reset Password</strong>
                </div>
                <div class="Card-Body">
                    <div class="form-group">

                        <div class="col-md-12">
                            <asp:Label ID="msgLabel" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">User Name</label>
                        <div class="col-md-9">
                            <asp:DropDownList ID="ddlUserName" runat="server" CssClass="form-control" AppendDataBoundItems="true"></asp:DropDownList>
                            <asp:RequiredFieldValidator runat="server" InitialValue="0" ControlToValidate="ddlUserName"
                                CssClass="text-danger" ErrorMessage="Select User Name." />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">Password</label>
                        <div class="col-md-9">
                            <asp:TextBox ID="tbxPassword" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="tbxPassword"
                                CssClass="text-danger" ErrorMessage="Enter Password." /><br />
                            <asp:RegularExpressionValidator CssClass="text-danger" ID="RegularExpressionValidator1" ControlToValidate="tbxPassword" Display="Dynamic" runat="server" ValidationExpression=".{5,}"
                                ErrorMessage="Password' must contain minimum 5 characters"></asp:RegularExpressionValidator>
                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-md-offset-3 col-md-10">
                            <asp:Button runat="server" Text="Reset" CssClass="btn btn-primary" ID="btnReset" OnClick="btnReset_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-6">

            <div class="form-horizontal">
                <div class="Card-header">
                    <strong>Clear Database</strong>
                </div>
                <div class="Card-Body">
                    <div class="form-group">
                        <div class="col-md-12">
                            <div id="lblAlert" runat="server">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <asp:Button ID="btnClear" CausesValidation="false" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnClear_Click" Text="Clear Database" />
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
                    <strong>Setting Date</strong>
                </div>
                <div class="Card-Body">
                    <div class="form-group">

                        <div class="col-md-12">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">From Date</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="tbxFromDate" CssClass="form-control" />
                            <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                Enabled="True" TargetControlID="tbxFromDate"></ajax:CalendarExtender>
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="Save" ControlToValidate="tbxFromDate" CssClass="text-danger" ErrorMessage="Enter to date." />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">To Date</label>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" ID="tbxToDate" CssClass="form-control" />
                            <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                Enabled="True" TargetControlID="tbxToDate"></ajax:CalendarExtender>
                            <asp:RequiredFieldValidator runat="server" ValidationGroup="Save" ControlToValidate="tbxToDate" CssClass="text-danger" ErrorMessage="Enter to date." />
                        </div>
                    </div>


                    <div class="form-group">
                        <div class="col-md-offset-3 col-md-10">
                            <asp:Button runat="server" Text="Save" ValidationGroup="Save" CssClass="btn btn-primary" ID="btnSave" OnClick="btnSave_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-6">

            <div class="form-horizontal">
                <div class="Card-header">
                    <strong>Send Email</strong>
                </div>
                <div class="Card-Body">
                    <div class="form-group">
                        <div class="col-md-12">
                            <div id="Div1" runat="server">
                                <asp:Label ID="lblNotify" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-12">
                            <asp:Button ID="btnNotify" CausesValidation="false" runat="server" CssClass="btn btn-primary btn-block" OnClick="btnNotify_Click" Text="Send" />
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
   
    <div class="row">
        <div class="col-md-12">

            <div class="form-horizontal">
                <div class="Card-header">
                    <strong>Email Config</strong>
                </div>
                <div class="Card-Body">
                    <div class="form-group">

                        <div class="col-md-12">
                            <asp:Label ID="lblEmailConfig" runat="server"></asp:Label>
                        </div>

                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">Email Enable?:</label>
                        <div class="col-md-9">
                            <asp:CheckBox runat="server" ID="chkIsEmailSent"></asp:CheckBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">Display Name</label>
                        <div class="col-md-9">
                            <asp:TextBox CssClass="form-control" runat="server" ID="tbxDisplayName"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">Display Email</label>
                        <div class="col-md-9">
                            <asp:TextBox CssClass="form-control" runat="server" ID="tbxDisplayEmail"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">Reply Email</label>
                        <div class="col-md-9">
                            <asp:TextBox CssClass="form-control" runat="server" ID="tbxReplyToEmail"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">SMTP Server:</label>
                        <div class="col-md-9">
                            <asp:TextBox CssClass="form-control" runat="server" ID="tbxServer"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">Port</label>
                        <div class="col-md-9">
                            <asp:TextBox CssClass="form-control" runat="server" ID="tbxPort"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">SSL Enable</label>
                        <div class="col-md-9">
                            <asp:CheckBox ID="chkSSL" runat="server" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label">Authentication</label>
                        <div class="col-md-9">
                            <asp:CheckBox ID="chkAuthentication" runat="server" AutoPostBack="true" OnCheckedChanged="chkAuthentication_CheckedChanged" />
                        </div>
                    </div>
                    <asp:Panel runat="server" ID="pnlEmail">
                        <div class="form-group">
                            <label class="col-md-3 control-label">User Name</label>
                            <div class="col-md-9">
                                 <asp:TextBox CssClass="form-control" runat="server" ID="tbxUserName"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label">Password</label>
                            <div class="col-md-9">
                                <asp:TextBox CssClass="form-control" runat="server"  ID="txtPassword" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
                    </asp:Panel>
                    <div class="form-group">
                        
                        <div class="col-md-12">
                            <asp:Button ID="btnConfig" CssClass="btn btn-primary pull-right" CausesValidation="false" runat="server" Text="Update" OnClick="btnConfig_Click"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

