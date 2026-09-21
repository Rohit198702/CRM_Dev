using BLLAERO;
using BOLAERO;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_FrmBDMActivityReport : System.Web.UI.Page
{
    commonclass1 cls = new commonclass1();
    ReportDocument rprt = new ReportDocument();
    BOLBDMActivity ObjBOL = new BOLBDMActivity();
    BLLBDMActivity ObjBLL = new BLLBDMActivity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetDates();
            BindControls();
            GetPermission();
        }
    }

    private void GetPermission()
    {
        try
        {
            if (!Utility.IsAuthorized())
            {
                return;
            }

            ObjBOL.Operation = 28;
            ObjBOL.Id = Utility.GetCurrentUser();

            string returnStatus = ObjBLL.Return_String(ObjBOL);

            if (returnStatus.Trim() == "S01")
            {
                ddlBDM.SelectedIndex = 0;
                ddlBDM.Enabled = true;
            }
            else if (returnStatus.Trim() == "S02")
            {
                if (ddlBDM.Items.FindByValue(Utility.GetCurrentUser().ToString()) != null)
                {
                    ddlBDM.SelectedValue = Utility.GetCurrentUser().ToString();
                    ddlBDM.Enabled = false;
                }
            }
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
            DataSet ds = new DataSet();
            cls.Return_DS(ds, "EXEC Get_BDMActivityReport 1");

            int index = 0;
            if (ds.Tables[index].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlBDM, ds.Tables[index]);
            }

            index = 1;
            if (ds.Tables[index].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlActivityType, ds.Tables[index]);
            }

            index = 2;
            if (ds.Tables[index].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlStatus, ds.Tables[index]);
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
            txtActivityDateFrom.Text = DateTime.Now.Month + "/01/" + DateTime.Now.Year;
            txtActivityDateTo.Text = DateTime.Now.AddMonths(0).Month + "/" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(0).Month) + "/" + DateTime.Now.AddMonths(0).Year;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlBDM.Enabled == true)
            {
                if (ddlBDM.Items.Count > 0)
                {
                    ddlBDM.SelectedIndex = 0;
                }
            }

            if (ddlActivityType.Items.Count > 0)
            {
                ddlActivityType.SelectedIndex = 0;
            }

            if (ddlStatus.Items.Count > 0)
            {
                ddlStatus.SelectedIndex = 0;
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
        PrepareReport(CrystalDecisions.Shared.ExportFormatType.Excel);
    }

    private DataSet ReportData()
    {
        DataSet ds = new DataSet();
        try
        {
            string query = "EXEC [dbo].[Get_BDMActivityReport] 2, ";

            if (ddlBDM.SelectedIndex > 0)
            {
                query += ddlBDM.SelectedValue + ", ";
            }
            else
            {
                query += "0, ";
            }

            if (ddlActivityType.SelectedIndex > 0)
            {
                query += ddlActivityType.SelectedValue + ", ";
            }
            else
            {
                query += "0, ";
            }

            if (txtActivityDateFrom.Text.Trim() != "" && txtActivityDateTo.Text.Trim() != "")
            {
                query += " '" + txtActivityDateFrom.Text + "', '" + txtActivityDateTo.Text + "', ";
            }
            else
            {
                query += " '','', ";
            }

            if (ddlStatus.SelectedIndex > 0)
            {
                query += ddlStatus.SelectedValue + " ";
            }
            else
            {
                query += "0 ";
            }

            cls.Return_DS(ds, query);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return ds;
    }

    private void PrepareReport(CrystalDecisions.Shared.ExportFormatType exportType)
    {
        try
        {
            string HeaderText = string.Empty;
            DataSet ds = ReportData();
            var reportType = exportType;
            rprt.Load(Server.MapPath("~/Reports/rptBDMActivityReport.rpt"));
            if (ds.Tables[0].Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = " Activity List From " + txtActivityDateFrom.Text + " to " + txtActivityDateTo.Text;
                string v = rprt.Subreports[0].Name;
                rprt.SetDataSource(ds.Tables[0]);
                rprt.Subreports["rptBDMActivityReport_Participant.rpt"].SetDataSource(ds.Tables[1]);
                rprt.Subreports["rptBDMActivityReport_Spec.rpt"].SetDataSource(ds.Tables[2]);
                rprt.Subreports["rptBDMActivityReport_Followup.rpt"].SetDataSource(ds.Tables[3]);
                rptBDMActivityReport.ReportSource = rprt;
                rptBDMActivityReport.DataBind();
                rprt.ExportToHttpResponse(reportType, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = " Activity List From " + txtActivityDateFrom.Text + " to " + txtActivityDateTo.Text;
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

    protected void btnExportToPDF_Click(object sender, EventArgs e)
    {
        PrepareReport(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
    }
}