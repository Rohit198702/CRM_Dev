using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BOLAERO;
using BLLAERO;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Text.RegularExpressions;
using System.Globalization;

public partial class Reports_FrmTriflexInvoice : System.Web.UI.Page
{
    BOLTriflexInvoice ObjBOL = new BOLTriflexInvoice();
    BLLManageTriflexInvoice ObjBLL = new BLLManageTriflexInvoice();
    commonclass1 clscon = new commonclass1();
    ReportDocument rprt = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_Controls("");
        }
    }
    private void Bind_Controls(string invoiceID)
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.operation = 1;
            ds = ObjBLL.GetInvoice(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlInvoiceNo, ds.Tables[0]);
                if(ddlInvoiceNo.Items.FindByValue(invoiceID) != null)
                {
                    ddlInvoiceNo.SelectedValue = invoiceID;
                }
            }

        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    private Boolean ValidationCheck()
    {
        try
        {
            if(txtInvoiceNo.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Invoice No !");
                txtInvoiceNo.Focus();
                return false;
            }
            if (txtDate.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Invoice Date !");
                txtDate.Focus();
                return false;
            }
            if (txtProjectNo.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Project No !");
                txtProjectNo.Focus();
                return false;
            }
            if (txtProjectName.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Project Name !");
                txtProjectName.Focus();
                return false;
            }
            if (txtTo.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter To !");
                txtTo.Focus();
                return false;
            }
            if (txtAddress.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Address !");
                txtAddress.Focus();
                return false;
            }
            if (txtGSTNo.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter GST No !");
                txtGSTNo.Focus();
                return false;
            }
            if (txtTelephoneNo.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Telephone No !");
                txtTelephoneNo.Focus();
                return false;
            }
            if (txtAttention.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Attention !");
                txtAttention.Focus();
                return false;
            }
            if (txtItemName.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Item Name !");
                txtItemName.Focus();
                return false;
            }
            if (txtQuantity.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Quantity !");
                txtQuantity.Focus();
                return false;
            }
            if (txtUnitPrice.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Unit Price !");
                txtUnitPrice.Focus();
                return false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidationCheck() == true)
            {
                string msg = String.Empty;
                ObjBOL.operation = 2;
                if (ddlInvoiceNo.SelectedIndex > 0)
                {
                    ObjBOL.InvoiceID =Convert.ToInt32(ddlInvoiceNo.SelectedValue);
                }
                else
                {
                    ObjBOL.InvoiceID = 0;
                }
                ObjBOL.InvoiceNo = txtInvoiceNo.Text;
                ObjBOL.Date = Utility.ConvertDate(txtDate.Text);
                ObjBOL.ProjectNo = txtProjectNo.Text;
                ObjBOL.ProjectName = txtProjectName.Text;
                ObjBOL.To = txtTo.Text;
                ObjBOL.Address = txtAddress.Text;
                ObjBOL.GSTNo = txtGSTNo.Text;
                ObjBOL.Telephone = txtTelephoneNo.Text;
                ObjBOL.Attention = txtAttention.Text;          
                ObjBOL.ItemName = txtItemName.Text;
                ObjBOL.Qty = Convert.ToInt32(txtQuantity.Text);
                ObjBOL.UnitPrice =Convert.ToDecimal(txtUnitPrice.Text);
                if(txtFreight.Text.Trim() != "")
                {
                    ObjBOL.Freight = Convert.ToDecimal(txtFreight.Text);
                }
                if(txtAssemblyandInstallation.Text.Trim() != "")
                {
                    ObjBOL.AssemblyandInstallation = Convert.ToDecimal(txtAssemblyandInstallation.Text);
                }
                if(txtGSTPercentage.Text.Trim() != "")
                {
                    ObjBOL.GSTPercentage = Convert.ToDecimal(txtGSTPercentage.Text);
                }                      
                msg = ObjBLL.SaveInvoice(ObjBOL);
                if (msg.Trim() == "ER01")
                {
                    Utility.ShowMessage_Error(Page, "Record Already Exists !");
                    return;
                }
                if(msg.Trim() == "U")
                {
                    Bind_Controls(ddlInvoiceNo.SelectedValue);
                    Utility.ShowMessage_Success(Page, "Invoice Updated Successfully !");
                    Utility.MaintainLogsSpecial("FrmTriflexInvoice.aspx", "Update", ddlInvoiceNo.SelectedValue);
                }
                else
                {
                    Bind_Controls(msg.Trim().ToString());
                    Utility.ShowMessage_Success(Page, "Invoice Added Successfully !");
                    Utility.MaintainLogsSpecial("FrmTriflexInvoice.aspx", "Add", msg.Trim().ToString());
                }
                btnSave.Text = "Update";
                btnExport.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void Reset()
    {
        try
        {
            txtInvoiceNo.Text = String.Empty;
            txtDate.Text = String.Empty;
            txtProjectNo.Text = String.Empty;
            txtProjectName.Text = String.Empty;
            txtTo.Text = String.Empty;
            txtAddress.Text = String.Empty;
            txtGSTNo.Text = String.Empty;
            txtTelephoneNo.Text = String.Empty;
            txtAttention.Text = String.Empty;
            txtItemName.Text = String.Empty;
            txtQuantity.Text = String.Empty;
            txtUnitPrice.Text = String.Empty;
            txtAmount.Text = String.Empty;
            txtFreight.Text = String.Empty;
            txtTaxableValue.Text = String.Empty;
            txtGSTPercentage.Text = String.Empty;
            txtCalculateGST.Text = String.Empty;
            txtAssemblyandInstallation.Text = String.Empty;
            txtTotaltaxablevalue.Text = String.Empty;
            btnExport.Enabled = false;
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void Calculations(string Quantity, string unitprice, string Freight, string GSTPercentage, string AssemblyandInstalltion)
    {
        try
        {
            if (!string.IsNullOrEmpty(Quantity) && !string.IsNullOrEmpty(unitprice))
            {               
                if(unitprice == "." || string.IsNullOrEmpty(unitprice))
                {
                    unitprice = "0";
                    txtUnitPrice.Text = unitprice;
                }
                if(Freight == "." || string.IsNullOrEmpty(Freight))
                {
                    Freight = "0";
                    txtFreight.Text = Freight;
                }
                if(AssemblyandInstalltion == "." || string.IsNullOrEmpty(AssemblyandInstalltion))
                {
                    AssemblyandInstalltion = "0";
                    txtAssemblyandInstallation.Text = AssemblyandInstalltion;
                }
                if(GSTPercentage == "." || string.IsNullOrEmpty(GSTPercentage))
                {
                    GSTPercentage = "0";
                    txtGSTPercentage.Text = GSTPercentage;
                }
                decimal quantity = Convert.ToDecimal(Quantity);
                decimal unitPrice = Convert.ToDecimal(unitprice);
                decimal freight = Convert.ToDecimal(Freight);
                decimal assemblyandinstalltion = Convert.ToDecimal(AssemblyandInstalltion);
                decimal gstPercentage = Convert.ToDecimal(GSTPercentage);

                decimal amount = (quantity * unitPrice) + freight + assemblyandinstalltion;
         
                decimal gstRate = Convert.ToDecimal(gstPercentage);   // GST %
                decimal gstAmount = amount * gstRate / 100;

                decimal totalAmount = amount + gstAmount;
                txtAmount.Text ="₹ " + amount.ToString("N2");
                txtTaxableValue.Text = "₹ " + amount.ToString("N2");
                txtCalculateGST.Text = "₹ " + gstAmount.ToString("N2");
                txtTotaltaxablevalue.Text = "₹ " + totalAmount.ToString("N2");
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void txtQuantity_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if(txtQuantity.Text.Trim() == "")
            {
                txtQuantity.Text = "0";
            }
            Calculations(txtQuantity.Text, txtUnitPrice.Text, txtFreight.Text, txtGSTPercentage.Text,txtAssemblyandInstallation.Text);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void txtUnitPrice_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if(txtUnitPrice.Text.Trim() == "")
            {
                txtUnitPrice.Text = "0";
            }
            Calculations(txtQuantity.Text, txtUnitPrice.Text, txtFreight.Text, txtGSTPercentage.Text, txtAssemblyandInstallation.Text);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlInvoiceNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlInvoiceNo.SelectedIndex > 0)
            {
                DataSet ds = new DataSet();
                ObjBOL.operation = 3;
                ObjBOL.InvoiceID = Convert.ToInt32(ddlInvoiceNo.SelectedValue);
                ds = ObjBLL.GetInvoice(ObjBOL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnExport.Enabled = true;
                    txtInvoiceNo.Text = ds.Tables[0].Rows[0]["InvoiceNo"].ToString();
                    txtDate.Text = ds.Tables[0].Rows[0]["InvoiceDate"].ToString();
                    txtProjectNo.Text = ds.Tables[0].Rows[0]["ProjectNo"].ToString();
                    txtProjectName.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                    txtTo.Text = ds.Tables[0].Rows[0]["To"].ToString();
                    txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                    txtGSTNo.Text = ds.Tables[0].Rows[0]["GSTNo"].ToString();
                    txtTelephoneNo.Text = ds.Tables[0].Rows[0]["Telephone"].ToString();
                    txtAttention.Text = ds.Tables[0].Rows[0]["Attention"].ToString();             
                    txtItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                    txtQuantity.Text = ds.Tables[0].Rows[0]["Qunatity"].ToString();
                    txtUnitPrice.Text = ds.Tables[0].Rows[0]["UnitPrice"].ToString();
                    txtAmount.Text = ds.Tables[0].Rows[0]["TotalDisplay"].ToString();
                    txtFreight.Text = ds.Tables[0].Rows[0]["Freight"].ToString();
                    txtAssemblyandInstallation.Text = ds.Tables[0].Rows[0]["AssemblyCharges"].ToString();
                    txtTaxableValue.Text = ds.Tables[0].Rows[0]["Total"].ToString();
                    txtCalculateGST.Text = ds.Tables[0].Rows[0]["GSTAmountDisplay"].ToString();
                    txtTotaltaxablevalue.Text = ds.Tables[0].Rows[0]["GrandTotalDisplay"].ToString();
                    txtGSTPercentage.Text = ds.Tables[0].Rows[0]["GSTPercentage"].ToString();
                    btnSave.Text = "Update";
                }                
            }
            else
            {
                Reset();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlInvoiceNo.Items.Count > 0)
            {
                ddlInvoiceNo.SelectedIndex = 0;
            }
            Reset();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void Bind_Report()
    {
        try
        {
            if (ValidationCheck() == true)
            {
                DataSet ds = new DataSet();
                string headerText = "Triflex Invoice - " + ddlInvoiceNo.SelectedItem.Text;
                ObjBOL.operation = 3;
                ObjBOL.InvoiceID = Convert.ToInt32(ddlInvoiceNo.SelectedValue);
                ds = ObjBLL.GetInvoice(ObjBOL);
                rprt.Load(Server.MapPath("~/Reports/rptTriflexInvoice.rpt"));
                if (ds.Tables[0].Rows.Count > 0)
                {                                  
                    rprt.SetDataSource(ds.Tables[0]);
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, headerText);
                }
                else
                {
                    //Utility.ShowMessage_Error(Page, "No Matching Data Found !!");
                    rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));                 
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, headerText);
                }
            }
        }
        catch (Exception ex)
        {
            if (ex.Message != "Thread was being aborted.")
            {
                Utility.AddEditException(ex);
            }
        }
        finally
        {
            rprt.Close();
            rprt.Dispose();
        }
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlInvoiceNo.SelectedIndex > 0)
            {
                Bind_Report();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void txtFreight_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if(txtFreight.Text.Trim() == "")
            {
                txtFreight.Text = "0";
            }
            Calculations(txtQuantity.Text, txtUnitPrice.Text, txtFreight.Text, txtGSTPercentage.Text, txtAssemblyandInstallation.Text);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void txtGSTPercentage_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if(txtGSTPercentage.Text.Trim() == "")
            {
                txtGSTPercentage.Text = "0";
            }
            Calculations(txtQuantity.Text, txtUnitPrice.Text, txtFreight.Text, txtGSTPercentage.Text, txtAssemblyandInstallation.Text);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
       
    }

    protected void txtAssemblyandInstallation_TextChanged(object sender, EventArgs e)
    {
        try
        {
            Calculations(txtQuantity.Text, txtUnitPrice.Text, txtFreight.Text, txtGSTPercentage.Text, txtAssemblyandInstallation.Text);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}