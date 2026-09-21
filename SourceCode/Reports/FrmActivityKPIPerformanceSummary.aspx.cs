using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_FrmActivityKPIPerformanceSummary : System.Web.UI.Page
{
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
            cls.Return_DS(ds, "EXEC Get_ActivityKPIPerformanceSummary 1");

            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownListWithoutFiller(ddlMonthHeaderList, ds.Tables[0]);
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
            DataSet ds = new DataSet();
            cls.Return_DS(ds, "EXEC Get_ActivityKPIPerformanceSummary 4, " + DateTime.Now.Year + ", " + ddlMonthHeaderList.SelectedValue);

            gvSearch.DataSource = ds.Tables[0];
            gvSearch.DataBind();

            gvFeedback.DataSource = ds.Tables[1];
            gvFeedback.DataBind();
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

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            String fileName = ddlMonthHeaderList.SelectedItem.Text.Trim().Replace(",", "") + " Performance Report";
            List<GridView> grids = new List<GridView>();
            List<string> gridNames = new List<string>();
            grids.Add(gvSearch);
            gridNames.Add(ddlMonthHeaderList.SelectedItem.Text + " KPI Table");

            //grids.Add(gvFeedback);
            //gridNames.Add(ddlMonthHeaderList.SelectedItem.Text + " Review Table");

            Utility.ExportGrids(grids, gridNames, Utility.ExportMode.MultipleSheets, fileName);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/SalesManagement/FrmBDMActivityFeedback.aspx", false);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}