<%@ Page Title="Register-Member" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Register-Member.aspx.cs" Inherits="Register_Member" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <style type="text/css">
       label
       {            
           display:block;
       }
       input.error
       {
           border: solid 2px #CC0000;
       }
 
       input.valid
       {
           border: solid 2px green;
       }
       span
       {
            margin-left:5px;
       }
       span.valid
       {
           width:16px;
           height:16px;
           color: green;
           background:url("http://cdn1.iconfinder.com/data/icons/basicset/tick_16.png") left center no-repeat;
           display:inline-block;
           
       }
       span.error
       {          
           width: 100%;
           color: red;
       }
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="row">
                <div class="col-md-6">

                    <div class="form-horizontal">
                        <div class="Card-header">
                            <strong>Register Member</strong>
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
                                    <asp:TextBox ID="tbxName" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ForeColor="Red" runat="server" Display="Dynamic" ControlToValidate="tbxName"
                                        CssClass="text-danger" ErrorMessage="Enter Member Name." />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Email</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="tbxEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ForeColor="Red" runat="server" Display="Dynamic" ControlToValidate="tbxEmail"
                                        CssClass="text-danger" ErrorMessage="Enter Email." />
                                    <asp:RegularExpressionValidator ID="regexEmailValid" Display="Dynamic" runat="server" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="tbxEmail" ForeColor="Red" ErrorMessage="Please enter valid email address"></asp:RegularExpressionValidator>

                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Mobile</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="tbxMobile" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ForeColor="Red" runat="server" Display="Dynamic" ControlToValidate="tbxMobile"
                                        CssClass="text-danger" ErrorMessage="Enter Mobile Number." />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Address</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="tbxAddress" TextMode="MultiLine" Rows="3" CssClass="form-control" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ForeColor="Red" runat="server" Display="Dynamic" ControlToValidate="tbxAddress"
                                        CssClass="text-danger" ErrorMessage="Enter Address." />
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3 control-label">Status</label>
                                <div class="col-md-9">
                                    <asp:DropDownList CssClass="form-control" ID="ddlstatus" runat="server">
                                        <asp:ListItem Value="Active">Active</asp:ListItem>
                                        <asp:ListItem Value="In Active">In Active</asp:ListItem>
                                    </asp:DropDownList>
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
                            <strong>Member List</strong>
                        </div>
                        <div class="Card-Body">

                            <div class="table-responsive">
                                <asp:GridView ID="gvMember" DataKeyNames="ID" ShowFooter="true" runat="server" OnRowDeleting="gvMember_RowDeleting" OnSelectedIndexChanged="gvMember_SelectedIndexChanged" CssClass="table table-bordered table-striped"
                                    AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="#SL">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                                <asp:Label ID="Label1" Visible="false" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>

                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" CommandName="Select" ImageUrl="Content/Images/btnedit.gif" Text="Select" />
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" runat="server" OnClientClick="return confirm('Pressing OK will delete this record. Do you want to continue?')" CausesValidation="False" CommandName="delete" ImageUrl="Content/Images/btndelete.gif" Text="delete" />
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




        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery/jquery-1.9.0.min.js"></script>
    <script src="http://ajax.aspnetcdn.com/ajax/jquery.validate/1.11.1/jquery.validate.min.js" type="text/javascript"></script>
    <script>
        $("#aspForm").validate({
               errorElement: 'span',                
               rules: {
                   UserName: {
                       required: true,
                       remote: function () {
                           return {
                               url: "./Register-Member/IsUserNameAvailable",
                               type: "POST",
                               contentType: "application/json; charset=utf-8",
                               dataType: "json",
                               data: JSON.stringify({ userName: $('#tbxName').val() }),
                               dataFilter: function (data) {
                                   var msg = JSON.parse(data);
                                   if (msg.hasOwnProperty('d'))
                                       return msg.d;
                                   else
                                       return msg;
                               }
                           }
                       },
                   },
                   
               },
               messages: {
                   UserName: {
                       required: "User name is Required",
                       remote: "This user name is already in use",
                   },
                   
               },
               onkeyup:false,
               onblur: true,
               onfocusout: function (element) { $(element).valid() }
 
                
 
        });

    </script>
</asp:Content>

