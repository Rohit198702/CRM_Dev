<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmSheetMetalStockIn.aspx.cs" Inherits="SheetMetalForecasting_FrmSheetMetalStockIn" %>

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
                            <h4 class="title-hyphen position-relative">Sheet Metal Stock In</h4>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-6 col-md-3 col-lg-3 col-xl-3">
                        <div class="row">
                            <div class="col-sm-3 col-md-auto mb-3">
                                <label class="mb-0">Lookup PO Number</label>
                            </div>
                            <div class="col-sm-6 col-md mb-3 chosenFullWidth">
                                <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlLookupPONumber" runat="server" DataTextField="text" DataValueField="id" AutoPostBack="true" OnSelectedIndexChanged="ddlLookupPONumber_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm col-md col-lg col-xl-auto">
                        <div class="row">
                            <div class="col-auto">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-sm" CausesValidation="false" Text="Save" OnClick="btnSave_Click" />
                                <asp:Button ID="btnPreview" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Text="Preview Report" OnClick="btnPreview_Click" />
                                <asp:Button ID="btnMaster" runat="server" CssClass="btn btn-primary btn-sm" Visible="false" CausesValidation="false" OnClientClick="window.document.forms[0].target='_blank';" Text="Master Form" OnClick="btnMaster_Click" />

                                <asp:Button ID="btnConsumeQuantity" runat="server" CssClass="btn btn-info btn-sm" Visible="false" OnClientClick="window.document.forms[0].target='_blank';" CausesValidation="false" Text="Stock Out" OnClick="btnConsumeQuantity_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-12">
                <div class="row pt-3">
                    <div class="col-sm-12">
                        <h5 class="text-uppercase">Sheet Metal Stock In Details</h5>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">PO Number*</label>
                            <asp:TextBox ID="txtPONumber" CssClass="form-control form-control-sm" runat="server" autocomplete="off" MaxLength="50">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Warehouse*</label>
                            <asp:DropDownList ID="ddlWarehouse" CssClass="form-control form-control-sm" runat="server" DataTextField="text" DataValueField="id" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="ddlWarehouse_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-4">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Sheet Metal ID*</label>
                            <asp:DropDownList ID="ddlSheetMetalID" CssClass="form-control form-control-sm" runat="server" DataTextField="text" DataValueField="id" AutoPostBack="true" OnSelectedIndexChanged="ddlSheetMetalID_SelectedIndexChanged">
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
                            <asp:TextBox ID="txtCurrentStock" CssClass="form-control form-control-sm text-right" runat="server" Enabled="false">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Order Date*</label>
                            <asp:TextBox ID="txtDeliveryDate" CssClass="form-control form-control-sm" OnBlur="validateDate(this)" runat="server" autocomplete="off">
                            </asp:TextBox>
                            <asp:CalendarExtender ID="txtDeliveryDate_Extender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtDeliveryDate" TargetControlID="txtDeliveryDate"></asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Expected Arrival Date*</label>
                            <asp:TextBox ID="txtExpectedArrivalDate" CssClass="form-control form-control-sm" OnBlur="validateDate(this)" runat="server" autocomplete="off">
                            </asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtExpectedArrivalDate" TargetControlID="txtExpectedArrivalDate"></asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Order Qty*</label>
                            <asp:TextBox ID="txtDelQty" CssClass="form-control form-control-sm text-right" onkeypress="return onlyNumbers(event);" MaxLength="5" runat="server" autocomplete="off">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Status*</label>
                            <asp:DropDownList ID="ddlDeliveryStatus" CssClass="form-control form-control-sm" runat="server" DataTextField="text" DataValueField="id">
                            </asp:DropDownList>
                        </div>
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
                                ForeColor="Black" GridLines="Vertical" Width="100%" DataKeyNames="StockInId,SheetMetalID,DeliveryStatusId,PODetailID"
                                ID="gvSheetMetalStockIn" runat="server" AutoGenerateColumns="False" CssClass="table mainGridTable table-sm"
                                EnableModelValidation="True" OnRowEditing="gvSheetMetalStockIn_RowEditing" OnRowCommand="gvSheetMetalStockIn_RowCommand"
                                OnRowDeleting="gvSheetMetalStockIn_RowDeleting" OnRowDataBound="gvSheetMetalStockIn_RowDataBound">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sheet Metal ID/Desc">
                                        <ItemStyle />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSheetMetalID" runat="server" Text='<%# Eval("SheetMetalDesc") %>'></asp:Label>
                                            <asp:Label ID="lblSheetMetal" runat="server" Text='<%# Eval("SheetMetalID") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Warehouse">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWarehouse" runat="server" Text='<%# Eval("Warehouse") %>'></asp:Label>
                                            <asp:Label ID="lblWarehouseID" runat="server" Text='<%# Eval("WarehouseId") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gauge" ItemStyle-HorizontalAlign="Left" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGauge" runat="server" Text='<%# Eval("gauge") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Width" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("width") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Length" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLength" runat="server" Text='<%# Eval("length") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# Eval("DeliveryDate", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expected Arrival Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExpectedArrivalDate" runat="server" Text='<%# Eval("ExpectedArrivalDate", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Current Stock">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOpeningStock" runat="server" Text='<%# Eval("openingstock") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactQty" runat="server" Text='<%# Eval("DeliveryQuantity") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Closing Qty" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblClosingQuantity" runat="server" Text='<%# Eval("ClosingStock") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Modify">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" Text="Edit" CommandName="Edit"><i class="far fa-edit" title="Edit"></i></asp:LinkButton>
                                            <asp:LinkButton runat="server" CssClass="btn btn-success btn-sm" Text="Stock In" CommandName="StockIn"
                                                OnClientClick="return confirm('System will make live Stock-In Transactions are you sure?');"
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
            <asp:HiddenField ID="hfPODetailID" runat="server" Value="-1" />
            <asp:HiddenField ID="hfStockDeliveryStatus" runat="server" Value="-1" />
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
            $('#<%=ddlLookupPONumber.ClientID%>').chosen();
            $('#<%=ddlSheetMetalID.ClientID%>').chosen();
            $('#<%=ddlWarehouse.ClientID%>').chosen();
            $('#<%=ddlDeliveryStatus.ClientID%>').chosen();
        }
    </script>
</asp:Content>
