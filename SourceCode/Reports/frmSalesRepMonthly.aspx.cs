using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using OfficeOpenXml.Style;
using System.IO;
using OfficeOpenXml;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI;
using BOLAERO;
using BLLAERO;

public partial class Reports_frmSalesRepMonthly : System.Web.UI.Page
{
    commonclass1 clscon = new commonclass1();   
    BOLProposalSearch ObjBOL = new BOLProposalSearch();
    BLLProposalSearch ObjBLL = new BLLProposalSearch();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Utility.IsAuthorized())
            {
                if (!IsPostBack)
                {
                    int Month = DateTime.Now.Month + 2;
                    txtFromDate.Text = DateTime.Now.Month + "/01/" + DateTime.Now.Year;
                    txtToDate.Text = DateTime.Now.AddMonths(0).Month + "/" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(0).Month) + "/" + DateTime.Now.AddMonths(0).Year;
                    Bind_Controls();
                }
            }

        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void Bind_Controls()
    {
        try
        {
            DataTable dt = new DataTable();
            clscon.Return_DT(dt, "EXEC [dbo].[Get_SalesRepMonthly_Excel] 1");
            if(dt.Rows.Count>0)
            {
                Utility.BindDropDownListAll(ddlProjectManager, dt);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }


    // check if data filled in required fields
    private Boolean ValidationCheck()
    {
        try
        {
            if (txtFromDate.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter From Date. !!");
                txtFromDate.Focus();
                return false;
            }
            if (txtToDate.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter To Date. !!");
                txtToDate.Focus();
                return false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    private DataTable ReportData_Excel()
    {
        DataTable dt = new DataTable();
        try
        {
            DateTime dtfrom = Convert.ToDateTime(txtFromDate.Text);
            DateTime dtto = Convert.ToDateTime(txtToDate.Text);
            string strDateFrom = dtfrom.ToString("MM/dd/yyyy");
            string strDateTo = dtto.ToString("MM/dd/yyyy");
            if(ddlProjectManager.SelectedIndex>0)
            {
                clscon.Return_DT(dt, "EXEC [dbo].[Get_SalesRepMonthly_Excel] 2,'" + ddlProjectManager.SelectedValue + "','" + strDateFrom + "','" + strDateTo + "'");
            }
            else
            {
                clscon.Return_DT(dt, "EXEC [dbo].[Get_SalesRepMonthly_Excel] 2,'" + null + "','" + strDateFrom + "','" + strDateTo + "'");
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }
    private void Reset_Grid()
    {
        try
        {
            gvSalesRepMonthly.DataSource = "";
            gvSalesRepMonthly.DataBind();
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
            txtFromDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
            txtToDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
            int Month = DateTime.Now.Month + 2;
            txtFromDate.Text = DateTime.Now.Month + "/01/" + DateTime.Now.Year;
            txtToDate.Text = DateTime.Now.AddMonths(0).Month + "/" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(0).Month) + "/" + DateTime.Now.AddMonths(0).Year;
            Reset_Grid();
            if (ddlProjectManager.Items.Count > 0)
            {
                ddlProjectManager.SelectedIndex = 0;
            }
            lblRecordsCount.Text = String.Empty;
            lblRecordsCount.Visible = false;
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
                DataTable dt_SalesRepExcelData = ReportData_Excel();
                if (dt_SalesRepExcelData.Rows.Count > 0)
                {
                    txtFromDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
                    txtToDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
                    int Month = DateTime.Now.Month + 2;
                    txtFromDate.Text = DateTime.Now.Month + "/01/" + DateTime.Now.Year;
                    txtToDate.Text = DateTime.Now.AddMonths(0).Month + "/" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(0).Month) + "/" + DateTime.Now.AddMonths(0).Year;
                    string sheetName = "Sales Rep Monthly Sales Report from " + txtFromDate.Text + " to " + txtToDate.Text;
                    List<ExcelHeaderGroup> groups = new List<ExcelHeaderGroup>
                    {
                        new ExcelHeaderGroup { HeaderText="AEROWERKS ID", StartColumn=1, EndColumn=2, FontColor= Color.FromArgb(139, 0, 0)  },

                        new ExcelHeaderGroup { HeaderText="PROJECT INFORMATION", StartColumn=3, EndColumn=6, FontColor=Color.FromArgb(75, 0, 130) },

                        new ExcelHeaderGroup { HeaderText="AW TOTAL EQUIP PKG.", StartColumn=7, EndColumn=7, FontColor=Color.Red },

                        new ExcelHeaderGroup { HeaderText="CONSULTANT", StartColumn=8, EndColumn=9, FontColor=Color.FromArgb(0,32,96) },

                        new ExcelHeaderGroup { HeaderText="CONVEYOR SPEC", StartColumn=10, EndColumn=11,  FontColor=Color.FromArgb(255,192,0) },

                        new ExcelHeaderGroup { HeaderText="BLOWER DRYER SPEC", StartColumn=12, EndColumn=13, FontColor=Color.FromArgb(191, 87, 0) },

                        new ExcelHeaderGroup { HeaderText="WASTE COLLECTOR SPEC", StartColumn=14, EndColumn=15, FontColor=Color.FromArgb(0,176,240) },

                        new ExcelHeaderGroup { HeaderText="FOOD SERVICE EQUIPMENT DEALER", StartColumn=16, EndColumn=17, FontColor=Color.FromArgb(0,176,80) },                    

                        new ExcelHeaderGroup { HeaderText="HOBART ITW or REP. GROUP", StartColumn=18, EndColumn=20, FontColor=Color.FromArgb(31, 78, 121) }
                    };

                    ExportToExcelDT(dt_SalesRepExcelData, sheetName, groups, "Dollers");
                }
                else
                {
                    Utility.ShowMessage_Error(Page, "No Matching Data Found !");
                }
            }

        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    public class ExcelHeaderGroup
    {
        public string HeaderText { get; set; }
        public int StartColumn { get; set; }
        public int EndColumn { get; set; }
        public Color FontColor { get; set; }
    }

    public static void ExportToExcelDT(DataTable dataTable, string fileName, List<ExcelHeaderGroup> headerGroups, string colName)
    {
        try
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                ExcelWorksheet workSheet = excel.Workbook.Worksheets.Add(fileName);

                // =========================
                // ROW 1 - GROUP HEADERS
                // =========================
                foreach (ExcelHeaderGroup grp in headerGroups)
                {
                    var range = workSheet.Cells[1, grp.StartColumn, 1, grp.EndColumn];
                    range.Merge = true;
                    range.Value = grp.HeaderText;
                    range.Style.Font.Name = "Aptos Narrow";
                    range.Style.Font.Bold = true;
                    range.Style.Font.Size = 12;
                    range.Style.Font.Color.SetColor(grp.FontColor);

                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    if (grp.HeaderText == "AW TOTAL EQUIP PKG.")
                    {
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    }

                }

                // =========================
                // ROW 2 - COLUMN HEADERS
                // =========================
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    var cell = workSheet.Cells[2, col + 1];

                    string headerText = dataTable.Columns[col].ColumnName;
                    if (headerText.Contains("Conveyor Prime Spec"))
                    {
                        headerText = "Prime Spec";
                    }
                    if (headerText.Contains("Blower Dryer Prime Spec"))
                    {
                        headerText = "Prime Spec";
                    }
                    if (headerText.Contains("Waste Collector Prime Spec"))
                    {
                        headerText = "Prime Spec";
                    }
                    if (headerText.Contains("Conveyor Alt 1"))
                    {
                        headerText = "Alternate 1";
                    }
                    if (headerText.Contains("Blower Dryer Alt 1"))
                    {
                        headerText = "Alternate 1";
                    }
                    if (headerText.Contains("Waste Collector Alt 1"))
                    {
                        headerText = "Alternate 1";
                    }
                    if(headerText.Contains(colName))
                    {
                        headerText = "DOLLERS ($)";
                    }
                    cell.Value = headerText;
                    cell.Style.Font.Name= "Aptos Narrow";
                    cell.Style.Font.Bold = true;
                    cell.Style.WrapText = true;
                    cell.Style.Font.Size = 12;

                    cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    cell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    // Match header color from group
                    ExcelHeaderGroup group = headerGroups.FirstOrDefault(x => col + 1 >= x.StartColumn && col + 1 <= x.EndColumn);


                    if (group != null)
                    {
                        cell.Style.Font.Color.SetColor(group.FontColor);
                    }

                    // Special Red Dollar Header
                    if (dataTable.Columns[col].ColumnName.Contains(colName))
                    {
                        cell.Style.Font.Color.SetColor(Color.Red);
                        cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    }


                }

                // =========================
                // DATA ROWS
                // =========================
                for (int row = 0; row < dataTable.Rows.Count; row++)
                {
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        var cell = workSheet.Cells[row + 3, col + 1];

                        cell.Value = dataTable.Rows[row][col];
                        cell.Style.Font.Name = "Aptos Narrow";
                        ExcelHeaderGroup group = headerGroups.FirstOrDefault(x => col + 1 >= x.StartColumn && col + 1 <= x.EndColumn);

                        if (group != null)
                        {
                            cell.Style.Font.Color.SetColor(group.FontColor);
                            cell.Style.Font.Size = 11;
                        }

                        string columnName = dataTable.Columns[col].ColumnName;

                        if (columnName.Contains(colName))
                        {
                            decimal amount;

                            if (decimal.TryParse(Convert.ToString(dataTable.Rows[row][col]), out amount))
                            {
                                cell.Value = amount;

                                cell.Style.Numberformat.Format = "$#,##0.00";

                                cell.Style.Font.Color.SetColor(Color.Red);

                                cell.Style.Font.Size = 11;
                            }
                        }
                    }
                }

                // =========================
                // ROW HEIGHTS
                // =========================
                workSheet.Row(1).Height = 22;
                workSheet.Row(2).Height = 35;

                // =========================
                // FREEZE TOP ROWS
                // =========================
                workSheet.View.FreezePanes(3, 1);


                //Zoom Size
                workSheet.View.ZoomScale = 80;
                // =========================
                // AUTOFIT
                // =========================
                workSheet.Cells.AutoFitColumns();
                workSheet.Column(1).Width = 18;
                workSheet.Column(4).Width = 45;
                workSheet.Column(7).Width = 25;
                workSheet.Column(10).Width = 25;
                workSheet.Column(11).Width = 25;
                workSheet.Column(12).Width = 25;
                workSheet.Column(13).Width = 25;
                workSheet.Column(14).Width = 30;
                workSheet.Column(15).Width = 25;
                workSheet.Column(17).Width = 20;
                workSheet.Column(18).Width = 20;
                workSheet.Column(19).Width = 20;
                workSheet.Column(20).Width = 20;

                // =========================
                // DOWNLOAD
                // =========================
                MemoryStream stream = new MemoryStream();

                excel.SaveAs(stream);

                HttpContext.Current.Response.Clear();

                HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + fileName + ".xlsx");

                HttpContext.Current.Response.BinaryWrite(stream.ToArray());

                HttpContext.Current.Response.End();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidationCheck() == true)
            {
                Bind_Grid();
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
            DataTable dt = ReportData_Excel();
            if(dt.Rows.Count>0)
            {
                ViewState["dirState"] = dt;
                gvSalesRepMonthly.DataSource = dt;
                gvSalesRepMonthly.DataBind();
                lblRecordsCount.Text = "Total No. of Records:" + dt.Rows.Count.ToString();
                lblRecordsCount.Visible = true;
            }
            else
            {
                Reset_Grid();
                lblRecordsCount.Text = String.Empty;
                lblRecordsCount.Visible = false;
                Utility.ShowMessage_Error(Page, "No Matching Data Found !");
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private string GridViewSortDirection
    {
        get { return ViewState["SortDirection"] as string ?? "DESC"; }
        set { ViewState["SortDirection"] = value; }
    }

    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;

            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }
        return GridViewSortDirection;
    }

    protected void gvSalesRepMonthly_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {
        try
        {
            DataTable dtrslt = (DataTable)ViewState["dirState"];
            if (dtrslt.Rows.Count > 0)
            {
                DataView dataView = new DataView(dtrslt);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);
                gvSalesRepMonthly.DataSource = dataView;
                gvSalesRepMonthly.DataBind();              
            }
            else
            {
                dtrslt.DefaultView.Sort = e.SortExpression + "DESC";
                gvSalesRepMonthly.DataSource = dtrslt;
                gvSalesRepMonthly.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvSalesRepMonthly_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Change the mouse cursor to Hand symbol to show the user the cell is selectable
                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.cursor = 'Pointer'";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                //e.Row.Attributes["OnClientClick"] = "SetTarget();";
                e.Row.ToolTip = "Click to select this row.";
                //Attach the click event to each cells
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvSalesRepMonthly, "Select$" + e.Row.RowIndex);
                //PrepareDT();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvSalesRepMonthly_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                Session["PNumber"] = "";
                DataSet ds = new DataSet();
                ObjBOL.Operation = 3;
                int iIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvRow = gvSalesRepMonthly.Rows[iIndex];
                string PNumber = Server.HtmlDecode(gvRow.Cells[0].Text);
                ObjBOL.PNumber = PNumber;
                ds = ObjBLL.GetProposalSearch(ObjBOL);
                Session["PNumber"] = ds.Tables[0].Rows[0]["Pnumber"].ToString();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenWindow", "window.open('../SalesManagement/FrmProposals.aspx','_blank')", true);                
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}