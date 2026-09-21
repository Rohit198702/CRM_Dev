using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Web.UI.WebControls;
using BOLAERO;
using BLLAERO;
public partial class SheetMetalForecasting_FrmSheetMetalStockAdjustment : System.Web.UI.Page
{
    BOLSheetMetal ObjBOL = new BOLSheetMetal();
    BLLSheetMetal ObjBLL = new BLLSheetMetal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_Control();
            AutoBindWarehouse();
        }
    }

    private void AutoBindWarehouse()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 24;
            ObjBOL.LoginUserID = Utility.GetCurrentUser();
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlWarehouse, ds.Tables[0]);
                switch (ObjBOL.LoginUserID)
                {
                    case 340:
                        ddlWarehouse.SelectedValue = "2";
                        ddlWarehouse.Enabled = false;
                        break;

                    case 335:
                        ddlWarehouse.SelectedValue = "3";
                        ddlWarehouse.Enabled = false;
                        break;

                    default:
                        ddlWarehouse.Enabled = true;
                        if (ddlWarehouse.Items.Count > 0)
                        {
                            ddlWarehouse.SelectedIndex = 0;
                        }
                        break;
                }
            }
            else
            {
                ddlWarehouse.Enabled = true;
                if (ddlWarehouse.Items.Count > 0)
                {
                    ddlWarehouse.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void Bind_Control()
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
                Utility.BindDropDownList(ddlLookupSheetMetal, ds.Tables[5]);
            }
            if (ds.Tables[8].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlAdjReason, ds.Tables[8]);
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
            if (ddlLookupSheetMetal.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Sheet Metal ID!");
                ddlWarehouse.Focus();
                return false;
            }
            if (ddlWarehouse.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Warehouse!");
                ddlWarehouse.Focus();
                return false;
            }
            if (ddlAdjustmentType.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Adjustment Type!");
                ddlWarehouse.Focus();
                return false;
            }
            if (ddlAdjReason.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Adjustment Reason!");
                ddlAdjReason.Focus();
                return false;
            }
            if (txtQuantity.Text.Trim() == "" || txtQuantity.Text.Trim() == "0")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Quantity!");
                txtQuantity.Focus();
                return false;
            }
            if (txtSummary.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Summary!");
                txtSummary.Focus();
                return false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    private void StockAdjustment()
    {
        try
        {
            if (ValidationCheck() == true)
            {
                string msg = String.Empty;
                ObjBOL.Operation = 7;                
                if (ddlAdjustmentType.SelectedValue == "1")
                {
                    ObjBOL.quantity = Convert.ToInt32(txtQuantity.Text);
                }
                //2 = out
                else if (ddlAdjustmentType.SelectedValue == "2")
                {
                    ObjBOL.quantity = -Math.Abs(Convert.ToInt32(txtQuantity.Text));
                }
                ObjBOL.warehouseid = Convert.ToInt32(ddlWarehouse.SelectedValue);
                ObjBOL.id = Convert.ToInt32(ddlLookupSheetMetal.SelectedValue);
                ObjBOL.AdjustmentReasonID = Convert.ToInt32(ddlAdjReason.SelectedValue);
                ObjBOL.TransactSummary = txtSummary.Text.Trim();               
                if (Utility.IsAuthorized())
                {
                    ObjBOL.LoginUserID = Utility.GetCurrentUser();
                }
                msg = ObjBLL.Return_String(ObjBOL);
                if (msg.Trim() == "S")
                {
                    Utility.ShowMessage_Success(Page, "Sheet Metal Quatntity Stock In Successfully!");
                    Utility.MaintainLogsSpecial("FrmSheetMetalStockAdjustment.aspx", "Stock In", ddlLookupSheetMetal.SelectedValue.ToString());
                }
                else if(msg.Trim() == "R")
                {
                    Utility.ShowMessage_Success(Page, "Sheet Metal Quatntity Stock Out Successfully!");
                    Utility.MaintainLogsSpecial("FrmSheetMetalStockAdjustment.aspx", "Stock-Out", ddlLookupSheetMetal.SelectedValue.ToString());
                }
                ResetPartsInfo();
                Bind_Grid();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    //STcok In
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            StockAdjustment();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    

    private void ResetPartsInfo()
    {
        try
        {
            AutoBindWarehouse();
            if (ddlAdjustmentType.Items.Count > 0)
            {
                ddlAdjustmentType.SelectedIndex = 0;
            }
            if (ddlAdjReason.Items.Count > 0)
            {
                ddlAdjReason.SelectedIndex = 0;
            }        
            txtQuantity.Text = String.Empty;
            txtSummary.Text = String.Empty;
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
            if (ddlLookupSheetMetal.Items.Count > 0)
            {
                ddlLookupSheetMetal.SelectedIndex = 0;
            }
            txtGauge.Text = String.Empty;
            txtLength.Text = String.Empty;
            txtWidth.Text = String.Empty;
            ResetPartsInfo();
            ResetGrid();
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }        
    }

    private void Bind_Grid()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 8;
            if (ddlWarehouse.Items.Count > 0)
            {
                ObjBOL.id = Convert.ToInt32(ddlLookupSheetMetal.SelectedValue);
            }
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                pangvRequititionDetails.Visible = true;
                gvSheetMetalStockIn.DataSource = ds.Tables[0];
                gvSheetMetalStockIn.DataBind();
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

    private void ResetGrid()
    {
        try
        {
            pangvRequititionDetails.Visible = false;
            gvSheetMetalStockIn.DataSource = "";
            gvSheetMetalStockIn.DataBind();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }  

    private void BindSheetMetalDetails()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 5;
            if (ddlLookupSheetMetal.Items.Count > 0)
            {
                ObjBOL.id = Convert.ToInt32(ddlLookupSheetMetal.SelectedValue);
            }
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtGauge.Text = ds.Tables[0].Rows[0]["gaugesize"].ToString();
                txtLength.Text = ds.Tables[0].Rows[0]["lengthsize"].ToString();
                txtWidth.Text = ds.Tables[0].Rows[0]["widthsize"].ToString();
            }
            else
            {
                ResetPartsInfo();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlLookupSheetMetal_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlLookupSheetMetal.SelectedIndex > 0)
            {

            }
            else
            {
                txtGauge.Text = String.Empty;
                txtLength.Text = String.Empty;
                txtWidth.Text = String.Empty;
            }
            BindSheetMetalDetails();
            Bind_Grid();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}