<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="FrmSheetForecastingData.aspx.cs" Inherits="SheetMetalForecasting_FrmSheetForecastingData" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <div class="col-12">
                <div class="row">
                    <div class="col-12 pt-2">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Sheet Metal Forecasting</h4>
                        </div>
                    </div>
                
                    <div class="col-6 col-sm-4 col-md-3 col-lg-4">
                        <div class="form-group">
                            <label class="text-danger">Sheet Metal ID*</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlSheetMetalID" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="text-danger">Warehouse*</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlWarehouse" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2" style="display:none">
                        <div class="form-group">
                            <label>From Date</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtFromdate" runat="server" autocomplete="off" OnBlur="validateDate(this)" AutoPostBack="true" OnTextChanged="txtFromdate_TextChanged"></asp:TextBox>
                            <asp:CalendarExtender ID="cal_txtFromdate" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtFromdate" TargetControlID="txtFromdate"></asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2" style="display:none">
                        <div class="form-group">
                            <label>To Date</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtToDate" runat="server" autocomplete="off" Enabled="false"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-md-auto">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnExporttoExcel" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false" OnClientClick="window.document.forms[0].target='_blank';setTimeout(function(){document.forms[0].target='';},0);" Text="Export To Excel" OnClick="btnExporttoExcel_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />

                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12" runat="server">
                <div id="pangvRequititionDetails" runat="server" class="row border-top pt-3 mt-2">
                    <div class="col-12">
                        <div class="table-responsive">
                            <asp:GridView BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                ForeColor="Black" GridLines="Vertical" Width="100%" EmptyDataText="No Records Found"
                                ID="gvJobSheetForecasting" runat="server" AutoGenerateColumns="true" CssClass="table mainGridTable table-sm"
                                EnableModelValidation="True" OnRowDataBound="gvJobSheetForecasting_RowDataBound" OnRowCreated="gvJobSheetForecasting_RowCreated">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <HeaderStyle Font-Names="Arial" BackColor="Black" Font-Bold="True" ForeColor="White" />
                            </asp:GridView>
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
                    $('#<%=ddlWarehouse.ClientID%>').chosen();
                    $('#<%=ddlSheetMetalID.ClientID%>').chosen();
                }
            </script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExporttoExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
