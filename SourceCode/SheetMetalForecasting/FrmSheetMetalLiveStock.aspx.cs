using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BOLAERO;
using BLLAERO;
using System.Data;

public partial class SheetMetalForecasting_FrmSheetMetalLiveStock : System.Web.UI.Page
{
    BOLSheetMetal ObjBOL = new BOLSheetMetal();
    BLLSheetMetal ObjBLL = new BLLSheetMetal();
    commonclass1 clscon = new commonclass1();
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
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[4].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlWarehouse, ds.Tables[4]);
            }
            if (ds.Tables[5].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlSheetMetalID, ds.Tables[5]);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private bool ValidationCheck()
    {
        try
        {
            if (ddlSheetMetalID.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Sheet Metal ID !");
                ddlSheetMetalID.Focus();
                return false;
            }
            if (ddlWarehouse.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Warehouse !");
                ddlWarehouse.Focus();
                return false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    private void ResetGrid()
    {
        try
        {
            gvJobSheetLiveStock.DataSource = "";
            gvJobSheetLiveStock.DataBind();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    } 

    private void ReportData()
    {
        try
        {
            string msg = String.Empty;
            DataSet ds = new DataSet();
            if (ddlSheetMetalID.Items.Count > 0)
            {
                ObjBOL.id = Convert.ToInt32(ddlSheetMetalID.SelectedValue);
            }
            if (ddlWarehouse.Items.Count > 0)
            {
                ObjBOL.warehouseid = Convert.ToInt32(ddlWarehouse.SelectedValue);
            }
            ds = ObjBLL.Return_LiveReportDataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Columns.Contains("ErrorMessage"))
                {
                    msg = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    if (msg.Trim() == "ER01")
                    {
                        Utility.ShowMessage_Error(Page, "From Date cannot be greater than To Date !");
                        return;
                    }
                    if (msg.Trim() == "ER02")
                    {
                        Utility.ShowMessage_Error(Page, "From Date and To Date must be within the same month !");
                        return;
                    }
                }               
                gvJobSheetLiveStock.DataSource = ds.Tables[0];
                gvJobSheetLiveStock.DataBind();
            }            
            else
            {
                ResetGrid();
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
            if (ddlSheetMetalID.Items.Count > 0)
            {
                ddlSheetMetalID.SelectedIndex = 0;
            }
            if (ddlWarehouse.Items.Count > 0)
            {
                ddlWarehouse.SelectedIndex = 0;
            }
            ResetGrid();
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
            if(ValidationCheck() == true)
            {
                ReportData();
            }            
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
            if (ValidationCheck() == true)
            {
                ReportData();
                Utility.ExportToExcelGrid(gvJobSheetLiveStock, "Sheet Forecasting Live Stock");
            }
            
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    
}