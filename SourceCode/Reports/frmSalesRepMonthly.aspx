<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="frmSalesRepMonthly.aspx.cs" Inherits="Reports_frmSalesRepMonthly" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <div class="col-md-auto mx-auto innerMain">
                <div class="row pt-3 flex-column">
                    <div class="col-12">
                        <h4 class="title-hyphen position-relative mb-3">Sales Rep Monthly Project Tracking Report</h4>
                    </div>
                    <%--            <div class="col-12"><div class="alert alert-danger" role="alert" runat="server" id="divError" visible="false">Error message</div></div>--%>
                    <div class="col-12 row">
                        <div class="row col-7">
                            <div class="col-sm-auto">
                                <div class="form-group chosenFullWidth">
                                    <label>Project Manager</label>
                                    <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlProjectManager" runat="server" DataValueField="id" DataTextField="text" autocomplete="off"></asp:DropDownList>

                                </div>
                            </div>
                            <div class="col-sm-auto">
                                <div class="form-group">
                                    <label>Start Date</label>
                                    <asp:TextBox CssClass="form-control form-control-sm" ID="txtFromDate" runat="server" OnBlur="validateDate(this)" autocomplete="off"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtFromDate" TargetControlID="txtFromDate"></asp:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-sm-auto">
                                <div class="form-group">
                                    <label>End Date</label>
                                    <asp:TextBox CssClass="form-control form-control-sm" ID="txtToDate" runat="server" OnBlur="validateDate(this)" autocomplete="off"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtToDate" TargetControlID="txtToDate"></asp:CalendarExtender>
                                </div>
                            </div>
                            <%--                 <div class="col-sm-12">
                                <div class="form-group mb-0 flex-column">
                                    <label>&nbsp;</label>
                                    <div>
                                        <asp:Button CssClass="btn btn-secondary btn-sm" ID="btnGenerate" runat="server" CausesValidation="false" Text="Preview" OnClick="btnGenerate_Click" />
                                        <asp:Button CssClass="btn btn-primary btn-sm" ID="btnExporttoExcel" runat="server" CausesValidation="false" OnClientClick="window.document.forms[0].target='_blank';" Text="Export to Excel" OnClick="btnExporttoExcel_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-danger btn-sm" runat="server" Text="Cancel" CausesValidation="false" OnClick="btnCancel_Click" />

                                    </div>

                                </div>
                                <div class="col-6">
                                    <strong class="text-center">
                                        <asp:Label CssClass="alert alert-success d-block py-1" ID="lblRecordsCount" runat="server" Text="Label" Visible="false"></asp:Label></strong>
                                </div>
                            </div>--%>
                            <div class="col-sm-12">
                                <div class="d-flex justify-content-between align-items-center">

                                    <!-- Buttons -->
                                    <div>
                                        <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-secondary btn-sm" Text="Preview" CausesValidation="false"
                                            OnClick="btnGenerate_Click" />

                                        <asp:Button ID="btnExporttoExcel" runat="server" CssClass="btn btn-primary btn-sm" Text="Export to Excel"
                                            CausesValidation="false"                                            
                                            OnClick="btnExporttoExcel_Click" />

                                        <asp:Button ID="btnCancel" runat="server"
                                            CssClass="btn btn-danger btn-sm"
                                            Text="Cancel"
                                            CausesValidation="false"
                                            OnClick="btnCancel_Click" />
                                    </div>

                                    <!-- Record Count -->
                                    <div>
                                        <asp:Label ID="lblRecordsCount"
                                            runat="server"
                                            CssClass="alert alert-success mb-0 py-1 px-3"
                                            Visible="false">
                                        </asp:Label>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div class="col-5 pl-0">
                            <div class="row">
                                <div class="col-12 pt-2">
                                    <div class="d-flex align-items-center mb-2">
                                        <h5>
                                            <strong>Help Section: Sales Rep Monthly Project Tracking Report</strong>
                                        </h5>
                                    </div>
                                </div>
                                <div class="col-12">
                                    <ul>
                                        <li>Date is based on <strong>Proposal Date</strong>.</li>
                                    </ul>
                                </div>
                            </div>
                        </div>

                        <div class="col-12">
                            <div class="row pt-3">
                                <div class="col-sm-12">
                                    <asp:GridView ID="gvSalesRepMonthly" CssClass="table mainGridTable table-sm mb-0" DataKeyNames="PROPOSAL #" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
                                        AutoGenerateColumns="False" EnableModelValidation="True" Style="font-size: small" ForeColor="Black" GridLines="Vertical" Width="100%" AllowSorting="true"
                                        OnSorting="gvSalesRepMonthly_Sorting" OnRowDataBound="gvSalesRepMonthly_RowDataBound" OnRowCommand="gvSalesRepMonthly_RowCommand">
                                        <AlternatingRowStyle BackColor="#CCCCCC" />
                                        <Columns>
                                            <asp:BoundField DataField="Proposal #" HeaderText="P#" SortExpression="Proposal #">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Job ID #" HeaderText="J#" SortExpression="Job ID #">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Name & Building" HeaderText="Project Name" SortExpression="Name & Building">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Market Segment" HeaderText="Industry" SortExpression="Market Segment">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="State" HeaderText="State" SortExpression="State">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Dollers" HeaderText="Net Eq. Price" DataFormatString="{0:C2}" SortExpression="Dollers">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Office" HeaderText="Consultant" SortExpression="Office">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Lead Designer" HeaderText="Consultant Member" SortExpression="Lead Designer" Visible="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Conveyor Prime Spec" HeaderText="Prime Spec" SortExpression="Conveyor Prime Spec" Visible="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Conveyor Alt 1" HeaderText="Alt 1" SortExpression="Conveyor Alt 1" Visible="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Blower Dryer Prime Spec" HeaderText="Prime Spec" SortExpression="Blower Dryer Prime Spec" Visible="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Blower Dryer Alt 1" HeaderText="Alt 1" SortExpression="Blower Dryer Alt 1" Visible="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Waste Collector Prime Spec" HeaderText="Prime Spec" SortExpression="Waste Collector Prime Spec" Visible="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Waste Collector Alt 1" HeaderText="Alt 1" SortExpression="Waste Collector Alt 1" Visible="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Branch" HeaderText="Branch" SortExpression="Branch" Visible="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Project Manager" HeaderText="Project Manager" SortExpression="Project Manager">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Consultant Rep" HeaderText="Consultant Rep" SortExpression="Consultant Rep">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Origination Rep" HeaderText="Origination Rep" SortExpression="Origination Rep" Visible="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Destination Rep" HeaderText="Destination Rep" SortExpression="Destination Rep" Visible="false">
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">

                $(document).ready(function () {
                    BindDrp();
                });

                function BindDrp() {
                    var ddl = $('#<%= ddlProjectManager.ClientID %>');

                    // Destroy existing Chosen instance if already initialized
                    if (ddl.data('chosen')) {
                        ddl.chosen('destroy');
                    }

                    // Initialize Chosen
                    ddl.chosen({
                        width: "100%",
                        search_contains: true,
                        allow_single_deselect: true
                    });
                }

                // Reinitialize Chosen after every UpdatePanel postback
                Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                    BindDrp();
                });
                var opennewwindow = function (ds) {
                    window.location = "../SalesManagement/FrmProposals.aspx?ds=" + ds;
                }
            </script>

        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
