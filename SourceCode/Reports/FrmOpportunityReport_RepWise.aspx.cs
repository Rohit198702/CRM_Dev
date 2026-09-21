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

public partial class Reports_FrmOpportunityReport_RepWise : System.Web.UI.Page
{
    BOLOpportunitiesReport ObjBOL = new BOLOpportunitiesReport();
    BLLOpportunitiesReport ObjBLL = new BLLOpportunitiesReport();
    ReportDocument rprt = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetDates();
            Bind_Controls();
        }
    }

    private void SetDates()
    {
        try
        {
            txtFromDate.Text = "01/01/" + DateTime.Now.Year;
            txtToDate.Text = "12/31/" + DateTime.Now.Year;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void Bind_Controls()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 2;
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlDestRep, ds.Tables[0]);
                if (ddlDestRep.Items.Count > 0)
                {
                    ddlDestRep.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private bool ValidateCheck()
    {
        try
        {
            if (txtFromDate.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter From Date !");
                txtFromDate.Focus();
                return false;
            }

            if (txtToDate.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter To Date !");
                txtToDate.Focus();
                return false;
            }
        }
        catch (Exception ex)
        {

            Utility.AddEditException(ex);
        }
        return true;
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateCheck() == true)
            {
                DataSet ds = new DataSet();
                ObjBOL.Operation = 3;
                ObjBOL.FromDate = Utility.ConvertDate(txtFromDate.Text);
                ObjBOL.ToDate = Utility.ConvertDate(txtToDate.Text);
                string headerText = "All TSM - Opportunities Report " + DateTime.Now.Year;
                if (ddlDestRep.SelectedIndex > 0)
                {
                    ObjBOL.DestRep = Convert.ToInt32(ddlDestRep.SelectedValue);
                    headerText = ddlDestRep.SelectedItem.Text + " - Opportunities Report " + DateTime.Now.Year;
                }
                ds = ObjBLL.Return_DataSet(ObjBOL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rprt.Load(Server.MapPath("~/Reports/rptOpportunities_Repwise.rpt"));                   
                    rprt.SetDataSource(ds.Tables[0]);
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, headerText);
                }
                else
                {
                    rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                    TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                    txtheader.Text = headerText;
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, headerText);
                }
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
            if (ddlDestRep.Items.Count > 0)
            {
                ddlDestRep.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}