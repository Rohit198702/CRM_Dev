<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmSalesActivityReport_V2.aspx.cs" Inherits="Reports_FrmSalesActivityReport_V2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content_SalesActivity" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel_SalesActivity" runat="server">
        <ContentTemplate>
            <div class="col-12 pt-2 piDiv position-sticky">
                <div class="row">
                    <div class="col-12">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Manufacturer Specification Report</h4>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Project Manager</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlProjectManager" runat="server" DataTextField="text" DataValueField="id">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="form-group">
                            <label>Other filter</label>
                            <asp:ListBox CssClass="form-control form-control-sm" ID="ddlOtherFilter" SelectionMode="multiple" runat="server">
                                <%--<asp:ListItem Value="0">All</asp:ListItem>--%>
                                <asp:ListItem Value="1">Alternate Specification Projects</asp:ListItem>
                                <asp:ListItem Value="2">Prime Spec Projects with Alternate</asp:ListItem>
                                <asp:ListItem Value="3">Prime Spec Projects without Alternate</asp:ListItem>
                            </asp:ListBox>
                        </div>
                    </div>

                    <div class="col-sm-2" style="display: none;">
                        <div class="form-group">
                            <label>Filter data on</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlDateType" runat="server" OnSelectedIndexChanged="ddlDateType_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="1">Proposal Date</asp:ListItem>
                                <asp:ListItem Value="0">Activity Date</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-2">
                        <div class="form-group">
                            <label id="lblFrom" runat="server">Followup Date From</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtDateFrom" runat="server" AutoComplete="off" OnBlur="validateDate(this)"></asp:TextBox>
                            <asp:CalendarExtender ID="txtDateFrom_Extender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtDateFrom" TargetControlID="txtDateFrom"></asp:CalendarExtender>

                        </div>
                    </div>

                    <div class="col-2">
                        <div class="form-group">
                            <label id="lblTo" runat="server">Followup Date To</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtDateTo" runat="server" AutoComplete="off" OnBlur="validateDate(this)"></asp:TextBox>
                            <asp:CalendarExtender ID="txtDateTo_Extender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtDateTo" TargetControlID="txtDateTo"></asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-sm-4 pl-0">
                        <div class="form-group">
                            <%--<label>&nbsp;</label>--%>
                            <div class="col-auto">
                                <asp:Button ID="btnReport" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false" OnClick="btnReport_Click" OnClientClick="window.document.forms[0].target='_blank';" Text="Generate Report" />
                                <asp:Button ID="btnGenerateExcel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Text="Export to Excel" OnClick="btnGenerateExcel_Click" OnClientClick="window.document.forms[0].target='_blank';"/>
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12 mt-3" style="display:none;">
                <div class="table-responsive eoeTable">
                    <asp:GridView ID="gvExportToExcel" runat="server" CellPadding="3" EmptyDataText="No Items Found" Width="100%" CssClass="table mainGridTable table-sm"
                        EnableModelValidation="True" ShowFooter="false" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="PM" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectManager" runat="server" Text='<%# Eval("Project Manager") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="P#" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblPNumber" runat="server" Text='<%# Eval("P-Number") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Project Name" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("Project Name") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="500px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Followup Date" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblFollowupDate" runat="server" Text='<%# Eval("EffectiveDate", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Expected Ship Date" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblShipDate" runat="server" Text='<%# Eval("ShipDate", "{0:MM/dd/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Equipment Price" Visible="true" HeaderStyle-CssClass="text-right">
                                <ItemTemplate>
                                    <asp:Label ID="lblEqPrice" runat="server" Text='<%# Eval("NetEqPrice", "${0:N2}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="150px" HorizontalAlign="Right" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Prime Spec" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblPrimeSpec" runat="server" Text='<%# Eval("PrimeSpec") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="150px" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Alternate" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblAlternate" runat="server" Text='<%# Eval("Alternate") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="150px" />
                            </asp:TemplateField>   
                            
                            <asp:TemplateField HeaderText="Dealer" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblDealer" runat="server" Text='<%# Eval("Dealer") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="300px" />
                            </asp:TemplateField> 
                            
                            <asp:TemplateField HeaderText="Consultant" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblConsultant" runat="server" Text='<%# Eval("Consultant") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="300px" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Notes" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblNotes" runat="server" Text='<%# Eval("Notes") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="300px" />
                            </asp:TemplateField>                  

                            <asp:TemplateField HeaderText="Update Status - This is an added field. (Open/Closed/Won/Lost)" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommentary" runat="server" Text=''></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="400px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnReport" />
            <asp:PostBackTrigger ControlID="btnGenerateExcel" />
        </Triggers>
    </asp:UpdatePanel>
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
            $('#<%=ddlProjectManager.ClientID%>').chosen();
            $('#<%=ddlDateType.ClientID%>').chosen();
            $('#<%=ddlOtherFilter.ClientID%>').chosen();
        }
    </script>
</asp:Content>
