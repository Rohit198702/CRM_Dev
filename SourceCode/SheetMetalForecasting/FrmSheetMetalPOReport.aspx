<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="FrmSheetMetalPOReport.aspx.cs" Inherits="SheetMetalForecasting_FrmSheetMetalPOReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <div class="col-12">
                <div class="row">
                    <div class="col-12 pt-2">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Sheet Metal Order Report</h4>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-6 col-sm-4 col-md-3 col-lg-3">
                        <div class="form-group">
                            <label>Sheet Metal ID</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlSheetMetalID" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label>Warehouse</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlWarehouse" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-1">
                        <div class="form-group">
                            <label>Gauge</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlGauge" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label>PO Number</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlPONumber" runat="server" DataTextField="text" DataValueField="id">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label id="lblFrom">Order Date From</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtOrderDateFrom" runat="server" AutoComplete="off" OnBlur="validateDate(this)"></asp:TextBox>
                            <asp:CalendarExtender ID="cal_txtOrderDateFrom" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtOrderDateFrom" TargetControlID="txtOrderDateFrom"></asp:CalendarExtender>

                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="form-group">
                            <label id="lblTo">Order Date To</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtOrderDateTo" runat="server" AutoComplete="off" OnBlur="validateDate(this)"></asp:TextBox>
                            <asp:CalendarExtender ID="cal_txtOrderDateTo" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtOrderDateTo" TargetControlID="txtOrderDateTo"></asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-md-auto">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" OnClientClick="window.document.forms[0].target='_blank';" CausesValidation="false" Text="Preview" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />

                            </div>

                        </div>
                    </div>
                </div>
            </div>

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
                    $('#<%=ddlSheetMetalID.ClientID%>').chosen();
                    $('#<%=ddlGauge.ClientID%>').chosen();
                    $('#<%=ddlWarehouse.ClientID%>').chosen();
                    $('#<%=ddlPONumber.ClientID%>').chosen();
                }
            </script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
        </Triggers>
    </asp:UpdatePanel>
    <CR:CrystalReportViewer ID="rptSheetMetal" runat="server" AutoDataBind="true" BestFitPage="False" Width="100%" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
</asp:Content>
