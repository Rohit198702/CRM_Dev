<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmMafsiRegionMaintenance.aspx.cs" Inherits="Mafsi_FrmMafsiRegionMaintenance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content_MafsiRegionMaintenance" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel_MafsiRegionMaintenance" runat="server">
        <ContentTemplate>
            <div class="col-12 pt-2 piDiv position-sticky">
                <div class="row">
                    <div class="col-12">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Mafsi Region Maintenance</h4>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-5 col-md-5 col-lg-5 col-xl-5">
                        <div class="row">
                            <div class="col-sm-3 mb-3">
                                <label class="mb-0">Mafsi Region</label>
                            </div>
                            <div class="col-sm-9 mb-3 chosenFullWidth">
                                <asp:DropDownList CssClass="form-control form-control-sm" ID="ddlMafsiRegion" runat="server" DataTextField="text" DataValueField="id"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlMafsiRegion_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm col-md col-lg col-xl-auto">
                        <div class="row">
                            <div class="col-auto">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-sm" CausesValidation="false" Text="Save" OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-12 border-top">
                <div class="row pt-3">
                    <div class="col-12">
                        <h5 class="text-uppercase">Task Information</h5>
                    </div>

                    <div class="col-sm-2">
                        <div class="form-group">
                            <label class="text-danger">Region No*</label>
                            <asp:TextBox CssClass="form-control form-control-sm " MaxLength="5" ID="txtRegionNo" runat="server" autocomplete="off" />
                        </div>
                    </div>

                    <div class="col-sm-4">
                        <div class="form-group">
                            <label class="text-danger">Region Name*</label>
                            <asp:TextBox CssClass="form-control form-control-sm " MaxLength="30" ID="txtRegionName" runat="server" autocomplete="off" />
                        </div>
                    </div>

                    <div class="col-sm-1">
                        <div class="form-group">
                            <label>Sort Order</label>
                            <asp:TextBox CssClass="form-control form-control-sm " MaxLength="5" ID="txtSortOrder" onkeypress="return onlyNumbers(this,event);" runat="server" autocomplete="off" />
                        </div>
                    </div>

                    <div class="col-sm-auto">
                        <div class="form-group srRadiosBtns">
                            <label>Is Active</label>
                            <asp:RadioButtonList ID="rdoStatus" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected>Yes &nbsp;</asp:ListItem>
                                <asp:ListItem Value="0">No</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hfTimeStamp" runat="server" Value="-1" />
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
            $('#<%=ddlMafsiRegion.ClientID%>').chosen();
        }
    </script>
</asp:Content>
