using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesManagement_FrmActivityOperationalDashboard : System.Web.UI.Page
{
    commonclass1 cls = new commonclass1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //BindData();
            ddlFilter_SelectedIndexChanged();
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
            cls.Return_DS(ds, "EXEC Get_ActivityOperationalDashboard 1, " + Utility.GetCurrentUser() + ", '" + ddlFilter.SelectedValue + "' ");

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
                e.Row.ToolTip = "Click to view more details";
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
                e.Row.Attributes["style"] = "cursor:pointer;";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvFollowup, "Select$" + e.Row.RowIndex);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
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

    protected void btnRedirectSales_Click(object sender, EventArgs e)
    {
        //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "OpenWindow", string.Format("window.open('/Default.aspx','_blank');"), true);
        Response.Redirect("/Default.aspx", false);
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

    protected void ddlFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlFilter_SelectedIndexChanged();
    }

    private void ddlFilter_SelectedIndexChanged()
    {
        try
        {
            if (ddlFilter.SelectedValue == "Y")
            {
                spFilterType_1.InnerText = " Year " + DateTime.Now.Year.ToString();
                spFilterType_2.InnerText = " Year " + DateTime.Now.Year.ToString();
            }
            else if (ddlFilter.SelectedValue == "Q")
            {
                int quarter = ((DateTime.Now.Month - 1) / 3) + 1;
                spFilterType_1.InnerText = " Quarter " + quarter;
                spFilterType_2.InnerText = " Quarter " + quarter;
            }
            else if (ddlFilter.SelectedValue == "W")
            {                
                spFilterType_1.InnerText = " Weekly";
                spFilterType_2.InnerText = " Weekly";
            }
            else if (ddlFilter.SelectedValue == "D")
            {
                spFilterType_1.InnerText = " Daily";
                spFilterType_2.InnerText = " Daily";
            }
            BindData();
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