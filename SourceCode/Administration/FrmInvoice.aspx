<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="FrmInvoice.aspx.cs" Inherits="Administration_FrmInvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>

            <div class="col-12 pt-2 piDiv position-sticky">
                <div class="row">
                    <div class="col-12">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Invoice Details</h4>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6 col-md-3 col-lg-3">
                        <div class="row">
                            <div class="col-sm-auto mb-3">
                                <label class="mb-0">Invoice No</label>
                            </div>
                            <div class="col-sm mb-3">
                                <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlInvoiceNo" runat="server" DataTextField="text" DataValueField="id" AutoPostBack="True" OnSelectedIndexChanged="ddlInvoiceNo_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-3">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-sm" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnExportToPDf" runat="server" CssClass="btn btn-primary btn-sm" Text="Generate Invoice" CausesValidation="false" OnClientClick="window.document.forms[0].target='_blank';" Enabled="false" OnClick="btnExportToPDf_Click" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>
            <div class="col-12 mt-3">
                <asp:Panel ID="pnlInvoice" runat="server">
                    <table id="tblInvoice" runat="server" class="table table-bordered table-sm" style="width: 100%; table-layout: fixed;">
                        <!-- Title -->
                        <colgroup>
                            <col style="width: 6%" />
                            <col style="width: 49%" />
                            <col style="width: 10%" />
                            <col style="width: 15%" />
                            <col style="width: 20%" />
                        </colgroup>

                        <tr>
                            <td colspan="5" class="text-center">
                                <h3><b>INVOICE</b></h3>
                            </td>
                        </tr>
                        <!-- Invoice Header -->
                        <tr>
                            <td style="width: 22%;"></td>
                            <td style="width: 28%;"></td>
                            <td style="width: 22%;"></td>

                            <td class="text-right">
                                <b>Invoice Number :</b>
                            </td>

                            <td>
                                <asp:TextBox ID="txtInvoiceNo"
                                    runat="server"
                                    CssClass="form-control form-control-sm d-inline-block"
                                    MaxLength="50" autocomplete="off">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <!-- Company -->
                        <tr>
                            <td colspan="5" style="padding-left: 110px;">
                                <h5 class="margin-bottom:3px;"><b>Smart Conveyer Design Solutions Pvt. Ltd.</b></h5>
                                Plot #82, Industrial Area
                            <br />
                                Phase-9, Mohali, Punjab, India
                            <br />
                                Phone: +91 172 5053287
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 22%;"></td>
                            <td style="width: 28%;"></td>
                            <td style="width: 22%;"></td>
                            <td class="text-right">
                                <b>Date :</b>
                            </td>

                            <td>
                                <asp:TextBox ID="txtInvoiceDate" CssClass="form-control form-control-sm"
                                    runat="server" OnBlur="validateDate(this)" autocomplete="off">
                                </asp:TextBox>
                                <asp:CalendarExtender ID="cal_txtInvoiceDate" runat="server" Format="MM/dd/yyyy"
                                    PopupButtonID="txtInvoiceDate" TargetControlID="txtInvoiceDate">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" style="padding-left: 110px;">
                                <b>Customer :</b><br />
                                Aerowerks Inc<br />
                                6625 Millcreek, Mississauga
                            <br />
                                Ontario, L5N 5M4
                            <br />
                                Phone: 905 363 6999
                            </td>

                        </tr>

                                                 
                                <tr>
                                    <td colspan="5">
                                        <div style="display: flex; align-items: center; justify-content: center;">
                                            <b style="margin-right: 8px;">Invoice Period :</b>

                                            <asp:TextBox ID="txtInvoicePeriod"
                                                runat="server"
                                                CssClass="form-control form-control-sm"
                                                Style="width: 300px;"
                                                MaxLength="250"
                                                autocomplete="off">
                                            </asp:TextBox>
                                        </div>
                                    </td>
                                </tr>             
                        <!-- Grid Header -->
                        <tr style="background: #E9ECEF; font-weight: bold;">
                            <td width="2%">S.No.</td>
                            <td width="45%">Project Details</td>
                            <td width="12%">Qty</td>
                            <td width="15%">Rate</td>
                            <td width="20%">Total Amount (US$)</td>
                        </tr>

                        <!-- Row 1 -->
                        <tr>
                            <td>1</td>
                            <td>Technical Drawings for Proposal</td>
                            <td>
                                <asp:TextBox ID="txtTechDwg"
                                    runat="server"
                                    CssClass="form-control form-control-sm text-right" onkeypress="return onlyNumbers(event);" MaxLength="5" OnTextChanged="txtTechDwg_TextChanged" AutoPostBack="true">
                                </asp:TextBox>
                            </td>
                            <td class="text-right">
                                <asp:Label ID="lblRate1" runat="server">90.00</asp:Label></td>
                            <td class="text-right">
                                <asp:Label ID="lblTechDwgTotal" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>

                        <!-- Row 2 -->
                        <tr>
                            <td>2</td>
                            <td>Job Drawings</td>
                            <td>
                                <asp:TextBox ID="txtJobDwg"
                                    runat="server"
                                    CssClass="form-control form-control-sm text-right" onkeypress="return onlyNumbers(event);" MaxLength="5" OnTextChanged="txtJobDwg_TextChanged" AutoPostBack="true">
                                </asp:TextBox>
                            </td>
                            <td class="text-right">
                                <asp:Label ID="lblRate2" runat="server">120.00</asp:Label></td>
                            <td class="text-right">
                                <asp:Label ID="lblJobDwgTotal" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>

                        <!-- Row 3 -->
                        <tr>
                            <td>3</td>
                            <td>Fabrication Drawings</td>
                            <td>
                                <asp:TextBox ID="txtFabDwg"
                                    runat="server"
                                    CssClass="form-control form-control-sm text-right" onkeypress="return onlyNumbers(event);" MaxLength="5" OnTextChanged="txtFabDwg_TextChanged" AutoPostBack="true">
                                </asp:TextBox>
                            </td>
                            <td class="text-right">
                                <asp:Label ID="lblRate3" runat="server">750.00</asp:Label></td>
                            <td class="text-right">
                                <asp:Label ID="lblFabDwgTotal" runat="server" Font-Bold="true"></asp:Label>
                            </td>
                        </tr>

                        <!-- Row 4 -->
                        <tr>
                            <td>4</td>
                            <td>On Call Advisory Support</td>
                            <td></td>
                            <td></td>
                            <td class="text-right">
                                <asp:Label ID="lblCallAdvisorySupport" runat="server" Font-Bold="true">1000</asp:Label></td>
                        </tr>

                        <!-- Row 5 -->
                        <tr>
                            <td>5</td>
                            <td>AWS Charges<br />
                            </td>
                            <td></td>
                            <td>
                                <asp:TextBox ID="txtAWSCharges"
                                    runat="server"
                                    CssClass="form-control form-control-sm text-right" onkeypress="return onlyDotsAndNumbers(this,event);" MaxLength="10" OnTextChanged="txtAWSCharges_TextChanged" AutoPostBack="true">
                                </asp:TextBox>
                            </td>
                            <td class="text-right">
                                <asp:Label ID="lblAWSCloudTotal" runat="server" Font-Bold="true"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>6</td>
                            <td>Support Charges</td>
                            <td></td>
                            <td>
                                <asp:TextBox ID="txtSupportChanges"
                                    runat="server"
                                    CssClass="form-control form-control-sm text-right" onkeypress="return onlyDotsAndNumbers(this,event);" MaxLength="10" OnTextChanged="txtSupportChanges_TextChanged" AutoPostBack="true">
                                </asp:TextBox>
                            </td>
                            <td class="text-right">
                                <asp:Label ID="lblSupportTotal" runat="server" Font-Bold="true"></asp:Label></td>
                        </tr>

                        <!-- Grand Total -->
                        <tr>
                            <td colspan="4" class="text-right">
                                <b>TOTAL</b>
                            </td>
                            <td class="text-right">
                                <asp:Label ID="lblGrandTotal"
                                    runat="server"
                                    Font-Bold="true">
                                </asp:Label>
                            </td>
                        </tr>

                        <!-- Amount in Words -->
                        <tr>
                            <td colspan="5" class="text-center">
                                <b>US Dollars : </b>
                                <asp:Label ID="lblAmountWords"
                                    runat="server" CssClass="boldtext">
                                </asp:Label>
                            </td>
                        </tr>

                        <!-- Signature -->
                        <tr>
                            <td colspan="5" style="height: 120px; padding-left: 110px;">

                                <!-- Signature -->
                                <div style="float: left; text-align: left;">

                                    <b>For Smart Conveyer Design Solutions Pvt. Ltd.</b>
                                    <br />

                                    <asp:Image ID="imgSignature"
                                        runat="server"
                                        Width="180px"
                                        Height="70px"
                                        ImageUrl="~/Images/Signature.jpg" />

                                    <br />
                                    <br />

                                    <b>Prateek Saurabh Kalsi</b><br />
                                    Manager-Engineering

                                </div>

                                <!-- Clear float -->
                                <div style="clear: both;"></div>

                                <!-- Approval -->
                                <div style="text-align: center; margin-top: 20px; font-weight: bold;">
                                    The above Invoice is as per approval of
                                Balbir Singh (President of Aerowerks Inc)
                                </div>

                            </td>
                        </tr>

                    </table>
                </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToPDf" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(PageLoaded);
        });

        function PageLoaded(sender, args) {
            DDLName();
        }
        $.when.apply($, PageLoaded).then(function () {
            DDLName();
        });

        function DDLName() {
            $('#<%=ddlInvoiceNo.ClientID%>').chosen();
        }
    </script>
</asp:Content>



