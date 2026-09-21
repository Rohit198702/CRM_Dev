<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="FrmSalesActivity_V1.aspx.cs" Inherits="SalesManagement_FrmSalesActivity_V1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel_SalesActivity" runat="server">
        <ContentTemplate>
            <div class="col-12 pt-2 piDiv position-sticky">
                <div class="row">
                    <div class="col-12">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Sales Activity</h4>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="row">
                            <div class="col-sm-3">
                                <div class="row">
                                    <div class="col-sm-auto">
                                        <label class="mb-0">Lookup Activity</label>
                                    </div>
                                    <div class="col-sm chosenFullWidth">
                                        <asp:DropDownList CssClass="form-control form-control-sm" ID="ddllookupActivityType" runat="server" DataTextField="ActivityName" DataValueField="ActivityTypeID" AutoPostBack="true" OnSelectedIndexChanged="ddllookupActivityType_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="col-sm-12 mt-3">
                        <div class="row">
                            <div class="col-auto">
                                <asp:Button ID="btnReset" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btnReset_Click" Text="Cancel" Visible="false" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12" id="divMain" runat="server" visible="false">
                <div class="row">
                    <div class="col-2">
                        <div class="form-group">
                            <label class="text-danger">Ref #*</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtRefNumber" runat="server" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label class="text-danger">Activity Date*</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtActivityDate" runat="server" OnBlur="validateDate(this)"></asp:TextBox>
                            <asp:CalendarExtender ID="txtActivityDate_Extender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtActivityDate" TargetControlID="txtActivityDate">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label class="text-danger">Activity Type*</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlActivityType" runat="server" DataValueField="ActivityTypeID"
                                DataTextField="ActivityName">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-5">
                        <div class="row">
                            <div class="col-auto">
                                <label class="col-12">&nbsp;</label>
                                <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-primary btn-sm" Text="Generate New Ref #" OnClick="btnAdd_Click" Enabled="false" />


                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <div class="col-12 border-top border-bottom" id="divStakeHolder" runat="server" visible="false">
                <div class="row">
                    <div class="col-12">
                        <h4 class="text-uppercase boldtext">
                            <asp:Label ID="lblActivityTypeHeading" runat="server"></asp:Label></h4>
                    </div>


                    <div class="col-3">
                        <div class="form-group">
                            <label class="text-danger" id="lblStakeHolderHeading" runat="server"></label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlStakeHolder" runat="server" DataValueField="id"
                                DataTextField="text" AutoPostBack="true" OnSelectedIndexChanged="ddlStakeHolder_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Tier</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlStakeHolderTier" runat="server" Enabled="false" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Estimated Value</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtEstimatedValue" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="form-group">
                            <label>Competitor</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlCompetitor" runat="server" DataValueField="Conveyorid" DataTextField="Conveyorname"></asp:DropDownList>
                        </div>
                    </div>
                </div>


            </div>
            <div class="col-12 border-top border-bottom" id="divCommonContent" runat="server" visible="false">
                <div class="row">
                    <div class="col-2">
                        <div class="form-group">
                            <label class="text-danger">Date*</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtDate" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="txtDate_Extender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtDate" TargetControlID="txtDate">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label class="text-danger">Task(Reason of Contact)*</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtTask" TextMode="MultiLine" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label class="text-danger">Type of Contact*</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlTypeofContact" runat="server" DataValueField="typeofcontactID" DataTextField="TypeofContact"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group chosenFullWidth">
                            <label>Proposal Number</label>
                            <div class="input-group input-group-sm d-flex align-items-center flex-nowrap">
                                <asp:Panel ID="PNumber" runat="server" CssClass="form-control form-control-sm" Style="height: 200px; overflow: scroll; display: none;"></asp:Panel>
                                <div class="col-sm chosenFullWidth pl-0">
                                    <asp:Panel ID="Panel_PNumber" runat="server" Style="height: 200px; overflow: scroll; display: none;">
                                    </asp:Panel>
                                    <asp:Panel ID="Panel_PNumberAutoComplete" runat="server">
                                        <asp:TextBox ID="txtPNumber" AutoComplete="off" placeholder="Type PNumber #" CssClass="form-control form-control-sm" runat="server" OnBlur="return ClickEvent(event)" onkeypress="return EnterEvent(event)">
                                        </asp:TextBox>
                                        <asp:AutoCompleteExtender ID="PNumber_AutoComplete" runat="server" TargetControlID="txtPNumber"
                                            CompletionInterval="1" CompletionSetCount="10" MinimumPrefixLength="1" CompletionListElementID="Panel_PNumber"
                                            ServicePath="../AutoComplete.asmx" ServiceMethod="SearchPNumberOnly" CompletionListCssClass="autocomplete" />
                                        <asp:Button ID="SearchPNumberButton" runat="server" Text="Submit" Style="display: none" OnClick="SearchPNumberButton_Click" />
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group chosenFullWidth">
                            <label>Job #</label>
                            <div class="input-group input-group-sm d-flex align-items-center flex-nowrap">
                                <asp:Panel ID="JobNumber" runat="server" CssClass="form-control form-control-sm" Style="height: 200px; overflow: scroll; display: none;"></asp:Panel>
                                <div class="col-sm chosenFullWidth pl-0">
                                    <asp:Panel ID="Panel_JobNumber" runat="server" Style="height: 200px; overflow: scroll; display: none;">
                                    </asp:Panel>
                                    <asp:Panel ID="Panel_JobNumberAutoComplete" runat="server">
                                        <asp:TextBox ID="txtJobID" AutoComplete="off" placeholder="Type Job #" CssClass="form-control form-control-sm" runat="server" OnBlur="return ClickEventJobNo(event)" onkeypress="return EnterEventJobNo(event)">
                                        </asp:TextBox>
                                        <asp:AutoCompleteExtender ID="JobNo_AutoComplete" runat="server" TargetControlID="txtJobID"
                                            CompletionInterval="1" CompletionSetCount="10" MinimumPrefixLength="1" CompletionListElementID="Panel_JobNumber"
                                            ServicePath="../AutoComplete.asmx" ServiceMethod="SearchJobNumberOnly" CompletionListCssClass="autocomplete" />
                                        <asp:Button ID="SearchJobNoButton" runat="server" Text="Submit" Style="display: none" OnClick="SearchJobNoButton_Click" />
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Project Name</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtProjectName" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Contact Person</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlResponsiblePerson" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label>Action Required</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtDeadline" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="form-group">
                            <label>Regional Industry Updates</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtRegIndustryUpdates" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label class="text-danger">Status*</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlStatus" runat="server" DataValueField="id" DataTextField="text"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Next Followup Date</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtNextFollowupDate" runat="server" OnBlur="validateDate(this)"></asp:TextBox>
                            <asp:CalendarExtender ID="txtNextFollowupDate_CalExtender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtNextFollowupDate" TargetControlID="txtNextFollowupDate">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                </div>
            </div>

            </div>
            <div class="col-12 border-top border-bottom" id="divCESession" runat="server" visible="false">
                <div class="row">
                    <div class="col-12">
                        <h5 class="text-uppercase boldtext">
                            <asp:Label ID="lblCESessionHeading" runat="server"></asp:Label></h5>
                    </div>
                    <div class="col-12">
                        <h6 class="text-uppercase boldtext">Basic Information</h6>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label class="text-danger">Session #*</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtSessionNumber" runat="server" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label class="text-danger">Session Name*</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtSessionName" runat="server" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label class="text-danger">Session Type*</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlSessionType" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label class="text-danger">Date</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtSessionDate" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="txtSessionDate_Extender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtSessionDate" TargetControlID="txtSessionDate"></asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Duration</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtDuration" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <h6 class="text-uppercase boldtext">Participants</h6>
                    </div>
                    <div class="col-3">
                        <div class="form-group">
                            <label class="text-danger">Consultant*</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlSessionConsultant" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="form-group">
                            <label>Company</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtCompany" runat="server" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="form-group">
                            <label>No. of Attendees</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtNoofAttendees" runat="server" MaxLength="10"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="form-group">
                            <label class="text-danger">Target Tier*</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlTargetTier" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <h6 class="text-uppercase boldtext">Execution Plan</h6>
                    </div>
                    <div class="col-3">
                        <div class="form-group">
                            <label>BDM</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlBDM" runat="server" DataTextField="text" DataValueField="id" Enabled="false"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Location/Platform</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlLocPlatform" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Materials Used</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlMaterialsUsed" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <h6 class="text-uppercase boldtext">Outcome Tracking</h6>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group srRadiosBtns">
                            <label>Follow-up Required</label>
                            <asp:RadioButtonList ID="rdbFollowuprequired" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                <asp:ListItem Value="2">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-4">
                        <div class="form-group">
                            <label>Followup Description</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtFollowupDescription" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="form-group">
                            <label>Level of Interest</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlLevelofInterest" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group srRadiosBtns">
                            <label>Potential Project Identified</label>
                            <asp:RadioButtonList ID="rdbPotentialProject" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                <asp:ListItem Value="2">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12 border-top border-bottom" id="divSpecProtection" runat="server" visible="false">
                <div class="row">
                    <div class="col-12">
                        <h5 class="text-uppercase boldtext">Spec Protection & Logging</h5>
                    </div>
                    <div class="col-12">
                        <h6 class="text-uppercase boldtext">Project Information</h6>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Project #</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtProjectNum" runat="server" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Project Name</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtSpecProjectName" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Project Type</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlProjectType" runat="server" DataValueField="id" DataTextField="text"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Project Stage</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlProjectStage" runat="server" DataValueField="id" DataTextField="text"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <h6 class="text-uppercase boldtext">Source Tracking</h6>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group srRadiosBtns">
                            <label>CE Session</label>
                            <asp:RadioButtonList ID="rdbCESession" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                <asp:ListItem Value="2">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group srRadiosBtns">
                            <label>Consultant Visit</label>
                            <asp:RadioButtonList ID="rdbConsultantVisit" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                <asp:ListItem Value="2">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="form-group">
                            <label>Consultant Name</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtConsultantName" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-3">
                        <div class="form-group">
                            <label>Dealer Name</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtDealerName" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <h6 class="text-uppercase boldtext">Ownership</h6>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>BDM Ownership</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlBDMOwnership" runat="server">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Owner 1</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Region</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlRegion" runat="server">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="1">Region 1</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <h6 class="text-uppercase boldtext">Spec Protection Details</h6>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Spec Status</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlSpecStatus" runat="server" DataValueField="id" DataTextField="text">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Date Logged</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtDateLogged" runat="server" OnBlur="validateDate(this)"></asp:TextBox>
                            <asp:CalendarExtender ID="txtDateLogged_Extender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtDateLogged" TargetControlID="txtDateLogged">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group srRadiosBtns">
                            <label>Logged In CRM</label>
                            <asp:RadioButtonList ID="rdbLoggedInCRM" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                <asp:ListItem Value="2">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <h6 class="text-uppercase boldtext">Follow up Date</h6>
                    </div>

                    <div class="col-2">
                        <div class="form-group">
                            <label>Last Follow-up Date</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtLastFollowupDate" runat="server" OnBlur="validateDate(this)"></asp:TextBox>
                            <asp:CalendarExtender ID="txtLastFollowupDate_Extender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtLastFollowupDate" TargetControlID="txtLastFollowupDate">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Next Action</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtNextAction" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Next Action Date</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtNextActionDate" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="txtNextActionDate_Extender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtNextActionDate" TargetControlID="txtNextActionDate">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <h6 class="text-uppercase boldtext">Commercial Data</h6>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Estimated Value</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtSpecEstimatedValue" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Competitor</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtSpecCompetitor" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Probability (%)</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtProbabilityPercentage" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <h6 class="text-uppercase boldtext">Outcome Tracking</h6>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group srRadiosBtns">
                            <label>Approved Drawings</label>
                            <asp:RadioButtonList ID="rdbApprovedDrawings" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                <asp:ListItem Value="2">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group srRadiosBtns">
                            <label>Order Received</label>
                            <asp:RadioButtonList ID="rdbOrderReceived" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                <asp:ListItem Value="2">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Date Closed</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtDateClosed" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="txtDateClosed_Extender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtDateClosed" TargetControlID="txtDateClosed">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-12" id="DivCommonReps" runat="server" visible="false">
                <div class="row">
                    <div class="col-2">
                        <div class="form-group">
                            <label>Consultant Rep</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlConsultantRep" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Origination Rep</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlOriRep" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Destination Rep</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlDestRep" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-2">
                        <div class="form-group">
                            <label>Project Manager</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlProjectManager" runat="server" DataTextField="text" DataValueField="id"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-sm-2 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label style="display: block;">Notify With Email</label>
                            <asp:CheckBox ID="chkNotifywithemail" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="table-responsive" id="divShowButtons" runat="server" visible="false">
                <div class="col-sm-12">
                    <div class="form-group">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-sm" CausesValidation="false" Text="Save" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" CausesValidation="false" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />

                    </div>
                </div>
            </div>
            <div class="col-12 border-top" id="divgvStakeHolder" runat="server" visible="false">
                <div class="row">
                    <div class="col-12">
                        <h5 class="text-uppercase">Activity Details</h5>
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="gvActivityDetail" runat="server" CssClass="table mainGridTable table-sm mb-2"
                        AutoGenerateColumns="False"
                        OnRowCommand="gvActivityDetail_RowCommand" OnRowDataBound="gvActivityDetail_RowDataBound" DataKeyNames="">
                        <Columns>
                            <asp:TemplateField HeaderText="Ref Number" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblRefNumber" runat="server" Text='<%# Eval("RefNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activity Date" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityDate" runat="server" Text='<%# Eval("ActivityDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activity Type" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityType" runat="server" Text='<%# Eval("ActivityTypeID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Consultant/Dealer" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblStakeHolderName" runat="server" Text='<%# Eval("Stakeholder") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 1 --%>
                            <asp:TemplateField HeaderText="Tier" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblTier" runat="server" Text='<%# Eval("Tier") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 2 --%>
                            <asp:TemplateField HeaderText="Estimated Value" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblEstimatedValue" runat="server" Text='<%# Eval("EstimatedValue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 3 --%>
                            <asp:TemplateField HeaderText="Competitor" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompetetor" runat="server" Text='<%# Eval("Competetor") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 4 --%>
                            <asp:TemplateField HeaderText="Date" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDate" runat="server" Text='<%# Eval("StakeHolderVisitDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 5 --%>
                            <asp:TemplateField HeaderText="Task" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblTask" runat="server" Text='<%# Eval("Task") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 6 --%>
                            <asp:TemplateField HeaderText="Type of Contact" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblTypeofContact" runat="server" Text='<%# Eval("TypeofContact") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 7 --%>
                            <asp:TemplateField HeaderText="P#" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblPNumber" runat="server" Text='<%# Eval("PNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 8 --%>
                            <asp:TemplateField HeaderText="J#" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblJNumber" runat="server" Text='<%# Eval("JNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 9 --%>
                            <asp:TemplateField HeaderText="Project Name" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 10 --%>
                            <asp:TemplateField HeaderText="ContactPerson" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblContactPerson" runat="server" Text='<%# Eval("ContactPerson") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 11 --%>
                            <asp:TemplateField HeaderText="ActionRequired" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblActionRequired" runat="server" Text='<%# Eval("ActionRequired") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 12 --%>
                            <asp:TemplateField HeaderText="RegionalIndustryUpdates" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblRegionalIndustryUpdates" runat="server" Text='<%# Eval("RegionalIndustryUpdates") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 13 --%>
                            <asp:TemplateField HeaderText="Status" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblStakeHolderStatus" runat="server" Text='<%# Eval("StakeHolderStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 14 --%>
                            <asp:TemplateField HeaderText="Next Follow Up Date" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblStakeNextFollwupdate" runat="server" Text='<%# Eval("NextFollowUpDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <%-- 13 --%>
                            <%-- CE Session Block Start--%>
                            <asp:TemplateField HeaderText="Session Number" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSessionNumber" runat="server" Text='<%# Eval("SessionNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--14  --%>
                            <asp:TemplateField HeaderText="Session Name" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSessionName" runat="server" Text='<%# Eval("SessionName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 15 --%>
                            <asp:TemplateField HeaderText="Session Type" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSessionType" runat="server" Text='<%# Eval("SessionType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 16 --%>
                            <asp:TemplateField HeaderText="Date" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSessionDate" runat="server" Text='<%# Eval("SessionDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 17 --%>
                            <asp:TemplateField HeaderText="Duration" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDuration" runat="server" Text='<%# Eval("Duration") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 18 --%>
                            <asp:TemplateField HeaderText="Consultant" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblConsultant" runat="server" Text='<%# Eval("Consultant") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--19  --%>
                            <asp:TemplateField HeaderText="Company" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompany" runat="server" Text='<%# Eval("Company") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--20  --%>
                            <asp:TemplateField HeaderText="No of Attendees" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblNoofAttendees" runat="server" Text='<%# Eval("NoofAttendees") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--21  --%>
                            <asp:TemplateField HeaderText="Target Tier" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblTargetTier" runat="server" Text='<%# Eval("TargetTier") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 22 --%>
                            <asp:TemplateField HeaderText="BDM" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblBDM" runat="server" Text='<%# Eval("BDM") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 23 --%>
                            <asp:TemplateField HeaderText="Location/Platform" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblLocationPlatform" runat="server" Text='<%# Eval("LocationPlatform") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 24 --%>
                            <asp:TemplateField HeaderText="Materials Used" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblMaterialsUsed" runat="server" Text='<%# Eval("MaterialsUsed") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 25 --%>
                            <asp:TemplateField HeaderText="Follow-up Required" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblFollowuprequired" runat="server" Text='<%# Eval("Followuprequired") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 26 --%>
                            <asp:TemplateField HeaderText="Follow-up-description" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblFollowupdescription" runat="server" Text='<%# Eval("Followupdescription") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--27  --%>
                            <asp:TemplateField HeaderText="Level of Interest" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbllevelofinterest" runat="server" Text='<%# Eval("levelofinterest") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--28  --%>
                            <asp:TemplateField HeaderText="Potential Project Identified" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblpotentialprojectidentified" runat="server" Text='<%# Eval("potentialprojectidentified") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- CE Session  Block End --%>

                            <%-- Spec Protection Block Start --%>
                            <%-- 29 --%>
                            <asp:TemplateField HeaderText="Project #" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecProjectNumber" runat="server" Text='<%# Eval("SpecProjectNumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--30  --%>
                            <asp:TemplateField HeaderText="Project Name" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecProjectName" runat="server" Text='<%# Eval("SpecProjectName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--31  --%>
                            <asp:TemplateField HeaderText="Project Type" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecProjectType" runat="server" Text='<%# Eval("SpecProjectType") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 32 --%>
                            <asp:TemplateField HeaderText="Project Stage" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecProjectStage" runat="server" Text='<%# Eval("SpecProjectStage") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 33 --%>
                            <asp:TemplateField HeaderText="CE Session" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecCESession" runat="server" Text='<%# Eval("SpecCESession") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--34  --%>
                            <asp:TemplateField HeaderText="Consultant Visit" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecConsultantVisit" runat="server" Text='<%# Eval("SpecConsultantVisit") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 35 --%>
                            <asp:TemplateField HeaderText="Consultant Name" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecConsultantName" runat="server" Text='<%# Eval("SpecConsultantName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 36 --%>
                            <asp:TemplateField HeaderText="Dealer Name" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecDealerName" runat="server" Text='<%# Eval("SpecDealerName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 37 --%>
                            <asp:TemplateField HeaderText="BDM Ownership" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecBDMOwnership" runat="server" Text='<%# Eval("SpecBDMOwnership") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 38 --%>
                            <asp:TemplateField HeaderText="Spec Region" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecRegion" runat="server" Text='<%# Eval("SpecRegion") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--39  --%>
                            <asp:TemplateField HeaderText="Spec Status" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecStatus" runat="server" Text='<%# Eval("SpecStatus") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--40  --%>
                            <asp:TemplateField HeaderText="Date Logged" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecDateLogged" runat="server" Text='<%# Eval("SpecDateLogged") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 41 --%>
                            <asp:TemplateField HeaderText="Logged In CRM" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecLoggedInCRM" runat="server" Text='<%# Eval("SpecLoggedInCRM") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 42 --%>
                            <asp:TemplateField HeaderText="Last Follow-up Date" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecLastFollowupdate" runat="server" Text='<%# Eval("SpecLastFollowupdate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 43 --%>
                            <asp:TemplateField HeaderText="Next Action" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecNextAction" runat="server" Text='<%# Eval("SpecNextAction") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--  44--%>
                            <asp:TemplateField HeaderText="Next Action Date" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecNextActionDate" runat="server" Text='<%# Eval("SpecNextActionDate") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 45 --%>
                            <asp:TemplateField HeaderText="Estimated Value" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecEstimatedValue" runat="server" Text='<%# Eval("SpecEstimatedValue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 46 --%>
                            <asp:TemplateField HeaderText="Competitor" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecCompetitor" runat="server" Text='<%# Eval("SpecCompetitor") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 47 --%>
                            <asp:TemplateField HeaderText="Probability (%)" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecProbPerc" runat="server" Text='<%# Eval("SpecProbPerc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 48 --%>
                            <asp:TemplateField HeaderText="Approved Drwaings" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecAppDwgs" runat="server" Text='<%# Eval("SpecAppDwgs") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 49 --%>
                            <asp:TemplateField HeaderText="Order Received" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecOrderReceived" runat="server" Text='<%# Eval("SpecOrderReceived") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- 50 --%>
                            <asp:TemplateField HeaderText="Date Closed" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSpecDateClosed" runat="server" Text='<%# Eval("SpecDateClosed") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Consultant Rep" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblConsultantRep" runat="server" Text='<%# Eval("ConsultantRep") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Destination Rep" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblDestRep" runat="server" Text='<%# Eval("DestinationRep") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Origination Rep" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrigRep" runat="server" Text='<%# Eval("OriginationRep") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--51  --%>
                            <%-- Spec Protection Block END --%>
                            <asp:TemplateField HeaderText="Modify" ItemStyle-Width="15%">
                                <ItemTemplate>

                                    <asp:LinkButton ID="btnEdit"
                                        runat="server"
                                        CssClass="btn btn-outline-primary btn-sm"
                                        Text="Edit Activity"
                                        CommandName="EditActivity" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' />
                                    <br />


                                    <asp:LinkButton ID="btnFollowup"
                                        runat="server"
                                        CssClass="btn btn-outline-secondary btn-sm"
                                        Text="View/Add Followups"
                                        CommandName="Followup" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' />
                                    <br />


                                    <asp:LinkButton ID="btnDelete"
                                        runat="server"
                                        CssClass="btn btn-danger btn-sm"
                                        Text="Delete Activity"
                                        CommandName="DeleteActivity" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'
                                        OnClientClick="return confirm('Are you sure?');" />

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <asp:LinkButton ID="btnFollowup_Modal" runat="server"></asp:LinkButton>
            <asp:ModalPopupExtender ID="Modal_Followup" runat="server" TargetControlID="btnFollowup_Modal" BehaviorID="mpeFollowup"
                PopupControlID="Panel_Followup" BackgroundCssClass="modalBackground" CancelControlID="btnCloseFollowup_Modal">
            </asp:ModalPopupExtender>
            <asp:Panel ID="Panel_Followup" runat="server" CssClass="ReportsModalPopup" Style="display: none" Width="80%" Height="80%">
                <div class="position-relative h-100">
                    <asp:ImageButton CssClass="position-absolute crossCloseBtn" ID="btnCloseFollowup_Modal" runat="server" ImageUrl="../images/closebtnCircle.png"
                        AlternateText="Close Popup" ToolTip="Close Popup" CausesValidation="false" OnClientClick="$find('mpeFollowup').hide(); return false;" />
                    <div class="overflow-auto h-100">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-12">
                                    <div class="form-group">
                                        <h5 class="text-uppercase ">Activity:-
                                        <asp:Label ID="lblActivity" runat="server"></asp:Label>
                                        </h5>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-2">
                                    <div class="form-group chosenFullWidth">
                                        <label class="text-danger">Status*</label>
                                        <asp:DropDownList ID="ddlActivityHistoryFollowUpStatus" CssClass="form-control form-control-sm" autocomplete="off" runat="server">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Value="1">New</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label class="text-danger">Follow-up Date</label>
                                        <asp:TextBox ID="txtActivityHistoryFollowUpDate" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)">
                                        </asp:TextBox>
                                        <asp:CalendarExtender ID="txtActivityHistoryFollowUpDate_Ext" runat="server" Format="MM/dd/yyyy"
                                            PopupButtonID="txtActivityHistoryFollowUpDate" TargetControlID="txtActivityHistoryFollowUpDate">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label class="text-danger">Next Action Date</label>
                                        <asp:TextBox ID="txtActivityHistoryNextActionDate" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)">
                                        </asp:TextBox>
                                        <asp:CalendarExtender ID="txtActivityHistoryNextActionDate_Ext" runat="server" Format="MM/dd/yyyy"
                                            PopupButtonID="txtActivityHistoryNextActionDate" TargetControlID="txtActivityHistoryNextActionDate">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-2">
                                    <div class="form-group">
                                        <label class="text-danger">Owner</label>
                                        <asp:TextBox ID="txtActivityHistoryOwner" CssClass="form-control form-control-sm" MaxLength="50" autocomplete="off" runat="server">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                                    <div class="form-group srRadiosBtns">
                                        <label>CRM Updated</label>
                                        <asp:RadioButtonList ID="rdbCRMUpdated" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                            <asp:ListItem Value="2">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                                    <div class="form-group srRadiosBtns">
                                        <label>Teams Posted</label>
                                        <asp:RadioButtonList ID="rdbTeamsPosted" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                            <asp:ListItem Value="2">No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnAddActivityHistory" runat="server" CssClass="btn btn-primary btn-sm" Text="Add" OnClick="btnAddActivityHistory_Click" Enabled="false" />
                                        <asp:Button ID="btnCancelActivityHistory" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancelActivityHistory_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-12 border-top" id="divFollowupSection" runat="server">
                                <div class="row">
                                    <%--<div class="col-12">--%>
                                    <h5 class="text-uppercase">Activity Details</h5>
                                    <%--</div>--%>

                                    <%--<div class="col-12">--%>
                                    <div class="table-responsive">
                                        <asp:GridView CssClass="table mainGridTable table-sm mb-0" ID="gvFollowups" runat="server"
                                            AutoGenerateColumns="False"
                                            EnableModelValidation="True" OnRowCommand="gvFollowups_RowCommand" OnRowDeleting="gvFollowups_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActHisStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Last Follow-up Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActHisFollowupdate" runat="server" Text='<%# Eval("LastFollowupDate") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Next Action">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActHisNextAction" runat="server" Text='<%# Eval("NextAction") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Owner">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActHisOwner" runat="server" Text='<%# Eval("Owner") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CRM Updated">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActHisCRMUpdated" runat="server" Text='<%# Eval("CRMUpdated") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Teams Posted">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActHisTeamsUpdated" runat="server" Text='<%# Eval("TeamsPosted") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Modify">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" CssClass="btn btn-outline-primary btn-sm" Text="Edit" CommandName="EditFollowup" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>'>Edit</asp:LinkButton>
                                                        <asp:LinkButton CssClass="btn btn-danger btn-sm" title="Delete" runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure.?');">
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="15%" />
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
            $('#<%=ddlActivityType.ClientID%>').chosen();
            $('#<%=ddlStakeHolder.ClientID%>').chosen();
            $('#<%=ddlCompetitor.ClientID%>').chosen();
            $('#<%=ddlTypeofContact.ClientID%>').chosen();
            $('#<%=ddlResponsiblePerson.ClientID%>').chosen();
            $('#<%=ddlStatus.ClientID%>').chosen();
            $('#<%=ddlSessionConsultant.ClientID%>').chosen();
            $('#<%=ddlTargetTier.ClientID%>').chosen();
            $('#<%=ddlSessionType.ClientID%>').chosen();
            $('#<%=ddlStakeHolderTier.ClientID%>').chosen();
            $('#<%=ddlLocPlatform.ClientID%>').chosen();
            $('#<%=ddlStakeHolderTier.ClientID%>').chosen();
            $('#<%=ddlMaterialsUsed.ClientID%>').chosen();
            $('#<%=ddlLevelofInterest.ClientID%>').chosen();
            $('#<%=ddlProjectType.ClientID%>').chosen();
            $('#<%=ddlProjectStage.ClientID%>').chosen();
            $('#<%=ddlBDMOwnership.ClientID%>').chosen();
            $('#<%=ddlRegion.ClientID%>').chosen();
            $('#<%=ddlSpecStatus.ClientID%>').chosen();
            $('#<%=ddlActivityHistoryFollowUpStatus.ClientID%>').chosen();
            $('#<%=ddlConsultantRep.ClientID%>').chosen();
            $('#<%=ddlDestRep.ClientID%>').chosen();
            $('#<%=ddlOriRep.ClientID%>').chosen();
            $('#<%=ddlProjectManager.ClientID%>').chosen();
            $('#<%=ddllookupActivityType.ClientID%>').chosen();
        }

        function ClickEvent(e) {
            __doPostBack('<%=SearchPNumberButton.UniqueID%>', "");
        }

        function EnterEvent(e) {
            if (e.keyCode == 13) {
                __doPostBack('<%=SearchPNumberButton.UniqueID%>', "");
            }
        }

        function ClickEventJobNo(e) {
            __doPostBack('<%=SearchJobNoButton.UniqueID%>', "");
        }

        function EnterEventJobNo(e) {
            if (e.keyCode == 13) {
                __doPostBack('<%=SearchJobNoButton.UniqueID%>', "");
            }
        }

    </script>
</asp:Content>

