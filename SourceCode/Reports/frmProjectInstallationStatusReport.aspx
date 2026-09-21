<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="frmProjectInstallationStatusReport.aspx.cs" Inherits="Reports_frmProjectInstallationStatusReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container">
    <div class="row">

        <!-- Left Section -->
        <div class="col-lg-9 d-flex">
            <div class="report-section w-100">
                <h4 class="mb-3">Weekly Installation Report</h4>
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Status</label>
                            <asp:DropDownList ID="ddlStatus" runat="server"
                                CssClass="form-control form-control-sm">
                                <asp:ListItem Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">Completed</asp:ListItem>
                                <asp:ListItem Value="2">In-Process</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-2">
                        <div class="form-group">
                            <label>Project Manager</label>
                            <asp:DropDownList ID="ddlProjectManagers" runat="server"
                                CssClass="form-control form-control-sm" DataTextField="EmployeeName" DataValueField="EmployeeID">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Installation Assigned To</label>
                            <asp:DropDownList ID="ddlInstallationAssTo" runat="server" DataTextField="text" DataValueField="id"
                                CssClass="form-control form-control-sm">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="mt-3">
                    <asp:Button ID="btnSearch" runat="server"
                        CssClass="btn btn-success btn-sm" CausesValidation="false" OnClientClick="window.document.forms[0].target='_blank';"
                        Text="Preview Report" OnClick="btnSearch_Click" />

                    <asp:Button ID="btnCancel" runat="server"
                        CssClass="btn btn-danger btn-sm"
                        Text="Clear Search" OnClick="btnCancel_Click" />
                </div>

            </div>
        </div>

        <!-- Right Section -->
        <div class="col-lg-3 d-flex">
            <div class="help-section w-100">

                <h4><strong>Help Section</strong></h4>

                <h5 class="mt-3">
                    <strong>Weekly Installation Report</strong>
                </h5>

                <ul class="mt-3">
                    <li><strong>Status</strong> is based on Completed or In-Process installation projects.</li>
                    <li><strong>Project Manager</strong> filters data by project manager.</li>
                </ul>

            </div>
        </div>

    </div>
</div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />

        </Triggers>
    </asp:UpdatePanel>
    <CR:CrystalReportViewer ID="rptProjectInstallationStatusReport" runat="server" AutoDataBind="true" BestFitPage="False" Width="100%" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(PageLoaded)
        });

        function PageLoaded(sender, args) {
            DDL();
        }

        $.when.apply($, PageLoaded).then(function () {
            DDL();
        });

        function DDL() {
            $('#<%=ddlProjectManagers.ClientID%>').chosen();
            $('#<%=ddlStatus.ClientID%>').chosen();
            $('#<%=ddlInstallationAssTo.ClientID%>').chosen();
        }
    </script>
</asp:Content>

