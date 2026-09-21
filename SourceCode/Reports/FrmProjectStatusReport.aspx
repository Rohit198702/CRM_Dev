<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmProjectStatusReport.aspx.cs" Inherits="Reports_FrmProjectStatusReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-12 pt-2 piDiv position-sticky">
                <div class="row">
                    <div class="col-12">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Project Status Report</h4>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-2">
                        <div class="form-group">
                            <label>Product Code</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlProductCode" runat="server" DataTextField="text" DataValueField="id" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlProductCode_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-2">
                        <div class="form-group srRadiosBtns">
                            <label>Sent to Fabrication</label>
                            <asp:RadioButtonList ID="rdbSentToFabrication" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="2">Both</asp:ListItem>
                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="col-2">
                        <div class="form-group srRadiosBtns">
                            <label>Sent to Nesting</label>
                            <asp:RadioButtonList ID="rdbSentToNesting" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="2">Both</asp:ListItem>
                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="col-2">
                        <div class="form-group srRadiosBtns">
                            <label>Sent to Production</label>
                            <asp:RadioButtonList ID="rdbSentToProduction" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="2">Both</asp:ListItem>
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Value="0" Selected="True">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <div class="col-4">
                        <div class="row pt-3">
                            <div class="d-flex align-items-center mb-2">
                                <h4>
                                    <strong>Help Section: </strong>
                                </h4>
                            </div>
                            <div class="col-12 pt-2">
                                <%--<div class="d-flex align-items-center mb-2">
                                    <h5>
                                        <strong>Product Code-All</strong>
                                    </h5>
                                </div>--%>
                                <ul>
                                    <li><strong>Sent to Fabrication</strong> filter only works aerowerks.</li>
                                </ul>
                                <%--<div class="col-12">
                                </div>--%>
                            </div>
                        </div>
                    </div>

                    <div class="col-5 mb-2">
                        <%--<label>&nbsp;</label>--%>
                        <div class="row">
                            <div class="col-md-auto">
                                <asp:Button CssClass="btn btn-success btn-sm" ID="btnSearch" runat="server" CausesValidation="false" Text="Search" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnGenerateExcel" runat="server" CssClass="btn btn-info btn-sm" 
                                    CausesValidation="false" Text="Export to Excel" OnClick="btnGenerateExcel_Click" />
                                <asp:Button ID="btnCancel" CssClass="btn btn-danger btn-sm" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                            <div class="col-md justify-content-center">
                                <strong class="text-center">
                                    <asp:Label CssClass="alert alert-success d-block py-1" ID="lblRecordsCount" runat="server" Text="Label" Visible="false"></asp:Label></strong>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12 mt-3">
                <div class="table-responsive eoeTable" style="height: 250px; overflow-y: scroll;">
                    <asp:GridView ID="gvSearch" runat="server" CellPadding="3" EmptyDataText="No Items Found" Width="100%" CssClass="table mainGridTable table-sm mb-0"
                        EnableModelValidation="True" ShowFooter="false" AutoGenerateColumns="true">
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSearch" />
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
            $('#<%=ddlProductCode.ClientID%>').chosen();
        }
    </script>
</asp:Content>
