<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="frmWasteEqDetailsReport.aspx.cs" Inherits="Reports_frmWasteEqDetailsReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <div class="col-12">
                <div class="row pt-3">
                    <div class="col-12">
                        <h5 class="text-uppercase">Waste Equipment Filters</h5>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Tracking#</label>
                            <asp:DropDownList ID="ddlTrackingNumber" runat="server" DataTextField="TrackingNo" DataValueField="TrackingNo" CssClass="form-control form-control-sm"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="form-group">
                            <label>Project Name</label>
                            <asp:DropDownList ID="ddlProject" runat="server" DataTextField="ProjectName" DataValueField="JobID" CssClass="form-control form-control-sm"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Manufacturer</label>
                            <asp:DropDownList ID="ddlManufacturer" runat="server" DataTextField="ManufacturerName" DataValueField="ManufacturerId" AutoPostBack="True" OnSelectedIndexChanged="ddlManufacturer_SelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Waste equipment</label>
                            <asp:DropDownList ID="ddlWasteEq" runat="server" DataTextField="WasteEqName" DataValueField="WasteEqId" AutoPostBack="True" OnSelectedIndexChanged="ddlWasteEq_SelectedIndexChanged" CssClass="form-control form-control-sm"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Accessory</label>
                            <asp:DropDownList ID="ddlAccessory" runat="server" DataTextField="AccName" DataValueField="AccId" CssClass="form-control form-control-sm"></asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Used From Stock</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlUsedFromStock" runat="server">
                                <asp:ListItem>All</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group srRadiosBtns">
                            <label>Assessory Required</label>
                            <asp:RadioButtonList ID="rdbAssessoryReq" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Filter By Date</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlFilterDate" runat="server" onchange="DateOnChange(this);">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Est. Delivery Date</asp:ListItem>
                                <asp:ListItem Value="2">Req. By Shop on</asp:ListItem>
                                <asp:ListItem Value="3">Ship Date</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-4" id="EstDeli" runat="server" style="display: none">
                        <div class="row">
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Est. Delivery Date From</label>
                                    <asp:TextBox ID="txtEstDeliveryDateFrom"
                                        runat="server"
                                        CssClass="form-control form-control-sm"
                                        autocomplete="off"
                                        onblur="validateDate(this)">
                                    </asp:TextBox>

                                    <asp:CalendarExtender ID="txtEstDeliveryDateFrom_Extender"
                                        runat="server"
                                        Format="MM/dd/yyyy"
                                        TargetControlID="txtEstDeliveryDateFrom"
                                        PopupButtonID="txtEstDeliveryDateFrom">
                                    </asp:CalendarExtender>
                                </div>
                            </div>

                            <div class="col-6">
                                <div class="form-group">
                                    <label>Est. Delivery Date To</label>
                                    <asp:TextBox ID="txtEstDeliveryDateTo"
                                        runat="server"
                                        CssClass="form-control form-control-sm"
                                        autocomplete="off"
                                        onblur="validateDate(this)">
                                    </asp:TextBox>

                                    <asp:CalendarExtender ID="txtEstDeliveryDateTo_Extender"
                                        runat="server"
                                        Format="MM/dd/yyyy"
                                        TargetControlID="txtEstDeliveryDateTo"
                                        PopupButtonID="txtEstDeliveryDateTo">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-4 " id="divReqByShop" runat="server" style="display: none">
                        <div class="row">
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Req. By Shop on From</label>
                                    <asp:TextBox ID="txtReqByShopDateFrom" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)">
                                    </asp:TextBox>
                                    <asp:CalendarExtender ID="txtReqByShopDateFrom_Extender" runat="server" Format="MM/dd/yyyy"
                                        PopupButtonID="txtReqByShopDateFrom" TargetControlID="txtReqByShopDateFrom">
                                    </asp:CalendarExtender>
                                </div>
                            </div>

                            <div class="col-6">
                                <div class="form-group">
                                    <label>Req. By Shop on To</label>
                                    <asp:TextBox ID="txtReqByShopDateTo" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)">
                                    </asp:TextBox>
                                    <asp:CalendarExtender ID="txtReqByShopDateTo_Extender" runat="server" Format="MM/dd/yyyy"
                                        PopupButtonID="txtReqByShopDateTo" TargetControlID="txtReqByShopDateTo">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="col-4" id="divShipDate" runat="server" style="display: none">
                        <div class="row">
                            <div class="col-6">
                                <div class="form-group">
                                    <label>Ship Date From</label>
                                    <asp:TextBox ID="txtShipDateFrom" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)">
                                    </asp:TextBox>
                                    <asp:CalendarExtender ID="calShipDate" runat="server" Format="MM/dd/yyyy"
                                        PopupButtonID="txtShipDateFrom" TargetControlID="txtShipDateFrom">
                                    </asp:CalendarExtender>
                                </div>
                            </div>

                            <div class="col-6">
                                <div class="form-group">
                                    <label>Ship Date To</label>
                                    <asp:TextBox ID="txtShipDateTo" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)">
                                    </asp:TextBox>
                                    <asp:CalendarExtender ID="calShipDateTo" runat="server" Format="MM/dd/yyyy"
                                        PopupButtonID="txtShipDateTo" TargetControlID="txtShipDateTo">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-md-auto">
                                <asp:Button ID="btnExportToPDF" runat="server" CssClass="btn btn-info btn-sm" OnClientClick="window.document.forms[0].target='_blank';" CausesValidation="false" Text="Preview Report" OnClick="btnExportToPDF_Click" />
                                <asp:Button ID="btnGenerateExcel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Text="Export to Excel" OnClick="btnGenerateExcel_Click" OnClientClick="window.document.forms[0].target='_blank';" />
                                <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger btn-sm" Text="Clear Search" OnClick="btnClear_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToPDF" />
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
            $('#<%=ddlTrackingNumber.ClientID%>').chosen();
            $('#<%=ddlProject.ClientID%>').chosen();
            $('#<%=ddlManufacturer.ClientID%>').chosen();
            $('#<%=ddlWasteEq.ClientID%>').chosen();
            $('#<%=ddlAccessory.ClientID%>').chosen();
            $('#<%=ddlUsedFromStock.ClientID%>').chosen();
            $('#<%=ddlFilterDate.ClientID%>').chosen();
        }

        function DateOnChange(ddl) {
            var selectedValue = ddl.value;
            if (selectedValue == "1" || selectedValue == "2" || selectedValue == "3") {
                if (selectedValue == "1") {
                    document.getElementById('<%= EstDeli.ClientID %>').style.display = 'block';
                    document.getElementById('<%= divReqByShop.ClientID %>').style.display = 'none';
                    document.getElementById('<%= divShipDate.ClientID %>').style.display = 'none';
                    resetReqByShopOnDate();
                    resetShipDate();
                }
                else if (selectedValue == "2") {
                    document.getElementById('<%= divReqByShop.ClientID %>').style.display = 'block';
                    document.getElementById('<%= divShipDate.ClientID %>').style.display = 'none';
                    document.getElementById('<%= EstDeli.ClientID %>').style.display = 'none';
                    resetEstiDeliDate();
                    resetShipDate();
                }
                else if (selectedValue == "3") {
                    document.getElementById('<%= divShipDate.ClientID %>').style.display = 'block';
                    document.getElementById('<%= divReqByShop.ClientID %>').style.display = 'none';
                    document.getElementById('<%= EstDeli.ClientID %>').style.display = 'none';
                    resetEstiDeliDate();
                    resetReqByShopOnDate();
                }
            }
            else {
                 document.getElementById('<%= EstDeli.ClientID %>').style.display = 'none';
                document.getElementById('<%= divReqByShop.ClientID %>').style.display = 'none';
                document.getElementById('<%= divShipDate.ClientID %>').style.display = 'none';
                resetEstiDeliDate();
                resetReqByShopOnDate();
                resetShipDate();
            }
        }

        function resetEstiDeliDate() {
            document.getElementById('<%= txtEstDeliveryDateFrom.ClientID%>').value = '';
            document.getElementById('<%= txtEstDeliveryDateTo.ClientID%>').value = '';
        }

        function resetReqByShopOnDate() {
            document.getElementById('<%= txtReqByShopDateFrom.ClientID%>').value = '';
            document.getElementById('<%= txtReqByShopDateTo.ClientID%>').value = '';
        }

        function resetShipDate() {
            document.getElementById('<%= txtShipDateFrom.ClientID%>').value = '';
            document.getElementById('<%= txtShipDateTo.ClientID%>').value = '';
        }
    </script>
    <CR:CrystalReportViewer ID="rptWasteEqDetailsReport" runat="server" AutoDataBind="true" BestFitPage="False" Width="100%" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
</asp:Content>
