<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmActivityKPIPerformanceSummary.aspx.cs" Inherits="Reports_FrmActivityKPIPerformanceSummary" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <div class="col-12">
                <div class="row pt-3">
                    <div class="col-12">
                        <h5 class="text-uppercase">KPI Performace Summary</h5>
                    </div>

                    <div class="col-2">
                        <div class="form-group">
                            <label>Quarter</label>
                            <asp:DropDownList ID="ddlMonthHeaderList" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-md-auto">
                                <asp:Button CssClass="btn btn-success btn-sm" ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnExportToExcel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Text="Export To Excel" OnClick="btnExportToExcel_Click" />
                                <asp:Button ID="btnRedirect" runat="server" CssClass="btn btn-secondary btn-sm" Text="Feedback" OnClick="btnRedirect_Click" />
                                <%--<asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger btn-sm" Text="Clear Search" OnClick="btnClear_Click" />--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12 mt-3">
                <div class="table-responsive eoeTable">
                    <asp:GridView ID="gvSearch" runat="server" CellPadding="3" EmptyDataText="No Items Found" Width="100%" CssClass="table mainGridTable table-sm"
                        EnableModelValidation="True" ShowFooter="false" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderText="KPI" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblKPI" runat="server" Text='<%# Eval("KPI") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="300px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Ed" Visible="true" HeaderStyle-CssClass="text-right">
                                <ItemTemplate>
                                    <asp:Label ID="lblEd" runat="server" Text='<%# Eval("Ed") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" HorizontalAlign="Right" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Lisa" Visible="true" HeaderStyle-CssClass="text-right">
                                <ItemTemplate>
                                    <asp:Label ID="lblLisa" runat="server" Text='<%# Eval("Lisa") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" HorizontalAlign="Right"/>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Target" Visible="true" HeaderStyle-CssClass="text-right">
                                <ItemTemplate>
                                    <asp:Label ID="lblTarget" runat="server" Text='<%# Eval("Target") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" HorizontalAlign="Right"/>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status(Met/Not Met)" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="200px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Commentary(Mandatory if Not Met)" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCommentary" runat="server" Text='<%# Eval("Commentary") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="500px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <div class="col-12 mt-3">
                <div class="table-responsive eoeTable">
                    <asp:GridView ID="gvFeedback" runat="server" CellPadding="3" EmptyDataText="No Items Found" Width="100%" CssClass="table mainGridTable table-sm"
                        EnableModelValidation="True" ShowFooter="false" AutoGenerateColumns="false" Visible="false">
                        <Columns>
                            <asp:TemplateField HeaderText="BDM" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblBDM" runat="server" Text='<%# Eval("BDM") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>                      

                            <asp:TemplateField HeaderText="Review Date" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblReviewDate" runat="server" Text='<%# Eval("ReviewDate") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="100px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Accomplishments" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblAccomplishments" runat="server" Text='<%# Eval("Accomplishments") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="500px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Gaps" Visible="true">
                                <ItemTemplate>
                                    <asp:Label ID="lblGaps" runat="server" Text='<%# Eval("Gaps") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="300px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
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
            $('#<%=ddlMonthHeaderList.ClientID%>').chosen();
        }
    </script>
    <CR:CrystalReportViewer ID="rptKPIPerformaceSummary" runat="server" AutoDataBind="true" BestFitPage="False" Width="100%" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
</asp:Content>
