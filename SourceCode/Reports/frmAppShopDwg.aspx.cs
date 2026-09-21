using System;
using System.Web.UI;
using BOLAERO;
using BLLAERO;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
public partial class Reports_frmAppShopDwg : System.Web.UI.Page
{
    BOLOpenProposalReports OBJBOL = new BOLOpenProposalReports();
    BLLOpenProposalReportDate OBJBLL = new BLLOpenProposalReportDate();
    commonclass1 clscon = new commonclass1();
    ReportDocument rprt = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetDates();
            BindControls();
        }
    }

    private void BindControls()
    {
        DataTable dt = new DataTable();
        try
        {
            clscon.Return_DT(dt, "EXEC [dbo].[Get_AppShopDwgs] '', '', 1 ");
            if (dt.Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlProjectManager, dt);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void SetDates()
    {
        try
        {
            // int Month = DateTime.Now.Month + 2;
            txtProposalShipDateFrom.Text = DateTime.Now.Month + "/01/" + DateTime.Now.Year;
            // txtProposalShipDateTo.Text = DateTime.Now.AddDays(8).Month + "/" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(2).Month) + "/" + DateTime.Now.AddMonths(2).Year;
            txtProposalShipDateTo.Text = DateTime.Now.AddMonths(0).Month + "/" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(0).Month) + "/" + DateTime.Now.AddMonths(0).Year;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    #region ValidationCheck
    private Boolean ValidationCheck()
    {
        try
        {
            if (txtProposalShipDateFrom.Text == "" && txtProposalShipDateTo.Text == "")
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "alert('Please Enter Proposal Ship Date From. !');", true);
                Utility.ShowMessage_Error(Page, "Please Enter Dwg App Date From. !!");
                txtProposalShipDateFrom.Focus();
                return false;
            }
            if (txtProposalShipDateFrom.Text != "")
            {
                if (txtProposalShipDateTo.Text == "")
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "alert('Please Enter Proposal Ship Date To. !');", true);
                    Utility.ShowMessage_Error(Page, "Please Enter Dwg App Date To. !!");
                    txtProposalShipDateTo.Focus();
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }
    #endregion

    private DataTable ReportDataQuoteOrder()
    {
        DataTable dt = new DataTable();
        try
        {
            DateTime dtfrom = Convert.ToDateTime(txtProposalShipDateFrom.Text);
            DateTime dtto = Convert.ToDateTime(txtProposalShipDateTo.Text);
            string strDateFrom = dtfrom.ToString("MM/dd/yyyy");
            string strDateTo = dtto.ToString("MM/dd/yyyy");
            clscon.Return_DT(dt, "EXEC [dbo].[Get_AppShopDwgs] '" + strDateFrom + "','" + strDateTo + "', 2, " + ddlProjectManager.SelectedValue);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    private void Bind_Report2()
    {
        try
        {
            if (ValidationCheck() == true)
            {
                string headerText = "Approved DWG Report ";
                if (txtProposalShipDateFrom.Text != "" && txtProposalShipDateTo.Text != "")
                {
                    headerText += "From " + txtProposalShipDateFrom.Text + " to " + txtProposalShipDateTo.Text;
                }
                DataTable dt = ReportDataQuoteOrder();

                rprt.Load(Server.MapPath("~/Reports/rptAppShopDwg.rpt"));
                if (dt.Rows.Count > 0)
                {
                    TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                    txtheader.Text = headerText;
                    rprt.SetDataSource(dt);
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
                }
                else
                {
                    //Utility.ShowMessage_Error(Page, "No Matching Data Found !!");
                    rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                    TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                    txtheader.Text = headerText;
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
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

    protected void btnSearchProposal_Click(object sender, EventArgs e)
    {
        try
        {
            Bind_Report2();
        }
        catch (Exception ex)
        {
            if (ex.Message != "Thread was being aborted.")
            {
                Utility.AddEditException(ex);
            }
        }
    }

    protected void btnClearProposal_Click(object sender, EventArgs e)
    {
        try
        {
            SetDates();
            ddlProjectManager.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            BindOrderinExcel();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void BindOrderinExcel()
    {
        try
        {
            if (ValidationCheck() == true)
            {
                DataTable dt = ReportDataQuoteOrder();
                Utility.ExportToExcelDT(dt, "Approved DWG Report From " + txtProposalShipDateFrom.Text + " to " + txtProposalShipDateTo.Text);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }

    }
}