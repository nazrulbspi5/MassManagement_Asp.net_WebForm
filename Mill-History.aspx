<%@ Page Title="Mill-History" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Mill-History.aspx.cs" Inherits="Mill_History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           

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
            <div class="form-horizontal">
                <div class="Card-header">
                    <strong>Meal History</strong>
                </div>
                <div class="Card-Body">
                    <div class="row">
                        <div class="col-md-12">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Member Name</label>
                                    <asp:DropDownList ID="ddlMember" AppendDataBoundItems="true" DataValueField="Name" DataTextField="Name" AutoPostBack="true" OnSelectedIndexChanged="ddlMember_SelectedIndexChanged" CssClass="form-control" runat="server">
                                                                      
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Form Date</label>
                                    <asp:TextBox runat="server" ID="tbxFormDate" CssClass="form-control" />
                                    <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy"
                                        Enabled="True" TargetControlID="tbxFormDate"></ajax:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>To Date</label>
                                    <asp:TextBox runat="server" ID="tbxToDate" CssClass="form-control" />
                                    <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                        Enabled="True" TargetControlID="tbxToDate"></ajax:CalendarExtender>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary mt-24" Text="Show" OnClick="btnShow_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnFromDate" runat="server" />
                <asp:HiddenField ID="hdnToDate" runat="server" />
                    <div class="row">
                        <div class="col-md-12">

                            <div class="table-responsive">
                                <asp:GridView ID="gvMillHistory" AllowPaging="true" OnPageIndexChanging="gvMillHistory_PageIndexChanging" PageSize="15" DataKeyNames="ID" ShowFooter="true" OnRowDataBound="gvMillHistory_RowDataBound" runat="server" CssClass="table table-bordered table-striped"
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



                                    </Columns>
                                    <PagerStyle CssClass="pagination-ys" />
                                   
                                    <FooterStyle HorizontalAlign="Center" Font-Bold="True"></FooterStyle>
                                </asp:GridView>

                            </div>

                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

