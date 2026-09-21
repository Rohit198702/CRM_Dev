<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmSheetMetalConsumption.aspx.cs" Inherits="SheetMetalForecasting_FrmSheetMetalConsumption" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="_Content_Forecasting_Models" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel_Forecasting_Models" runat="server">
        <ContentTemplate>
            <div class="col-12 pt-2 piDiv position-sticky">
                <div class="row">
                    <div class="col-12">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()">
                                <i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Sheet Metal Stock Out History</h4>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-3 col-md-3 col-lg-3 col-xl-3" style="display: none">
                        <div class="row">
                            <div class="col-sm-3 col-md-auto mb-3">
                                <label class="mb-0">Lookup Product Code</label>
                            </div>
                            <div class="col-sm-6 col-md mb-3 chosenFullWidth">
                                <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlLookupProductCode" runat="server" DataTextField="text" DataValueField="id">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm col-md col-lg col-xl-auto">
                        <div class="row">
                            <div class="col-auto">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-12">
                <div class="row pt-3">
                    <div class="col-sm-12">
                        <h5 class="text-uppercase">Sheet Metal Consumption Entry</h5>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2" style="display: none">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Product Code*</label>
                            <asp:DropDownList ID="ddlProductCode" CssClass="form-control form-control-sm" runat="server" DataTextField="text" DataValueField="id" AutoPostBack="true" OnSelectedIndexChanged="ddlProductCode_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-4">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Sheet Metal No*</label>
                            <asp:DropDownList ID="ddlSheetMetalNo" CssClass="form-control form-control-sm" runat="server" DataTextField="text" DataValueField="id" AutoPostBack="true" OnSelectedIndexChanged="ddlSheetMetalNo_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Warehouse*</label>
                            <asp:DropDownList ID="ddlWarehouse" Enabled="false" CssClass="form-control form-control-sm" runat="server" DataTextField="text" DataValueField="id" AutoPostBack="true" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2 col-lg-1" style="display: none">
                        <div class="form-group">
                            <label class="pl-0 col-12">Gauge</label>
                            <asp:TextBox ID="txtGauge" CssClass="form-control form-control-sm" runat="server" Enabled="false">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2 col-lg-1" style="display: none">
                        <div class="form-group">
                            <label class="pl-0 col-12">Width</label>
                            <asp:TextBox ID="txtWidth" CssClass="form-control form-control-sm text-right" runat="server" Enabled="false">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2 col-lg-1" style="display: none">
                        <div class="form-group">
                            <label class="pl-0 col-12">Length</label>
                            <asp:TextBox ID="txtLength" CssClass="form-control form-control-sm text-right" runat="server" Enabled="false">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12">Current Stock</label>
                            <asp:TextBox ID="txtcurrentStock" CssClass="form-control form-control-sm text-right" runat="server" Enabled="false">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2 col-lg-2" style="display: none">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Date*</label>
                            <asp:TextBox ID="txtDate" CssClass="form-control form-control-sm" runat="server" OnBlur="validateDate(this)" autocomplete="off" Enabled="false">
                            </asp:TextBox>
                            <asp:CalendarExtender ID="cal_txtDate" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtDate" TargetControlID="txtDate">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-sm col-md col-lg col-xl-auto">
                        <div class="d-flex align-items-end flex-nowrap">

                            <!-- Transact Quantity -->
                            <div class="mr-2">
                                <label class="text-danger">Transact Quantity*</label>
                                <asp:TextBox ID="txtQuantity"
                                    runat="server"
                                    CssClass="form-control form-control-sm text-right"
                                    Style="width: 197px;">
                                </asp:TextBox>
                            </div>

                            <!-- Save -->
                            <div class="mr-1">
                                <asp:Button ID="btnSave"
                                    runat="server"
                                    CssClass="btn btn-success btn-sm"
                                    CausesValidation="false"
                                    Text="Save"
                                    OnClick="btnSave_Click"
                                    OnClientClick="return confirm('Are you sure.?');" />
                            </div>

                            <!-- Cancel -->
                            <div>
                                <asp:Button ID="btnCancel"
                                    runat="server"
                                    CssClass="btn btn-danger btn-sm"
                                    Text="Cancel"
                                    OnClick="btnCancel_Click" />
                            </div>

                        </div>
                    </div>
                    <div class="col-sm col-md col-lg col-xl-auto">
                        <div class="row">
                            <div class="col-auto">

                                <asp:Button ID="btnMaster" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" OnClientClick="window.document.forms[0].target='_blank';" Text="Master Form" OnClick="btnMaster_Click" Visible="false" />
                                <asp:Button ID="btnStockIn" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false" OnClientClick="window.document.forms[0].target='_blank';" Text="Stock In" OnClick="btnStockIn_Click" Visible="false" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12">
                <div id="pangvRequititionDetails" runat="server" class="row border-top pt-3 mt-2" visible="false">
                    <div class="col-sm-12">
                        <h5 class="text-uppercase">Sheet Metal Consumption History</h5>
                    </div>
                    <div class="col-12">

                        <div class="table-responsive">
                            <asp:GridView BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                ForeColor="Black" GridLines="Vertical" Width="100%"
                                ID="gvSheetMetalConsumptionHistory" runat="server" AutoGenerateColumns="False" CssClass="table mainGridTable table-sm"
                                EnableModelValidation="True">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sheet Metal ID" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSheetMetal" runat="server" Text='<%# Eval("SheetMetal") %>'></asp:Label>
                                            <asp:Label ID="lblSheetMetalID" runat="server" Text='<%# Eval("sheetmetalid") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Warehouse">
                                        <ItemStyle />
                                        <ItemTemplate>
                                            <asp:Label ID="lblWarehouse" runat="server" Text='<%# Eval("WarehouseName") %>'></asp:Label>
                                            <asp:Label ID="lblWarehouseID" runat="server" Text='<%# Eval("WarehouseID") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Gauge" ItemStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGauge" runat="server" Text='<%# Eval("Gauge") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Width" ItemStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Width") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Length" ItemStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Length") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("transactdatetime","{0:MM/dd/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Stock">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpeningStock" runat="server" Text='<%# Eval("CurrentStock") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transact Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactQty" runat="server" Text='<%# Eval("TransactQty") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClosingQuantity" runat="server" Text='<%# Eval("PendingQty") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Modify" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" Text="Edit" CommandName="Edit"><i class="far fa-edit" title="Edit"></i></asp:LinkButton>
                                            <asp:LinkButton runat="server" CssClass="btn btn-success btn-sm" Text="Stock Out" CommandName="StockOut"
                                                OnClientClick="return confirm('System will make live Stock-Out Transactions are you sure?');"
                                                CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="far fa-paper-plane" title="Stock In"></i></asp:LinkButton>
                                            <asp:LinkButton CssClass="btn btn-danger btn-sm" title="Delete" runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure.?');">
                                                         <i class="far fa-times-circle"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hfStockOutID" runat="server" Value="-1" />
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
            $('#<%=ddlLookupProductCode.ClientID%>').chosen();
            $('#<%=ddlProductCode.ClientID%>').chosen();
            $('#<%=ddlWarehouse.ClientID%>').chosen();
            $('#<%=ddlSheetMetalNo.ClientID%>').chosen();
        }
    </script>
</asp:Content>
