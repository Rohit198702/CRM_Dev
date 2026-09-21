<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmNestingReport.aspx.cs" Inherits="Reports_FrmNestingReport" %>

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
                            <h4 class="title-hyphen position-relative">Nesting Report</h4>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-2">
                        <div class="form-group">
                            <label>Product Code</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlNestingFor" runat="server" DataTextField="text" DataValueField="id">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Filter data on</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlDateType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlDateType_SelectedIndexChanged">
                                <asp:ListItem Value="1">Nesting Start Date</asp:ListItem>
                                <asp:ListItem Value="2">Nesting Sent Date</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-2" id="divStartDateFrom" runat="server">
                        <div class="form-group">
                            <label>Nesting Start Date From</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtStartDateFrom" OnBlur="validateDate(this)" runat="server" Width="100%"></asp:TextBox>
                            <asp:CalendarExtender ID="txtStartDateFrom_Extender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtStartDateFrom" TargetControlID="txtStartDateFrom">
                            </asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-2" id="divStartDateTo" runat="server">
                        <div class="form-group">
                            <label>Nesting Start Date To</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtStartDateTo" OnBlur="validateDate(this)" runat="server" Width="100%"></asp:TextBox>
                            <asp:CalendarExtender ID="txtStartDateTo_Extender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtStartDateTo" TargetControlID="txtStartDateTo">
                            </asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-2" id="divSentDateFrom" runat="server">
                        <div class="form-group">
                            <label>Nesting Sent Date From</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtSentDateFrom" OnBlur="validateDate(this)" runat="server" Width="100%"></asp:TextBox>
                            <asp:CalendarExtender ID="txtSentDateFrom_Extender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtSentDateFrom" TargetControlID="txtSentDateFrom">
                            </asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-2" id="divSentDateTo" runat="server">
                        <div class="form-group">
                            <label>Nesting Sent Date To</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtSentDateTo" OnBlur="validateDate(this)" runat="server" Width="100%"></asp:TextBox>
                            <asp:CalendarExtender ID="txtSentDateTo_Extender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtSentDateTo" TargetControlID="txtSentDateTo">
                            </asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-2">
                        <div class="form-group srRadiosBtns">
                            <label>Sent to Production</label>
                            <asp:RadioButtonList ID="rdbSentToProduction" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="2">Both</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="col-2">
                        <div class="form-group">
                            <label>Nesting Status</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlNestingStatus" runat="server">
                                <asp:ListItem Value="">All</asp:ListItem>
                                <asp:ListItem Value="0">Not started</asp:ListItem>
                                <asp:ListItem Value="1">In Progress</asp:ListItem>
                                <%--<asp:ListItem Value="9">Cancelled</asp:ListItem>--%>
                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                <asp:ListItem Value="3">Shipment within 4 weeks</asp:ListItem>
                                <asp:ListItem Value="4">(P.O / Drawings) Not Received</asp:ListItem>
                                <asp:ListItem Value="5">On Hold</asp:ListItem>
                                <asp:ListItem Value="6">Used from Stock</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-4 mb-2">
                        <%--<label>&nbsp;</label>--%>
                        <div class="form-group">
                            <asp:Button ID="btnPreview" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" OnClick="btnPreview_Click"
                                OnClientClick="window.document.forms[0].target='_blank';" Text="Preview" />
                            <asp:Button ID="btnGenerateExcel" runat="server" CssClass="btn btn-info btn-sm" CausesValidation="false" Text="Export to Excel" OnClick="btnGenerateExcel_Click" OnClientClick="window.document.forms[0].target='_blank';"/>
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPreview" />
            <asp:PostBackTrigger ControlID="btnGenerateExcel" />
        </Triggers>
    </asp:UpdatePanel>
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
            $('#<%=ddlNestingFor.ClientID%>').chosen();
            $('#<%=ddlNestingStatus.ClientID%>').chosen();
            $('#<%=ddlDateType.ClientID%>').chosen();
        }
    </script>
</asp:Content>
