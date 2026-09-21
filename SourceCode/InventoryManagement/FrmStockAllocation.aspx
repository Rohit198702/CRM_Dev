<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.master" CodeFile="FrmStockAllocation.aspx.cs" Inherits="InventoryManagement_FrmStockAllocation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
        <ContentTemplate>
            <div class="col-12 pt-2 piDiv position-sticky">
                <div class="row">
                    <div class="col-12">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()"><i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Stock Allocation</h4>
                        </div>
                    </div>
                </div>

                <%--<div class="col-sm col-md col-lg col-xl-auto">--%>
                    <div class="row">
                        <div class="col-auto">
                            <asp:Button CssClass="btn btn-info btn-sm" ID="btnGenerateExcel" CausesValidation="false" runat="server" Text="Export to Excel" OnClick="btnGenerateExcel_Click" />
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-sm" CausesValidation="false" Visible="false" Text="Save" OnClick="btnSave_Click" />
                            <%--<asp:Button ID="btnAllocate" runat="server" CssClass="btn btn-primary btn-sm" Visible="false" CausesValidation="false" Text="Allocate" OnClick="btnAllocate_Click" />--%>
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                <%--</div>--%>
            </div>

            <div class="col-sm-12">
                <asp:Panel ID="panGrid" runat="server">
                    <div class="row pt-3">
                        <div class="col-12">
                            <div class="table-responsive eoeTable">
                                <asp:GridView ID="gvMainPartDetail" CssClass="table mainGridTable table-sm mx-auto mb-0" DataKeyNames="PartId"
                                    runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Width="100%">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Part#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartNumber" runat="server" Text='<%# Eval("PartNumber") %>'></asp:Label>
                                            </ItemTemplate>                                           
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartDesc" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                            </ItemTemplate>                                           
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="UM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUM" runat="server" Text='<%# Eval("UM") %>'></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                            <HeaderTemplate>
                                                <span title="Qty Received">Qty Rec.
                                                </span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblQtyReceived" runat="server" Text='<%# Eval("QtyReceived") %>'></asp:Label>
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="10%" />--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                            <HeaderTemplate>
                                                <span title="Qty Pending">Qty Pen.
                                                </span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblQtyPending" runat="server" Text='<%# Eval("QtyPending") %>'></asp:Label>
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="10%" />--%>
                                            <HeaderStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                            <HeaderTemplate>
                                                <span title="(Received + Pending)">Qty Ava.
                                                </span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblQtyAvailable" runat="server" Text='<%# 
                                                (Convert.ToInt32(Eval("QtyPending") == DBNull.Value ? 0 : Eval("QtyPending")) 
                                                + 
                                                Convert.ToInt32(Eval("QtyReceived") == DBNull.Value ? 0 : Eval("QtyReceived")))
                                                %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="10%" />--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Allocation %">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAllocationPercent" runat="server" autocomplete="off" CssClass="form-control form-control-sm text-right" Text='<%# Eval("AllocationPercent") %>'
                                                    onkeypress="return onlyDotsAndNumbers(this,event);" MaxLength="5" onchange="changeAllocateQty(this)"></asp:TextBox>
                                            </ItemTemplate>                                           
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Allocation Qty" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAllocationQty" runat="server"></asp:Label>
                                                <asp:HiddenField ID="hfAllocationQty" runat="server" Value='<%# Eval("AllocationQty") %>' />
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="10%" />--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qty to keep in Canada" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                            <HeaderTemplate>
                                                <span title="Qty to keep in Canada">Qty Canada
                                                </span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblKeepInCanada" runat="server" >
                                                </asp:Label>
                                                <asp:HiddenField ID="hfKeepInCanada" runat="server" Value='<%# Eval("QtyKeepInCanada") %>' />
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="10%" />--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <span title="Ship Date to Gaffeny">Ship Date
                                                </span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtShipDate" CssClass="form-control form-control-sm" autocomplete="off" runat="server" OnBlur="validateDate(this)" Text='<%#Eval("ShipDate")%>'>
                                                </asp:TextBox>
                                                <asp:CalendarExtender ID="txtShipDate_Extender" runat="server" Format="MM/dd/yyyy"
                                                    PopupButtonID="txtShipDate" TargetControlID="txtShipDate">
                                                </asp:CalendarExtender>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Priority">
                                            <ItemTemplate>
                                                <asp:DropDownList CssClass="form-control form-control-sm control-access" ID="ddlPriority" runat="server" SelectedValue='<%#Eval("Priority")%>'>
                                                    <asp:ListItem Value="">Select</asp:ListItem>
                                                    <asp:ListItem Value="H">High</asp:ListItem>
                                                    <asp:ListItem Value="M">Medium</asp:ListItem>
                                                    <asp:ListItem Value="L">Low</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="10%" />--%>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Notes">
                                            <ItemTemplate>
                                                <asp:TextBox CssClass="form-control form-control-sm " MaxLength="200" ID="txtNotes" runat="server" autocomplete="off" Text='<%#Eval("Notes")%>' />
                                            </ItemTemplate>
                                            <%--<ItemStyle Width="18%" />--%>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnGenerateExcel" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function changeAllocateQty(sender) {
            var row = sender.closest("tr");

            // Get controls inside the row

            var lblQtyAvailable = row.querySelector("span[id$='lblQtyAvailable']");
            var txtAllocationPercent = row.querySelector("input[id$='txtAllocationPercent']");
            var lblAllocationQty = row.querySelector("span[id$='lblAllocationQty']");
            var lblKeepInCanada = row.querySelector("span[id$='lblKeepInCanada']");
            var hfAllocationQty = row.querySelector("input[type='hidden'][id$='hfAllocationQty']");
            var hfKeepInCanada = row.querySelector("input[type='hidden'][id$='hfKeepInCanada']");

            // Convert values to numbers         
            let qtyAvailable = parseInt(lblQtyAvailable.textContent.trim(), 10);
            let allocationPercent = parseFloat(txtAllocationPercent.value);

            if (allocationPercent > 100) {
                showError("allocation percent cannot be greater then 100 !!");
                txtAllocationPercent.value = "";
                return;
            }

            if (!isNaN(allocationPercent) && allocationPercent > 0) {
                let allocationQty = Math.floor((qtyAvailable * allocationPercent) / 100);

                lblAllocationQty.innerText = allocationQty;
                hfAllocationQty.value = allocationQty;

                let keepQty = qtyAvailable - allocationQty;
                lblKeepInCanada.innerText = keepQty;
                hfKeepInCanada.value = keepQty;
            } else {
                let allocationQty = "";

                lblAllocationQty.innerText = allocationQty;
                hfAllocationQty.value = allocationQty;

                let keepQty = "";
                lblKeepInCanada.innerText = keepQty;
                hfKeepInCanada.value = keepQty;
            }
        }
    </script>
</asp:Content>
