<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" EnableEventValidation="false" CodeFile="FrmActivityManagementDashboard.aspx.cs" Inherits="SalesManagement_FrmActivityManagementDashboard" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="up_ManagementDashboard" runat="server">
        <ContentTemplate>
            <div class="col-12 row">
                <div class="col-2">
                    <div class="form-group">
                        <label>Quarter</label>
                        <asp:DropDownList ID="ddlMonthHeaderList" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlMonthHeaderList_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-2">
                    <div class="form-group">
                        <label>BDM</label>
                        <asp:DropDownList ID="ddlBDMHeaderList" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlBDMHeaderList_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-4">
                    <label>&nbsp;</label>
                    <div class="form-group">
                        <asp:Button ID="btnRedirect" runat="server" CssClass="btn btn-primary btn-sm" Text="KPI Report" OnClick="btnRedirect_Click" /> 
                        <asp:Button ID="btnActivePipelineReport" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false" OnClick="btnActivePipelineReport_Click"
                            OnClientClick="window.document.forms[0].target='_blank';" Text="Active Pipeline Report" />
                    </div>

                </div>
            </div>

            <div class="col-12 row mt-4">
                <!-- Left Section -->
                <div class="col-lg-8 mb-4">
                    <div class="card shadow-sm">
                        <div class="card-header bg-white d-flex justify-content-between align-items-center">
                            <div>
                                <h4 class="mb-0"><%= ddlBDMHeaderList.SelectedItem.Text %></h4>
                                <small class="text-muted"><%= ddlMonthHeaderList.SelectedItem.Text %></small>
                            </div>
                            <%--<asp:Button ID="btnRedirectActivity" runat="server" CssClass="btn btn-primary btn-sm" Text="+ New Activity" OnClick="btnRedirectActivity_Click" />--%>
                        </div>

                        <div class="card-body">
                            <h4>CE Session</h4>

                            <div class="col-12">
                                <asp:GridView ID="gvCESession" runat="server" CssClass="table mainGridTable table-sm mb-2" AutoGenerateColumns="false" EmptyDataText="No CE Session exists"
                                    DataKeyNames="ActivityID" EnableModelValidation="True" OnRowCommand="gvCESession_RowCommand" OnRowDataBound="gvCESession_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Activity#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityNo" runat="server" Text='<%# Eval("Activity#") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Activity Type" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityType" runat="server" Text='<%# Eval("Activity Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Subject">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Company Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("Participants") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <h4>Consultant Engagement</h4>

                            <div class="col-12">
                                <asp:GridView ID="gvConsultantEngagement" runat="server" CssClass="table mainGridTable table-sm mb-2" AutoGenerateColumns="false" EmptyDataText="No CE Consultant Engagement exists"
                                    DataKeyNames="ActivityID" EnableModelValidation="True" OnRowCommand="gvConsultantEngagement_RowCommand" OnRowDataBound="gvConsultantEngagement_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Activity#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityNo" runat="server" Text='<%# Eval("Activity#") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Activity Type" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityType" runat="server" Text='<%# Eval("Activity Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Subject">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Company Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("Participants") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <h4>Dealer Onboarding</h4>

                            <div class="col-12">
                                <asp:GridView ID="gvDealerOnboarding" runat="server" CssClass="table mainGridTable table-sm mb-2" AutoGenerateColumns="false" EmptyDataText="No Dealer Onboarding exists"
                                    DataKeyNames="ActivityID" EnableModelValidation="True" OnRowCommand="gvDealerOnboarding_RowCommand" OnRowDataBound="gvDealerOnboarding_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Activity#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityNo" runat="server" Text='<%# Eval("Activity#") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Activity Type" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityType" runat="server" Text='<%# Eval("Activity Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Subject">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Company Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("Participants") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <h4>Sales Rep</h4>

                            <div class="col-12">
                                <asp:GridView ID="gvSalesRep" runat="server" CssClass="table mainGridTable table-sm mb-2" AutoGenerateColumns="false" EmptyDataText="No Sales Rep exists"
                                    DataKeyNames="ActivityID" EnableModelValidation="True" OnRowCommand="gvSalesRep_RowCommand" OnRowDataBound="gvSalesRep_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Activity#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityNo" runat="server" Text='<%# Eval("Activity#") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Activity Type" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityType" runat="server" Text='<%# Eval("Activity Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Subject">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Company Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("Participants") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <h4>Customer</h4>

                            <div class="col-12">
                                <asp:GridView ID="gvCustomer" runat="server" CssClass="table mainGridTable table-sm mb-2" AutoGenerateColumns="false" EmptyDataText="No Sales Rep exists"
                                    DataKeyNames="ActivityID" EnableModelValidation="True" OnRowCommand="gvCustomer_RowCommand" OnRowDataBound="gvCustomer_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Activity#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityNo" runat="server" Text='<%# Eval("Activity#") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Activity Type" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityType" runat="server" Text='<%# Eval("Activity Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Subject">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Company Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("Participants") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <h4>Event</h4>

                            <div class="col-12">
                                <asp:GridView ID="gvEvent" runat="server" CssClass="table mainGridTable table-sm mb-2" AutoGenerateColumns="false" EmptyDataText="No Event exists"
                                    DataKeyNames="ActivityID" EnableModelValidation="True" OnRowCommand="gvEvent_RowCommand" OnRowDataBound="gvEvent_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Activity#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityNo" runat="server" Text='<%# Eval("Activity#") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Activity Type" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityType" runat="server" Text='<%# Eval("Activity Type") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Subject">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Company Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("Participants") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <h4>Followup</h4>
                            <p class="text-muted mb-2">
                                Rows highlighted with
                               
                                <span style="display: inline-block; padding: 2px 8px; background: #F5C2C7; color: #7A1C1C; border-radius: 4px; font-weight: 600;">red
                                </span>
                                are overdue follow-ups.
                           
                            </p>
                            <div class="col-12">
                                <asp:GridView ID="gvFollowup" runat="server" CssClass="table mainGridTable table-sm mb-2" AutoGenerateColumns="false" EmptyDataText="No Followup exists"
                                    DataKeyNames="ActivityID, FollowUpID" EnableModelValidation="True" OnRowCommand="gvFollowup_RowCommand" OnRowDataBound="gvFollowup_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Followup#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityNo" runat="server" Text='<%# Eval("FollowUpNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Followup Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFollowUpType" runat="server" Text='<%# Eval("FollowUpType") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Followup Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFollowupDate" runat="server" Text='<%# Eval("FollowupDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Subject">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Priority">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPriority" runat="server" Text='<%# Eval("Priority") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Due Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDueDate" runat="server" Text='<%# Eval("DueDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <h4>Spec</h4>

                            <div class="col-12">
                                <asp:GridView ID="gvSpecDetails" runat="server" CssClass="table mainGridTable table-sm mb-2" AutoGenerateColumns="false" EmptyDataText="No Spec exists"
                                    DataKeyNames="ActivityID, SpecID" EnableModelValidation="True" OnRowCommand="gvSpecDetails_RowCommand" OnRowDataBound="gvSpecDetails_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Spec#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpecNo" runat="server" Text='<%# Eval("SpecNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Stage">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectStage" runat="server" Text='<%# Eval("ProjectStage") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Priority">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPriority" runat="server" Text='<%# Eval("Priority") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Next Action Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNextActionDate" runat="server" Text='<%# Eval("NextActionDate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>

                            <h4>Proposals</h4>

                            <div class="col-12">
                                <asp:GridView ID="gvProposals" runat="server" CssClass="table mainGridTable table-sm mb-2" AutoGenerateColumns="false" EmptyDataText="No Proposals exists"
                                    DataKeyNames="PNumber" EnableModelValidation="True" OnRowCommand="gvProposals_RowCommand" OnRowDataBound="gvProposals_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="P#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPNumber" runat="server" Text='<%# Eval("PNumber") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Proposal Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProposalDate" runat="server" Text='<%# Eval("ProposalDate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Activity#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityNo" runat="server" Text='<%# Eval("ActivityNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField> 

                                        <asp:TemplateField HeaderText="Spec#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSpecNo" runat="server" Text='<%# Eval("SpecNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateField>                                        
                                    </Columns>
                                </asp:GridView>
                            </div>

                        </div>
                    </div>
                </div>

                <!-- Right Section -->
                <div class="col-lg-4">
                    <div class="card shadow-sm">
                        <div class="card-header bg-white">
                            <div>
                                <h5 class="mb-0"><%= ddlMonthHeaderList.SelectedItem.Text %> Summary</h5>
                            </div>
                        </div>

                        <div class="card-body row">
                            <div class="col-6 mb-3">
                                <div class="card bg-primary text-white">
                                    <div class="card-body text-center py-3">
                                        <h3 class="mb-1 font-weight-bold" id="lblCESession" runat="server">-</h3>
                                        <h5>CE Sessions</h5>
                                    </div>
                                </div>
                            </div>

                            <div class="col-6 mb-3">
                                <div class="card bg-success text-white">
                                    <div class="card-body text-center py-3">
                                        <h3 class="mb-1 font-weight-bold" id="lblConsultantEngagement" runat="server">-</h3>
                                        <h5>Consultant Engagement</h5>
                                    </div>
                                </div>
                            </div>

                            <div class="col-6 mb-3">
                                <div class="card bg-secondary text-white">
                                    <div class="card-body text-center py-3">
                                        <h3 class="mb-1 font-weight-bold" id="lblDealerOnboarding" runat="server">-</h3>
                                        <h5>Dealer Onboarding</h5>
                                    </div>
                                </div>
                            </div>

                            <div class="col-6 mb-3">
                                <div class="card bg-primary text-white">
                                    <div class="card-body text-center py-3">
                                        <h3 class="mb-1 font-weight-bold" id="lblSalesRep" runat="server">-</h3>
                                        <h5>Sales Rep</h5>
                                    </div>
                                </div>
                            </div>

                            <div class="col-6 mb-3">
                                <div class="card bg-success text-white">
                                    <div class="card-body text-center py-3">
                                        <h3 class="mb-1 font-weight-bold" id="lblCustomer" runat="server">-</h3>
                                        <h5>Customer</h5>
                                    </div>
                                </div>
                            </div>

                             <div class="col-6 mb-3">
                                <div class="card bg-secondary text-white">
                                    <div class="card-body text-center py-3">
                                        <h3 class="mb-1 font-weight-bold" id="lblEvent" runat="server">-</h3>
                                        <h5>Event</h5>
                                    </div>
                                </div>
                            </div>

                            <div class="col-6 mb-3">
                                <div class="card bg-danger text-white">
                                    <div class="card-body text-center py-3">
                                        <h3 class="mb-1 font-weight-bold" id="lblDueFollowup" runat="server">-</h3>
                                        <h5>Followup</h5>
                                    </div>
                                </div>
                            </div>

                            <div class="col-6 mb-3">
                                <div class="card bg-warning ">
                                    <div class="card-body text-center py-3">
                                        <h3 class="mb-1 font-weight-bold" id="lblSpec" runat="server">-</h3>
                                        <h5>Spec</h5>
                                    </div>
                                </div>
                            </div>

                            <div class="col-6 mb-3">
                                <div class="card bg-primary text-white">
                                    <div class="card-body text-center py-3">
                                        <h3 class="mb-1 font-weight-bold" id="lblProposals" runat="server">-</h3>
                                        <h5>Proposals</h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnActivePipelineReport" />
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
            $('#<%=ddlBDMHeaderList.ClientID%>').chosen();
        }
    </script>
</asp:Content>
