<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmSheetMetalJobConsumption.aspx.cs" Inherits="SheetMetalForecasting_FrmSheetMetalJobConsumption" %>

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
                            <h4 class="title-hyphen position-relative">Job Sheet Metal Consumption Form</h4>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-7 col-md-8 col-lg-3 col-xl-3" style="display: none">
                        <div class="row">
                            <div class="col-sm-3 col-md-auto mb-3">
                                <label class="mb-0">Lookup Project Code</label>
                            </div>
                            <div class="col-sm-6 col-md mb-3 chosenFullWidth">
                                <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlLookupProjectCode" runat="server" DataTextField="text" DataValueField="id" AutoPostBack="true" OnSelectedIndexChanged="ddlLookupProjectCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <%--<div class="col-12">
                <div class="row pt-3">
                    <div class="col-sm-12">
                        <h5 class="text-uppercase">Job Sheet Metal Consumption Details</h5>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Project Code*</label>
                            <asp:DropDownList ID="ddlProjectCode" CssClass="form-control form-control-sm" runat="server" DataTextField="text" DataValueField="id" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectCode_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2" id="dvJob" runat="server" visible="false">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Job ID*</label>
                            <asp:Panel ID="Panel1" runat="server" Style="height: 200px; overflow: scroll; display: none;">
                            </asp:Panel>
                            <asp:Panel ID="PanelJNum" runat="server" DefaultButton="SearchJNumberButton">
                                <asp:TextBox ID="txtSearchPNum" AutoComplete="off" placeholder="Type Job Number" CssClass="form-control form-control-sm" OnBlur="return ClickEvent(event)" runat="server">
                                </asp:TextBox>
                                <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtSearchPNum"
                                    CompletionInterval="3" CompletionSetCount="10" MinimumPrefixLength="3" CompletionListElementID="Panel1"
                                    ServicePath="../AutoComplete.asmx" ServiceMethod="SearchJobNumberOnly" CompletionListCssClass="autocomplete" />
                                <asp:Button ID="SearchJNumberButton" runat="server" Text="Submit" Style="display: none" OnClick="txtSearchPNum_TextChanged" />
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Warehouse*</label>
                            <asp:DropDownList ID="ddlWarehouse" CssClass="form-control form-control-sm" runat="server" DataTextField="text" DataValueField="id">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-3">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Sheet Metal ID*</label>
                            <asp:DropDownList ID="ddlSheetMetal" CssClass="form-control form-control-sm" runat="server" DataTextField="text" DataValueField="id">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-6 col-sm-4 col-md-2 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Date*</label>
                            <asp:TextBox ID="txtDate" CssClass="form-control form-control-sm" runat="server" OnBlur="validateDate(this)" autocomplete="off">
                            </asp:TextBox>
                            <asp:CalendarExtender ID="cal_txtDate" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtDate" TargetControlID="txtDate"></asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-2s col-lg-1">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Qty*</label>
                            <asp:TextBox ID="txtTransactQty" CssClass="form-control form-control-sm text-right" runat="server" MaxLength="5" onkeypress="return onlyNumbers(event);">                                
                            </asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-auto">
                        <asp:Button ID="btnSave"
                            runat="server"
                            CssClass="btn btn-success btn-sm me-1"
                            CausesValidation="false"
                            Text="Save"
                            OnClick="btnSave_Click" />

                        <asp:Button ID="btnCancel"
                            runat="server"
                            CssClass="btn btn-danger btn-sm"
                            Text="Cancel"
                            OnClick="btnCancel_Click" />
                    </div>                    

                </div>
            </div>--%>
            <div class="col-12">
                <div class="row pt-3">

                    <div class="col-sm-12">
                        <h5 class="text-uppercase">Job Sheet Metal Consumption Details</h5>
                    </div>

                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Project Code*</label>
                            <asp:DropDownList ID="ddlProjectCode"
                                CssClass="form-control form-control-sm"
                                runat="server"
                                DataTextField="text"
                                DataValueField="id"
                                AutoPostBack="true"
                                OnSelectedIndexChanged="ddlProjectCode_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-6 col-sm-4 col-md-3 col-lg-2"
                        id="dvJob"
                        runat="server"
                        visible="false">

                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Job ID*</label>

                            <asp:Panel ID="Panel1"
                                runat="server"
                                Style="height: 200px; overflow: scroll; display: none;">
                            </asp:Panel>

                            <asp:Panel ID="PanelJNum"
                                runat="server"
                                DefaultButton="SearchJNumberButton">

                                <asp:TextBox ID="txtSearchPNum"
                                    AutoComplete="off"
                                    placeholder="Type Job Number"
                                    CssClass="form-control form-control-sm"
                                    OnBlur="return ClickEvent(event)"
                                    runat="server">
                                </asp:TextBox>

                                <asp:AutoCompleteExtender
                                    ID="AutoCompleteExtender2"
                                    runat="server"
                                    TargetControlID="txtSearchPNum"
                                    CompletionInterval="3"
                                    CompletionSetCount="10"
                                    MinimumPrefixLength="3"
                                    CompletionListElementID="Panel1"
                                    ServicePath="../AutoComplete.asmx"
                                    ServiceMethod="SearchJobNumberOnly"
                                    CompletionListCssClass="autocomplete" />

                                <asp:Button ID="SearchJNumberButton"
                                    runat="server"
                                    Text="Submit"
                                    Style="display: none"
                                    OnClick="txtSearchPNum_TextChanged" />

                            </asp:Panel>
                        </div>
                    </div>

                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Warehouse*</label>
                            <asp:DropDownList ID="ddlWarehouse"
                                CssClass="form-control form-control-sm"
                                runat="server"
                                DataTextField="text"
                                DataValueField="id">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-6 col-sm-4 col-md-3 col-lg-3">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Sheet Metal ID*</label>
                            <asp:DropDownList ID="ddlSheetMetal"
                                CssClass="form-control form-control-sm"
                                runat="server"
                                DataTextField="text"
                                DataValueField="id">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-6 col-sm-4 col-md-2 col-lg-2">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Date*</label>

                            <asp:TextBox ID="txtDate"
                                CssClass="form-control form-control-sm"
                                runat="server"
                                OnBlur="validateDate(this)"
                                autocomplete="off">
                            </asp:TextBox>

                            <asp:CalendarExtender
                                ID="cal_txtDate"
                                runat="server"
                                Format="MM/dd/yyyy"
                                PopupButtonID="txtDate"
                                TargetControlID="txtDate">
                            </asp:CalendarExtender>

                        </div>
                    </div>

                    <div class="col-6 col-sm-4 col-md-2 col-lg-1">
                        <div class="form-group">
                            <label class="pl-0 col-12 text-danger">Qty*</label>

                            <asp:TextBox ID="txtTransactQty"
                                CssClass="form-control form-control-sm text-right"
                                runat="server"
                                MaxLength="5"
                                onkeypress="return onlyNumbers(event);">
                            </asp:TextBox>

                        </div>
                    </div>

                    <!-- BUTTONS -->
                    <div class="col-12 col-sm-auto d-flex align-items-end pb-3">
                        <asp:Button ID="btnSave"
                            runat="server"
                            CssClass="btn btn-success btn-sm mr-2"
                            CausesValidation="false"
                            Text="Save"
                            OnClick="btnSave_Click" />

                        <asp:Button ID="btnCancel"
                            runat="server"
                            CssClass="btn btn-danger btn-sm"
                            Text="Cancel"
                            OnClick="btnCancel_Click" />
                    </div>

                </div>
            </div>
            <div class="col-12">
                <div id="pangvRequititionDetails" runat="server" class="row border-top pt-3" visible="false">
                    <div class="col-sm-12">
                        <h5 class="text-uppercase">Job Sheet Metal Consumption Summary</h5>
                    </div>
                    <div class="col-12">
                        <div class="table-responsive">
                            <asp:GridView BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3"
                                ForeColor="Black" GridLines="Vertical" Width="100%" DataKeyNames="ConsumptionID,SheetMetalID,ProductCodeID,WareHouseID"
                                ID="gvJobSummary" runat="server" AutoGenerateColumns="False" CssClass="table mainGridTable table-sm"
                                EnableModelValidation="True" OnRowEditing="gvSheetMetalParts_RowEditing" OnRowDeleting="gvSheetMetalParts_RowDeleting" OnRowDataBound="gvJobSummary_RowDataBound">
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Job ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobNo" runat="server" Text='<%# Eval("JobID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job Name">
                                        <ItemStyle />
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobName" runat="server" Text='<%# Eval("JobName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Warehouse" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWarehouse" runat="server" Text='<%# Eval("WarehouseName") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sheet Metal Desc" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSheetMetal" runat="server" Text='<%# Eval("SheetMetalDesc") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSheetMetaldate" runat="server" Text='<%# Eval("Date") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transact Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactQty" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
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
            <asp:HiddenField ID="hfConsumptionID" runat="server" Value="-1" />
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
            $('#<%=ddlLookupProjectCode.ClientID%>').chosen();
            $('#<%=ddlProjectCode.ClientID%>').chosen();
            $('#<%=ddlSheetMetal.ClientID%>').chosen();
            $('#<%=ddlWarehouse.ClientID%>').chosen();
        }

        function ClickEvent(e) {
            __doPostBack('<%=SearchJNumberButton.UniqueID%>', "");
        }
    </script>
</asp:Content>
