<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="frmOpportunitiesReport.aspx.cs" Inherits="Reports_frmOpportunitiesReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <div class="container">
                 <div class="row">
            <div class="col-9">
                <div class="row">
                    <div class="col-12 pt-2">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Opportunities Report</h4>
                        </div>
                    </div>
                </div>
                <div class="row mb-2">
                    <div class="col-12 col-sm-6 col-md-3 col-lg-3">                      
                            <asp:Label ID="lblFromDate" runat="server">From Date</asp:Label>
                            <asp:TextBox ID="txtFromDate" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)">
                            </asp:TextBox>
                            <asp:CalendarExtender ID="fromDateExtender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtFromDate" TargetControlID="txtFromDate">
                            </asp:CalendarExtender>                      
                    </div>
                    <div class="col-12 col-sm-4 col-md-3 col-lg-3">                       
                            <asp:Label ID="lblToDate" runat="server">To Date</asp:Label>
                            <asp:TextBox ID="txtToDate" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)">
                            </asp:TextBox>
                            <asp:CalendarExtender ID="toDateExtender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtToDate" TargetControlID="txtToDate">
                            </asp:CalendarExtender>                       
                    </div>
                    <div class="col-12 col-sm-4 col-md-3 col-lg-3">                       
                            <asp:Label ID="lblPM" runat="server">Dest. Rep </asp:Label>
                            <asp:DropDownList ID="ddlDestRep" CssClass="form-control form-control-sm" runat="server" DataTextField="EmployeeName" DataValueField="RepID">
                            </asp:DropDownList>                                                 
                    </div>
                    <div class="col-auto d-flex align-items-end pt-3">
                        <asp:Button ID="btnPreview" runat="server" Enabled="true" CssClass="btn btn-info btn-sm mr-2" CausesValidation="false" Text="Preview" OnClientClick="window.document.forms[0].target='_blank';" OnClick="btnPreview_Click" />                       
                        <asp:Button ID="btnCancel" CssClass="btn btn-danger btn-sm" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </div>
                </div>              
                </div>
             <div class="col-3"> <!-- Help section on the right -->
            <div class="row pt-3">
                <div class="d-flex align-items-center mb-2">
                    <h4>
                        <strong>Help Section: </strong>
                    </h4>
                </div>
                <div class="col-12 pt-2" runat="server" id="divProposalDwgs_HelpSection">
                    <div class="d-flex align-items-center mb-2">
                        <h5>
                            <strong>Opportunities Report</strong>
                        </h5>
                    </div>
                    <div class="col-12">
                        <ul>
                            <li>Date is based on <strong>Proposal Date</strong>.</li>                                                                                                  
                        </ul>
                    </div>
                </div>   
            </div>
        </div>
            </div>
            </div>
        </ContentTemplate>
            <Triggers>
            <asp:PostBackTrigger ControlID="btnPreview" />           
        </Triggers>
        </asp:UpdatePanel>
     <CR:CrystalReportViewer ID="rptAeroInvoice" runat="server" AutoDataBind="true" BestFitPage="False" Width="100%" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
<script type="text/javascript" language="javascript">
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
    $('#<%=ddlDestRep.ClientID%>').chosen();   
}

</script>
</asp:Content>


