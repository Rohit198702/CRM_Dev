<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmMafsiDashboardConsultant.aspx.cs" Inherits="Mafsi_FrmMafsiDashboardConsultant" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content_KPIChina" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel_Mafsi" runat="server">
        <ContentTemplate>
            <div class="col-12 pt-2 piDiv position-sticky">
                <div class="row">
                    <div class="col-12">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Consultant Performance Report</h4>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-2 mb-2">
                        <div class="row chosenFullWidth">
                            <label class="col-12">Region</label>
                            <div class="col">
                                <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlRegion" DataTextField="text" DataValueField="id" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-2 mb-2">
                        <div class="row chosenFullWidth">
                            <label class="col-12">Consultant</label>
                            <div class="col">
                                <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlCompanyName" DataTextField="text" DataValueField="id" runat="server"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-2">
                        <div class="form-group">
                            <label id="lblFrom">From Date</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtFromDate" runat="server" AutoComplete="off" OnBlur="validateDate(this)"></asp:TextBox>
                            <asp:CalendarExtender ID="txtFromDateExtender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtFromDate" TargetControlID="txtFromDate"></asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-2">
                        <div class="form-group">
                            <label id="lblTo">To Date</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtToDate" runat="server" AutoComplete="off" OnBlur="validateDate(this)"></asp:TextBox>
                            <asp:CalendarExtender ID="txtToDateExtender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtToDate" TargetControlID="txtToDate"></asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-12 mb-2">
                        <div class="row">
                            <%--<label class="col-12">&nbsp;</label>--%>
                            <div class="col-auto">
                                <asp:Button ID="btnSummary" runat="server" CssClass="btn btn-primary btn-sm" Text="Summary" OnClick="btnSummary_Click" />                               
                                <asp:Button ID="btnExportExcel" runat="server" CssClass="btn btn-info btn-sm" Text="Export to Excel" CausesValidation="false" Enabled="false" OnClick="btnExportExcel_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                            <%--<div class="col-md justify-content-center">
                                <strong class="text-center">
                                    <asp:Label CssClass="alert alert-success d-block py-1" ID="lblRecordsCount" runat="server" Text="Label" Visible="false"></asp:Label></strong>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-12">
                <h4 id="lblSummaryTable" runat="server">Consultant Summary Table</h4>
            </div>

            <div class="col-12 my-3">
                <div class="table-responsive eoeTable" style="max-height: 300px">
                    <asp:GridView ID="gvSummary" runat="server" CellPadding="3" Width="100%" CssClass="table mainGridTable table-sm mb-0"
                        EnableModelValidation="True" ShowFooter="false" AutoGenerateColumns="true" OnRowCreated="gvSummary_RowCreated">
                    </asp:GridView>
                </div>
            </div>

            <div class="col-12">
                <h4 id="lblDetailedProfiles" runat="server">Detailed Consultant Profiles</h4>
            </div>

            <div class="col-12 my-3">
                <div class="table-responsive eoeTable" style="max-height: 300px">
                    <asp:GridView ID="gvProfiles" runat="server" CellPadding="3" Width="100%" CssClass="table mainGridTable table-sm mb-0"
                        EnableModelValidation="True" ShowFooter="false" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Info">
                                <ItemTemplate>
                                    <asp:Label ID="lblColumnName" runat="server" Text='<%# Eval("ColumnName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="true" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblColumnValue" runat="server" Text='<%# Eval("ColumnValue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <div class="col-12">
                <h4 id="lblLeads" runat="server">Proposals (Current Leads & Opportunities)</h4>
            </div>

            <div class="col-12 my-3">
                <div class="table-responsive eoeTable" style="max-height: 300px">
                    <asp:GridView ID="gvCurrentLeads" runat="server" CellPadding="3" Width="100%" CssClass="table mainGridTable table-sm mb-0"
                        EnableModelValidation="True" ShowFooter="false" AutoGenerateColumns="true" OnRowCreated="gvCurrentLeads_RowCreated">
                    </asp:GridView>
                </div>
            </div>

            <div class="col-12">
                <h4 id="lblSales" runat="server">Jobs (Converted Sales)</h4>
            </div>

            <div class="col-12 my-3">
                <div class="table-responsive eoeTable" style="max-height: 300px">
                    <asp:GridView ID="gvConvertedSales" runat="server" CellPadding="3" Width="100%" CssClass="table mainGridTable table-sm mb-0"
                        EnableModelValidation="True" ShowFooter="false" AutoGenerateColumns="true" OnRowCreated="gvConvertedSales_RowCreated">
                    </asp:GridView>
                </div>
            </div>

            <div class="col-12">
                <h4 id="lblPerformanceMatric" runat="server">Performance Matrics</h4>
            </div>

            <div class="col-12 my-3">
                <div class="table-responsive eoeTable" style="max-height: 300px">
                    <asp:GridView ID="gvPerformanceMatrics" runat="server" CellPadding="3" Width="100%" CssClass="table mainGridTable table-sm mb-0"
                        EnableModelValidation="True" ShowFooter="false" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Info">
                                <ItemTemplate>
                                    <asp:Label ID="lblColumnName" runat="server" Text='<%# Eval("ColumnName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="true" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Value">
                                <ItemTemplate>
                                    <asp:Label ID="lblColumnValue" runat="server" Text='<%# Eval("ColumnValue") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right"/>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportExcel" />
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
            $('#<%=ddlCompanyName.ClientID%>').chosen();
            $('#<%=ddlRegion.ClientID%>').chosen();
        }
    </script>
</asp:Content>
