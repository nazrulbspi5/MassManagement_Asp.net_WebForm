<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Account_Login" Async="true" %>

<%--<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>--%>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <div class="row">
        <div class="col-md-3"></div>
        <div class="col-md-6">

            <div class="form-horizontal">
                <div class="Card-header">
                    <strong>Log in</strong>
                </div>
                <div class="Card-Body">
                    <asp:Login ID="Login1" RememberMeSet="True" Width="100%" runat="server"
                        TitleText="" OnLoggedIn="Login1_LoggedIn" OnLoginError="Login1_OnLoginError">
                        <LayoutTemplate>
                            <%-- <div class="col-md-12">--%>
                            <asp:Panel runat="server" Visible="False" ID="pnlAlert">
                                <div class="msg_warn">
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="false"></asp:Literal>
                                </div>
                            </asp:Panel>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label>User Name</label>
                                    <asp:TextBox runat="server" ID="Username" CssClass="form-control" placeholder="Username"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rqrdUserName" ForeColor="red" Display="Dynamic" SetFocusOnError="True" ControlToValidate="Username" ValidationGroup="login" runat="server" ErrorMessage="Enter your user name."></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <label>Password</label>
                                    <asp:TextBox runat="server" ID="Password" TextMode="password" CssClass="form-control"
                                        placeholder="Password"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="red" Display="Dynamic" SetFocusOnError="True" ControlToValidate="Password" ValidationGroup="login" runat="server" ErrorMessage="Enter your password."></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <div class="checkbox">

                                        <label>
                                            <input type="checkbox" />
                                            Remember me                           
                                        </label>
                                        <%--<label class="pull-right">
                                            <a href="Forgot-Password">Forgot Password?</a>
                                        </label>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12">
                                    <asp:Button ID="btnLogin" runat="server" ValidationGroup="login" CommandName="Login" CssClass="btn btn-success btn-block" Text="Log in" />
                                </div>
                            </div>
                           <%-- <div class="register-link m-t-15 text-center">
                                <p>Don't have account ? <a href="Register">Sign Up Here</a></p>
                            </div>--%>
                            <%-- </div>--%>
                        </LayoutTemplate>
                    </asp:Login>
                </div>
            </div>
        </div>
        <div class="col-md-3"></div>
    </div>

   
</asp:Content>

