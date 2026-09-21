using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Web.UI.WebControls;
using BOLAERO;
using BLLAERO;
using System.Drawing;

public partial class SheetMetalForecasting_FrmSheetMetalStockIn : System.Web.UI.Page
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
                Utility.BindDropDownList(ddlSheetMetalID, ds.Tables[5]);
            }
            if (ds.Tables[6].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlLookupPONumber, ds.Tables[6]);
            }
            if (ds.Tables[7].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlDeliveryStatus, ds.Tables[7]);
                if (ddlDeliveryStatus.Items.Count > 0)
                {
                    ddlDeliveryStatus.SelectedValue = "1";
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
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

    private Boolean ValidationCheck()
    {
        try
        {
            if (txtPONumber.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter PO Number!");
                txtPONumber.Focus();
                return false;
            }
            if (ddlWarehouse.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Warehouse!");
                ddlWarehouse.Focus();
                return false;
            }
            if (ddlSheetMetalID.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Sheet Metal ID!");
                ddlWarehouse.Focus();
                return false;
            }

            if (txtDeliveryDate.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Order Date!");
                txtDeliveryDate.Focus();
                return false;
            }
            if (txtExpectedArrivalDate.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Expected Arrival Date!");
                txtExpectedArrivalDate.Focus();
                return false;
            }
            if (txtDelQty.Text.Trim() == "" || txtDelQty.Text.Trim() == "0")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Order Quantity!");
                txtDelQty.Focus();
                return false;
            }
            if (ddlDeliveryStatus.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Status!");
                ddlDeliveryStatus.Focus();
                return false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    private void Save()
    {
        try
        {
            if (ValidationCheck() == true)
            {
                string msg = String.Empty;
                if (btnSave.Text=="Update")
                {
                    //Update
                    ObjBOL.Operation = 13;                    
                    ObjBOL.PODetailID = Convert.ToInt32(hfPODetailID.Value);                    
                }
                else
                {
                    //Save
                    ObjBOL.Operation = 12;                    
                }
                if (ddlLookupPONumber.Items.Count > 0)
                {
                    ObjBOL.POId = Convert.ToInt32(ddlLookupPONumber.SelectedValue);
                }
                
                if (txtPONumber.Text.Trim() != "")
                {
                    ObjBOL.ReceiptNo = txtPONumber.Text.Trim();
                }
                if (ddlWarehouse.SelectedIndex > 0)
                {
                    ObjBOL.warehouseid =Convert.ToInt32(ddlWarehouse.SelectedValue);
                }
                if (ddlSheetMetalID.Items.Count > 0)
                {
                    ObjBOL.id = Convert.ToInt32(ddlSheetMetalID.SelectedValue);
                }
                if(txtDeliveryDate.Text.Trim() != "")
                {
                    ObjBOL.DeliveryDate = Convert.ToDateTime(txtDeliveryDate.Text.Trim());
                }
                if(txtExpectedArrivalDate.Text.Trim() != "")
                {
                    ObjBOL.ExpectedArrivalDate = Convert.ToDateTime(txtExpectedArrivalDate.Text.Trim());
                }
                if(txtDelQty.Text.Trim() != "")
                {
                    ObjBOL.DeliveryQuantity = Convert.ToInt32(txtDelQty.Text.Trim());
                }
                if (ddlDeliveryStatus.SelectedIndex > 0)
                {
                    ObjBOL.DeliveryStatus = Convert.ToInt32(ddlDeliveryStatus.SelectedValue);
                }
                if(txtCurrentStock.Text.Trim() != "")
                {
                    ObjBOL.currentstock = Convert.ToInt32(txtCurrentStock.Text.Trim());
                }
                msg = ObjBLL.Return_String(ObjBOL);

                if (msg.Trim() == "ER")
                {
                    Utility.ShowMessage_Error(Page, "Sheet Metal already Exists!");
                    return;
                }
                if (msg.Trim() == "ER02")
                {
                    Utility.ShowMessage_Error(Page, "Already Stock In!");
                    return;
                }
                if(msg.Trim() == "ER03")
                {
                    Utility.ShowMessage_Error(Page, "PO Number Already Submitted!");
                    return;
                }
                if (msg.Trim() == "U")
                {
                    BindPONumber(ddlLookupPONumber.SelectedValue);
                    txtPONumber.Enabled = false;
                    Utility.ShowMessage_Success(Page, "Stock Updated Successfully!");
                    Utility.MaintainLogsSpecial("FrmSheetMetalStockIn", "Update", ddlLookupPONumber.SelectedValue);
                }
                else
                {
                    BindPONumber(msg.ToString());
                    Utility.ShowMessage_Success(Page, "Stock Added Successfully!");
                    Utility.MaintainLogsSpecial("FrmSheetMetalStockIn", "Add", msg.Trim().ToString());
                }
                ResetPartsInfo();
                ResetCommonParts();
                btnSave.Text = "Save";
                txtPONumber.Enabled = false;
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
            Save();
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
            if (ddlSheetMetalID.Items.Count > 0)
            {
                ddlSheetMetalID.SelectedIndex = 0;
            }                                           
            txtDeliveryDate.Text = String.Empty;
            txtExpectedArrivalDate.Text = String.Empty;
            txtDelQty.Text = String.Empty;
            if (ddlDeliveryStatus.Items.Count > 0)
            {
                ddlDeliveryStatus.SelectedValue = "1";
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetCommonParts()
    {
        try
        {
            txtGauge.Text = String.Empty;
            txtLength.Text = String.Empty;
            txtWidth.Text = String.Empty;
            txtGauge.Text = String.Empty;
            txtLength.Text = String.Empty;
            txtWidth.Text = String.Empty;
            txtCurrentStock.Text = String.Empty;
            txtCurrentStock.ToolTip = String.Empty;
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
            if (ddlLookupPONumber.Items.Count > 0)
            {
                ddlLookupPONumber.SelectedIndex = 0;
            }
            txtPONumber.Enabled = true;
            txtPONumber.Text = String.Empty;
            ResetPartsInfo();
            ResetCommonParts();
            ResetGrid();
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }        
    }

    private void Bind_Grid(string POId)
    {
        try
        {         
            DataSet ds = new DataSet();
            ObjBOL.Operation = 14;
            ObjBOL.LoginUserID = Utility.GetCurrentUser();
            if (POId != "")
            {
                ObjBOL.POId = Convert.ToInt32(POId);
            }       
            if(ObjBOL.LoginUserID.ToString() != "263")
            {
                if (ddlSheetMetalID.Items.Count > 0)
                {
                    ObjBOL.id = Convert.ToInt32(ddlSheetMetalID.SelectedValue);
                }
                if (ddlWarehouse.Items.Count > 0)
                {
                    ObjBOL.warehouseid = Convert.ToInt32(ddlWarehouse.SelectedValue);
                }
            }
            else
            {
                ObjBOL.id = 0;
                ObjBOL.warehouseid = 0;
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
            ObjBOL.Operation = 25;
            if (ddlSheetMetalID.Items.Count > 0)
            {
                ObjBOL.id = Convert.ToInt32(ddlSheetMetalID.SelectedValue);
            }
            if (ddlWarehouse.Items.Count > 0)
            {
                ObjBOL.warehouseid = Convert.ToInt32(ddlWarehouse.SelectedValue);
            }
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {                
                txtCurrentStock.Text = ds.Tables[0].Rows[0]["currentstock"].ToString();               
            }
            else
            {
                txtCurrentStock.Text = String.Empty;
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
            if (ddlSheetMetalID.Items.Count > 0)
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
                    txtCurrentStock.ToolTip = ds.Tables[0].Rows[0]["CurrentStockToolTip"].ToString();
                }
                else
                {
                    txtCurrentStock.ToolTip = String.Empty;
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvSheetMetalStockIn_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            int StockInID = Convert.ToInt32(gvSheetMetalStockIn.DataKeys[e.NewEditIndex].Values[0]);
            int SheetMetalID = Convert.ToInt32(gvSheetMetalStockIn.DataKeys[e.NewEditIndex].Values[1]);
            int PODetailID = Convert.ToInt32(gvSheetMetalStockIn.DataKeys[e.NewEditIndex].Values[3]);
            ObjBOL.Operation = 18;
            ObjBOL.PODetailID = StockInID;
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (PODetailID > 0)
                {
                    hfPODetailID.Value = PODetailID.ToString();
                }
                
                if (ddlSheetMetalID.Items.FindByValue(ds.Tables[0].Rows[0]["SheetMetalID"].ToString()) != null)
                {
                    ddlSheetMetalID.SelectedValue = ds.Tables[0].Rows[0]["SheetMetalID"].ToString();
                }                
                txtPONumber.Text = ds.Tables[0].Rows[0]["PONumber"].ToString();
                if(txtPONumber.Text != "")
                {
                    txtPONumber.Enabled = false;
                }
                else
                {
                    txtPONumber.Enabled = true;
                }
                if(ddlWarehouse.Items.FindByValue(ds.Tables[0].Rows[0]["WarehouseId"].ToString()) != null)
                {
                    ddlWarehouse.SelectedValue = ds.Tables[0].Rows[0]["WarehouseId"].ToString();
                }
                txtGauge.Text = ds.Tables[0].Rows[0]["gauge"].ToString();
                txtLength.Text = ds.Tables[0].Rows[0]["length"].ToString();
                txtWidth.Text = ds.Tables[0].Rows[0]["width"].ToString();
                string orderDate = ds.Tables[0].Rows[0]["DeliveryDate"].ToString();
                if (!string.IsNullOrEmpty(orderDate))
                {
                    txtDeliveryDate.Text = Convert.ToDateTime(orderDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    txtDeliveryDate.Text = String.Empty;
                }
                string expectedDate = ds.Tables[0].Rows[0]["ExpectedArrivalDate"].ToString();
                if (!string.IsNullOrEmpty(expectedDate))
                {
                    txtExpectedArrivalDate.Text = Convert.ToDateTime(expectedDate).ToString("MM/dd/yyyy");
                }
                else
                {
                    txtExpectedArrivalDate.Text = String.Empty;
                }
                txtDelQty.Text= ds.Tables[0].Rows[0]["DeliveryQuantity"].ToString();                
                if(ddlDeliveryStatus.Items.FindByValue(ds.Tables[0].Rows[0]["DeliveryStatusId"].ToString()) != null)
                {
                    ddlDeliveryStatus.SelectedValue = ds.Tables[0].Rows[0]["DeliveryStatusId"].ToString();
                    hfStockDeliveryStatus.Value = ddlDeliveryStatus.SelectedValue;
                }
                BindSheetMetalDetails();                       
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvSheetMetalStockIn_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if(e.CommandName== "StockIn")
            {
                string msg = "";
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int PODetailID = Int32.Parse(gvSheetMetalStockIn.DataKeys[rowIndex].Values[0].ToString());
                Label lblSheetMetalID= gvSheetMetalStockIn.Rows[rowIndex].FindControl("lblSheetMetal") as Label;
                Label lblWareHouseID = gvSheetMetalStockIn.Rows[rowIndex].FindControl("lblWarehouseID") as Label;
                Label lblDelQty = gvSheetMetalStockIn.Rows[rowIndex].FindControl("lblTransactQty") as Label;
                int DeliveryStatusID = Int32.Parse(gvSheetMetalStockIn.DataKeys[rowIndex].Values[2].ToString());
                if(DeliveryStatusID != 2)
                {
                    Utility.ShowMessage_Error(Page, "Please Change Delivery Status to Received!");
                    return;
                }
                ObjBOL.Operation = 16;
                if(lblSheetMetalID.Text != "")
                {
                    ObjBOL.id = Convert.ToInt32(lblSheetMetalID.Text);
                }
                if(lblWareHouseID.Text != "")
                {
                    ObjBOL.warehouseid = Convert.ToInt32(lblWareHouseID.Text);
                }
                if(lblDelQty.Text != "")
                {
                    ObjBOL.DeliveryQuantity = Convert.ToInt32(lblDelQty.Text);
                }
                ObjBOL.LoginUserID = Utility.GetCurrentUser();
                ObjBOL.PODetailID = PODetailID;
                ObjBOL.DeliveryStatus = DeliveryStatusID;
                msg = ObjBLL.Return_String(ObjBOL);
                if(msg.Trim() == "ER01")
                {
                    Utility.ShowMessage_Error(Page, "Please Change Delivery Status to Received!");
                    return;
                }
                if (msg.Trim() == "ER02")
                {
                    Utility.ShowMessage_Error(Page, "Already Stock-In!");
                    return;
                }
                if (msg == "S")
                {
                    Utility.ShowMessage_Success(Page, "Stock-In Successfully!");
                    Utility.MaintainLogsSpecial("FrmSheetMetalStockIn", "Stock-In", PODetailID.ToString());
                }
                Bind_Grid(ddlLookupPONumber.SelectedValue);
                BindPONumber(ddlLookupPONumber.SelectedValue);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }    

    protected void gvSheetMetalStockIn_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string msg = "";
            int SheetMetalID = Convert.ToInt32(gvSheetMetalStockIn.DataKeys[e.RowIndex].Values[1]);
            int ID = Convert.ToInt32(gvSheetMetalStockIn.DataKeys[e.RowIndex].Values[3]);
            Label lblWareHouseID = gvSheetMetalStockIn.Rows[e.RowIndex].FindControl("lblWarehouseID") as Label;
            ObjBOL.Operation = 19;
            ObjBOL.id = SheetMetalID;
            ObjBOL.PODetailID = ID;
            ObjBOL.warehouseid =Convert.ToInt32(lblWareHouseID.Text);
            msg = ObjBLL.Return_String(ObjBOL);
            if (msg.Trim() == "ER")
            {
                Utility.ShowMessage_Error(Page, "Already Stock In!");
            }
            if (msg.Trim() == "D")
            {
                Utility.ShowMessage_Success(Page, "Record Deleted Successfully !");
                Utility.MaintainLogsSpecial("FrmSheetMetalStockIn", "Delete", ddlLookupPONumber.SelectedValue.ToString());
            }          
            Bind_Grid(ddlLookupPONumber.SelectedValue);
            BindPONumber(ddlLookupPONumber.SelectedValue);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlSheetMetalID_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlSheetMetalID.SelectedIndex > 0)
            {
                BindSheetMetalDetails();
            }
            else
            {           
                ResetCommonParts();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlLookupPONumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlLookupPONumber.SelectedIndex > 0)
            {
                Bind_Grid(ddlLookupPONumber.SelectedValue);
                txtPONumber.Text=ddlLookupPONumber.SelectedItem.Text;
                txtPONumber.Enabled = false;
            }
            else
            {
                txtPONumber.Text = String.Empty;
                txtPONumber.Enabled = true;
                btnSave.Text = "Save";
                ResetCommonParts();
                ResetPartsInfo();
                ResetGrid();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void BindPONumber(string Stockinid)
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 15;            
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlLookupPONumber, ds.Tables[0]);
                if (ddlLookupPONumber.Items.FindByValue(Stockinid) != null)
                {
                    ddlLookupPONumber.SelectedValue = Stockinid;
                }
            }
            else
            {
                if (ddlLookupPONumber.Items.Count > 0)
                {
                    ddlLookupPONumber.Items.Clear();
                    txtPONumber.Text = String.Empty;
                    txtPONumber.Enabled = true;
                }
            }
            Bind_Grid(ddlLookupPONumber.SelectedValue);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvSheetMetalStockIn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int deliveryStatusId = Convert.ToInt32(
                    gvSheetMetalStockIn.DataKeys[e.Row.RowIndex]["DeliveryStatusId"]);

                switch (deliveryStatusId)
                {
                    case 1:

                        foreach (TableCell cell in e.Row.Cells)
                        {
                            cell.BackColor = System.Drawing.Color.LightGreen;
                        }

                        break;

                    case 2:

                        foreach (TableCell cell in e.Row.Cells)
                        {
                            cell.BackColor = System.Drawing.Color.Gold;
                        }

                        break;

                    case 3:

                        foreach (TableCell cell in e.Row.Cells)
                        {
                            cell.BackColor = System.Drawing.Color.DeepSkyBlue;
                        }

                        break;
                }
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

    protected void btnConsumeQuantity_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/SheetMetalForecasting/FrmSheetMetalConsumption.aspx", false);
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
            if (ddlWarehouse.SelectedIndex > 0)
            {
                BindSheetMetalDetails();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/SheetMetalForecasting/FrmSheetMetalPOReport.aspx", false);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}