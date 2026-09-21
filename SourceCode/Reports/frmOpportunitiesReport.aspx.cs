using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BOLAERO;
using BLLAERO;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

public partial class Reports_frmOpportunitiesReport : System.Web.UI.Page
{
    BOLOpportunitiesReport ObjBOL = new BOLOpportunitiesReport();
    BLLOpportunitiesReport ObjBLL = new BLLOpportunitiesReport();
    ReportDocument rprt = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_Controls();
            SetDates();
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

    private void SetDates()
    {
        try
        {
            int year = DateTime.Now.Year;
            DateTime startDate = new DateTime(year, 1, 1);
            DateTime endDate = new DateTime(year, 12, 31);
            txtFromDate.Text = startDate.ToString("MM/dd/yyyy");
            txtToDate.Text = endDate.ToString("MM/dd/yyyy");
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
            if(txtToDate.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter To Date !");
                txtToDate.Focus();
                return false;
            }
            if(txtFromDate.Text == "" && txtToDate.Text != "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter From Date !");
                txtFromDate.Focus();
                return false;
            }
            if (txtFromDate.Text != "" && txtToDate.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter To Date !");
                txtFromDate.Focus();
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
                string headerText = "Opportunities Report from " + txtFromDate.Text + " to " + txtToDate.Text;
                ObjBOL.Operation = 1;
                ObjBOL.FromDate =Utility.ConvertDate(txtFromDate.Text);
                ObjBOL.ToDate = Utility.ConvertDate(txtToDate.Text);
                ObjBOL.DestRep = Convert.ToInt32(ddlDestRep.SelectedValue);
                ds = ObjBLL.Return_DataSet(ObjBOL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    rprt.Load(Server.MapPath("~/Reports/rptOpportunities.rpt"));
                    TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                    txtheader.Text = headerText;
                    rprt.SetDataSource(ds.Tables[0]);
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
                }
                else
                {
                    rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                    TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                    txtheader.Text = headerText;
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
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