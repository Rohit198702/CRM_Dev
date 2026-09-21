<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" EnableEventValidation="false" CodeFile="PreventativeMaintenance_CallStatusReport.aspx.cs" Inherits="Reports_PreventativeMaintenance_CallStatusReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <div class="col-12">
                <div class="row">
                    <div class="col-12 pt-2">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Aerowerks Preventive Maintenance Call Status Report</h4>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-2">
                        <div class="form-group">
                            <label>Status</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" runat="server" ID="ddlStatus" DataTextField="text" DataValueField="id">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <%--<div class="col-2 ">
                        <div class="form-group">
                            <label>Installation Completion From</label>
                            <asp:TextBox ID="txtInstallationCompletionFrom" CssClass="form-control form-control-sm" AutoComplete="off" runat="server" OnBlur="validateDate(this)"></asp:TextBox>
                            <asp:CalendarExtender ID="txtInstallationCompletionFrom_Extender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtInstallationCompletionFrom" TargetControlID="txtInstallationCompletionFrom">
                            </asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-2 ">
                        <div class="form-group">
                            <label>Installation Completion To</label>
                            <asp:TextBox ID="txtInstallationCompletionTo" CssClass="form-control form-control-sm" AutoComplete="off" runat="server" OnBlur="validateDate(this)"></asp:TextBox>
                            <asp:CalendarExtender ID="txtInstallationCompletionTo_Extender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtInstallationCompletionTo" TargetControlID="txtInstallationCompletionTo">
                            </asp:CalendarExtender>
                        </div>
                    </div>--%>

                    <div class="col-sm-2 ">
                        <div class="form-group" style="display:none;">
                            <label>Warranty End Date From</label>
                            <asp:TextBox ID="txtWarrantyEndDateFrom" CssClass="form-control form-control-sm" autocomplete="off" runat="server">
                            </asp:TextBox>
                            <asp:CalendarExtender ID="txtWarrantyEndDateFrom_Extender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtWarrantyEndDateFrom" TargetControlID="txtWarrantyEndDateFrom">
                            </asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-sm-2 ">
                        <div class="form-group" style="display:none;">
                            <label>Warranty End Date To</label>
                            <asp:TextBox ID="txtWarrantyEndDateTo" CssClass="form-control form-control-sm" autocomplete="off" runat="server">
                            </asp:TextBox>
                            <asp:CalendarExtender ID="txtWarrantyEndDateTo_Extender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtWarrantyEndDateTo" TargetControlID="txtWarrantyEndDateTo">
                            </asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-md-auto">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-secondary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                                 <asp:Button CssClass="btn btn-info btn-sm" ID="btnExportToExcel" CausesValidation="false" runat="server" Enabled="false" Text="Export to Excel" OnClick="btnExportToExcel_Click" />
                                <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger btn-sm" Text="Clear Search" OnClick="btnClear_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12 mt-2">
                <asp:GridView ID="gvSearch" runat="server" ForeColor="White" CssClass="table mainGridTable table-sm mb-0" AutoGenerateColumns="true" OnRowDataBound="gvSearch_RowDataBound" AllowSorting="true"
                    OnSorting="gvSearch_Sorting" EnableViewState="false">
                </asp:GridView>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
        </Triggers>
    </asp:UpdatePanel>

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
            $('#<%=ddlStatus.ClientID%>').chosen();
        }
    </script>
</asp:Content>
