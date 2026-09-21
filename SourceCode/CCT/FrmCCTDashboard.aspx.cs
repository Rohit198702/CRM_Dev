using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CCT_FrmCCTDashboard : System.Web.UI.Page
{
    ReportDocument rprt = new ReportDocument();
    commonclass1 cls = new commonclass1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetDates();
            ShippingOverview();
            //LoadStatusProjects("NOTSHIPPED");
        }
    }

    private void ShippingOverview()
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        try
        {
            cls.Return_DS(ds, "EXEC Get_CCTDashboard 1, 1, '" + txtFromDate.Text + "', '" + txtToDate.Text + "' ");
            dt = ds.Tables[0];
            if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0][0].ToString().Trim() == "0")
                {
                    lnkNotShipped.Visible = false;
                    lnkPreviewNotShipped.Visible = false;
                }
                else
                {
                    lnkNotShipped.Visible = true;
                    lnkPreviewNotShipped.Visible = true;
                }
                btnNotShipped.Text = dt.Rows[0][0].ToString();

                if (dt.Rows[0][1].ToString().Trim() == "0")
                {
                    lnkShipped.Visible = false;
                    lnkPreviewShipped.Visible = false;
                }
                else
                {
                    lnkShipped.Visible = true;
                    lnkPreviewShipped.Visible = true;
                }
                btnShipped.Text = dt.Rows[0][1].ToString();

                if (dt.Rows[0][2].ToString().Trim() == "0")
                {
                    lnkDelivered.Visible = false;
                    lnkPreviewDelivered.Visible = false;
                }
                else
                {
                    lnkDelivered.Visible = true;
                    lnkPreviewDelivered.Visible = true;
                }
                btnDelivered.Text = dt.Rows[0][2].ToString();

                if (dt.Rows[0][3].ToString().Trim() == "0")
                {
                    lnkInTransit.Visible = false;
                    lnkPreviewInTransit.Visible = false;
                }
                else
                {
                    lnkInTransit.Visible = true;
                    lnkPreviewInTransit.Visible = true;
                }
                btnInTransit.Text = dt.Rows[0][3].ToString();

                if (dt.Rows[0][4].ToString().Trim() == "0")
                {
                    lnkInstallation_Scheduled.Visible = false;
                    lnkPreviewInstallation_Scheduled.Visible = false;
                }
                else
                {
                    lnkInstallation_Scheduled.Visible = true;
                    lnkPreviewInstallation_Scheduled.Visible = true;
                }
                btnInstallation_Scheduled.Text = dt.Rows[0][4].ToString();

                if (dt.Rows[0][5].ToString().Trim() == "0")
                {
                    lnkInstallation_InProgress.Visible = false;
                    lnkPreviewInstallation_InProgress.Visible = false;
                }
                else
                {
                    lnkInstallation_InProgress.Visible = true;
                    lnkPreviewInstallation_InProgress.Visible = true;
                }
                btnInstallation_InProgress.Text = dt.Rows[0][5].ToString();

                if (dt.Rows[0][6].ToString().Trim() == "0")
                {
                    lnkInstallation_Completed.Visible = false;
                    lnkPreviewInstallation_Completed.Visible = false;
                }
                else
                {
                    lnkInstallation_Completed.Visible = true;
                    lnkPreviewInstallation_Completed.Visible = true;
                }
                btnInstallation_Completed.Text = dt.Rows[0][6].ToString();
            }

            dt = ds.Tables[1];
            if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0][0].ToString().Trim() == "0")
                {
                    lnkDelayed.Visible = false;
                    lnkPreviewDelayed.Visible = false;
                }
                else
                {
                    lnkDelayed.Visible = true;
                    lnkPreviewDelayed.Visible = true;
                }
                btnDelayed.Text = dt.Rows[0][0].ToString();
            }

            dt = ds.Tables[2];
            if (dt.Rows.Count == 1)
            {
                if (dt.Rows[0][0].ToString().Trim() == "0")
                {
                    lnkRepair_Pending.Visible = false;
                    lnkPreviewRepair_Pending.Visible = false;
                }
                else
                {
                    lnkRepair_Pending.Visible = true;
                    lnkPreviewRepair_Pending.Visible = true;
                }
                btnRepair_Pending.Text = dt.Rows[0][0].ToString();

                if (dt.Rows[0][1].ToString().Trim() == "0")
                {
                    lnkRepair_Resolved.Visible = false;
                    lnkPreviewRepair_Resolved.Visible = false;
                }
                else
                {
                    lnkRepair_Resolved.Visible = true;
                    lnkPreviewRepair_Resolved.Visible = true;
                }
                btnRepair_Resolved.Text = dt.Rows[0][1].ToString();

                if (dt.Rows[0][2].ToString().Trim() == "0")
                {
                    lnkRepair_Followup.Visible = false;
                    lnkPreviewRepair_Followup.Visible = false;
                }
                else
                {
                    lnkRepair_Followup.Visible = true;
                    lnkPreviewRepair_Followup.Visible = true;
                }
                btnRepair_Followup.Text = dt.Rows[0][2].ToString();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private DataTable GetNotShippedData()
    {
        DataTable dt = new DataTable();
        try
        {
            cls.Return_DT(dt, "EXEC Get_CCTDashboard 2, 1, '" + txtFromDate.Text + "', '" + txtToDate.Text + "' ");
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    private DataTable GetShippedData()
    {
        DataTable dt = new DataTable();
        try
        {
            cls.Return_DT(dt, "EXEC Get_CCTDashboard 2, 2, '" + txtFromDate.Text + "', '" + txtToDate.Text + "' ");
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    private DataTable GetDeliveredData()
    {
        DataTable dt = new DataTable();
        try
        {
            cls.Return_DT(dt, "EXEC Get_CCTDashboard 2, 3, '" + txtFromDate.Text + "', '" + txtToDate.Text + "' ");
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    private DataTable GetInTransitData()
    {
        DataTable dt = new DataTable();
        try
        {
            cls.Return_DT(dt, "EXEC Get_CCTDashboard 2, 4, '" + txtFromDate.Text + "', '" + txtToDate.Text + "' ");
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    private DataTable GetDelayedData()
    {
        DataTable dt = new DataTable();
        try
        {
            cls.Return_DT(dt, "EXEC Get_CCTDashboard 8, 5, '" + txtFromDate.Text + "', '" + txtToDate.Text + "' ");
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    private DataTable GetInstallationSchedule()
    {
        DataTable dt = new DataTable();
        try
        {
            cls.Return_DT(dt, "EXEC Get_CCTDashboard 6, 6, '" + txtFromDate.Text + "', '" + txtToDate.Text + "' ");
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    private DataTable GetInstallationInProgress()
    {
        DataTable dt = new DataTable();
        try
        {
            cls.Return_DT(dt, "EXEC Get_CCTDashboard 6, 7, '" + txtFromDate.Text + "', '" + txtToDate.Text + "' ");
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    private DataTable GetInstallationCompleted()
    {
        DataTable dt = new DataTable();
        try
        {
            cls.Return_DT(dt, "EXEC Get_CCTDashboard 6, 8, '" + txtFromDate.Text + "', '" + txtToDate.Text + "' ");
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    private DataTable GetRepairPending()
    {
        DataTable dt = new DataTable();
        try
        {
            cls.Return_DT(dt, "EXEC Get_CCTDashboard 7, 9, '" + txtFromDate.Text + "', '" + txtToDate.Text + "' ");
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    private DataTable GetRepairResolved()
    {
        DataTable dt = new DataTable();
        try
        {
            cls.Return_DT(dt, "EXEC Get_CCTDashboard 7, 11, '" + txtFromDate.Text + "', '" + txtToDate.Text + "' ");
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    private DataTable GetRepairFollowup()
    {
        DataTable dt = new DataTable();
        try
        {
            cls.Return_DT(dt, "EXEC Get_CCTDashboard 7, 12, '" + txtFromDate.Text + "', '" + txtToDate.Text + "' ");
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    private void LoadStatusProjects(string status)
    {
        DataTable dt = new DataTable();
        try
        {
            if (status == "NOTSHIPPED")
            {
                FocusShippingGrid();
                ShowTab("Shipping");
                dt = GetNotShippedData();
                gvShipping.DataSource = dt;
                gvShipping.DataKeyNames = new[] { "JobID" };
                gvShipping.DataBind();
                //hdDailyShippingActivity.Visible = true;
                hdDailyShippingActivity.InnerText = "Shipping Overview - Not Shipped";
            }

            else if (status == "SHIPPED")
            {
                FocusShippingGrid();
                ShowTab("Shipping");
                dt = GetShippedData();
                gvShipping.DataSource = dt;
                gvShipping.DataKeyNames = new[] { "JobID" };
                gvShipping.DataBind();
                //hdDailyShippingActivity.Visible = true;
                hdDailyShippingActivity.InnerText = "Shipping Overview - Shipped";
            }

            else if (status == "DELIVERED")
            {
                FocusShippingGrid();
                ShowTab("Shipping");
                dt = GetDeliveredData();
                gvShipping.DataSource = dt;
                gvShipping.DataKeyNames = new[] { "JobID" };
                gvShipping.DataBind();
                //hdDailyShippingActivity.Visible = true;
                hdDailyShippingActivity.InnerText = "Shipping Overview - Delivered";
            }

            else if (status == "INTRANSIT")
            {
                FocusShippingGrid();
                ShowTab("Shipping");
                dt = GetInTransitData();
                gvShipping.DataSource = dt;
                gvShipping.DataKeyNames = new[] { "JobID" };
                gvShipping.DataBind();
                //hdDailyShippingActivity.Visible = true;
                hdDailyShippingActivity.InnerText = "Shipping Overview - In Transit";
            }

            else if (status == "DELAYED")
            {
                FocusShippingGrid();
                ShowTab("Shipping");
                dt = GetDelayedData();
                gvShipping.DataSource = dt;
                gvShipping.DataKeyNames = new[] { "JobID" };
                gvShipping.DataBind();
                //hdDailyShippingActivity.Visible = true;
                hdDailyShippingActivity.InnerText = "Shipping Overview - Delayed";
            }

            else if (status == "INSTALLATION_SCHEDULED")
            {
                FocusInstallationGrid();
                ShowTab("Installation");
                dt = GetInstallationSchedule();
                gvInstallation.DataSource = dt;
                gvInstallation.DataKeyNames = new[] { "JobID" };
                gvInstallation.DataBind();
                //hdDailyShippingActivity.Visible = true;
                h1InstallationActivity.InnerText = "Installation Overview - Scheduled";
            }

            else if (status == "INSTALLATION_INPROGRESS")
            {
                FocusInstallationGrid();
                ShowTab("Installation");
                dt = GetInstallationInProgress();
                gvInstallation.DataSource = dt;
                gvInstallation.DataKeyNames = new[] { "JobID" };
                gvInstallation.DataBind();
                //hdDailyShippingActivity.Visible = true;
                h1InstallationActivity.InnerText = "Installation Overview - In Progress";
            }

            else if (status == "INSTALLATION_COMPLETED")
            {
                FocusInstallationGrid();
                ShowTab("Installation");
                dt = GetInstallationCompleted();
                gvInstallation.DataSource = dt;
                gvInstallation.DataKeyNames = new[] { "JobID" };
                gvInstallation.DataBind();
                //hdDailyShippingActivity.Visible = true;
                h1InstallationActivity.InnerText = "Installation Overview - Completed";
            }

            else if (status == "REPAIR_PENDING")
            {
                FocusRepairGrid();
                ShowTab("Repair");
                dt = GetRepairPending();
                gvRepair.DataSource = dt;
                if (dt.Columns.Contains("Ticket No"))
                {
                    gvRepair.DataKeyNames = new[] { "Ticket No" };
                }
                else
                {
                    gvRepair.DataKeyNames = new[] { "JobID" };
                }
                gvRepair.DataBind();

                //hdDailyShippingActivity.Visible = true;
                h1RepairActivity.InnerText = "Repair Overview - Open";
            }

            else if (status == "REPAIR_RESOLVED")
            {
                FocusRepairGrid();
                ShowTab("Repair");
                dt = GetRepairResolved();
                gvRepair.DataSource = dt;
                if (dt.Columns.Contains("Ticket No"))
                {
                    gvRepair.DataKeyNames = new[] { "Ticket No" };
                }
                else
                {
                    gvRepair.DataKeyNames = new[] { "JobID" };
                }
                gvRepair.DataBind();

                //hdDailyShippingActivity.Visible = true;
                h1RepairActivity.InnerText = "Repair Overview - Closed";
            }

            else if (status == "REPAIR_FOLLOWUP")
            {
                FocusRepairGrid();
                ShowTab("Repair");
                dt = GetRepairFollowup();
                gvRepair.DataSource = dt;
                if (dt.Columns.Contains("Ticket No"))
                {
                    gvRepair.DataKeyNames = new[] { "Ticket No" };
                }
                else
                {
                    gvRepair.DataKeyNames = new[] { "JobID" };
                }
                gvRepair.DataBind();

                //hdDailyShippingActivity.Visible = true;
                h1RepairActivity.InnerText = "Repair Overview - Followup";
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void lnkNotShipped_Click(object sender, EventArgs e)
    {
        LoadStatusProjects("NOTSHIPPED");
    }

    protected void lnkShipped_Click(object sender, EventArgs e)
    {
        LoadStatusProjects("SHIPPED");
    }

    protected void lnkDelivered_Click(object sender, EventArgs e)
    {
        LoadStatusProjects("DELIVERED");
    }

    protected void lnkDelayed_Click(object sender, EventArgs e)
    {
        LoadStatusProjects("DELAYED");
    }

    protected void gvShipping_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.cursor = 'Pointer'";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.ToolTip = "Click to view Project details";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvShipping, "Select$" + e.Row.RowIndex);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvShipping_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string jobId = gvShipping.DataKeys[index].Value.ToString();

                string link = "window.open('/SalesManagement/FrmProjects.aspx?jid=" + jobId + "', '_blank');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "openWindow", link, true);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvInstallation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.cursor = 'Pointer'";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.ToolTip = "Click to view Project details";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvInstallation, "Select$" + e.Row.RowIndex);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvInstallation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string jobId = gvInstallation.DataKeys[index].Value.ToString();

                string link = "window.open('/SalesManagement/FrmProjects.aspx?jid=" + jobId + "', '_blank');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "openWindow", link, true);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvRepair_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.cursor = 'Pointer'";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.ToolTip = "Click to view ticket";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvRepair, "Select$" + e.Row.RowIndex);

                DateTime openDate = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Open Date"));
                string status = DataBinder.Eval(e.Row.DataItem, "Status").ToString();

                if (status.Equals("Open", StringComparison.OrdinalIgnoreCase) &&
                    openDate <= DateTime.Now.AddDays(-5))
                {
                    e.Row.Attributes["style"] = "background-color:#ff4d4d !important;";
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvRepair_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string jobId = gvRepair.DataKeys[index].Value.ToString();
                string link = "window.open('/CCT/frmCustomerCareTickets.aspx?tid=" + jobId + "', '_blank');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "openWindow", link, true);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private DateTime GetMonday()
    {
        DateTime today = DateTime.Today;
        DateTime monday = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
        try
        {
            // Handle case when today is Sunday (DayOfWeek = 0)
            if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                monday = today.AddDays(-6);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return monday;
    }

    private DateTime GetFriday()
    {
        DateTime today = DateTime.Today;
        DateTime friday = new DateTime();
        DateTime monday = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
        try
        {
            // Handle case when today is Sunday (DayOfWeek = 0)
            if (today.DayOfWeek == DayOfWeek.Sunday)
            {
                monday = today.AddDays(-6);
            }

            friday = monday.AddDays(4);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return friday;
    }

    private void SetDates()
    {
        try
        {
            txtFromDate.Text = GetMonday().ToShortDateString();
            txtToDate.Text = GetFriday().ToShortDateString();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    public class WeeklyShippingActivity
    {
        String _WeekDay;
        public String WeekDay
        {
            get { return _WeekDay; }
            set { _WeekDay = value; }
        }

        Decimal _ProjectsShipped;
        public Decimal ProjectsShipped
        {
            get { return _ProjectsShipped; }
            set { _ProjectsShipped = value; }
        }
    }

    [WebMethod]
    public static List<WeeklyShippingActivity> Get_WeeklyShippingActivity()
    {
        List<WeeklyShippingActivity> weeklyShippingActivity = new List<WeeklyShippingActivity>();
        try
        {
            DataSet ds = Utility.GetWeeklyShippingActivity(3); // Your data source
            DataTable dt = ds.Tables[0];
            DataTable dtFinal = dt.AsEnumerable().CopyToDataTable();
            foreach (DataRow row in dtFinal.Rows)
            {
                //if (row["WeekDay"].ToString() == "Grand Total")
                //    continue;
                weeklyShippingActivity.Add(new WeeklyShippingActivity
                {
                    WeekDay = row["WeekDay"].ToString(),
                    ProjectsShipped = Convert.ToDecimal(row["ProjectsShipped"].ToString())
                });
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }

        return weeklyShippingActivity;
    }

    [WebMethod]
    public static List<WeeklyShippingActivity> Get_WeeklyInstallationActivity()
    {
        List<WeeklyShippingActivity> weeklyInstallationActivity = new List<WeeklyShippingActivity>();
        try
        {
            DataSet ds = Utility.GetWeeklyShippingActivity(9); // Your data source
            DataTable dt = ds.Tables[0];
            DataTable dtFinal = dt.AsEnumerable().CopyToDataTable();
            foreach (DataRow row in dtFinal.Rows)
            {
                //if (row["WeekDay"].ToString() == "Grand Total")
                //    continue;
                weeklyInstallationActivity.Add(new WeeklyShippingActivity
                {
                    WeekDay = row["WeekDay"].ToString(),
                    ProjectsShipped = Convert.ToDecimal(row["ProjectsInstalled"].ToString())
                });
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }

        return weeklyInstallationActivity;
    }

    [WebMethod]
    public static List<WeeklyShippingActivity> Get_WeeklyRepairActivity()
    {
        List<WeeklyShippingActivity> weeklyRepairActivity = new List<WeeklyShippingActivity>();
        try
        {
            DataSet ds = Utility.GetWeeklyShippingActivity(11); // Your data source
            DataTable dt = ds.Tables[0];
            DataTable dtFinal = dt.AsEnumerable().CopyToDataTable();
            foreach (DataRow row in dtFinal.Rows)
            {
                //if (row["WeekDay"].ToString() == "Grand Total")
                //    continue;
                weeklyRepairActivity.Add(new WeeklyShippingActivity
                {
                    WeekDay = row["WeekDay"].ToString(),
                    ProjectsShipped = Convert.ToDecimal(row["ProjectsRepair"].ToString())
                });
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }

        return weeklyRepairActivity;
    }

    [WebMethod]
    public static List<WeeklyShippingActivity> Get_MonthlyShippingActivity()
    {
        List<WeeklyShippingActivity> monthlyShippingActivity = new List<WeeklyShippingActivity>();
        try
        {
            DataSet ds = Utility.GetWeeklyShippingActivity(4); // Your data source
            DataTable dt = ds.Tables[0];
            DataTable dtFinal = dt.AsEnumerable().CopyToDataTable();
            foreach (DataRow row in dtFinal.Rows)
            {
                //if (row["WeekDay"].ToString() == "Grand Total")
                //    continue;
                monthlyShippingActivity.Add(new WeeklyShippingActivity
                {
                    WeekDay = row["Month"].ToString(),
                    ProjectsShipped = Convert.ToDecimal(row["ProjectsShipped"].ToString())
                });
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }

        return monthlyShippingActivity;
    }

    [WebMethod]
    public static List<WeeklyShippingActivity> Get_MonthlyInstallationActivity()
    {
        List<WeeklyShippingActivity> monthlyInstallationActivity = new List<WeeklyShippingActivity>();
        try
        {
            DataSet ds = Utility.GetWeeklyShippingActivity(10); // Your data source
            DataTable dt = ds.Tables[0];
            DataTable dtFinal = dt.AsEnumerable().CopyToDataTable();
            foreach (DataRow row in dtFinal.Rows)
            {
                //if (row["WeekDay"].ToString() == "Grand Total")
                //    continue;
                monthlyInstallationActivity.Add(new WeeklyShippingActivity
                {
                    WeekDay = row["Month"].ToString(),
                    ProjectsShipped = Convert.ToDecimal(row["ProjectsInstalled"].ToString())
                });
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }

        return monthlyInstallationActivity;
    }

    [WebMethod]
    public static List<WeeklyShippingActivity> Get_MonthlyRepairActivity()
    {
        List<WeeklyShippingActivity> monthlyRepairActivity = new List<WeeklyShippingActivity>();
        try
        {
            DataSet ds = Utility.GetWeeklyShippingActivity(12); // Your data source
            DataTable dt = ds.Tables[0];
            DataTable dtFinal = dt.AsEnumerable().CopyToDataTable();
            foreach (DataRow row in dtFinal.Rows)
            {
                //if (row["WeekDay"].ToString() == "Grand Total")
                //    continue;
                monthlyRepairActivity.Add(new WeeklyShippingActivity
                {
                    WeekDay = row["Month"].ToString(),
                    ProjectsShipped = Convert.ToDecimal(row["ProjectsRepair"].ToString())
                });
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }

        return monthlyRepairActivity;
    }

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        ResetAllGrids();
        ShippingOverview();
    }

    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        ResetAllGrids();
        ShippingOverview();
    }

    protected void lnkInTransit_Click(object sender, EventArgs e)
    {
        LoadStatusProjects("INTRANSIT");
    }

    protected void lnkPreviewNotShipped_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = GetNotShippedData();
            rprt.Load(Server.MapPath("~/Reports/rptCCTDashboard_NotShipped.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Not Shipped Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Not Shipped Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
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

    protected void lnkPreviewShipped_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = GetShippedData();
            rprt.Load(Server.MapPath("~/Reports/rptCCTDashboard_NotShipped.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Shipped Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Shipped Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
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

    protected void lnkPreviewDelayed_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = GetDelayedData();
            rprt.Load(Server.MapPath("~/Reports/rptCCTDashboard_Delayed.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Delayed Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Delayed Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
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

    protected void lnkPreviewDelivered_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = GetDeliveredData();
            rprt.Load(Server.MapPath("~/Reports/rptCCTDashboard_NotShipped.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Delivered Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Delivered Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
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

    protected void lnkPreviewInTransit_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = GetInTransitData();
            rprt.Load(Server.MapPath("~/Reports/rptCCTDashboard_NotShipped.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "In-Transit Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "In-Transit Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            SetDates();
            ShippingOverview();
            //hdDailyShippingActivity.Visible = false;
            ResetAllGrids();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void lnkPreviewInstallation_Scheduled_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = GetInstallationSchedule();
            rprt.Load(Server.MapPath("~/Reports/rptCCTDashboard_Scheduled.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Installation Scheduled Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Installation Scheduled Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
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

    protected void lnkInstallation_Scheduled_Click(object sender, EventArgs e)
    {
        LoadStatusProjects("INSTALLATION_SCHEDULED");
    }

    protected void lnkPreviewInstallation_InProgress_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = GetInstallationInProgress();
            rprt.Load(Server.MapPath("~/Reports/rptCCTDashboard_Scheduled.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Installation In Progress Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Installation In Progress Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
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

    protected void lnkInstallation_InProgress_Click(object sender, EventArgs e)
    {
        LoadStatusProjects("INSTALLATION_INPROGRESS");
    }

    protected void lnkPreviewInstallation_Completed_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = GetInstallationCompleted();
            rprt.Load(Server.MapPath("~/Reports/rptCCTDashboard_Scheduled.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Installation Completed Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Installation Completed Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
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

    protected void lnkInstallation_Completed_Click(object sender, EventArgs e)
    {
        LoadStatusProjects("INSTALLATION_COMPLETED");
    }

    protected void lnkPreviewRepair_Pending_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = GetRepairPending();
            rprt.Load(Server.MapPath("~/Reports/rptCCTDashboard_RepairPending.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Repair Pending Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Repair Pending Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
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

    protected void lnkRepair_Pending_Click(object sender, EventArgs e)
    {
        LoadStatusProjects("REPAIR_PENDING");
    }

    protected void lnkPreviewRepair_InProgress_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void lnkRepair_InProgress_Click(object sender, EventArgs e)
    {

    }

    protected void lnkPreviewRepair_Resolved_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = GetRepairResolved();
            rprt.Load(Server.MapPath("~/Reports/rptCCTDashboard_RepairPending.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Repair Resolved Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Repair Resolved Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
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

    protected void lnkRepair_Resolved_Click(object sender, EventArgs e)
    {
        LoadStatusProjects("REPAIR_RESOLVED");
    }

    private void FocusShippingGrid()
    {
        try
        {
            shipping_WeeklyChart.Visible = true;
            shipping_MonthlyChart.Visible = true;

            installation_WeeklyChart.Visible = false;
            installation_MonthlyChart.Visible = false;

            repair_WeeklyChart.Visible = false;
            repair_MonthlyChart.Visible = false;

            h1InstallationActivity.InnerText = "";
            gvInstallation.DataSource = string.Empty;
            gvInstallation.DataBind();

            h1RepairActivity.InnerText = "";
            gvRepair.DataSource = string.Empty;
            gvRepair.DataBind();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void FocusInstallationGrid()
    {
        try
        {
            shipping_WeeklyChart.Visible = false;
            shipping_MonthlyChart.Visible = false;

            installation_WeeklyChart.Visible = true;
            installation_MonthlyChart.Visible = true;

            repair_WeeklyChart.Visible = false;
            repair_MonthlyChart.Visible = false;

            hdDailyShippingActivity.InnerText = "";
            gvShipping.DataSource = string.Empty;
            gvShipping.DataBind();

            h1RepairActivity.InnerText = "";
            gvRepair.DataSource = string.Empty;
            gvRepair.DataBind();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void FocusRepairGrid()
    {
        try
        {
            repair_WeeklyChart.Visible = true;
            repair_MonthlyChart.Visible = true;

            shipping_WeeklyChart.Visible = false;
            shipping_MonthlyChart.Visible = false;

            installation_WeeklyChart.Visible = false;
            installation_MonthlyChart.Visible = false;

            h1InstallationActivity.InnerText = "";
            gvInstallation.DataSource = string.Empty;
            gvInstallation.DataBind();

            hdDailyShippingActivity.InnerText = "";
            gvShipping.DataSource = string.Empty;
            gvShipping.DataBind();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetAllGrids()
    {
        try
        {
            shipping_WeeklyChart.Visible = true;
            shipping_MonthlyChart.Visible = true;

            gvShipping.DataSource = string.Empty;
            gvShipping.DataBind();

            gvInstallation.DataSource = string.Empty;
            gvInstallation.DataBind();

            gvRepair.DataSource = string.Empty;
            gvRepair.DataBind();

            hdDailyShippingActivity.InnerText = "";
            h1InstallationActivity.InnerText = "";
            h1RepairActivity.InnerText = "";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ShowTab(string tabName)
    {
        try
        {
            string strMethodNameNew = "";
            if (tabName == "Shipping")
            {
                strMethodNameNew = "SetShipping();";
            }
            else if (tabName == "Installation")
            {
                strMethodNameNew = "SetInstallation();";
            }
            else if (tabName == "Repair")
            {
                strMethodNameNew = "SetRepair();";
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), Guid.NewGuid().ToString(), strMethodNameNew, true);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void lnkPreviewRepair_Followup_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = GetRepairFollowup();
            rprt.Load(Server.MapPath("~/Reports/rptCCTDashboard_RepairPending.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Repair Followup Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Repair Followup Projects List from " + txtFromDate.Text + " to " + txtToDate.Text;

                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
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

    protected void lnkRepair_Followup_Click(object sender, EventArgs e)
    {
        LoadStatusProjects("REPAIR_FOLLOWUP");
    }
}