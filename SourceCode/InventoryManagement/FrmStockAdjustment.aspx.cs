using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BOLAERO;
using BLLAERO;

public partial class INVManagement_FrmStockAdjustment : System.Web.UI.Page
{
    BOLINVPartsInfo ObjBOL = new BOLINVPartsInfo();
    BLLINVPartsinfo ObjBLL = new BLLINVPartsinfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                if (Utility.IsAuthorized())
                {
                    Bind_Control();
                    BindWareHouse();
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
            ObjBOL.operation = 1;
            ds = ObjBLL.GetJobs(ObjBOL);
            if (ds.Tables[2].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlPartNumber, ds.Tables[2]);
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlReason, ds.Tables[3]);
            }

            if (ds.Tables[5].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlProductCode, ds.Tables[5]);
            }
            //ddlReason
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void BindWareHouse()
    {
        try
        {
            if (Utility.IsAuthorized())
            {
                DataSet ds = new DataSet();
                int EmployeeID = Utility.GetCurrentSession().EmployeeID;
                if (EmployeeID != 0)
                {
                    ObjBOL.operation = 8;
                    ObjBOL.userid = EmployeeID;
                    ds = ObjBLL.GetJobs(ObjBOL);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string checkEmpWarehouse = ds.Tables[0].Rows[0]["WareHouse"].ToString();
                        Utility.BindDropDownList(ddlWarehouse, ds.Tables[1]);
                        if (checkEmpWarehouse != "0")
                        {
                            ddlWarehouse.SelectedValue = checkEmpWarehouse;
                        }
                        else
                        {
                            if (ddlWarehouse.Items.Count > 0)
                            {
                                if(EmployeeID == 280 || EmployeeID== 309)
                                {
                                    ddlWarehouse.SelectedValue = "1";
                                }
                                else
                                {
                                    ddlWarehouse.SelectedIndex = 0;
                                }
                               
                            }

                        }
                    }
                }
            }
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
            ObjBOL.operation = 6;
            if (ddlPartNumber.Items.Count > 0)
            {
                if (ddlPartNumber.SelectedIndex > 0)
                {
                    ObjBOL.PartId = Convert.ToInt32(ddlPartNumber.SelectedValue);
                }
            }
            if (ddlWarehouse.Items.Count > 0)
            {
                if (ddlWarehouse.SelectedIndex > 0)
                {
                    ObjBOL.WarehouseId = Convert.ToInt32(ddlWarehouse.SelectedValue);
                }
            }            
            ds = ObjBLL.GetStockAdjustment(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSearch.DataSource = ds.Tables[0];
                gvSearch.DataBind();
            }
            else
            {
                gvSearch.DataSource = "";
                gvSearch.DataBind();
            }
            //ddlReason
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ClearAll()
    {
        try
        {
            ddlProductCode.SelectedIndex = 0;
            ddlProductCode_SelectedIndexChanged();
            ddlPartNumber.SelectedIndex = 0;
            ddlType.SelectedIndex = 0;
            ddlReason.SelectedIndex = 0;
            ddlWarehouse.SelectedIndex = 0;
            txtQty.Text = string.Empty;
            txtSummary.Text = string.Empty;
            gvSearch.DataSource = "";
            gvSearch.DataBind();
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
            gvSearch.DataSource = "";
            gvSearch.DataBind();
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
            ClearAll();
            BindWareHouse();
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
            if (ddlPartNumber.SelectedIndex == 0)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "alert('Please Select Part No.');", true);
                Utility.ShowMessage_Error(Page, "Please Select Part No.");
                ddlPartNumber.Focus();
                return false;
            }

            if (ddlWarehouse.SelectedIndex == 0)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "alert('Please Select Part No.');", true);
                Utility.ShowMessage_Error(Page, "Please Select Warehouse");
                ddlWarehouse.Focus();
                return false;
            }

            if (ddlType.SelectedIndex == 0)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "alert('Please Select Adjustment Type.');", true);
                Utility.ShowMessage_Error(Page, "Please Select Adjustment Type.");
                ddlType.Focus();
                return false;
            }

            if (ddlReason.SelectedIndex == 0)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "alert('Please Select Adjustment Reason.');", true);
                Utility.ShowMessage_Error(Page, "Please Select Adjustment Reason.");
                ddlReason.Focus();
                return false;
            }

            if (txtQty.Text == "")
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "alert('Please Enter Quantity.');", true);
                Utility.ShowMessage_Error(Page, "Please Enter Quantity.");
                txtQty.Focus();
                return false;
            }

            if (txtSummary.Text == "")
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "alert('Please Enter Summary.');", true);
                Utility.ShowMessage_Error(Page, "Please Enter Summary.");
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidationCheck())
            {

                string msg = "";
                ObjBOL.operation = 5;
                if(ddlPartNumber.SelectedIndex>0)
                {
                    ObjBOL.PartId = Convert.ToInt32(ddlPartNumber.SelectedValue);
                }             
                if (ddlType.SelectedValue == "1")
                {
                    ObjBOL.Qty = Convert.ToInt32(txtQty.Text);
                }
                //2 = out
                else if (ddlType.SelectedValue == "2")
                {
                    ObjBOL.Qty = -Math.Abs(Convert.ToInt32(txtQty.Text));
                }
                if(ddlReason.Items.Count>0)
                {
                    ObjBOL.adjustmentreasonid = Convert.ToInt32(ddlReason.SelectedValue);
                }                
                ObjBOL.transactsummary = txtSummary.Text;
                if(Utility.IsAuthorized())
                {
                    ObjBOL.userid = Convert.ToInt32(Utility.GetCurrentUser());
                }               
                if(ddlWarehouse.Items.Count>0)
                {
                    ObjBOL.WarehouseId = Int32.Parse(ddlWarehouse.SelectedValue);
                }                
                msg = ObjBLL.StockAdjustment(ObjBOL);
                if (msg.Trim() == "S")
                {
                    Utility.ShowMessage_Success(Page, "Stock Adjusted !!");
                    Utility.MaintainLogsSpecial("FrmStockAdjustment.aspx", "StockAdjust", ddlPartNumber.SelectedValue.ToString());
                    BindPartNumber(ddlPartNumber.SelectedValue);
                }
                Bind_Grid();
            }

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
            if (ddlPartNumber.SelectedIndex > 0)
            {
                Bind_Grid();
            }
            else
            {
                gvSearch.DataSource = "";
                gvSearch.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void BindPartNumber(string PartID)
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.operation = 1;
            if (PartID != "")
            {
                ObjBOL.PartId = Convert.ToInt32(PartID);
                ds = ObjBLL.GetStockAdjustment(ObjBOL);
                if (ds.Tables[2].Rows.Count > 0)
                {
                    Utility.BindDropDownList(ddlPartNumber, ds.Tables[2]);
                    ddlPartNumber.SelectedValue = PartID;
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlProductCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlProductCode_SelectedIndexChanged();
    }

    protected void ddlProductCode_SelectedIndexChanged()
    {
        try
        {
            ResetGrid();
            if (ddlProductCode.SelectedIndex > 0)
            {
                ObjBOL.userid = Int32.Parse(ddlProductCode.SelectedValue);
            }

            ObjBOL.operation = 7;
            DataSet ds = new DataSet();
            ds = ObjBLL.GetJobs(ObjBOL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlPartNumber, ds.Tables[0]);
            }
            else
            {
                if(ddlPartNumber.Items.Count>0)
                {
                    ddlPartNumber.Items.Clear();
                }                    
            }
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
            if (ddlPartNumber.SelectedIndex > 0)
            {
                Bind_Grid();
            }
            else
            {
                gvSearch.DataSource = "";
                gvSearch.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}