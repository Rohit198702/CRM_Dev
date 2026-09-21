<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmSheetMetal.aspx.cs" Inherits="SheetMetalForecasting_FrmSheetMetal" %>

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
                            <h4 class="title-hyphen position-relative">Sheet Metal Maintenance Form</h4>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-7 col-md-8 col-lg-6 col-xl-6">
                        <div class="row">
                            <div class="col-sm-3 col-md-auto mb-3">
                                <label class="mb-0">Lookup Part #/Description</label>
                            </div>
                            <div class="col-sm-6 col-md mb-6 chosenFullWidth">
                                <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlPartNumber" runat="server" DataTextField="text" DataValueField="id" AutoPostBack="true" OnSelectedIndexChanged="ddlPartNumber_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm col-md col-lg col-xl-auto">
                        <div class="row">
                            <div class="col-auto">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-sm" CausesValidation="false" Text="Save" OnClick="btnSave_Click" />
                                <asp:Button ID="btnPreviewReport" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Text="Preview Report" OnClick="btnPreviewReport_Click" />
                                <asp:Button ID="tblStockIn" runat="server" CssClass="btn btn-primary btn-sm" OnClientClick="window.document.forms[0].target='_blank';" CausesValidation="false" Text="Stock In" OnClick="tblStockIn_Click" Visible="false" />
                                <asp:Button ID="btnConsumeQuantity" runat="server" CssClass="btn btn-secondary btn-sm" OnClientClick="window.document.forms[0].target='_blank';" CausesValidation="false" Text="Stock Out" Visible="false" OnClick="btnConsumeQuantity_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-12">
                <div class="row pt-3">
                    <div class="col-sm-12">
                        <h5 class="text-uppercase">Add Sheet Metal Information</h5>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Sheet Metal ID*</label>
                            <asp:TextBox ID="txtSheetMetalID" CssClass="form-control form-control-sm" runat="server" MaxLength="50" autocomplete="off">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Sheet Metal Description*</label>
                            <asp:TextBox ID="txtSheetMetalDesc" CssClass="form-control form-control-sm" runat="server" MaxLength="150" autocomplete="off">
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2 col-lg-1">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Gauge*</label>
                            <asp:DropDownList ID="ddlGauge" CssClass="form-control form-control-sm" runat="server" DataValueField="id" DataTextField="text">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2 col-lg-1">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Width*</label>
                            <asp:DropDownList ID="ddlWidth" CssClass="form-control form-control-sm" runat="server" DataValueField="id" DataTextField="text">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2 col-lg-1">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Length*</label>
                            <asp:DropDownList ID="ddlLength" CssClass="form-control form-control-sm" runat="server" DataTextField="text" DataValueField="id">
                            </asp:DropDownList>
                        </div>
                    </div>


                    <div class="col-sm-8 col-md-4 col-lg-2">
                        <div class="form-group">
                            <label class="text-danger">Status*</label>
                            <asp:DropDownList CssClass="form-control form-control-sm control-access" ID="ddlSheetMetalStatus" runat="server">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Current</asp:ListItem>
                                <asp:ListItem Value="2">Obsolete</asp:ListItem>
                                <asp:ListItem Value="3">Not In Use</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2s col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12">Current Stock</label>
                            <asp:TextBox ID="txtCurrentStock" CssClass="form-control form-control-sm text-right" runat="server" MaxLength="5" onkeypress="return onlyNumbers(event);" Enabled="false">                                
                            </asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12">
                <div class="row pt-3">
                    <div class="col-sm-12">
                        <h5 class="text-uppercase">Add Warehouse Information &emsp;<asp:Button CssClass="btn btn-success btn-sm rounded" ID="btnAdd" runat="server" Text="Add Details" Enabled="false" OnClick="btnAdd_Click" /></h5>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Warehouse*</label>
                            <asp:DropDownList ID="ddlWarehouse" CssClass="form-control form-control-sm" runat="server" Enabled="false" DataTextField="text" DataValueField="id">                                
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-1">
                        <div class="form-group">
                            <label class="pl-0 col-12">Min Qty</label>
                            <asp:TextBox ID="txtMinQty" CssClass="form-control form-control-sm text-right" runat="server" Enabled="false" MaxLength="5" onkeypress="return onlyNumbers(event);" autocomplete="off">                                
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-1">
                        <div class="form-group">
                            <label class="pl-0 col-12">Max Qty</label>
                            <asp:TextBox ID="txtMaxQty" CssClass="form-control form-control-sm text-right" runat="server" Enabled="false" onkeypress="return onlyNumbers(event);" MaxLength="5" autocomplete="off">                                
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12">Lead Time</label>
                            <asp:TextBox ID="txtLeadTime" CssClass="form-control form-control-sm" runat="server" Enabled="false" MaxLength="10" autocomplete="off">                                
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12">Quantity on Skid</label>
                            <asp:TextBox ID="txtQtyonSkid" CssClass="form-control form-control-sm" runat="server" Enabled="false" MaxLength="10" autocomplete="off">                                
                            </asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12" id="divsheetmetaldetails" runat="server" visible="false">
                <div id="pangvRequititionDetails" runat="server" class="row border-top pt-3">
                    <div class="col-sm-12">
                        <h5 class="text-uppercase">Sheet Metal Summary</h5>
                    </div>
                    <div class="col-12">

                        <div class="table-responsive">
                            <asp:GridView BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                ForeColor="Black" GridLines="Vertical" Width="100%" DataKeyNames="ID"
                                ID="gvSheetMetalParts" runat="server" AutoGenerateColumns="False" CssClass="table mainGridTable table-sm"
                                EnableModelValidation="True" OnRowEditing="gvSheetMetalParts_RowEditing" OnRowDeleting="gvSheetMetalParts_RowDeleting">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sheet Metal ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsheetmetalid" runat="server" Text='<%# Eval("sheetmetalid") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemStyle />
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesc" runat="server" Text='<%# Eval("sheetmetaldesc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Gauge" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGauge" runat="server" Text='<%# Eval("gaugesize") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Width" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("widthsize") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Length" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLength" runat="server" Text='<%# Eval("lengthsize") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="Current Stock">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCurrentStock" runat="server" Text='<%# Eval("currentstock") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-CssClass="ws-nowrap" FooterStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CssClass="btn btn-primary btn-sm" Text="Edit"><i class="far fa-edit" title="Edit"></i></asp:LinkButton>
                                            <asp:LinkButton CssClass="btn btn-info  btn-sm btn-danger" ID="Delete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure.?');"><i class="fas fa-times" title="Delete"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle />

                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
              <div class="col-12" id="divsheetmetalwarehouse" runat="server" visible="false">
                <div id="Div3" runat="server" class="row border-top pt-3">
                    <div class="col-sm-12">
                        <h5 class="text-uppercase">Sheet Metal Warehouse Summary</h5>
                    </div>
                    <div class="col-12">

                        <div class="table-responsive">
                            <asp:GridView BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                ForeColor="Black" GridLines="Vertical" Width="100%" DataKeyNames="SheetDetailID,SheetMetalID"
                                ID="gvSheetWarehouseDetails" runat="server" AutoGenerateColumns="False" CssClass="table mainGridTable table-sm"
                                EnableModelValidation="True" OnRowEditing="gvSheetWarehouseDetails_RowEditing" OnRowDeleting="gvSheetWarehouseDetails_RowDeleting">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <Columns>                  
                                    <asp:TemplateField HeaderText="Warehouse">
                                        <ItemStyle />
                                        <ItemTemplate>
                                            <asp:Label ID="lblSMWarehouse" runat="server" Text='<%# Eval("WarehouseName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField HeaderText="Min" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMin" runat="server" Text='<%# Eval("minqty") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Max" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMax" runat="server" Text='<%# Eval("maxqty") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lead Time" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLeadTime" runat="server" Text='<%# Eval("leadtime") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty On Skid" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQtyonSkid" runat="server" Text='<%# Eval("qtyonskid") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>                                  
                                    <asp:TemplateField ItemStyle-CssClass="ws-nowrap" FooterStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CssClass="btn btn-primary btn-sm" Text="Edit"><i class="far fa-edit" title="Edit"></i></asp:LinkButton>
                                            <asp:LinkButton CssClass="btn btn-info  btn-sm btn-danger" ID="Delete" runat="server" CommandName="Delete" OnClientClick="return confirm('Are you sure.?');"><i class="fas fa-times" title="Delete"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle />

                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12" id="divWarehouse" runat="server" visible="false">
                <div id="Div1" runat="server" class="row border-top pt-3">
                    <div class="col-sm-12">
                        <h5 class="text-uppercase">Sheet Metal Warehouse Stock Details</h5>
                    </div>
                    <div class="col-4">
                        <div class="table-responsive">
                            <asp:GridView BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                ForeColor="Black" GridLines="Vertical" Width="100%"
                                ID="gvSheetMetalWarehouse" runat="server" AutoGenerateColumns="False" CssClass="table mainGridTable table-sm"
                                EnableModelValidation="True">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Warehouse Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWarehouseName" runat="server" Text='<%# Eval("WarehouseName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity">
                                        <ItemStyle />
                                        <ItemTemplate>
                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
                <asp:HiddenField ID="hfSheetDetailID" runat="server" Value="-1" />
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
            $('#<%=ddlPartNumber.ClientID%>').chosen();
            $('#<%=ddlGauge.ClientID%>').chosen();
            $('#<%=ddlLength.ClientID%>').chosen();
            $('#<%=ddlWidth.ClientID%>').chosen();
            $('#<%=ddlWarehouse.ClientID%>').chosen();
            $('#<%=ddlSheetMetalStatus.ClientID%>').chosen();
        }
    </script>

</asp:Content>
