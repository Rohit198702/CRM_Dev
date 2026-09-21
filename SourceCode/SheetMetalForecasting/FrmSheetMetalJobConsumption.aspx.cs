using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;
using System.Web.UI.WebControls;
using BOLAERO;
using BLLAERO;
public partial class SheetMetalForecasting_FrmSheetMetalJobConsumption : System.Web.UI.Page
{
    BOLSheetMetal ObjBOL = new BOLSheetMetal();
    BLLSheetMetal ObjBLL = new BLLSheetMetal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_Control();           
        }
    }

    private void Bind_Control()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 1;
            ds = ObjBLL.Return_JobConsumptionDataSet(ObjBOL);     
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlSheetMetal, ds.Tables[0]);
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlLookupProjectCode, ds.Tables[1]);
                Utility.BindDropDownList(ddlProjectCode, ds.Tables[1]);
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlWarehouse, ds.Tables[2]);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void txtSearchPNum_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtSearchPNum.Text.Length >= 7)
            {
                string OutJnumber = string.Empty;
                if (txtSearchPNum.Text.Length > 7)
                {
                    int index = txtSearchPNum.Text.IndexOf(',');
                    if (index < 0)
                    {
                        index = 0;
                    }
                    OutJnumber = txtSearchPNum.Text.Substring(0, index);
                    if(OutJnumber == "")
                    {
                        txtSearchPNum.Text = String.Empty;
                    }
                }
                else
                {
                    OutJnumber = txtSearchPNum.Text;
                }
                Bind_Grid();
            }
            else
            {
                txtSearchPNum.Text = String.Empty;
                ResetGrid();
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
            if (ddlProjectCode.SelectedIndex==0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Product Code!");
                ddlProjectCode.Focus();
                return false;
            }
            if (ddlProjectCode.SelectedValue == "1")
            {
                if (txtSearchPNum.Text.Trim() == "")
                {
                    Utility.ShowMessage_Error(Page, "Please Select Job ID!");
                    txtSearchPNum.Focus();
                    return false;
                }
            }
            if (ddlWarehouse.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Warehouse!");
                ddlWarehouse.Focus();
                return false;
            }

            
            if (ddlSheetMetal.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Sheet Metal ID!");
                ddlSheetMetal.Focus();
                return false;
            }
            if (txtDate.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Date!");
                txtDate.Focus();
                return false;
            }
            if (txtTransactQty.Text == "" || txtTransactQty.Text == "0")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Transact Quantity!");
                txtTransactQty.Focus();
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
                if (btnSave.Text == "Update")
                {
                    ObjBOL.Operation = 3;
                    if(hfConsumptionID.Value != "-1")
                    {
                        ObjBOL.ConsumptionID =Convert.ToInt32(hfConsumptionID.Value);
                    }
                }
                else
                {
                    ObjBOL.Operation = 2;
                }
                if (ddlProjectCode.Items.Count > 0)
                {
                    ObjBOL.productcodeid = Convert.ToInt32(ddlProjectCode.SelectedValue);
                }
                if (ddlWarehouse.Items.Count > 0)
                {
                    ObjBOL.warehouseid = Convert.ToInt32(ddlWarehouse.SelectedValue);
                }
                if(txtSearchPNum.Text.Trim() != "")
                {
                    ObjBOL.JobNo = txtSearchPNum.Text.Trim();
                }
                if(ddlSheetMetal.SelectedIndex>0)
                {
                    ObjBOL.JobSheetMetalID =Convert.ToInt32(ddlSheetMetal.SelectedValue);
                }  
                if(txtDate.Text.Trim() != "")
                {
                    ObjBOL.JobDate = Convert.ToDateTime(txtDate.Text.Trim());
                }
                if(txtTransactQty.Text.Trim() != "")
                {
                    ObjBOL.JobTransactQty = Convert.ToInt32(txtTransactQty.Text.Trim());
                }                
                msg = ObjBLL.Return_JobConsumptionString(ObjBOL);
                if(msg.Trim() == "ER")
                {
                    Utility.ShowMessage_Error(Page, "Job Sheet Metal ID already exists!");
                    return;
                }
                if(btnSave.Text == "Update")
                {                   
                    Utility.ShowMessage_Success(Page, "Record Updated Successfully!");
                    Utility.MaintainLogsSpecial("FrmSheetMetalJobConsumption", "Update", hfConsumptionID.Value.ToString());                                                                   
                }
                else
                {                    
                    Utility.ShowMessage_Success(Page, "Record Added Successfully !");
                    Utility.MaintainLogsSpecial("FrmSheetMetalJobConsumption", "Save", msg.ToString());                    
                }
                ResetPartsInfo();
                Bind_Grid();             
                btnSave.Text = "Save";
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
            txtSearchPNum.Text = String.Empty;
            if (ddlSheetMetal.Items.Count > 0)
            {
                ddlSheetMetal.SelectedIndex = 0;
            }
            if (ddlWarehouse.Items.Count > 0)
            {
                ddlWarehouse.SelectedIndex = 0;
            }
            txtDate.Text = String.Empty;
            txtTransactQty.Text = String.Empty;
            hfConsumptionID.Value = "-1";     
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
            if (ddlLookupProjectCode.Items.Count > 0)
            {
                ddlLookupProjectCode.SelectedIndex = 0;
            }
            if (ddlProjectCode.Items.Count > 0)
            {
                ddlProjectCode.SelectedIndex = 0;
            }    
            ResetPartsInfo();
            ShowHideJobNo();         
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
            ObjBOL.Operation = 4;
            if (ddlProjectCode.SelectedValue == "1" && txtSearchPNum.Text != "")
            {
                ObjBOL.JobNo = txtSearchPNum.Text;
            }
            else 
            {
                ObjBOL.productcodeid = Convert.ToInt32(ddlProjectCode.SelectedValue);
            }
            ds = ObjBLL.Return_JobConsumptionDataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                pangvRequititionDetails.Visible = true;
                gvJobSummary.DataSource = ds.Tables[0];
                gvJobSummary.DataBind();
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
            gvJobSummary.DataSource = "";
            gvJobSummary.DataBind();           
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    protected void ddlLookupProjectCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (ddlLookupProjectCode.SelectedIndex > 0)
            {
                //DisabledJobNo(ddlLookupJobID.SelectedItem.Text);
                ddlProjectCode.SelectedValue = ddlLookupProjectCode.SelectedValue;
                ShowHideJobNo();
                Bind_Grid();
            }
            else
            {
                ResetPartsInfo();
                ShowHideJobNo();            
                if (ddlProjectCode.Items.Count > 0)
                {
                    ddlProjectCode.SelectedIndex = 0;
                }
                ResetGrid();
                btnSave.Text = "Save";
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
            int ID = Convert.ToInt32(gvJobSummary.DataKeys[e.NewEditIndex].Values[0]);
            int SheetMetalID = Convert.ToInt32(gvJobSummary.DataKeys[e.NewEditIndex].Values[1]);
            string ProductCodeID = gvJobSummary.DataKeys[e.NewEditIndex].Values[2].ToString();
            string WarehouseID = gvJobSummary.DataKeys[e.NewEditIndex].Values[3].ToString();
            ObjBOL.Operation = 6;
            ObjBOL.ConsumptionID = ID;
            ds = ObjBLL.Return_JobConsumptionDataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if(ID > 0)
                {
                    hfConsumptionID.Value = ID.ToString();
                }
                if(ddlProjectCode.Items.FindByValue(ProductCodeID) != null)
                {
                    ddlProjectCode.SelectedValue = ProductCodeID;
                }
                if(ddlWarehouse.Items.FindByValue(WarehouseID) != null)
                {
                    ddlWarehouse.SelectedValue = WarehouseID;
                }
                ShowHideJobNo();
                txtSearchPNum.Text = ds.Tables[0].Rows[0]["JobID"].ToString();                
                if(ddlSheetMetal.Items.FindByValue(SheetMetalID.ToString()) != null)
                {
                    ddlSheetMetal.SelectedValue = SheetMetalID.ToString();
                }
                txtDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Date"]).ToString("MM/dd/yyyy");
                txtTransactQty.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();
                
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
            int ID = Convert.ToInt32(gvJobSummary.DataKeys[e.RowIndex].Values[0]);
            ObjBOL.Operation = 5;
            ObjBOL.ConsumptionID = ID;                  
            msg = ObjBLL.Return_JobConsumptionString(ObjBOL);            
            if (msg.Trim() == "D")
            {
                Utility.ShowMessage_Success(Page, "Record Deleted Successfully !");
                Utility.MaintainLogsSpecial("FrmSheetMetalJobConsumption", "Delete", ID.ToString());
            }           
            Bind_Grid();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    private void ShowHideJobNo()
    {
        try
        {
            if (ddlProjectCode.SelectedValue == "1")
            {
                dvJob.Visible = true;
                txtSearchPNum.Text = String.Empty;
            }
            else
            {
                dvJob.Visible = false;
                txtSearchPNum.Text = String.Empty;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlProjectCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlProjectCode.SelectedIndex > 0)
            {
                //DisabledJobNo(ddlLookupJobID.SelectedItem.Text); 
                txtSearchPNum.Text = String.Empty;
                ShowHideJobNo();
                Bind_Grid();      
            }
            else
            {
                txtSearchPNum.Text = String.Empty;
                ResetPartsInfo();
                ShowHideJobNo();
                if (ddlProjectCode.Items.Count > 0)
                {
                    ddlProjectCode.SelectedIndex = 0;
                }
                ResetGrid();            
                btnSave.Text = "Save";
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }



    protected void gvJobSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView drv = (DataRowView)e.Row.DataItem;

                int productCodeId = Convert.ToInt32(drv["ProductCodeID"]);

                if (productCodeId == 2 || productCodeId == 3)
                {
                    gvJobSummary.Columns[0].Visible = false; // Column index
                    gvJobSummary.Columns[1].Visible = false; // Column index
                }
                else
                {
                    gvJobSummary.Columns[0].Visible = true;
                    gvJobSummary.Columns[1].Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}