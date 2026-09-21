using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_PreventativeMaintenance_CallStatusReport : System.Web.UI.Page
{
    commonclass1 cls = new commonclass1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControls();
            SetDates();
        }
    }

    private void SetDates()
    {
        try
        {
            txtWarrantyEndDateFrom.Text = "01/01/" + DateTime.Now.Year;
            txtWarrantyEndDateTo.Text = DateTime.Now.ToShortDateString();
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
            DataTable dt = new DataTable();
            cls.Return_DT(dt, "EXEC Get_PreventativeMaintenanceCallStatusReport 1");
            if (dt.Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlStatus, dt);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private string PrepareQuery()
    {
        string query = string.Empty;
        try
        {
            query += " SELECT st.[State], CONVERT(VARCHAR, p.JobOrderDate, 101) AS [Job Order Date], p.JobId, f.ProjectName AS [Project Name], c.City, r.FirstName AS [Sales Rep], ";
            query += " (SELECT STRING_AGG(mc.[Name], ', ') FROM tblSelectedModels sm INNER JOIN tblModelChild mc ON mc.id = sm.ChildModelID WHERE sm.PNumber = f.PNumber ) AS [Models], ";
            query += " CONVERT(VARCHAR, p.InstallationCompletionDate, 101) AS [Installation Completion Date], CONVERT(VARCHAR, p.WarrantyEndDate, 101) AS [Warranty End Date], ";
            query += " '$' + FORMAT(f.NetEqPrice, 'N2') AS [Net Eq Price], s.[Status], CONVERT(VARCHAR, h.DateCalled, 101) AS [Last Call Date], S.ColorCode ";
            query += " FROM tblProjects p ";
            query += " OUTER APPLY ( ";
            query += " SELECT TOP 1 h1.* FROM tblPreventativeMaintenanceCallHistory h1 WHERE h1.JobID = p.JobID AND h1.DateCalled IS NOT NULL ORDER BY h1.DateCalled DESC ";
            query += " ) h ";
            query += " LEFT JOIN tblPFiles f ON f.PNumber = p.ProposalID ";
            query += " LEFT JOIN tblHobartListing r ON r.RepId = f.RepId ";
            query += " LEFT JOIN tblCustomers c ON c.CustomerID = p.CustomerID ";
            query += " LEFT JOIN tblStates st ON st.StateID = c.StateID ";
            query += " LEFT JOIN tblPreventativeMaintenanceCallHistory_Status s ON s.id = h.StatusId ";
            query += " WHERE h.JobID IS NOT NULL ";

            if (ddlStatus.SelectedIndex > 0)
            {
                query += " AND S.Id = " + ddlStatus.SelectedValue;
            }

            if (txtWarrantyEndDateFrom.Text.Trim() != "")
            {
                query += " AND p.WarrantyEndDate >= '" + txtWarrantyEndDateFrom.Text.Trim() + "' ";
            }

            if (txtWarrantyEndDateTo.Text.Trim() != "")
            {
                query += " AND p.WarrantyEndDate <= '" + txtWarrantyEndDateTo.Text.Trim() + "' ";
            }

            query += " ORDER BY s.SortOrder, p.JobID; ";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return query;
    }

    private DataTable ReportData()
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "EXEC Get_PreventativeMaintenanceCallStatusReport 3, '" + txtWarrantyEndDateFrom.Text + "', '" + txtWarrantyEndDateTo.Text + "', " + ddlStatus.SelectedValue;
            cls.Return_DT(dt, query);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            SetDates();
            ddlStatus.SelectedIndex = 0;
            ViewState["dirState"] = string.Empty;
            gvSearch.DataSource = string.Empty;
            gvSearch.DataBind();
            btnExportToExcel.Enabled = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        btnSearch_Click();
    }

    private void btnSearch_Click()
    {
        try
        {
            DataTable dt = ReportData();
            if (dt.Rows.Count > 0)
            {
                ViewState["dirState"] = dt;
                ViewState["dtColumn"] = dt.Columns.Count;
                gvSearch.DataSource = dt;
                gvSearch.DataBind();
                btnExportToExcel.Enabled = true;
            }
            else
            {
                ViewState["dirState"] = string.Empty;
                gvSearch.DataSource = string.Empty;
                gvSearch.DataBind();
                btnExportToExcel.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvSearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell lastCell = e.Row.Cells[e.Row.Cells.Count - 1];
                string hexColor = lastCell.Text.Trim();
                hexColor = hexColor.Replace("&nbsp;", "");
                if (!string.IsNullOrEmpty(hexColor))
                {
                    //e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(hexColor);
                    for (int i = 0; i < e.Row.Cells.Count; i++)
                    {
                        e.Row.Cells[i].BackColor = System.Drawing.ColorTranslator.FromHtml(hexColor);
                    }
                }
            }
            e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void gvSearch_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable dtrslt = (DataTable)ViewState["dirState"];
            if (dtrslt.Rows.Count > 0)
            {
                DataView dataView = new DataView(dtrslt);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);
                gvSearch.DataSource = dataView;
                gvSearch.DataBind();
            }
            else
            {
                dtrslt.DefaultView.Sort = e.SortExpression + "DESC";
                gvSearch.DataSource = dtrslt;
                gvSearch.DataBind();
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

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            btnSearch_Click();
            ExportToExcel();
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
            if (gvSearch.Rows.Count > 0)
            {
                Response.Clear();
                Response.Buffer = true;
                string FileName = "Daily Purchase report from " + txtWarrantyEndDateFrom.Text + " to " + txtWarrantyEndDateTo.Text + ".xls";
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
                    foreach (TableCell cell in gvSearch.HeaderRow.Cells)
                    {
                        for (int i = cell.Controls.Count - 1; i >= 0; i--) // loop backwards when removing
                        {
                            if (cell.Controls[i] is LinkButton)
                            {
                                LinkButton lb = (LinkButton)cell.Controls[i];
                                cell.Controls.RemoveAt(i);
                                cell.Text = lb.Text;
                            }
                        }
                    }

                    gvSearch.RenderControl(hw);
                    hw.Write("<br/>");
                    hw.Write("<br/>");
                    hw.Write("<table border='1' cellspacing='0' cellpadding='5'>");
                    hw.Write("<tr><th>Color</th><th>Status</th></tr>");
                    DataTable dtLegend = new DataTable();
                    cls.Return_DT(dtLegend, "EXEC Get_PreventativeMaintenanceCallStatusReport 2");
                    foreach (DataRow dr in dtLegend.Rows)
                    {
                        string status = dr["Status"].ToString();
                        string color = dr["ColorCode"].ToString();

                        hw.Write("<tr>");
                        hw.Write("<td style='background-color:" + color + "'></td>");
                        hw.Write("<td>" + status + "</td>");
                        hw.Write("</tr>");
                    }

                    hw.Write("</table>");
                    //Style to format numbers to string.
                    string style = @"<style> td, th { color: black; } .textmode { mso-number-format:\@; } </style> <meta http-equiv='Content-Type' content='text/html; charset=UTF-8'>";
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
}