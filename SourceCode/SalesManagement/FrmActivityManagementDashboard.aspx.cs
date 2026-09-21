using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesManagement_FrmActivityManagementDashboard : System.Web.UI.Page
{
    ReportDocument rprt = new ReportDocument();
    commonclass1 cls = new commonclass1();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControls();
            BindData();
        }
    }

    private void BindControls()
    {
        try
        {
            DataSet ds = new DataSet();
            cls.Return_DS(ds, "EXEC Get_ActivityManagementDashboard 1");

            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownListWithoutFiller(ddlMonthHeaderList, ds.Tables[0]);
                //ddlMonthHeaderList.SelectedValue = DateTime.Now.Month.ToString();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                Utility.BindDropDownListWithoutFiller(ddlBDMHeaderList, ds.Tables[1]);
            }          
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void BindData()
    {
        try
        {
            if (!Utility.IsAuthorized())
            {
                return;
            }
            DataSet ds = new DataSet();
            string query = "EXEC Get_ActivityManagementDashboard 2, " + DateTime.Now.Year + ", " + ddlMonthHeaderList.SelectedValue + ", " + ddlBDMHeaderList.SelectedValue;
            cls.Return_DS(ds, query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCESession.DataSource = ds.Tables[0];
                gvCESession.DataBind();
            }
            else
            {
                gvCESession.DataSource = string.Empty;
                gvCESession.DataBind();
            }

            lblCESession.InnerText = gvCESession.Rows.Count.ToString();

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvConsultantEngagement.DataSource = ds.Tables[1];
                gvConsultantEngagement.DataBind();
            }
            else
            {
                gvConsultantEngagement.DataSource = string.Empty;
                gvConsultantEngagement.DataBind();
            }

            lblConsultantEngagement.InnerText = gvConsultantEngagement.Rows.Count.ToString();

            if (ds.Tables[2].Rows.Count > 0)
            {
                gvDealerOnboarding.DataSource = ds.Tables[2];
                gvDealerOnboarding.DataBind();
            }
            else
            {
                gvDealerOnboarding.DataSource = string.Empty;
                gvDealerOnboarding.DataBind();
            }

            lblDealerOnboarding.InnerText = gvDealerOnboarding.Rows.Count.ToString();

            if (ds.Tables[3].Rows.Count > 0)
            {
                gvFollowup.DataSource = ds.Tables[3];
                gvFollowup.DataBind();
            }
            else
            {
                gvFollowup.DataSource = string.Empty;
                gvFollowup.DataBind();
            }

            lblDueFollowup.InnerText = gvFollowup.Rows.Count.ToString();

            if (ds.Tables[4].Rows.Count > 0)
            {
                gvSpecDetails.DataSource = ds.Tables[4];
                gvSpecDetails.DataBind();
            }
            else
            {
                gvSpecDetails.DataSource = string.Empty;
                gvSpecDetails.DataBind();
            }

            lblSpec.InnerText = gvSpecDetails.Rows.Count.ToString();

            if (ds.Tables[5].Rows.Count > 0)
            {
                gvSalesRep.DataSource = ds.Tables[5];
                gvSalesRep.DataBind();
            }
            else
            {
                gvSalesRep.DataSource = string.Empty;
                gvSalesRep.DataBind();
            }

            lblSalesRep.InnerText = gvSalesRep.Rows.Count.ToString();

            if (ds.Tables[6].Rows.Count > 0)
            {
                gvCustomer.DataSource = ds.Tables[6];
                gvCustomer.DataBind();
            }
            else
            {
                gvCustomer.DataSource = string.Empty;
                gvCustomer.DataBind();
            }

            lblCustomer.InnerText = gvCustomer.Rows.Count.ToString();

            if (ds.Tables[7].Rows.Count > 0)
            {
                gvEvent.DataSource = ds.Tables[7];
                gvEvent.DataBind();
            }
            else
            {
                gvEvent.DataSource = string.Empty;
                gvEvent.DataBind();
            }

            lblEvent.InnerText = gvEvent.Rows.Count.ToString();

            if (ds.Tables[8].Rows.Count > 0)
            {
                gvProposals.DataSource = ds.Tables[8];
                gvProposals.DataBind();
            }
            else
            {
                gvProposals.DataSource = string.Empty;
                gvProposals.DataBind();
            }

            lblProposals.InnerText = gvProposals.Rows.Count.ToString();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlBDMHeaderList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void ddlMonthHeaderList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void ddlYearHeaderList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData();
    }

    protected void btnRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Reports/FrmActivityKPIPerformanceSummary.aspx", false);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvCESession_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                string activityId = gvCESession.DataKeys[rowIndex].Value.ToString();

                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "OpenWindow",
                    string.Format("window.open('FrmBDMActivity.aspx?ActivityID={0}','_blank');", activityId),
                    true);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvCESession_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["style"] = "cursor:pointer;";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvCESession, "Select$" + e.Row.RowIndex);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }


    protected void gvConsultantEngagement_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                string activityId = gvConsultantEngagement.DataKeys[rowIndex].Value.ToString();

                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "OpenWindow",
                    string.Format("window.open('FrmBDMActivity.aspx?ActivityID={0}','_blank');", activityId),
                    true);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvConsultantEngagement_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["style"] = "cursor:pointer;";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvConsultantEngagement, "Select$" + e.Row.RowIndex);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvDealerOnboarding_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["style"] = "cursor:pointer;";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvDealerOnboarding, "Select$" + e.Row.RowIndex);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvDealerOnboarding_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                string activityId = gvDealerOnboarding.DataKeys[rowIndex].Value.ToString();

                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "OpenWindow",
                    string.Format("window.open('FrmBDMActivity.aspx?ActivityID={0}','_blank');", activityId),
                    true);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnRedirectActivity_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenWindow", string.Format("window.open('FrmBDMActivity.aspx','_blank');"), true);
    }

    protected void gvFollowup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                string activityId = gvFollowup.DataKeys[rowIndex].Value.ToString();

                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "OpenWindow",
                    string.Format("window.open('FrmBDMActivity.aspx?ActivityID={0}','_blank');", activityId),
                    true);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvFollowup_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Status = DataBinder.Eval(e.Row.DataItem, "Status") as string;
                string DueDate = DataBinder.Eval(e.Row.DataItem, "DueDate") as string;

                DateTime dueDate;
                bool isOverdue = DateTime.TryParseExact(
                    DueDate,
                    "MM/dd/yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out dueDate)
                    && dueDate.Date < DateTime.Today;

                if (!string.IsNullOrEmpty(Status) && Status != "Completed" && Status != "Cancelled" && isOverdue)
                {
                    e.Row.Attributes.Add("style", "background-color: #F5C2C7 !important; color: #7A1C1C !important;");
                }
                else
                {
                    e.Row.Attributes.Add("style", "color: black !important;");
                }

                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.cursor = 'Pointer'";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.ToolTip = "Click to view more details";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvFollowup, "Select$" + e.Row.RowIndex);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnActivePipelineReport_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            cls.Return_DS(ds, "EXEC Get_ActivityManagementDashboard 3, " + ddlBDMHeaderList.SelectedValue);
            rprt.Load(Server.MapPath("~/Reports/rptActivity_ActivePipeline.rpt"));
            if (ds.Tables[0].Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Active Pipeline Report - " + ddlMonthHeaderList.SelectedItem.Text.Replace(",", "-") + " - " + ddlBDMHeaderList.SelectedItem.Text;
                rprt.SetDataSource(ds.Tables[0]);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Active Pipeline Report - " + ddlMonthHeaderList.SelectedItem.Text.Replace(",", "-") + " - " + ddlBDMHeaderList.SelectedItem.Text;
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

    protected void gvSpecDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                string activityId = gvSpecDetails.DataKeys[rowIndex].Value.ToString();

                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "OpenWindow",
                    string.Format("window.open('FrmBDMActivity.aspx?ActivityID={0}','_blank');", activityId),
                    true);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvSpecDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["style"] = "cursor:pointer;";
                e.Row.ToolTip = "Click to view more details";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvSpecDetails, "Select$" + e.Row.RowIndex);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }


    protected void gvSalesRep_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                string activityId = gvSalesRep.DataKeys[rowIndex].Value.ToString();

                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "OpenWindow",
                    string.Format("window.open('FrmBDMActivity.aspx?ActivityID={0}','_blank');", activityId),
                    true);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvSalesRep_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["style"] = "cursor:pointer;";
                e.Row.ToolTip = "Click to view more details";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvSalesRep, "Select$" + e.Row.RowIndex);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["style"] = "cursor:pointer;";
                e.Row.ToolTip = "Click to view more details";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvCustomer, "Select$" + e.Row.RowIndex);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvCustomer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                string activityId = gvCustomer.DataKeys[rowIndex].Value.ToString();

                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "OpenWindow",
                    string.Format("window.open('FrmBDMActivity.aspx?ActivityID={0}','_blank');", activityId),
                    true);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvEvent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                string activityId = gvEvent.DataKeys[rowIndex].Value.ToString();

                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "OpenWindow",
                    string.Format("window.open('FrmBDMActivity.aspx?ActivityID={0}','_blank');", activityId),
                    true);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvEvent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["style"] = "cursor:pointer;";
                e.Row.ToolTip = "Click to view more details";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvEvent, "Select$" + e.Row.RowIndex);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvProposals_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                string pnumber = gvProposals.DataKeys[rowIndex].Value.ToString();
                Session["PNumber"] = pnumber;
                ScriptManager.RegisterClientScriptBlock(
                    this,
                    this.GetType(),
                    "OpenWindow",
                    string.Format("window.open('FrmProposals.aspx?PNumber={0}','_blank');", pnumber),
                    true);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvProposals_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["style"] = "cursor:pointer;";
                e.Row.ToolTip = "Click to view more details";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvProposals, "Select$" + e.Row.RowIndex);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}