using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_FrmApprovedDwgAndFollowupReport : System.Web.UI.Page
{
    commonclass1 cls = new commonclass1();
    ReportDocument rprt = new ReportDocument();

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
            txtDateCalledFrom.Text = "01/01/" + DateTime.Now.Year;
            txtDateCalledTo.Text = DateTime.Now.ToShortDateString();
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
            cls.Return_DS(ds, "EXEC Get_ApprovedDwgAndFollowupReport 1");

            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlProjectManager, ds.Tables[0]);
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlFollowupBy, ds.Tables[2]);
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlStatus, ds.Tables[1]);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    #region Validation

    private bool IsNullOrWhiteSpace(string value)
    {
        return value == null || value.Trim().Length == 0;
    }

    private bool ValidationCheck()
    {
        try
        {
            if ((!IsNullOrWhiteSpace(txtDateCalledFrom.Text) && IsNullOrWhiteSpace(txtDateCalledTo.Text)) ||
            (IsNullOrWhiteSpace(txtDateCalledFrom.Text) && !IsNullOrWhiteSpace(txtDateCalledTo.Text)))
            {
                Utility.ShowMessage_Error(Page, "Please Select Both From And To Date Called !");
                return false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    #endregion

    private DataTable ReportData()
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "EXEC Get_ApprovedDwgAndFollowupReport 2, '" + txtDateCalledFrom.Text + "', '" + txtDateCalledTo.Text + "', "
                + ddlProjectManager.SelectedValue + ", " + ddlStatus.SelectedValue + ", " + ddlFollowupBy.SelectedValue;
            if (query != "")
            {
                cls.Return_DT(dt, query);
            }
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
            if (ValidationCheck())
            {
                string header = "Approved Dwg and Followups ";
                if (txtDateCalledFrom.Text != "" && txtDateCalledTo.Text != "")
                {
                    header += " From " + txtDateCalledFrom.Text + " to " + txtDateCalledTo.Text;
                }
                DataTable dt = ReportData();
                rprt.Load(Server.MapPath("~/Reports/rptApprovedDwgAndFollowup.rpt"));
                if (dt.Rows.Count > 0)
                {
                    TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                    txtheader.Text = header;
                    rprt.SetDataSource(dt);
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
                }
                else
                {
                    rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                    TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                    txtheader.Text = header;
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

    protected void btnGenerateExcel_Click(object sender, EventArgs e)
    {
        try
        {

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
            SetDates();
            if (ddlProjectManager.SelectedIndex > 0)
            {
                ddlProjectManager.SelectedIndex = 0;
            }

            if (ddlFollowupBy.SelectedIndex > 0)
            {
                ddlFollowupBy.SelectedIndex = 0;
            }

            if (ddlStatus.SelectedIndex > 0)
            {
                ddlStatus.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}