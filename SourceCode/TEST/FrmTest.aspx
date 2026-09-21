<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmTest.aspx.cs" Inherits="TEST_FrmTest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content_Projects" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel_Projects" runat="server">
        <ContentTemplate>
            <div class="col-12">
                <asp:Label id="lblEmail" runat="server"></asp:Label>
                <br />

                <asp:Label id="lblCalendars" runat="server"></asp:Label>
                <br />
                <br />
                <asp:Label id="lblInfo" runat="server"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
