using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_FrmProjectStatusReport : System.Web.UI.Page
{
    commonclass1 cls = new commonclass1();
    ReportDocument rprt = new ReportDocument();
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
            cls.Return_DS(ds, "EXEC Get_ProjectStatusReport 1");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlProductCode, ds.Tables[0]);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private string PrepareParameters()
    {
        string query = "";
        try
        {
            if (rdbSentToFabrication.SelectedValue == "1")
            {
                query += ", 1 ";
            }
            else if (rdbSentToFabrication.SelectedValue == "0")
            {
                query += ", 0 ";
            }
            else if (rdbSentToFabrication.SelectedValue == "2")
            {
                query += ", 2 ";
            }

            if (rdbSentToNesting.SelectedValue == "1")
            {
                query += ", 1 ";
            }
            else if (rdbSentToNesting.SelectedValue == "0")
            {
                query += ", 0 ";
            }
            else if (rdbSentToNesting.SelectedValue == "2")
            {
                query += ", 2 ";
            }

            if (rdbSentToProduction.SelectedValue == "1")
            {
                query += ", 1 ";
            }
            else if (rdbSentToProduction.SelectedValue == "0")
            {
                query += ", 0 ";
            }
            else if (rdbSentToProduction.SelectedValue == "2")
            {
                query += ", 2 ";
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return query;
    }

    private string PrepareConsolidatedQuery()
    {
        string query = "EXEC Get_ProjectStatusReport 5 " + PrepareParameters();
        return query;
    }

    private void ConsolidatedReport()
    {
        try
        {
            DataTable dt = new DataTable();
            string query = PrepareConsolidatedQuery();
            cls.Return_DT(dt, query);
            gvSearch.DataSource = dt;
            gvSearch.DataBind();
            lblRecordsCount.Text = "Total No. of Records: " + dt.Rows.Count.ToString();
            lblRecordsCount.Visible = true;
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

    private string PrepareAerowerksQuery()
    {
        string query = "EXEC Get_ProjectStatusReport 2 " + PrepareParameters();
        return query;
    }

    private void AerowerksReport()
    {
        try
        {
            DataTable dt = new DataTable();
            string query = PrepareAerowerksQuery();
            cls.Return_DT(dt, query);
            gvSearch.DataSource = dt;
            gvSearch.DataBind();
            lblRecordsCount.Text = "Total No. of Records: " + dt.Rows.Count.ToString();
            lblRecordsCount.Visible = true;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private string PrepareITWQuery()
    {
        string query = "EXEC Get_ProjectStatusReport 3 " + PrepareParameters();
        return query;
    }

    private void ITWReport()
    {
        try
        {
            DataTable dt = new DataTable();
            string query = PrepareITWQuery();
            cls.Return_DT(dt, query);
            gvSearch.DataSource = dt;
            gvSearch.DataBind();
            lblRecordsCount.Text = "Total No. of Records: " + dt.Rows.Count.ToString();
            lblRecordsCount.Visible = true;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private string PrepareGaylordQuery()
    {
        string query = "EXEC Get_ProjectStatusReport 4 " + PrepareParameters();
        return query;
    }

    private void GaylordReport()
    {
        try
        {
            DataTable dt = new DataTable();
            string query = PrepareGaylordQuery();
            cls.Return_DT(dt, query);
            gvSearch.DataSource = dt;
            gvSearch.DataBind();
            lblRecordsCount.Text = "Total No. of Records: " + dt.Rows.Count.ToString();
            lblRecordsCount.Visible = true;
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductCode.SelectedValue == "0")
            {
                ConsolidatedReport();
            }
            else if (ddlProductCode.SelectedValue == "1")
            {
                AerowerksReport();
            }
            else if (ddlProductCode.SelectedValue == "2")
            {
                ITWReport();
            }
            else if (ddlProductCode.SelectedValue == "3")
            {
                GaylordReport();
            }
            else
            {
                Utility.ShowMessage_Info(Page, "not available !");
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnGenerateExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlProductCode.SelectedValue == "0")
            {
                //all
                DataTable dt = new DataTable();
                string query = PrepareConsolidatedQuery();
                cls.Return_DT(dt, query);
                Utility.ExportToExcelDT(dt, "Projects Status Report-" + ddlProductCode.SelectedItem.Text);
            }
            else if (ddlProductCode.SelectedValue == "1")
            {
                //aerowerks
                DataTable dt = new DataTable();
                string query = PrepareAerowerksQuery();
                cls.Return_DT(dt, query);
                Utility.ExportToExcelDT(dt, "Projects Status Report-" + ddlProductCode.SelectedItem.Text);
            }
            else if (ddlProductCode.SelectedValue == "2")
            {
                //itw
                DataTable dt = new DataTable();
                string query = PrepareITWQuery();
                cls.Return_DT(dt, query);
                Utility.ExportToExcelDT(dt, "Projects Status Report-" + ddlProductCode.SelectedItem.Text);
            }
            else if (ddlProductCode.SelectedValue == "3")
            {
                //gaylord
                DataTable dt = new DataTable();
                string query = PrepareGaylordQuery();
                cls.Return_DT(dt, query);
                Utility.ExportToExcelDT(dt, "Projects Status Report-" + ddlProductCode.SelectedItem.Text);
            }
            else
            {
                Utility.ShowMessage_Info(Page, "not available !");
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
            rdbSentToFabrication.SelectedValue = "1";
            rdbSentToNesting.SelectedValue = "1";
            rdbSentToProduction.SelectedValue = "2";
            gvSearch.DataSource = string.Empty;
            gvSearch.DataBind();
            lblRecordsCount.Visible = false;
            ddlProductCode_SelectedIndexChanged();
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
            if (ddlProductCode.SelectedValue == "1" || ddlProductCode.SelectedValue == "0")
            {
                rdbSentToFabrication.Enabled = true;
            }
            else
            {
                rdbSentToFabrication.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}