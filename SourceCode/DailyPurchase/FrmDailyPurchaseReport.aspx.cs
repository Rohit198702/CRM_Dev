using System;
using BLLAERO;
using BOLAERO;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Web.UI;
using System.IO;

public partial class DailyPurchase_FrmDailyPurchaseReport : System.Web.UI.Page
{
    BOLDailyPurchase ObjBOL = new BOLDailyPurchase();
    BLLDailyPurchase ObjBLL = new BLLDailyPurchase();
    ReportDocument rprt = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindControls();
                SetDates();
                BindGrid_15Days();
            }

            GridViewHelper helper = new GridViewHelper(this.gvExportToExcel);
            helper.RegisterGroup("Requester", true, true);
            helper.GroupHeader += new GroupEvent(helper_GroupHeader);
            helper.GroupHeader += new GroupEvent(helper_Header);
            helper.GroupSummary += new GroupEvent(helper_Bug);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void helper_GroupHeader(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == "Requester")
        {
            row.Cells[0].BackColor = Color.LightGray;
        }
    }

    private void helper_Footer(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == "Requester")
        {

        }
    }

    private void helper_Bug(string groupName, object[] values, GridViewRow row)
    {

    }

    private void helper_Header(string groupName, object[] values, GridViewRow row)
    {
        if (groupName == null) return;
        row.Cells[0].Font.Size = 14;
        row.Cells[0].Font.Bold = true;
        if (groupName != null)
        {
            int count = (int)ViewState["dtColumn"];
            row.Cells[0].ColumnSpan = count;
            row.Cells[0].HorizontalAlign = HorizontalAlign.Left;
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // Required to avoid runtime error
    }

    private void SetDates()
    {
        try
        {
            txtOrderDateFrom.Text = DateTime.Now.AddDays(-15).ToString("MM/dd/yyyy");
            txtOrderDateTo.Text = DateTime.Now.ToString("MM/dd/yyyy");
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
            ObjBOL.Operation = 1;
            DataSet ds = ObjBLL.Return_DataSet(ObjBOL);
            int tableIndex = 0;
            if (ds.Tables[tableIndex].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlPOHeaderList, ds.Tables[tableIndex]);
            }

            tableIndex = 1;
            if (ds.Tables[tableIndex].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlVendor, ds.Tables[tableIndex]);
            }

            tableIndex = 2;
            if (ds.Tables[tableIndex].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlPart, ds.Tables[tableIndex]);
            }

            tableIndex = 3;
            if (ds.Tables[tableIndex].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlRequester, ds.Tables[tableIndex]);
            }

            //tableIndex = 4;
            //if (ds.Tables[tableIndex].Rows.Count > 0)
            //{
            //    Utility.BindDropDownListAll(ddlProject, ds.Tables[tableIndex]);
            //}

            //tableIndex = 5;
            //if (ds.Tables[tableIndex].Rows.Count > 0)
            //{
            //    Utility.BindDropDownList(ddlDepartment, ds.Tables[tableIndex]);
            //}

            //tableIndex = 6;
            //if (ds.Tables[tableIndex].Rows.Count > 0)
            //{
            //    Utility.BindDropDownList(ddlUM, ds.Tables[tableIndex]);
            //}

            //tableIndex = 7;
            //if (ds.Tables[tableIndex].Rows.Count > 0)
            //{
            //    Utility.BindDropDownListAll(ddlOrderStatus, ds.Tables[tableIndex]);
            //}
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
            if (txtOrderDateFrom.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter From Date !");
                txtOrderDateFrom.Focus();
                return false;
            }

            if (txtOrderDateTo.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter To Date !");
                txtOrderDateTo.Focus();
                return false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    private DataTable ReportData()
    {
        DataTable dt = new DataTable();
        try
        {
            ObjBOL.Operation = 10;
            if (ddlPOHeaderList.SelectedIndex > 0)
            {
                ObjBOL.DailyPurchaseId = Int32.Parse(ddlPOHeaderList.SelectedValue);
            }

            if (ddlVendor.SelectedIndex > 0)
            {
                ObjBOL.VendorId = Int32.Parse(ddlVendor.SelectedValue);
            }

            if (ddlPart.SelectedIndex > 0)
            {
                ObjBOL.PartId = Int32.Parse(ddlPart.SelectedValue);
            }

            if (ddlRequester.SelectedIndex > 0)
            {
                ObjBOL.RequesterId = Int32.Parse(ddlRequester.SelectedValue);
            }

            if (ddlOrderStatus.SelectedIndex > 0)
            {
                ObjBOL.OrderStatus = ddlOrderStatus.SelectedValue;
            }

            if (txtOrderDateFrom.Text.Trim() != "")
            {
                ObjBOL.OrderDate = Utility.ConvertDate(txtOrderDateFrom.Text);
            }

            if (txtOrderDateTo.Text.Trim() != "")
            {
                ObjBOL.ReceivedDate = Utility.ConvertDate(txtOrderDateTo.Text);
            }
            DataSet ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dt = ds.Tables[0];
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    private void GenerateReport()
    {
        try
        {
            if (ValidationCheck())
            {
                DataTable dt = ReportData();
                if (dt.Rows.Count > 0)
                {
                    rprt.Load(Server.MapPath("~/DailyPurchase/rptDailyPurchase.rpt"));
                    if (dt.Rows.Count > 0)
                    {
                        TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                        txtheader.Text = "Daily Purchase Report - " + DateTime.Now.ToString("dddd, dd MMMM yyyy").Replace(',', ' ');
                        rprt.SetDataSource(dt);
                        rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
                    }
                }
                else
                {
                    Utility.ShowMessage_Error(Page, "Records not found !");
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        finally
        {
            rprt.Close();
            rprt.Dispose();
        }
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        GenerateReport();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnCancel_Click();
    }

    private void btnCancel_Click()
    {
        try
        {
            if (ddlPOHeaderList.Items.Count > 0)
            {
                ddlPOHeaderList.SelectedIndex = 0;
            }

            if (ddlVendor.Items.Count > 0)
            {
                ddlVendor.SelectedIndex = 0;
            }

            if (ddlPart.Items.Count > 0)
            {
                ddlPart.SelectedIndex = 0;
            }

            if (ddlRequester.Items.Count > 0)
            {
                ddlRequester.SelectedIndex = 0;
            }

            if (ddlProject.Items.Count > 0)
            {
                ddlProject.SelectedIndex = 0;
            }

            txtOrderDateFrom.Text = string.Empty;
            txtOrderDateTo.Text = string.Empty;

            if (ddlOrderStatus.Items.Count > 0)
            {
                ddlOrderStatus.SelectedIndex = 0;
            }

            SetDates();

            gvDailyPurchaseDetail.DataSource = string.Empty;
            gvDailyPurchaseDetail.DataBind();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidationCheck())
            {
                BindExportGrid();
                //string FileName = "Daily Purchase report from " + txtOrderDateFrom.Text + " to " + txtOrderDateTo.Text;
                //Utility.ExportToExcelGrid(gvExportToExcel, FileName);
                ExportToExcel();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        finally
        {
            rprt.Close();
            rprt.Dispose();
        }
    }

    private void BindExportGrid()
    {
        try
        {
            if (ValidationCheck())
            {
                DataTable dt = ReportData();
                DataView dv = dt.DefaultView;
                dv.Sort = "Requester ASC";
                DataTable sortedDt = dv.ToTable();

                if (sortedDt.Columns.Contains("Requester") && sortedDt.Columns[0].ColumnName != "Requester")
                {
                    sortedDt.Columns["Requester"].SetOrdinal(0);
                }

                ViewState["dtColumn"] = sortedDt.Columns.Count;
                gvExportToExcel.DataSource = sortedDt;
                gvExportToExcel.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ExportToExcel()
    {
        try
        {
            if (gvExportToExcel.Rows.Count > 0)
            {
                Response.Buffer = true;
                string FileName = "Daily Purchase report from " + txtOrderDateFrom.Text + " to " + txtOrderDateTo.Text + ".xls";
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    gvExportToExcel.AllowPaging = false;
                    GridViewRow Row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                    TableHeaderCell cell = new TableHeaderCell();
                    int count = (int)ViewState["dtColumn"];
                    cell.Text = "Daily Purchase report from " + txtOrderDateFrom.Text + " to " + txtOrderDateTo.Text;
                    cell.ColumnSpan = count;
                    cell.Font.Size = 20;
                    cell.HorizontalAlign = HorizontalAlign.Left;
                    Row.Controls.Add(cell);
                    gvExportToExcel.HeaderRow.ForeColor = Color.White;
                    gvExportToExcel.HeaderRow.Parent.Controls.AddAt(0, Row);
                    foreach (GridViewRow row in gvExportToExcel.Rows)
                    {
                        ;
                        if (row.RowType == DataControlRowType.DataRow)
                        {
                            for (int i = 0; i < row.Cells.Count; i++)
                            {
                                if (i == 5 || i == 6)
                                {
                                    gvExportToExcel.HeaderRow.Cells[i].Style.Add("background-color", "#0856A1");
                                    row.Cells[i].HorizontalAlign = HorizontalAlign.Right;
                                    row.Cells[i].VerticalAlign = VerticalAlign.Top;
                                }
                                else
                                {
                                    gvExportToExcel.HeaderRow.Cells[i].Style.Add("background-color", "#0856A1");
                                    row.Cells[i].HorizontalAlign = HorizontalAlign.Left;
                                    row.Cells[i].VerticalAlign = VerticalAlign.Top;
                                }
                            }
                        }
                    }
                    gvExportToExcel.RenderControl(hw);
                    //Style to format numbers to string.
                    string style = @"<style> .textmode { mso-number-format:\@; } </style> <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }

            }
            else
            {
                Utility.ShowMessage_Error(Page, "No Matching Data Found !!");
            }
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
            if (ddlPOHeaderList.SelectedIndex > 0)
            {
                ObjBOL.Operation = 3;
                ObjBOL.DailyPurchaseId = Int32.Parse(ddlPOHeaderList.SelectedValue);
                DataSet ds = ObjBLL.Return_DataSet(ObjBOL);

                if (ds.Tables[1].Rows.Count > 0)
                {
                    gvDailyPurchaseDetail.DataSource = ds.Tables[1];
                    gvDailyPurchaseDetail.DataBind();
                }
                else
                {
                    gvDailyPurchaseDetail.DataSource = string.Empty;
                    gvDailyPurchaseDetail.DataBind();
                }
            }
            else
            {
                Utility.ShowMessage_Error(Page, "Please select PO!!");
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void BindGrid_15Days()
    {
        try
        {
            ObjBOL.Operation = 11;
            DataSet ds = ObjBLL.Return_DataSet(ObjBOL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDailyPurchaseDetail.DataSource = ds.Tables[0];
                gvDailyPurchaseDetail.DataBind();
            }
            else
            {
                gvDailyPurchaseDetail.DataSource = string.Empty;
                gvDailyPurchaseDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvExportToExcel_DataBound(object sender, EventArgs e)
    {
        try
        {
            for (int i = gvExportToExcel.Rows.Count - 1; i > 0; i--)
            {
                GridViewRow row = gvExportToExcel.Rows[i];
                GridViewRow previousRow = gvExportToExcel.Rows[i - 1];

                Label lblCurrent = row.FindControl("lblRequester") as Label;
                Label lblPrevious = previousRow.FindControl("lblRequester") as Label;

                if (lblCurrent.Text == lblPrevious.Text)
                {
                    if (previousRow.Cells[0].RowSpan == 0)
                    {
                        if (row.Cells[0].RowSpan == 0)
                        {
                            previousRow.Cells[0].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[0].RowSpan = row.Cells[0].RowSpan + 1;
                        }
                        row.Cells[0].Visible = false;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}