using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Data;

public partial class Reports_FrmNestingSchedule : System.Web.UI.Page
{
    commonclass1 cls = new commonclass1();
    ReportDocument rprt = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetDates();
            divAero.Visible = false;
            divITW.Visible = false;
        }
    }

    private void SetDates()
    {
        try
        {
            //txtFromDate.Text = DateTime.Now.Month + "/01/" + DateTime.Now.Year;
            //txtToDate.Text = DateTime.Now.AddMonths(0).Month + "/" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(0).Month) + "/" + DateTime.Now.AddMonths(0).Year;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private Boolean ValidationCheck()
    {
        try
        {

        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    private DataTable ReportData_SubReport1()
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "EXEC Get_NestingSchedule 2, " + ddlProductCode.SelectedValue;
            cls.Return_DT(dt, query);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    private DataTable ReportData_SubReport2()
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "EXEC Get_NestingSchedule 3, " + ddlProductCode.SelectedValue;
            cls.Return_DT(dt, query);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    protected void btnReport_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidationCheck())
            {
                DataTable dt1 = ReportData_SubReport1();
                DataTable dt2 = ReportData_SubReport2();

                if (dt1.Rows.Count > 0)
                {
                    gvAerowerksNesting.DataSource = dt1;
                    gvAerowerksNesting.DataBind();
                    divAero.Visible = true;
                }
                else
                {
                    gvAerowerksNesting.DataSource = string.Empty;
                    gvAerowerksNesting.DataBind();
                    divAero.Visible = false;
                }

                if (dt2.Rows.Count > 0)
                {
                    gvITWNesting.DataSource = dt2;
                    gvITWNesting.DataBind();
                    divITW.Visible = true;
                }
                else
                {
                    gvITWNesting.DataSource = string.Empty;
                    gvITWNesting.DataBind();
                    divITW.Visible = false;
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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            //ddlNestingStatus.SelectedIndex = 0;
            //ddlDates.SelectedIndex = 0;
            //ddlDates_SelectedIndexChanged();
            SetDates();
            ddlProductCode.SelectedIndex = 0;
            gvAerowerksNesting.DataSource = string.Empty;
            gvAerowerksNesting.DataBind();
            gvITWNesting.DataSource = string.Empty;
            gvITWNesting.DataBind();
            divAero.Visible = false;
            divITW.Visible = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlDates_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ddlDates_SelectedIndexChanged();
    }

    //protected void ddlDates_SelectedIndexChanged()
    //{
    //    try
    //    {
    //        if (ddlDates.SelectedValue == "1")
    //        {
    //            lblStartDate.InnerText = "Nesting Start Date From";
    //            lblEndDate.InnerText = "Nesting Start Date To";
    //        }
    //        else if (ddlDates.SelectedValue == "2")
    //        {
    //            lblStartDate.InnerText = "Nesting End Date From";
    //            lblEndDate.InnerText = "Nesting End Date To";
    //        }
    //        else if (ddlDates.SelectedValue == "3")
    //        {
    //            lblStartDate.InnerText = "Nesting Sent Date From";
    //            lblEndDate.InnerText = "Nesting Sent Date To";
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Utility.AddEditException(ex);
    //    }
    //}   
}