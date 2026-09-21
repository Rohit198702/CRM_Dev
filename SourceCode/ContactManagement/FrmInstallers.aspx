<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmInstallers.aspx.cs" Inherits="ContactManagement_FrmInstallers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <div class="col-12 pt-2 piDiv position-sticky">
                <div class="row">
                    <div class="col-12">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Installers</h4>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-6">
                        <div class="row">
                            <div class="col-sm-2">
                                <label class="mb-0">Installer</label>
                            </div>
                            <div class="col-sm-9 col-md mb-3 chosenFullWidth">
                                <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlInstallerLookupList" runat="server" DataTextField="text" DataValueField="id"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlInstallerLookupList_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm col-md col-lg col-xl-auto">
                        <div class="row">
                            <div class="col-auto">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-sm" Text="Save" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-12">
                <div class="row pt-3">
                    <div class="col-12">
                        <h5 class="text-uppercase">Installer Info</h5>
                    </div>
                    <div class="col-sm-3">
                        <div class="form-group">
                            <label class="text-danger">Company Name*</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtCompanyName" AutoComplete="off" runat="server" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Address</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtAddress" AutoComplete="off" runat="server"
                                oninput="return limitMultiLineInputLength(this, 250)" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Country</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlCountry" DataTextField="text" DataValueField="id" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>State</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlState" DataTextField="text" DataValueField="id" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>City</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtCity" AutoComplete="off" runat="server" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Zip Code</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtZipCode" AutoComplete="off" runat="server" MaxLength="20"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <label>Bank Details</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtBankDetails" AutoComplete="off" runat="server"
                                oninput="return limitMultiLineInputLength(this, 250)" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-3">
                        <div class="form-group">
                            <label class="text-danger">Status*</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlStatus" DataTextField="text" DataValueField="id" runat="server">
                                <asp:ListItem Value="1">Active</asp:ListItem>
                                <asp:ListItem Value="0">In-Active</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-12 border-top">
                <div class="row pt-3">
                    <div class="col-12">
                        <h5 class="text-uppercase">Installer Details</h5>
                    </div>
                    <asp:TextBox CssClass="form-control form-control-sm" ID="txtId" AutoComplete="off" runat="server" Visible="false"></asp:TextBox>
                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Title</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtTitle" AutoComplete="off" runat="server" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label class="text-danger">First Name*</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtFirstName" AutoComplete="off" runat="server" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Last Name</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtLastName" AutoComplete="off" runat="server" MaxLength="50"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Extension</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtExtension" AutoComplete="off" runat="server" MaxLength="5"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Phone</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtPhone" AutoComplete="off" runat="server" MaxLength="25"
                                onkeypress="return onlyNumbers(event);" onblur="phoneMask(this)"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Cell</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtCell" AutoComplete="off" runat="server" MaxLength="25"
                                onkeypress="return onlyNumbers(event);" onblur="phoneMask(this)"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label>Email</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtEmail" AutoComplete="off" runat="server" MaxLength="50"
                                onchange="return validateEmail(this)"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-4">
                        <label>&nbsp;</label>
                        <div class="form-group">
                            <asp:Button ID="btnSaveDetail" runat="server" CssClass="btn btn-success btn-sm" Text="Save Contact" OnClick="btnSaveDetail_Click" Enabled="false" />
                            <asp:Button ID="btnCancelDetail" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancelDetail_Click" />
                        </div>
                    </div>
                </div>

                <div class="row pt-3">
                    <div class="col-sm-12">
                        <%--<h5 class="text-uppercase">Task Details</h5>--%>
                        <div class="table-responsive">
                            <asp:GridView CssClass="table mainGridTable table-sm mb-0" ForeColor="White" ID="gvDetail" runat="server" AutoGenerateColumns="false" DataKeyNames="ID,TimeStamp"
                                EnableModelValidation="True" ShowFooter="false" OnRowEditing="gvDetail_RowEditing" OnRowDeleting="gvDetail_RowDeleting" AllowSorting="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Title">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="First Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFirstName" runat="server" Text='<%# Eval("FirstName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Last Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLastName" runat="server" Text='<%# Eval("LastName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Ext.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExtension" runat="server" Text='<%# Eval("Extension") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Phone">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cell">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCell" runat="server" Text='<%# Eval("Cell") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Email">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Modify">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" Text="Edit" CommandName="Edit"><i class="far fa-edit" title="Edit"></i></asp:LinkButton>
                                            <asp:LinkButton CssClass="btn btn-danger btn-sm" title="Delete" runat="server" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure.?');">
                                                <i class="far fa-times-circle"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="100px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hfTimeStamp" runat="server" Value="-1" />
            <asp:HiddenField ID="hfTimeStampDetail" runat="server" Value="-1" />

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
            $('#<%=ddlInstallerLookupList.ClientID%>').chosen();
            $('#<%=ddlCountry.ClientID%>').chosen();
            $('#<%=ddlState.ClientID%>').chosen();
            $('#<%=ddlStatus.ClientID%>').chosen();
        }
    </script>
</asp:Content>
