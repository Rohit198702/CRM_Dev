using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mafsi_FrmMafsiDashboard : System.Web.UI.Page
{
    commonclass1 cls = new commonclass1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControls();
            ddlStakeholder_SelectedIndexChanged();
            HideAllGrids();
            //SetDates();
        }
    }

    private void SetDates()
    {
        try
        {
            txtFromDate.Text = DateTime.Now.Month + "/01/" + DateTime.Now.Year;
            txtToDate.Text = DateTime.Now.AddMonths(0).Month + "/" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(0).Month) + "/" + DateTime.Now.AddMonths(0).Year;
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
            cls.Return_DS(ds, "EXEC Get_MafsiDashboardReport 1");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlRegion, ds.Tables[0]);
            }
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
            //if (txtFromDate.Text == "")
            //{
            //    Utility.ShowMessage_Error(Page, "Please enter From Date! ");
            //    txtFromDate.Focus();
            //    return false;
            //}

            //if (txtToDate.Text == "")
            //{
            //    Utility.ShowMessage_Error(Page, "Please enter To Date! ");
            //    txtToDate.Focus();
            //    return false;
            //}

            if (ddlCompanyName.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Company! ");
                ddlCompanyName.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtFromDate.Text) ^ string.IsNullOrEmpty(txtToDate.Text))
            {
                Utility.ShowMessage_Error(Page, "Please enter both From Date and To Date!");

                if (string.IsNullOrEmpty(txtFromDate.Text))
                {
                    txtFromDate.Focus();
                }
                else
                {
                    txtToDate.Focus();
                }

                return false;
            }

            if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
            {
                DateTime fromDate;
                DateTime toDate;

                if (!DateTime.TryParse(txtFromDate.Text, out fromDate) ||
                    !DateTime.TryParse(txtToDate.Text, out toDate))
                {
                    Utility.ShowMessage_Error(Page, "Invalid date format!");
                    return false;
                }

                if (fromDate > toDate)
                {
                    Utility.ShowMessage_Error(Page, "From Date cannot be greater than To Date!");
                    txtFromDate.Focus();
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlRegion.SelectedIndex = 0;
            ddlStakeholder.SelectedIndex = 0;
            ddlStakeholder_SelectedIndexChanged();
            ddlCompanyName.SelectedIndex = 0;
            HideAllGrids();
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            //SetDates();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlStakeholder_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStakeholder_SelectedIndexChanged();
    }

    protected void ddlStakeholder_SelectedIndexChanged()
    {
        try
        {
            DataTable dt = new DataTable();
            cls.Return_DT(dt, "Exec Get_MafsiDashboardReport 2, " + ddlStakeholder.SelectedValue);
            if (dt.Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlCompanyName, dt);
            }
            changeLabels();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void changeLabels()
    {
        try
        {
            if (ddlStakeholder.SelectedValue == "1")
            {
                lblSummaryTable.InnerText = "Consultant Summary Table";
                lblDetailedProfiles.InnerText = "Detailed Consultant Profiles";
            }
            else if (ddlStakeholder.SelectedValue == "2")
            {
                lblSummaryTable.InnerText = "Dealer Summary Table";
                lblDetailedProfiles.InnerText = "Detailed Dealer Profiles";
            }

            btnExportExcel.Enabled = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void HideAllGrids()
    {
        try
        {
            HideSummaryTable();
            HideProfileTable();
            HideLeadsTable();
            HideConvertedSalesTable();
            HidePerformanceMatricTable();
            btnExportExcel.Enabled = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void HideSummaryTable()
    {
        try
        {
            gvSummary.DataSource = string.Empty;
            gvSummary.DataBind();
            lblSummaryTable.Visible = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ShowSummaryTable(DataTable dt)
    {
        try
        {
            if (dt.Rows.Count > 0)
            {
                gvSummary.DataSource = dt;
                gvSummary.DataBind();
                lblSummaryTable.Visible = true;
            }
            else
            {
                HideSummaryTable();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void HideProfileTable()
    {
        try
        {
            gvProfiles.DataSource = string.Empty;
            gvProfiles.DataBind();
            lblDetailedProfiles.Visible = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ShowProfileTable(DataTable dt)
    {
        try
        {
            if (dt.Rows.Count > 0)
            {
                gvProfiles.DataSource = dt;
                gvProfiles.DataBind();
                lblDetailedProfiles.Visible = true;
            }
            else
            {
                HideProfileTable();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void HideLeadsTable()
    {
        try
        {
            gvCurrentLeads.DataSource = string.Empty;
            gvCurrentLeads.DataBind();
            lblLeads.Visible = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ShowLeadsTable(DataTable dt)
    {
        try
        {
            if (dt.Rows.Count > 0)
            {
                gvCurrentLeads.DataSource = dt;
                gvCurrentLeads.DataBind();
                lblLeads.Visible = true;
            }
            else
            {
                HideLeadsTable();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void HideConvertedSalesTable()
    {
        try
        {
            gvConvertedSales.DataSource = string.Empty;
            gvConvertedSales.DataBind();
            lblSales.Visible = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ShowSalesTable(DataTable dt)
    {
        try
        {
            if (dt.Rows.Count > 0)
            {
                gvConvertedSales.DataSource = dt;
                gvConvertedSales.DataBind();
                lblSales.Visible = true;
            }
            else
            {
                HideConvertedSalesTable();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void HidePerformanceMatricTable()
    {
        try
        {
            gvPerformanceMatrics.DataSource = string.Empty;
            gvPerformanceMatrics.DataBind();
            lblPerformanceMatric.Visible = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ShowPerformanceMatricTable(DataTable dt)
    {
        try
        {
            if (dt.Rows.Count > 0)
            {
                gvPerformanceMatrics.DataSource = dt;
                gvPerformanceMatrics.DataBind();
                lblPerformanceMatric.Visible = true;
            }
            else
            {
                HidePerformanceMatricTable();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnSummary_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidationCheck())
            {
                return;
            }
            string query = "EXEC Get_MafsiDashboardReport 3, " + ddlStakeholder.SelectedValue + ", " + ddlRegion.SelectedValue + ", " + ddlCompanyName.SelectedValue + ", '"
                            + txtFromDate.Text + "', '" + txtToDate.Text + "' ";

            DataSet ds = new DataSet();
            cls.Return_DS(ds, query);

            ShowSummaryTable(ds.Tables[0]);
            ShowProfileTable(ds.Tables[1]);
            ShowLeadsTable(ds.Tables[2]);            
            ShowSalesTable(ds.Tables[3]);
            ShowPerformanceMatricTable(ds.Tables[4]);

            if (ddlCompanyName.SelectedIndex > 0)
            {
                btnExportExcel.Enabled = true;
                lblSummaryTable.InnerText = ddlCompanyName.SelectedItem.Text + " Summary Table";
                lblDetailedProfiles.InnerText = "Detailed " + ddlCompanyName.SelectedItem.Text + " Profiles";
            }
            else
            {
                changeLabels();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnCurrentLeads_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidationCheck())
            {
                return;
            }
            string query = "EXEC Get_MafsiDashboardReport 4, " + ddlStakeholder.SelectedValue + ", " + ddlRegion.SelectedValue + ", " + ddlCompanyName.SelectedValue + ", '"
                            + txtFromDate.Text + "', '" + txtToDate.Text + "' ";

            DataTable dt = new DataTable();
            cls.Return_DT(dt, query);
            if (dt.Rows.Count > 0)
            {
                gvSummary.DataSource = dt;
                gvSummary.DataBind();
                //lblRecordsCount.Text = "Total No. of Records: " + dt.Rows.Count.ToString();
                //lblRecordsCount.Visible = true;
            }
            else
            {
                gvSummary.DataSource = string.Empty;
                gvSummary.DataBind();
                //lblRecordsCount.Text = string.Empty;
                //lblRecordsCount.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnConvertedSales_Click(object sender, EventArgs e)
    {
        try
        {
            if (!ValidationCheck())
            {
                return;
            }
            string query = "EXEC Get_MafsiDashboardReport 5, " + ddlStakeholder.SelectedValue + ", " + ddlRegion.SelectedValue + ", " + ddlCompanyName.SelectedValue + ", '"
                            + txtFromDate.Text + "', '" + txtToDate.Text + "' ";

            DataTable dt = new DataTable();
            cls.Return_DT(dt, query);
            if (dt.Rows.Count > 0)
            {
                gvSummary.DataSource = dt;
                gvSummary.DataBind();
                //lblRecordsCount.Text = "Total No. of Records: " + dt.Rows.Count.ToString();
                //lblRecordsCount.Visible = true;
            }
            else
            {
                gvSummary.DataSource = string.Empty;
                gvSummary.DataBind();
                //lblRecordsCount.Text = string.Empty;
                //lblRecordsCount.Visible = false;
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        try
        {
            String fileName = ddlCompanyName.SelectedItem.Text.Trim().Replace(",", "") + " Performance Report";
            List<GridView> grids = new List<GridView>();
            List<string> gridNames = new List<string>();
            grids.Add(gvSummary);
            gridNames.Add(ddlCompanyName.SelectedItem.Text + " Summary Table");

            grids.Add(gvProfiles);
            gridNames.Add("Detailed " + ddlCompanyName.SelectedItem.Text + " Profiles");

            grids.Add(gvCurrentLeads);
            gridNames.Add("Proposals (Current Leads & Opportunities)");

            grids.Add(gvConvertedSales);
            gridNames.Add("Jobs (Converted Sales)");

            grids.Add(gvPerformanceMatrics);
            gridNames.Add("Performance Matrics");

            Utility.ExportGrids(grids, gridNames, Utility.ExportMode.SingleSheet, fileName);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvSummary_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string[] rightAlignColumns = { "Total Leads", "Converted Leads", "Conversion Rate", "Total Project Value", "Won Project Value" };

            if (e.Row.RowType == DataControlRowType.Header)
            {
                Dictionary<string, int> colIndexes = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string header = e.Row.Cells[i].Text.Replace("&nbsp;", " ").Trim();

                    for (int j = 0; j < rightAlignColumns.Length; j++)
                    {
                        if (header.Equals(rightAlignColumns[j], StringComparison.OrdinalIgnoreCase))
                        {
                            colIndexes[rightAlignColumns[j]] = i;
                            e.Row.Cells[i].Style["text-align"] = "right";
                            break;
                        }
                    }
                }

                ViewState["RightAlignIndexes"] = colIndexes;
            }

            if (e.Row.RowType == DataControlRowType.DataRow && ViewState["RightAlignIndexes"] != null)
            {
                Dictionary<string, int> colIndexes = (Dictionary<string, int>)ViewState["RightAlignIndexes"];

                string[] keys = new string[colIndexes.Count];
                colIndexes.Keys.CopyTo(keys, 0);

                for (int k = 0; k < keys.Length; k++)
                {
                    int idx = colIndexes[keys[k]];

                    if (idx < e.Row.Cells.Count)
                    {
                        e.Row.Cells[idx].HorizontalAlign = HorizontalAlign.Right;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvCurrentLeads_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string[] rightAlignColumns = { "Expected Price" };

            if (e.Row.RowType == DataControlRowType.Header)
            {
                Dictionary<string, int> colIndexes = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string header = e.Row.Cells[i].Text.Replace("&nbsp;", " ").Trim();

                    for (int j = 0; j < rightAlignColumns.Length; j++)
                    {
                        if (header.Equals(rightAlignColumns[j], StringComparison.OrdinalIgnoreCase))
                        {
                            colIndexes[rightAlignColumns[j]] = i;
                            e.Row.Cells[i].Style["text-align"] = "right";
                            break;
                        }
                    }
                }

                ViewState["RightAlignIndexes"] = colIndexes;
            }

            if (e.Row.RowType == DataControlRowType.DataRow && ViewState["RightAlignIndexes"] != null)
            {
                Dictionary<string, int> colIndexes = (Dictionary<string, int>)ViewState["RightAlignIndexes"];

                string[] keys = new string[colIndexes.Count];
                colIndexes.Keys.CopyTo(keys, 0);

                for (int k = 0; k < keys.Length; k++)
                {
                    int idx = colIndexes[keys[k]];

                    if (idx < e.Row.Cells.Count)
                    {
                        e.Row.Cells[idx].HorizontalAlign = HorizontalAlign.Right;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvConvertedSales_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string[] rightAlignColumns = { "Final Value" };

            if (e.Row.RowType == DataControlRowType.Header)
            {
                Dictionary<string, int> colIndexes = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    string header = e.Row.Cells[i].Text.Replace("&nbsp;", " ").Trim();

                    for (int j = 0; j < rightAlignColumns.Length; j++)
                    {
                        if (header.Equals(rightAlignColumns[j], StringComparison.OrdinalIgnoreCase))
                        {
                            colIndexes[rightAlignColumns[j]] = i;
                            e.Row.Cells[i].Style["text-align"] = "right";
                            break;
                        }
                    }
                }

                ViewState["RightAlignIndexes"] = colIndexes;
            }

            if (e.Row.RowType == DataControlRowType.DataRow && ViewState["RightAlignIndexes"] != null)
            {
                Dictionary<string, int> colIndexes = (Dictionary<string, int>)ViewState["RightAlignIndexes"];

                string[] keys = new string[colIndexes.Count];
                colIndexes.Keys.CopyTo(keys, 0);

                for (int k = 0; k < keys.Length; k++)
                {
                    int idx = colIndexes[keys[k]];

                    if (idx < e.Row.Cells.Count)
                    {
                        e.Row.Cells[idx].HorizontalAlign = HorizontalAlign.Right;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlCompanyName_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            HideAllGrids();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}