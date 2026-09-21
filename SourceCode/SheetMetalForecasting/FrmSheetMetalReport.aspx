<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="FrmSheetMetalReport.aspx.cs" Inherits="SheetMetalForecasting_FrmSheetMetalReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <div class="col-12">
                <div class="row">
                    <div class="col-12 pt-2">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Sheet Metal Information</h4>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-6 col-sm-4 col-md-3 col-lg-4">
                        <div class="form-group">
                            <label>Sheet Metal ID</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlSheetMetalID" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label>Warehouse</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlWarehouse" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>     
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label>Length</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlLength" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label>Width</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlWidth" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label>Status</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlStatus" runat="server">
                                <asp:ListItem Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">Current</asp:ListItem>
                                <asp:ListItem Value="2">Obsolete</asp:ListItem>
                                <asp:ListItem Value="3">Not In Use</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-md-auto">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary btn-sm" Text="Search" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnExporttoExcel" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false" OnClientClick="window.document.forms[0].target='_blank';setTimeout(function(){document.forms[0].target='';},0);" Text="Export To Excel" OnClick="btnExporttoExcel_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />

                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12" runat="server">
                <div id="pangvRequititionDetails" runat="server" class="row border-top pt-3 mt-2">
                    <div class="col-12">
                        <div class="table-responsive">
                            <asp:GridView EmptyDataText="No Records Found" DataKeyNames="ID"
                                ID="gvJobSheetForecasting" runat="server" AutoGenerateColumns="false" CssClass="table mainGridTable table-sm mb-0"
                                EnableModelValidation="True" OnRowCommand="gvJobSheetForecasting_RowCommand" OnRowDataBound="gvJobSheetForecasting_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Part ID#">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsheetmetalid" runat="server" Text='<%# Eval("sheetmetalid") %>'></asp:Label>
                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Part Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsheetmetaldesc" runat="server" Text='<%# Eval("sheetmetaldesc") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>            
                                    <asp:TemplateField HeaderText="Width">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("width") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Length">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLength" runat="server" Text='<%# Eval("length") %>'></asp:Label>
                                        </ItemTemplate>
                                       <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Stock In Hand">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStockinhand" runat="server" Text='<%# Eval("currentstock") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <asp:ModalPopupExtender ID="SheetMetalModel" runat="server" TargetControlID="lnkSheetDetailButton"
                PopupControlID="panelForModal" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="panelForModal" runat="server" CssClass="ReportsModalPopup" Style="display: none;" Width="60%" Height="60%">
                <div class="position-relative h-100">
                    <asp:ImageButton CssClass="position-absolute crossCloseBtn" ID="btnClose" runat="server" ImageUrl="../images/closebtnCircle.png"
                        AlternateText="Close Popup" ToolTip="Close Popup" />
                    <div class="overflow-auto h-100">
                        <%-- Title --%>
                        <div class="row justify-content-center col-12">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-sm-4 col-md-auto mb-3 modal-title text-center">
                                        <h5>
                                            <label class="mb-0 title-hyphen position-relative">Sheet Metal ID:</label>
                                        </h5>
                                    </div>
                                    <div class="col-sm-8 col-md mb-3 chosenFullWidth ">
                                        <h5>
                                            <asp:Label ID="lblSheetMetalTitleInModal" runat="server"></asp:Label>
                                        </h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-12">
                            <div class="table-responsive">
                                <asp:GridView CssClass="table mainGridTable table-sm mb-0" ID="gvSheetMetalWarehouseDeatils" runat="server" AutoGenerateColumns="true"
                                    EnableModelValidation="True" EmptyDataText="No Data Found">
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
            <asp:LinkButton ID="lnkSheetDetailButton" runat="server"></asp:LinkButton>
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
                    $('#<%=ddlSheetMetalID.ClientID%>').chosen();
                    $('#<%=ddlLength.ClientID%>').chosen();
                    $('#<%=ddlWidth.ClientID%>').chosen();                    
                    $('#<%=ddlStatus.ClientID%>').chosen();
                    $('#<%=ddlWarehouse.ClientID%>').chosen();
                }
            </script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExporttoExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
