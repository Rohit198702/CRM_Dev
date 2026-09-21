using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Gaylord_FrmGaylordProjectsReport : System.Web.UI.Page
{
    ReportDocument rprt = new ReportDocument();
    commonclass1 commonClass = new commonclass1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetDates();
            BindControls();
        }
    }

    private void SetDates()
    {
        try
        {
            txtPODateFrom.Text = DateTime.Now.Month + "/01/" + DateTime.Now.Year;
            txtPODateTo.Text = DateTime.Now.AddMonths(0).Month + "/" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(0).Month) + "/" + DateTime.Now.AddMonths(0).Year;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void BindControls()
    {
        try
        {
            DataTable dt = new DataTable();
            commonClass.Return_DT(dt, "Exec GL_GetProjects 2 ");
            if (dt.Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlPOType, dt);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private DataTable ReportData()
    {
        DataTable dt = new DataTable();
        try
        {

            string query = "EXEC GL_GetProjects 1, '' ";            

            if (txtPODateFrom.Text != "" && txtPODateTo.Text != "")
            {
                query += ", '" + txtPODateFrom.Text + "' ";
                query += ", '" + txtPODateTo.Text + "' ";
            }
            else
            {
                query += ", '' ";
                query += ", '' ";
            }

            if (ddlPOType.SelectedIndex > 0)
            {
                query += ", " + ddlPOType.SelectedValue;
            }
            else
            {
                query += ", 0 ";
            }

            commonClass.Return_DT(dt, query);
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
            DataTable dt = ReportData();
            rprt.Load(Server.MapPath("~/Reports/rptGaylordProjects.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                //txtheader.Text = txtSearchPNum.Text + " - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                txtheader.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                //txtheader.Text = txtSearchPNum.Text + " - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                txtheader.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlPOType.Items.Count > 0)
            {
                ddlPOType.SelectedIndex = 0;
            }

            SetDates();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnGenerateExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = ReportData();
            rprt.Load(Server.MapPath("~/Reports/rptGaylordProjects.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                //txtheader.Text = txtSearchPNum.Text + " - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                txtheader.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                //txtheader.Text = txtSearchPNum.Text + " - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                txtheader.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
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
}