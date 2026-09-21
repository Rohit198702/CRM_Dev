<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmSheetMetalStockAdjustment.aspx.cs" Inherits="SheetMetalForecasting_FrmSheetMetalStockAdjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="_Content_Forecasting_Models" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel_Forecasting_Models" runat="server">
        <ContentTemplate>
            <div class="col-12">
                <div class="row pt-3">
                    <div class="col-sm-12">
                        <h5 class="text-uppercase">Sheet Metal Stock Adjustments</h5>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-4">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Sheet Metal ID*</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlLookupSheetMetal" runat="server" DataTextField="text" DataValueField="id" AutoPostBack="true" OnSelectedIndexChanged="ddlLookupSheetMetal_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2 col-lg-1">
                        <div class="form-group">
                            <label class="pl-0 col-12">Gauge</label>
                            <asp:TextBox ID="txtGauge" CssClass="form-control form-control-sm" runat="server" Enabled="false">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2 col-lg-1">
                        <div class="form-group">
                            <label class="pl-0 col-12">Width</label>
                            <asp:TextBox ID="txtWidth" CssClass="form-control form-control-sm text-right" runat="server" Enabled="false">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2 col-lg-1">
                        <div class="form-group">
                            <label class="pl-0 col-12">Length</label>
                            <asp:TextBox ID="txtLength" CssClass="form-control form-control-sm text-right" runat="server" Enabled="false">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Warehouse*</label>
                            <asp:DropDownList ID="ddlWarehouse" CssClass="form-control form-control-sm" runat="server" DataTextField="text" DataValueField="id" Enabled="false">
                            </asp:DropDownList>
                        </div>
                    </div>


                    <div class="col-6 col-sm-4 col-md-2 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Adjustment Type*</label>
                            <asp:DropDownList ID="ddlAdjustmentType" CssClass="form-control form-control-sm text-right" runat="server">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Stock-In</asp:ListItem>
                                <asp:ListItem Value="2">Stock-Out</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Adjustment Reason*</label>
                            <asp:DropDownList ID="ddlAdjReason" CssClass="form-control form-control-sm" runat="server" DataTextField="text" DataValueField="id">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Adjustment Quantity*</label>
                            <asp:TextBox ID="txtQuantity" CssClass="form-control form-control-sm text-right" runat="server" autocomplete="off" onkeypress="return onlyNumbers(event);" MaxLength="7">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2 col-lg-4">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Summary*</label>
                            <asp:TextBox ID="txtSummary" CssClass="form-control form-control-sm" runat="server" MaxLength="500" autocomplete="off" TextMode="MultiLine">                                
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-auto d-flex align-items-end mb-3">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-sm mr-1" CausesValidation="false" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
                    </div>

                </div>
            </div>
            <div class="col-12">
                <div id="pangvRequititionDetails" runat="server" class="row border-top pt-3" visible="false">
                    <div class="col-sm-12">
                        <h5 class="text-uppercase">Stock Entry Details</h5>
                    </div>
                    <div class="col-12">
                        <div class="table-responsive">
                            <asp:GridView BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                ForeColor="Black" GridLines="Vertical" Width="100%"
                                ID="gvSheetMetalStockIn" runat="server" AutoGenerateColumns="False" CssClass="table mainGridTable table-sm"
                                EnableModelValidation="True">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Transaction ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactionID" runat="server" Text='<%# Eval("TransactionID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactionType" runat="server" Text='<%# Eval("TransactionType") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sheet Metal ID">
                                        <ItemStyle />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSheetMetalID" runat="server" Text='<%# Eval("sheetmetalid") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sheet Metal Desc">
                                        <ItemStyle />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSheetMetalDesc" runat="server" Text='<%# Eval("sheetmetaldesc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Warehouse">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWarehouse" runat="server" Text='<%# Eval("Warehouse") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Opening Stock" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblopeningstock" runat="server" Text='<%# Eval("openingstock") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transact Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactQty" runat="server" Text='<%# Eval("transactqty") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing Stock">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClosingStock" runat="server" Text='<%# Eval("closingstock") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transact Date Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactDateTime" runat="server" Text='<%# Eval("TransactDateTime") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>        
                                    <asp:TemplateField HeaderText="Transact Reason">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactReason" runat="server" Text='<%# Eval("Reason") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transact By">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactBy" runat="server" Text='<%# Eval("transactby") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>

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
            $('#<%=ddlLookupSheetMetal.ClientID%>').chosen();
            $('#<%=ddlAdjustmentType.ClientID%>').chosen();
            $('#<%=ddlWarehouse.ClientID%>').chosen();
            $('#<%=ddlAdjustmentType.ClientID%>').chosen();
            $('#<%=ddlAdjReason.ClientID%>').chosen();
        }
    </script>
</asp:Content>
