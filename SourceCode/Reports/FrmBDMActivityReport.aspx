<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmBDMActivityReport.aspx.cs" Inherits="Reports_FrmBDMActivityReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <div class="col-12">
                <div class="row pt-3">
                    <div class="col-12">
                        <h5 class="text-uppercase">BDM Activity Report</h5>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>BDM</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlBDM" DataTextField="text" DataValueField="id" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group chosenFullWidth">
                            <label>Activity Type</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlActivityType" DataTextField="text" DataValueField="id" runat="server"></asp:DropDownList>
                        </div>
                    </div>

                     <div class="col-sm-2">
                        <div class="form-group">
                            <label>Status</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlStatus" DataTextField="text" DataValueField="id" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-2 ">
                        <div class="form-group">
                            <label>Activity Date From</label>
                            <asp:TextBox ID="txtActivityDateFrom" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)">
                            </asp:TextBox>
                            <asp:CalendarExtender ID="txtActivityDateFrom_Extender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtActivityDateFrom" TargetControlID="txtActivityDateFrom">
                            </asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-sm-2 ">
                        <div class="form-group">
                            <label>Activity Date To</label>
                            <asp:TextBox ID="txtActivityDateTo" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)">
                            </asp:TextBox>
                            <asp:CalendarExtender ID="txtActivityDateTo_Extender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtActivityDateTo" TargetControlID="txtActivityDateTo">
                            </asp:CalendarExtender>
                        </div>
                    </div>                   

                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-md-auto">
                                <asp:Button ID="btnExportToPDF" runat="server" CssClass="btn btn-info btn-sm" OnClientClick="window.document.forms[0].target='_blank';"
                                    CausesValidation="false" Text="Preview Report" OnClick="btnExportToPDF_Click" />
                                <asp:Button ID="btnGenerateExcel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Text="Export to Excel"
                                    OnClick="btnGenerateExcel_Click" OnClientClick="window.document.forms[0].target='_blank';" />
                                <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger btn-sm" Text="Clear Search" OnClick="btnClear_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToPDF" />
            <asp:PostBackTrigger ControlID="btnGenerateExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <CR:CrystalReportViewer ID="rptBDMActivityReport" runat="server" AutoDataBind="true" BestFitPage="False" Width="100%" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(PageLoaded)
        });

        function PageLoaded(sender, args) {
            BindDrp();
        }

        $.when.apply($, PageLoaded).then(function () {
            BindDrp();
        });

        function BindDrp() {
            $('#<%=ddlStatus.ClientID%>').chosen();
            $('#<%=ddlBDM.ClientID%>').chosen();
            $('#<%=ddlActivityType.ClientID%>').chosen();
        }
    </script>
</asp:Content>
