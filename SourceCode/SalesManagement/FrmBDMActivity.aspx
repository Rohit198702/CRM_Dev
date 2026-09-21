<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmBDMActivity.aspx.cs" Inherits="SalesManagement_FrmBDMActivity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="up_BDMActivity" runat="server">
        <ContentTemplate>
            <div class="col-12 pt-2 piDiv position-sticky">
                <div class="row">
                    <div class="col-12">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">BDM Activity</h4>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-2">
                        <div class="form-group">
                            <label>BDM</label>
                            <asp:DropDownList ID="ddlBDMHeaderList" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlBDMHeaderList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-2">
                        <div class="form-group">
                            <label>Activity#</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlActivityHeaderList" runat="server" DataTextField="text" DataValueField="id" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlActivityHeaderList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-6">
                        <label>&nbsp;</label>
                        <div class="row">
                            <div class="col-auto">
                                <asp:Button ID="btnNewActivity" runat="server" CssClass="btn btn-primary btn-sm" Text="Create Activity" OnClick="btnNewActivity_Click" />
                                <asp:Button ID="btnSaveActivity" runat="server" CssClass="btn btn-success btn-sm" CausesValidation="false" Text="Save Activity" OnClick="btnSaveActivity_Click" />
                                <asp:Button ID="btnFilter" runat="server" CssClass="btn btn-secondary btn-sm" Enabled="true" OnClick="btnFilter_Click" Text="Report" />
                                <%-- <asp:Button ID="btnOpenSpec" runat="server" CssClass="btn btn-primary btn-sm" Text="Manage Spec" OnClick="btnOpenSpec_Click" />
                                <asp:Button ID="btnOpenParticipant" runat="server" CssClass="btn btn-secondary btn-sm" Text="Manage Followup" OnClick="btnOpenParticipant_Click" />
                                <asp:Button ID="btnopenFollowup" runat="server" CssClass="btn btn-secondary btn-sm" Text="Manage Followup" OnClick="btnopenFollowup_Click" />--%>
                                <asp:Button ID="btnCancelActivity" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancelActivity_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-12 row mt-2">
                <div class="col-12">
                    <h5 class="text-uppercase">Activity Information                       
                    </h5>
                </div>

                <div class="col-2">
                    <div class="form-group">
                        <label class="text-danger">Activity#*</label>
                        <asp:TextBox ID="txtActivityNo" runat="server" CssClass="form-control form-control-sm" disabled />
                    </div>
                </div>

                <div class="col-2">
                    <div class="form-group">
                        <label class="text-danger">BDM*</label>
                        <asp:DropDownList ID="ddlBDM" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm"></asp:DropDownList>
                    </div>
                </div>

                <div class="col-2">
                    <div class="form-group">
                        <label class="text-danger">Activity Type*</label>
                        <asp:DropDownList ID="ddlActivityType" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm" autocomplete="off"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlActivityType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-2">
                    <div class="form-group">
                        <label class="text-danger">Activity Date*</label>
                        <asp:TextBox ID="txtActivityDate" runat="server" CssClass="form-control form-control-sm " autocomplete="off" OnBlur="validateDate(this)" />
                        <asp:CalendarExtender ID="txtActivityDate_Extender" runat="server" Format="MM/dd/yyyy"
                            PopupButtonID="txtActivityDate" TargetControlID="txtActivityDate">
                        </asp:CalendarExtender>
                    </div>
                </div>

                <div class="col-2">
                    <div class="form-group">
                        <label>Location Type</label>
                        <asp:DropDownList ID="ddlLocationType" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm"></asp:DropDownList>
                    </div>
                </div>

                <div class="col-2">
                    <div class="form-group">
                        <label>Visit Mode</label>
                        <asp:DropDownList ID="ddlVisitMode" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm"></asp:DropDownList>
                    </div>
                </div>

                <div class="col-2">
                    <div class="form-group">
                        <label class="text-danger">Status*</label>
                        <asp:DropDownList ID="ddlStatus" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm"></asp:DropDownList>
                    </div>
                </div>

                <div class="col-2">
                    <div class="form-group">
                        <label class="text-danger">Subject*</label>
                        <asp:TextBox ID="txtSubject" runat="server" MaxLength="200" CssClass="form-control form-control-sm" autocomplete="off" />
                    </div>
                </div>

                <div class="col-sm-4">
                    <div class="form-group">
                        <label>Discussion</label>
                        <asp:TextBox ID="txtDiscussion" runat="server" TextMode="MultiLine" oninput="return limitMultiLineInputLength(this, 5000)"
                            CssClass="form-control form-control-sm " autocomplete="off" />
                    </div>
                </div>

                <div class="col-sm-4">
                    <div class="form-group">
                        <label>Business Outcome</label>
                        <asp:TextBox ID="txtBusinessOutcome" runat="server" TextMode="MultiLine" oninput="return limitMultiLineInputLength(this, 1000)"
                            CssClass="form-control form-control-sm " autocomplete="off" />
                    </div>
                </div>

                <div class="col-sm-2 d-flex align-items-center">
                    <div class="form-group mb-0">
                        <div class="input-group input-group-sm d-flex align-items-center">
                            <div class="input-group-prepend pr-3">Team Posted</div>
                            <asp:CheckBox ID="chkTeamPosted" CssClass="text" runat="server" />
                        </div>
                    </div>
                </div>

                <div class="col-2">
                    <div class="form-group">
                        <label>Teams Posted Date</label>
                        <asp:TextBox ID="txtTeamPostedDate" runat="server" CssClass="form-control form-control-sm " autocomplete="off" OnBlur="validateDate(this)" />
                        <asp:CalendarExtender ID="txtTeamPostedDate_Extender" runat="server" Format="MM/dd/yyyy"
                            PopupButtonID="txtTeamPostedDate" TargetControlID="txtTeamPostedDate">
                        </asp:CalendarExtender>
                    </div>
                </div>

                <div class="col-2">
                    <div class="form-group">
                        <label>Duration(hours)</label>
                        <asp:TextBox ID="txtDurations" oninput="enforceDecimal(this, 3, 1);" AutoComplete="off" runat="server"
                            CssClass="form-control form-control-sm text-right" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                    </div>
                </div>

                <div class="col-12">
                    <asp:GridView ID="gvActivity" runat="server" CssClass="table mainGridTable table-sm mb-2" AutoGenerateColumns="False" DataKeyNames="ActivityID, Timestamp"
                        EnableModelValidation="True" OnRowEditing="gvActivity_RowEditing" OnRowDeleting="gvActivity_RowDeleting" OnRowDataBound="gvActivity_RowDataBound"
                        OnRowCommand="gvActivity_RowCommand" EmptyDataText="No Activity exists">
                        <Columns>
                            <asp:TemplateField HeaderText="Activity No" SortExpression="ActivityNo">
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityNo" runat="server" Text='<%# Eval("ActivityNo") %>'></asp:Label>
                                </ItemTemplate>
                                <%--<ItemStyle Width="100px" />--%>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Activity Type" SortExpression="ActivityType">
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityType" runat="server" Text='<%# Eval("ActivityType") %>'></asp:Label>
                                </ItemTemplate>
                                <%--<ItemStyle Width="200px" />--%>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Subject" SortExpression="Subject">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Participant Count" SortExpression="ParticipantCount" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblParticipantCount" runat="server" Text='<%# Eval("ParticipantCount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Spec Count" SortExpression="SpecCount" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecCount" runat="server" Text='<%# Eval("SpecCount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Followup Count" SortExpression="FollowupCount" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblFollowupCount" runat="server" Text='<%# Eval("FollowupCount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Modify">
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" CssClass="btn btn-outline-primary btn-sm mb-1" Text="Edit Activity" CommandName="Edit"></asp:LinkButton>
                                    <asp:LinkButton runat="server" CssClass="btn btn-outline-secondary btn-sm mb-1" Text="Participants" ID="lnkOpenParticipant" CommandName="Participant" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                    <asp:LinkButton runat="server" CssClass="btn btn-outline-success btn-sm mb-1" Text="Specs" ID="lnkOpenSpecs" CommandName="Spec" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                    <asp:LinkButton runat="server" CssClass="btn btn-outline-secondary btn-sm mb-1" Text="Follow-ups" ID="lnkFollowups" CommandName="Followup" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'></asp:LinkButton>
                                    <asp:LinkButton CssClass="btn btn-outline-danger btn-sm mb-1" title="Delete" runat="server" Text="Delete Activity" CommandName="Delete" OnClientClick="return confirm('Are you sure.?');">                                               
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="29%" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <div class="col-12 row">
                <asp:LinkButton ID="btnParticipant" runat="server"></asp:LinkButton>
                <asp:ModalPopupExtender ID="Modal_Participant" runat="server" TargetControlID="btnParticipant"
                    PopupControlID="Panel_Participant" BackgroundCssClass="modalBackground" CancelControlID="btnCloseParticipant_Participant">
                </asp:ModalPopupExtender>
                <asp:Panel ID="Panel_Participant" runat="server" CssClass="ReportsModalPopup" Style="display: none" Width="80%" Height="80%">
                    <div class="position-relative h-100">
                        <asp:ImageButton CssClass="position-absolute crossCloseBtn" ID="btnCloseParticipant_Participant" runat="server" ImageUrl="../images/closebtnCircle.png"
                            AlternateText="Close Popup" ToolTip="Close Popup" />
                        <div class="overflow-auto h-100">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-group">
                                            <h5 class="text-uppercase ">Activity:-
                                            <asp:Label ID="lblActivity_Participant" runat="server"></asp:Label>
                                            </h5>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="col-12 row">
                                    <div class="col-12 mt-2">
                                        <h5 class="text-uppercase">
                                            <span id="lblParticipant" runat="server">Participants</span> Information                                            
                                        </h5>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group chosenFullWidth">
                                            <label class="text-danger">Participant Type*</label>
                                            <asp:DropDownList ID="ddlParticipantType" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlParticipantType_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group chosenFullWidth">
                                            <label class="text-danger">Company Name*</label>
                                            <%--                        <asp:DropDownList ID="ddlCompany" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm" AutoPostBack="true"
                             OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                        </asp:DropDownList>--%>
                                            <asp:ListBox ID="ddlCompany" runat="server" CssClass="form-control form-control-sm" DataTextField="text" DataValueField="id" SelectionMode="multiple"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"></asp:ListBox>
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group chosenFullWidth">
                                            <label>Contact</label>
                                            <%--                        <asp:DropDownList ID="ddlContact" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlContact_SelectedIndexChanged">
                        </asp:DropDownList>--%>
                                            <asp:ListBox ID="ddlContact" runat="server" CssClass="form-control form-control-sm" DataTextField="text" DataValueField="id" SelectionMode="multiple"></asp:ListBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-2 d-flex align-items-center">
                                        <div class="form-group mb-0">
                                            <div class="input-group input-group-sm d-flex align-items-center">
                                                <div class="input-group-prepend pr-3">Is Primary</div>
                                                <asp:CheckBox ID="chkIsPrimary" CssClass="text" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-2" id="divLocation_Participant" runat="server" visible="false">
                                        <div class="form-group chosenFullWidth">
                                            <label>Location</label>
                                            <asp:DropDownList ID="ddlLocation_Participant" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-2" id="divMaterialUsed_Participant" runat="server" visible="false">
                                        <div class="form-group chosenFullWidth">
                                            <label>Material Used</label>
                                            <asp:DropDownList ID="ddlMaterialUsed_Participant" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-sm-4" id="divDetails_Participant" runat="server">
                                        <div class="form-group chosenFullWidth">
                                            <label>Details</label>
                                            <asp:TextBox ID="txtDetails_Participant" runat="server" TextMode="MultiLine" oninput="return limitMultiLineInputLength(this, 500)"
                                                CssClass="form-control form-control-sm " autocomplete="off" />
                                        </div>
                                    </div>

                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <label>Remarks</label>
                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" oninput="return limitMultiLineInputLength(this, 500)"
                                                CssClass="form-control form-control-sm " autocomplete="off" />
                                        </div>
                                    </div>

                                    <div class="col-2" style="display: none;">
                                        <div class="form-group chosenFullWidth">
                                            <label class="text-danger">Status*</label>
                                            <asp:DropDownList ID="ddlParticipantStatus" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <asp:Button ID="btnSaveParticipant" runat="server" CssClass="btn btn-success btn-sm" CausesValidation="false" Text="Add Participant" OnClick="btnSaveParticipant_Click" />
                                        <asp:Button ID="btnCancelParticipant" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancelParticipant_Click" />
                                    </div>

                                    <div class="col-12 mt-3">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvActivityParticipant" runat="server" CssClass="table mainGridTable table-sm mb-0" AutoGenerateColumns="False" DataKeyNames="ActivityParticipantID, Timestamp"
                                                EnableModelValidation="True" OnRowEditing="gvActivityParticipant_RowEditing" OnRowDeleting="gvActivityParticipant_RowDeleting" EmptyDataText="No participant exists">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Participant Type" SortExpression="ParticipantType">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblParticipantType" runat="server" Text='<%# Eval("ParticipantType") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Company Name" SortExpression="CompanyName">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Contact Name" SortExpression="ContactName">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContactName" runat="server" Text='<%# Eval("ContactName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Is Primary" SortExpression="IsPrimary">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIsPrimary" runat="server" Text='<%# Eval("IsPrimary") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Modify">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" Text="Edit" CommandName="Edit"><i class="far fa-edit" title="Edit"></i></asp:LinkButton>
                                                            <asp:LinkButton CssClass="btn btn-danger btn-sm" title="Delete" runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure.?');">
                                                <i class="far fa-times-circle"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div class="col-12 row">
                <asp:LinkButton ID="btnFollowup" runat="server"></asp:LinkButton>
                <asp:ModalPopupExtender ID="Modal_Followup" runat="server" TargetControlID="btnFollowup"
                    PopupControlID="Panel_Followup" BackgroundCssClass="modalBackground" CancelControlID="btnCloseFollowup_Followup">
                </asp:ModalPopupExtender>
                <asp:Panel ID="Panel_Followup" runat="server" CssClass="ReportsModalPopup" Style="display: none" Width="80%" Height="80%">
                    <div class="position-relative h-100">
                        <asp:ImageButton CssClass="position-absolute crossCloseBtn" ID="btnCloseFollowup_Followup" runat="server" ImageUrl="../images/closebtnCircle.png"
                            AlternateText="Close Popup" ToolTip="Close Popup" />
                        <div class="overflow-auto h-100">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-group">
                                            <h5 class="text-uppercase ">Activity:-
                                        <asp:Label ID="lblActivity_Followup" runat="server"></asp:Label>
                                            </h5>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="col-12 row mt-2">
                                    <div class="col-12 mt-2">
                                        <h5 class="text-uppercase">Followup Information                                                                                     
                                        </h5>
                                    </div>

                                    <div class="col-12 row">
                                        <div class="col-auto">
                                            <div class="form-group">
                                                <asp:Button ID="btnNewFollowup" runat="server" CssClass="btn btn-primary btn-sm" Text="Create Followup" OnClick="btnNewFollowup_Click" />
                                                <asp:Button ID="btnAddFollowup" runat="server" CssClass="btn btn-success btn-sm" Text="Add" OnClick="btnAddFollowup_Click" />
                                                <asp:Button ID="btnCancelFollowup" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancelFollowup_Click" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label class="text-danger">Followup#*</label>
                                            <asp:TextBox ID="txtFollowupNo_Followup" runat="server" CssClass="form-control form-control-sm" disabled />
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group chosenFullWidth">
                                            <label class="text-danger">Followup Type*</label>
                                            <asp:DropDownList ID="ddlFollowupType_Followup" runat="server" CssClass="form-control form-control-sm"
                                                DataTextField="text" DataValueField="id">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label class="text-danger">Followup Date*</label>
                                            <asp:TextBox ID="txtFollowupDate_Followup" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)">
                                            </asp:TextBox>
                                            <asp:CalendarExtender ID="txtFollowupDate_Followup_Extender" runat="server" Format="MM/dd/yyyy"
                                                PopupButtonID="txtFollowupDate_Followup" TargetControlID="txtFollowupDate_Followup">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label class="text-danger">Subject*</label>
                                            <asp:TextBox ID="txtSubject_Followup" CssClass="form-control form-control-sm" runat="server" autocomplete="off" MaxLength="200">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group chosenFullWidth">
                                            <label>Assigned To</label>
                                            <asp:DropDownList ID="ddlAssignedTo_Followup" runat="server" CssClass="form-control form-control-sm" DataTextField="text" DataValueField="id">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Description</label>
                                            <asp:TextBox ID="txtDescription_Followup" CssClass="form-control form-control-sm" runat="server" autocomplete="off" TextMode="MultiLine"
                                                oninput="return limitMultiLineInputLength(this, 5000)">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group chosenFullWidth">
                                            <label class="text-danger">Priority*</label>
                                            <asp:DropDownList ID="ddlPriority_Followup" runat="server" CssClass="form-control form-control-sm" DataTextField="text" DataValueField="id">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label class="text-danger">Due Date*</label>
                                            <asp:TextBox ID="txtDueDate_Followup" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)">
                                            </asp:TextBox>
                                            <asp:CalendarExtender ID="txtDueDate_Followup_Extender" runat="server" Format="MM/dd/yyyy"
                                                PopupButtonID="txtDueDate_Followup" TargetControlID="txtDueDate_Followup">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Completed On</label>
                                            <asp:TextBox ID="txtCompletedOnDate_Followup" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)">
                                            </asp:TextBox>
                                            <asp:CalendarExtender ID="txtCompletedOnDate_Followup_Extender" runat="server" Format="MM/dd/yyyy"
                                                PopupButtonID="txtCompletedOnDate_Followup" TargetControlID="txtCompletedOnDate_Followup">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group chosenFullWidth">
                                            <label class="text-danger">Status*</label>
                                            <asp:DropDownList ID="ddlStatus_Followup" runat="server" CssClass="form-control form-control-sm" DataTextField="text" DataValueField="id">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Completion Remarks</label>
                                            <asp:TextBox ID="txtRemarks_Followup" CssClass="form-control form-control-sm" runat="server" autocomplete="off" TextMode="MultiLine"
                                                oninput="return limitMultiLineInputLength(this, 1000)">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Reminder Date</label>
                                            <asp:TextBox ID="txtReminderDate_Followup" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)">
                                            </asp:TextBox>
                                            <asp:CalendarExtender ID="txtReminderDate_Followup_Extender" runat="server" Format="MM/dd/yyyy"
                                                PopupButtonID="txtReminderDate_Followup" TargetControlID="txtReminderDate_Followup">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>

                                    <div class="col-sm-2 d-flex align-items-center">
                                        <div class="form-group mb-0">
                                            <div class="input-group input-group-sm d-flex align-items-center">
                                                <div class="input-group-prepend pr-3">Reminder Sent</div>
                                                <asp:CheckBox ID="chkReminderSent" CssClass="text" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-12">
                                        <div class="table-responsive">
                                            <asp:GridView CssClass="table mainGridTable table-sm mb-0" ID="gvFollowups" runat="server" AutoGenerateColumns="False" DataKeyNames="FollowUpID,TimeStamp"
                                                EnableModelValidation="True" OnRowDeleting="gvFollowups_RowDeleting" OnRowEditing="gvFollowups_RowEditing" EmptyDataText="No followups exists">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Followup No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFollowupNo" runat="server" Text='<%# Eval("FollowupNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <%--<ItemStyle Width="15%" />--%>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Followup Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFollowupDate" runat="server" Text='<%# Eval("FollowupDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <%--<ItemStyle Width="15%" />--%>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Subject">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <%--<ItemStyle Width="25%" />--%>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Modify">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" Text="Edit" CommandName="Edit"><i class="far fa-edit" title="Edit"></i></asp:LinkButton>
                                                            <asp:LinkButton CssClass="btn btn-danger btn-sm" title="Delete" runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure.?');">
                                                                <i class="far fa-times-circle"></i>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <%--</div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div class="col-12 row">
                <asp:LinkButton ID="btnSpec" runat="server"></asp:LinkButton>
                <asp:ModalPopupExtender ID="Modal_Spec" runat="server" TargetControlID="btnSpec"
                    PopupControlID="Panel_Spec" BackgroundCssClass="modalBackground" CancelControlID="btnCloseFollowup_Spec">
                </asp:ModalPopupExtender>
                <asp:Panel ID="Panel_Spec" runat="server" CssClass="ReportsModalPopup" Style="display: none" Width="80%" Height="80%">
                    <div class="position-relative h-100">
                        <asp:ImageButton CssClass="position-absolute crossCloseBtn" ID="btnCloseFollowup_Spec" runat="server" ImageUrl="../images/closebtnCircle.png"
                            AlternateText="Close Popup" ToolTip="Close Popup" />
                        <div class="overflow-auto h-100">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-12">
                                        <div class="form-group">
                                            <h5 class="text-uppercase ">Activity:-
                                                <asp:Label ID="lblActivity_Spec" runat="server"></asp:Label>
                                            </h5>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="col-12 row mt-2">
                                    <div class="col-12 mt-2">
                                        <h5 class="text-uppercase">Spec Information
                                        </h5>
                                    </div>
                                    <div class="col-12">
                                        <div class="form-group">
                                            <asp:Button ID="btnNewSpec" runat="server" CssClass="btn btn-primary btn-sm" Text="Create Spec" OnClick="btnNewSpec_Click" />
                                            <asp:Button ID="btnAddSpec" runat="server" CssClass="btn btn-success btn-sm" Text="Add" OnClick="btnAddSpec_Click" />
                                            <asp:Button ID="btnCancelSpec" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancelSpec_Click" />
                                        </div>
                                    </div>
                                    <div class="col-2">
                                        <div class="form-group">
                                            <label class="text-danger">Spec #*</label>
                                            <asp:TextBox ID="txtSpecNo_Spec" runat="server" CssClass="form-control form-control-sm" disabled />
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label class="text-danger">Project Name*</label>
                                            <asp:TextBox ID="txtProjectName_Spec" CssClass="form-control form-control-sm" runat="server" autocomplete="off" MaxLength="80">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group chosenFullWidth">
                                            <label>Industry</label>
                                            <asp:DropDownList ID="ddlIndustry_Spec" runat="server" CssClass="form-control form-control-sm" DataTextField="text" DataValueField="id">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group chosenFullWidth">
                                            <label>Country</label>
                                            <asp:DropDownList ID="ddlCountry_Spec" runat="server" CssClass="form-control form-control-sm" DataTextField="text" DataValueField="id"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlCountry_Spec_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group chosenFullWidth">
                                            <label>State</label>
                                            <asp:DropDownList ID="ddlState_Spec" runat="server" CssClass="form-control form-control-sm" DataTextField="text" DataValueField="id">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>City</label>
                                            <asp:TextBox ID="txtCity_Spec" CssClass="form-control form-control-sm" runat="server" autocomplete="off" MaxLength="100">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Address</label>
                                            <asp:TextBox ID="txtAddress_Spec" CssClass="form-control form-control-sm" runat="server" autocomplete="off"
                                                TextMode="MultiLine" oninput="return limitMultiLineInputLength(this, 500)">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group chosenFullWidth">
                                            <label class="text-danger">Project Stage*</label>
                                            <asp:DropDownList ID="ddlProjectStage_Spec" runat="server" CssClass="form-control form-control-sm" DataTextField="text" DataValueField="id"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlProjectStage_Spec_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-2" style="display: none;">
                                        <div class="form-group chosenFullWidth">
                                            <label class="text-danger">Status*</label>
                                            <asp:DropDownList ID="ddlStatus_Spec" runat="server" CssClass="form-control form-control-sm" DataTextField="text" DataValueField="id">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group chosenFullWidth">
                                            <label class="text-danger">Priority*</label>
                                            <asp:DropDownList ID="ddlPriority_Spec" runat="server" CssClass="form-control form-control-sm" DataTextField="text" DataValueField="id">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Estimated Value($)</label>
                                            <asp:TextBox ID="txtEstimatedValue_Spec" oninput="enforceDecimal(this, 10, 2);" AutoComplete="off" runat="server"
                                                CssClass="form-control form-control-sm text-right" onkeyup="javascript:this.value=Comma(this.value);"
                                                onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Competitor</label>
                                            <asp:TextBox ID="txtCompetitor_Spec" CssClass="form-control form-control-sm" runat="server" autocomplete="off"
                                                TextMode="MultiLine" oninput="return limitMultiLineInputLength(this, 250)">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Business Opportunity</label>
                                            <asp:TextBox ID="txtBusinessOpportunity_Spec" CssClass="form-control form-control-sm" runat="server" autocomplete="off"
                                                TextMode="MultiLine" oninput="return limitMultiLineInputLength(this, 500)">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label>Current Issue</label>
                                            <asp:TextBox ID="txtCurrentIssue_Spec" CssClass="form-control form-control-sm" runat="server" autocomplete="off"
                                                TextMode="MultiLine" oninput="return limitMultiLineInputLength(this, 5000)">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-4">
                                        <div class="form-group">
                                            <label id="lblNextAction" runat="server">Next Action</label>
                                            <asp:TextBox ID="txtNextAction_Spec" CssClass="form-control form-control-sm" runat="server" autocomplete="off"
                                                TextMode="MultiLine" oninput="return limitMultiLineInputLength(this, 1000)">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label id="lblNextActionDate" runat="server">Next Action Date</label>
                                            <asp:TextBox ID="txtNextActionDate_Spec" runat="server" CssClass="form-control form-control-sm" OnBlur="validateDate(this)" autocomplete="off" />
                                            <asp:CalendarExtender ID="txtNextActionDate_Spec_Extender" runat="server" Format="MM/dd/yyyy"
                                                PopupButtonID="txtNextActionDate_Spec" TargetControlID="txtNextActionDate_Spec">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group chosenFullWidth">
                                            <label>Probability (%)</label>
                                            <%--<asp:TextBox ID="txtProbability_Spec" CssClass="form-control form-control-sm" runat="server" autocomplete="off" MaxLength="3"
                                                onkeypress="return onlyNumbers(this,event);" oninput="if(parseInt(this.value,10)>100)this.value=100;">
                                            </asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlProbability_Spec" runat="server" CssClass="form-control form-control-sm" DataTextField="text" DataValueField="id">
                                                <asp:ListItem Value="">Select</asp:ListItem>
                                                <asp:ListItem Value="10">10%</asp:ListItem>
                                                <asp:ListItem Value="25">25%</asp:ListItem>
                                                <asp:ListItem Value="50">50%</asp:ListItem>
                                                <asp:ListItem Value="75">75%</asp:ListItem>
                                                <asp:ListItem Value="90">90%</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group chosenFullWidth">
                                            <label>Risk Level</label>
                                            <asp:DropDownList ID="ddlRiskLevel_Spec" runat="server" CssClass="form-control form-control-sm" DataTextField="text" DataValueField="id">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-sm-2 d-flex align-items-center">
                                        <div class="form-group mb-0">
                                            <div class="input-group input-group-sm d-flex align-items-center">
                                                <div class="input-group-prepend pr-3">Approved Drawings</div>
                                                <asp:CheckBox ID="chkApprovedDrawing_Spec" CssClass="text" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-2 d-flex align-items-center">
                                        <div class="form-group mb-0">
                                            <div class="input-group input-group-sm d-flex align-items-center">
                                                <div class="input-group-prepend pr-3">Order Received</div>
                                                <asp:CheckBox ID="chkOrderReceived_Spec" CssClass="text" runat="server" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Order Value($)</label>
                                            <asp:TextBox ID="txtOrdervalue_Spec" AutoComplete="off" runat="server"
                                                CssClass="form-control form-control-sm text-right" oninput="enforceDecimal(this, 10, 2);" onkeyup="javascript:this.value=Comma(this.value);"
                                                onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Expected Closure Date</label>
                                            <asp:TextBox ID="txtExpectedClosureDate_Spec" runat="server" CssClass="form-control form-control-sm " OnBlur="validateDate(this)" autocomplete="off" />
                                            <asp:CalendarExtender ID="txtExpectedClosureDate_Spec_Extender" runat="server" Format="MM/dd/yyyy"
                                                PopupButtonID="txtExpectedClosureDate_Spec" TargetControlID="txtExpectedClosureDate_Spec">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group">
                                            <label>Closed Date</label>
                                            <asp:TextBox ID="txtClosedDate_Spec" runat="server" CssClass="form-control form-control-sm " OnBlur="validateDate(this)" autocomplete="off" />
                                            <asp:CalendarExtender ID="txtClosedDate_Spec_Extender" runat="server" Format="MM/dd/yyyy"
                                                PopupButtonID="txtClosedDate_Spec" TargetControlID="txtClosedDate_Spec">
                                            </asp:CalendarExtender>
                                        </div>
                                    </div>

                                    <div class="col-2">
                                        <div class="form-group chosenFullWidth">
                                            <label>BDM</label>
                                            <asp:DropDownList ID="ddlBDM_Spec" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-sm-2 chosenFullWidth">
                                        <asp:Panel ID="PNumber" runat="server" Style="height: 200px; overflow: scroll; display: none;">
                                        </asp:Panel>
                                        <label>P#</label>
                                        <asp:Panel ID="PanelN" runat="server">
                                            <asp:TextBox ID="txtPNumber_Spec" placeholder="Type P #" AutoComplete="off" CssClass="form-control form-control-sm" runat="server">
                                            </asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txtPNumber_Spec_Extender" runat="server" TargetControlID="txtPNumber_Spec"
                                                CompletionInterval="1" CompletionSetCount="10" MinimumPrefixLength="1" CompletionListElementID="PNumber"
                                                ServicePath="../AutoComplete.asmx" ServiceMethod="SearchPNumberOnly" CompletionListCssClass="autocomplete" />
                                        </asp:Panel>
                                    </div>
                                </div>
                                <div class="row">
                                </div>
                                <div class="col-12">
                                    <div class="table-responsive">
                                        <asp:GridView CssClass="table mainGridTable table-sm mb-0" ID="gvSpecDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="SpecID,TimeStamp"
                                            EnableModelValidation="True" OnRowCommand="gvSpecDetails_RowCommand" OnRowDeleting="gvSpecDetails_RowDeleting"
                                            OnRowEditing="gvSpecDetails_RowEditing" EmptyDataText="No Spec exists">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Project Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<ItemStyle Width="5%" />--%>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Project Stage">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProjectStage" runat="server" Text='<%# Eval("ProjectStage") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<ItemStyle Width="25%" />--%>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Estimated Value" HeaderStyle-CssClass="text-right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEstimatedValue" runat="server" Text='<%#"$" + Eval("EstimatedValue") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Probability (%)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProbability" runat="server" Text='<%# Eval("ProbabilityPercent") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<ItemStyle Width="25%" />--%>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Modify">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" Text="Edit" CommandName="Edit"><i class="far fa-edit" title="Edit"></i></asp:LinkButton>
                                                        <asp:LinkButton runat="server" CssClass="btn btn-success btn-sm" Text="Create P#" CommandName="Create_PNumber" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'>
                                                            <i class="far fa-paper-plane" title="Create P#"></i>
                                                        </asp:LinkButton>
                                                        <asp:LinkButton CssClass="btn btn-danger btn-sm" title="Delete" runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure.?');">
                                                                <i class="far fa-times-circle"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <asp:HiddenField Value="-1" ID="hfActivityID" runat="server" />
            <asp:HiddenField Value="-1" ID="hfTimestamp_Activity" runat="server" />
            <asp:HiddenField Value="-1" ID="hfTimestamp_Participant" runat="server" />
            <asp:HiddenField Value="-1" ID="hfParticipantId" runat="server" />
            <asp:HiddenField Value="-1" ID="hfSpecId" runat="server" />
            <asp:HiddenField Value="-1" ID="hfTimestamp_Spec" runat="server" />
            <asp:HiddenField Value="-1" ID="hfFollowupId" runat="server" />
            <asp:HiddenField Value="-1" ID="hfTimestamp_Followup" runat="server" />


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
            $('#<%=ddlBDMHeaderList.ClientID%>').chosen();
            $('#<%=ddlActivityHeaderList.ClientID%>').chosen();
            $('#<%=ddlActivityType.ClientID%>').chosen();
            $('#<%=ddlBDM.ClientID%>').chosen();
            $('#<%=ddlStatus.ClientID%>').chosen();
            $('#<%=ddlParticipantType.ClientID%>').chosen();
            $('#<%=ddlContact.ClientID%>').chosen();
            $('#<%=ddlCompany.ClientID%>').chosen();
            $('#<%=ddlBDM_Spec.ClientID%>').chosen();
            $('#<%=ddlPriority_Followup.ClientID%>').chosen();
            $('#<%=ddlStatus_Followup.ClientID%>').chosen();
            $('#<%=ddlAssignedTo_Followup.ClientID%>').chosen();
            $('#<%=ddlStatus_Spec.ClientID%>').chosen();
            $('#<%=ddlParticipantStatus.ClientID%>').chosen();
            $('#<%=ddlLocationType.ClientID%>').chosen();
            $('#<%=ddlVisitMode.ClientID%>').chosen();
            $('#<%=ddlCountry_Spec.ClientID%>').chosen();
            $('#<%=ddlState_Spec.ClientID%>').chosen();
            $('#<%=ddlProjectStage_Spec.ClientID%>').chosen();
            $('#<%=ddlPriority_Spec.ClientID%>').chosen();
            $('#<%=ddlRiskLevel_Spec.ClientID%>').chosen();
            $('#<%=ddlFollowupType_Followup.ClientID%>').chosen();
            $('#<%=ddlLocation_Participant.ClientID%>').chosen();
            $('#<%=ddlMaterialUsed_Participant.ClientID%>').chosen();
            $('#<%=ddlIndustry_Spec.ClientID%>').chosen();
            $('#<%=ddlProbability_Spec.ClientID%>').chosen();
        }
    </script>
</asp:Content>
