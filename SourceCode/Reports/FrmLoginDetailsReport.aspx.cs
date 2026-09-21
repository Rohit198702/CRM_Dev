using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_FrmLoginDetailsReport : System.Web.UI.Page
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
            cls.Return_DS(ds, " EXEC Get_LoginDetails 1 ");
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlUsers, ds.Tables[0]);
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlIPAddress, ds.Tables[1]);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            string query = "EXEC Get_LoginDetails 2 ";
            if (ddlUsers.SelectedIndex > 0)
            {
                query += ", " + ddlUsers.SelectedValue;
            }
            else
            {
                query += ", 0";
            }

            if (ddlIPAddress.SelectedIndex > 0)
            {
                query += ", '" + ddlIPAddress.SelectedValue + "' ";
            }
            else
            {
                query += ", ''";
            }

            cls.Return_DS(ds, query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvLoginDetails.DataSource = ds.Tables[0];
                gvLoginDetails.DataBind();
                ViewState["dirState"] = ds.Tables[0];
            }
            else
            {
                gvLoginDetails.DataSource = string.Empty;
                gvLoginDetails.DataBind();
                ViewState["dirState"] = "";
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
            ddlUsers.SelectedIndex = 0;
            ddlIPAddress.SelectedIndex = 0;

            gvLoginDetails.DataSource = string.Empty;
            gvLoginDetails.DataBind();
            ViewState["dirState"] = "";
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

    protected void gvLoginDetails_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable dtrslt = (DataTable)ViewState["dirState"];
            if (dtrslt.Rows.Count > 0)
            {
                DataView dataView = new DataView(dtrslt);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);
                gvLoginDetails.DataSource = dataView;
                gvLoginDetails.DataBind();
            }
            else
            {
                dtrslt.DefaultView.Sort = e.SortExpression + "DESC";
                gvLoginDetails.DataSource = dtrslt;
                gvLoginDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}