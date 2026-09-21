using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;

public partial class Reports_FrmVisualPartOrderReport : System.Web.UI.Page
{
    commonclass1 cls = new commonclass1();
    ReportDocument rprt = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControls();
        }
    }

    private void BindControls()
    {
        try
        {
            DataSet ds = new DataSet();
            cls.Return_DS(ds, "EXEC Get_VisualPartOrderReport 1");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlPO, ds.Tables[0]);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private string PrepareQuery()
    {
        string query = "EXEC Get_VisualPartOrderReport 2 ";
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

            if (ddlPO.SelectedIndex > 0)
            {
                query += ", " + ddlPO.SelectedValue;
            }
            else
            {
                query += ", '' ";
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }

        return query;
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            string query = PrepareQuery();
            cls.Return_DT(dt, query);
            rprt.Load(Server.MapPath("~/Reports/rptVisualPartOrder.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Visual Part Order Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Visual Part Order Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
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
            if (ddlPO.Items.Count > 0)
            {
                ddlPO.SelectedIndex = 0;
            }
            txtStartDateFrom.Text = string.Empty;
            txtStartDateTo.Text = string.Empty;
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
            DataTable dt = new DataTable();
            string query = PrepareQuery();
            cls.Return_DT(dt, query);
            rprt.Load(Server.MapPath("~/Reports/rptVisualPartOrder.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Visual Part Order Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Visual Part Order Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
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