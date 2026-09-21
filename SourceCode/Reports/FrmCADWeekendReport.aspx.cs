using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_FrmCADWeekendReport : System.Web.UI.Page
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
            if (!Utility.IsAuthorized())
            {
                return;
            }
            cls.Return_DS(ds, "EXEC Get_CADProjectEngineerReport 1, " + Utility.GetCurrentUser());
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlNatureOfTask, ds.Tables[0]);
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                Utility.BindListBoxList(ddlModels, ds.Tables[1]);
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlProjectEngineer, ds.Tables[2]);
            }

            ProjectEngineerPermission(ds.Tables[3].Rows[0][0].ToString());
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ProjectEngineerPermission(string permission)
    {
        try
        {
            divProjectEngineer.Visible = bool.Parse(permission);
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
            cls.Return_DT(dt, "EXEC Get_CADReport " + ddlDays.SelectedValue);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    protected void btnExportToPDF_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = ReportData();
            rprt.Load(Server.MapPath("~/Reports/rptCADDailyProjectReport.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "CAD Daily Project Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "CAD Daily Project Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
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

    protected void btnGenerateExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = ReportData();
            rprt.Load(Server.MapPath("~/Reports/rptCADDailyProjectReport.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "CAD Daily Project Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "CAD Daily Project Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
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

    private bool IsNullOrWhiteSpace(string value)
    {
        return value == null || value.Trim().Length == 0;
    }

    private bool ValidationCheck()
    {
        try
        {
            if ((!IsNullOrWhiteSpace(txtReqByRCDFrom.Text) && IsNullOrWhiteSpace(txtReqByRCDTo.Text)) ||
            (IsNullOrWhiteSpace(txtReqByRCDFrom.Text) && !IsNullOrWhiteSpace(txtReqByRCDTo.Text)))
            {
                Utility.ShowMessage_Error(Page, "Please enter a valid date range by providing both dates, or leave both blank. ");
                return false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    private DataTable ReportData_ProjectEngineer()
    {
        DataTable dt = new DataTable();
        try
        {
            string models = string.Join(";",
                ddlModels.Items.Cast<ListItem>()
                    .Where(x => x.Selected)
                    .Select(x => x.Value)
                    .ToArray());

            string query = "EXEC Get_CADProjectEngineerReport 2, " + ddlNatureOfTask.SelectedValue + ", " + ddlProjectEngineer.SelectedValue +
                ", '" + models + "', '" + txtReqByRCDFrom.Text + "', '" + txtReqByRCDTo.Text + "' ";
            cls.Return_DT(dt, query);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    protected void btnProjectEngineerReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidationCheck())
            {
                return;
            }

            DataTable dt = ReportData_ProjectEngineer();
            rprt.Load(Server.MapPath("~/Reports/rptCADProjectEngineerReport.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "CAD Project Engineer Report - " + ddlProjectEngineer.SelectedItem.Text;
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "CAD Project Engineer Report - " + ddlProjectEngineer.SelectedItem.Text;
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

    protected void btnProjectEngineerReportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidationCheck())
            {
                return;
            }

            DataTable dt = ReportData_ProjectEngineer();
            rprt.Load(Server.MapPath("~/Reports/rptCADProjectEngineerReport.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "CAD Project Engineer Report - " + ddlProjectEngineer.SelectedItem.Text;
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "CAD Project Engineer Report - " + ddlProjectEngineer.SelectedItem.Text;
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlNatureOfTask.Items.Count > 0)
            {
                ddlNatureOfTask.SelectedIndex = 0;
            }

            foreach (System.Web.UI.WebControls.ListItem item in ddlModels.Items)
            {
                item.Selected = false;
            }

            if (ddlProjectEngineer.Items.Count > 0)
            {
                ddlProjectEngineer.SelectedIndex = 0;
            }

            txtReqByRCDFrom.Text = string.Empty;
            txtReqByRCDTo.Text = string.Empty;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}