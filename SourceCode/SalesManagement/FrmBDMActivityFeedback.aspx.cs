using BLLAERO;
using BOLAERO;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesManagement_FrmBDMActivityFeedback : System.Web.UI.Page
{
    BOLActivityMonthlyReview ObjBOL = new BOLActivityMonthlyReview();
    BLLActivityMonthlyReview ObjBLL = new BLLActivityMonthlyReview();

    ReportDocument rprt = new ReportDocument();
    commonclass1 cls = new commonclass1();

    string formName = "FrmBDMActivityFeedback.aspx";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControls();
            //GetQueryParameters();
        }
    }

    private void GetQueryParameters()
    {
        try
        {
            if (Request.QueryString["Year"] != null)
            {
                var year = Request.QueryString["Year"];
                var Month = Request.QueryString["Month"];
                var Owner = Request.QueryString["Owner"];

                //if (ddlYearHeaderList.Items.FindByValue(year) != null)
                //{
                //    ddlYearHeaderList.SelectedValue = year;
                //}

                //if (ddlMonthHeaderList.Items.FindByValue(Month) != null)
                //{
                //    ddlMonthHeaderList.SelectedValue = Month;
                //}

                if (ddlBDMHeaderList.Items.FindByValue(Owner) != null)
                {
                    ddlBDMHeaderList.SelectedValue = Owner;
                }

                LoadInfo();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void BindControls()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 1;
            ds = ObjBLL.Return_DataSet(ObjBOL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlBDMHeaderList, ds.Tables[0]);
                Utility.BindDropDownList(ddlBDM, ds.Tables[0]);
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlStatus, ds.Tables[1]);
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlQuarterHeaderList, ds.Tables[2]);
                Utility.BindDropDownList(ddlQuarter, ds.Tables[2]);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        btnCancel_Click();
    }

    private void btnCancel_Click()
    {
        try
        {
            if (ddlQuarterHeaderList.Items.Count > 0)
            {
                ddlQuarterHeaderList.SelectedIndex = 0;
            }

            if (ddlBDMHeaderList.Items.Count > 0)
            {
                ddlBDMHeaderList.SelectedIndex = 0;
            }

            if (ddlQuarter.Items.Count > 0)
            {
                ddlQuarter.SelectedIndex = 0;
            }

            if (ddlBDM.Items.Count > 0)
            {
                ddlBDM.SelectedIndex = 0;
            }

            resetExceptHeaders();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void resetExceptHeaders()
    {
        try
        {
            if (ddlStatus.Items.Count > 0)
            {
                ddlStatus.SelectedIndex = 0;
            }

            txtReviewDate.Text = string.Empty;
            txtAccomplishment_1.Text = string.Empty;
            txtAccomplishment_2.Text = string.Empty;
            txtAccomplishment_3.Text = string.Empty;
            txtGap_1.Text = string.Empty;
            txtGap_2.Text = string.Empty;
            txtCorrectiveAction.Text = string.Empty;

            hfTimestamp.Value = "-1";

            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private bool validationCheck()
    {
        try
        {
            if (ddlQuarter.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please select quarter !!");
                ddlQuarter.Focus();
                return false;
            }

            if (ddlBDM.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please select BDM !!");
                ddlBDM.Focus();
                return false;
            }

            if (txtReviewDate.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter review date!!");
                txtReviewDate.Focus();
                return false;
            }

            if (txtAccomplishment_1.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter Accomplishment 1 !!");
                txtAccomplishment_1.Focus();
                return false;
            }

            if (txtAccomplishment_2.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter Accomplishment 2 !!");
                txtAccomplishment_2.Focus();
                return false;
            }

            if (txtAccomplishment_3.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter Accomplishment 3 !!");
                txtAccomplishment_3.Focus();
                return false;
            }

            if (txtGap_1.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter Gap 1 !!");
                txtGap_1.Focus();
                return false;
            }

            if (txtGap_2.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter Gap 2 !!");
                txtGap_2.Focus();
                return false;
            }

            if (ddlStatus.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please select Status !!");
                ddlStatus.Focus();
                return false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }

        return true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!validationCheck())
            {
                return;
            }

            ObjBOL.Operation = 3;

            if (Utility.IsAuthorized())
            {
                ObjBOL.LoginUserId = Utility.GetCurrentUser();
            }

            if (txtReviewDate.Text.Trim() != "")
            {
                ObjBOL.ReviewDate = Utility.ConvertDate(txtReviewDate.Text);
            }

            if (ddlQuarter.SelectedIndex > 0)
            {
                ObjBOL.QuarterId = Int32.Parse(ddlQuarter.SelectedValue);
            }

            if (ddlBDM.SelectedIndex > 0)
            {
                ObjBOL.OwnerUserId = Int32.Parse(ddlBDM.SelectedValue);
            }

            ObjBOL.Accomplishment_1 = txtAccomplishment_1.Text;
            ObjBOL.Accomplishment_2 = txtAccomplishment_2.Text;
            ObjBOL.Accomplishment_3 = txtAccomplishment_3.Text;
            ObjBOL.Gap_1 = txtGap_1.Text;
            ObjBOL.Gap_2 = txtGap_2.Text;
            ObjBOL.CorrectiveAction = txtCorrectiveAction.Text;

            if (ddlStatus.SelectedIndex > 0)
            {
                ObjBOL.StatusId = Int32.Parse(ddlStatus.SelectedValue);
            }

            if (hfTimestamp.Value.Trim() != "-1")
            {
                ObjBOL.Timestamp = long.Parse(hfTimestamp.Value);
            }

            string returnStatus = ObjBLL.Return_String(ObjBOL);

            if (returnStatus.Trim() == "ER02")
            {
                Utility.ShowMessage_Error(Page, Utility.ConcurrencyErrorMessage());
                return;
            }

            string returnValue = returnStatus.Trim();

            if (returnValue.StartsWith("S01-") || returnValue.StartsWith("S02-"))
            {
                bool isUpdate = returnValue.StartsWith("S02-");

                Utility.MaintainLogsSpecial(formName, isUpdate ? "Update" : "Save", returnValue.Substring(4));
                Utility.ShowMessage_Success(Page, isUpdate ? "Review updated successfully !!" : "Review inserted successfully !!");

                LoadInfo();
                btnCancel_Click();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlBDMHeaderList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBDM.SelectedValue = ddlBDMHeaderList.SelectedValue;
        LoadInfo();
    }

    private void LoadInfo()
    {
        try
        {
            resetExceptHeaders();
            if (ddlQuarterHeaderList.SelectedIndex > 0 && ddlBDMHeaderList.SelectedIndex > 0)
            {
                ObjBOL.Operation = 2;
                ObjBOL.QuarterId = int.Parse(ddlQuarterHeaderList.SelectedValue);
                ObjBOL.OwnerUserId = int.Parse(ddlBDMHeaderList.SelectedValue);

                DataSet ds = new DataSet();
                ds = ObjBLL.Return_DataSet(ObjBOL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ddlQuarter.Items.FindByValue(ds.Tables[0].Rows[0]["QuarterId"].ToString()) != null)
                    {
                        ddlQuarter.SelectedValue = ds.Tables[0].Rows[0]["QuarterId"].ToString();
                    }
                    else if (ddlQuarter.Items.Count > 0)
                    {
                        ddlQuarter.SelectedIndex = 0;
                    }

                    if (ddlBDM.Items.FindByValue(ds.Tables[0].Rows[0]["OwnerUserId"].ToString()) != null)
                    {
                        ddlBDM.SelectedValue = ds.Tables[0].Rows[0]["OwnerUserId"].ToString();
                    }
                    else if (ddlBDM.Items.Count > 0)
                    {
                        ddlBDM.SelectedIndex = 0;
                    }

                    txtReviewDate.Text = ds.Tables[0].Rows[0]["ReviewDate"].ToString();
                    txtAccomplishment_1.Text = ds.Tables[0].Rows[0]["Accomplishment_1"].ToString();
                    txtAccomplishment_2.Text = ds.Tables[0].Rows[0]["Accomplishment_2"].ToString();
                    txtAccomplishment_3.Text = ds.Tables[0].Rows[0]["Accomplishment_3"].ToString();
                    txtGap_1.Text = ds.Tables[0].Rows[0]["Gap_1"].ToString();
                    txtGap_2.Text = ds.Tables[0].Rows[0]["Gap_2"].ToString();
                    txtCorrectiveAction.Text = ds.Tables[0].Rows[0]["CorrectiveAction"].ToString();

                    if (ddlStatus.Items.FindByValue(ds.Tables[0].Rows[0]["StatusId"].ToString()) != null)
                    {
                        ddlStatus.SelectedValue = ds.Tables[0].Rows[0]["StatusId"].ToString();
                    }
                    else if (ddlStatus.Items.Count > 0)
                    {
                        ddlStatus.SelectedIndex = 0;
                    }

                    hfTimestamp.Value = ds.Tables[0].Rows[0]["Timestamp"].ToString();

                    btnSave.Text = "Update";
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnPerformanceReview_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtReviewDate.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please select Review Date");
                return;
            }

            DataSet ds = new DataSet();
            cls.Return_DS(ds, "EXEC Get_ActivityMonthlyReview 1, '" + txtReviewDate.Text + "'");
            rprt.Load(Server.MapPath("~/Reports/rptActivityMonthlyReview.rpt"));
            if (ds.Tables[0].Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Performance Review Report - " + txtReviewDate.Text;
                rprt.SetDataSource(ds.Tables[0]);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.WordForWindows, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Performance Review Report - " + txtReviewDate.Text;
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.WordForWindows, Response, false, txtheader.Text);
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

    protected void ddlQuarterHeaderList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlQuarter.SelectedValue = ddlQuarterHeaderList.SelectedValue;
        LoadInfo();
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

    protected void ddlQuarter_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlQuarterHeaderList.SelectedValue = ddlQuarter.SelectedValue;
        LoadInfo();
    }

    protected void ddlBDM_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBDMHeaderList.SelectedValue = ddlBDM.SelectedValue;
        LoadInfo();
    }
}