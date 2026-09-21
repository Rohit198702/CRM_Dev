<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmNesting.aspx.cs" Inherits="SalesManagement_FrmNesting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <div class="col-12 pt-2 piDiv position-sticky">
                <div class="row">
                    <div class="col-12">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Nesting Tasks</h4>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-4">
                        <div class="row">
                            <div class="col-4">
                                <label class="mb-0">Product Code</label>
                            </div>
                            <div class="col-8 mb-3 chosenFullWidth">
                                <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlNestingFor" runat="server" DataTextField="text" DataValueField="id"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlNestingFor_SelectedIndexChanged">
                                    <%-- <asp:ListItem Value="AEROWERKS">Aerowerks</asp:ListItem>
                                    <asp:ListItem Value="ITW">ITW</asp:ListItem>
                                    <asp:ListItem Value="GAYLORD">Gaylord</asp:ListItem>--%>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <div class="col-3">
                        <asp:Button ID="btnNestingScheduleReport" runat="server" CssClass="btn btn-secondary btn-sm" Enabled="true" OnClick="btnNestingScheduleReport_Click" Text="Nesting Schedule Report" Visible="false" />
                        <asp:Button ID="btnNestingReport" runat="server" CssClass="btn btn-primary btn-sm" Enabled="true" OnClick="btnNestingReport_Click" Text="Nesting Report" />
                    </div>
                </div>
            </div>

            <div class="col-12" id="divAero" runat="server">
                <div class="table-responsive" style="height: 300px; display: none;">
                    <asp:GridView CssClass="table mainGridTable table-sm mb-0" ForeColor="White" ID="gvAerowerksNesting" runat="server" AutoGenerateColumns="true" DataKeyNames="Job #"
                        OnRowCommand="gvAerowerksNesting_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="Modify">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" Text="Open" CommandArgument='<%# Container.DataItemIndex %>'
                                        CommandName="Open"><i class="far fa-edit" title="Open"></i></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="row" style="display: none;">
                    <div class="col-3">
                        <div class="row">
                            <div class="col-sm chosenFullWidth">
                                <asp:Panel ID="PName" runat="server" Style="height: 200px; overflow: scroll; display: none;">
                                </asp:Panel>
                                <label>Project</label>
                                <asp:Panel ID="PanelPName" runat="server" DefaultButton="SearchPNameButton">
                                    <asp:TextBox ID="txtSearchPName" AutoComplete="off" placeholder="Type Job Name" CssClass="form-control form-control-sm" OnBlur="return ClickEventForPName(event)"
                                        runat="server">
                                    </asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtSearchPName"
                                        CompletionInterval="1" CompletionSetCount="10" MinimumPrefixLength="1" CompletionListElementID="PName"
                                        ServicePath="../AutoComplete.asmx" ServiceMethod="SearchProject" CompletionListCssClass="autocomplete" />
                                    <asp:Button ID="SearchPNameButton" runat="server" Text="Submit" Style="display: none" OnClick="SearchPNameButton_Click" />
                                </asp:Panel>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-2 chosenFullWidth">
                        <asp:Panel ID="JobID" runat="server" Style="height: 200px; overflow: scroll; display: none;">
                        </asp:Panel>
                        <label>Job#</label>
                        <asp:Panel ID="Panel2" runat="server" DefaultButton="SearchJobIDButton">
                            <asp:TextBox ID="txtSearchJobID" placeholder="Type JobID" AutoComplete="off" CssClass="form-control form-control-sm" runat="server"
                                OnBlur="return ClickEventForJobID(event)" onkeypress="return EnterEventForJobID(event)">
                            </asp:TextBox>
                            <asp:AutoCompleteExtender ID="AutoCompleteExtender3" runat="server" TargetControlID="txtSearchJobID"
                                CompletionInterval="1" CompletionSetCount="10" MinimumPrefixLength="1" CompletionListElementID="JobID"
                                ServicePath="../AutoComplete.asmx" ServiceMethod="SearchJobNumberOnly" CompletionListCssClass="autocomplete" />
                            <asp:Button ID="SearchJobIDButton" runat="server" Text="Submit" Style="display: none" OnClick="txtSearchJobID_TextChanged" />
                        </asp:Panel>
                    </div>
                </div>
                <div class="row pt-3">
                    <div class="col-sm-12">
                        <%--<h5 class="text-uppercase">Nesting Tasks</h5>--%>
                        <div class="table-responsive">
                            <asp:GridView CssClass="table mainGridTable table-sm mb-0" ForeColor="White" ID="gvNestingTasks" runat="server" AutoGenerateColumns="false" DataKeyNames="id,Timestamp"
                                EnableModelValidation="True" ShowFooter="true" OnRowEditing="gvNestingTasks_RowEditing" OnRowDeleting="gvNestingTasks_RowDeleting" EmptyDataText="No Items Found"
                                OnRowCancelingEdit="gvNestingTasks_RowCancelingEdit" OnRowUpdating="gvNestingTasks_RowUpdating" OnRowCommand="gvNestingTasks_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="Task #" SortExpression="TaskNumber">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaskNumber" runat="server" Text='<%# Eval("TaskNumber") %>'></asp:Label>
                                        </ItemTemplate>

                                        <EditItemTemplate>
                                            <asp:Label ID="lblTaskNumber_EditNesting" runat="server" Text='<%#Eval("TaskNumber") %>'></asp:Label>
                                        </EditItemTemplate>

                                        <ItemStyle Width="140px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Job #" SortExpression="JobID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJobID" runat="server" Text='<%# Eval("JobID") %>'></asp:Label>
                                        </ItemTemplate>

                                        <EditItemTemplate>
                                            <asp:Label ID="lblJobID_EditNesting" runat="server" Text='<%#Eval("JobID") %>'></asp:Label>
                                        </EditItemTemplate>

                                        <FooterTemplate>
                                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlJobID_FooterNesting" runat="server" DataTextField="text" DataValueField="text"></asp:DropDownList>
                                            <asp:Label ID="lblJobID_FooterNesting" runat="server" Width="140px" Text='<%#Eval("JobID") %>' Visible="false"></asp:Label>
                                        </FooterTemplate>

                                        <ItemStyle Width="140px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Nature of Task" SortExpression="NatureOfTask">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNatureOfTask" runat="server" Text='<%# Eval("NatureOfTask") %>'></asp:Label>
                                        </ItemTemplate>

                                        <EditItemTemplate>
                                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlNatureOfTask_EditNesting" runat="server">
                                                <asp:ListItem Value="">Select</asp:ListItem>
                                                <asp:ListItem Value="NR">New Release</asp:ListItem>
                                                <asp:ListItem Value="RH">Partial Release</asp:ListItem>
                                                <asp:ListItem Value="FR">Full Release</asp:ListItem>
                                                <asp:ListItem Value="SP">Service Parts</asp:ListItem>
                                                <asp:ListItem Value="RW">Rework</asp:ListItem>
                                                <asp:ListItem Value="U">Urgent</asp:ListItem>
                                                <asp:ListItem Value="E">Expedited</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblNatureOfTask_EditNesting" runat="server" Text='<%#Eval("NatureOfTask") %>' Visible="false"></asp:Label>
                                        </EditItemTemplate>

                                        <FooterTemplate>
                                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlNatureOfTask_FooterNesting" runat="server">
                                                <asp:ListItem Value="">Select</asp:ListItem>
                                                <asp:ListItem Value="NR">New Release</asp:ListItem>
                                                <asp:ListItem Value="RH">Partial Release</asp:ListItem>
                                                <asp:ListItem Value="FR">Full Release</asp:ListItem>
                                                <asp:ListItem Value="SP">Service Parts</asp:ListItem>
                                                <asp:ListItem Value="RW">Rework</asp:ListItem>
                                                <asp:ListItem Value="U">Urgent</asp:ListItem>
                                                <asp:ListItem Value="E">Expedited</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblNatureOfTask_FooterNesting" runat="server" Text='<%#Eval("NatureOfTask") %>' Visible="false"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Assigned From" SortExpression="AssignedFrom">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAssignedFrom" runat="server" Text='<%# Eval("AssignedFrom") %>'></asp:Label>
                                        </ItemTemplate>

                                        <EditItemTemplate>
                                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlAssignedFrom_EditNesting" runat="server">
                                                <asp:ListItem Value="">Select</asp:ListItem>
                                                <asp:ListItem Value="F">Fabrication</asp:ListItem>
                                                <asp:ListItem Value="S">Shop</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblAssignedFrom_EditNesting" runat="server" Text='<%#Eval("AssignedFrom") %>' Visible="false"></asp:Label>
                                        </EditItemTemplate>

                                        <FooterTemplate>
                                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlAssignedFrom_FooterNesting" runat="server">
                                                <asp:ListItem Value="">Select</asp:ListItem>
                                                <asp:ListItem Value="F">Fabrication</asp:ListItem>
                                                <asp:ListItem Value="S">Shop</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblAssignedFrom_FooterNesting" runat="server" Text='<%#Eval("AssignedFrom") %>' Visible="false"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Task Type" SortExpression="TaskType">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTaskType" runat="server" Text='<%# Eval("TaskType") %>'></asp:Label>
                                        </ItemTemplate>

                                        <EditItemTemplate>
                                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlTaskType_EditNesting" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                                            <asp:Label ID="lblTaskType_EditNesting" runat="server" Width="140px" Text='<%#Eval("TaskType") %>' Visible="false"></asp:Label>
                                        </EditItemTemplate>

                                        <FooterTemplate>
                                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlTaskType_FooterNesting" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                                            <asp:Label ID="lblTaskType_FooterNesting" runat="server" Width="140px" Text='<%#Eval("TaskType") %>' Visible="false"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Project Engineer" SortExpression="ProjectEngineer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProjectEngineer" runat="server" Text='<%# Eval("ProjectEngineer") %>'></asp:Label>
                                        </ItemTemplate>

                                        <EditItemTemplate>
                                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlProjectEngineer_EditNesting" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                                            <asp:Label ID="lblProjectEngineer_EditNesting" runat="server" Width="140px" Text='<%#Eval("ProjectEngineer") %>' Visible="false"></asp:Label>
                                        </EditItemTemplate>

                                        <FooterTemplate>
                                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlProjectEngineer_FooterNesting" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                                            <asp:Label ID="lblProjectEngineer_FooterNesting" runat="server" Width="140px" Text='<%#Eval("ProjectEngineer") %>' Visible="false"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Start Date" SortExpression="StartDate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStartDate" runat="server" Text='<%# Eval("StartDate") %>'></asp:Label>
                                        </ItemTemplate>

                                        <EditItemTemplate>
                                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtStartDate_EditNesting" OnBlur="validateDate(this)" runat="server"
                                                Width="100%" Text='<%#Eval("StartDate") %>'></asp:TextBox>
                                            <asp:CalendarExtender ID="txtStartDate_EditNesting_Extender" runat="server" Format="MM/dd/yyyy"
                                                PopupButtonID="txtStartDate_EditNesting" TargetControlID="txtStartDate_EditNesting">
                                            </asp:CalendarExtender>
                                        </EditItemTemplate>

                                        <FooterTemplate>
                                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtStartDate_FooterNesting" OnBlur="validateDate(this)" runat="server" Width="100%"></asp:TextBox>
                                            <asp:CalendarExtender ID="txtStartDate_FooterNesting_Extender" runat="server" Format="MM/dd/yyyy"
                                                PopupButtonID="txtStartDate_FooterNesting" TargetControlID="txtStartDate_FooterNesting">
                                            </asp:CalendarExtender>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="End Date" SortExpression="EndDate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEndDate" runat="server" Text='<%# Eval("EndDate") %>'></asp:Label>
                                        </ItemTemplate>

                                        <EditItemTemplate>
                                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtEndDate_EditNesting" OnBlur="validateDate(this)" runat="server"
                                                Width="100%" Text='<%#Eval("EndDate") %>'></asp:TextBox>
                                            <asp:CalendarExtender ID="txtEndDate_EditNesting_Extender" runat="server" Format="MM/dd/yyyy"
                                                PopupButtonID="txtEndDate_EditNesting" TargetControlID="txtEndDate_EditNesting">
                                            </asp:CalendarExtender>
                                        </EditItemTemplate>

                                        <FooterTemplate>
                                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtEndDate_FooterNesting" OnBlur="validateDate(this)" runat="server" Width="100%"></asp:TextBox>
                                            <asp:CalendarExtender ID="txtEndDate_FooterNesting_Extender" runat="server" Format="MM/dd/yyyy"
                                                PopupButtonID="txtEndDate_FooterNesting" TargetControlID="txtEndDate_FooterNesting">
                                            </asp:CalendarExtender>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sent Date" SortExpression="SentDate">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSentDate" runat="server" Text='<%# Eval("SentDate") %>'></asp:Label>
                                        </ItemTemplate>

                                        <EditItemTemplate>
                                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtSentDate_EditNesting" OnBlur="validateDate(this)" runat="server"
                                                Width="100%" Text='<%#Eval("SentDate") %>'></asp:TextBox>
                                            <asp:CalendarExtender ID="txtSentDate_EditNesting_Extender" runat="server" Format="MM/dd/yyyy"
                                                PopupButtonID="txtSentDate_EditNesting" TargetControlID="txtSentDate_EditNesting">
                                            </asp:CalendarExtender>
                                        </EditItemTemplate>

                                        <FooterTemplate>
                                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtSentDate_FooterNesting" OnBlur="validateDate(this)" runat="server" Width="100%"></asp:TextBox>
                                            <asp:CalendarExtender ID="txtSentDate_FooterNesting_Extender" runat="server" Format="MM/dd/yyyy"
                                                PopupButtonID="txtSentDate_FooterNesting" TargetControlID="txtSentDate_FooterNesting">
                                            </asp:CalendarExtender>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                        </ItemTemplate>

                                        <EditItemTemplate>
                                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlStatus_EditNesting" runat="server">
                                                <asp:ListItem Value="">Select</asp:ListItem>
                                                <asp:ListItem Value="0">Not started</asp:ListItem>
                                                <asp:ListItem Value="1">In Progress</asp:ListItem>
                                                <asp:ListItem Value="9">Cancelled</asp:ListItem>
                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                <asp:ListItem Value="3">Shipment within 4 weeks</asp:ListItem>
                                                <asp:ListItem Value="4">(P.O / Drawings) Not Received</asp:ListItem>
                                                <asp:ListItem Value="5">On Hold</asp:ListItem>
                                                <asp:ListItem Value="6">Used from Stock</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblStatus_EditNesting" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>
                                        </EditItemTemplate>

                                        <FooterTemplate>
                                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlStatus_FooterNesting" runat="server">
                                                <asp:ListItem Value="">Select</asp:ListItem>
                                                <asp:ListItem Value="0">Not started</asp:ListItem>
                                                <asp:ListItem Value="1">In Progress</asp:ListItem>
                                                <asp:ListItem Value="9">Cancelled</asp:ListItem>
                                                <asp:ListItem Value="2">Completed</asp:ListItem>
                                                <asp:ListItem Value="3">Shipment within 4 weeks</asp:ListItem>
                                                <asp:ListItem Value="4">(P.O / Drawings) Not Received</asp:ListItem>
                                                <asp:ListItem Value="5">On Hold</asp:ListItem>
                                                <asp:ListItem Value="6">Used from Stock</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblStatus_FooterNesting" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sent To Production" SortExpression="Sent To Production">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSentToProduction" runat="server" Text='<%# Eval("SentToProduction") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Modify">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" Text="Edit" CommandName="Edit"><i class="far fa-edit" title="Edit"></i></asp:LinkButton>
                                            <asp:LinkButton runat="server" CssClass="btn btn-success btn-sm" Text="Send To Production" CommandName="Send" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'><i class="far fa-paper-plane" title="Send To Production"></i></asp:LinkButton>
                                            <asp:LinkButton CssClass="btn btn-danger btn-sm" title="Delete" runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure.?');">
                                                         <i class="far fa-times-circle"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>

                                        <EditItemTemplate>
                                            <asp:LinkButton CssClass="btn btn-success btn-sm" ID="lnkUpdate" runat="server" CommandName="Update"><i class="far fa-save" title="Update"></i></asp:LinkButton>
                                            <asp:LinkButton CssClass="btn btn-danger btn-sm" ID="lnkCancel" runat="server" CommandName="Cancel"><i class="fas fa-redo" title="Redo"></i></asp:LinkButton>
                                        </EditItemTemplate>

                                        <FooterTemplate>
                                            <asp:Button CssClass="btn btn-info btn-sm rounded" ID="btnAddRecord" runat="server" Text="Add" CommandName="Insert" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' />
                                        </FooterTemplate>

                                        <ItemStyle Width="120px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-12" id="divITW" runat="server">
                <div class="table-responsive" style="height: 600px;">
                    <asp:GridView CssClass="table mainGridTable table-sm mb-0" ForeColor="White" ID="gvITWNesting" runat="server" AutoGenerateColumns="false" DataKeyNames="RefId,Timestamp,ShipmentId"
                        OnRowCommand="gvITWNesting_RowCommand" OnRowEditing="gvITWNesting_RowEditing" OnRowCancelingEdit="gvITWNesting_RowCancelingEdit" EmptyDataText="No Items Found"
                        OnRowUpdating="gvITWNesting_RowUpdating">
                        <Columns>
                            <asp:TemplateField HeaderText="Ref #" SortExpression="RefId">
                                <ItemTemplate>
                                    <asp:Label ID="lblRefId" runat="server" Text='<%# Eval("RefId") %>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle Width="140px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Job #" SortExpression="JobID">
                                <ItemTemplate>
                                    <asp:Label ID="lblJobID" runat="server" Text='<%# Eval("JobID") %>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle Width="140px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PO #" SortExpression="PONumber">
                                <ItemTemplate>
                                    <asp:Label ID="lblPONumber" runat="server" Text='<%# Eval("PONumber") %>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle Width="140px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PO Type" SortExpression="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                </ItemTemplate>

                                <%--<ItemStyle Width="140px" />--%>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Project Name" SortExpression="ProjectName">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Ship Date" SortExpression="ShipDate">
                                <ItemTemplate>
                                    <asp:Label ID="lblShipDate" runat="server" Text='<%# Eval("ShipDate") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Start Date" SortExpression="NestingStartDate">
                                <ItemTemplate>
                                    <asp:Label ID="lblNestingStartDate" runat="server" Text='<%# Eval("NestingStartDate") %>'></asp:Label>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox CssClass="form-control form-control-sm" ID="txtStartDate_EditNesting" OnBlur="validateDate(this)" runat="server"
                                        Width="100%" Text='<%#Eval("NestingStartDate") %>'></asp:TextBox>
                                    <asp:CalendarExtender ID="txtStartDate_EditNesting_Extender" runat="server" Format="MM/dd/yyyy"
                                        PopupButtonID="txtStartDate_EditNesting" TargetControlID="txtStartDate_EditNesting">
                                    </asp:CalendarExtender>
                                </EditItemTemplate>

                                <ItemStyle Width="140px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="End Date" SortExpression="NestingEndDate">
                                <ItemTemplate>
                                    <asp:Label ID="lblNestingEndDate" runat="server" Text='<%# Eval("NestingEndDate") %>'></asp:Label>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox CssClass="form-control form-control-sm" ID="txtEndDate_EditNesting" OnBlur="validateDate(this)" runat="server"
                                        Width="100%" Text='<%#Eval("NestingEndDate") %>'></asp:TextBox>
                                    <asp:CalendarExtender ID="txtEndDate_EditNesting_Extender" runat="server" Format="MM/dd/yyyy"
                                        PopupButtonID="txtEndDate_EditNesting" TargetControlID="txtEndDate_EditNesting">
                                    </asp:CalendarExtender>
                                </EditItemTemplate>

                                <%--<ItemStyle Width="140px" />--%>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sent Date" SortExpression="SentDate">
                                <ItemTemplate>
                                    <asp:Label ID="lblNestingSentDate" runat="server" Text='<%# Eval("SentDate") %>'></asp:Label>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox CssClass="form-control form-control-sm" ID="txtSentDate_EditNesting" OnBlur="validateDate(this)" runat="server"
                                        Width="100%" Text='<%#Eval("SentDate") %>'></asp:TextBox>
                                    <asp:CalendarExtender ID="txtSentDate_EditNesting_Extender" runat="server" Format="MM/dd/yyyy"
                                        PopupButtonID="txtSentDate_EditNesting" TargetControlID="txtSentDate_EditNesting">
                                    </asp:CalendarExtender>
                                </EditItemTemplate>


                                <ItemStyle Width="140px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("NestingStatus") %>'></asp:Label>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlStatus_EditNesting" runat="server">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="0">Not started</asp:ListItem>
                                        <asp:ListItem Value="1">In Progress</asp:ListItem>
                                        <%--<asp:ListItem Value="9">Cancelled</asp:ListItem>--%>
                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                        <asp:ListItem Value="3">Shipment within 4 weeks</asp:ListItem>
                                        <asp:ListItem Value="4">(P.O / Drawings) Not Received</asp:ListItem>
                                        <asp:ListItem Value="5">On Hold</asp:ListItem>
                                        <asp:ListItem Value="6">Used from Stock</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblStatus_EditNesting" runat="server" Text='<%#Eval("NestingStatus") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>

                                <FooterTemplate>
                                    <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlStatus_FooterNesting" runat="server">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="0">Not started</asp:ListItem>
                                        <asp:ListItem Value="1">In Progress</asp:ListItem>
                                        <%--<asp:ListItem Value="9">Cancelled</asp:ListItem>--%>
                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                        <asp:ListItem Value="3">Shipment within 4 weeks</asp:ListItem>
                                        <asp:ListItem Value="4">(P.O / Drawings) Not Received</asp:ListItem>
                                        <asp:ListItem Value="5">On Hold</asp:ListItem>
                                        <asp:ListItem Value="6">Used from Stock</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblStatus_FooterNesting" runat="server" Text='<%#Eval("NestingStatus") %>' Visible="false"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sent To Production" SortExpression="Sent To Production">
                                <ItemTemplate>
                                    <asp:Label ID="lblSentToProduction" runat="server" Text='<%# Eval("SentToProduction") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Modify">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" Text="Edit" CommandName="Edit"><i class="far fa-edit" title="Edit"></i></asp:LinkButton>
                                    <asp:LinkButton runat="server" CssClass="btn btn-success btn-sm" Text="Send To Production" CommandName="Send" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'>
                                        <i class="far fa-paper-plane" title="Send To Production"></i></asp:LinkButton>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:LinkButton CssClass="btn btn-success btn-sm" ID="lnkUpdate" runat="server" CommandName="Update"><i class="far fa-save" title="Update"></i></asp:LinkButton>
                                    <asp:LinkButton CssClass="btn btn-danger btn-sm" ID="lnkCancel" runat="server" CommandName="Cancel"><i class="fas fa-redo" title="Redo"></i></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemStyle Width="90px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>

                <div class="row" style="display: none;">
                    <div class="col-sm-4">
                        <div class="row">
                            <div class="col-sm chosenFullWidth">
                                <asp:Panel ID="pmProjectName_ITW_Hidden" runat="server" Style="height: 200px; overflow: scroll; display: none;">
                                </asp:Panel>
                                <label>Project Name</label>
                                <asp:Panel ID="Panel3" runat="server" DefaultButton="btnProjectName_ITW">
                                    <asp:TextBox ID="txtProjectName_ITW" AutoComplete="off" placeholder="Type ITW Job Name" CssClass="form-control form-control-sm" OnBlur="return ClickEventForPName_ITW(event)" runat="server">
                                    </asp:TextBox>
                                    <asp:AutoCompleteExtender ID="PName_Extender" runat="server" TargetControlID="txtProjectName_ITW"
                                        CompletionInterval="1" CompletionSetCount="10" MinimumPrefixLength="1" CompletionListElementID="pmProjectName_ITW_Hidden"
                                        ServicePath="../AutoComplete.asmx" ServiceMethod="SearchITWProject" CompletionListCssClass="autocomplete" />
                                    <asp:Button ID="btnProjectName_ITW" runat="server" Text="Submit" Style="display: none" OnClick="btnProjectName_ITW_Click" />
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <div class="row">
                            <div class="col-sm chosenFullWidth">
                                <asp:Panel ID="PanelJNum_ITW_Hidden" runat="server" Style="height: 200px; overflow: scroll; display: none;">
                                </asp:Panel>
                                <label>Job ID</label>
                                <asp:Panel ID="PanelJNum_ITW" runat="server" DefaultButton="SearchJobId_ITW">
                                    <asp:TextBox ID="txtSearchJobId_ITW" AutoComplete="off" placeholder="Type Job #/PO" CssClass="form-control form-control-sm"
                                        OnBlur="return ClickEvent(event)" runat="server">
                                    </asp:TextBox>
                                    <asp:AutoCompleteExtender ID="AutoCompleteExtender2" runat="server" TargetControlID="txtSearchJobId_ITW"
                                        CompletionInterval="3" CompletionSetCount="10" MinimumPrefixLength="2" CompletionListElementID="PanelJNum_ITW_Hidden"
                                        ServicePath="../AutoComplete.asmx" ServiceMethod="SearchITWJobID" CompletionListCssClass="autocomplete" />
                                    <asp:Button ID="SearchJobId_ITW" runat="server" Text="Submit" Style="display: none" OnClick="SearchJobId_ITW_Click" />
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-12 pl-0" style="display: none;">
                    <div class="row pt-3">
                        <div class="col-12">
                            <h5 class="text-uppercase">Nesting Information</h5>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label class="text-danger">Ref Id*</label>
                                <asp:TextBox ID="txtRefId" runat="server" MaxLength="50" AutoComplete="off" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <label>Nesting Start Date</label>
                                <asp:TextBox CssClass="form-control form-control-sm" ID="txtNestingStartDate" AutoComplete="off" runat="server" OnBlur="validateDate(this)"></asp:TextBox>
                                <asp:CalendarExtender ID="txtNestingStartDate_Extender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtNestingStartDate" TargetControlID="txtNestingStartDate"></asp:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <label>Nesting End Date</label>
                                <asp:TextBox CssClass="form-control form-control-sm" ID="txtNestingEndDate" AutoComplete="off" runat="server" OnBlur="validateDate(this)"></asp:TextBox>
                                <asp:CalendarExtender ID="txtNestingEndDate_Extender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtNestingEndDate" TargetControlID="txtNestingEndDate"></asp:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-sm-2">
                            <div class="form-group">
                                <label>Nesting Sent Date</label>
                                <asp:TextBox CssClass="form-control form-control-sm" ID="txtNestingSentDate" AutoComplete="off" runat="server" OnBlur="validateDate(this)"></asp:TextBox>
                                <asp:CalendarExtender ID="txtNestingSentDate_Extender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtNestingSentDate" TargetControlID="txtNestingSentDate"></asp:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-2">
                            <div class="form-group chosenFullWidth">
                                <label>Nesting Status</label>
                                <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlNestingStatus" runat="server">
                                    <%--<asp:ListItem Value="">Select</asp:ListItem>--%>
                                    <asp:ListItem Value="0">Not started</asp:ListItem>
                                    <asp:ListItem Value="1">In Progress</asp:ListItem>
                                    <asp:ListItem Value="2">Completed</asp:ListItem>
                                    <asp:ListItem Value="3">Shipment within 4 weeks</asp:ListItem>
                                    <asp:ListItem Value="4">(P.O / Drawings) Not Received</asp:ListItem>
                                    <asp:ListItem Value="5">On Hold</asp:ListItem>
                                    <asp:ListItem Value="6">Used from Stock</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-2 d-flex align-items-center">
                            <div class="form-group mb-0">
                                <div class="input-group input-group-sm d-flex align-items-center">
                                    <div class="input-group-prepend pr-3">Send to Production</div>
                                    <asp:CheckBox ID="chkSendToProduction" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="col-8">
                            <%--<label>&nbsp;</label>--%>
                            <div class="form-group">
                                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" OnClick="btnAdd_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12" id="divGaylord" runat="server">
                <div class="table-responsive" style="height: 600px;">
                    <asp:GridView CssClass="table mainGridTable table-sm mb-0" ForeColor="White" ID="gvGaylord" runat="server" AutoGenerateColumns="false" DataKeyNames="Id,Timestamp"
                        OnRowCommand="gvGaylord_RowCommand" OnRowEditing="gvGaylord_RowEditing" OnRowCancelingEdit="gvGaylord_RowCancelingEdit" EmptyDataText="No Items Found"
                        OnRowUpdating="gvGaylord_RowUpdating">
                        <Columns>
                            <asp:TemplateField HeaderText="PO #" SortExpression="PONumber">
                                <ItemTemplate>
                                    <asp:Label ID="lblPONumber" runat="server" Text='<%# Eval("PONumber") %>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle Width="140px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Work Order" SortExpression="WorkOrder">
                                <ItemTemplate>
                                    <asp:Label ID="lblWorkOrder" runat="server" Text='<%# Eval("WorkOrder") %>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle Width="140px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PO Type" SortExpression="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                </ItemTemplate>

                                <%--<ItemStyle Width="140px" />--%>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Project Name" SortExpression="ProjectName">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Start Date" SortExpression="NestingStartDate">
                                <ItemTemplate>
                                    <asp:Label ID="lblNestingStartDate" runat="server" Text='<%# Eval("NestingStartDate") %>'></asp:Label>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox CssClass="form-control form-control-sm" ID="txtStartDate_EditNesting" OnBlur="validateDate(this)" runat="server"
                                        Width="100%" Text='<%#Eval("NestingStartDate") %>'></asp:TextBox>
                                    <asp:CalendarExtender ID="txtStartDate_EditNesting_Extender" runat="server" Format="MM/dd/yyyy"
                                        PopupButtonID="txtStartDate_EditNesting" TargetControlID="txtStartDate_EditNesting">
                                    </asp:CalendarExtender>
                                </EditItemTemplate>

                                <ItemStyle Width="140px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="End Date" SortExpression="NestingEndDate">
                                <ItemTemplate>
                                    <asp:Label ID="lblNestingEndDate" runat="server" Text='<%# Eval("NestingEndDate") %>'></asp:Label>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox CssClass="form-control form-control-sm" ID="txtEndDate_EditNesting" OnBlur="validateDate(this)" runat="server"
                                        Width="100%" Text='<%#Eval("NestingEndDate") %>'></asp:TextBox>
                                    <asp:CalendarExtender ID="txtEndDate_EditNesting_Extender" runat="server" Format="MM/dd/yyyy"
                                        PopupButtonID="txtEndDate_EditNesting" TargetControlID="txtEndDate_EditNesting">
                                    </asp:CalendarExtender>
                                </EditItemTemplate>

                                <%--<ItemStyle Width="140px" />--%>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sent Date" SortExpression="SentDate">
                                <ItemTemplate>
                                    <asp:Label ID="lblNestingSentDate" runat="server" Text='<%# Eval("NestingSentDate") %>'></asp:Label>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox CssClass="form-control form-control-sm" ID="txtSentDate_EditNesting" OnBlur="validateDate(this)" runat="server"
                                        Width="100%" Text='<%#Eval("NestingSentDate") %>'></asp:TextBox>
                                    <asp:CalendarExtender ID="txtSentDate_EditNesting_Extender" runat="server" Format="MM/dd/yyyy"
                                        PopupButtonID="txtSentDate_EditNesting" TargetControlID="txtSentDate_EditNesting">
                                    </asp:CalendarExtender>
                                </EditItemTemplate>

                                <ItemStyle Width="140px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("NestingStatus") %>'></asp:Label>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlStatus_EditNesting" runat="server">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="0">Not started</asp:ListItem>
                                        <asp:ListItem Value="1">In Progress</asp:ListItem>
                                        <%--<asp:ListItem Value="9">Cancelled</asp:ListItem>--%>
                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                        <asp:ListItem Value="3">Shipment within 4 weeks</asp:ListItem>
                                        <asp:ListItem Value="4">(P.O / Drawings) Not Received</asp:ListItem>
                                        <asp:ListItem Value="5">On Hold</asp:ListItem>
                                        <asp:ListItem Value="6">Used from Stock</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblStatus_EditNesting" runat="server" Text='<%#Eval("NestingStatus") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>

                                <FooterTemplate>
                                    <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlStatus_FooterNesting" runat="server">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="0">Not started</asp:ListItem>
                                        <asp:ListItem Value="1">In Progress</asp:ListItem>
                                        <%--<asp:ListItem Value="9">Cancelled</asp:ListItem>--%>
                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                        <asp:ListItem Value="3">Shipment within 4 weeks</asp:ListItem>
                                        <asp:ListItem Value="4">(P.O / Drawings) Not Received</asp:ListItem>
                                        <asp:ListItem Value="5">On Hold</asp:ListItem>
                                        <asp:ListItem Value="6">Used from Stock</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblStatus_FooterNesting" runat="server" Text='<%#Eval("NestingStatus") %>' Visible="false"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sent To Production" SortExpression="Sent To Production">
                                <ItemTemplate>
                                    <asp:Label ID="lblSentToProduction" runat="server" Text='<%# Eval("SentToProduction") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Modify">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" Text="Edit" CommandName="Edit"><i class="far fa-edit" title="Edit"></i></asp:LinkButton>
                                    <asp:LinkButton runat="server" CssClass="btn btn-success btn-sm" Text="Send To Production" CommandName="Send" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'>
                                        <i class="far fa-paper-plane" title="Send To Production"></i></asp:LinkButton>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:LinkButton CssClass="btn btn-success btn-sm" ID="lnkUpdate" runat="server" CommandName="Update"><i class="far fa-save" title="Update"></i></asp:LinkButton>
                                    <asp:LinkButton CssClass="btn btn-danger btn-sm" ID="lnkCancel" runat="server" CommandName="Cancel"><i class="fas fa-redo" title="Redo"></i></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemStyle Width="90px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <div class="col-12" id="divCaddy" runat="server">
                <div class="table-responsive" style="height: 600px;">
                    <asp:GridView CssClass="table mainGridTable table-sm mb-0" ForeColor="White" ID="gvCaddyNestingTasks" runat="server" AutoGenerateColumns="false" EmptyDataText="No Items Found">
                        <Columns>
                            <asp:TemplateField HeaderText="PO #" SortExpression="PONumber">
                                <ItemTemplate>
                                    <asp:Label ID="lblPONumber" runat="server" Text='<%# Eval("PONumber") %>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle Width="140px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Work Order" SortExpression="WorkOrder">
                                <ItemTemplate>
                                    <asp:Label ID="lblWorkOrder" runat="server" Text='<%# Eval("WorkOrder") %>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle Width="140px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="PO Type" SortExpression="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                </ItemTemplate>

                                <%--<ItemStyle Width="140px" />--%>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Project Name" SortExpression="ProjectName">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Start Date" SortExpression="NestingStartDate">
                                <ItemTemplate>
                                    <asp:Label ID="lblNestingStartDate" runat="server" Text='<%# Eval("NestingStartDate") %>'></asp:Label>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox CssClass="form-control form-control-sm" ID="txtStartDate_EditNesting" OnBlur="validateDate(this)" runat="server"
                                        Width="100%" Text='<%#Eval("NestingStartDate") %>'></asp:TextBox>
                                    <asp:CalendarExtender ID="txtStartDate_EditNesting_Extender" runat="server" Format="MM/dd/yyyy"
                                        PopupButtonID="txtStartDate_EditNesting" TargetControlID="txtStartDate_EditNesting">
                                    </asp:CalendarExtender>
                                </EditItemTemplate>

                                <ItemStyle Width="140px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="End Date" SortExpression="NestingEndDate">
                                <ItemTemplate>
                                    <asp:Label ID="lblNestingEndDate" runat="server" Text='<%# Eval("NestingEndDate") %>'></asp:Label>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox CssClass="form-control form-control-sm" ID="txtEndDate_EditNesting" OnBlur="validateDate(this)" runat="server"
                                        Width="100%" Text='<%#Eval("NestingEndDate") %>'></asp:TextBox>
                                    <asp:CalendarExtender ID="txtEndDate_EditNesting_Extender" runat="server" Format="MM/dd/yyyy"
                                        PopupButtonID="txtEndDate_EditNesting" TargetControlID="txtEndDate_EditNesting">
                                    </asp:CalendarExtender>
                                </EditItemTemplate>

                                <%--<ItemStyle Width="140px" />--%>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sent Date" SortExpression="SentDate">
                                <ItemTemplate>
                                    <asp:Label ID="lblNestingSentDate" runat="server" Text='<%# Eval("NestingSentDate") %>'></asp:Label>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:TextBox CssClass="form-control form-control-sm" ID="txtSentDate_EditNesting" OnBlur="validateDate(this)" runat="server"
                                        Width="100%" Text='<%#Eval("NestingSentDate") %>'></asp:TextBox>
                                    <asp:CalendarExtender ID="txtSentDate_EditNesting_Extender" runat="server" Format="MM/dd/yyyy"
                                        PopupButtonID="txtSentDate_EditNesting" TargetControlID="txtSentDate_EditNesting">
                                    </asp:CalendarExtender>
                                </EditItemTemplate>

                                <ItemStyle Width="140px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status" SortExpression="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("NestingStatus") %>'></asp:Label>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlStatus_EditNesting" runat="server">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="0">Not started</asp:ListItem>
                                        <asp:ListItem Value="1">In Progress</asp:ListItem>
                                        <%--<asp:ListItem Value="9">Cancelled</asp:ListItem>--%>
                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                        <asp:ListItem Value="3">Shipment within 4 weeks</asp:ListItem>
                                        <asp:ListItem Value="4">(P.O / Drawings) Not Received</asp:ListItem>
                                        <asp:ListItem Value="5">On Hold</asp:ListItem>
                                        <asp:ListItem Value="6">Used from Stock</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblStatus_EditNesting" runat="server" Text='<%#Eval("NestingStatus") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>

                                <FooterTemplate>
                                    <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlStatus_FooterNesting" runat="server">
                                        <asp:ListItem Value="">Select</asp:ListItem>
                                        <asp:ListItem Value="0">Not started</asp:ListItem>
                                        <asp:ListItem Value="1">In Progress</asp:ListItem>
                                        <%--<asp:ListItem Value="9">Cancelled</asp:ListItem>--%>
                                        <asp:ListItem Value="2">Completed</asp:ListItem>
                                        <asp:ListItem Value="3">Shipment within 4 weeks</asp:ListItem>
                                        <asp:ListItem Value="4">(P.O / Drawings) Not Received</asp:ListItem>
                                        <asp:ListItem Value="5">On Hold</asp:ListItem>
                                        <asp:ListItem Value="6">Used from Stock</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblStatus_FooterNesting" runat="server" Text='<%#Eval("NestingStatus") %>' Visible="false"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Sent To Production" SortExpression="Sent To Production">
                                <ItemTemplate>
                                    <asp:Label ID="lblSentToProduction" runat="server" Text='<%# Eval("SentToProduction") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Modify">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" Text="Edit" CommandName="Edit"><i class="far fa-edit" title="Edit"></i></asp:LinkButton>
                                    <asp:LinkButton runat="server" CssClass="btn btn-success btn-sm" Text="Send To Production" CommandName="Send" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'>
                                        <i class="far fa-paper-plane" title="Send To Production"></i></asp:LinkButton>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:LinkButton CssClass="btn btn-success btn-sm" ID="lnkUpdate" runat="server" CommandName="Update"><i class="far fa-save" title="Update"></i></asp:LinkButton>
                                    <asp:LinkButton CssClass="btn btn-danger btn-sm" ID="lnkCancel" runat="server" CommandName="Cancel"><i class="fas fa-redo" title="Redo"></i></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemStyle Width="90px" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
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
            $('#<%=ddlNestingFor.ClientID%>').chosen();
            $('#<%=ddlNestingStatus.ClientID%>').chosen();
        }

        function ClickEventForPName(e) {
            __doPostBack('<%=SearchPNameButton.UniqueID%>', "");
        }

        function ClickEventForPName_ITW(e) {
            __doPostBack('<%=btnProjectName_ITW.UniqueID%>', "");
        }

        function ClickEvent(e) {
            __doPostBack('<%=SearchJobId_ITW.UniqueID%>', "");
        }

        function EnterEventForJobID(e) {
            if (e.keyCode == 13) {
                __doPostBack('<%=SearchJobIDButton.UniqueID%>', "");
            }
        }

        function ClickEventForJobID(e) {
            __doPostBack('<%=SearchJobIDButton.UniqueID%>', "");
        }
    </script>
</asp:Content>
