using System;
using BOLAERO;
using BLLAERO;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

public partial class Reports_frmProjectInstallationStatusReport : System.Web.UI.Page
{
    BOLWeeklyInstallments ObjBOL = new BOLWeeklyInstallments();
    BLLWeeklyInstallations ObjBLL = new BLLWeeklyInstallations();
    commonclass1 clscon = new commonclass1();
    ReportDocument rprt = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_Controls();
        }
    }
    private void Bind_Controls()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 1;
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlProjectManagers, ds.Tables[0]);
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlInstallationAssTo, ds.Tables[1]);
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
            if (ddlProjectManagers.Items.Count > 0)
            {
                ddlProjectManagers.SelectedIndex = 0;
            }
            if (ddlStatus.Items.Count > 0)
            {
                ddlStatus.SelectedIndex = 0;
            }
            if (ddlInstallationAssTo.SelectedIndex > 0)
            {
                ddlInstallationAssTo.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private DataTable dtSubReport()
    {
        DataTable dtSubReport = new DataTable();
        try
        {
            clscon.Return_DT(dtSubReport, "Exec [dbo].[Get_ProjectInstallationStatus_SubReport]");
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dtSubReport;
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            string Qstr = String.Empty;
            DateTime? fromDate = null;
            DateTime? toDate = null;
            DateTime? inputDate = DateTime.Today;

            if (inputDate != null)
            {
                DateTime today = inputDate.Value;

                // Get current week's Monday
                int diff = today.DayOfWeek - DayOfWeek.Monday;

                if (diff < 0)
                    diff += 7;

                DateTime currentWeekMonday = today.AddDays(-diff);

                // Last week's Monday
                fromDate = currentWeekMonday.AddDays(-7);

                // Next week's Saturday
                toDate = currentWeekMonday.AddDays(12);
            }
            if (ddlStatus.SelectedIndex > 0)
            {
                if (ddlStatus.SelectedValue == "1")
                {
                    Qstr += " AND tblProjects.InstallationCompletionDate IS NOT NULL";
                }
                else if (ddlStatus.SelectedValue == "2")
                {
                    Qstr += " AND tblProjects.InstallationCompletionDate IS NULL";
                }
            }
            if (ddlProjectManagers.SelectedIndex > 0)
            {
                Qstr += " AND tblPFiles.projectmanagerid= " + ddlProjectManagers.SelectedValue;
            }
            if (ddlInstallationAssTo.SelectedIndex > 0)
            {
                Qstr += " AND tblProjects.InstallationAssignedTo= " + ddlInstallationAssTo.SelectedValue;
            }
            Qstr += " Order by tblProjects.JobID ";
            DataSet ds = new DataSet();
            ObjBOL.Operation = 2;
            ObjBOL.searchvar = Qstr;
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables.Count == 0) return;
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtMainReport = ds.Tables[0];
                DataTable dt1 = dtSubReport();
                rprt.Load(Server.MapPath("~/Reports/rptProjectInstallationStatusReport.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                if(inputDate != null)
                {
                    txtheader.Text = "Weekly Installation Report from " + fromDate.Value.ToString("MM/dd/yyyy") + " to " + toDate.Value.ToString("MM/dd/yyyy");
                }
                else
                {
                    txtheader.Text = "Weekly Installation Report ";
                }
                rprt.SetDataSource(dtMainReport);
                rprt.Subreports[0].SetDataSource(dt1);
                rptProjectInstallationStatusReport.ReportSource = rprt;
                rptProjectInstallationStatusReport.DataBind();
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Weekly Installation Report ";
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

}