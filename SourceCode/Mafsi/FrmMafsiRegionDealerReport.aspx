<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmMafsiRegionDealerReport.aspx.cs" Inherits="Mafsi_FrmMafsiRegionDealerReport" %>

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
                            <h4 class="title-hyphen position-relative">Mafsi Region Dealer Report</h4>
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
                            <label class="col-12">Dealer</label>
                            <div class="col">
                                <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlDealer" DataTextField="text" DataValueField="id" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>                  

                    <div class="col-6 mb-2">
                        <div class="row">
                            <label class="col-12">&nbsp;</label>
                            <div class="col-auto">
                                <asp:Button ID="btnReport" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false" OnClick="btnReport_Click" OnClientClick="window.document.forms[0].target='_blank';" Text="Dealer Summary" />
                                <asp:Button ID="btnConvertedSales" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Text="Converted Sales" OnClick="btnConvertedSales_Click" OnClientClick="window.document.forms[0].target='_blank';"/>
                                <asp:Button ID="btnCurrentLeads" runat="server" CssClass="btn btn-success btn-sm" CausesValidation="false" Text="Current Leads" OnClick="btnCurrentLeads_Click" OnClientClick="window.document.forms[0].target='_blank';"/>
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnConvertedSales" />
            <asp:PostBackTrigger ControlID="btnCurrentLeads" />
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
            $('#<%=ddlDealer.ClientID%>').chosen();
            $('#<%=ddlRegion.ClientID%>').chosen();
        }
    </script>
</asp:Content>