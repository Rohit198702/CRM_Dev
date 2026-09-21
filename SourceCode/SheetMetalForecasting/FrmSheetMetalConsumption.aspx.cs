using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Web.UI.WebControls;
using BOLAERO;
using BLLAERO;
public partial class SheetMetalForecasting_FrmSheetMetalConsumption : System.Web.UI.Page
{
    BOLSheetMetal ObjBOL = new BOLSheetMetal();
    BLLSheetMetal ObjBLL = new BLLSheetMetal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Utility.IsAuthorized())
            {
                Bind_Control();
                AutoBindWarehouse();                
            }
                
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
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlLookupProductCode, ds.Tables[0]);
                Utility.BindDropDownList(ddlProductCode, ds.Tables[0]);
            }    
            //if (ds.Tables[4].Rows.Count > 0)
            //{                
            //    Utility.BindDropDownList(ddlWarehouse, ds.Tables[4]);
            //}
            if (ds.Tables[5].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlSheetMetalNo, ds.Tables[5]);
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
            //if (ddlProductCode.SelectedIndex == 0)
            //{
            //    Utility.ShowMessage_Error(Page, "Please Select Product Code!");
            //    ddlProductCode.Focus();
            //    return false;
            //}
            if (ddlSheetMetalNo.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Sheet Metal ID!");
                ddlSheetMetalNo.Focus();
                return false;
            }
            if (ddlWarehouse.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Warehouse!");
                ddlWarehouse.Focus();
                return false;
            }
            //if (txtDate.Text.Trim() == "")
            //{
            //    Utility.ShowMessage_Error(Page, "Please Enter Date!");
            //    txtDate.Focus();
            //    return false;
            //}
            if (txtQuantity.Text.Trim() == "" || txtQuantity.Text.Trim() == "0")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Transact Quantity!");
                txtQuantity.Focus();
                return false;
            }            
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    private void StockTransactions()
    {
        try
        {
            if (ValidationCheck() == true)
            {
                string msg = String.Empty;         
                if (btnSave.Text == "Save")
                {
                    ObjBOL.Operation = 20;
                }
                else
                {
                    ObjBOL.Operation = 21;
                    if(hfStockOutID.Value != "-1")
                    {
                        ObjBOL.StockOutID = Convert.ToInt32(hfStockOutID.Value);
                    }                   
                   
                }
                ObjBOL.productcodeid = Convert.ToInt32(ddlProductCode.SelectedValue);
                if (ddlWarehouse.Items.Count > 0)
                {
                    ObjBOL.warehouseid = Convert.ToInt32(ddlWarehouse.SelectedValue);
                }                
                ObjBOL.id = Convert.ToInt32(ddlSheetMetalNo.SelectedValue);
                //ObjBOL.date = Convert.ToDateTime(txtDate.Text.Trim());
                ObjBOL.TransactQuantity =-Math.Abs(Convert.ToInt32(txtQuantity.Text.Trim()));
                if (Utility.IsAuthorized())
                {
                    ObjBOL.LoginUserID = Utility.GetCurrentUser();
                }
                msg = ObjBLL.Return_String(ObjBOL);
                if(msg.Trim() == "ER")
                {
                    Utility.ShowMessage_Error(Page, "Already Stock Out!");
                    return;
                }
                if(msg.Trim() == "ER01")
                {
                    Utility.ShowMessage_Error(Page, "Record Already Exists!");
                    return;
                }
                if (msg.Trim() == "U")
                {
                    Utility.ShowMessage_Success(Page, "Stock Quantity Updated Successfully!");
                    Utility.MaintainLogsSpecial("FrmSheetMetalConsumption", "Update", ddlSheetMetalNo.SelectedValue.ToString());
                }
                else
                {
                    Utility.ShowMessage_Success(Page, "Stock Quantity Added Successfully!");
                    Utility.MaintainLogsSpecial("FrmSheetMetalConsumption", "Add", ddlSheetMetalNo.SelectedValue.ToString());
                }           
                ResetPartsInfo();
                Bind_Grid(ObjBOL.id.ToString(), ObjBOL.warehouseid.ToString());
                btnSave.Text = "Save";
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
            StockOutTransaction();
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
            if (ddlSheetMetalNo.Items.Count > 0)
            {
                ddlSheetMetalNo.SelectedIndex = 0;
            }
            txtGauge.Text = String.Empty;
            txtLength.Text = String.Empty;
            txtWidth.Text = String.Empty;
            //txtDate.Text = DateTime.Now.ToString("MM/dd/yyyy");
            txtQuantity.Text = String.Empty;
            txtcurrentStock.Text = String.Empty;
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
            if (ddlLookupProductCode.Items.Count > 0)
            {
                ddlLookupProductCode.SelectedIndex = 0;
            }
            if (ddlProductCode.Items.Count > 0)
            {
                ddlProductCode.SelectedIndex = 0;
            }
            ResetPartsInfo();
            ResetGrid();
            AutoBindWarehouse();
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }        
    }

    private void Bind_Grid(string sheetmetalid, string warehouseid)
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 11;
            ObjBOL.LoginUserID = Utility.GetCurrentUser();
            if (sheetmetalid != "")
            {
                ObjBOL.id = Convert.ToInt32(sheetmetalid);
            }
            if(ObjBOL.LoginUserID.ToString() != "263")
            {
                if (warehouseid != "")
                {
                    ObjBOL.warehouseid = Convert.ToInt32(warehouseid);
                }
            }
            else
            {
                ObjBOL.warehouseid = 0;
            }
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                pangvRequititionDetails.Visible = true;
                gvSheetMetalConsumptionHistory.DataSource = ds.Tables[0];
                gvSheetMetalConsumptionHistory.DataBind();
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
            gvSheetMetalConsumptionHistory.DataSource = "";
            gvSheetMetalConsumptionHistory.DataBind();
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
            ObjBOL.Operation = 25;
            if (ddlSheetMetalNo.Items.Count > 0)
            {
                ObjBOL.id = Convert.ToInt32(ddlSheetMetalNo.SelectedValue);
            }
            if (ddlWarehouse.Items.Count > 0)
            {
                ObjBOL.warehouseid = Convert.ToInt32(ddlWarehouse.SelectedValue);
            }
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtcurrentStock.Text = ds.Tables[0].Rows[0]["currentstock"].ToString();
            }
            else
            {
                txtcurrentStock.Text = String.Empty;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void BindCurrentStockToolTip(string sheetmetalid, string warehouseid)
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 5;
            if (ddlSheetMetalNo.Items.Count > 0)
            {
                ObjBOL.id = Convert.ToInt32(sheetmetalid);
            }
            if (ddlWarehouse.Items.Count > 0)
            {
                ObjBOL.warehouseid = Convert.ToInt32(warehouseid);
            }
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["CurrentStockToolTip"].ToString() != null)
                {
                    txtcurrentStock.ToolTip = ds.Tables[0].Rows[0]["CurrentStockToolTip"].ToString();
                }
                else
                {
                    txtcurrentStock.ToolTip = String.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvSheetMetalConsumptionHistory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string msg = "";
            int ID = Convert.ToInt32(gvSheetMetalConsumptionHistory.DataKeys[e.RowIndex].Values[1]);
            string SheetMetalID = gvSheetMetalConsumptionHistory.DataKeys[e.RowIndex].Values[2].ToString();
            string WarehouseID = gvSheetMetalConsumptionHistory.DataKeys[e.RowIndex].Values[3].ToString();
            ObjBOL.Operation = 23;
            ObjBOL.StockOutID = ID;
            msg = ObjBLL.Return_String(ObjBOL);
            if (msg.Trim() == "ER")
            {
                Utility.ShowMessage_Error(Page, "Already Stock Out!");
            }
            if (msg.Trim() == "D")
            {
                Utility.ShowMessage_Success(Page, "Record Deleted Successfully !");
                Utility.MaintainLogsSpecial("FrmSheetMetalConsumption", "Delete", SheetMetalID.ToString());
            }
            Bind_Grid(SheetMetalID, WarehouseID);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlSheetMetalNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSheetMetalNo.SelectedIndex > 0)
            {
                BindSheetMetalDetails();
                if(ddlSheetMetalNo.Items.Count>0 && ddlWarehouse.Items.Count > 0)
                {
                    Bind_Grid(ddlSheetMetalNo.SelectedValue, ddlWarehouse.SelectedValue);
                }
                
            }
            else
            {
                txtGauge.Text = String.Empty;
                txtLength.Text = String.Empty;
                txtWidth.Text = String.Empty;
                txtcurrentStock.Text = String.Empty;
                ResetPartsInfo();
                ResetGrid();
                btnSave.Text = "Save";
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }



    protected void ddlProductCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductCode.SelectedIndex > 0)
            {
                ddlLookupProductCode.SelectedValue = ddlProductCode.SelectedValue;
            }
                     
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void StockOutTransaction()
    {
        try
        {
            if (ValidationCheck() == true)
            {
                string msg = string.Empty;
                ObjBOL.Operation = 10;
                ObjBOL.productcodeid = Convert.ToInt32(ddlProductCode.SelectedValue);
                if (ddlWarehouse.Items.Count > 0)
                {
                    ObjBOL.warehouseid = Convert.ToInt32(ddlWarehouse.SelectedValue);
                }
                ObjBOL.id = Convert.ToInt32(ddlSheetMetalNo.SelectedValue);                
                ObjBOL.TransactQuantity = -Math.Abs(Convert.ToInt32(txtQuantity.Text.Trim()));
                if (Utility.IsAuthorized())
                {
                    ObjBOL.LoginUserID = Utility.GetCurrentUser();
                }
                ObjBOL.LoginUserID = Utility.GetCurrentUser();
                msg = ObjBLL.Return_String(ObjBOL);      
                if(msg.Trim() == "ER01")
                {
                    Utility.ShowMessage_Error(Page, "Record Already Exists !");
                    return;
                }         
                if (msg == "S")
                {
                    Utility.ShowMessage_Success(Page, "Stock-Out Successfully!");
                    Utility.MaintainLogsSpecial("FrmSheetMetalConsumption", "Stock-Out", ObjBOL.id.ToString());
                }
                ResetPartsInfo();
                Bind_Grid(ObjBOL.id.ToString(), ObjBOL.warehouseid.ToString());
                btnSave.Text = "Save";
            }
                
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvSheetMetalConsumptionHistory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "StockOut")
            {
                string msg = "";
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int ProductCodeID = Int32.Parse(gvSheetMetalConsumptionHistory.DataKeys[rowIndex].Values[0].ToString());
                int StockOutID = Int32.Parse(gvSheetMetalConsumptionHistory.DataKeys[rowIndex].Values[1].ToString());
                Label lblSheetMetalID = gvSheetMetalConsumptionHistory.Rows[rowIndex].FindControl("lblSheetMetalID") as Label;
                Label lblWareHouseID = gvSheetMetalConsumptionHistory.Rows[rowIndex].FindControl("lblWarehouseID") as Label;
                Label lblTransactQty = gvSheetMetalConsumptionHistory.Rows[rowIndex].FindControl("lblTransactQty") as Label;               

                ObjBOL.Operation = 10;
                if(StockOutID >0)
                {
                    hfStockOutID.Value = StockOutID.ToString();
                }
                if(ProductCodeID > 0)
                {
                    ObjBOL.productcodeid =Convert.ToInt32(ProductCodeID.ToString());
                }
                if(StockOutID > 0)
                {
                    ObjBOL.StockOutID = StockOutID;
                }
                if (lblSheetMetalID.Text != "")
                {
                    ObjBOL.id = Convert.ToInt32(lblSheetMetalID.Text);
                }
                if (lblWareHouseID.Text != "")
                {
                    ObjBOL.warehouseid = Convert.ToInt32(lblWareHouseID.Text);
                }
                if (lblTransactQty.Text != "")
                {
                    ObjBOL.TransactQuantity =-Math.Abs(Convert.ToInt32(lblTransactQty.Text));
                }
                ObjBOL.LoginUserID = Utility.GetCurrentUser();              
                msg = ObjBLL.Return_String(ObjBOL);   
                if (msg.Trim() == "ER")
                {
                    Utility.ShowMessage_Error(Page, "Already Stock-Out!");
                    return;
                }
                if (msg == "S")
                {
                    Utility.ShowMessage_Success(Page, "Stock-Out Successfully!");
                    Utility.MaintainLogsSpecial("FrmSheetMetalConsumption", "Stock-Out", ddlLookupProductCode.SelectedValue.ToString());
                }
                Bind_Grid(ObjBOL.id.ToString(), ObjBOL.warehouseid.ToString());
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvSheetMetalConsumptionHistory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            int StockConsumptionID = Convert.ToInt32(gvSheetMetalConsumptionHistory.DataKeys[e.NewEditIndex].Values[1]);
            if (StockConsumptionID > 0)
            {
                hfStockOutID.Value = StockConsumptionID.ToString();
            }
            ObjBOL.Operation = 22;
            ObjBOL.StockOutID = StockConsumptionID;
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ddlProductCode.Items.FindByValue(ds.Tables[0].Rows[0]["ProductCodeID"].ToString()) != null)
                {
                    ddlProductCode.SelectedValue = ds.Tables[0].Rows[0]["ProductCodeID"].ToString();
                }                
                if (ddlWarehouse.Items.FindByValue(ds.Tables[0].Rows[0]["WarehouseID"].ToString()) != null)
                {
                    ddlWarehouse.SelectedValue = ds.Tables[0].Rows[0]["WarehouseID"].ToString();
                }
                if(ddlSheetMetalNo.Items.FindByValue(ds.Tables[0].Rows[0]["SheetMetalID"].ToString()) != null)
                {
                    ddlSheetMetalNo.SelectedValue = ds.Tables[0].Rows[0]["SheetMetalID"].ToString();
                }
                txtGauge.Text = ds.Tables[0].Rows[0]["Gauge"].ToString();
                txtLength.Text = ds.Tables[0].Rows[0]["Length"].ToString();
                txtWidth.Text = ds.Tables[0].Rows[0]["Width"].ToString();               
                //txtcurrentStock.Text = ds.Tables[0].Rows[0]["currentstock"].ToString();
                //txtDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Date"]).ToString("MM/dd/yyyy");
                txtQuantity.Text = Math.Abs(Convert.ToDecimal(ds.Tables[0].Rows[0]["TransactQty"])).ToString();
                //if(ddlWarehouse.Items.Count>0 && ddlSheetMetalNo.Items.Count > 0)
                //{
                //    BindCurrentStockToolTip(ds.Tables[0].Rows[0]["SheetMetalID"].ToString(), ds.Tables[0].Rows[0]["WarehouseID"].ToString());
                //}         
                BindSheetMetalDetails();
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    protected void btnMaster_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/SheetMetalForecasting/FrmSheetMetal.aspx", false);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }


    protected void btnStockIn_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/SheetMetalForecasting/FrmSheetMetalStockIn.aspx", false);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSheetMetalNo.Items.Count>0)
            {
                BindSheetMetalDetails();
            }
            
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}