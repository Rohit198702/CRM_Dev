using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;

public partial class Reports_FrmNestingReport : System.Web.UI.Page
{
    commonclass1 cls = new commonclass1();
    ReportDocument rprt = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControls();
            NestingStartDate();
        }
    }

    private void BindControls()
    {
        try
        {
            DataSet ds = new DataSet();
            cls.Return_DS(ds, "EXEC Get_NestingReport 1");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlNestingFor, ds.Tables[0]);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private string PrepareParameters()
    {
        string query = "";
        try
        {
            if (txtStartDateFrom.Text != "" && txtStartDateTo.Text != "")
            {
                query += ", '" + txtStartDateFrom.Text + "', '" + txtStartDateTo.Text + "' ";
            }
            else
            {
                query += ", '', '' ";
            }

            if (txtSentDateFrom.Text != "" && txtSentDateTo.Text != "")
            {
                query += ", '" + txtSentDateFrom.Text + "', '" + txtSentDateTo.Text + "' ";
            }
            else
            {
                query += ", '', '' ";
            }

            if (ddlNestingStatus.SelectedIndex > 0)
            {
                query += ", " + ddlNestingStatus.SelectedValue;
            }
            else
            {
                query += ", -1 ";
            }

            if (rdbSentToProduction.SelectedValue == "1")
            {
                query += ", 1 ";
            }
            else if (rdbSentToProduction.SelectedValue == "0")
            {
                query += ", 0 ";
            }
            else
            {
                query += ", 2 ";
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return query;
    }

    private string PrepareConsolidatedQuery()
    {
        string query = "EXEC Get_NestingReport 5 " + PrepareParameters();
        return query;
    }

    private void ConsolidatedReport()
    {
        try
        {
            DataTable dt = new DataTable();
            string query = PrepareConsolidatedQuery();
            cls.Return_DT(dt, query);
            rprt.Load(Server.MapPath("~/Reports/rptConsolidatedNestingReport.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Nesting Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Nesting Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
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

    private string PrepareAerowerksQuery()
    {
        string query = "EXEC Get_NestingReport 2 " + PrepareParameters();
        return query;
    }

    private void AerowerksReport()
    {
        try
        {
            DataTable dt = new DataTable();
            string query = PrepareAerowerksQuery();
            cls.Return_DT(dt, query);
            rprt.Load(Server.MapPath("~/Reports/rptAerowerksNestingReport.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Aerowerks Nesting Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Aerowerks Nesting Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
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

    private string PrepareITWQuery()
    {
        string query = "EXEC Get_NestingReport 3 " + PrepareParameters();
        return query;
    }

    private void ITWReport()
    {
        try
        {
            DataTable dt = new DataTable();
            string query = PrepareITWQuery();
            cls.Return_DT(dt, query);
            rprt.Load(Server.MapPath("~/Reports/rptITWNestingReport.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "ITW Nesting Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "ITW Nesting Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
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

    private string PrepareGaylordQuery()
    {
        string query = "EXEC Get_NestingReport 4 " + PrepareParameters();
        return query;
    }

    private void GaylordReport()
    {
        try
        {
            DataTable dt = new DataTable();
            string query = PrepareGaylordQuery();
            cls.Return_DT(dt, query);
            rprt.Load(Server.MapPath("~/Reports/rptGaylordNestingReport.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Gaylord Nesting Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Gaylord Nesting Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
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

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlNestingFor.SelectedValue == "0")
            {
                ConsolidatedReport();
            }
            else if (ddlNestingFor.SelectedValue == "1")
            {
                AerowerksReport();
            }
            else if (ddlNestingFor.SelectedValue == "2")
            {
                ITWReport();
            }
            else if (ddlNestingFor.SelectedValue == "3")
            {
                GaylordReport();
            }
            else
            {
                Utility.ShowMessage_Info(Page, "not available !");
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
            ddlNestingFor.SelectedIndex = 0;
            txtStartDateFrom.Text = string.Empty;
            txtStartDateTo.Text = string.Empty;
            txtSentDateFrom.Text = string.Empty;
            txtSentDateTo.Text = string.Empty;
            rdbSentToProduction.SelectedValue = "2";
            ddlNestingStatus.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlDateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDateType.SelectedValue == "1")
            {
                NestingStartDate();
            }
            else
            {
                NestingSentDate();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void NestingStartDate()
    {
        try
        {
            divStartDateFrom.Visible = true;
            divStartDateTo.Visible = true;
            divSentDateFrom.Visible = false;
            divSentDateTo.Visible = false;

            txtStartDateFrom.Text = txtSentDateFrom.Text;
            txtStartDateTo.Text = txtSentDateTo.Text;
            txtSentDateFrom.Text = string.Empty;
            txtSentDateTo.Text = string.Empty;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void NestingSentDate()
    {
        try
        {
            divSentDateFrom.Visible = true;
            divSentDateTo.Visible = true;
            divStartDateFrom.Visible = false;
            divStartDateTo.Visible = false;

            txtSentDateFrom.Text = txtStartDateFrom.Text;
            txtSentDateTo.Text = txtStartDateTo.Text;
            txtStartDateFrom.Text = string.Empty;
            txtStartDateTo.Text = string.Empty;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ConsolidatedReport_Excel()
    {
        try
        {
            DataTable dt = new DataTable();
            string query = PrepareConsolidatedQuery();
            cls.Return_DT(dt, query);
            rprt.Load(Server.MapPath("~/Reports/rptConsolidatedNestingReport.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Nesting Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Nesting Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, txtheader.Text);
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

    private void AerowerksReport_Excel()
    {
        try
        {
            DataTable dt = new DataTable();
            string query = PrepareAerowerksQuery();
            cls.Return_DT(dt, query);
            rprt.Load(Server.MapPath("~/Reports/rptAerowerksNestingReport.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Aerowerks Nesting Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Aerowerks Nesting Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, txtheader.Text);
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

    private void ITWReport_Excel()
    {
        try
        {
            DataTable dt = new DataTable();
            string query = PrepareITWQuery();
            cls.Return_DT(dt, query);
            rprt.Load(Server.MapPath("~/Reports/rptITWNestingReport.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "ITW Nesting Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "ITW Nesting Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, txtheader.Text);
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

    private void GaylordReport_Excel()
    {
        try
        {
            DataTable dt = new DataTable();
            string query = PrepareGaylordQuery();
            cls.Return_DT(dt, query);
            rprt.Load(Server.MapPath("~/Reports/rptGaylordNestingReport.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Gaylord Nesting Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Gaylord Nesting Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, txtheader.Text);
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
            if (ddlNestingFor.SelectedValue == "0")
            {
                ConsolidatedReport_Excel();
            }
            else if (ddlNestingFor.SelectedValue == "1")
            {
                AerowerksReport_Excel();
            }
            else if (ddlNestingFor.SelectedValue == "2")
            {
                ITWReport_Excel();
            }
            else if (ddlNestingFor.SelectedValue == "3")
            {
                GaylordReport_Excel();
            }
            else
            {
                Utility.ShowMessage_Info(Page, "not available !");
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}