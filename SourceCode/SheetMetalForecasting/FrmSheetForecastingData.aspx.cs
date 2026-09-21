using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using BOLAERO;
using BLLAERO;
using System.Data;
using System.Web;

public partial class SheetMetalForecasting_FrmSheetForecastingData : System.Web.UI.Page
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

    private void ReportData()
    {
        try
        {
            DataSet ds = new DataSet();

            ObjBOL.Operation = 1;  

            if (ddlWarehouse.SelectedIndex > 0)
                ObjBOL.warehouseid = Convert.ToInt32(ddlWarehouse.SelectedValue);        

            if (ddlSheetMetalID.SelectedIndex > 0)
                ObjBOL.id = Convert.ToInt32(ddlSheetMetalID.SelectedValue);

            if (!string.IsNullOrEmpty(txtFromdate.Text))
                ObjBOL.FromDate = Convert.ToDateTime(txtFromdate.Text);
            else
                ObjBOL.FromDate = null;

            ds = ObjBLL.Return_ForecastReportDataSet(ObjBOL);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];

                if (!dt.Columns.Contains("C/S"))
                    dt.Columns.Add("C/S", typeof(int));

                foreach (DataRow dr in dt.Rows)
                {
                    int openingStock = 0;

                    int.TryParse(dr["O/S"].ToString(), out openingStock);

                    int totalUsage = 0;

                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.ColumnName == "PartID#" ||
                            col.ColumnName == "Material" ||
                            col.ColumnName == "Gauge" ||
                            col.ColumnName == "Width" ||
                            col.ColumnName == "Length" ||                            
                            col.ColumnName == "Lead Time" ||
                            col.ColumnName == "Qty on skid" ||
                            col.ColumnName == "Min" ||
                            col.ColumnName == "Max" ||
                            col.ColumnName == "O/S" ||
                            col.ColumnName == "C/S")
                            continue;

                        // Reset value for every column
                        int value = 0;

                        int.TryParse(dr[col].ToString(), out value);

                        totalUsage += value;
                    }

                    dr["C/S"] = openingStock - totalUsage;
                }
                DataRow grandRow = dt.NewRow();
                grandRow[1] = "Grand Total";

                for (int col = 10; col < dt.Columns.Count-1; col++)
                {
                    int grandTotal = 0;

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr[col] != DBNull.Value)
                        {
                            grandTotal += Convert.ToInt32(dr[col]);
                        }
                    }

                    // If total is 0, show blank
                    grandRow[col] = grandTotal == 0
                        ? (object)DBNull.Value
                        : grandTotal;
                }

                dt.Rows.Add(grandRow);

                gvJobSheetForecasting.DataSource = dt;
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
            if (ddlWarehouse.Items.Count > 0)
            {
                ddlWarehouse.SelectedIndex = 0;
            }        
            txtFromdate.Text = String.Empty;
            txtToDate.Text = String.Empty;
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

    protected void gvJobSheetForecasting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView grid = (GridView)sender;
                // Material Column               
                e.Row.Cells[1].Width = Unit.Pixel(320);                
                // Dynamic columns start from index 2   
                for (int i = 9; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                    e.Row.Cells[i].Width = Unit.Pixel(80);

                }
                int lastColumn = e.Row.Cells.Count - 1;
               // e.Row.Cells[lastColumn].BackColor = System.Drawing.Color.Yellow;
                e.Row.Cells[lastColumn].Attributes.Add(
                    "style",
                    "font-weight:bold !important;font-size:18px !important;"
                );
                if (e.Row.Cells[1].Text == "Grand Total")
                {

                    // Merge Sr No, Part ID, Material, Gauge, Width
                    e.Row.Cells[1].ColumnSpan = 10;

                    // Hide merged cells
                    e.Row.Cells[0].Visible = false;
                    e.Row.Cells[2].Visible = false;
                    e.Row.Cells[3].Visible = false;
                    e.Row.Cells[4].Visible = false;
                    e.Row.Cells[5].Visible = false;
                    e.Row.Cells[6].Visible = false;
                    e.Row.Cells[7].Visible = false;
                    e.Row.Cells[8].Visible = false;
                    e.Row.Cells[9].Visible = false;
                    e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        cell.Attributes.Add(
                            "style",
                            "font-weight:bold !important;font-size:18px !important;"
                        );
                    }
                    for (int i = 10; i < e.Row.Cells.Count - 1; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvJobSheetForecasting_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Height = Unit.Pixel(150);
                for (int i = 0; i <= 9; i++)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.Color.DarkGray;
                    e.Row.Cells[i].BorderColor = System.Drawing.Color.Black;
                    e.Row.Cells[i].BorderWidth = Unit.Pixel(1);
                    e.Row.Cells[i].Text =
                    "<div style='height:140px;" +
                    "display:flex;" +
                    "align-items:center;" +         // Vertical Center
                    "justify-content:center;" +     // Horizontal Center
                    "writing-mode:vertical-rl;" +
                    "transform:rotate(180deg);" +   // Bottom to Top
                    "font-weight:bold;" +
                    "color:black;" +
                    "white-space:nowrap;'>" +
                    e.Row.Cells[i].Text +
                    "</div>";
                }
                // Start from Week Columns
                for (int i = 10; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                    e.Row.Cells[i].BorderColor = System.Drawing.Color.Black;
                    e.Row.Cells[i].BorderWidth = Unit.Pixel(1);
                    e.Row.Cells[i].Width = Unit.Pixel(45);

                    e.Row.Cells[i].Text =
                    "<div style='height:140px;" +
                    "display:flex;" +
                    "align-items:center;" +         // Vertical Center
                    "justify-content:center;" +     // Horizontal Center
                    "writing-mode:vertical-rl;" +
                    "transform:rotate(180deg);" +   // Bottom to Top
                    "font-weight:bold;" +
                    "color:black;" +
                    "white-space:nowrap;'>" +
                    e.Row.Cells[i].Text +
                    "</div>";
                }
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
                Utility.ExportToExcelGrid(gvJobSheetForecasting, "Sheet Forecasting");
            }
            
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void txtFromdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DateTime toDate = Convert.ToDateTime(txtFromdate.Text).AddMonths(3);
            string formattedDate = toDate.ToString("MM/dd/yyyy");
            txtToDate.Text = formattedDate;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}