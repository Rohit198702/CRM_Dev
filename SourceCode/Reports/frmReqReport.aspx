<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="frmReqReport.aspx.cs" Inherits="Reports_frmReqReport" %>

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
                            <h4 class="title-hyphen position-relative">Requisition Report</h4>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-7 col-md-8 col-lg-5 col-xl-5">
                        <div class="row">
                            <div class="col-sm-3 col-md-auto mb-3">
                                <label class="mb-0">Prepared By</label>
                            </div>
                            <div class="col-sm-6 col-md mb-3 chosenFullWidth">
                                <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlPreparedByList" runat="server" DataTextField="FirstName" DataValueField="EmployeeID" AutoPostBack="true" OnSelectedIndexChanged="ddlPreparedByList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-3 col-md-auto mb-3">
                                <label class="mb-0">By Requisition</label>
                            </div>
                            <div class="col-sm-6 col-md mb-3 chosenFullWidth">
                                <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlReq" runat="server" DataTextField="ReqNo" DataValueField="Requisitionid" AutoPostBack="true" OnSelectedIndexChanged="ddlReq_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md justify-content-center">
                        <asp:Button ID="btnPreview" runat="server" CssClass="btn btn-success btn-sm" CausesValidation="false" Text="Preview" OnClientClick="window.document.forms[0].target='_blank';" OnClick="btnPreview_Click" />
                        <asp:Button ID="btnGenerateExcel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Text="Export to Excel" OnClick="btnGenerateExcel_Click" OnClientClick="window.document.forms[0].target='_blank';"/>
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click1" />
                    </div>
                </div>

                <div class="row">
                </div>
            </div>


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
                    $('#<%=ddlPreparedByList.ClientID%>').chosen();
                    $('#<%=ddlReq.ClientID%>').chosen();
                }
            </script>
            <CR:CrystalReportViewer ID="rptPO" runat="server" AutoDataBind="true" BestFitPage="False" Width="100%" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
            <asp:HiddenField ID="HfBindAutoPreparedBy" runat="server" Value="-1" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPreview" />
            <asp:PostBackTrigger ControlID="btnGenerateExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

