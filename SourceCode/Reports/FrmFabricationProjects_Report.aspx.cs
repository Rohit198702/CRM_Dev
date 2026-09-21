using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_FrmFabricationProjects_Report : System.Web.UI.Page
{
    commonclass1 cls = new commonclass1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetDates();
            BindControls();
        }
    }

    private void SetDates()
    {
        try
        {
            txtDateFrom.Text = DateTime.Now.Month + "/01/" + DateTime.Now.Year;
            txtDateTo.Text = DateTime.Now.AddMonths(0).Month + "/" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(0).Month) + "/" + DateTime.Now.AddMonths(0).Year;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void BindControls()
    {
        DataTable dt = new DataTable();
        try
        {
            cls.Return_DT(dt, "EXEC Get_FabricationProjects_Report 1 ");

            if (dt.Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlWarehouse, dt);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private DataTable GetReportData()
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "EXEC Get_FabricationProjects_Report 2, ";
            if (txtDateFrom.Text.Trim() != "" && txtDateTo.Text.Trim() != "")
            {
                query += " '" + txtDateFrom.Text + "', '" + txtDateTo.Text + "', ";
            }

            if (ddlWarehouse.SelectedIndex > 0)
            {
                query += ddlWarehouse.SelectedValue;
            }
            else
            {
                query += " 0";
            }

            cls.Return_DT(dt, query);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }

        return dt;
    }

    protected void btnGenerateExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = GetReportData();
            //Utility.ExportToExcelGrid(gvMonthlyExportToExcel, "Fabrication Projects from " + txtDateFrom.Text + " to " + txtDateTo.Text);
            Utility.ExportToExcelDT(dt, "Fabrication Projects from " + txtDateFrom.Text + " to " + txtDateTo.Text);
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
            SetDates();
            ddlWarehouse.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}