<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmGaffneyPOReport.aspx.cs" Inherits="Reports_FrmGaffneyPOReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upGaffneyPo" runat="server">
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="col-8">
                        <div class="col-md-auto mx-auto innerMain">
                            <div class="row pt-3 flex-column">
                                <div class="col-12">
                                    <h4 class="title-hyphen position-relative mb-3">Gaffney PO Report</h4>
                                </div>
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-sm-auto">
                                            <div class="form-group">
                                                <label id="lblFrom">PO Received Date From</label>
                                                <asp:TextBox CssClass="form-control form-control-sm" ID="txtDateFrom" runat="server" AutoComplete="off" OnBlur="validateDate(this)"></asp:TextBox>
                                                <asp:CalendarExtender ID="txtDateFromExtender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtDateFrom"
                                                    TargetControlID="txtDateFrom">
                                                </asp:CalendarExtender>

                                            </div>
                                        </div>
                                        <div class="col-sm-auto">
                                            <div class="form-group">
                                                <label id="lblTo">PO Received Date To</label>
                                                <asp:TextBox CssClass="form-control form-control-sm" ID="txtDateTo" runat="server" AutoComplete="off" OnBlur="validateDate(this)"></asp:TextBox>
                                                <asp:CalendarExtender ID="txtDateToExtender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtDateTo"
                                                    TargetControlID="txtDateTo">
                                                </asp:CalendarExtender>
                                            </div>
                                        </div>

                                        <div class="col-12 pt-2">
                                            <div class="form-group mb-0 flex-column">
                                                <div>
                                                    <asp:Button ID="btnPreview" runat="server" CssClass="btn btn-success btn-sm" CausesValidation="false"
                                                        OnClientClick="window.document.forms[0].target='_blank';" Text="Preview Report" OnClick="btnPreview_Click" />
                                                    <asp:Button ID="btnExportExcel" runat="server" CssClass="btn btn-info btn-sm" Text="Export to Excel" CausesValidation="false"
                                                        OnClick="btnExportExcel_Click" />
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Clear Search" OnClick="btnCancel_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-4">
                        <!-- Help section on the right -->
                        <div class="row pt-3">
                            <div class="d-flex align-items-center mb-2">
                                <h4>
                                    <strong>Help Section: </strong>
                                </h4>
                            </div>
                            <div class="col-12 pt-2">
                                <div class="d-flex align-items-center mb-2">
                                    <h5>
                                        <strong>Gaffney PO Report</strong>
                                    </h5>
                                </div>
                                <div class="col-12">
                                    <ul>
                                        <li>Date is based on <strong>PO Received Date</strong>.</li>
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
            <asp:PostBackTrigger ControlID="btnExportExcel" />
        </Triggers>
    </asp:UpdatePanel>

    <CR:CrystalReportViewer ID="rptGeffneyPO" runat="server" AutoDataBind="true" BestFitPage="False" Width="100%" EnableDatabaseLogonPrompt="False"
        EnableParameterPrompt="False" ToolPanelView="None" />
</asp:Content>

