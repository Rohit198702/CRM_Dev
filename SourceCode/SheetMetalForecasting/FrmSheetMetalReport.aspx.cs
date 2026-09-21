using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BOLAERO;
using BLLAERO;
using System.Data;

public partial class SheetMetalForecasting_FrmSheetMetalReport : System.Web.UI.Page
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
            ds = ObjBLL.Return_SheetMetalReportDataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlSheetMetalID, ds.Tables[0]);
            }            
            if (ds.Tables[2].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlLength, ds.Tables[2]);
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlWidth, ds.Tables[3]);
            }
            if (ds.Tables[4].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlWarehouse, ds.Tables[4]);
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
            gvJobSheetForecasting.DataSource = "";
            gvJobSheetForecasting.DataBind();
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
            DataSet ds = new DataSet();
            ObjBOL.Operation = 2;
            if (ddlSheetMetalID.SelectedIndex > 0)
            {
                ObjBOL.id = Convert.ToInt32(ddlSheetMetalID.SelectedValue);
            }           
            if(ddlLength.SelectedIndex>0)
            {
                ObjBOL.length = Convert.ToInt32(ddlLength.SelectedValue);
            }
            if (ddlWidth.SelectedIndex > 0)
            {
                ObjBOL.width = Convert.ToInt32(ddlWidth.SelectedValue);
            }
            if(ddlStatus.SelectedIndex>0)
            {
                ObjBOL.SheetMetalStatus = Convert.ToInt32(ddlStatus.SelectedValue);
            }           
            ds = ObjBLL.Return_SheetMetalReportDataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvJobSheetForecasting.DataSource = ds.Tables[0];
                gvJobSheetForecasting.DataBind();
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
            if (ddlLength.Items.Count > 0)
            {
                ddlLength.SelectedIndex = 0;
            }
            if (ddlWidth.Items.Count > 0)
            {
                ddlWidth.SelectedIndex = 0;
            }           
            if (ddlStatus.Items.Count > 0)
            {
                ddlStatus.SelectedIndex = 0;
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
            Utility.ExportToExcelGrid(gvJobSheetForecasting, "Sheet Metal Details");
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvJobSheetForecasting_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                GridViewRow clickedRow = gvJobSheetForecasting.Rows[Convert.ToInt32(e.CommandArgument)];
                Label lblSheetMetalID = (Label)clickedRow.FindControl("lblID");
                Label lblpartid = (Label)clickedRow.FindControl("lblsheetmetalid");
                Label lblsheetmetaldesc = (Label)clickedRow.FindControl("lblsheetmetaldesc");
                LoadModal(lblSheetMetalID.Text);
                string title = lblpartid.Text + " " + lblsheetmetaldesc.Text;
                SetTitle(title);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void LoadModal(string SheetMetalID)
    {
        try
        {
            if (SheetMetalID.Trim() != "")
            {
                ShowWarehouseData(SheetMetalID);
                SheetMetalModel.Show();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ShowWarehouseData(string SheetMetalID)
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 3;
            ObjBOL.id = Convert.ToInt32(SheetMetalID);
            ObjBOL.warehouseid = Convert.ToInt32(ddlWarehouse.SelectedValue);
            ds = ObjBLL.Return_SheetMetalReportDataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSheetMetalWarehouseDeatils.DataSource = ds.Tables[0];
                gvSheetMetalWarehouseDeatils.DataBind();
            }
            else
            {
                ResetWarehouseGrid();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetWarehouseGrid()
    {
        try
        {
            gvSheetMetalWarehouseDeatils.DataSource = "";
            gvSheetMetalWarehouseDeatils.DataBind();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvJobSheetForecasting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.cursor = 'Pointer'";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.ToolTip = "Click to View Warehouse Details";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvJobSheetForecasting, "Select$" + e.Row.RowIndex);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    private void SetTitle(string Title)
    {
        try
        {
            lblSheetMetalTitleInModal.Text = Title;           
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}