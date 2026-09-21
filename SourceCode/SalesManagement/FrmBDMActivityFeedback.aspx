<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmBDMActivityFeedback.aspx.cs" Inherits="SalesManagement_FrmBDMActivityFeedback" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="up_BDMActivityFeedback" runat="server">
        <ContentTemplate>
            <div class="col-12 pt-2 piDiv position-sticky">
                <div class="row">
                    <div class="col-12">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">BDM Activity Feedback</h4>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-2">
                        <div class="form-group">
                            <label>Quarter</label>
                            <asp:DropDownList ID="ddlQuarterHeaderList" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlQuarterHeaderList_SelectedIndexChanged">
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

                    <div class="col-6">
                        <label>&nbsp;</label>
                        <div class="row">
                            <div class="col-auto">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-sm" CausesValidation="false" Text="Save" OnClientClick="return AdditionWarning();" />
                                <asp:Button ID="btnRedirect" runat="server" CssClass="btn btn-primary btn-sm" Text="KPI Report" OnClick="btnRedirect_Click" />
                                <asp:Button ID="btnSave_Hidden" runat="server" CssClass="btn btn-success btn-sm" Style="display: none;" CausesValidation="false" Text="Save" OnClick="btnSave_Click" />
                                <%--   <asp:Button ID="btnPerformanceReview" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false" OnClick="btnPerformanceReview_Click"
                                                OnClientClick="window.document.forms[0].target='_blank';" Text="Performance Review Report" />--%>
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-12 row border-top align-items-start">
                <div class="col-12 mt-2">
                    <h5 class="text-uppercase">Feedback Information
                    </h5>
                </div>
                <div class="col-6 row">
                    <div class="col-4">
                        <div class="form-group">
                            <label class="text-danger">Quarter*</label>
                            <asp:DropDownList ID="ddlQuarter" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlQuarter_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-4">
                        <div class="form-group">
                            <label class="text-danger">BDM*</label>
                            <asp:DropDownList ID="ddlBDM" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm"
                                AutoPostBack="true" OnSelectedIndexChanged="ddlBDM_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-4">
                        <div class="form-group">
                            <label class="text-danger">Review Date*</label>
                            <asp:TextBox ID="txtReviewDate" runat="server" CssClass="form-control form-control-sm " autocomplete="off" OnBlur="validateDate(this)" />
                            <asp:CalendarExtender ID="txtReviewDate_Extender" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtReviewDate" TargetControlID="txtReviewDate">
                            </asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-4">
                        <div class="form-group">
                            <label class="text-danger">Status*</label>
                            <asp:DropDownList ID="ddlStatus" runat="server" DataTextField="text" DataValueField="id" CssClass="form-control form-control-sm">
                                <asp:ListItem Value="">Select</asp:ListItem>
                                <asp:ListItem Value="ontrack">On Track</asp:ListItem>
                                <asp:ListItem Value="atrisk">At Risk</asp:ListItem>
                                <asp:ListItem Value="offtrack">Off Track</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="col-3 row">
                    <div class="col-12">
                        <div class="form-group">
                            <label class="text-danger">Accomplishment 1*</label>
                            <asp:TextBox ID="txtAccomplishment_1" runat="server" TextMode="MultiLine" oninput="return limitMultiLineInputLength(this, 200)" CssClass="form-control form-control-sm" />
                        </div>
                    </div>

                    <div class="col-12">
                        <div class="form-group">
                            <label class="text-danger">Accomplishment 2*</label>
                            <asp:TextBox ID="txtAccomplishment_2" runat="server" TextMode="MultiLine" oninput="return limitMultiLineInputLength(this, 200)" CssClass="form-control form-control-sm" />
                        </div>
                    </div>

                    <div class="col-12">
                        <div class="form-group">
                            <label class="text-danger">Accomplishment 3*</label>
                            <asp:TextBox ID="txtAccomplishment_3" runat="server" TextMode="MultiLine" oninput="return limitMultiLineInputLength(this, 200)" CssClass="form-control form-control-sm" />
                        </div>
                    </div>
                </div>

                <div class="col-3 row">
                    <div class="col-12">
                        <div class="form-group">
                            <label class="text-danger">Gap 1*</label>
                            <asp:TextBox ID="txtGap_1" runat="server" TextMode="MultiLine" oninput="return limitMultiLineInputLength(this, 200)" CssClass="form-control form-control-sm" />
                        </div>
                    </div>

                    <div class="col-12">
                        <div class="form-group">
                            <label class="text-danger">Gap 2*</label>
                            <asp:TextBox ID="txtGap_2" runat="server" TextMode="MultiLine" oninput="return limitMultiLineInputLength(this, 200)" CssClass="form-control form-control-sm" />
                        </div>
                    </div>

                    <div class="col-12">
                        <div class="form-group">
                            <label>Corrective Action</label>
                            <asp:TextBox ID="txtCorrectiveAction" runat="server" TextMode="MultiLine" oninput="return limitMultiLineInputLength(this, 200)" CssClass="form-control form-control-sm" />
                        </div>
                    </div>
                </div>
            </div>

            <asp:HiddenField Value="-1" ID="hfTimestamp" runat="server" />
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
            $('#<%=ddlQuarterHeaderList.ClientID%>').chosen();
            $('#<%=ddlQuarter.ClientID%>').chosen();
            $('#<%=ddlBDM.ClientID%>').chosen();
            $('#<%=ddlStatus.ClientID%>').chosen();
        }


        function AdditionWarning(event) {
            var flag = document.getElementById('<%= btnSave.ClientID %>').value.toLowerCase() == "update";

            if (!flag) {
                document.getElementById('<%= btnSave_Hidden.ClientID %>').click();
                return false;
            }

            var confirmBox = document.createElement("div");
            confirmBox.classList.add("confirm-box");

            var messageBox = document.createElement("div");
            messageBox.classList.add("message-box");
            messageBox.textContent = "Review already exists. Changes will be overridden. Are you sure?";
            confirmBox.appendChild(messageBox);

            var buttonBox = document.createElement("div");
            buttonBox.classList.add("button-box");
            messageBox.appendChild(buttonBox);

            var yesButton = document.createElement("button");
            yesButton.classList.add("yes-button");

            yesButton.textContent = document.getElementById('<%= btnSave.ClientID %>').value;
            buttonBox.appendChild(yesButton);
            yesButton.addEventListener("click", YesButtonClick);

            var noButton = document.createElement("button");
            noButton.classList.add("no-button");
            noButton.textContent = "Cancel";
            buttonBox.appendChild(noButton);
            noButton.addEventListener("click", NoButtonClick);

            function removeConfirmBox() {
                //document.body.removeChild(confirmBox);
                if (confirmBox.parentNode) {
                    confirmBox.parentNode.removeChild(confirmBox);
                }
            }

            function YesButtonClick() {
                removeConfirmBox();
                document.getElementById('<%= btnSave_Hidden.ClientID %>').click();
                return true;
            }

            function NoButtonClick() {
                removeConfirmBox();
            }

            document.body.appendChild(confirmBox);
            return false;
        }
    </script>
</asp:Content>
