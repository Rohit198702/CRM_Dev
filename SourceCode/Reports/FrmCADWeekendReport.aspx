<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="FrmCADWeekendReport.aspx.cs" Inherits="Reports_FrmCADWeekendReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel_FrmCadWeekendReport" runat="server">
        <ContentTemplate>
            <div class="col-12">
                <div class="row">
                    <div class="col-12 pt-2">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">CAD Report</h4>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-2">
                        <div class="form-group">
                            <label>Select Past Days</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" runat="server" ID="ddlDays">
                                <asp:ListItem Value="-1" Selected>1</asp:ListItem>
                                <asp:ListItem Value="-2">2</asp:ListItem>
                                <asp:ListItem Value="-3">3</asp:ListItem>
                                <asp:ListItem Value="-4">4</asp:ListItem>
                                <asp:ListItem Value="-5">5</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="form-group">
                            <label class="col-12">&nbsp;</label>
                            <asp:Button ID="btnExportToPDF" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false" Text="Preview Report" OnClientClick="window.document.forms[0].target='_blank';" OnClick="btnExportToPDF_Click" />
                            <asp:Button ID="btnGenerateExcel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Text="Export to Excel" OnClick="btnGenerateExcel_Click" OnClientClick="window.document.forms[0].target='_blank';" />
                        </div>
                    </div>
                </div>
                <div class="row border-top pt-2" id="divProjectEngineer" runat="server">
                    <div class="col-2">
                        <div class="form-group">
                            <label>Nature of Task</label>
                            <asp:DropDownList ID="ddlNatureOfTask" CssClass="form-control form-control-sm" runat="server" DataTextField="text" DataValueField="id">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-4">
                        <div class="form-group">
                            <label>Models</label>
                            <asp:ListBox ID="ddlModels" runat="server" DataTextField="text" DataValueField="id" SelectionMode="multiple"
                                CssClass="form-control form-control-sm"></asp:ListBox>
                        </div>
                    </div>

                    <div class="col-2">
                        <div class="form-group">
                            <label>Project Engineer</label>
                            <asp:DropDownList ID="ddlProjectEngineer" CssClass="form-control form-control-sm" runat="server"
                                DataTextField="text" DataValueField="id">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-2">
                        <div class="form-group">
                            <label>Date Project Sent To Customer From</label>
                            <asp:TextBox ID="txtReqByRCDFrom" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)">
                            </asp:TextBox>
                            <asp:CalendarExtender ID="txtReqByRCDFrom_Extender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtReqByRCDFrom" TargetControlID="txtReqByRCDFrom">
                            </asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-2">
                        <div class="form-group">
                            <label>Date Project Sent To Customer To</label>
                            <asp:TextBox ID="txtReqByRCDTo" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)">
                            </asp:TextBox>
                            <asp:CalendarExtender ID="txtReqByRCDTo_Extender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtReqByRCDTo" TargetControlID="txtReqByRCDTo">
                            </asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-4">
                        <div class="form-group">
                            <%--<label class="col-12">&nbsp;</label>--%>
                            <asp:Button ID="btnProjectEngineerReport" runat="server" CssClass="btn btn-secondary btn-sm" 
                                CausesValidation="false" Text="Preview Report" OnClientClick="window.document.forms[0].target='_blank';" OnClick="btnProjectEngineerReport_Click" />
                            <asp:Button ID="btnProjectEngineerReportExcel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Text="Export to Excel" 
                                OnClick="btnProjectEngineerReportExcel_Click" OnClientClick="window.document.forms[0].target='_blank';" />         
                             <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />            
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnProjectEngineerReport" />
            <asp:PostBackTrigger ControlID="btnProjectEngineerReportExcel" />
            <asp:PostBackTrigger ControlID="btnExportToPDF" />
            <asp:PostBackTrigger ControlID="btnGenerateExcel" />
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
            $('#<%=ddlDays.ClientID%>').chosen();
            $('#<%=ddlNatureOfTask.ClientID%>').chosen();
            $('#<%=ddlModels.ClientID%>').chosen();
            $('#<%=ddlProjectEngineer.ClientID%>').chosen();
        }
    </script>
</asp:Content>
