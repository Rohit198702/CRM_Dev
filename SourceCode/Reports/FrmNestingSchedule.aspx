<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmNestingSchedule.aspx.cs" Inherits="Reports_FrmNestingSchedule" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content_NestingSchedule" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel_NestingSchedule" runat="server">
        <ContentTemplate>

            <div class="col-12 pt-2 piDiv position-sticky">
                <div class="row">
                    <div class="col-12">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Nesting Schedule Report</h4>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Product Code</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlProductCode" runat="server">
                                <asp:ListItem Value="0" Selected>All</asp:ListItem>
                                <asp:ListItem Value="1">Aerowerks</asp:ListItem>
                                <asp:ListItem Value="2">ITW</asp:ListItem>
                                <%--<asp:ListItem Value="3">Gaylord</asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <%--<div class="col-2">
                        <div class="form-group">
                            <label id="lblStartDate" runat="server">Nesting Start Date From</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtFromDate" runat="server" AutoComplete="off" OnBlur="validateDate(this)"></asp:TextBox>
                            <asp:CalendarExtender ID="txtFromDate_Extender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtFromDate" TargetControlID="txtFromDate"></asp:CalendarExtender>

                        </div>
                    </div>

                    <div class="col-2">
                        <div class="form-group">
                            <label id="lblEndDate" runat="server">Nesting Start Date To</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtToDate" runat="server" AutoComplete="off" OnBlur="validateDate(this)"></asp:TextBox>
                            <asp:CalendarExtender ID="txtToDate_Extender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtToDate" TargetControlID="txtToDate"></asp:CalendarExtender>
                        </div>
                    </div>--%>

                    <%--<div class="col-sm-2">
                        <div class="form-group">
                            <label>Nesting Status</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlNestingStatus" runat="server" DataTextField="text" DataValueField="id">
                                <asp:ListItem Value="">All</asp:ListItem>
                                <asp:ListItem Value="0">Not started</asp:ListItem>
                                <asp:ListItem Value="1">In Progress</asp:ListItem>
                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                <asp:ListItem Value="3">Shipment within 4 weeks</asp:ListItem>
                                <asp:ListItem Value="4">(P.O / Drawings) Not Received</asp:ListItem>
                                <asp:ListItem Value="5">On Hold</asp:ListItem>
                                <asp:ListItem Value="6">Used from Stock</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>--%>

                    <div class="col-sm-3 pl-0">
                        <div class="form-group">
                            <label>&nbsp;</label>
                            <div class="col-auto">
                                <asp:Button ID="btnReport" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false" OnClick="btnReport_Click" Text="Generate Report" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-12 mt-2" id="divAero" runat="server">
                <h5>Aerowerks</h5>
                <div class="table-responsive">
                    <asp:GridView CssClass="table mainGridTable table-sm mb-0" ForeColor="White" ID="gvAerowerksNesting" runat="server" AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
            </div>

            <div class="col-12 mt-2" id="divITW" runat="server">
                <h5>ITW</h5>
                <div class="table-responsive">
                    <asp:GridView CssClass="table mainGridTable table-sm mb-0" ForeColor="White" ID="gvITWNesting" runat="server" AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
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
            $('#<%=ddlProductCode.ClientID%>').chosen();
    <%--    $('#<%=ddlDates.ClientID%>').chosen();--%>
        }
    </script>
</asp:Content>
