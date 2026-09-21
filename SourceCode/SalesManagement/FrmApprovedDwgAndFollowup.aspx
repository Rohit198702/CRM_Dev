<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" EnableEventValidation="false" CodeFile="FrmApprovedDwgAndFollowup.aspx.cs" Inherits="SalesManagement_FrmApprovedDwgAndFollowup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <div class="col-12">
                <div class="row pt-3">
                    <div class="col-12">
                        <h5 class="text-uppercase">Approved Drawings and Followups</h5>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Followup By</label>
                            <asp:TextBox CssClass="form-control form-control-sm " MaxLength="250" ID="txtLoginUser" runat="server" autocomplete="off" Enabled="false" />
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Project Manager</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlProjectManager" DataTextField="text" DataValueField="id" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-2 ">
                        <div class="form-group">
                            <label>Dwg App Date From</label>
                            <asp:TextBox ID="txtFromDate" CssClass="form-control form-control-sm" autocomplete="off" runat="server">
                            </asp:TextBox>
                            <asp:CalendarExtender ID="txtFromDate_Extender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtFromDate" TargetControlID="txtFromDate">
                            </asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-sm-2 ">
                        <div class="form-group">
                            <label>Dwg App Date To</label>
                            <asp:TextBox ID="txtToDate" CssClass="form-control form-control-sm" autocomplete="off" runat="server">
                            </asp:TextBox>
                            <asp:CalendarExtender ID="txtToDate_Extender" runat="server" Format="MM/dd/yyyy"
                                PopupButtonID="txtToDate" TargetControlID="txtToDate">
                            </asp:CalendarExtender>
                        </div>
                    </div>

                    <div class="col-sm-10">
                        <div class="row">
                            <%--<label class="col-md-12">&nbsp;</label>--%>
                            <div class="col-md-6">
                                <asp:Button ID="btnShow" runat="server" CssClass="btn btn-secondary btn-sm" CausesValidation="false" Text="Search" OnClick="btnShow_Click" />
                                <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger btn-sm" Text="Clear Search" OnClick="btnClear_Click" />
                                <asp:Button ID="btnExportToExcel" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Enabled="false" Text="Export to Excel" OnClick="btnExportToExcel_Click" />
                                <asp:Button CssClass="btn btn-info btn-sm rounded" ID="btnReportRedirect" OnClick="btnReportRedirect_Click" runat="server" Text="Report" />
                            </div>
                            <div class="col-md-6 justify-content-center">
                                <strong class="text-center">
                                    <asp:Label CssClass="alert alert-success d-block py-1" ID="lblRecordsCount" runat="server" Text="Label" Visible="false"></asp:Label>
                                </strong>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col">
                <asp:GridView ID="gvApprovedDwg" runat="server" ForeColor="White" CssClass="table mainGridTable table-sm mb-0" OnRowDataBound="gvApprovedDwg_RowDataBound"
                    OnRowCommand="gvApprovedDwg_RowCommand" EnableModelValidation="True" AutoGenerateColumns="false" DataKeyNames="JobID" AllowSorting="true"
                    OnSorting="gvApprovedDwg_Sorting">
                    <Columns>
                        <asp:TemplateField HeaderText="P#" Visible="true" SortExpression="PNumber">
                            <ItemTemplate>
                                <asp:Label ID="lblPNumber" runat="server" Text='<%# Eval("PNumber") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="J#" Visible="true" SortExpression="JobID">
                            <ItemTemplate>
                                <asp:Label ID="lblJobID" runat="server" Text='<%# Eval("JobID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Project Name" SortExpression="ProjectName">
                            <ItemTemplate>
                                <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dwg App Date" SortExpression="DwgAppDate">
                            <ItemTemplate>
                                <asp:Label ID="lblDwgAppDate_Grid" runat="server" Text='<%# Eval("DwgAppDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dealer" SortExpression="Dealer">
                            <ItemTemplate>
                                <asp:Label ID="lblDealer" runat="server" Text='<%# Eval("Dealer") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Consultant" SortExpression="Consultant">
                            <ItemTemplate>
                                <asp:Label ID="lblConsultant" runat="server" Text='<%# Eval("Consultant") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dest Rep" SortExpression="DestRep">
                            <ItemTemplate>
                                <asp:Label ID="lblDestRep" runat="server" Text='<%# Eval("DestRep") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ship Date" SortExpression="ShipDate">
                            <ItemTemplate>
                                <asp:Label ID="lblShipDate" runat="server" Text='<%# Eval("ShipDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Net Eq. Price" SortExpression="NetEqPrice">
                            <ItemTemplate>
                                <asp:Label ID="lblNetEqPrice" runat="server" Text='<%# Eval("NetEqPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle CssClass="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PM" SortExpression="ProjectManager">
                            <ItemTemplate>
                                <asp:Label ID="lblProjectManager_Grid" runat="server" Text='<%# Eval("ProjectManager") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Last Date of call" SortExpression="LastCallDate">
                            <ItemTemplate>
                                <asp:Label ID="lblLastCallDate" runat="server" Text='<%# Eval("LastCallDate") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <%-- Modal --%>
            <asp:ModalPopupExtender ID="modalForJob" runat="server" TargetControlID="lnkModalButton"
                PopupControlID="panelForModal" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="panelForModal" runat="server" CssClass="ReportsModalPopup" Style="display: none;" Width="90%" Height="80%">
                <div class="position-relative h-100">
                    <asp:ImageButton CssClass="position-absolute crossCloseBtn" ID="btnClose" runat="server" ImageUrl="../images/closebtnCircle.png"
                        AlternateText="Close Popup" ToolTip="Close Popup" OnClick="btnClose_Click" />
                    <div class="overflow-auto h-100">
                        <%-- Title --%>
                        <div class="row justify-content-center col-12">
                            <div class="col-12">
                                <div class="row">
                                    <div class="col-sm-4 col-md-auto mb-3 modal-title text-center">
                                        <h5>
                                            <label class="mb-0 title-hyphen position-relative">Job ID:</label>
                                            <asp:Button CssClass="btn btn-info btn-sm rounded" ID="btnAddContactRedirect" Visible="false" OnClick="btnAddContactRedirect_Click" runat="server" Text="Add Contact" />
                                        </h5>
                                    </div>
                                    <div class="col-sm-8 col-md mb-3 chosenFullWidth ">
                                        <h5>
                                            <asp:Label ID="lblJobIDTitleInModal" runat="server"></asp:Label>
                                        </h5>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4 col-md-auto mb-3 modal-title text-center">
                                        <h5>
                                            <label class="mb-0 title-hyphen position-relative">Dwg App Date:</label>
                                        </h5>
                                    </div>
                                    <div class="col-sm-8 col-md mb-3 chosenFullWidth ">
                                        <h5>
                                            <asp:Label ID="lblDwgAppDate" runat="server"></asp:Label>
                                        </h5>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-4 col-md-auto mb-3 modal-title text-center">
                                        <h5>
                                            <label class="mb-0 title-hyphen position-relative">Project Manager:</label>
                                        </h5>
                                    </div>
                                    <div class="col-sm-8 col-md mb-3 chosenFullWidth ">
                                        <h5>
                                            <asp:Label ID="lblProjectManager" runat="server"></asp:Label>
                                        </h5>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%-- First Grid (for contacts) --%>
                        <div class="col-12">
                            <h5>Dealer Contacts</h5>
                            <div class="table-responsive" style="height: 140px; overflow-y: auto;">
                                <asp:GridView CssClass="table mainGridTable table-sm mb-0" ID="gvJobContacts" runat="server" AutoGenerateColumns="False"
                                    EnableModelValidation="True" EmptyDataText="No Contacts" DataKeyNames="ContactID,TimeStamp" ShowFooter="True"
                                    OnRowCommand="gvJobContacts_RowCommand" OnRowDeleting="gvJobContacts_RowDeleting" OnRowEditing="gvJobContacts_RowEditing"
                                    OnRowCancelingEdit="gvJobContacts_RowCancelingEdit" OnRowUpdating="gvJobContacts_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Position*">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'>
                                                </asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtTitle" Text='<%# Eval("Title") %>' MaxLength="35" AutoComplete="off" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                            </EditItemTemplate>

                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFooterTitle" MaxLength="35" AutoComplete="off" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="First Name*">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("FirstName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtFirstName" Text='<%# Eval("FirstName") %>' MaxLength="30" AutoComplete="off" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                            </EditItemTemplate>

                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFooterFirstName" MaxLength="30" AutoComplete="off" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Last Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("LastName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtLastName" Text='<%# Eval("LastName") %>' MaxLength="30" AutoComplete="off" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                            </EditItemTemplate>

                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFooterLastName" MaxLength="30" AutoComplete="off" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Contact Name" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContactName" runat="server" Text='<%# Eval("ContactName") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cell Phone">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone") %>'>
                                                </asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtPhone" onblur="phoneMask(this)" Text='<%# Eval("Phone") %>' MaxLength="25" AutoComplete="off" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                            </EditItemTemplate>

                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFooterPhone" onblur="phoneMask(this)" MaxLength="25" AutoComplete="off" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'>
                                                </asp:Label>
                                            </ItemTemplate>

                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEmail" Text='<%# Eval("Email") %> ' MaxLength="50" onchange="return validateEmail(this)" AutoComplete="off" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                            </EditItemTemplate>

                                            <FooterTemplate>
                                                <asp:TextBox ID="txtFooterEmail" onchange="return validateEmail(this)" MaxLength="50" AutoComplete="off" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Modify" ItemStyle-Width="150">
                                            <ItemTemplate>
                                                <asp:LinkButton CssClass="btn btn-info btn-sm" ID="btnEdit" runat="server" CommandName="Edit">
                                                <i class="far fa-edit" title="Edit"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton CssClass="btn btn-danger btn-sm" ID="btnDelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this Member ?');" CommandName="Delete">
                                                <i class="far fa-times-circle" title="Delete"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <%-- while edit buttons --%>
                                            <EditItemTemplate>
                                                <asp:LinkButton CssClass="btn btn-success btn-sm" ID="lnkUpdate" runat="server" CommandName="Update">
                                                <i class="far fa-save" title="Update"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton CssClass="btn btn-danger btn-sm" ID="lnkCancel" runat="server" CommandName="Cancel">
                                                <i class="fas fa-redo" title="Redo"></i>
                                                </asp:LinkButton>
                                            </EditItemTemplate>
                                            <%-- new entry fields --%>
                                            <FooterTemplate>
                                                <asp:Button CssClass="btn btn-info btn-sm rounded" ID="btnAddContact" CommandName="Insert" runat="server" TabIndex="40" Text="Add" />
                                            </FooterTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                        <%-- form to enter call details --%>
                        <div class="col-12 mt-2">
                            <div class="row">
                                <div class="col-sm-2">
                                    <div class="form-group chosenFullWidth">
                                        <label class="text-danger">Followup By*</label>
                                        <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlFollowupBy" DataTextField="text" DataValueField="id" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <label class="text-danger">Date Called*</label>
                                        <asp:TextBox CssClass="form-control form-control-sm " ID="txtDateCalled" runat="server" />
                                        <asp:CalendarExtender ID="txtDateCalled_Extender" runat="server" Format="MM/dd/yyyy"
                                            PopupButtonID="txtDateCalled" TargetControlID="txtDateCalled">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>

                                <div class="col-sm-2" style="display: none;">
                                    <div class="form-group">
                                        <label class="text-danger">Contact Name*</label>
                                        <asp:TextBox CssClass="form-control form-control-sm " MaxLength="50" ID="txtContact" runat="server" />
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <div class="form-group chosenFullWidth">
                                        <label class="">Contact Name</label>
                                        <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlContactName" DataTextField="ContactName" DataValueField="ContactID" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-sm-2">
                                    <div class="form-group chosenFullWidth">
                                        <label class="text-danger">Status*</label>
                                        <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlCallHistoryStatus" DataTextField="text" DataValueField="id" runat="server"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-sm-2 d-flex align-items-center">
                                    <div class="form-group mb-0">
                                        <div class="input-group input-group-sm d-flex align-items-center">
                                            <div class="input-group-prepend pr-3">Notify the PM</div>
                                            <asp:CheckBox ID="chkNotifyThePM" CssClass="text" runat="server" />
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-8">
                                    <div class="form-group">
                                        <label>Notes</label>
                                        <asp:TextBox CssClass="form-control form-control-sm " TextMode="MultiLine" oninput="return limitMultiLineInputLength(this, 5000)" ID="txtNotes" runat="server" />
                                    </div>
                                </div>

                                <div class="col-sm-2 pl-0">
                                    <label>&nbsp;</label>
                                    <div class="col-12">
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info btn-sm" CausesValidation="false" Text="Save" OnClientClick="return NotifyPmAdditionWarning();" />
                                        <asp:Button ID="btnSave_Hidden" runat="server" CssClass="btn btn-info btn-sm" Style="display: none;" CausesValidation="false" Text="Save" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" CausesValidation="false" Text="Cancel" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%-- Second Grid (for call history) --%>
                        <div class="col-12 mt-2">
                            <div class="table-responsive">
                                <asp:GridView CssClass="table mainGridTable table-sm mb-0" ID="gvCallHistory" runat="server" AutoGenerateColumns="False" DataKeyNames="ID,TimeStamp"
                                    OnRowDeleting="gvCallHistory_RowDeleting" OnRowEditing="gvCallHistory_RowEditing" EnableModelValidation="true" EmptyDataText="No Followup History">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date Called" ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDateCalled" runat="server" Text='<%# Eval("DateCalled") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Contact Name" ItemStyle-Width="170">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContact" runat="server" Text='<%# Eval("Contact") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Notes" ItemStyle-Width="250">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNotes" runat="server" Text='<%# Eval("Notes") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Status" ItemStyle-Width="250">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Modify" ItemStyle-Width="150">
                                            <ItemTemplate>
                                                <asp:LinkButton CssClass="btn btn-info btn-sm" ID="btnEdit" runat="server" CommandName="Edit">
                                                <i class="far fa-edit" title="Edit"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton CssClass="btn btn-danger btn-sm" ID="btnDelete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this Member ?');" CommandName="Delete">
                                                <i class="far fa-times-circle" title="Delete"></i>
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:GridView ID="gvExportToExcel" Style="display: none;" runat="server" ForeColor="White" CssClass="table mainGridTable table-sm mb-0" OnRowDataBound="gvExportToExcel_RowDataBound"
                    EnableModelValidation="True" AutoGenerateColumns="false" DataKeyNames="J#" AllowSorting="true"
                    OnSorting="gvApprovedDwg_Sorting">
                    <Columns>
                        <asp:TemplateField HeaderText="Followup By" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblFollowupBy_Export" runat="server" Text='<%# Eval("FollowupBy") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="P#" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblPNumber_Export" runat="server" Text='<%# Eval("P#") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="J#" Visible="true">
                            <ItemTemplate>
                                <asp:Label ID="lblJobId_Export" runat="server" Text='<%# Eval("J#") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Project Name">
                            <ItemTemplate>
                                <asp:Label ID="lblProjectName_Export" runat="server" Text='<%# Eval("Project Name") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="500px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dwg Approval Date">
                            <ItemTemplate>
                                <asp:Label ID="lblDwgAppDate_Export" runat="server" Text='<%# Eval("Dwg App Date") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dealer">
                            <ItemTemplate>
                                <asp:Label ID="lblDealer_Export" runat="server" Text='<%# Eval("Dealer") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="300px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Consultant">
                            <ItemTemplate>
                                <asp:Label ID="lblConsultant_Export" runat="server" Text='<%# Eval("Consultant") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="300px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dest Rep">
                            <ItemTemplate>
                                <asp:Label ID="lblDestRep_Export" runat="server" Text='<%# Eval("Dest Rep") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ship Date">
                            <ItemTemplate>
                                <asp:Label ID="lblShipDate_Export" runat="server" Text='<%# Eval("Ship Date") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Net Eq. Price">
                            <ItemTemplate>
                                <asp:Label ID="lblNetEqPrice" runat="server" Text='<%# Eval("NetEqPrice") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" Width="100px" />
                            <HeaderStyle CssClass="Right" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Days Until Ship">
                            <ItemTemplate>
                                <asp:Label ID="lblDaysUntilShip_Export" runat="server" Text='<%# Eval("Days Until Ship") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblStatus_Export" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="PM">
                            <ItemTemplate>
                                <asp:Label ID="lblProjectManager_Export" runat="server" Text='<%# Eval("Project Manager") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Followup Date">
                            <ItemTemplate>
                                <asp:Label ID="lblFollowupDate_Export" runat="server" Text='<%# Eval("Followup Date") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dealer Contact">
                            <ItemTemplate>
                                <asp:Label ID="lblDealerContact_Export" runat="server" Text='<%# Eval("Dealer Contact") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dealer Tier">
                            <ItemTemplate>
                                <asp:Label ID="lblDealerTier_Export" runat="server" Text='<%# Eval("Dealer Tier") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dealer Phone">
                            <ItemTemplate>
                                <asp:Label ID="lblDealerPhone_Export" runat="server" Text='<%# Eval("Dealer Phone") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Dealer Email">
                            <ItemTemplate>
                                <asp:Label ID="lblDealerEmail_Export" runat="server" Text='<%# Eval("Dealer Email") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="150px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date Called">
                            <ItemTemplate>
                                <asp:Label ID="lblDateCalled_Export" runat="server" Text='<%# Eval("Date Called") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Notes">
                            <ItemTemplate>
                                <asp:Label ID="lblDealerNotes_Export" runat="server" Text='<%# Eval("Notes") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="700px" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>

            <asp:LinkButton ID="lnkModalButton" runat="server"></asp:LinkButton>
            <asp:HiddenField ID="hfDetailID" runat="server" />
            <asp:HiddenField ID="hfJobIDTitleInModal" runat="server" />
            <asp:HiddenField ID="HfJObID" runat="server" Value="" />
            <asp:HiddenField ID="HfProjectName" runat="server" Value="-1" />
            <asp:HiddenField ID="hfTimeStamp" runat="server" Value="-1" />
            <asp:HiddenField ID="hfTimeStampCH" runat="server" Value="-1" />
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
            $('#<%=ddlFollowupBy.ClientID%>').chosen();
            $('#<%=ddlContactName.ClientID%>').chosen();
            $('#<%=ddlCallHistoryStatus.ClientID%>').chosen();
            $('#<%=ddlProjectManager.ClientID%>').chosen();
        }

        function NotifyPmAdditionWarning(event) {
            var modal = $find('<%= modalForJob.ClientID %>');

            var notifyPM = document.getElementById('<%= chkNotifyThePM.ClientID %>').checked;
            // If not checked, skip the confirmation and save directly.
            if (!notifyPM) {
                document.getElementById('<%= btnSave_Hidden.ClientID %>').click();
                return false;
            }

            var confirmBox = document.createElement("div");
            confirmBox.classList.add("confirm-box");

            var messageBox = document.createElement("div");
            messageBox.classList.add("message-box");
            messageBox.textContent = "Email will be sent to the project manager. Are you sure?";
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

            //document.body.appendChild(confirmBox);
            document.getElementById('<%= panelForModal.ClientID %>').appendChild(confirmBox);
            modal.show();
            return false;
        }
    </script>
</asp:Content>
