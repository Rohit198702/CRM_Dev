<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmAdvancedSearchProposal.aspx.cs" Inherits="SalesManagement_FrmAdvancedSearchProposal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <div class="col-12">
                <div class="row">
                    <div class="col-12 pt-2">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Advanced Search Proposal</h4>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-6">
                        <div class="form-group">
                            <label>Keywords</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtKeyword" placeholder="open proposals in last 2 months above 100000 for pm prateekkalsi, .... etc" AutoComplete="off" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-4">
                        <label>&nbsp;</label>
                        <div class="form-group">
                            <asp:Button ID="btnSearchProposal" runat="server" CssClass="btn btn-secondary btn-sm" Text="Search" OnClick="btnSearchProposal_Click" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
                            <asp:Button ID="btnExportToExcel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Enabled="false" Text="Export to Excel" OnClick="btnExportToExcel_Click" />
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md justify-content-center">
                        <strong class="text-center">
                            <asp:Label CssClass="alert alert-success d-block py-1" ID="lblRecordsCount" runat="server" Text="Label" Visible="false"></asp:Label>
                        </strong>
                    </div>
                </div>

                <div class="row px-3">                    
                    <details>        
                        <summary style="font-weight:bold; font-size:14px; cursor:pointer;">
                            🔍 Advanced Search Help
                        </summary>

                        <div style="margin-top:15px;">

                            <!-- Date Filters -->
                            <h5 class="font-weight-bold ">📅 Date Filters</h5>
                            <ul>
                                <li><b>Between:</b> between 01/01/2024 and 02/15/2024</li>
                                <li>
                                    <b>Relative:</b>
                                    last 7 days, last 1 day,
                                    past 2 weeks, previous 3 weeks,
                                    last 2 months, past 6 months,
                                    last 1 year, previous 2 years,
                                    last week, last month, last year
                                </li>
                            </ul>

                            <h5 class="font-weight-bold mt-2">💰 Price</h5>
                            <div style="margin-left:10px;">
                                equal 1000<br/>                               
                                below 3000<br/>                              
                                above 5000<br/>                                
                            </div>

                            <!-- Project Manager -->
                            <h5 class="font-weight-bold mt-2">👤 Project Manager</h5>
                            <div style="margin-left:10px;">
                                my<br/>
                                pm johndoe (without space)(partial allowed)
                            </div>

                                <!-- Location -->
                            <h5 class="font-weight-bold mt-2">📍 Location</h5>
                            <div style="margin-left:10px;">
                                country usa(without space)(partial allowed)<br/>
                                state newyork(without space)(partial allowed)<br/>
                                city newyorkcity(without space)(partial allowed)
                            </div>

                                <!-- Order For -->
                            <h5 class="font-weight-bold mt-2">🏢 Order For</h5>
                            <div style="margin-left:10px;">
                                order for aerowerks<br/>
                                order for tragenflex<br/>
                                order for aero (partial match)
                            </div>

                            <!-- Consultant / Dealer -->
                            <h5 class="font-weight-bold mt-2">👥 Consultant / Dealer</h5>
                            <div style="margin-left:10px;">
                                consultant john (without space)(partial allowed)<br/>
                                dealer abc (without space)(partial allowed)
                            </div>

                            <!-- Sales Rep -->
                            <h5 class="font-weight-bold mt-2">🧑‍💼 Sales Rep</h5>
                            <div style="margin-left:10px;">
                                rep john (without space)(partial allowed)
                            </div>

                            <!-- Industry / Spec / Model -->
                            <h5 class="font-weight-bold mt-2">🏭 Industry / Spec / Model</h5>
                            <div style="margin-left:10px;">
                                industry oil (without space)(partial allowed)<br/>
                                primespec xyz (without space)(partial allowed)<br/>
                                model sdt (only one model)
                            </div>

                            <!-- Project / Numbers -->
                            <h5 class="font-weight-bold mt-2">📄 Project</h5>
                            <div style="margin-left:10px;">
                                pn bridge (without space)(partial allowed)<br/>
                                p# P011986 (without space)(partial allowed)<br/>
                                j# J12345 (without space)(partial allowed)
                            </div>

                            <!-- Examples -->
                            <h5 class="font-weight-bold mt-2">🧠 Examples</h5>
                            <div>
                                consultant john last 6 months<br/>
                                order for aero between 01/01/2026 and 02/15/2026<br/>
                                rep smith last 30 days<br/>
                                open proposals in the last 2 months for pm prateekkalsi for state newyork<br/>
                            </div>

                        </div>
                    </details>
                </div>

                <div class="row">
                    <div class="col-12" style="overflow-x: auto; max-height: 400px; overflow-y: auto;">
                        <asp:GridView ID="gvProposalSearch" runat="server" AutoGenerateColumns="False" Visible="false" BackColor="White" BorderColor="#999999" BorderStyle="Solid"
                            BorderWidth="1px" CellPadding="3" DataKeyNames="PNumber" EnableModelValidation="True" ForeColor="Black" GridLines="Vertical" Width="100%"
                            CssClass="table mainGridTable table-sm"
                            Style="font-size: small" AllowSorting="true" OnSorting="gvProposalSearch_Sorting">
                            <AlternatingRowStyle BackColor="#CCCCCC" />
                            <Columns>
                                <asp:BoundField DataField="PNumber" HeaderText="Proposal#" SortExpression="PNumber">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="JobID" HeaderText="Job#" SortExpression="JobID">
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ProjectName" HeaderText="Project Name" SortExpression="ProjectName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ProposalDate" HeaderText="Proposal Date" SortExpression="ProposalDate">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="City" HeaderText="City" SortExpression="City">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="State" HeaderText="State" SortExpression="State">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C2}" SortExpression="Price">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Installation" HeaderText="Installation" DataFormatString="{0:C2}" SortExpression="Installation" Visible="false">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Freight" HeaderText="Freight" DataFormatString="{0:C2}" SortExpression="Freight" Visible="false">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OrderBelongsTo" HeaderText="Order For" SortExpression="OrderBelongsTo">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ConveyorType" HeaderText="Conveyor Type" SortExpression="ConveyorType" Visible="false">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Models" HeaderText="Model" SortExpression="Models">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="CompetitorName" HeaderText="Prime Spec" SortExpression="CompetitorName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ConsultantName" HeaderText="Consultant" SortExpression="ConsultantName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DealerName" HeaderText="Dealer" SortExpression="DealerName">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="SalesRep" HeaderText="Sales Rep" SortExpression="SalesRep">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Manager" HeaderText="Manager" SortExpression="Manager">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Industry" HeaderText="Industry" SortExpression="Industry">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
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
        </ContentTemplate>
         <Triggers>
            <asp:PostBackTrigger ControlID="btnExportToExcel" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
