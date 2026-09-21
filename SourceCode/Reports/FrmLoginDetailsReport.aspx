<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmLoginDetailsReport.aspx.cs" Inherits="Reports_FrmLoginDetailsReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content_SalesActivity" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel_SalesActivity" runat="server">
        <ContentTemplate>
            <div class="col-12 pt-2 piDiv position-sticky">
                <div class="row">
                    <div class="col-12">
                        <div class="d-flex align-items-center mb-2">
                            <h4 class="title-hyphen position-relative">Login Details</h4>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-2">
                        <div class="form-group">
                            <label>Users</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlUsers" runat="server" DataTextField="text" DataValueField="id">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-2">
                        <div class="form-group">
                            <label>IP Address</label>
                            <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlIPAddress" runat="server" DataTextField="text" DataValueField="text">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="col-2">
                        <div class="row">
                            <label class="col-12">&nbsp;</label>
                            <div class="col-auto">
                                <asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary btn-sm" CausesValidation="false" Text="Search" OnClick="btnShow_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-12">
                <div class="row">
                    <div class="col-12 mx-auto">
                        <div class="table-responsive">
                            <asp:GridView CssClass="table mainGridTable table-sm mb-0" ID="gvLoginDetails" runat="server" AutoGenerateColumns="true" BackColor="White" BorderColor="#999999"
                                BorderStyle="Solid" BorderWidth="1px" ForeColor="White"
                                EnableModelValidation="True" EmptyDataText="No Item found! " AllowSorting="true" OnSorting="gvLoginDetails_Sorting">
                            </asp:GridView>
                        </div>
                    </div>
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
            $('#<%=ddlUsers.ClientID%>').chosen();
            $('#<%=ddlIPAddress.ClientID%>').chosen();
        }
    </script>
</asp:Content>
