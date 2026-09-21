using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BOLAERO;
using BLLAERO;
using System.Data;
using System.Data.SqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;

public partial class SalesManagement_FrmSearchReps : System.Web.UI.Page
{
    BOLRepSearch ObjBOL = new BOLRepSearch();
    BLLRepsSearch ObjBLL = new BLLRepsSearch();
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
    /// <summary>
    ///  BindControl() function bind all Drop Down Lists on the Search Proposal PAGE
    /// </summary>
    private void BindControls()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 1;
            ds = ObjBLL.GetRepSearch(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlModelName, ds.Tables[0]);
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlPrimeSpec, ds.Tables[2]);
            }
            if (ds.Tables[5].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlCountry, ds.Tables[5]);
            }
            if (ds.Tables[8].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlRepName, ds.Tables[8]);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    /// <summary>
    /// Reset all the controls
    /// </summary>
    private void Reset()
    {
        try
        {
            txtFromDate.Text = String.Empty;
            txttodate.Text = String.Empty;
            ddlRepName.SelectedIndex = 0;        
            ddlModelName.SelectedIndex = 0;
            ddlConveyorType.SelectedIndex = 0;
            ddlPrimeSpec.SelectedIndex = 0;           
            ddlCountry.SelectedIndex = 0;
            ddlState.DataSource = "";
            ddlState.DataBind();
            if (ddlSalesRepGroup.Items.Count > 0)
            {
                ddlSalesRepGroup.SelectedIndex = 0;
            }
            //ddlSalesRepGroup.DataSource = "";
            //ddlSalesRepGroup.DataBind();
            gvProposalSearch.Visible = false;
            lblRecordsCount.Visible = false;          
            rdbOrderFor.SelectedValue = "0";          
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    /// <summary>
    /// GridBind method prepare data in grid view from Project page to Search Project Page
    /// </summary>
    private void GridBind()
    {
        try
        {
            if (ValidationCheck() == true)
             {
                DataTable dt = new DataTable();
                 string Qstr = String.Empty;
                string SearchVar = String.Empty;
                //if (txtRepName.Text != "")
                //{
                //    Qstr += " AND tblHobartListing.FirstName +' '+ tblHobartListing.LastName LIKE  '%" + txtRepName.Text + "%' ";
                //}

                if (ddlRepName.SelectedIndex > 0)
                {
                    Qstr += " AND tblHobartListing.RepID='" + ddlRepName.SelectedValue + "' ";
                }
                if (ddlModelName.SelectedIndex > 0)
                {
                    Qstr += " AND tblConveyorModel.ModelID='" + ddlModelName.SelectedValue + "' ";
                }
                if (ddlConveyorType.SelectedIndex > 0)
                {
                    Qstr += " AND tblConveyorType.ConveyorTypeID='" + ddlConveyorType.SelectedValue + "' ";
                }
                if (ddlPrimeSpec.SelectedIndex > 0)
                {
                    Qstr += " AND tblCompetitor.CompetitorID='" + ddlPrimeSpec.SelectedValue + "' ";
                }                
                if (ddlState.SelectedIndex > 0)
                {
                    Qstr += " AND tblPFiles.StateID='" + ddlState.SelectedValue + "' ";
                }
                if (ddlCountry.SelectedIndex > 0)
                {
                    Qstr += " AND tblPFiles.Country LIKE '%" + ddlCountry.SelectedItem.Text + "%' ";
                }
                if (ddlSalesRepGroup.SelectedIndex > 0)
                {
                    Qstr += " AND tblHobartBranchListing.CompanyName LIKE '%" + ddlSalesRepGroup.SelectedValue + "%' ";
                }
                if (txtFromDate.Text != "")
                {
                    Qstr += " AND tblPFiles.ProposalDate>='" + txtFromDate.Text + "' ";
                }
                if (txttodate.Text != "")
                {
                    Qstr += " AND tblPFiles.ProposalDate<='" + txttodate.Text + "' ";
                }
                if (rdbOrderFor.SelectedValue == "1")
                {
                    Qstr += " AND tblProjects.JobID IS NOT NULL ";
                }
                else if (rdbOrderFor.SelectedValue == "2")
                {                   
                    Qstr += " AND tblProjects.JobID IS NULL ";
                }              
                Qstr += " order by tblPFiles.PNumber desc";
                DataSet ds = new DataSet();
                ObjBOL.Operation = 2;
                ObjBOL.SearchVar = Qstr;
                ds = ObjBLL.GetRepSearch(ObjBOL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblRecordsCount.Text = "Total No. of Records:" + ds.Tables[0].Rows.Count.ToString();
                    lblRecordsCount.Visible = true;
                    if (hfbtnshowclick.Value == "1")
                    {
                        gvProposalSearch.PageIndex = 0;
                        int PaginationSize=Convert.ToInt32(ds.Tables[1].Rows[0]["PageSize"].ToString());
                        if(PaginationSize > 0)
                        {                            
                            ViewState["PageSize"] = PaginationSize;
                            gvProposalSearch.PageSize = PaginationSize;
                        }
                        
                    }
                    else
                    {
                        hfbtnshowclick.Value = "-1";
                        ViewState["PageSize"] = null;
                    }
                    gvProposalSearch.DataSource = ds;
                    gvProposalSearch.DataBind();
                    DataTable dtsearch = new DataTable();
                    dtsearch = ds.Tables[0];
                    ViewState["dirState"] = dtsearch;
                    gvProposalSearch.Visible = true;
                    btnExportToExcel.Enabled = true;
                }
                else
                {
                    lblRecordsCount.Text = "No Record Found.";
                    lblRecordsCount.Visible = true;
                    gvProposalSearch.DataSource = "";
                    gvProposalSearch.DataBind();
                    btnExportToExcel.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    /// <summary>
    /// Search Button displays matching condition data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearchProposal_Click(object sender, EventArgs e)
    {
        try
        {
            hfbtnshowclick.Value = "1";
            GridBind();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    /// <summary>
    /// Validation Check used for fields mandetory
    /// </summary>
    /// <returns></returns>
    private Boolean ValidationCheck()
    {
        try
        {
            //if (txtFromDate.Text == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "alert('Please Enter From Date. !');", true);
            //    txtFromDate.Focus();
            //    return false;
            //}
            //if (txttodate.Text == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "alert('Please Enter To Date. !');", true);
            //    txttodate.Focus();
            //    return false;
            //}
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }
    /// <summary>
    /// Clear Button Reset all the TextBoxes and Drop Down Values
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnClearProposal_Click(object sender, EventArgs e)
    {
        try
        {
            Reset();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }       
    }
    /// <summary>
    /// It used for mouse hand design
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvProposalSearch_RowDataBound(object sender, GridViewRowEventArgs e)
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
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvProposalSearch, "Select$" + e.Row.RowIndex);
                //PrepareDT();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }        
    }
    /// <summary>
    /// It used for read a row record from the Grid View Control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvProposalSearch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {                
                Session["PNumber"] = "";                
                DataSet ds = new DataSet();
                ObjBOL.Operation = 3;
                int iIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow gvRow = gvProposalSearch.Rows[iIndex];
                string PNumber = Server.HtmlDecode(gvRow.Cells[0].Text);
                ObjBOL.PNumber = PNumber;
                ds = ObjBLL.GetRepSearch(ObjBOL);
                Session["PNumber"] = ds.Tables[0].Rows[0]["Pnumber"].ToString();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenWindow", "window.open('FrmProposals.aspx','_blank')", true);
                //Response.Redirect("~/SalesManagement/FrmProposals.aspx");
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    /// <summary>
    /// Selected number of records display in the Grid View Control
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvProposalSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            int newPage = gvProposalSearch.PageIndex;

            string arg = e.NewPageIndex.ToString();
            if (arg == "Prev")
                newPage = gvProposalSearch.PageIndex - 1;
            else if (arg == "Next")
                newPage = gvProposalSearch.PageIndex + 1;
            else if (arg == "Last")
                newPage = gvProposalSearch.PageCount - 1;
            else // numeric button
                newPage = e.NewPageIndex;

            newPage = Math.Max(0, Math.Min(newPage, gvProposalSearch.PageCount - 1));

            gvProposalSearch.PageIndex = newPage;
            BindGridOnPagination();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }       
    }

    private void BindGridOnPagination()
    {
        try
        {
            int PageSize = (int)ViewState["PageSize"];
            gvProposalSearch.AllowPaging = true;
            if (PageSize > 0)
            {
                gvProposalSearch.PageSize = PageSize;
            }
            gvProposalSearch.DataSource = (DataTable)ViewState["dirState"];
            gvProposalSearch.DataBind();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DropDownList ddl = (DropDownList)sender;
            int pageSize = int.Parse(ddl.SelectedValue);
            gvProposalSearch.PageSize = pageSize;
            gvProposalSearch.PageIndex = 0;
            BindGridOnPagination();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvProposalSearch_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable dtrslt = (DataTable)ViewState["dirState"];
            if (dtrslt.Rows.Count > 0)
            {
                DataView dataView = new DataView(dtrslt);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);
                gvProposalSearch.DataSource = dataView;
                gvProposalSearch.DataBind();
            }
            else
            {
                dtrslt.DefaultView.Sort = e.SortExpression  + "DESC";
                gvProposalSearch.DataSource = dtrslt;
                gvProposalSearch.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);   
        }
    }
    /// <summary>
    /// 
    /// </summary>
    private string GridViewSortDirection
    {
        get { return ViewState["SortDirection"] as string ?? "DESC"; }
        set { ViewState["SortDirection"] = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sortDirection"></param>
    /// <returns></returns>
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
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {        
        if (ddlCountry.SelectedIndex > 0)
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 4;
            ObjBOL.country = Convert.ToInt32(ddlCountry.SelectedValue);
            ds = ObjBLL.GetRepSearch(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlState, ds.Tables[0]);
            }
            else
            {
                ddlState.DataSource = "";
                ddlState.DataBind();
            }
        }        
    }    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered, tem a ver com obotão de exportação para excel*/
    }  
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportToExcel_Click1(object sender, EventArgs e)
    {
        string FileName = "Search Reps Details";
        gvProposalSearch.Attributes.Remove("class");
        //Utility.ExportToExcelGrid(gvProposalSearch, FileName);

        DataTable dt = (DataTable)ViewState["dirState"];
        Utility.ExportToExcelDT(dt, FileName);

    }

    protected void gvProposalSearch_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {            
            if (e.Row.RowType == DataControlRowType.Pager)
            {
                GridViewRow pagerRow = e.Row;

                LinkButton lnkPrev = (LinkButton)pagerRow.FindControl("lnkPrev");
                LinkButton lnkNext = (LinkButton)pagerRow.FindControl("lnkNext");

                int currentPage = gvProposalSearch.PageIndex;
                int totalPages = gvProposalSearch.PageCount;

                if (lnkPrev != null)
                    lnkPrev.Enabled = currentPage > 0;

                if (lnkNext != null)
                    lnkNext.Enabled = currentPage < totalPages - 1;
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlPageSize");
                int PageSize = (int)ViewState["PageSize"];
                ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem(PageSize.ToString(), PageSize.ToString()));
                if (ddl != null)
                {
                    ddl.SelectedValue = gvProposalSearch.PageSize.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    public class PageItem
    {
        public int PageIndex { get; set; }
        public int PageNumber { get; set; }
    }

    public int CurrentPageIndex
    {
        get { return gvProposalSearch.PageIndex; }
    }

    protected void gvProposalSearch_DataBound(object sender, EventArgs e)
    {
        try
        {
            try
            {
                GridViewRow pagerRow = gvProposalSearch.BottomPagerRow;

                if (pagerRow == null) return;

                Repeater rptPager = (Repeater)pagerRow.FindControl("rptPager");

                int totalPages = gvProposalSearch.PageCount;
                int currentPage = gvProposalSearch.PageIndex;
                int pageGroupSize = 1500;

                int startPage = (currentPage / pageGroupSize) * pageGroupSize;
                int endPage = Math.Min(startPage + pageGroupSize, totalPages);

                List<PageItem> pages = new List<PageItem>();
                for (int i = startPage; i < endPage; i++)
                {
                    pages.Add(new PageItem
                    {
                        PageIndex = i,
                        PageNumber = i + 1
                    });
                }


                rptPager.DataSource = pages;
                rptPager.DataBind();

                LinkButton lnkPrev = (LinkButton)pagerRow.FindControl("lnkPrev");
                LinkButton lnkNext = (LinkButton)pagerRow.FindControl("lnkNext");
                lnkPrev.Enabled = currentPage > 0;
                lnkNext.Enabled = currentPage < totalPages - 1;
                hfbtnshowclick.Value = "-1";
            }
            catch (Exception ex)
            {
                Utility.AddEditException(ex);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    protected void rptPager_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Page")
            {
                int pageIndex = Convert.ToInt32(e.CommandArgument);
                gvProposalSearch.PageIndex = pageIndex;
                BindGridOnPagination();  // your method to bind the GridView
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}