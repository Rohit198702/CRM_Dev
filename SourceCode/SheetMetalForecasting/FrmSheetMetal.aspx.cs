using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Web.UI.WebControls;
using BOLAERO;
using BLLAERO;
public partial class SheetMetalForecasting_FrmSheetMetal : System.Web.UI.Page
{
    BOLSheetMetal ObjBOL = new BOLSheetMetal();
    BLLSheetMetal ObjBLL = new BLLSheetMetal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_Control();
            BindPartNumber("");
        }
    }

    private void Bind_Control()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 1;
            ds = ObjBLL.Return_DataSet(ObjBOL);     
            if (ds.Tables[1].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlGauge, ds.Tables[1]);
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlLength, ds.Tables[2]);
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlWidth, ds.Tables[3]);
            }
            if (ds.Tables[4].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlWarehouse, ds.Tables[4]);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void CheckStatus(string partid)
    {
        try
        {
            if(partid != "")
            {
                string msg = String.Empty;
                ObjBOL.Operation = 26;
                ObjBOL.id = Convert.ToInt32(partid);
                msg = ObjBLL.Return_String(ObjBOL);
                if (msg.Trim() == "ER")
                {
                    ddlSheetMetalStatus.Enabled = false;
                }
                else
                {
                    ddlSheetMetalStatus.Enabled = true;
                }
            }
            else
            {
                ddlSheetMetalStatus.Enabled = true;
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
            if (txtSheetMetalID.Text.Trim()=="")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Sheet Metal ID!");
                txtSheetMetalID.Focus();
                return false;
            }
            if (txtSheetMetalDesc.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Sheet Metal Description!");
                txtSheetMetalDesc.Focus();
                return false;
            }
            if (ddlGauge.SelectedIndex==0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Gauge!");
                ddlGauge.Focus();
                return false;
            }
            if (ddlWidth.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Width!");
                ddlWidth.Focus();
                return false;
            }
            if (ddlLength.SelectedIndex==0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Length!");
                ddlLength.Focus();
                return false;
            }

            if (ddlSheetMetalStatus.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Status!");
                ddlSheetMetalStatus.Focus();
                return false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidationCheck() == true)
            {
                string msg = String.Empty;
                if (btnSave.Text=="Save")
                {
                    //Save 
                    ObjBOL.Operation = 2;
                }
                else
                {
                    ObjBOL.Operation = 3;
                    ObjBOL.id =Convert.ToInt32(ddlPartNumber.SelectedValue);
                }                
                if(txtSheetMetalID.Text.Trim() != "")
                {
                    ObjBOL.sheetmetalid = txtSheetMetalID.Text.Trim();
                }
                if(txtSheetMetalDesc.Text.Trim() != "")
                {
                    ObjBOL.sheetmetaldesc = txtSheetMetalDesc.Text.Trim();
                }
                if (ddlGauge.Items.Count > 0)
                {
                    ObjBOL.gauge = Convert.ToInt32(ddlGauge.SelectedValue);
                }
                if (ddlLength.Items.Count > 0)
                {
                    ObjBOL.length = Convert.ToInt32(ddlLength.SelectedValue);
                }
                if (ddlWidth.Items.Count > 0)
                {
                    ObjBOL.width = Convert.ToInt32(ddlWidth.SelectedValue);
                }            
                if(txtCurrentStock.Text.Trim() != "")
                {
                    ObjBOL.currentstock =Convert.ToInt32(txtCurrentStock.Text);
                }
                if(ddlSheetMetalStatus.SelectedIndex > 0)
                {
                    ObjBOL.SheetMetalStatus = Convert.ToInt32(ddlSheetMetalStatus.SelectedValue);
                }
                msg = ObjBLL.Return_String(ObjBOL);
                if(msg.Trim() == "ER01")
                {
                    Utility.ShowMessage_Error(Page, "Sheet Metal ID already exists!");
                    return;
                }
                if(msg.Trim() != "U")
                {
                    Utility.ShowMessage_Success(Page, "Sheet Metal Added Successfully !");
                    Utility.MaintainLogsSpecial("FrmSheetMetal", "Save", msg.ToString());
                    BindPartNumber(msg.ToString());                              
                }
                else
                {
                    Utility.ShowMessage_Success(Page, "Sheet Metal Updated Successfully!");
                    Utility.MaintainLogsSpecial("FrmSheetMetal", "Update", ddlPartNumber.SelectedValue);
                    BindPartNumber(ddlPartNumber.SelectedValue);
                }
                ResetPartsInfo();
                Bind_Grid();
                Bind_GridDetails();
                ResetWarehouseControls();
                DisabledSheetMetalMainTable();
                EnabledWarehouseControls();
                btnSave.Enabled = false;
                btnAdd.Enabled = true;
                btnSave.Text = "Save";
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetWarehouseModule()
    {
        try
        {
            ResetWarehouseControls();
            DisabledWarehouseControls();
            ResetSheetWarehouseGridInfo();
            btnAdd.Text = "Add Details";
            btnAdd.Enabled = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void BindPartNumber(string id)
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 4;
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlPartNumber, ds.Tables[0]);
                if(ddlPartNumber.Items.FindByValue(id) != null)
                {
                    ddlPartNumber.SelectedValue = id;
                }
                else
                {
                    ResetWarehouseModule();
                }
            }
            else
            {
                ResetWarehouseModule();
            }
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
            txtSheetMetalID.Text = String.Empty;
            txtSheetMetalDesc.Text = String.Empty;
            if (ddlGauge.Items.Count > 0)
            {
                ddlGauge.SelectedIndex = 0;
            }
            if (ddlLength.Items.Count > 0)
            {
                ddlLength.SelectedIndex = 0;
            }
            if (ddlWidth.Items.Count > 0)
            {
                ddlWidth.SelectedIndex = 0;
            }                       
            txtCurrentStock.Text = String.Empty;
            txtCurrentStock.ToolTip = String.Empty;
            if (ddlSheetMetalStatus.Items.Count > 0)
            {
                ddlSheetMetalStatus.SelectedIndex = 0;
            }
            CheckStatus("");       
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
            if (ddlPartNumber.Items.Count > 0)
            {
                ddlPartNumber.SelectedIndex = 0;
            }
            ResetOnLoad();
            ResetGrid();
            ResetSheetWarehouseGridInfo();
            ResetWarehouseGridInfo();       
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
            ObjBOL.Operation = 5;
            if (ddlPartNumber.Items.Count > 0)
            {
                ObjBOL.id = Convert.ToInt32(ddlPartNumber.SelectedValue);
            }
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                divsheetmetaldetails.Visible = true;
                gvSheetMetalParts.DataSource = ds.Tables[0];
                gvSheetMetalParts.DataBind();
            }
            else
            {
                ResetGrid();
            }
            Bind_WarehouseGrid();
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
            gvSheetMetalParts.DataSource = "";
            gvSheetMetalParts.DataBind();
            divsheetmetaldetails.Visible = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlPartNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ResetPartsInfo();            
            btnSave.Text = "Save";
            if (ddlPartNumber.SelectedIndex > 0)
            {
                ResetWarehouseControls();
                DisabledSheetMetalMainTable();
                EnabledWarehouseControls();

                Bind_Grid();
                Bind_GridDetails();
                btnAdd.Enabled = true;
                btnAdd.Text = "Add Details";
                btnSave.Enabled = false;                
            }
            else
            {
                btnSave.Enabled = true;
                btnAdd.Enabled = false;
                ResetGrid();
                ResetWarehouseGridInfo();
                ResetWarehouseModule();
                EnabledSheetMetalMainTable();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvSheetMetalParts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            int ID = Convert.ToInt32(gvSheetMetalParts.DataKeys[e.NewEditIndex].Values[0]);            
            ObjBOL.Operation = 5;
            ObjBOL.id = ID;
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtSheetMetalID.Text = ds.Tables[0].Rows[0]["sheetmetalid"].ToString();
                txtSheetMetalDesc.Text = ds.Tables[0].Rows[0]["sheetmetaldesc"].ToString();
                if(ddlGauge.Items.FindByValue(ds.Tables[0].Rows[0]["gauge"].ToString()) != null)
                {
                    ddlGauge.SelectedValue = ds.Tables[0].Rows[0]["gauge"].ToString();
                }
                if (ddlLength.Items.FindByValue(ds.Tables[0].Rows[0]["length"].ToString()) != null)
                {
                    ddlLength.SelectedValue = ds.Tables[0].Rows[0]["length"].ToString();
                }
                if (ddlWidth.Items.FindByValue(ds.Tables[0].Rows[0]["width"].ToString()) != null)
                {
                    ddlWidth.SelectedValue = ds.Tables[0].Rows[0]["width"].ToString();
                }              
                txtCurrentStock.Text= ds.Tables[0].Rows[0]["currentstock"].ToString();
                if(ddlSheetMetalStatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()) != null)
                {
                    ddlSheetMetalStatus.SelectedValue = ds.Tables[0].Rows[0]["Status"].ToString();
                }
                if(ds.Tables[0].Rows[0]["CurrentStockToolTip"].ToString() != null)
                {
                    txtCurrentStock.ToolTip = ds.Tables[0].Rows[0]["CurrentStockToolTip"].ToString();
                }
                else
                {
                    txtCurrentStock.ToolTip = String.Empty;
                }
                CheckStatus(ObjBOL.id.ToString());
                btnSave.Enabled = true;               
                DisabledWarehouseControls();
                EnabledSheetMetalMainTable();
                ResetWarehouseControls();
                btnAdd.Text = "Add Details";
                btnAdd.Enabled = false;      
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }


    protected void gvSheetMetalParts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string msg = "";
            int ID = Convert.ToInt32(gvSheetMetalParts.DataKeys[e.RowIndex].Values[0]);
            ObjBOL.Operation = 6;
            ObjBOL.id = ID;
            msg = ObjBLL.Return_String(ObjBOL);
            if (msg.Trim() == "ER")
            {
                Utility.ShowMessage_Error(Page, "Sheet Metal cannot be deleted!");
            }
            if (msg.Trim() == "ER01")
            {
                Utility.ShowMessage_Error(Page, "Already Linked!");
            }
            if (msg.Trim() == "D")
            {
                Utility.ShowMessage_Success(Page, "Part Deleted Successfully !");
                Utility.MaintainLogsSpecial("FrmSheetMetal", "Delete", ddlPartNumber.SelectedValue.ToString());
                BindPartNumber("");
            }      
            Bind_Grid();
            ResetOnLoad();
            DisabledSheetMetalMainTable();
            btnSave.Text = "Save";
            EnabledWarehouseControls();
            btnAdd.Text = "Add Details";
            btnAdd.Enabled = true;
            btnSave.Enabled = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetWarehouseGridInfo()
    {
        try
        {
            gvSheetMetalWarehouse.DataSource = "";
            gvSheetMetalWarehouse.DataBind();
            divWarehouse.Visible = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetOnLoad()
    {
        try
        {
            ResetPartsInfo();
            ResetWarehouseControls();
            EnabledSheetMetalMainTable();
            DisabledWarehouseControls();
            btnSave.Text = "Save";
            btnAdd.Text = "Add Details";
            btnSave.Enabled = true;
            btnAdd.Enabled = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void Bind_WarehouseGrid()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 9;
            ObjBOL.id = Convert.ToInt32(ddlPartNumber.SelectedValue);
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                divWarehouse.Visible = true;
                gvSheetMetalWarehouse.DataSource = ds.Tables[0];
                gvSheetMetalWarehouse.DataBind();
            }
            else
            {
                ResetWarehouseGridInfo();
            }
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

    protected void tblStockIn_Click(object sender, EventArgs e)
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
    private void ResetWarehouseControls()
    {
        try
        {
            if (ddlWarehouse.Items.Count > 0)
            {
                ddlWarehouse.SelectedIndex = 0;
            }
            txtMinQty.Text = String.Empty;
            txtMaxQty.Text = String.Empty;
            txtLeadTime.Text = String.Empty;
            txtQtyonSkid.Text = String.Empty;
            btnAdd.Text = "Add Details";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    private void EnabledWarehouseControls()
    {
        try
        {
            ddlWarehouse.Enabled = true;
            txtMinQty.Enabled = true;
            txtMaxQty.Enabled = true;
            txtLeadTime.Enabled = true;
            txtQtyonSkid.Enabled = true;           
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    private void DisabledWarehouseControls()
    {
        try
        {
            ddlWarehouse.Enabled = false;
            txtMinQty.Enabled = false;
            txtMaxQty.Enabled = false;
            txtLeadTime.Enabled = false;
            txtQtyonSkid.Enabled = false;           
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private bool ValidationDetailsCheck()
    {
        try
        {
            if (ddlPartNumber.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Sheet Metal !");
                ddlPartNumber.Focus();
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
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if(ValidationDetailsCheck() == true)
            {
                string msg = string.Empty;
                if (btnAdd.Text == "Add Details")
                {
                    ObjBOL.Operation = 27;
                }
                else
                {
                    ObjBOL.Operation = 28;
                    if (hfSheetDetailID.Value != "-1")
                    {
                        ObjBOL.SheetDetailID = Convert.ToInt32(hfSheetDetailID.Value);
                    }
                }
                ObjBOL.id = Convert.ToInt32(ddlPartNumber.SelectedValue);
                ObjBOL.warehouseid = Convert.ToInt32(ddlWarehouse.SelectedValue);
                if(txtMinQty.Text.Trim() != "")
                {
                    ObjBOL.minqty = Convert.ToInt32(txtMinQty.Text);
                }
                if(txtMaxQty.Text.Trim() != "")
                {
                    ObjBOL.maxqty = Convert.ToInt32(txtMaxQty.Text);
                }                
                ObjBOL.leadtime = txtLeadTime.Text;
                ObjBOL.qtyonskid = txtQtyonSkid.Text;
                msg = ObjBLL.Return_String(ObjBOL);
                if (msg.Trim() == "ER")
                {
                    Utility.ShowMessage_Error(Page, "Duplicate Record !");
                    return;
                }
                if (msg.Trim() == "U")
                {
                    Utility.ShowMessage_Success(Page, "Record Updated Successfully !");
                    Utility.MaintainLogsSpecial("FrmSheetMetal", "Update-Details", ObjBOL.SheetDetailID.ToString());
                }
                else
                {
                    Utility.ShowMessage_Success(Page, "Record Added Successfully !");
                    Utility.MaintainLogsSpecial("FrmSheetMetal", "Add-Details", msg.Trim().ToString());
                }
                btnAdd.Text = "Update Details";
                ResetPartsInfo();
                ResetWarehouseControls();
                DisabledSheetMetalMainTable();
                btnSave.Enabled = false;
                btnSave.Text = "Save";
                Bind_GridDetails();
            }
            
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvSheetWarehouseDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            int SheetDetailID = Convert.ToInt32(gvSheetWarehouseDetails.DataKeys[e.NewEditIndex].Values[0]);
            int SheetMetalID = Convert.ToInt32(gvSheetWarehouseDetails.DataKeys[e.NewEditIndex].Values[1]);
            ObjBOL.Operation = 31;
            ObjBOL.SheetDetailID = SheetDetailID;
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hfSheetDetailID.Value = ds.Tables[0].Rows[0]["SheetDetailID"].ToString();
                ddlWarehouse.SelectedValue = ds.Tables[0].Rows[0]["WarehouseID"].ToString();
                txtMinQty.Text = ds.Tables[0].Rows[0]["minqty"].ToString();
                txtMaxQty.Text = ds.Tables[0].Rows[0]["maxqty"].ToString();
                txtLeadTime.Text = ds.Tables[0].Rows[0]["leadtime"].ToString();
                txtQtyonSkid.Text = ds.Tables[0].Rows[0]["qtyonskid"].ToString();
                btnSave.Enabled = false;
                btnSave.Text = "Save";
                ResetPartsInfo();
                DisabledSheetMetalMainTable();
                EnabledWarehouseControls();
                btnAdd.Enabled = true;               
                btnAdd.Text = "Update Details";
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetSheetWarehouseGridInfo()
    {
        try
        {
            divsheetmetalwarehouse.Visible = false;
            gvSheetWarehouseDetails.DataSource = "";
            gvSheetWarehouseDetails.DataBind();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void Bind_GridDetails()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 29;
            ObjBOL.id = Convert.ToInt32(ddlPartNumber.SelectedValue);
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                divsheetmetalwarehouse.Visible = true;
                gvSheetWarehouseDetails.DataSource = ds.Tables[0];
                gvSheetWarehouseDetails.DataBind();
            }
            else
            {
                ResetSheetWarehouseGridInfo();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvSheetWarehouseDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            string msg = "";
            int ID = Convert.ToInt32(gvSheetWarehouseDetails.DataKeys[e.RowIndex].Values[0]);
            ObjBOL.Operation = 30;
            ObjBOL.SheetDetailID = ID;
            msg = ObjBLL.Return_String(ObjBOL);           
            if (msg.Trim() == "D")
            {
                Utility.ShowMessage_Success(Page, "Record Deleted Successfully !");
                Utility.MaintainLogsSpecial("FrmSheetMetal", "Delete-Details", ObjBOL.SheetDetailID.ToString());                
            }
            Bind_GridDetails();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnPreviewReport_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/SheetMetalForecasting/FrmSheetMetalReport.aspx", false);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    private void EnabledSheetMetalMainTable()
    {
        try
        {
            txtSheetMetalID.Enabled = true;
            txtSheetMetalDesc.Enabled = true;
            ddlGauge.Enabled = true;
            ddlWidth.Enabled = true;
            ddlLength.Enabled = true;
            ddlSheetMetalStatus.Enabled = true;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    private void DisabledSheetMetalMainTable()
    {
        try
        {
            txtSheetMetalID.Enabled = false;
            txtSheetMetalDesc.Enabled = false;
            ddlGauge.Enabled = false;
            ddlWidth.Enabled = false;
            ddlLength.Enabled = false;
            ddlSheetMetalStatus.Enabled = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    
}