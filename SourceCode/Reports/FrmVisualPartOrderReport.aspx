<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmVisualPartOrderReport.aspx.cs" Inherits="Reports_FrmVisualPartOrderReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <div class="col-12 pt-2 piDiv position-sticky">
                <div class="row">
                    <div class="col-12">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Visual Part Order Report</h4>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-2">
                        <div class="form-group">
                            <label>PO #</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlPO" runat="server" DataTextField="PO" DataValueField="PO">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-2">
                        <div class="form-group">
                            <label>Order Date From</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtStartDateFrom" OnBlur="validateDate(this)" runat="server" Width="100%"></asp:TextBox>
                            <asp:CalendarExtender ID="txtStartDateFrom_Extender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtStartDateFrom" TargetControlID="txtStartDateFrom">
                            </asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-2">
                        <div class="form-group">
                            <label>Order Date To</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtStartDateTo" OnBlur="validateDate(this)" runat="server" Width="100%"></asp:TextBox>
                            <asp:CalendarExtender ID="txtStartDateTo_Extender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtStartDateTo" TargetControlID="txtStartDateTo">
                            </asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-4 mb-2">
                        <label>&nbsp;</label>
                        <div class="form-group">
                            <asp:Button ID="btnPreview" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" OnClick="btnPreview_Click"
                                OnClientClick="window.document.forms[0].target='_blank';" Text="Preview" />
                            <asp:Button ID="btnExportExcel" runat="server" CssClass="btn btn-info btn-sm" Text="Export to Excel" CausesValidation="false" OnClick="btnExportExcel_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPreview" />
            <asp:PostBackTrigger ControlID="btnExportExcel" />
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
            $('#<%=ddlPO.ClientID%>').chosen();
        }
    </script>
</asp:Content>
