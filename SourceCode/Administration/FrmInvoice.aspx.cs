using System;
using BOLAERO;
using BLLAERO;
using System.Data;


public partial class Administration_FrmInvoice : System.Web.UI.Page
{
    BOLManageInvoice ObjBOL = new BOLManageInvoice();
    BLLManageInvoice ObjBLL = new BLLManageInvoice();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Bind_Control("");
            }
            imgSignature.ImageUrl = "~/Images/Signature.jpg";
            //txtInvoiceDate.Text = DateTime.Today.ToString("MMMM dd, yyyy"); 
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void Bind_Control(string invoiceid)
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.operation = 1;
            ds = ObjBLL.GetInvoice(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlInvoiceNo, ds.Tables[0]);
                if(ddlInvoiceNo.Items.FindByValue(invoiceid) != null)
                {
                    ddlInvoiceNo.SelectedValue = invoiceid;
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }


    private bool ValidationCheck()
    {
        try
        {
            if(txtInvoiceNo.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Invoice No !");
                txtInvoiceNo.Focus();
                return false;
            }
            if(txtInvoiceDate.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Invoice Date !");
                txtInvoiceDate.Focus();
                return false;
            }
            if (txtInvoicePeriod.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Invoice Period !");
                txtInvoicePeriod.Focus();
                return false;
            }
            if (txtTechDwg.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Technical Drawings for Proposal Qty !");
                txtTechDwg.Focus();
                return false;
            }
            if (txtJobDwg.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Job Drawings Qty !");
                txtJobDwg.Focus();
                return false;
            }
            if (txtFabDwg.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter 	Fabrication Drawings Qty !");
                txtFabDwg.Focus();
                return false;
            }
            if (txtAWSCharges.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter AWS Chanrges !");
                txtAWSCharges.Focus();
                return false;
            }
            if (txtSupportChanges.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Support Charges !");
                txtSupportChanges.Focus();
                return false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }


    protected void btnExportToPDf_Click(object sender, EventArgs e)
    {
        try
        {     
            if(ValidationCheck() == true)
            {
                string fileName = "Invoice " + DateTime.Today.ToString("MMMM dd yyyy");
                InvoicePdfBuilder pdf = new InvoicePdfBuilder(Response,fileName);
                if(txtInvoiceDate.Text.Trim() != "")
                {
                    txtInvoiceDate.Text = Convert.ToDateTime(txtInvoiceDate.Text).ToString("MMMM dd, yyyy");
                }                
                pdf.AddHeader(txtInvoiceNo.Text, txtInvoiceDate.Text,txtInvoicePeriod.Text);
                pdf.AddInvoiceDetails(txtTechDwg.Text, txtJobDwg.Text, txtFabDwg.Text,txtAWSCharges.Text,txtSupportChanges.Text, lblTechDwgTotal.Text, lblJobDwgTotal.Text, lblFabDwgTotal.Text,lblAWSCloudTotal.Text,lblSupportTotal.Text ,lblGrandTotal.Text,"90.00","120.00","750.00");
                pdf.AddSignature(Server.MapPath("~/Images/Signature.jpg"));
                pdf.Close(Response);
            }
            
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }



    protected void txtTechDwg_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if(txtTechDwg.Text.Trim() != "")
            {
                decimal calculateTotal1 = Convert.ToDecimal(txtTechDwg.Text) * 90;
                lblTechDwgTotal.Text = calculateTotal1.ToString("N2");
                GrandTotal();
            }
            else
            {
                decimal calculateTotal1 = 0;
                lblTechDwgTotal.Text = calculateTotal1.ToString("N2");
                GrandTotal();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void txtJobDwg_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if(txtJobDwg.Text.Trim() != "")
            {
                decimal calculateTotal2 = Convert.ToDecimal(txtJobDwg.Text) * 120;
                lblJobDwgTotal.Text = calculateTotal2.ToString("N2");
                GrandTotal();
            }
            else
            {
                decimal calculateTotal2 = 0;
                lblJobDwgTotal.Text = calculateTotal2.ToString("N2");
                GrandTotal();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void txtFabDwg_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtFabDwg.Text.Trim() != "")
            {
                decimal calculateTotal3 = Convert.ToDecimal(txtFabDwg.Text) * 750;
                lblFabDwgTotal.Text = calculateTotal3.ToString();
                GrandTotal();
            }
            else
            {
                decimal calculateTotal3 = 0;                
                lblFabDwgTotal.Text = calculateTotal3.ToString("N2");
                GrandTotal();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void GrandTotal()
    {
        try
        {
            if(lblTechDwgTotal.Text.Trim() != "" && lblJobDwgTotal.Text.Trim() != "" && lblFabDwgTotal.Text.Trim() != "" && lblCallAdvisorySupport.Text.Trim() != "" && lblAWSCloudTotal.Text.Trim() != "" && lblSupportTotal.Text.Trim() != "")
            {
                decimal GrandTotal = Convert.ToDecimal(lblTechDwgTotal.Text) + Convert.ToDecimal(lblJobDwgTotal.Text) + Convert.ToDecimal(lblFabDwgTotal.Text) + Convert.ToDecimal(lblCallAdvisorySupport.Text) + Convert.ToDecimal(lblAWSCloudTotal.Text) + Convert.ToDecimal(lblSupportTotal.Text);
                lblGrandTotal.Text = GrandTotal.ToString();
                lblAmountWords.Text = InvoicePdfBuilder.NumberToWords.Convert(Convert.ToDecimal(GrandTotal));
            }
           
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    private void ResetControls()
    {
        try
        {
            txtInvoiceNo.Text = String.Empty;
            txtInvoiceDate.Text = String.Empty;
            txtInvoicePeriod.Text = String.Empty;
            txtTechDwg.Text = String.Empty;
            txtJobDwg.Text = String.Empty;
            txtFabDwg.Text = String.Empty;
            txtAWSCharges.Text = String.Empty;
            txtSupportChanges.Text = String.Empty;
            lblTechDwgTotal.Text = String.Empty;
            lblJobDwgTotal.Text = String.Empty;
            lblFabDwgTotal.Text = String.Empty;
            lblAWSCloudTotal.Text = String.Empty;
            lblSupportTotal.Text = String.Empty;
            lblGrandTotal.Text = String.Empty;
            lblAmountWords.Text = String.Empty;
            btnSave.Text = "Save";
            btnExportToPDf.Enabled = false;
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
            ResetControls();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
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
                    ObjBOL.InvoiceID = Convert.ToInt32(ddlInvoiceNo.SelectedValue);
                }
                else
                {
                    ObjBOL.InvoiceID = 0;
                }
                ObjBOL.InvoiceNo = txtInvoiceNo.Text;
                ObjBOL.InvoiceDate = Convert.ToDateTime(txtInvoiceDate.Text);
                ObjBOL.InvoicePeriod = txtInvoicePeriod.Text;
                ObjBOL.TechnicalDwg = Convert.ToInt32(txtTechDwg.Text);
                ObjBOL.JobDwg = Convert.ToInt32(txtJobDwg.Text);
                ObjBOL.FabDwg = Convert.ToInt32(txtFabDwg.Text);
                ObjBOL.AWSCharges = Convert.ToDecimal(txtAWSCharges.Text);
                ObjBOL.SupportCharges = Convert.ToDecimal(txtSupportChanges.Text);
                ObjBOL.Total = Convert.ToDecimal(lblGrandTotal.Text);
                msg = ObjBLL.SaveInvoice(ObjBOL);
                if(msg.Trim() == "ER02")
                {
                    Utility.ShowMessage_Error(Page, "Record Already Exists !");
                    return;
                }
                if (msg.Trim() == "U")
                {
                    Bind_Control(ddlInvoiceNo.SelectedValue);
                    Utility.ShowMessage_Success(Page, "Invoice Updated Successfully !");
                    Utility.MaintainLogsSpecial("FrmInvoice", "Update", ddlInvoiceNo.SelectedValue);
                    
                }
                else
                {
                    Bind_Control(msg.Trim().ToString());
                    Utility.ShowMessage_Success(Page, "Invoice Added Successfully !");
                    Utility.MaintainLogsSpecial("FrmInvoice", "Add", msg.ToString());
                }
                btnSave.Text = "Update";
                btnExportToPDf.Enabled = true;
            }
           
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
                    txtInvoiceNo.Text = ds.Tables[0].Rows[0]["InvoiceNo"].ToString();
                    txtInvoiceDate.Text= ds.Tables[0].Rows[0]["InvoiceDate"].ToString();
                    txtInvoicePeriod.Text= ds.Tables[0].Rows[0]["InvoicePeriod"].ToString();
                    txtTechDwg.Text= ds.Tables[0].Rows[0]["TechnicalDwg"].ToString();
                    txtJobDwg.Text= ds.Tables[0].Rows[0]["JobDwg"].ToString();
                    txtFabDwg.Text = ds.Tables[0].Rows[0]["FabDwg"].ToString();
                    txtAWSCharges.Text = ds.Tables[0].Rows[0]["AWSCharges"].ToString();
                    txtSupportChanges.Text = ds.Tables[0].Rows[0]["SupportCharges"].ToString();
                    lblTechDwgTotal.Text = ds.Tables[0].Rows[0]["TechDwgTotal"].ToString();
                    lblJobDwgTotal.Text = ds.Tables[0].Rows[0]["JobDwgTotal"].ToString();
                    lblFabDwgTotal.Text= ds.Tables[0].Rows[0]["FabDwgTotal"].ToString();
                    lblAWSCloudTotal.Text = ds.Tables[0].Rows[0]["AWSTotal"].ToString();
                    lblSupportTotal.Text = ds.Tables[0].Rows[0]["SupportTotal"].ToString();
                    lblGrandTotal.Text= ds.Tables[0].Rows[0]["GrandTotal"].ToString();
                    if(lblGrandTotal.Text != "")
                    {
                        lblAmountWords.Text = InvoicePdfBuilder.NumberToWords.Convert(Convert.ToDecimal(lblGrandTotal.Text));
                    }
                    btnSave.Text = "Update";
                    btnExportToPDf.Enabled = true;
                }
            }
            else
            {
                ResetControls();
                btnExportToPDf.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void txtSupportChanges_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if(txtSupportChanges.Text.Trim() != "" && txtSupportChanges.Text.Trim() != ".")
            {
                decimal calculateTotal5 = Convert.ToDecimal(txtSupportChanges.Text) * 1;
                lblSupportTotal.Text = calculateTotal5.ToString("N2");
                GrandTotal();
            }
            else
            {
                decimal calculateTotal5 = 0;
                txtSupportChanges.Text = calculateTotal5.ToString("N2");
                lblSupportTotal.Text = calculateTotal5.ToString("N2");
                GrandTotal();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void txtAWSCharges_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if(txtAWSCharges.Text.Trim() != "" && txtAWSCharges.Text.Trim() != ".")
            {
                decimal calculateTotal4 = Convert.ToDecimal(txtAWSCharges.Text) * 1;
                lblAWSCloudTotal.Text = calculateTotal4.ToString("N2");
                GrandTotal();
            }
            else
            {
                decimal calculateTotal4 = 0;
                txtAWSCharges.Text = calculateTotal4.ToString("N2");
                lblAWSCloudTotal.Text = calculateTotal4.ToString("N2");
                GrandTotal();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}