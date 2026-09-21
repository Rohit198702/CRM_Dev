<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/DefaultMain.master" CodeFile="FrmCCTDashboard.aspx.cs" Inherits="CCT_FrmCCTDashboard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <div class="col-12">
                <div class="row col-12">
                    <div class="col-2 pt-1 pb-1">
                        <div class="form-group">
                            <label>From Date</label>
                            <asp:TextBox ID="txtFromDate" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)"
                                OnTextChanged="txtFromDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <asp:CalendarExtender ID="txtFromDate_Extender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtFromDate" TargetControlID="txtFromDate">
                            </asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-2 pt-1 pb-1">
                        <div class="form-group">
                            <label>To Date</label>
                            <asp:TextBox ID="txtToDate" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)"
                                OnTextChanged="txtToDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                            <asp:CalendarExtender ID="txtToDate_Extender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtToDate" TargetControlID="txtToDate">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-3 pt-1 pb-1">
                        <label>&nbsp;&nbsp;&nbsp;&nbsp;</label>
                        <div class="form-group">
                            <%--<label>&nbsp;&nbsp;&nbsp;&nbsp;</label>--%>
                            <asp:Button runat="server" ID="btnCancel" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>

                <div class="d-flex shadow-sm border-bottom">
                    <h5 class="mb-0"><strong><%--CCT Dashboard--%></strong></h5>
                    <ul class="nav nav-tabs customTabs" id="myTabs" role="tablist">
                        <li class="nav-item border-right">
                            <a class="nav-link active" id="shipping-tab" data-toggle="tab" href="#Shipping" role="presentation">
                                <span>Shipping
                                    <i class="fas fa-arrow-right"></i>
                                </span>
                            </a>
                        </li>
                        <li class="nav-item border-right">
                            <a class="nav-link" id="installation-tab" data-toggle="tab" href="#Installation" role="presentation">
                                <span>Installation
                                     <i class="fas fa-arrow-right"></i>
                                </span>
                            </a>
                        </li>
                        <li class="nav-item border-right">
                            <a class="nav-link" id="repair-tab" data-toggle="tab" href="#Repair" role="presentation">
                                <span>Repair
                                    <i class="fas fa-arrow-right"></i>
                                </span>
                            </a>
                        </li>
                    </ul>
                </div>

                <div class="row col-12">
                    <div class="col-12">
                        <div class="tab-content">
                            <div class="tab-pane fade show active" id="Shipping" role="tabpanel">
                                <div class="row align-items-start">
                                    <div class="col-4 row">
                                        <h5 class="ml-3">Shipping Overview</h5>
                                        <div class="col-12 pt-1 pb-1 border-bottom" runat="server">
                                            <asp:Button ID="btnNotShipped" runat="server" CssClass="btn btn-sm btn-secondary" Text="_" Enabled="false" />
                                            <asp:Label ID="lblNotShipped" runat="server" Text="Not Shipped"></asp:Label>
                                            <asp:LinkButton ID="lnkPreviewNotShipped" runat="server" OnClick="lnkPreviewNotShipped_Click" CssClass="text-decoration-none" OnClientClick="window.document.forms[0].target='_blank';" Text="| Generate Report"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkNotShipped" runat="server" OnClick="lnkNotShipped_Click" CssClass="text-decoration-none" Text="| Get Details"></asp:LinkButton>
                                        </div>

                                        <div class="col-12 pt-1 pb-1 border-bottom" runat="server">
                                            <asp:Button ID="btnShipped" runat="server" CssClass="btn btn-sm btn-success" Text="_" Enabled="false" />
                                            <asp:Label ID="lblShipped" runat="server" Text="Shipped"></asp:Label>
                                            <asp:LinkButton ID="lnkPreviewShipped" runat="server" OnClick="lnkPreviewShipped_Click" CssClass="text-decoration-none" OnClientClick="window.document.forms[0].target='_blank';" Text="| Generate Report"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkShipped" runat="server" OnClick="lnkShipped_Click" CssClass="text-decoration-none" Text="| Get Details"></asp:LinkButton>
                                        </div>

                                        <div class="col-12 pt-1 pb-1 border-bottom">
                                            <asp:Button ID="btnInTransit" runat="server" CssClass="btn btn-sm btn-primary" Text="_" Enabled="false" />
                                            <asp:Label ID="lblInTransit" runat="server" Text="In-Transit"></asp:Label>
                                            <asp:LinkButton ID="lnkPreviewInTransit" runat="server" OnClick="lnkPreviewInTransit_Click" CssClass="text-decoration-none" OnClientClick="window.document.forms[0].target='_blank';" Text="| Generate Report"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkInTransit" runat="server" OnClick="lnkInTransit_Click" CssClass="text-decoration-none" Text="| Get Details"></asp:LinkButton>
                                        </div>

                                        <div class="col-12 pt-1 pb-1 border-bottom">
                                            <asp:Button ID="btnDelivered" runat="server" CssClass="btn btn-sm btn-success" Text="_" Enabled="false" />
                                            <asp:Label ID="lblDelivered" runat="server" Text="Delivered"></asp:Label>
                                            <asp:LinkButton ID="lnkPreviewDelivered" runat="server" OnClick="lnkPreviewDelivered_Click" CssClass="text-decoration-none" OnClientClick="window.document.forms[0].target='_blank';" Text="| Generate Report"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkDelivered" runat="server" OnClick="lnkDelivered_Click" CssClass="text-decoration-none" Text="| Get Details"></asp:LinkButton>
                                        </div>

                                        <div class="col-12 pt-1 pb-1 border-bottom">
                                            <asp:Button ID="btnDelayed" runat="server" CssClass="btn btn-sm btn-danger" Text="_" Enabled="false" />
                                            <asp:Label ID="lblDelayed" runat="server" Text="Delayed"></asp:Label>
                                            <asp:LinkButton ID="lnkPreviewDelayed" runat="server" OnClick="lnkPreviewDelayed_Click" CssClass="text-decoration-none" OnClientClick="window.document.forms[0].target='_blank';" Text="| Generate Report"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkDelayed" runat="server" OnClick="lnkDelayed_Click" CssClass="text-decoration-none" Text="| Get Details"></asp:LinkButton>
                                        </div>

                                    </div>

                                    <div class="col-8" style="height:400px; overflow-y:scroll;">
                                        <h5 id="hdDailyShippingActivity" runat="server"></h5>
                                        <asp:GridView ID="gvShipping" runat="server" CssClass="table mainGridTable table-sm mb-0" AutoGenerateColumns="true"
                                            DataKeyNames="JobId" OnRowDataBound="gvShipping_RowDataBound" OnRowCommand="gvShipping_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField HeaderText="Ship Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblShipDate" runat="server" Text='<%# Eval("ShipDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Job#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblJobId" runat="server" Text='<%# Eval("JobId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Shipper">
                                            <ItemTemplate>
                                                <asp:Label ID="lblShipper" runat="server" Text='<%# Eval("Shipper") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tracking No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrackingNo" runat="server" Text='<%# Eval("TrackingNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <div class="col-4" id="shipping_WeeklyChart" runat="server">
                                        <h5>Weekly Shipping Activity</h5>
                                        <div id="chtWeeklyShippingActivity" class="h-100 w-100"></div>
                                    </div>

                                    <div class="col-8" id="shipping_MonthlyChart" runat="server">
                                        <h5>Monthly Shipping Activity</h5>
                                        <div id="chtMonthlyShippingActivity" class="h-100 w-100"></div>
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane fade" id="Installation" role="tabpanel">
                                <div class="row align-items-start">
                                    <div class="col-4 row">
                                        <h5 class="mt-2 ml-2">Installation Overview</h5>
                                        <div class="col-12 pt-1 pb-1 border-bottom" runat="server">
                                            <asp:Button ID="btnInstallation_Scheduled" runat="server" CssClass="btn btn-sm btn-secondary" Text="_" Enabled="false" />
                                            <asp:Label ID="lblInstallation_Scheduled" runat="server" Text="Scheduled"></asp:Label>
                                            <asp:LinkButton ID="lnkPreviewInstallation_Scheduled" runat="server" OnClick="lnkPreviewInstallation_Scheduled_Click" CssClass="text-decoration-none" OnClientClick="window.document.forms[0].target='_blank';" Text="| Generate Report"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkInstallation_Scheduled" runat="server" OnClick="lnkInstallation_Scheduled_Click" CssClass="text-decoration-none" Text="| Get Details"></asp:LinkButton>
                                        </div>

                                        <div class="col-12 pt-1 pb-1 border-bottom" runat="server">
                                            <asp:Button ID="btnInstallation_InProgress" runat="server" CssClass="btn btn-sm btn-success" Text="_" Enabled="false" />
                                            <asp:Label ID="lblInstallation_InProgress" runat="server" Text="In Progress"></asp:Label>
                                            <asp:LinkButton ID="lnkPreviewInstallation_InProgress" runat="server" OnClick="lnkPreviewInstallation_InProgress_Click" CssClass="text-decoration-none" OnClientClick="window.document.forms[0].target='_blank';" Text="| Generate Report"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkInstallation_InProgress" runat="server" OnClick="lnkInstallation_InProgress_Click" CssClass="text-decoration-none" Text="| Get Details"></asp:LinkButton>
                                        </div>

                                        <div class="col-12 pt-1 pb-1 border-bottom" runat="server">
                                            <asp:Button ID="btnInstallation_Completed" runat="server" CssClass="btn btn-sm btn-primary" Text="_" Enabled="false" />
                                            <asp:Label ID="lblInstallation_Completed" runat="server" Text="Completed"></asp:Label>
                                            <asp:LinkButton ID="lnkPreviewInstallation_Completed" runat="server" OnClick="lnkPreviewInstallation_Completed_Click" CssClass="text-decoration-none" OnClientClick="window.document.forms[0].target='_blank';" Text="| Generate Report"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkInstallation_Completed" runat="server" OnClick="lnkInstallation_Completed_Click" CssClass="text-decoration-none" Text="| Get Details"></asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="col-8" style="height:400px; overflow-y:scroll;">
                                        <h5 id="h1InstallationActivity" runat="server"></h5>
                                        <asp:GridView ID="gvInstallation" runat="server" CssClass="table mainGridTable table-sm mb-0" AutoGenerateColumns="true"
                                            DataKeyNames="JobId" OnRowDataBound="gvInstallation_RowDataBound" OnRowCommand="gvInstallation_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField HeaderText="Ship Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblShipDate" runat="server" Text='<%# Eval("ShipDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Job#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblJobId" runat="server" Text='<%# Eval("JobId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Shipper">
                                            <ItemTemplate>
                                                <asp:Label ID="lblShipper" runat="server" Text='<%# Eval("Shipper") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tracking No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrackingNo" runat="server" Text='<%# Eval("TrackingNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <div class="col-4" id="installation_WeeklyChart" runat="server">
                                        <h5>Weekly Installation Activity</h5>
                                        <div id="chtWeeklyInstallationActivity" class="h-100 w-100"></div>
                                    </div>

                                    <div class="col-8" id="installation_MonthlyChart" runat="server">
                                        <h5>Monthly Installation Activity</h5>
                                        <div id="chtMonthlyInstallationActivity" class="h-100 w-100"></div>
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane fade" id="Repair" role="tabpanel">
                                <div class="row align-items-start">
                                    <div class="col-4 row">
                                        <h5 class="mt-2 ml-2">Repair Overview</h5>
                                        <div class="col-12 pt-1 pb-1 border-bottom" runat="server">
                                            <asp:Button ID="btnRepair_Pending" runat="server" CssClass="btn btn-sm btn-secondary" Text="_" Enabled="false" />
                                            <asp:Label ID="lblRepair_Pending" runat="server" Text="Open"></asp:Label>
                                            <asp:LinkButton ID="lnkPreviewRepair_Pending" runat="server" OnClick="lnkPreviewRepair_Pending_Click" CssClass="text-decoration-none" OnClientClick="window.document.forms[0].target='_blank';" Text="| Generate Report"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkRepair_Pending" runat="server" OnClick="lnkRepair_Pending_Click" CssClass="text-decoration-none" Text="| Get Details"></asp:LinkButton>
                                        </div>

                                        <div class="col-12 pt-1 pb-1 border-bottom" runat="server" style="display: none;">
                                            <asp:Button ID="btnRepair_InProgress" runat="server" CssClass="btn btn-sm btn-success" Text="_" Enabled="false" />
                                            <asp:Label ID="lblRepair_InProgress" runat="server" Text="In Progress"></asp:Label>
                                            <asp:LinkButton ID="lnkPreviewRepair_InProgress" runat="server" OnClick="lnkPreviewRepair_InProgress_Click" CssClass="text-decoration-none" OnClientClick="window.document.forms[0].target='_blank';" Text="| Generate Report"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkRepair_InProgress" runat="server" OnClick="lnkRepair_InProgress_Click" CssClass="text-decoration-none" Text="| Get Details"></asp:LinkButton>
                                        </div>

                                        <div class="col-12 pt-1 pb-1 border-bottom" runat="server">
                                            <asp:Button ID="btnRepair_Resolved" runat="server" CssClass="btn btn-sm btn-primary" Text="_" Enabled="false" />
                                            <asp:Label ID="lblRepair_Resolved" runat="server" Text="Closed"></asp:Label>
                                            <asp:LinkButton ID="lnkPreviewRepair_Resolved" runat="server" OnClick="lnkPreviewRepair_Resolved_Click" CssClass="text-decoration-none" OnClientClick="window.document.forms[0].target='_blank';" Text="| Generate Report"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkRepair_Resolved" runat="server" OnClick="lnkRepair_Resolved_Click" CssClass="text-decoration-none" Text="| Get Details"></asp:LinkButton>
                                        </div>

                                         <div class="col-12 pt-1 pb-1 border-bottom" runat="server">
                                            <asp:Button ID="btnRepair_Followup" runat="server" CssClass="btn btn-sm btn-primary" Text="_" Enabled="false" />
                                            <asp:Label ID="lblRepair_Followup" runat="server" Text="Followup"></asp:Label>
                                            <asp:LinkButton ID="lnkPreviewRepair_Followup" runat="server" OnClick="lnkPreviewRepair_Followup_Click" CssClass="text-decoration-none" OnClientClick="window.document.forms[0].target='_blank';" Text="| Generate Report"></asp:LinkButton>
                                            <asp:LinkButton ID="lnkRepair_Followup" runat="server" OnClick="lnkRepair_Followup_Click" CssClass="text-decoration-none" Text="| Get Details"></asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="col-8" style="height:400px; overflow-y:scroll;">
                                        <h5 id="h1RepairActivity" runat="server"></h5>
                                        <asp:GridView ID="gvRepair" runat="server" CssClass="table mainGridTable table-sm mb-0" AutoGenerateColumns="true"
                                            DataKeyNames="JobId" OnRowDataBound="gvRepair_RowDataBound" OnRowCommand="gvRepair_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField HeaderText="Ship Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblShipDate" runat="server" Text='<%# Eval("ShipDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Job#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblJobId" runat="server" Text='<%# Eval("JobId") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Shipper">
                                            <ItemTemplate>
                                                <asp:Label ID="lblShipper" runat="server" Text='<%# Eval("Shipper") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Tracking No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrackingNo" runat="server" Text='<%# Eval("TrackingNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <div class="col-4" id="repair_WeeklyChart" runat="server">
                                        <h5>Weekly Repair Activity</h5>
                                        <div id="chtWeeklyRepairActivity" class="h-100 w-100"></div>
                                    </div>

                                    <div class="col-8" id="repair_MonthlyChart" runat="server">
                                        <h5>Monthly Repair Activity</h5>
                                        <div id="chtMonthlyRepairActivity" class="h-100 w-100"></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkPreviewNotShipped" />
            <asp:PostBackTrigger ControlID="lnkPreviewShipped" />
            <asp:PostBackTrigger ControlID="lnkPreviewInTransit" />
            <asp:PostBackTrigger ControlID="lnkPreviewDelivered" />
            <asp:PostBackTrigger ControlID="lnkPreviewDelayed" />

            <asp:PostBackTrigger ControlID="lnkPreviewInstallation_Scheduled" />
            <asp:PostBackTrigger ControlID="lnkPreviewInstallation_InProgress" />
            <asp:PostBackTrigger ControlID="lnkPreviewInstallation_Completed" />

            <asp:PostBackTrigger ControlID="lnkPreviewRepair_Pending" />
            <asp:PostBackTrigger ControlID="lnkPreviewRepair_Resolved" />
            <asp:PostBackTrigger ControlID="lnkPreviewRepair_Followup" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function SetShipping() {
            document.getElementById("Shipping").className = 'tab-pane fade show active pt-2';
            document.getElementById("Installation").className = 'tab-pane fade pt-2';
            document.getElementById("Repair").className = 'nav-link fade pt-2';

            document.getElementById("shipping-tab").className = 'nav-link active';
            document.getElementById("installation-tab").className = 'nav-link';
            document.getElementById("repair-tab").className = 'nav-link';
        }

        function SetInstallation() {
            document.getElementById("Installation").className = 'tab-pane fade show active pt-2';
            document.getElementById("Shipping").className = 'tab-pane fade pt-2';
            document.getElementById("Repair").className = 'nav-link fade pt-2';

            document.getElementById("installation-tab").className = 'nav-link active';
            document.getElementById("shipping-tab").className = 'nav-link';
            document.getElementById("repair-tab").className = 'nav-link';
        }

        function SetRepair() {
            document.getElementById("Repair").className = 'tab-pane fade show active pt-2';
            document.getElementById("Installation").className = 'tab-pane fade pt-2';
            document.getElementById("Shipping").className = 'tab-pane fade pt-2';

            document.getElementById("repair-tab").className = 'nav-link active';
            document.getElementById("installation-tab").className = 'nav-link';
            document.getElementById("shipping-tab").className = 'nav-link';
        }

        //Common Chart Load
        function loadChartData(options) {
            $.ajax({
                type: "POST",
                url: options.url,
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    const result = response.d;
                    const chartDataArray = [options.headers];
                    if (!Array.isArray(result)) {
                        console.warn("Unexpected data format from server:", result);
                        return;
                    }
                    result.forEach(function (item) {
                        const rows = options.rowBuilder(item);
                        if (Array.isArray(rows[0])) {
                            rows.forEach(r => chartDataArray.push(r));
                        } else {
                            chartDataArray.push(rows);
                        }
                    });
                    drawGenericChart(chartDataArray, options.chartOptions, options.containerId, options.chartType, options.formatterCallback);
                },
                error: function (xhr, status, error) {
                    console.error("Error loading chart:", error);
                }
            });
        }

        function drawGenericChart(chartDataArray, chartOptions, containerId, chartType, formatterCallback) {
            chartType = chartType || 'ColumnChart';
            const data = google.visualization.arrayToDataTable(chartDataArray);

            if (chartOptions.formatter) {
                const formatter = new google.visualization.NumberFormat(chartOptions.formatter);
                formatter.format(data, 1); // Format second column
            }

            //For Multi Bars Number format
            if (typeof formatterCallback === 'function') {
                formatterCallback(data);
            }

            let chart;
            switch (chartType) {
                case 'BarChart':
                    chart = new google.visualization.BarChart(document.getElementById(containerId));
                    break;
                case 'PieChart':
                    chart = new google.visualization.PieChart(document.getElementById(containerId));
                    break;
                case 'LineChart':
                    chart = new google.visualization.LineChart(document.getElementById(containerId));
                    break;
                default:
                    chart = new google.visualization.ColumnChart(document.getElementById(containerId));
            }

            chart.draw(data, chartOptions.options);
        }

        const weeklyShippingActivity_Template = [
                {
                    url: "../CCT/FrmCCTDashboard.aspx/Get_WeeklyShippingActivity",
                    headers: ["Week Day", "Projects Shipping"],
                    containerId: "chtWeeklyShippingActivity",
                    chartType: "barChart",
                    rowBuilder: function (item) {
                        return [item.WeekDay, parseFloat(item.ProjectsShipped)];
                    },
                    chartOptions: {
                        options: {
                            chartArea: {
                                left: 40,
                                right: 5,
                            },
                            //title: 'Sales ' + new Date().getFullYear(),
                            hAxis: {
                                textStyle: { fontSize: 10, bold: true },
                                slantedText: false,
                                slantedTextAngle: 90
                            },
                            vAxes: {
                                0: { title: 'Count', format: '0', textStyle: { fontSize: 12, bold: true }},
                            },
                            seriesType: 'bars',
                            //width: 850,
                            //height: 500,
                            series: {
                                0: { color: '#0856a1' } // Targeted Sales (bar)
                                //1: { color: '#f86d3b' }  // Actual Sales (bar)
                            },
                            legend: {
                                position: "top",
                                textStyle: { fontSize: 10, bold: true },
                                alignment: "start"
                            },
                            titleTextStyle: { fontSize: 12, bold: true },
                            colors: ['#0856a1'] // Bar colors for Targeted and Actual totals
                        }
                    },
                    formatterCallback: function (dataTable) {
                        const formatter = new google.visualization.NumberFormat({
                            prefix: '',
                            fractionDigits: 0
                        });

                        //const formatter1 = new google.visualization.NumberFormat({
                        //    prefix: '$',
                        //    fractionDigits: 0
                        //});

                        formatter.format(dataTable, 1);
                        //formatter1.format(dataTable, 2);
                    }
                }
        ];

        const weeklyInstallationActivity_Template = [
               {
                   url: "../CCT/FrmCCTDashboard.aspx/Get_WeeklyInstallationActivity",
                   headers: ["Week Day", "Projects Installation"],
                   containerId: "chtWeeklyInstallationActivity",
                   chartType: "barChart",
                   rowBuilder: function (item) {
                       return [item.WeekDay, parseFloat(item.ProjectsShipped)];
                   },
                   chartOptions: {
                       options: {
                           chartArea: {
                               left: 40,
                               right: 5,
                           },
                           //title: 'Sales ' + new Date().getFullYear(),
                           hAxis: {
                               textStyle: { fontSize: 10, bold: true },
                               slantedText: false,
                               slantedTextAngle: 90
                           },
                           vAxes: {
                               0: { title: 'Count', format: '0', textStyle: { fontSize: 12, bold: true }},
                           },
                           seriesType: 'bars',
                           //width: 850,
                           //height: 500,
                           series: {
                               0: { color: '#0856a1' } // Targeted Sales (bar)
                               //1: { color: '#f86d3b' }  // Actual Sales (bar)
                           },
                           legend: {
                               position: "top",
                               textStyle: { fontSize: 10, bold: true },
                               alignment: "start"
                           },
                           titleTextStyle: { fontSize: 12, bold: true },
                           colors: ['#0856a1'] // Bar colors for Targeted and Actual totals
                       }
                   },
                   formatterCallback: function (dataTable) {
                       const formatter = new google.visualization.NumberFormat({
                           prefix: '',
                           fractionDigits: 0
                       });

                       //const formatter1 = new google.visualization.NumberFormat({
                       //    prefix: '$',
                       //    fractionDigits: 0
                       //});

                       formatter.format(dataTable, 1);
                       //formatter1.format(dataTable, 2);
                   }
               }
        ];

        const weeklyRepairActivity_Template = [
               {
                   url: "../CCT/FrmCCTDashboard.aspx/Get_WeeklyRepairActivity",
                   headers: ["Week Day", "Tickets Open"],
                   containerId: "chtWeeklyRepairActivity",
                   chartType: "barChart",
                   rowBuilder: function (item) {
                       return [item.WeekDay, parseFloat(item.ProjectsShipped)];
                   },
                   chartOptions: {
                       options: {
                           chartArea: {
                               left: 40,
                               right: 5,
                           },
                           //title: 'Sales ' + new Date().getFullYear(),
                           hAxis: {
                               textStyle: { fontSize: 10, bold: true },
                               slantedText: false,
                               slantedTextAngle: 90
                           },
                           vAxes: {
                               0: { title: 'Count', format: '0', textStyle: { fontSize: 12, bold: true }},
                           },
                           seriesType: 'bars',
                           //width: 850,
                           //height: 500,
                           series: {
                               0: { color: '#0856a1' } // Targeted Sales (bar)
                               //1: { color: '#f86d3b' }  // Actual Sales (bar)
                           },
                           legend: {
                               position: "top",
                               textStyle: { fontSize: 10, bold: true },
                               alignment: "start"
                           },
                           titleTextStyle: { fontSize: 12, bold: true },
                           colors: ['#0856a1'] // Bar colors for Targeted and Actual totals
                       }
                   },
                   formatterCallback: function (dataTable) {
                       const formatter = new google.visualization.NumberFormat({
                           prefix: '',
                           fractionDigits: 0
                       });

                       //const formatter1 = new google.visualization.NumberFormat({
                       //    prefix: '$',
                       //    fractionDigits: 0
                       //});

                       formatter.format(dataTable, 1);
                       //formatter1.format(dataTable, 2);
                   }
               }
        ];

        function weeklyShippingActivity() {
            var config = weeklyShippingActivity_Template[0];
            if (config && config.containerId) {
                var container = document.getElementById(config.containerId);

                if (container) {
                    var style = window.getComputedStyle(container);
                    var isVisible = style.display !== 'none' && style.visibility !== 'hidden' && style.opacity !== '0';

                    if (isVisible) {
                        loadChartData(config);
                    } else {
                        console.warn("Container is hidden:", config.containerId);
                    }
                }
            }
        }

        function weeklyInstallationActivity() {
            var config = weeklyInstallationActivity_Template[0];
            if (config && config.containerId) {
                var container = document.getElementById(config.containerId);

                if (container) {
                    var style = window.getComputedStyle(container);
                    var isVisible = style.display !== 'none' && style.visibility !== 'hidden' && style.opacity !== '0';

                    if (isVisible) {
                        loadChartData(config);
                    } else {
                        console.warn("Container is hidden:", config.containerId);
                    }
                }
            }
        }

        function weeklyRepairActivity() {
            var config = weeklyRepairActivity_Template[0];
            if (config && config.containerId) {
                var container = document.getElementById(config.containerId);

                if (container) {
                    var style = window.getComputedStyle(container);
                    var isVisible = style.display !== 'none' && style.visibility !== 'hidden' && style.opacity !== '0';

                    if (isVisible) {
                        loadChartData(config);
                    } else {
                        console.warn("Container is hidden:", config.containerId);
                    }
                }
            }
        }

        const monthlyShippingActivity_Template = [
                {
                    url: "../CCT/FrmCCTDashboard.aspx/Get_MonthlyShippingActivity",
                    headers: ["Months", "Projects Shipped"],
                    containerId: "chtMonthlyShippingActivity",
                    chartType: "barChart",
                    rowBuilder: function (item) {
                        return [item.WeekDay, parseFloat(item.ProjectsShipped)];
                    },
                    chartOptions: {
                        options: {
                            chartArea: {
                                left: 40,
                                right: 5,
                            },
                            //title: 'Sales ' + new Date().getFullYear(),
                            hAxis: {
                                textStyle: { fontSize: 10, bold: true },
                                slantedText: false,
                                slantedTextAngle: 90
                            },
                            vAxes: {
                                0: { title: 'Count', format: '0', textStyle: { fontSize: 12, bold: true } }
                            },
                            seriesType: 'bars',
                            //width: 850,
                            //height: 500,
                            series: {
                                0: { color: '#f86d3b' } // Targeted Sales (bar)
                                //1: { color: '#f86d3b' }  // Actual Sales (bar)
                            },
                            legend: {
                                position: "top",
                                textStyle: { fontSize: 10, bold: true },
                                alignment: "start"
                            },
                            titleTextStyle: { fontSize: 12, bold: true },
                            colors: ['#0856a1'] // Bar colors for Targeted and Actual totals
                        }
                    },
                    formatterCallback: function (dataTable) {
                        const formatter = new google.visualization.NumberFormat({
                            prefix: '',
                            fractionDigits: 0
                        });

                        //const formatter1 = new google.visualization.NumberFormat({
                        //    prefix: '$',
                        //    fractionDigits: 0
                        //});

                        formatter.format(dataTable, 1);
                        //formatter1.format(dataTable, 2);
                    }
                }
        ];

        const monthlyInstallationActivity_Template = [
                {
                    url: "../CCT/FrmCCTDashboard.aspx/Get_MonthlyInstallationActivity",
                    headers: ["Months", "Projects Installed"],
                    containerId: "chtMonthlyInstallationActivity",
                    chartType: "barChart",
                    rowBuilder: function (item) {
                        return [item.WeekDay, parseFloat(item.ProjectsShipped)];
                    },
                    chartOptions: {
                        options: {
                            chartArea: {
                                left: 40,
                                right: 5,
                            },
                            //title: 'Sales ' + new Date().getFullYear(),
                            hAxis: {
                                textStyle: { fontSize: 10, bold: true },
                                slantedText: false,
                                slantedTextAngle: 90
                            },
                            vAxes: {
                                0: { title: 'Count', format: '0', textStyle: { fontSize: 12, bold: true }}
                            },
                            seriesType: 'bars',
                            //width: 850,
                            //height: 500,
                            series: {
                                0: { color: '#f86d3b' } // Targeted Sales (bar)
                                //1: { color: '#f86d3b' }  // Actual Sales (bar)
                            },
                            legend: {
                                position: "top",
                                textStyle: { fontSize: 10, bold: true },
                                alignment: "start"
                            },
                            titleTextStyle: { fontSize: 12, bold: true },
                            colors: ['#0856a1'] // Bar colors for Targeted and Actual totals
                        }
                    },
                    formatterCallback: function (dataTable) {
                        const formatter = new google.visualization.NumberFormat({
                            prefix: '',
                            fractionDigits: 0
                        });

                        //const formatter1 = new google.visualization.NumberFormat({
                        //    prefix: '$',
                        //    fractionDigits: 0
                        //});

                        formatter.format(dataTable, 1);
                        //formatter1.format(dataTable, 2);
                    }
                }
        ];

        const monthlyRepairActivity_Template = [
                {
                    url: "../CCT/FrmCCTDashboard.aspx/Get_MonthlyRepairActivity",
                    headers: ["Months", "Projects Repair"],
                    containerId: "chtMonthlyRepairActivity",
                    chartType: "barChart",
                    rowBuilder: function (item) {
                        return [item.WeekDay, parseFloat(item.ProjectsShipped)];
                    },
                    chartOptions: {
                        options: {
                            chartArea: {
                                left: 40,
                                right: 5,
                            },
                            //title: 'Sales ' + new Date().getFullYear(),
                            hAxis: {
                                textStyle: { fontSize: 10, bold: true },
                                slantedText: false,
                                slantedTextAngle: 90
                            },
                            vAxes: {
                                0: { title: 'Count', format: '0', textStyle: { fontSize: 12, bold: true }}
                            },
                            seriesType: 'bars',
                            //width: 850,
                            //height: 500,
                            series: {
                                0: { color: '#f86d3b' } // Targeted Sales (bar)
                                //1: { color: '#f86d3b' }  // Actual Sales (bar)
                            },
                            legend: {
                                position: "top",
                                textStyle: { fontSize: 10, bold: true },
                                alignment: "start"
                            },
                            titleTextStyle: { fontSize: 12, bold: true },
                            colors: ['#0856a1'] // Bar colors for Targeted and Actual totals
                        }
                    },
                    formatterCallback: function (dataTable) {
                        const formatter = new google.visualization.NumberFormat({
                            prefix: '',
                            fractionDigits: 0
                        });

                        //const formatter1 = new google.visualization.NumberFormat({
                        //    prefix: '$',
                        //    fractionDigits: 0
                        //});

                        formatter.format(dataTable, 1);
                        //formatter1.format(dataTable, 2);
                    }
                }
        ];

        function monthlyShippingActivity() {
            var config = monthlyShippingActivity_Template[0];
            if (config && config.containerId) {
                var container = document.getElementById(config.containerId);

                if (container) {
                    var style = window.getComputedStyle(container);
                    var isVisible = style.display !== 'none' && style.visibility !== 'hidden' && style.opacity !== '0';

                    if (isVisible) {
                        loadChartData(config);
                    } else {
                        console.warn("Container is hidden:", config.containerId);
                    }
                }
            }
        }

        function monthlyInstallationActivity() {
            var config = monthlyInstallationActivity_Template[0];
            if (config && config.containerId) {
                var container = document.getElementById(config.containerId);

                if (container) {
                    var style = window.getComputedStyle(container);
                    var isVisible = style.display !== 'none' && style.visibility !== 'hidden' && style.opacity !== '0';

                    if (isVisible) {
                        loadChartData(config);
                    } else {
                        console.warn("Container is hidden:", config.containerId);
                    }
                }
            }
        }

        function monthlyRepairActivity() {
            var config = monthlyRepairActivity_Template[0];
            if (config && config.containerId) {
                var container = document.getElementById(config.containerId);

                if (container) {
                    var style = window.getComputedStyle(container);
                    var isVisible = style.display !== 'none' && style.visibility !== 'hidden' && style.opacity !== '0';

                    if (isVisible) {
                        loadChartData(config);
                    } else {
                        console.warn("Container is hidden:", config.containerId);
                    }
                }
            }
        }

        google.charts.load('current', { packages: ['corechart', 'bar'] });

        google.charts.setOnLoadCallback(function () {
            setTimeout(() => {
                weeklyShippingActivity();
                monthlyShippingActivity();

                weeklyInstallationActivity();
                monthlyInstallationActivity();

                weeklyRepairActivity();
                monthlyRepairActivity();
            }, 1000);
        });

        Sys.Application.add_load(function () {
            weeklyShippingActivity();
            monthlyShippingActivity();

            weeklyInstallationActivity();
            monthlyInstallationActivity();

            weeklyRepairActivity();
            monthlyRepairActivity();
        });

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

        }
    </script>
</asp:Content>
