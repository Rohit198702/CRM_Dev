using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BOLAERO;
using BLLAERO;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

public partial class SheetMetalForecasting_FrmSheetMetalPOReport : System.Web.UI.Page
{
    BOLSheetMetal ObjBOL = new BOLSheetMetal();
    BLLSheetMetal ObjBLL = new BLLSheetMetal();
    commonclass1 clscon = new commonclass1();
    ReportDocument rprt = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindControls();
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
            ObjBOL.Operation = 1;
            ds = ObjBLL.Return_SheetMetalPOReportDataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlSheetMetalID, ds.Tables[0]);
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlGauge, ds.Tables[1]);
            }            
            if (ds.Tables[2].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlWarehouse, ds.Tables[2]);
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlPONumber, ds.Tables[3]);
            }

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
            if (txtOrderDateFrom.Text != "")
            {
                if (txtOrderDateTo.Text == "")
                {                    
                    Utility.ShowMessage_Error(Page, "Please Enter Order Date To. !!");
                    txtOrderDateTo.Focus();
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }


    private void ReportData()
    {
        try
        {
            if(ValidationCheck() == true)
            {
                string headerText = String.Empty;                
                if (ddlPONumber.Items.Count > 0)
                {
                    if (ddlPONumber.SelectedIndex > 0)
                    {
                        headerText = ddlPONumber.SelectedItem.Text;
                        if (txtOrderDateFrom.Text != "" && txtOrderDateTo.Text != "")
                        {
                            headerText += " From " + txtOrderDateFrom.Text + " to " + txtOrderDateTo.Text;
                        }
                    }
                }
                else
                {
                    headerText = "PO Order Report ";
                    if (txtOrderDateFrom.Text != "" && txtOrderDateTo.Text != "")
                    {
                        headerText += "From " + txtOrderDateFrom.Text + " to " + txtOrderDateTo.Text;
                    }
                }
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
                ObjBOL.Operation = 2;               
                ObjBOL.id =Convert.ToInt32(ddlSheetMetalID.SelectedValue);
                ObjBOL.gauge = Convert.ToInt32(ddlGauge.SelectedValue);
                ObjBOL.warehouseid = Convert.ToInt32(ddlWarehouse.SelectedValue);
                ObjBOL.POId = Convert.ToInt32(ddlPONumber.SelectedValue);
                if(txtOrderDateFrom.Text.Trim() != "")
                {
                    ObjBOL.OrderDateFrom = Convert.ToDateTime(txtOrderDateFrom.Text);
                }
                if(txtOrderDateTo.Text.Trim() != "")
                {
                    ObjBOL.OrderDateTo = Convert.ToDateTime(txtOrderDateTo.Text);
                }
                ds = ObjBLL.Return_SheetMetalPOReportDataSet(ObjBOL);
                dt = ds.Tables[0];
                rprt.Load(Server.MapPath("~/SheetMetalForecasting/rptSheetMetalPOReport.rpt"));
                if (dt.Rows.Count > 0)
                {
                    TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                    txtheader.Text = headerText;
                    rprt.SetDataSource(dt);
                    rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
                }
                else
                {
                    //Utility.ShowMessage_Error(Page, "No Matching Data Found !!");
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
            if (ddlSheetMetalID.Items.Count > 0)
            {
                ddlSheetMetalID.SelectedIndex = 0;
            }
            if (ddlWarehouse.Items.Count > 0)
            {
                ddlWarehouse.SelectedIndex = 0;
            }
            if (ddlGauge.Items.Count > 0)
            {
                ddlGauge.SelectedIndex = 0;
            }
            if (ddlPONumber.Items.Count > 0)
            {
                ddlPONumber.SelectedIndex = 0;
            }
            txtOrderDateFrom.Text = String.Empty;
            txtOrderDateTo.Text = String.Empty;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            ReportData();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }    

    protected void btnExporttoExcel_Click(object sender, EventArgs e)
    {
        try
        {
            ReportData();            
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }        
}