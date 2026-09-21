using BLLAERO;
using BOLAERO;
using OfficeOpenXml;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

public partial class InventoryManagement_FrmStockAllocation : System.Web.UI.Page
{
    BOLStockAllocation ObjBOL = new BOLStockAllocation();
    BLLStockAllocation ObjBLL = new BLLStockAllocation();
    commonclass1 cls = new commonclass1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControls();
        }
    }

    private void BindControls()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 1;
            ObjBOL.PartList = EmptyDT();
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMainPartDetail.DataSource = ds.Tables[0];
                gvMainPartDetail.DataBind();

                bool exists = ds.Tables[0].AsEnumerable().Any(r => r.Field<decimal?>("AllocationPercent") > 0);

                if (exists)
                {
                    btnSave.Text = "Update";
                }
                else
                {
                    btnSave.Text = "Save";
                }
            }
            else
            {
                gvMainPartDetail.DataSource = string.Empty;
                gvMainPartDetail.DataBind();
                btnSave.Text = "Save";
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow row in gvMainPartDetail.Rows)
            {
                Label lblAllocationQty = (Label)row.FindControl("lblAllocationQty");
                HiddenField hfAllocationQty = (HiddenField)row.FindControl("hfAllocationQty");

                lblAllocationQty.Text = hfAllocationQty.Value;

                Label lblKeepInCanada = (Label)row.FindControl("lblKeepInCanada");
                HiddenField hfKeepInCanada = (HiddenField)row.FindControl("hfKeepInCanada");

                lblKeepInCanada.Text = hfKeepInCanada.Value;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        BindControls();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            //DataTable dt = PrepareGridDataForDB();
            //if (dt.Rows.Count == 0)
            //{
            //    Utility.ShowMessage_Error(Page, "Nothing to update !");
            //    return;
            //}

            //ObjBOL.Operation = 2;
            //ObjBOL.PartList = dt;
            //if (Utility.IsAuthorized())
            //{
            //    ObjBOL.UserId = Utility.GetCurrentUser();
            //}

            //string returnStatus = ObjBLL.Return_String(ObjBOL);
            //if (returnStatus.Trim() == "E")
            //{
            //    Utility.ShowMessage_Error(Page, "Database error occured while trying to save changes !!");
            //    return;
            //}

            //if (returnStatus.Trim() == "S")
            //{
            //    Utility.ShowMessage_Success(Page, "Record saved successfully !!");
            //    Utility.MaintainLogs("FrmStockAllocation.aspx", "Save/update");
            //    BindControls();
            //}
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private DataTable EmptyDT()
    {
        DataTable dt = new DataTable();
        try
        {
            dt.TableName = "tableData";
            dt.Columns.Add(new DataColumn("PartId", typeof(int)));
            dt.Columns.Add(new DataColumn("ContainerId", typeof(int)));
            dt.Columns.Add(new DataColumn("FromWarehouseId", typeof(int)));
            dt.Columns.Add(new DataColumn("ToWarehouseId", typeof(int)));
            dt.Columns.Add(new DataColumn("AllocationPercent", typeof(decimal)));
            dt.Columns.Add(new DataColumn("AllocationQty", typeof(int)));
            dt.Columns.Add(new DataColumn("QtyKeepInCanada", typeof(int)));
            dt.Columns.Add(new DataColumn("ShipDate", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Priority", typeof(char)));
            dt.Columns.Add(new DataColumn("Notes", typeof(string)));
            ViewState["tableData"] = dt;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    //private DataTable PrepareGridDataForDB()
    //{
    //    DataTable dt = EmptyDT();
    //    try
    //    {
    //        foreach (GridViewRow row in gvMainPartDetail.Rows)
    //        {
    //            DataRow dr;
    //            if (row.RowType == DataControlRowType.DataRow)
    //            {
    //                string PartId = gvMainPartDetail.DataKeys[row.RowIndex].Values[0].ToString();
    //                string ContainerId = gvMainPartDetail.DataKeys[row.RowIndex].Values[1].ToString();
    //                //string FromWarehouseId = gvMainPartDetail.DataKeys[row.RowIndex].Values[2].ToString();
    //                //string ToWarehouseId = gvMainPartDetail.DataKeys[row.RowIndex].Values[3].ToString();
    //                string FromWarehouseId = "1";
    //                string ToWarehouseId = "5";


    //                dr = dt.NewRow();
    //                TextBox txtAllocationPercent = ((TextBox)row.FindControl("txtAllocationPercent"));
    //                float ap = -1;
    //                if (!float.TryParse(txtAllocationPercent.Text, out ap) || ap <= 0)
    //                {
    //                    continue;
    //                }
    //                HiddenField hfAllocationQty = (HiddenField)row.FindControl("hfAllocationQty");
    //                if (hfAllocationQty.Value.Trim() == "0")
    //                {
    //                    continue;
    //                }
    //                HiddenField hfKeepInCanada = (HiddenField)row.FindControl("hfKeepInCanada");
    //                TextBox txtShipDate = ((TextBox)row.FindControl("txtShipDate"));
    //                DropDownList ddlPriority = ((DropDownList)row.FindControl("ddlPriority"));
    //                TextBox txtNotes = ((TextBox)row.FindControl("txtNotes"));

    //                if (PartId != "")
    //                {
    //                    dr[0] = Convert.ToInt32(PartId);
    //                }
    //                else
    //                {
    //                    dr[0] = 0;
    //                }

    //                if (ContainerId != "")
    //                {
    //                    dr[1] = Convert.ToInt32(ContainerId);
    //                }
    //                else
    //                {
    //                    dr[1] = 0;
    //                }

    //                if (FromWarehouseId != "")
    //                {
    //                    dr[2] = Convert.ToInt32(FromWarehouseId);
    //                }
    //                else
    //                {
    //                    dr[2] = 0;
    //                }

    //                if (ToWarehouseId != "")
    //                {
    //                    dr[3] = Convert.ToInt32(ToWarehouseId);
    //                }
    //                else
    //                {
    //                    dr[3] = 0;
    //                }

    //                if (txtAllocationPercent.Text != "")
    //                {
    //                    dr[4] = Convert.ToDecimal(txtAllocationPercent.Text);
    //                }
    //                else
    //                {
    //                    dr[4] = 0;
    //                }

    //                if (hfAllocationQty.Value != "")
    //                {
    //                    dr[5] = Convert.ToInt32(hfAllocationQty.Value);
    //                }
    //                else
    //                {
    //                    dr[5] = 0;
    //                }

    //                if (hfKeepInCanada.Value != "")
    //                {
    //                    dr[6] = Convert.ToInt32(hfKeepInCanada.Value);
    //                }
    //                else
    //                {
    //                    dr[6] = 0;
    //                }

    //                if (txtShipDate.Text != "")
    //                {
    //                    dr[7] = Utility.ConvertDate(txtShipDate.Text);
    //                }
    //                else
    //                {
    //                    dr[7] = DBNull.Value;
    //                }

    //                if (ddlPriority.SelectedIndex > 0)
    //                {
    //                    dr[8] = ddlPriority.SelectedValue;
    //                }
    //                else
    //                {
    //                    dr[8] = DBNull.Value;
    //                }

    //                dr[9] = txtNotes.Text;

    //                dt.Rows.Add(dr);
    //                dt.AcceptChanges();
    //            }
    //        }
    //        //ViewState["tableData"] = dt;
    //    }
    //    catch (Exception ex)
    //    {
    //        Utility.AddEditException(ex);
    //    }
    //    return dt;
    //}

    //protected void btnAllocate_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ObjBOL.Operation = 3;
    //        if (Utility.IsAuthorized())
    //        {
    //            ObjBOL.UserId = Utility.GetCurrentUser();
    //        }
    //        ObjBOL.PartList = EmptyDT();

    //        string returnStatus = ObjBLL.Return_String(ObjBOL);
    //        if (returnStatus.Trim() == "E")
    //        {
    //            Utility.ShowMessage_Error(Page, "Database error occured while trying to save changes !!");
    //            return;
    //        }

    //        if (returnStatus.Trim() == "ER01")
    //        {
    //            Utility.ShowMessage_Error(Page, "No part to allocate !!");
    //            return;
    //        }

    //        if (returnStatus.Trim() == "S")
    //        {
    //            Utility.ShowMessage_Success(Page, "Records allocated successfully !!");
    //            Utility.MaintainLogs("FrmStockAllocation.aspx", "Allocate");
    //            BindControls();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Utility.AddEditException(ex);
    //    }
    //}

    protected void btnGenerateExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 4;
            ObjBOL.PartList = EmptyDT();
            ds = ObjBLL.Return_DataSet(ObjBOL);

            //Utility.ExportToExcelDT(ds.Tables[0], "Gaffney Allocation");
            DataTable dataTable = ds.Tables[0];
            string fileName = "Geffney Allocation";

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (ExcelPackage excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add(fileName);

                workSheet.Cells["A1"].LoadFromDataTable(dataTable, true);

                int rows = dataTable.Rows.Count + 1;

                // Get column indexes
                int totalCol = dataTable.Columns["Total Available"].Ordinal + 1;
                int percentCol = dataTable.Columns["Allocation % to Gaffney"].Ordinal + 1;
                int percentAmtCol = dataTable.Columns["Quantity to Ship to Gaffney"].Ordinal + 1;
                int remainingCol = dataTable.Columns["Qty to Keep In Canada"].Ordinal + 1;

                // If percent stored as 25 instead of 0.25 use /100
                for (int r = 2; r <= rows; r++)
                {
                    string totalCell = workSheet.Cells[r, totalCol].Address;
                    string percentCell = workSheet.Cells[r, percentCol].Address;
                    string percentAmtCell = workSheet.Cells[r, percentAmtCol].Address;

                    workSheet.Cells[r, totalCol].Formula = "$E" + r + "+$F" + r;
                    workSheet.Cells[r, percentAmtCol].Formula = "IF($G" + r + "=\"\", \"\", ROUND($G" + r + "*$H" + r + "/100,0))";

                    workSheet.Cells[r, remainingCol].Formula = "IF($G" + r + " =\"\", \"\", $G" + r + "-$I" + r + ")";
                }

                // Optional: format percent column
                workSheet.Column(percentCol).Style.Numberformat.Format = "0";

                using (var headerRange = workSheet.Cells[1, 1, 1, dataTable.Columns.Count])
                {
                    headerRange.Style.Font.Bold = true;
                }

                for (int i = 1; i <= dataTable.Columns.Count; i++)
                {
                    workSheet.Column(i).Width = 20;
                }

                workSheet.Workbook.CalcMode = ExcelCalcMode.Automatic;
                workSheet.Calculate();
                excel.Workbook.FullCalcOnLoad = true;

                var stream = new MemoryStream();
                excel.SaveAs(stream);
                var content = stream.ToArray();

                System.Web.HttpContext.Current.Response.Clear();
                System.Web.HttpContext.Current.Response.ContentType =
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                System.Web.HttpContext.Current.Response.AddHeader(
                    "content-disposition",
                    "attachment; filename=" + fileName + ".xlsx");
                System.Web.HttpContext.Current.Response.BinaryWrite(content);
                System.Web.HttpContext.Current.Response.End();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}