<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="FrmTriflexInvoice.aspx.cs" Inherits="Reports_FrmTriflexInvoice" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="triflexUpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="col-12 pt-2 piDiv position-sticky">
                <div class="row">
                    <div class="col-12">
                        <div class="d-flex align-items-center mb-2">
                            <button type="button" class="btn btn-info btn-sm mr-3" onclick="previousPage()">
                                <i class="fas fa-chevron-left fa-sm"></i>Back</button>
                            <h4 class="title-hyphen position-relative">Triflex Invoice Details</h4>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-sm-7 col-md-8 col-lg-6 col-xl-6">
                        <div class="row">
                            <div class="col-sm-3 col-md-auto mb-3">
                                <label class="mb-0">Invoice No</label>
                            </div>
                            <div class="col-sm-6 col-md mb-6 chosenFullWidth">
                                <asp:DropDownList ID="ddlInvoiceNo"
                                    runat="server"
                                    CssClass="form-control form-control-sm"
                                    DataTextField="text"
                                    DataValueField="id"
                                    AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlInvoiceNo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm col-md col-lg col-xl-auto">
                        <div class="row">
                            <div class="col-auto">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-sm"
                                    Text="Save" OnClientClick="return updateEditor();" OnClick="btnSave_Click" />

                                <asp:Button ID="btnExport" runat="server" CssClass="btn btn-secondary btn-sm"
                                    CausesValidation="false"
                                    OnClientClick="window.document.forms[0].target='_blank';setTimeout(function(){document.forms[0].target='';},0);"
                                    Text="Preview" OnClick="btnExport_Click" Enabled="false" />

                                <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger btn-sm"
                                    Text="Cancel" OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="col-12">
                <div class="row">
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="text-danger">Invoice No*</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtInvoiceNo" runat="server" MaxLength="50" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="text-danger">Date*</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtDate" runat="server" OnBlur="validateDate(this)" autocomplete="off"></asp:TextBox>
                            <asp:CalendarExtender ID="cal_txtDate" runat="server" Format="MM/dd/yyyy" PopupButtonID="txtDate" TargetControlID="txtDate"></asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="text-danger">Project No*</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtProjectNo" runat="server" MaxLength="50" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="text-danger">Project Name*</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtProjectName" runat="server" MaxLength="50" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-4">
                        <div class="form-group">
                            <label class="text-danger">To*</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtTo" runat="server" MaxLength="50" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-4">
                        <div class="form-group">
                            <label class="text-danger">Address*</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtAddress" runat="server" MaxLength="500" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="text-danger">GST No*</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtGSTNo" runat="server" MaxLength="50" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="text-danger">Telephone No*</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtTelephoneNo" runat="server" MaxLength="20" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="text-danger">Attention*</label>
                            <asp:TextBox CssClass="form-control form-control-sm" ID="txtAttention" runat="server" MaxLength="20" autocomplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-12">
                        <div class="form-group">

                            <label class="text-danger font-weight-bold mb-1">
                                Item Name*
                            </label>

                            <div class="card shadow-sm">

                                <!-- Toolbar -->
                                <div class="card-header bg-light py-2">

                                    <div class="d-flex align-items-center flex-wrap">

                                        <!-- Font -->
                                        <select class="form-control form-control-sm mr-2"
                                            style="width: 170px"
                                            onchange="cmd('fontName',this.value)">
                                            <option>Arial</option>
                                            <option>Calibri</option>
                                            <option>Tahoma</option>
                                            <option>Verdana</option>
                                            <option>Times New Roman</option>
                                        </select>

                                        <!-- Font Size -->
                                        <select class="form-control form-control-sm mr-2"
                                            style="width: 70px"
                                            onchange="cmd('fontSize',this.value)">
                                            <option value="1">8</option>
                                            <option value="2" selected>10</option>
                                            <option value="3">12</option>
                                            <option value="4">14</option>
                                            <option value="5">18</option>
                                            <option value="6">24</option>
                                        </select>

                                        <div class="btn-group mr-2">

                                            <button type="button"
                                                class="btn btn-outline-secondary btn-sm"
                                                onclick="simple('bold')">
                                                <b>B</b>
                                            </button>

                                            <button type="button"
                                                class="btn btn-outline-secondary btn-sm"
                                                onclick="simple('italic')">
                                                <i>I</i>
                                            </button>

                                            <button type="button"
                                                class="btn btn-outline-secondary btn-sm"
                                                onclick="simple('underline')">
                                                <u>U</u>
                                            </button>

                                            <button type="button"
                                                class="btn btn-outline-secondary btn-sm"
                                                onclick="simple('strikeThrough')">
                                                <s>S</s>
                                            </button>

                                        </div>

                                        <div class="btn-group mr-2">

                                            <button type="button"
                                                class="btn btn-outline-secondary btn-sm"
                                                onclick="simple('insertUnorderedList')">
                                                •
                                            </button>

                                            <button type="button"
                                                class="btn btn-outline-secondary btn-sm"
                                                onclick="simple('insertOrderedList')">
                                                1.
                                            </button>

                                        </div>

                                        <div class="btn-group mr-2">

                                            <button type="button"
                                                class="btn btn-outline-secondary btn-sm"
                                                onclick="simple('justifyLeft')">
                                                <i class="fas fa-align-left"></i>
                                            </button>

                                            <button type="button"
                                                class="btn btn-outline-secondary btn-sm"
                                                onclick="simple('justifyCenter')">
                                                <i class="fas fa-align-center"></i>
                                            </button>

                                            <button type="button"
                                                class="btn btn-outline-secondary btn-sm"
                                                onclick="simple('justifyRight')">
                                                <i class="fas fa-align-right"></i>
                                            </button>

                                            <button type="button"
                                                class="btn btn-outline-secondary btn-sm"
                                                onclick="simple('justifyFull')">
                                                <i class="fas fa-align-justify"></i>
                                            </button>

                                        </div>

                                    </div>

                                </div>

                                <!-- Editor -->

                                <div id="editor"
                                    contenteditable="true"
                                    class="form-control border-0"
                                    style="height: 350px; overflow: auto; resize: vertical; font-family: Arial; font-size: 10pt; background: #fff;">
                                </div>

                            </div>

                            <asp:TextBox
                                ID="txtItemName"
                                runat="server"
                                TextMode="MultiLine"
                                CssClass="d-none">
                            </asp:TextBox>

                        </div>
                    </div>

                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="text-danger">Quantity*</label>
                            <asp:TextBox CssClass="form-control form-control-sm text-right" ID="txtQuantity" runat="server" autocomplete="off" onkeypress="return onlyNumbers(event);" OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="text-danger">Unit Price*</label>
                            <asp:TextBox CssClass="form-control form-control-sm text-right" ID="txtUnitPrice" runat="server" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" OnTextChanged="txtUnitPrice_TextChanged" AutoPostBack="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label>Freight</label>
                            <asp:TextBox CssClass="form-control form-control-sm text-right bg-light text-dark border" ID="txtFreight" runat="server" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" AutoPostBack="true" OnTextChanged="txtFreight_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label>Assemply & Installation</label>
                            <asp:TextBox CssClass="form-control form-control-sm text-right bg-light text-dark border" ID="txtAssemblyandInstallation" runat="server" autocomplete="off" onkeypress="return onlyDotsAndNumbers(this,event);" AutoPostBack="true" OnTextChanged="txtAssemblyandInstallation_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label>Total Amount</label>
                            <asp:TextBox CssClass="form-control form-control-sm text-right" ID="txtAmount" runat="server" autocomplete="off" Enabled="false" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label class="text-danger">GST*</label>
                            <asp:TextBox class="form-control form-control-sm text-right" ID="txtGSTPercentage" runat="server" autocomplete="off" onkeypress="return onlyNumbers(event);" MaxLength="3" AutoPostBack="true" OnTextChanged="txtGSTPercentage_TextChanged"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2" style="display: none">
                        <div class="form-group">
                            <label>Taxable Value</label>
                            <asp:TextBox CssClass="form-control form-control-sm text-right" ID="txtTaxableValue" runat="server" autocomplete="off" Enabled="false" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>

                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label>GST Amount</label>
                            <asp:TextBox class="form-control form-control-sm text-right" ID="txtCalculateGST" runat="server" autocomplete="off" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6 col-sm-4 col-md-3 col-lg-2">
                        <div class="form-group">
                            <label>Total Taxable Value</label>
                            <asp:TextBox CssClass="form-control form-control-sm text-right" ID="txtTotaltaxablevalue" runat="server" autocomplete="off" Enabled="false" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>

                </div>
            </div>
            <asp:HiddenField ID="hfEditor" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
    </asp:UpdatePanel>
    <script type="text/javascript">
        $(function () {
            InitializePage();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function () {
                // Save latest editor HTML before postback
                updateEditor();
            });

            prm.add_endRequest(function () {
                InitializePage();
            });
        });

        function InitializePage() {
            DDLName();
            LoadEditor();
            var editor = document.getElementById("editor");
            editor.removeEventListener("keyup", updateEditor);
            editor.removeEventListener("paste", updateEditor);
            editor.removeEventListener("blur", updateEditor);
            editor.removeEventListener("input", updateEditor);
            editor.addEventListener("keyup", updateEditor);
            editor.addEventListener("paste", updateEditor);
            editor.addEventListener("blur", updateEditor);
            editor.addEventListener("input", updateEditor);

        }

        function DDLName() {
            $('#<%=ddlInvoiceNo.ClientID%>').chosen();
        }

        function simple(c) {
            document.getElementById("editor").focus();
            document.execCommand(c, false, null);
            updateEditor();
        }

        function cmd(c, v) {
            document.getElementById("editor").focus();
            document.execCommand(c, false, v);
            updateEditor();
        }
        function LoadEditor() {

            var editor = document.getElementById("editor");
            var hidden = document.getElementById("<%=txtItemName.ClientID%>");

            if (editor.innerHTML.trim() == "") {
                editor.innerHTML = hidden.value;
            }
        }

        function updateEditor() {
            var html = document.getElementById("editor").innerHTML;
            document.getElementById("<%=txtItemName.ClientID%>").value = html;
            document.getElementById("<%=hfEditor.ClientID%>").value = html;
        }
    </script>
    <CR:CrystalReportViewer ID="rptGenerateInvoice" runat="server" AutoDataBind="true" BestFitPage="False" Width="100%" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ToolPanelView="None" />
</asp:Content>

