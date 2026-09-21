using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_FrmThirdPartyInstallations : System.Web.UI.Page
{
    commonclass1 clscon = new commonclass1();
    ReportDocument rprt = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetDates();
        }
    }

    private void SetDates()
    {
        try
        {
            txtInstallationStartDateFrom.Text = DateTime.Now.Month + "/01/" + DateTime.Now.Year;
            txtInstallationStartDateTo.Text = DateTime.Now.AddMonths(0).Month + "/" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(0).Month) + "/" + DateTime.Now.AddMonths(0).Year;
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
            if (txtInstallationStartDateFrom.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter From Date !");
                txtInstallationStartDateFrom.Focus();
                return false;
            }

            if (txtInstallationStartDateTo.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter To Date !");
                txtInstallationStartDateTo.Focus();
                return false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    private DataTable ReportData()
    {
        DataTable dt = new DataTable();
        try
        {
            string technician = string.Empty;
            string query = "EXEC Get_ThirdPartyInstallations 1, '" + Utility.ConvertDate(txtInstallationStartDateFrom.Text) + "', '" + Utility.ConvertDate(txtInstallationStartDateTo.Text) + "'";
            clscon.Return_DT(dt, query);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidationCheck())
            {
                DataTable dt = ReportData();
                if (dt.Rows.Count > 0)
                {
                    rprt.Load(Server.MapPath("~/Reports/rptThirdPartyInstallations.rpt"));
                    if (dt.Rows.Count > 0)
                    {
                        TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                        txtheader.Text = "Third Party Installations From " + txtInstallationStartDateFrom.Text + " to " + txtInstallationStartDateTo.Text;
                        rprt.SetDataSource(dt);
                        rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
                    }
                }
                else
                {
                    rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                    TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                    txtheader.Text = "Third Party Installations From " + txtInstallationStartDateFrom.Text + " to " + txtInstallationStartDateTo.Text;
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
                }
            }
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
            if (ValidationCheck())
            {
                DataTable dt = ReportData();
                Utility.ExportToExcelDT(dt, "Third Party Installations From " + txtInstallationStartDateFrom.Text + " to " + txtInstallationStartDateTo.Text);
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
            SetDates();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}