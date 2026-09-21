using System;
using System.Web.UI;
using BOLAERO;
using BLLAERO;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Web.UI.WebControls;
using System.Linq;
using System.Text;
using System.Web;
using OfficeOpenXml.Style;
using OfficeOpenXml;

public partial class Reports_frmcustomercaretickets : System.Web.UI.Page
{
    BOLCustCareTickets ObjBOL = new BOLCustCareTickets();
    BLLCustCareTickets ObjBLL = new BLLCustCareTickets();
    commonclass1 clscon = new commonclass1();
    ReportDocument rprt = new ReportDocument();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Bind_Controls();
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
            DataSet ds = new DataSet();
            ObjBOL.Operation = 1;
            ds = ObjBLL.GetControlsData(ObjBOL);
            if (ds.Tables[1].Rows.Count > 0)
            {
                ddlCategory.DataSource = ds.Tables[1];
                ddlCategory.DataBind();
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlIssueCategory, ds.Tables[2]);
                if (ddlIssueCategory.Items.Count > 0)
                {
                    ddlIssueCategory.SelectedIndex = 0;
                }
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlStatus, ds.Tables[3]);
                if (ddlStatus.Items.Count > 0)
                {
                    ddlStatus.SelectedIndex = 0;
                }
            }
            if (ds.Tables[4].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlSubAssembly, ds.Tables[4]);
                if (ddlSubAssembly.Items.Count > 0)
                {
                    ddlSubAssembly.SelectedIndex = 0;
                }
            }
            if (ds.Tables[6].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlAssignedto, ds.Tables[6]);
                if (ddlAssignedto.Items.Count > 0)
                {
                    ddlAssignedto.SelectedIndex = 0;
                }
            }
            if (ds.Tables[7].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlConveyorType, ds.Tables[7]);
                if (ddlConveyorType.Items.Count > 0)
                {
                    ddlConveyorType.SelectedIndex = 0;
                }
            }
            if (ds.Tables[8].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlProjectNameList, ds.Tables[8]);
                if (ddlProjectNameList.Items.Count > 0)
                {
                    ddlProjectNameList.SelectedIndex = 0;
                }
            }
            if (ds.Tables[9].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlProjectManager, ds.Tables[9]);
                if (ddlProjectManager.Items.Count > 0)
                {
                    ddlProjectManager.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private DataTable Bind_Grid()
    {
        DataTable dt = new DataTable();
        try
        {
            string Qstr = PrepareSQLCommandForSearch();
            clscon.Return_DT(dt, Qstr);
            if (dt.Rows.Count > 0)
            {
                ViewState["dtView"] = dt;
                DataTable dtProject = dt.AsEnumerable()
                .GroupBy(row => new
                {
                    ProjectName = row.Field<string>("Project Name"),
                    TicketNo = row.Field<string>("Ticket No")
                })
                .Select(g => g.First())
                .CopyToDataTable();
                if (dtProject.Columns.Contains("DenseRank"))
                    dtProject.Columns.Remove("DenseRank");
                if (dtProject.Columns.Contains("Summary Date"))
                    dtProject.Columns.Remove("Summary Date");

                if (dtProject.Columns.Contains("Summary"))
                    dtProject.Columns.Remove("Summary");
                gvSummary.DataSource = dtProject;
                gvSummary.DataBind();
                hfjobname.Value = dt.Rows[0]["Project Name"].ToString();
                btnExportToExcel.Enabled = true;
            }
            else
            {
                gvSummary.DataSource = "";
                gvSummary.DataBind();
                ViewState["dtView"] = null;
                hfjobname.Value = String.Empty;
                btnExportToExcel.Enabled = false;
            }

        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        // controller   
    }

    private void ExporttoExcel()
    {
        try
        {
            DataTable dt = Bind_Grid();
            if (dt.Rows.Count > 0)
            {
                Utility.ExportToExcelDT(dt, "Customercare Tickets");
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnSearchProposal_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = Bind_Grid();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void Reset()
    {
        try
        {
            ddlProjectNameList.SelectedIndex = 0;
            ddlProjectManager.SelectedIndex = 0;
            //ddlCategory.SelectedIndex = 0;
            ddlIssueCategory.SelectedIndex = 0;
            ddlSubAssembly.SelectedIndex = 0;
            ddlAssignedto.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            ddlConveyorType.SelectedIndex = 0;
            txtfromdate.Text = String.Empty;
            txttodate.Text = String.Empty;
            txtPO.Text = String.Empty;
            txtTicketNo.Text = String.Empty;
            gvSummary.DataSource = "";
            gvSummary.DataBind();
            btnExportToExcel.Enabled = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ClearMultiCategoryDropdownList()
    {
        try
        {
            if (ddlCategory.Items.Count > 0)
            {
                ddlCategory.Items.Clear();
                Bind_Controls();
            }

        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnClearProposal_Click(object sender, EventArgs e)
    {
        try
        {
            Reset();
            ClearMultiCategoryDropdownList();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    public void btnGenrate_Click(object sender, EventArgs e)
    {
        try
        {
            if (true)
            {
                Get_CustomerCareTicketReport();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private string PrepareSQLCommandForSearch()
    {
        try
        {
            string Qstr = String.Empty;
            Qstr += " SELECT * FROM ( ";
            Qstr += " Select ROW_NUMBER () over (PARTITION BY TicketID order by CCT_TicketDetails.summarydate DESC) as DenseRank, ";
            Qstr += " CONCAT(tblProjects.JobID + ', ', tblCustomers.CompanyName +', ', tblCustomers.City  +', ', tblStates.[State] +', ' , ";
            Qstr += " tblCountries.Country) AS [Project Name],  TicketNo as [Ticket No], ";
            Qstr += " CASE WHEN CCT_Ticket.CategoryID != 3 THEN CCT_Category.[name] ELSE CCT_Ticket.CategoryOther END AS [Category], ";
            Qstr += " CASE WHEN CCT_Ticket.IssueCategoryID != 14 THEN CCT_IssueCategory.[name] ELSE IssueCategoryOther END AS [Issue Category], ";
            Qstr += " CONVERT(varchar, OpenDate,1) as [Open Date], CONVERT(varchar,CloseDate,1) as [Close Date], CONVERT(varchar,CCT_Ticket.FollowUpDate,1)  as [FollowUp Date], ";
            Qstr += " CASE WHEN CCT_Ticket.SubAssemblyID=18 THEN CCT_Ticket.SubAssemblyOther ELSE CCT_SubAssembly.[name] end as [Sub Assembly],";
            Qstr += " tblConveyorType.ConveyorType as [Conveyor Type],CCT_Status.[name] as [Status], tblEmployees.FirstName as [Assigned To],CCT_Ticket.ServicePO as [Service PO], ";
            Qstr += " Task,CCT_TicketDetails.summarydate as SummaryDateSort,CONVERT(varchar,CCT_TicketDetails.summarydate,1) as [Summary Date], summary as [Summary] ";
            Qstr += " from CCT_Ticket LEFT JOIN CCT_TicketDetails ON CCT_TicketDetails.TicketID=CCT_Ticket.id ";
            Qstr += " LEFT JOIN CCT_Category ON CCT_Category.id=CCT_Ticket.CategoryID ";
            Qstr += " LEFT JOIN CCT_IssueCategory ON CCT_IssueCategory.id=CCT_Ticket.IssueCategoryID ";
            Qstr += " LEFT JOIN CCT_IssueReportedBy  ON CCT_IssueReportedBy.id=CCT_Ticket.IssueCategoryID ";
            Qstr += " LEFT JOIN CCT_SubAssembly ON CCT_SubAssembly.id=CCT_Ticket.SubAssemblyID ";
            Qstr += " LEFT JOIN tblProjects ON tblProjects.JobID=CCT_Ticket.JobID LEFT join tblPFiles on tblPFiles.PNumber=tblProjects.ProposalID ";
            Qstr += " LEFT JOIN tblConveyorType ON tblConveyorType.ConveyorTypeID=tblPFiles.ConveyorTypeID ";
            Qstr += " left join CCT_Status on CCT_Status.id=CCT_Ticket.StatusID ";
            Qstr += " left join tblEmployees on CCT_Ticket.AssignedTo=tblEmployees.EmployeeID ";
            Qstr += " INNER JOIN tblCustomers ON tblCustomers.CustomerID=tblProjects.CustomerID ";
            Qstr += " LEFT JOIN tblCountries ON tblCustomers.CountryID=tblCountries.CountryID ";
            Qstr += " LEFT JOIN tblStates ON tblCustomers.StateID=tblStates.StateID  where TicketNo IS NOT NULL   ";
            if (ddlProjectNameList.SelectedIndex > 0)
            {
                Qstr += " AND tblProjects.JobID  LIKE '%" + ddlProjectNameList.SelectedValue.Split(',')[0] + "%' ";
            }
            if (txtTicketNo.Text != "")
            {
                Qstr += " AND CCT_Ticket.TicketNo = '" + txtTicketNo.Text + "' ";
            }
            var index = 0;
            foreach (ListItem item in ddlCategory.Items)
            {
                if (item.Selected && index == 0)
                {
                    Qstr += " AND CCT_Ticket.CategoryID IN ( ";
                    Qstr += "'" + item.Value + "'";
                    index++;
                }
                else if (item.Selected && index > 0)
                {
                    Qstr += ",'" + item.Value + "'";
                    index++;
                }
            }
            if (index > 0)
            {
                Qstr += " ) ";
            }
            if (ddlIssueCategory.SelectedIndex > 0)
            {
                Qstr += " AND CCT_Ticket.IssueCategoryID = '" + ddlIssueCategory.SelectedValue + "' ";
            }
            if (ddlAssignedto.SelectedIndex > 0)
            {
                Qstr += " AND CCT_Ticket.AssignedTo= '" + ddlAssignedto.SelectedValue + "' ";
            }
            if (ddlSubAssembly.SelectedIndex > 0)
            {
                Qstr += " AND CCT_Ticket.SubAssemblyID= '" + ddlSubAssembly.SelectedValue + "' ";
            }
            if (ddlConveyorType.SelectedIndex > 0)
            {
                Qstr += " AND tblPFiles.ConveyorTypeID= '" + ddlConveyorType.SelectedValue + "' ";
            }
            if (ddlStatus.SelectedIndex > 0)
            {
                Qstr += " AND CCT_Ticket.StatusID = '" + ddlStatus.SelectedValue + "' ";
            }
            if (txtfromdate.Text != "" && txttodate.Text != "")
            {
                if (txtfromdate.Text != "")
                {
                    Qstr += " AND CCT_Ticket.OpenDate >= '" + txtfromdate.Text + "' ";
                }
                if (txttodate.Text != "")
                {
                    Qstr += " AND CCT_Ticket.OpenDate <= '" + txttodate.Text + "' ";
                }
            }
            if (txtPO.Text != "")
            {
                Qstr += " AND CCT_Ticket.ServicePO  LIKE '%" + txtPO.Text + "%' ";
            }
            if (ddlProjectManager.SelectedIndex > 0)
            {
                Qstr += " AND tblPFiles.projectmanagerid = " + ddlProjectManager.SelectedValue;
            }
            Qstr += " ) T  order by [Project Name] ";
            return Qstr;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return "";
    }

    private string PrepareSQLCommandForReport()
    {
        try
        {
            string Qstr = String.Empty;
            Qstr += " SELECT * FROM ( ";
            Qstr += " Select ROW_NUMBER () over (PARTITION BY TicketID order by CCT_TicketDetails.summarydate DESC) as DenseRank, ";
            Qstr += " tblProjects.JobID, CONCAT(tblProjects.JobID + ', ', tblCustomers.CompanyName +', ', tblCustomers.City  +', ', tblStates.[State] +', ' , ";
            Qstr += " tblCountries.Country) AS [Project Name],  TicketNo as [Ticket No], ";
            Qstr += " CASE WHEN CCT_Ticket.CategoryID != 3 THEN CCT_Category.[name] ELSE CCT_Ticket.CategoryOther END AS [Category], ";
            Qstr += " ISNULL(UPPER(projectManager.FirstName), '') AS ProjectManager, ";
            //Qstr += " CASE WHEN CCT_Ticket.IssueCategoryID != 14 THEN CCT_IssueCategory.[name] ELSE IssueCategoryOther END AS [Issue Category], ";
            Qstr += " CONVERT(varchar, OpenDate,101) as [Open Date], CONVERT(varchar,CloseDate,101) as [Close Date], CONVERT(varchar,CCT_Ticket.FollowUpDate,101)  as [FollowUp Date], ";
            Qstr += " CASE WHEN CCT_Ticket.SubAssemblyID=18 THEN CCT_Ticket.SubAssemblyOther ELSE CCT_SubAssembly.[name] end as [Sub Assembly],";
            Qstr += " tblConveyorType.ConveyorType as [Conveyor Type],CCT_Status.[name] as [Status], tblEmployees.FirstName as [Assigned To],CCT_Ticket.ServicePO as [Service PO], ";
            Qstr += " Task,CONVERT(varchar,CCT_TicketDetails.summarydate,101) as [Summary Date], summary as [Summary], ";
            Qstr += " CASE WHEN CCT_Status.[name] = 'Open' AND ";
            Qstr += " (DATEDIFF(DAY, CAST(OpenDate AS DATE), ";
            Qstr += " CAST(GETDATE() AS DATE)) - DATEDIFF(WEEK, ";
            Qstr += " CAST(OpenDate AS DATE), CAST(GETDATE() AS DATE)) * 2 ";
            Qstr += " - CASE WHEN DATEPART(WEEKDAY, OpenDate) IN (1,7) ";
            Qstr += " THEN 1 ELSE 0 END + CASE WHEN ";
            Qstr += " DATEPART(WEEKDAY, GETDATE()) IN (1,7) THEN 1 ";
            Qstr += " ELSE 0 END) > 7 AND ";
            Qstr += " (DATEDIFF(DAY, CAST(OpenDate AS DATE), ";
            Qstr += " CAST(GETDATE() AS DATE)) - DATEDIFF(WEEK, ";
            Qstr += " CAST(OpenDate AS DATE), CAST(GETDATE() AS DATE)) * 2 ";
            Qstr += " - CASE WHEN DATEPART(WEEKDAY, OpenDate) IN (1,7) ";
            Qstr += " THEN 1 ELSE 0 END + CASE WHEN ";
            Qstr += " DATEPART(WEEKDAY, GETDATE()) IN (1,7) THEN 1 ";
            Qstr += " ELSE 0 END) <= 30 THEN 1 ELSE 0 END ";
            Qstr += " AS [Open Days Red], ";

            Qstr += " CASE WHEN CCT_Status.[name] = 'Open' AND ";
            Qstr += " (DATEDIFF(DAY, CAST(OpenDate AS DATE), ";
            Qstr += " CAST(GETDATE() AS DATE)) - DATEDIFF(WEEK, ";
            Qstr += " CAST(OpenDate AS DATE), CAST(GETDATE() AS DATE)) * 2 ";
            Qstr += " - CASE WHEN DATEPART(WEEKDAY, OpenDate) IN (1,7) ";
            Qstr += " THEN 1 ELSE 0 END + CASE WHEN ";
            Qstr += " DATEPART(WEEKDAY, GETDATE()) IN (1,7) THEN 1 ";
            Qstr += " ELSE 0 END) > 30 THEN 1 ELSE 0 END ";
            Qstr += " AS [Open Days Yellow] ";
            Qstr += " from CCT_Ticket LEFT JOIN CCT_TicketDetails on CCT_Ticket.id = CCT_TicketDetails.TicketID ";
            Qstr += " LEFT JOIN CCT_Category ON CCT_Category.id=CCT_Ticket.CategoryID ";
            Qstr += " LEFT JOIN CCT_IssueCategory ON CCT_IssueCategory.id=CCT_Ticket.IssueCategoryID ";
            Qstr += " LEFT JOIN CCT_IssueReportedBy  ON CCT_IssueReportedBy.id=CCT_Ticket.IssueCategoryID ";
            Qstr += " LEFT JOIN CCT_SubAssembly ON CCT_SubAssembly.id=CCT_Ticket.SubAssemblyID ";
            Qstr += " LEFT JOIN tblProjects ON tblProjects.JobID=CCT_Ticket.JobID LEFT join tblPFiles on tblPFiles.PNumber=tblProjects.ProposalID ";
            Qstr += " LEFT JOIN tblConveyorType ON tblConveyorType.ConveyorTypeID=tblPFiles.ConveyorTypeID ";
            Qstr += " left join CCT_Status on CCT_Status.id=CCT_Ticket.StatusID ";
            Qstr += " left join tblEmployees on CCT_Ticket.AssignedTo=tblEmployees.EmployeeID ";
            Qstr += " left join tblEmployees projectManager on tblPFiles.projectmanagerid=projectManager.EmployeeID  ";
            Qstr += " INNER JOIN tblCustomers ON tblCustomers.CustomerID=tblProjects.CustomerID ";
            Qstr += " LEFT JOIN tblCountries ON tblCustomers.CountryID=tblCountries.CountryID ";
            Qstr += " LEFT JOIN tblStates ON tblCustomers.StateID=tblStates.StateID  where TicketNo IS NOT NULL  ";        
            if (ddlProjectNameList.SelectedIndex > 0)
            {
                Qstr += " AND tblProjects.JobID  LIKE '%" + ddlProjectNameList.SelectedValue.Split(',')[0] + "%' ";
            }
            if (txtTicketNo.Text != "")
            {
                Qstr += " AND CCT_Ticket.TicketNo = '" + txtTicketNo.Text + "' ";
            }
            var index = 0;
            foreach (ListItem item in ddlCategory.Items)
            {
                if (item.Selected && index == 0)
                {
                    Qstr += " AND CCT_Ticket.CategoryID IN ( ";
                    Qstr += "'" + item.Value + "'";
                    index++;
                }
                else if (item.Selected && index > 0)
                {
                    Qstr += ",'" + item.Value + "'";
                    index++;
                }
            }
            if (index > 0)
            {
                Qstr += " ) ";
            }
            if (ddlIssueCategory.SelectedIndex > 0)
            {
                Qstr += " AND CCT_Ticket.IssueCategoryID = '" + ddlIssueCategory.SelectedValue + "' ";
            }
            if (ddlAssignedto.SelectedIndex > 0)
            {
                Qstr += " AND CCT_Ticket.AssignedTo= '" + ddlAssignedto.SelectedValue + "' ";
            }
            if (ddlSubAssembly.SelectedIndex > 0)
            {
                Qstr += " AND CCT_Ticket.SubAssemblyID= '" + ddlSubAssembly.SelectedValue + "' ";
            }
            if (ddlConveyorType.SelectedIndex > 0)
            {
                Qstr += " AND tblPFiles.ConveyorTypeID= '" + ddlConveyorType.SelectedValue + "' ";
            }
            if (ddlStatus.SelectedIndex > 0)
            {

                Qstr += " AND CCT_Ticket.StatusID = '" + ddlStatus.SelectedValue + "' ";

            }
            if (txtfromdate.Text != "" && txttodate.Text != "")
            {
                if (txtfromdate.Text != "")
                {
                    Qstr += " AND CCT_Ticket.OpenDate >= '" + txtfromdate.Text + "' ";
                }
                if (txttodate.Text != "")
                {
                    Qstr += " AND CCT_Ticket.OpenDate <= '" + txttodate.Text + "' ";
                }
            }
            if (txtPO.Text != "")
            {
                Qstr += " AND CCT_Ticket.ServicePO  LIKE '%" + txtPO.Text + "%' ";
            }
            if (ddlProjectManager.SelectedIndex > 0)
            {
                Qstr += " AND tblPFiles.projectmanagerid = " + ddlProjectManager.SelectedValue;
            }
            Qstr += " ) T order by [Open Date] ASC ";
            return Qstr;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return "";
    }

    private string Bind_Report()
    {
        string Qstr = string.Empty;

        try
        {
            Qstr += " ;WITH BaseData AS ( ";

            Qstr += " SELECT ROW_NUMBER() OVER ( ";
            Qstr += " PARTITION BY CCT_Ticket.id ";
            Qstr += " ORDER BY CCT_TicketDetails.summarydate DESC ";
            Qstr += " ) AS DenseRank, ";

            Qstr += " tblProjects.JobID, ";

            Qstr += " CONCAT( ";
            Qstr += " CAST(tblProjects.JobID AS varchar(50)), ', ', ";
            Qstr += " tblCustomers.CompanyName, ', ', ";
            Qstr += " tblCustomers.City, ', ', ";
            Qstr += " tblStates.[State], ', ', ";
            Qstr += " tblCountries.Country ";
            Qstr += " ) AS [Project Name], ";

            Qstr += " CCT_Ticket.TicketNo AS [Ticket No], ";

            Qstr += " CASE ";
            Qstr += " WHEN CCT_Ticket.CategoryID <> 3 ";
            Qstr += " THEN CCT_Category.[name] ";
            Qstr += " ELSE CCT_Ticket.CategoryOther ";
            Qstr += " END AS [Category], ";

            Qstr += " ISNULL(UPPER(projectManager.FirstName), '') ";
            Qstr += " AS ProjectManager, ";

            Qstr += " ISNULL(projectManager.EmployeeID, -1) ";
            Qstr += " AS ProjectManagerID, ";

            Qstr += " CCT_Ticket.OpenDate AS RawOpenDate, ";

            Qstr += " CONVERT(varchar, CCT_Ticket.OpenDate, 101) ";
            Qstr += " AS [Open Date], ";

            Qstr += " CONVERT(varchar, CCT_Ticket.CloseDate, 101) ";
            Qstr += " AS [Close Date], ";

            Qstr += " CONVERT(varchar, CCT_Ticket.FollowUpDate, 101) ";
            Qstr += " AS [FollowUp Date], ";

            Qstr += " CASE ";
            Qstr += " WHEN CCT_Ticket.SubAssemblyID = 18 ";
            Qstr += " THEN CCT_Ticket.SubAssemblyOther ";
            Qstr += " ELSE CCT_SubAssembly.[name] ";
            Qstr += " END AS [Sub Assembly], ";

            Qstr += " tblConveyorType.ConveyorType AS [Conveyor Type], ";

            Qstr += " CCT_Status.[name] AS [Status], ";

            Qstr += " tblEmployees.FirstName AS [Assigned To], ";

            Qstr += " CCT_Ticket.ServicePO AS [Service PO], ";

            Qstr += " CCT_Ticket.Task AS [Task], ";

            Qstr += " CONVERT(varchar, CCT_TicketDetails.summarydate, 101) ";
            Qstr += " AS [Summary Date], ";

            Qstr += " CCT_TicketDetails.summary AS [Summary], ";

            // Open Days Red
            Qstr += " CASE ";
            Qstr += " WHEN CCT_Status.[name] = 'Open' ";
            Qstr += " AND ( ";

            Qstr += " DATEDIFF(DAY, CAST(CCT_Ticket.OpenDate AS date), ";
            Qstr += " CAST(GETDATE() AS date)) ";

            Qstr += " - DATEDIFF(WEEK, CAST(CCT_Ticket.OpenDate AS date), ";
            Qstr += " CAST(GETDATE() AS date)) * 2 ";

            Qstr += " - CASE ";
            Qstr += " WHEN DATEPART(WEEKDAY, CCT_Ticket.OpenDate) IN (1, 7) ";
            Qstr += " THEN 1 ELSE 0 ";
            Qstr += " END ";

            Qstr += " + CASE ";
            Qstr += " WHEN DATEPART(WEEKDAY, GETDATE()) IN (1, 7) ";
            Qstr += " THEN 1 ELSE 0 ";
            Qstr += " END ";

            Qstr += " ) > 7 ";

            Qstr += " AND ( ";

            Qstr += " DATEDIFF(DAY, CAST(CCT_Ticket.OpenDate AS date), ";
            Qstr += " CAST(GETDATE() AS date)) ";

            Qstr += " - DATEDIFF(WEEK, CAST(CCT_Ticket.OpenDate AS date), ";
            Qstr += " CAST(GETDATE() AS date)) * 2 ";

            Qstr += " - CASE ";
            Qstr += " WHEN DATEPART(WEEKDAY, CCT_Ticket.OpenDate) IN (1, 7) ";
            Qstr += " THEN 1 ELSE 0 ";
            Qstr += " END ";

            Qstr += " + CASE ";
            Qstr += " WHEN DATEPART(WEEKDAY, GETDATE()) IN (1, 7) ";
            Qstr += " THEN 1 ELSE 0 ";
            Qstr += " END ";

            Qstr += " ) <= 30 ";
            Qstr += " THEN 1 ELSE 0 ";
            Qstr += " END AS [Open Days Red], ";

            // Open Days Yellow
            Qstr += " CASE ";
            Qstr += " WHEN CCT_Status.[name] = 'Open' ";
            Qstr += " AND ( ";

            Qstr += " DATEDIFF(DAY, CAST(CCT_Ticket.OpenDate AS date), ";
            Qstr += " CAST(GETDATE() AS date)) ";

            Qstr += " - DATEDIFF(WEEK, CAST(CCT_Ticket.OpenDate AS date), ";
            Qstr += " CAST(GETDATE() AS date)) * 2 ";

            Qstr += " - CASE ";
            Qstr += " WHEN DATEPART(WEEKDAY, CCT_Ticket.OpenDate) IN (1, 7) ";
            Qstr += " THEN 1 ELSE 0 ";
            Qstr += " END ";

            Qstr += " + CASE ";
            Qstr += " WHEN DATEPART(WEEKDAY, GETDATE()) IN (1, 7) ";
            Qstr += " THEN 1 ELSE 0 ";
            Qstr += " END ";

            Qstr += " ) > 30 ";
            Qstr += " THEN 1 ELSE 0 ";
            Qstr += " END AS [Open Days Yellow] ";

            // Tables
            Qstr += " FROM CCT_Ticket ";

            Qstr += " LEFT JOIN CCT_TicketDetails ";
            Qstr += " ON CCT_Ticket.id = CCT_TicketDetails.TicketID ";

            Qstr += " LEFT JOIN CCT_Category ";
            Qstr += " ON CCT_Category.id = CCT_Ticket.CategoryID ";

            Qstr += " LEFT JOIN CCT_IssueCategory ";
            Qstr += " ON CCT_IssueCategory.id = CCT_Ticket.IssueCategoryID ";

            Qstr += " LEFT JOIN CCT_IssueReportedBy ";
            Qstr += " ON CCT_IssueReportedBy.id = CCT_Ticket.IssueCategoryID ";

            Qstr += " LEFT JOIN CCT_SubAssembly ";
            Qstr += " ON CCT_SubAssembly.id = CCT_Ticket.SubAssemblyID ";

            Qstr += " LEFT JOIN tblProjects ";
            Qstr += " ON tblProjects.JobID = CCT_Ticket.JobID ";

            Qstr += " LEFT JOIN tblPFiles ";
            Qstr += " ON tblPFiles.PNumber = tblProjects.ProposalID ";

            Qstr += " LEFT JOIN tblConveyorType ";
            Qstr += " ON tblConveyorType.ConveyorTypeID = tblPFiles.ConveyorTypeID ";

            Qstr += " LEFT JOIN CCT_Status ";
            Qstr += " ON CCT_Status.id = CCT_Ticket.StatusID ";

            Qstr += " LEFT JOIN tblEmployees ";
            Qstr += " ON CCT_Ticket.AssignedTo = tblEmployees.EmployeeID ";

            Qstr += " LEFT JOIN tblEmployees AS projectManager ";
            Qstr += " ON tblPFiles.projectmanagerid = projectManager.EmployeeID ";

            Qstr += " INNER JOIN tblCustomers ";
            Qstr += " ON tblCustomers.CustomerID = tblProjects.CustomerID ";

            Qstr += " LEFT JOIN tblCountries ";
            Qstr += " ON tblCustomers.CountryID = tblCountries.CountryID ";

            Qstr += " LEFT JOIN tblStates ";
            Qstr += " ON tblCustomers.StateID = tblStates.StateID ";

            // Base conditions
            Qstr += " WHERE CCT_Ticket.TicketNo IS NOT NULL ";          

            // Project filter
            if (ddlProjectNameList.SelectedIndex > 0)
            {
                string projectJobID =
                    ddlProjectNameList.SelectedValue.Split(',')[0].Trim();

                Qstr += " AND tblProjects.JobID LIKE '%"
                    + projectJobID.Replace("'", "''")
                    + "%' ";
            }

            // Ticket number filter
            if (!string.IsNullOrEmpty(txtTicketNo.Text))
            {
                Qstr += " AND CCT_Ticket.TicketNo = '"
                    + txtTicketNo.Text.Replace("'", "''")
                    + "' ";
            }

            // Category filter
            var index = 0;

            foreach (ListItem item in ddlCategory.Items)
            {
                if (item.Selected)
                {
                    if (index == 0)
                    {
                        Qstr += " AND CCT_Ticket.CategoryID IN ( ";
                    }
                    else
                    {
                        Qstr += " , ";
                    }

                    Qstr += "'" + item.Value.Replace("'", "''") + "'";
                    index++;
                }
            }

            if (index > 0)
            {
                Qstr += " ) ";
            }

            // Issue category filter
            if (ddlIssueCategory.SelectedIndex > 0)
            {
                Qstr += " AND CCT_Ticket.IssueCategoryID = '"
                    + ddlIssueCategory.SelectedValue.Replace("'", "''")
                    + "' ";
            }

            // Assigned employee filter
            if (ddlAssignedto.SelectedIndex > 0)
            {
                Qstr += " AND CCT_Ticket.AssignedTo = '"
                    + ddlAssignedto.SelectedValue.Replace("'", "''")
                    + "' ";
            }

            // Subassembly filter
            if (ddlSubAssembly.SelectedIndex > 0)
            {
                Qstr += " AND CCT_Ticket.SubAssemblyID = '"
                    + ddlSubAssembly.SelectedValue.Replace("'", "''")
                    + "' ";
            }

            // Conveyor type filter
            if (ddlConveyorType.SelectedIndex > 0)
            {
                Qstr += " AND tblPFiles.ConveyorTypeID = '"
                    + ddlConveyorType.SelectedValue.Replace("'", "''")
                    + "' ";
            }

            // Status filter
            if (ddlStatus.SelectedIndex > 0)
            {
                Qstr += " AND CCT_Ticket.StatusID = '"
                    + ddlStatus.SelectedValue.Replace("'", "''")
                    + "' ";
            }

            // Open date filter
            if (!string.IsNullOrEmpty(txtfromdate.Text))
            {
                Qstr += " AND CCT_Ticket.OpenDate >= '"
                    + txtfromdate.Text.Replace("'", "''")
                    + "' ";
            }

            if (!string.IsNullOrEmpty(txttodate.Text))
            {
                Qstr += " AND CCT_Ticket.OpenDate <= '"
                    + txttodate.Text.Replace("'", "''")
                    + "' ";
            }

            // Service PO filter
            if (!string.IsNullOrEmpty(txtPO.Text))
            {
                Qstr += " AND CCT_Ticket.ServicePO LIKE '%"
                    + txtPO.Text.Replace("'", "''")
                    + "%' ";
            }

            // Project manager filter
            if (ddlProjectManager.SelectedIndex > 0)
            {
                Qstr += " AND tblPFiles.projectmanagerid = "
                    + ddlProjectManager.SelectedValue;
            }

            // Close BaseData CTE
            Qstr += " ), ProjectDates AS ( ";

            Qstr += " SELECT ";
            Qstr += " ProjectManagerID, ";
            Qstr += " JobID, ";
            Qstr += " MIN(RawOpenDate) AS JobOldestOpenDate ";

            Qstr += " FROM BaseData ";

            Qstr += " GROUP BY ";
            Qstr += " ProjectManagerID, ";
            Qstr += " JobID ";

            Qstr += " ), ProjectOrder AS ( ";

            Qstr += " SELECT ";
            Qstr += " ProjectManagerID, ";
            Qstr += " JobID, ";
            Qstr += " JobOldestOpenDate, ";

            Qstr += " ROW_NUMBER() OVER ( ";
            Qstr += " PARTITION BY ProjectManagerID ";
            Qstr += " ORDER BY JobOldestOpenDate ASC, JobID ASC ";
            Qstr += " ) AS JobSortOrder ";

            Qstr += " FROM ProjectDates ";

            Qstr += " ) ";

            // Final output
            Qstr += " SELECT ";
            Qstr += " B.DenseRank, ";
            Qstr += " B.JobID, ";
            Qstr += " B.[Project Name], ";
            Qstr += " B.[Ticket No], ";
            Qstr += " B.[Category], ";
            Qstr += " B.ProjectManager, ";

            Qstr += " P.JobOldestOpenDate, ";
            Qstr += " P.JobSortOrder, ";

            Qstr += " B.[Open Date], ";
            Qstr += " B.[Close Date], ";
            Qstr += " B.[FollowUp Date], ";
            Qstr += " B.[Sub Assembly], ";
            Qstr += " B.[Conveyor Type], ";
            Qstr += " B.[Status], ";
            Qstr += " B.[Assigned To], ";
            Qstr += " B.[Service PO], ";
            Qstr += " B.[Task], ";
            Qstr += " B.[Summary Date], ";
            Qstr += " B.[Summary], ";
            Qstr += " B.[Open Days Red], ";
            Qstr += " B.[Open Days Yellow] ";

            Qstr += " FROM BaseData AS B ";

            Qstr += " INNER JOIN ProjectOrder AS P ";
            Qstr += " ON P.ProjectManagerID = B.ProjectManagerID ";

            Qstr += " AND ( ";
            Qstr += " P.JobID = B.JobID ";
            Qstr += " OR (P.JobID IS NULL AND B.JobID IS NULL) ";
            Qstr += " ) ";

            // Final sorting
            Qstr += " ORDER BY ";       
            Qstr += " B.RawOpenDate ASC ";         

            return Qstr;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }

        return "";
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();

            string Qstr = PrepareSQLCommandForReport();

            clscon.Return_DT(dt, Qstr);

            if (dt.Rows.Count > 0)
            {
                // Remove DenseRank if you don't want it in Excel
                if (dt.Columns.Contains("DenseRank"))
                    dt.Columns.Remove("DenseRank");

                ExportToExcel(dt);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ExportToExcel(DataTable dt)
    {
        try
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                ExcelWorksheet ws =
                    package.Workbook.Worksheets.Add("Customercare Tickets");

                string[] parentColumns =
                {
                "Project Name",
                "Ticket No",
                "Category",
                "ProjectManager",
                "Open Date",
                "Close Date",
                "FollowUp Date",
                "Sub Assembly",
                "Conveyor Type",
                "Status",
                "Assigned To",
                "Service PO",
                "Task"
            };

                int totalColumns = parentColumns.Length + 2;

                // ============================================
                // HEADER
                // ============================================

                int col = 1;

                foreach (string column in parentColumns)
                {
                    ws.Cells[1, col].Value = column;
                    col++;
                }

                ws.Cells[1, col].Value = "Summary Date";
                ws.Cells[1, col + 1].Value = "Summary";

                // Header formatting
                ExcelRange header = ws.Cells[1, 1, 1, totalColumns];
                header.Style.Font.Bold = true;
                ws.View.FreezePanes(2, 1);
                var groups = dt.AsEnumerable()
                    .GroupBy(dataRow => new
                    {
                        ProjectName = dataRow["Project Name"].ToString(),
                        TicketNo = dataRow["Ticket No"].ToString()
                    });

                int excelRow = 2;

                foreach (var group in groups)
                {
                    DataRow parentRow = group.First();

                    // ========================================
                    // FIND FIRST VALID SUMMARY ROW
                    // ========================================

                    DataRow firstSummaryRow = null;
                    foreach (DataRow childRow in group)
                    {
                        bool summaryDateEmpty =
                            childRow["Summary Date"] == DBNull.Value ||
                            string.IsNullOrEmpty(
                                childRow["Summary Date"].ToString().Trim()
                            );

                        bool summaryEmpty =
                            childRow["Summary"] == DBNull.Value ||
                            string.IsNullOrEmpty(
                                childRow["Summary"].ToString().Trim()
                            );

                        if (!summaryDateEmpty || !summaryEmpty)
                        {
                            firstSummaryRow = childRow;
                            break;
                        }
                    }

                    // ========================================
                    // PARENT ROW
                    // ========================================

                    col = 1;

                    foreach (string column in parentColumns)
                    {
                        ws.Cells[excelRow, col].Value =
                            CleanExcelValue(parentRow[column]);

                        col++;
                    }

                    // ========================================
                    // FIRST SUMMARY ON SAME ROW AS PARENT
                    // ========================================

                    if (firstSummaryRow != null)
                    {
                        bool summaryDateEmpty = firstSummaryRow["Summary Date"] == DBNull.Value || string.IsNullOrEmpty(firstSummaryRow["Summary Date"].ToString().Trim());
                        bool summaryEmpty = firstSummaryRow["Summary"] == DBNull.Value || string.IsNullOrEmpty(firstSummaryRow["Summary"].ToString().Trim());

                        // Summary Date
                        if (!summaryDateEmpty)
                        {
                            ws.Cells[excelRow, parentColumns.Length + 1].Value = CleanExcelValue(firstSummaryRow["Summary Date"]);
                        }

                        // Summary
                        if (!summaryEmpty)
                        {
                            ws.Cells[excelRow, parentColumns.Length + 2].Value =CleanExcelValue(firstSummaryRow["Summary"]);
                        }
                    }

                    // Move to next row
                    excelRow++;

                    // ========================================
                    // REMAINING SUMMARY ROWS
                    // ========================================

                    bool firstSummarySkipped = false;

                    foreach (DataRow childRow in group)
                    {
                        bool summaryDateEmpty =
                            childRow["Summary Date"] == DBNull.Value ||
                            string.IsNullOrEmpty(
                                childRow["Summary Date"].ToString().Trim()
                            );

                        bool summaryEmpty =
                            childRow["Summary"] == DBNull.Value ||
                            string.IsNullOrEmpty(
                                childRow["Summary"].ToString().Trim()
                            );

                        // Skip rows where both are empty
                        if (summaryDateEmpty && summaryEmpty)
                            continue;

                        // First summary was already written
                        // on the parent row
                        if (!firstSummarySkipped &&
                            firstSummaryRow != null &&
                            childRow == firstSummaryRow)
                        {
                            firstSummarySkipped = true;
                            continue;
                        }

                        // Summary Date
                        if (!summaryDateEmpty)
                        {
                            ws.Cells[
                                excelRow,
                                parentColumns.Length + 1
                            ].Value =
                                CleanExcelValue(
                                    childRow["Summary Date"]
                                );
                        }

                        // Summary
                        if (!summaryEmpty)
                        {
                            ws.Cells[
                                excelRow,
                                parentColumns.Length + 2
                            ].Value =
                                CleanExcelValue(
                                    childRow["Summary"]
                                );
                        }

                        excelRow++;
                    }
                }

                // ============================================
                // FORMAT COLUMNS
                // ============================================

                // Summary Date
                ws.Column(totalColumns - 1).Width = 15;

                // Summary
                ws.Column(totalColumns).Width = 60;
                ws.Column(totalColumns).Style.WrapText = true;

                // Other columns
                for (int i = 1; i <= parentColumns.Length; i++)
                {
                    ws.Column(i).Width = 18;
                }

                // Project Name
                ws.Column(1).Width = 45;

                // Task
                ws.Column(parentColumns.Length).Width = 30;
                ws.Column(parentColumns.Length).Style.WrapText = true;

                // ============================================
                // NO BORDERS
                // ============================================

                ExcelRange usedRange =
                    ws.Cells[1, 1, excelRow - 1, totalColumns];

                usedRange.Style.Border.Top.Style =
                    ExcelBorderStyle.None;

                usedRange.Style.Border.Bottom.Style =
                    ExcelBorderStyle.None;

                usedRange.Style.Border.Left.Style =
                    ExcelBorderStyle.None;

                usedRange.Style.Border.Right.Style =
                    ExcelBorderStyle.None;

                // ============================================
                // DOWNLOAD XLSX
                // ============================================

                byte[] fileBytes = package.GetAsByteArray();

                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();

                Response.Buffer = true;

                Response.ContentType =
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                Response.AddHeader(
                    "Content-Disposition",
                    "attachment; filename=Customercare Tickets.xlsx"
                );

                Response.AddHeader(
                    "Content-Length",
                    fileBytes.Length.ToString()
                );

                Response.OutputStream.Write(
                    fileBytes,
                    0,
                    fileBytes.Length
                );

                Response.Flush();

                HttpContext.Current.ApplicationInstance
                    .CompleteRequest();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private string CleanExcelValue(object value)
    {
        if (value == null || value == DBNull.Value)
            return "";

        string text = value.ToString();

        StringBuilder result = new StringBuilder();

        foreach (char c in text)
        {
            // Valid XML characters
            if (c == '\t' ||
                c == '\n' ||
                c == '\r' ||
                (c >= 0x20 && c <= 0xD7FF) ||
                (c >= 0xE000 && c <= 0xFFFD))
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }

    private DataTable ReportData()
    {
        DataTable dt = new DataTable();
        try
        {
            //divError.Visible = true;
            string query = Bind_Report();
            if (query.Length > 1)
            {
                clscon.Return_DT(dt, query);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    private void Get_CustomerCareTicketReport()
    {
        try
        {
            string HeaderText = "Project Tickets Report ";
            if (txtfromdate.Text != "" && txttodate.Text != "")
            {
                HeaderText += " From " + txtfromdate.Text + " to " + txttodate.Text;
            }
            //divError.Visible = true;
            DataTable dt = ReportData();
            rprt.Load(Server.MapPath("~/Reports/rptCustomerCareTickets.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = HeaderText;
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = HeaderText;
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
        }
        catch (Exception ex)
        {
            if (ex.Message != "Thread was being aborted.")
            {
                Utility.AddEditException(ex);
            }
        }
        finally
        {
            rprt.Close();
            rprt.Dispose();
        }
    }

    protected void gvSummary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;

            string projectName = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Project Name"));

            string ticketNo = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Ticket No"));

            GridView gvChild = e.Row.FindControl("gvChild") as GridView;

            if (gvChild == null)
                return;

            DataTable dt = ViewState["dtView"] as DataTable;

            if (dt == null || dt.Rows.Count == 0)
                return;

            DataRow[] rows = dt.Select("[Project Name] = '" +  projectName.Replace("'", "''") + "' AND [Ticket No] = '" +   ticketNo.Replace("'", "''") +  "'");

            if (rows.Length == 0)
                return;

            DataTable dtChild = rows.CopyToDataTable();
            // Remove rows where BOTH Summary Date and Summary are NULL/empty
            DataRow[] validRows = dtChild.Select("([Summary Date] IS NOT NULL AND [Summary Date] <> '') " + "OR ([Summary] IS NOT NULL AND [Summary] <> '')");

            if (validRows.Length > 0)
            {
                dtChild = validRows.CopyToDataTable();
            }
            else
            {
                dtChild = dtChild.Clone();
            }
            // Only columns required in the child grid
            dtChild = dtChild.DefaultView.ToTable(true,  "Project Name", "Ticket No", "SummaryDateSort", "Summary Date", "Summary");
            dtChild.DefaultView.Sort = "SummaryDateSort DESC";
            dtChild = dtChild.DefaultView.ToTable();
            gvChild.DataSource = dtChild;
            gvChild.DataBind();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}