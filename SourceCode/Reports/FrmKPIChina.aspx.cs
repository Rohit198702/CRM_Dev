using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;

public partial class Reports_FrmKPIChina : System.Web.UI.Page
{
    commonclass1 cls = new commonclass1();
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
            txtDateFrom.Text = DateTime.Now.Month + "/01/" + DateTime.Now.Year;
            txtDateTo.Text = DateTime.Now.AddMonths(0).Month + "/" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(0).Month) + "/" + DateTime.Now.AddMonths(0).Year;
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
            if (txtDateFrom.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter From Date !");
                txtDateFrom.Focus();
                return false;
            }

            if (txtDateTo.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter To Date !");
                txtDateTo.Focus();
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
            string query = "EXEC Get_KPIChina 1, '" + txtDateFrom.Text + "', '" + txtDateTo.Text + "'";
            cls.Return_DT(dt, query);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlReportType.SelectedValue == "1")
            {
                KPIChinaReport();
            }
            else if (ddlReportType.SelectedValue == "2")
            {
                KPIChinaDwhQualityReport();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void KPIChinaReport()
    {
        try
        {
            if (ValidationCheck())
            {
                DataTable dt = ReportData();
                rprt.Load(Server.MapPath("~/Reports/rptKPIChina.rpt"));
                if (dt.Rows.Count > 0)
                {
                    TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                    if (txtDateFrom.Text != "" && txtDateTo.Text != "")
                    {
                        txtheader.Text = "KPI Agilent From " + txtDateFrom.Text + " to " + txtDateTo.Text;
                    }
                    else
                    {
                        txtheader.Text = "KPI Agilent Report ";
                    }
                    rprt.SetDataSource(dt);
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
                }
                else
                {
                    rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                    TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                    if (txtDateFrom.Text != "" && txtDateTo.Text != "")
                    {
                        txtheader.Text = "KPI Agilent From " + txtDateFrom.Text + " to " + txtDateTo.Text;
                    }
                    else
                    {
                        txtheader.Text = "KPI Agilent Report ";
                    }
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        finally
        {
            rprt.Close();
            rprt.Dispose();
        }
    }

    private DataTable ReportDataDwgQuality()
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "EXEC Get_KPIChina_DwgQuality 1, '" + txtDateFrom.Text + "', '" + txtDateTo.Text + "'";
            cls.Return_DT(dt, query);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    private void KPIChinaDwhQualityReport()
    {
        try
        {
            if (ValidationCheck())
            {
                DataTable dt = ReportDataDwgQuality();
                rprt.Load(Server.MapPath("~/Reports/rptKPIChina_DwgQuality.rpt"));
                if (dt.Rows.Count > 0)
                {
                    TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                    if (txtDateFrom.Text != "" && txtDateTo.Text != "")
                    {
                        txtheader.Text = "KPI Agilent Accuracy Level From " + txtDateFrom.Text + " to " + txtDateTo.Text;
                    }
                    else
                    {
                        txtheader.Text = "KPI Agilent Accuracy Level Report ";
                    }
                    rprt.SetDataSource(dt);
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
                }
                else
                {
                    rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                    TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                    if (txtDateFrom.Text != "" && txtDateTo.Text != "")
                    {
                        txtheader.Text = "KPI Agilent Accuracy Level From " + txtDateFrom.Text + " to " + txtDateTo.Text;
                    }
                    else
                    {
                        txtheader.Text = "KPI Agilent Accuracy Level Report ";
                    }
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        finally
        {
            rprt.Close();
            rprt.Dispose();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            SetDates();
            ddlReportType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void KPIChinaReport_Excel()
    {
        try
        {
            if (ValidationCheck())
            {
                DataTable dt = ReportData();
                rprt.Load(Server.MapPath("~/Reports/rptKPIChina.rpt"));
                if (dt.Rows.Count > 0)
                {
                    TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                    if (txtDateFrom.Text != "" && txtDateTo.Text != "")
                    {
                        txtheader.Text = "KPI Agilent From " + txtDateFrom.Text + " to " + txtDateTo.Text;
                    }
                    else
                    {
                        txtheader.Text = "KPI Agilent Report ";
                    }
                    rprt.SetDataSource(dt);
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, txtheader.Text);
                }
                else
                {
                    rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                    TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                    if (txtDateFrom.Text != "" && txtDateTo.Text != "")
                    {
                        txtheader.Text = "KPI Agilent From " + txtDateFrom.Text + " to " + txtDateTo.Text;
                    }
                    else
                    {
                        txtheader.Text = "KPI Agilent Report ";
                    }
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, txtheader.Text);
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        finally
        {
            rprt.Close();
            rprt.Dispose();
        }
    }

    private void KPIChinaDwhQualityReport_Excel()
    {
        try
        {
            if (ValidationCheck())
            {
                DataTable dt = ReportDataDwgQuality();
                rprt.Load(Server.MapPath("~/Reports/rptKPIChina_DwgQuality.rpt"));
                if (dt.Rows.Count > 0)
                {
                    TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                    if (txtDateFrom.Text != "" && txtDateTo.Text != "")
                    {
                        txtheader.Text = "KPI Agilent Accuracy Level From " + txtDateFrom.Text + " to " + txtDateTo.Text;
                    }
                    else
                    {
                        txtheader.Text = "KPI Agilent Accuracy Level Report ";
                    }
                    rprt.SetDataSource(dt);
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, txtheader.Text);
                }
                else
                {
                    rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                    TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                    if (txtDateFrom.Text != "" && txtDateTo.Text != "")
                    {
                        txtheader.Text = "KPI Agilent Accuracy Level From " + txtDateFrom.Text + " to " + txtDateTo.Text;
                    }
                    else
                    {
                        txtheader.Text = "KPI Agilent Accuracy Level Report ";
                    }
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, txtheader.Text);
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        finally
        {
            rprt.Close();
            rprt.Dispose();
        }
    }

    protected void btnGenerateExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlReportType.SelectedValue == "1")
            {
                KPIChinaReport_Excel();
            }
            else if (ddlReportType.SelectedValue == "2")
            {
                KPIChinaDwhQualityReport_Excel();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}