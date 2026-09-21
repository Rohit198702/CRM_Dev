using BLLAERO;
using BOLAERO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesManagement_FrmBDMActivity : System.Web.UI.Page
{
    BOLBDMActivity ObjBOL = new BOLBDMActivity();
    BLLBDMActivity ObjBLL = new BLLBDMActivity();

    string formName = "FrmBDMActivity_V1.aspx";
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
            ObjBOL.Operation = 1;
            ds = ObjBLL.Return_DataSet(ObjBOL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlActivityType, ds.Tables[0]);
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlBDM, ds.Tables[1]);
                Utility.BindDropDownList(ddlBDM_Spec, ds.Tables[1]);
                Utility.BindDropDownListAll(ddlBDMHeaderList, ds.Tables[1]);
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlStatus, ds.Tables[2]);
                ddlStatus.SelectedValue = "1";
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlLocationType, ds.Tables[3]);
            }

            if (ds.Tables[4].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlVisitMode, ds.Tables[4]);
            }

            if (ds.Tables[5].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlParticipantType, ds.Tables[5]);
                ViewState["ParticipantType"] = ds.Tables[5];
            }

            if (ds.Tables[6].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlParticipantStatus, ds.Tables[6]);
            }

            if (ds.Tables[7].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlCountry_Spec, ds.Tables[7]);
            }

            if (ds.Tables[8].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlProjectStage_Spec, ds.Tables[8]);
            }

            if (ds.Tables[9].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlStatus_Spec, ds.Tables[9]);
            }

            if (ds.Tables[10].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlRiskLevel_Spec, ds.Tables[10]);
                Utility.BindDropDownList(ddlPriority_Spec, ds.Tables[10]);
                Utility.BindDropDownList(ddlPriority_Followup, ds.Tables[10]);
            }

            if (ds.Tables[11].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlFollowupType_Followup, ds.Tables[11]);
            }

            if (ds.Tables[12].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlAssignedTo_Followup, ds.Tables[12]);
            }

            if (ds.Tables[13].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlStatus_Followup, ds.Tables[13]);
            }

            if (ds.Tables[14].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlActivityHeaderList, ds.Tables[14]);
            }

            if (ds.Tables[15].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlLocation_Participant, ds.Tables[15]);
            }

            if (ds.Tables[16].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlMaterialUsed_Participant, ds.Tables[16]);
            }

            if (ds.Tables[17].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlIndustry_Spec, ds.Tables[17]);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void BindControlActivity()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 3;
            ds = ObjBLL.Return_DataSet(ObjBOL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlActivityHeaderList, ds.Tables[0]);
            }
            else
            {
                ddlActivityHeaderList.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlBDMHeaderList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            resetExceptHeaders();

            if (ddlBDMHeaderList.SelectedIndex > 0)
            {
                ObjBOL.Operation = 9;
                ObjBOL.Id = Int32.Parse(ddlBDMHeaderList.SelectedValue);
                DataSet ds = ObjBLL.Return_DataSet(ObjBOL);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    ddlActivityHeaderList.Items.Clear();
                    return;
                }

                Utility.BindDropDownList(ddlActivityHeaderList, ds.Tables[0]);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlActivityHeaderList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlActivityHeaderList_SelectedIndexChanged();
    }

    private void ddlActivityHeaderList_SelectedIndexChanged()
    {
        try
        {
            resetExceptHeaders();

            if (ddlActivityHeaderList.SelectedIndex > 0)
            {
                ObjBOL.Operation = 4;
                ObjBOL.Id = Int32.Parse(ddlActivityHeaderList.SelectedValue);
                DataSet ds = ObjBLL.Return_DataSet(ObjBOL);
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return;
                }

                DataRow dr = ds.Tables[0].Rows[0];
                Dictionary<string, Action<DataRow>> assignments = new Dictionary<string, Action<DataRow>>
                {
                    { "ActivityNo", d => txtActivityNo.Text = Convert.ToString(d["ActivityNo"]) },
                    { "OwnerUserID", d =>
                        {
                            if (ddlBDM.Items.FindByValue(Convert.ToString(d["OwnerUserID"])) != null)
                            {
                                ddlBDM.SelectedValue = Convert.ToString(d["OwnerUserID"]);
                            }
                            else if(ddlBDM.Items.Count > 0)
                            {
                                ddlBDM.SelectedIndex = 0;
                            }
                        }
                    },
                    { "ActivityTypeID", d =>
                        {
                            if (ddlActivityType.Items.FindByValue(Convert.ToString(d["ActivityTypeID"])) != null)
                            {
                                ddlActivityType.SelectedValue = Convert.ToString(d["ActivityTypeID"]);
                                ddlActivityType_SelectedIndexChanged();
                            }
                            else if(ddlActivityType.Items.Count > 0)
                            {
                                ddlActivityType.SelectedIndex = 0;
                            }
                        }
                    },
                    { "ActivityDate", d => txtActivityDate.Text = Convert.ToString(d["ActivityDate"]) },
                    { "Subject", d => txtSubject.Text = Convert.ToString(d["Subject"]) },
                    { "Discussion", d => txtDiscussion.Text = Convert.ToString(d["Discussion"]) },
                    { "BusinessOutcome", d => txtBusinessOutcome.Text = Convert.ToString(d["BusinessOutcome"]) },
                    { "TeamsPostConfirmed", d => chkTeamPosted.Checked = Convert.ToBoolean(d["TeamsPostConfirmed"]) },
                    { "TeamsPostDate", d => txtTeamPostedDate.Text = Convert.ToString(d["TeamsPostDate"]) },
                    { "LocationTypeID", d =>
                        {
                            if (ddlLocationType.Items.FindByValue(Convert.ToString(d["LocationTypeID"])) != null)
                            {
                                ddlLocationType.SelectedValue = Convert.ToString(d["LocationTypeID"]);
                            }
                            else if(ddlLocationType.Items.Count > 0)
                            {
                                ddlLocationType.SelectedIndex = 0;
                            }
                        }
                    },
                    { "VisitModeID", d =>
                        {
                            if (ddlVisitMode.Items.FindByValue(Convert.ToString(d["VisitModeID"])) != null)
                            {
                                ddlVisitMode.SelectedValue = Convert.ToString(d["VisitModeID"]);
                            }
                            else if(ddlVisitMode.Items.Count > 0)
                            {
                                ddlVisitMode.SelectedIndex = 0;
                            }
                        }
                    },
                    { "ActivityStatusID", d =>
                        {
                            if (ddlStatus.Items.FindByValue(Convert.ToString(d["ActivityStatusID"])) != null)
                            {
                                ddlStatus.SelectedValue = Convert.ToString(d["ActivityStatusID"]);
                            }
                            else if(ddlStatus.Items.Count > 0)
                            {
                                ddlStatus.SelectedIndex = 0;
                            }
                        }
                    },
                    { "Timestamp", d => hfTimestamp_Activity.Value = Convert.ToString(d["Timestamp"]) },

                };

                foreach (var assignment in assignments)
                {
                    try
                    {
                        assignment.Value(dr);
                    }
                    catch (Exception ex)
                    {
                        Utility.AddEditException(ex, assignment.Key);
                    }
                }

                btnSaveActivity.Text = "Update Activity";

                GetParticipantGrid();
                GetSpecGrid();
                GetFollowupGrid();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void GetParticipantGrid()
    {
        try
        {
            ObjBOL.Operation = 14;
            ObjBOL.Id = Int32.Parse(ddlActivityHeaderList.SelectedValue);
            DataSet ds = ObjBLL.Return_DataSet(ObjBOL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvActivityParticipant.DataSource = ds.Tables[0];
                gvActivityParticipant.DataBind();
            }
            else
            {
                gvActivityParticipant.DataSource = string.Empty;
                gvActivityParticipant.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnCancelActivity_Click(object sender, EventArgs e)
    {
        btnCancelActivity_Click();
    }

    private void btnCancelActivity_Click()
    {
        try
        {
            if (ddlBDMHeaderList != null && ddlBDMHeaderList.Items.Count > 0)
            {
                ddlBDMHeaderList.SelectedIndex = 0;
            }

            BindControlActivity();

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
            txtActivityNo.Text = string.Empty;

            if (ddlBDM != null && ddlBDM.Items.Count > 0)
            {
                ddlBDM.SelectedIndex = 0;
            }

            if (ddlActivityType != null && ddlActivityType.Items.Count > 0)
            {
                ddlActivityType.SelectedIndex = 0;
                ddlActivityType_SelectedIndexChanged();
            }

            txtActivityDate.Text = string.Empty;
            txtSubject.Text = string.Empty;
            txtDiscussion.Text = string.Empty;
            txtBusinessOutcome.Text = string.Empty;
            chkTeamPosted.Checked = false;
            txtTeamPostedDate.Text = string.Empty;

            if (ddlLocationType != null && ddlLocationType.Items.Count > 0)
            {
                ddlLocationType.SelectedIndex = 0;
            }

            if (ddlVisitMode != null && ddlVisitMode.Items.Count > 0)
            {
                ddlVisitMode.SelectedIndex = 0;
            }

            if (ddlStatus != null && ddlStatus.Items.Count > 0)
            {
                ddlStatus.SelectedValue = "1";
            }

            hfTimestamp_Activity.Value = "-1";

            btnSaveActivity.Text = "Save Activity";

            gvActivityParticipant.DataSource = string.Empty;
            gvActivityParticipant.DataBind();

            gvSpecDetails.DataSource = string.Empty;
            gvSpecDetails.DataBind();

            gvFollowups.DataSource = string.Empty;
            gvFollowups.DataBind();

            btnCancelParticipant_Click();
            btnCancelSpec_Click();
            btnCancelFollowup_Click();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private bool validationCheck_Activity()
    {
        try
        {
            if (txtActivityNo.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please create Activity No !!");
                btnNewActivity.Focus();
                return false;
            }

            if (ddlBDM.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select BDM !!");
                ddlBDM.Focus();
                return false;
            }

            if (ddlActivityType.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Activity Type !!");
                ddlActivityType.Focus();
                return false;
            }

            if (txtSubject.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter Subject !!");
                txtSubject.Focus();
                return false;
            }

            if (ddlStatus.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Status !!");
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

    protected void btnSaveActivity_Click(object sender, EventArgs e)
    {
        try
        {
            if (!validationCheck_Activity())
            {
                return;
            }

            ObjBOL.Operation = 5;
            string message = "Activity Inserted Successfully !!";
            string operation = "Save_Activity";

            if (Utility.IsAuthorized())
            {
                ObjBOL.LoginUserId = Utility.GetCurrentUser();
            }

            ObjBOL.ActivityNo = txtActivityNo.Text;

            if (ddlActivityHeaderList.SelectedIndex > 0)
            {
                ObjBOL.Id = Int32.Parse(ddlActivityHeaderList.SelectedValue);
                ObjBOL.Timestamp = long.Parse(hfTimestamp_Activity.Value);
                ObjBOL.Operation = 6;
                message = "Activity updated Successfully !!";
                operation = "Update_Activity";
            }

            if (ddlBDM.SelectedIndex > 0)
            {
                ObjBOL.BDMId = Int32.Parse(ddlBDM.SelectedValue);
            }

            if (ddlActivityType.SelectedIndex > 0)
            {
                ObjBOL.ActivityTypeId = Int32.Parse(ddlActivityType.SelectedValue);
            }

            if (txtActivityDate.Text != "")
            {
                ObjBOL.ActivityDate = Utility.ConvertDateFormat(txtActivityDate.Text);
            }

            ObjBOL.Subject = txtSubject.Text;
            ObjBOL.Discussion = txtDiscussion.Text;
            ObjBOL.BusinessOutcome = txtBusinessOutcome.Text;

            if (chkTeamPosted.Checked)
            {
                ObjBOL.TeamPosted = true;
            }

            if (txtTeamPostedDate.Text != "")
            {
                ObjBOL.TeamPostedDate = Utility.ConvertDateFormat(txtTeamPostedDate.Text);
            }

            if (ddlLocationType.SelectedIndex > 0)
            {
                ObjBOL.LocationTypeId = Int32.Parse(ddlLocationType.SelectedValue);
            }

            if (ddlLocationType.SelectedIndex > 0)
            {
                ObjBOL.LocationTypeId = Int32.Parse(ddlLocationType.SelectedValue);
            }

            if (ddlVisitMode.SelectedIndex > 0)
            {
                ObjBOL.VisitModeId = Int32.Parse(ddlVisitMode.SelectedValue);
            }

            if (ddlStatus.SelectedIndex > 0)
            {
                ObjBOL.StatusId = Int32.Parse(ddlStatus.SelectedValue);
            }

            string returnStatus = ObjBLL.Return_String(ObjBOL);
            if (returnStatus.Trim() == "ER01")
            {
                Utility.ShowMessage_Error(Page, "Activity# already exists !");
                return;
            }

            if (returnStatus.Trim() == "ER02")
            {
                Utility.ShowMessage_Error(Page, Utility.ConcurrencyErrorMessage());
                return;
            }

            if (returnStatus.StartsWith("S01-") || returnStatus.StartsWith("S02-"))
            {
                string value = returnStatus.Trim().Replace("S01-", "").Replace("S02-", "");

                Utility.MaintainLogsSpecial(formName, operation, value);
                if (returnStatus.StartsWith("S02-"))
                {
                    Utility.ShowMessage_Error(Page, "Participants are deleted for changed activity type");
                }

                Utility.ShowMessage_Success(Page, message);
                BindControlActivity();

                if (ddlActivityHeaderList.Items.FindByValue(value) != null)
                {
                    ddlActivityHeaderList.SelectedValue = value;
                    ddlActivityHeaderList_SelectedIndexChanged();
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnNewActivity_Click(object sender, EventArgs e)
    {
        try
        {
            btnCancelActivity_Click();
            ObjBOL.Operation = 2;
            string returnStatus = ObjBLL.Return_String(ObjBOL);
            if (returnStatus.Trim() != "")
            {
                txtActivityNo.Text = returnStatus;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private bool validationCheck_Participant()
    {
        try
        {
            if (ddlActivityHeaderList.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Activity# !!");
                ddlActivityHeaderList.Focus();
                return false;
            }

            if (ddlParticipantType.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Participant Type !!");
                ddlParticipantType.Focus();
                return false;
            }

            if (ddlCompany.GetSelectedIndices().Length == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Company !!");
                ddlCompany.Focus();
                return false;
            }

            //if (ddlParticipantStatus.SelectedIndex == 0)
            //{
            //    Utility.ShowMessage_Error(Page, "Please Select Status !!");
            //    ddlStatus.Focus();
            //    return false;
            //}
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }

        return true;
    }

    protected void btnSaveParticipant_Click(object sender, EventArgs e)
    {
        try
        {
            if (!validationCheck_Participant())
            {
                return;
            }

            ObjBOL.Operation = 10;
            string message = "Participant Inserted Successfully !!";
            string operation = "Save_Participant";

            if (Utility.IsAuthorized())
            {
                ObjBOL.LoginUserId = Utility.GetCurrentUser();
            }

            if (hfParticipantId.Value.Trim() != "-1")
            {
                ObjBOL.Id = Int32.Parse(hfParticipantId.Value.Trim());
                ObjBOL.Timestamp = long.Parse(hfTimestamp_Participant.Value);
                ObjBOL.Operation = 12;
                message = "Participant updated Successfully !!";
                operation = "Update_Participant";
            }

            if (ddlActivityHeaderList.SelectedIndex > 0)
            {
                ObjBOL.ActivityId = Int32.Parse(ddlActivityHeaderList.SelectedValue);
            }

            if (ddlParticipantStatus.SelectedIndex > 0)
            {
                ObjBOL.StatusId = Int32.Parse(ddlParticipantStatus.SelectedValue);
            }

            if (ddlParticipantType.SelectedIndex > 0)
            {
                ObjBOL.ParticipantTypeId = Int32.Parse(ddlParticipantType.SelectedValue);
            }

            if (ddlLocation_Participant.SelectedIndex > 0)
            {
                ObjBOL.LocationTypeId = Int32.Parse(ddlLocation_Participant.SelectedValue);
            }

            if (ddlMaterialUsed_Participant.SelectedIndex > 0)
            {
                ObjBOL.MaterialUsed = Int32.Parse(ddlMaterialUsed_Participant.SelectedValue);
            }

            ObjBOL.Details = txtDetails_Participant.Text;

            StringBuilder sb = new StringBuilder();

            foreach (ListItem item in ddlCompany.Items)
            {
                if (item.Selected)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(";");
                    }

                    sb.Append(item.Value);
                }
            }

            string ids = sb.ToString();
            ObjBOL.CompanyIds = ids;

            sb = new StringBuilder();
            ids = "";

            foreach (ListItem item in ddlContact.Items)
            {
                if (item.Selected)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(";");
                    }

                    sb.Append(item.Value);
                }
            }

            ids = sb.ToString();
            ObjBOL.ContactIds = ids;

            if (chkIsPrimary.Checked)
            {
                ObjBOL.IsPrimary = true;
            }

            ObjBOL.Remarks = txtRemarks.Text;

            string returnStatus = ObjBLL.Return_String(ObjBOL);
            if (returnStatus.Trim() == "ER01")
            {
                Utility.ShowMessage_Error(Page, "company or contact already exists. please check the grid below !");
                return;
            }

            if (returnStatus.Trim() == "ER02")
            {
                Utility.ShowMessage_Error(Page, Utility.ConcurrencyErrorMessage());
                return;
            }

            if (returnStatus.Trim().Contains("S01-"))
            {
                Utility.MaintainLogsSpecial(formName, operation, returnStatus.Trim().Replace("S01-", ""));
                Utility.ShowMessage_Success(Page, message);

                GetParticipantGrid();
                btnCancelParticipant_Click();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnCancelParticipant_Click(object sender, EventArgs e)
    {
        btnCancelParticipant_Click();
    }

    private void btnCancelParticipant_Click()
    {
        try
        {
            if (ddlParticipantType.Items.Count > 0)
            {
                ddlParticipantType.SelectedIndex = 0;
            }
            ddlCompany.Items.Clear();
            ddlContact.Items.Clear();

            chkIsPrimary.Checked = false;
            txtRemarks.Text = string.Empty;
            txtDetails_Participant.Text = string.Empty;
            if (ddlParticipantStatus.Items.Count > 0)
            {
                ddlParticipantStatus.SelectedIndex = 0;
            }

            if (ddlLocation_Participant.Items.Count > 0)
            {
                ddlLocation_Participant.SelectedIndex = 0;
            }

            if (ddlMaterialUsed_Participant.Items.Count > 0)
            {
                ddlMaterialUsed_Participant.SelectedIndex = 0;
            }

            hfTimestamp_Participant.Value = "-1";
            hfParticipantId.Value = "-1";
            btnSaveParticipant.Text = "Add Participant";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlParticipantType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlParticipantType_SelectedIndexChanged();
    }

    private void ddlParticipantType_SelectedIndexChanged()
    {
        try
        {
            if (ddlParticipantType.SelectedIndex > 0)
            {
                DataSet ds = new DataSet();
                ObjBOL.Operation = 7;
                if (ddlParticipantType.SelectedIndex > 0)
                {
                    ObjBOL.Id = Int32.Parse(ddlParticipantType.SelectedValue);
                }

                ds = ObjBLL.Return_DataSet(ObjBOL);

                Utility.BindListBoxList(ddlCompany, ds.Tables[0]);
            }
            else
            {
                ddlCompany.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCompany_SelectedIndexChanged();
    }

    private void ddlCompany_SelectedIndexChanged()
    {
        try
        {
            StringBuilder sb = new StringBuilder();

            foreach (ListItem item in ddlCompany.Items)
            {
                if (item.Selected)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(";");
                    }

                    sb.Append(item.Value);
                }
            }

            string ids = sb.ToString();
            if (ids.Trim() == "")
            {
                ddlContact.Items.Clear();
                return;
            }

            DataSet ds = new DataSet();
            ObjBOL.Operation = 8;
            if (ddlParticipantType.SelectedIndex > 0)
            {
                ObjBOL.Id = Int32.Parse(ddlParticipantType.SelectedValue);
            }

            ObjBOL.CompanyIds = ids;
            ds = ObjBLL.Return_DataSet(ObjBOL);
            Utility.BindListBoxList(ddlContact, ds.Tables[0]);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlContact_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gvActivityParticipant_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(gvActivityParticipant.DataKeys[e.RowIndex].Values[0]);
            string TimeStamp = Convert.ToString(gvActivityParticipant.DataKeys[e.RowIndex].Values[1]);
            ObjBOL.Operation = 13;
            ObjBOL.Id = ID;
            if (TimeStamp != "")
            {
                ObjBOL.Timestamp = long.Parse(TimeStamp);
            }
            string returnStatus = ObjBLL.Return_String(ObjBOL);
            if (returnStatus.Trim() == "ER01")
            {
                Utility.ShowMessage_Error(Page, Utility.ConcurrencyErrorMessage());
                return;
            }

            if (returnStatus != "")
            {
                Utility.MaintainLogsSpecial(formName, "Delete-Participant", ddlActivityHeaderList.SelectedValue);
                Utility.ShowMessage_Success(Page, "Participant Deleted Successfully !!");
                GetParticipantGrid();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvActivityParticipant_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            int ID = Convert.ToInt32(gvActivityParticipant.DataKeys[e.NewEditIndex].Values[0]);
            hfParticipantId.Value = ID.ToString();
            ObjBOL.Operation = 11;
            ObjBOL.Id = ID;
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return;
            }

            DataRow dr = ds.Tables[0].Rows[0];

            Dictionary<string, Action<DataRow>> assignments = new Dictionary<string, Action<DataRow>>
            {
                { "Details", d => txtDetails_Participant.Text = Convert.ToString(d["Details"]) },
                { "Remarks", d => txtRemarks.Text = Convert.ToString(d["Remarks"]) },
                { "Timestamp", d => hfTimestamp_Participant.Value = Convert.ToString(d["Timestamp"]) },
                { "IsPrimary", d => chkIsPrimary.Checked = Convert.ToBoolean(d["IsPrimary"]) },
                { "ParticipantType", d =>
                    {
                        if (ddlParticipantType.Items.FindByValue(Convert.ToString(d["ParticipantType"])) != null)
                        {
                            ddlParticipantType.SelectedValue = Convert.ToString(d["ParticipantType"]);
                            ddlParticipantType_SelectedIndexChanged();
                        }
                        else if(ddlParticipantType.Items.Count > 0)
                        {
                            ddlParticipantType.SelectedIndex = 0;
                        }
                    }
                },
                { "CompanyId", d =>
                    {
                        foreach (ListItem item in ddlCompany.Items)
                        {
                            item.Selected = false;
                        }

                        string companyIds = Convert.ToString(d["CompanyId"]);

                        if (!string.IsNullOrEmpty(companyIds) && companyIds != "0")
                        {
                            foreach (string id in companyIds.Split(';'))
                            {
                                ListItem item = ddlCompany.Items.FindByValue(id.Trim());

                                if (item != null)
                                {
                                    item.Selected = true;
                                }
                            }

                            ddlCompany_SelectedIndexChanged();
                        }
                    }
                },
                { "ContactId", d =>
                    {
                        foreach (ListItem item in ddlContact.Items)
                        {
                            item.Selected = false;
                        }

                        string contactIds = Convert.ToString(d["ContactId"]);

                        if (!string.IsNullOrEmpty(contactIds) && contactIds != "0")
                        {
                            foreach (string id in contactIds.Split(';'))
                            {
                                ListItem item = ddlContact.Items.FindByValue(id.Trim());

                                if (item != null)
                                {
                                    item.Selected = true;
                                }
                            }
                        }
                    }
                },
                { "AttendanceStatusID", d =>
                    {
                        if (ddlParticipantStatus.Items.FindByValue(Convert.ToString(d["AttendanceStatusID"])) != null)
                        {
                            ddlParticipantStatus.SelectedValue = Convert.ToString(d["AttendanceStatusID"]);
                        }
                        else if(ddlParticipantStatus.Items.Count > 0)
                        {
                            ddlParticipantStatus.SelectedIndex = 0;
                        }
                    }
                },
                { "Location", d =>
                    {
                        if (ddlLocation_Participant.Items.FindByValue(Convert.ToString(d["Location"])) != null)
                        {
                            ddlLocation_Participant.SelectedValue = Convert.ToString(d["Location"]);
                        }
                        else if(ddlLocation_Participant.Items.Count > 0)
                        {
                            ddlLocation_Participant.SelectedIndex = 0;
                        }
                    }
                },
                { "MaterialUsed", d =>
                    {
                        if (ddlMaterialUsed_Participant.Items.FindByValue(Convert.ToString(d["MaterialUsed"])) != null)
                        {
                            ddlMaterialUsed_Participant.SelectedValue = Convert.ToString(d["MaterialUsed"]);
                        }
                        else if(ddlMaterialUsed_Participant.Items.Count > 0)
                        {
                            ddlMaterialUsed_Participant.SelectedIndex = 0;
                        }
                    }
                },
            };

            foreach (var assignment in assignments)
            {
                try
                {
                    assignment.Value(dr);
                }
                catch (Exception ex)
                {
                    Utility.AddEditException(ex, assignment.Key);
                }
            }

            btnSaveParticipant.Text = "Update Participant";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void GetFollowupGrid()
    {
        try
        {
            ObjBOL.Operation = 25;
            ObjBOL.Id = Int32.Parse(ddlActivityHeaderList.SelectedValue);
            DataSet ds = ObjBLL.Return_DataSet(ObjBOL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvFollowups.DataSource = ds.Tables[0];
                gvFollowups.DataBind();
            }
            else
            {
                gvFollowups.DataSource = string.Empty;
                gvFollowups.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private bool validationCheck_Followup()
    {
        try
        {
            if (ddlActivityHeaderList.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Activity# !!");
                ddlActivityHeaderList.Focus();
                return false;
            }

            if (txtFollowupNo_Followup.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please create Activity No !!");
                txtFollowupNo_Followup.Focus();
                return false;
            }

            if (ddlFollowupType_Followup.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Status !!");
                ddlFollowupType_Followup.Focus();
                return false;
            }

            if (txtFollowupDate_Followup.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter Followup Date !!");
                txtFollowupDate_Followup.Focus();
                return false;
            }

            if (txtSubject_Followup.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter Subject !!");
                txtSubject_Followup.Focus();
                return false;
            }

            if (ddlPriority_Followup.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Priority !!");
                ddlPriority_Followup.Focus();
                return false;
            }

            if (ddlStatus.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Status !!");
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

    protected void btnAddFollowup_Click(object sender, EventArgs e)
    {
        try
        {
            if (!validationCheck_Followup())
            {
                return;
            }

            ObjBOL.Operation = 23;
            string message = "Followup Inserted Successfully !!";
            string operation = "Save_Followup";

            if (Utility.IsAuthorized())
            {
                ObjBOL.LoginUserId = Utility.GetCurrentUser();
            }

            if (hfFollowupId.Value.Trim() != "-1")
            {
                ObjBOL.Id = Int32.Parse(hfFollowupId.Value.Trim());
                ObjBOL.Timestamp = long.Parse(hfTimestamp_Followup.Value);
                ObjBOL.Operation = 24;
                message = "Followup updated Successfully !!";
                operation = "Update_Followup";
            }

            if (ddlActivityHeaderList.SelectedIndex > 0)
            {
                ObjBOL.ActivityId = Int32.Parse(ddlActivityHeaderList.SelectedValue);
            }

            ObjBOL.FollowupNo = txtFollowupNo_Followup.Text;

            if (ddlFollowupType_Followup.SelectedIndex > 0)
            {
                ObjBOL.FollowupTypeId = Int32.Parse(ddlFollowupType_Followup.SelectedValue);
            }

            if (txtFollowupDate_Followup.Text != "")
            {
                ObjBOL.FollowupDate = Utility.ConvertDateFormat(txtFollowupDate_Followup.Text);
            }

            ObjBOL.Subject = txtSubject_Followup.Text;

            if (ddlAssignedTo_Followup.SelectedIndex > 0)
            {
                ObjBOL.AssignedToId = Int32.Parse(ddlAssignedTo_Followup.SelectedValue);
            }

            ObjBOL.Discussion = txtDescription_Followup.Text;

            if (ddlPriority_Followup.SelectedIndex > 0)
            {
                ObjBOL.PriorityId = Int32.Parse(ddlPriority_Followup.SelectedValue);
            }

            if (txtDueDate_Followup.Text != "")
            {
                ObjBOL.DueDate = Utility.ConvertDateFormat(txtDueDate_Followup.Text);
            }

            if (txtCompletedOnDate_Followup.Text != "")
            {
                ObjBOL.CompletedOn = Utility.ConvertDateFormat(txtCompletedOnDate_Followup.Text);
            }

            if (ddlStatus_Followup.SelectedIndex > 0)
            {
                ObjBOL.StatusId = Int32.Parse(ddlStatus_Followup.SelectedValue);
            }

            ObjBOL.Remarks = txtRemarks_Followup.Text;

            if (txtReminderDate_Followup.Text != "")
            {
                ObjBOL.ReminderDate = Utility.ConvertDateFormat(txtReminderDate_Followup.Text);
            }

            if (chkReminderSent.Checked)
            {
                ObjBOL.ReminderSent = true;
            }

            string returnStatus = ObjBLL.Return_String(ObjBOL);
            if (returnStatus.Trim() == "ER01")
            {
                Utility.ShowMessage_Error(Page, "Followup already exists !");
                return;
            }

            if (returnStatus.Trim() == "ER02")
            {
                Utility.ShowMessage_Error(Page, Utility.ConcurrencyErrorMessage());
                return;
            }

            if (returnStatus.Trim().Contains("S01-"))
            {
                Utility.MaintainLogsSpecial(formName, operation, returnStatus.Trim().Replace("S01-", ""));
                Utility.ShowMessage_Success(Page, message);

                GetFollowupGrid();
                btnCancelFollowup_Click();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnCancelFollowup_Click(object sender, EventArgs e)
    {
        btnCancelFollowup_Click();
    }

    private void btnCancelFollowup_Click()
    {
        try
        {
            txtFollowupNo_Followup.Text = string.Empty;

            if (ddlFollowupType_Followup.Items.Count > 0)
            {
                ddlFollowupType_Followup.SelectedIndex = 0;
            }

            txtFollowupDate_Followup.Text = string.Empty;
            txtSubject_Followup.Text = string.Empty;

            if (ddlAssignedTo_Followup.Items.Count > 0)
            {
                ddlAssignedTo_Followup.SelectedIndex = 0;
            }

            txtDescription_Followup.Text = string.Empty;

            if (ddlPriority_Followup.Items.Count > 0)
            {
                ddlPriority_Followup.SelectedIndex = 0;
            }

            txtDueDate_Followup.Text = string.Empty;
            txtCompletedOnDate_Followup.Text = string.Empty;

            if (ddlStatus_Followup.Items.Count > 0)
            {
                ddlStatus_Followup.SelectedIndex = 0;
            }

            txtRemarks_Followup.Text = string.Empty;
            txtReminderDate_Followup.Text = string.Empty;
            chkReminderSent.Checked = false;

            hfFollowupId.Value = "-1";
            hfTimestamp_Followup.Value = "-1";
            btnAddFollowup.Text = "Add Followup";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvFollowups_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(gvFollowups.DataKeys[e.RowIndex].Values[0]);
            string TimeStamp = Convert.ToString(gvFollowups.DataKeys[e.RowIndex].Values[1]);
            ObjBOL.Operation = 27;
            ObjBOL.Id = ID;
            if (TimeStamp != "")
            {
                ObjBOL.Timestamp = long.Parse(TimeStamp);
            }
            string returnStatus = ObjBLL.Return_String(ObjBOL);
            if (returnStatus.Trim() == "ER01")
            {
                Utility.ShowMessage_Error(Page, Utility.ConcurrencyErrorMessage());
                return;
            }

            if (returnStatus != "")
            {
                Utility.MaintainLogsSpecial(formName, "Delete_Followup", ddlActivityHeaderList.SelectedValue);
                Utility.ShowMessage_Success(Page, "Followup Deleted Successfully !!");
                GetFollowupGrid();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvFollowups_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            int ID = Convert.ToInt32(gvFollowups.DataKeys[e.NewEditIndex].Values[0]);
            hfFollowupId.Value = ID.ToString();
            ObjBOL.Operation = 26;
            ObjBOL.Id = ID;
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return;
            }

            DataRow dr = ds.Tables[0].Rows[0];

            Dictionary<string, Action<DataRow>> assignments = new Dictionary<string, Action<DataRow>>
            {
                { "FollowUpNo", d => txtFollowupNo_Followup.Text = Convert.ToString(d["FollowUpNo"]) },
                { "Timestamp", d => hfTimestamp_Followup.Value = Convert.ToString(d["Timestamp"]) },
                { "FollowUpTypeID", d =>
                    {
                        if (ddlFollowupType_Followup.Items.FindByValue(Convert.ToString(d["FollowUpTypeID"])) != null)
                        {
                            ddlFollowupType_Followup.SelectedValue = Convert.ToString(d["FollowUpTypeID"]);
                        }
                        else if(ddlFollowupType_Followup.Items.Count > 0)
                        {
                            ddlFollowupType_Followup.SelectedIndex = 0;
                        }
                    }
                },
                { "FollowupDate", d => txtFollowupDate_Followup.Text = Convert.ToString(d["FollowupDate"]) },
                { "Subject", d => txtSubject_Followup.Text = Convert.ToString(d["Subject"]) },
                { "Description", d => txtDescription_Followup.Text = Convert.ToString(d["Description"]) },
                { "AssignedToUserID", d =>
                    {
                        if (ddlAssignedTo_Followup.Items.FindByValue(Convert.ToString(d["AssignedToUserID"])) != null)
                        {
                            ddlAssignedTo_Followup.SelectedValue = Convert.ToString(d["AssignedToUserID"]);
                        }
                        else if(ddlAssignedTo_Followup.Items.Count > 0)
                        {
                            ddlAssignedTo_Followup.SelectedIndex = 0;
                        }
                    }
                },
                { "PriorityID", d =>
                    {
                        if (ddlPriority_Followup.Items.FindByValue(Convert.ToString(d["PriorityID"])) != null)
                        {
                            ddlPriority_Followup.SelectedValue = Convert.ToString(d["PriorityID"]);
                        }
                        else if(ddlPriority_Followup.Items.Count > 0)
                        {
                            ddlPriority_Followup.SelectedIndex = 0;
                        }
                    }
                },
                { "DueDate", d => txtDueDate_Followup.Text = Convert.ToString(d["DueDate"]) },
                { "CompletedDate", d => txtCompletedOnDate_Followup.Text = Convert.ToString(d["CompletedDate"]) },
                { "StatusID", d =>
                    {
                        if (ddlStatus_Followup.Items.FindByValue(Convert.ToString(d["StatusID"])) != null)
                        {
                            ddlStatus_Followup.SelectedValue = Convert.ToString(d["StatusID"]);
                        }
                        else if(ddlStatus_Followup.Items.Count > 0)
                        {
                            ddlStatus_Followup.SelectedIndex = 0;
                        }
                    }
                },
                { "CompletionRemarks", d => txtRemarks_Followup.Text = Convert.ToString(d["CompletionRemarks"]) },
                { "ReminderDate", d => txtReminderDate_Followup.Text = Convert.ToString(d["ReminderDate"]) },
                { "ReminderSent", d => chkReminderSent.Checked = Convert.ToBoolean(d["ReminderSent"]) },

            };

            foreach (var assignment in assignments)
            {
                try
                {
                    assignment.Value(dr);
                }
                catch (Exception ex)
                {
                    Utility.AddEditException(ex, assignment.Key);
                }
            }

            btnAddFollowup.Text = "Update Followup";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void GetSpecGrid()
    {
        try
        {
            ObjBOL.Operation = 17;
            ObjBOL.Id = Int32.Parse(ddlActivityHeaderList.SelectedValue);
            DataSet ds = ObjBLL.Return_DataSet(ObjBOL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSpecDetails.DataSource = ds.Tables[0];
                gvSpecDetails.DataBind();
            }
            else
            {
                gvSpecDetails.DataSource = string.Empty;
                gvSpecDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private bool validationCheck_Spec()
    {
        try
        {
            if (ddlActivityHeaderList.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Activity# !!");
                ddlActivityHeaderList.Focus();
                return false;
            }

            if (txtSpecNo_Spec.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please create Spec No !!");
                txtSpecNo_Spec.Focus();
                return false;
            }

            if (txtProjectName_Spec.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter Project Name !!");
                txtProjectName_Spec.Focus();
                return false;
            }

            if (ddlProjectStage_Spec.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Project Stage !!");
                ddlProjectStage_Spec.Focus();
                return false;
            }

            if (ddlPriority_Spec.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Priority !!");
                ddlPriority_Spec.Focus();
                return false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }

        return true;
    }

    protected void btnAddSpec_Click(object sender, EventArgs e)
    {
        try
        {
            if (!validationCheck_Spec())
            {
                return;
            }

            ObjBOL.Operation = 18;
            string message = "Spec Inserted Successfully !!";
            string operation = "Save_Spec";

            if (Utility.IsAuthorized())
            {
                ObjBOL.LoginUserId = Utility.GetCurrentUser();
            }

            if (hfSpecId.Value.Trim() != "-1")
            {
                ObjBOL.Id = Int32.Parse(hfSpecId.Value.Trim());
                ObjBOL.Timestamp = long.Parse(hfTimestamp_Spec.Value);
                ObjBOL.Operation = 19;
                message = "Spec updated Successfully !!";
                operation = "Update_Spec";
            }

            if (ddlActivityHeaderList.SelectedIndex > 0)
            {
                ObjBOL.ActivityId = Int32.Parse(ddlActivityHeaderList.SelectedValue);
            }

            ObjBOL.SpecNo = txtSpecNo_Spec.Text;
            ObjBOL.ProjectName = txtProjectName_Spec.Text;
            if (ddlCountry_Spec.SelectedIndex > 0)
            {
                ObjBOL.CountryId = Int32.Parse(ddlCountry_Spec.SelectedValue);
                if (ddlState_Spec.Items.Count > 0 && ddlState_Spec.SelectedIndex > 0)
                {
                    ObjBOL.StateId = Int32.Parse(ddlState_Spec.SelectedValue);
                }
            }

            ObjBOL.City = txtCity_Spec.Text;
            ObjBOL.Address = txtAddress_Spec.Text;

            if (ddlIndustry_Spec.SelectedIndex > 0)
            {
                ObjBOL.IndustryId = Int32.Parse(ddlIndustry_Spec.SelectedValue);
            }

            if (ddlProjectStage_Spec.SelectedIndex > 0)
            {
                ObjBOL.ProjectStageId = Int32.Parse(ddlProjectStage_Spec.SelectedValue);
            }

            if (ddlStatus_Spec.SelectedIndex > 0)
            {
                ObjBOL.StatusId = Int32.Parse(ddlStatus_Spec.SelectedValue);
            }

            if (ddlPriority_Spec.SelectedIndex > 0)
            {
                ObjBOL.PriorityId = Int32.Parse(ddlPriority_Spec.SelectedValue);
            }

            if (txtEstimatedValue_Spec.Text.Trim() != "")
            {
                ObjBOL.EstimatedValue = decimal.Parse(txtEstimatedValue_Spec.Text);
            }

            if (txtProbability_Spec.Text.Trim() != "")
            {
                ObjBOL.Probability = Int32.Parse(txtProbability_Spec.Text);
            }

            if (ddlRiskLevel_Spec.SelectedIndex > 0)
            {
                ObjBOL.RiskLevelId = Int32.Parse(ddlRiskLevel_Spec.SelectedValue);
            }

            ObjBOL.Competitor = txtCompetitor_Spec.Text;
            ObjBOL.BusinessOpportunity = txtBusinessOpportunity_Spec.Text;
            ObjBOL.CurrentIssue = txtCurrentIssue_Spec.Text;

            if (chkApprovedDrawing_Spec.Checked)
            {
                ObjBOL.ApprovedDrawings = true;
            }

            if (chkOrderReceived_Spec.Checked)
            {
                ObjBOL.OrderReceived = true;
            }

            if (txtOrdervalue_Spec.Text.Trim() != "")
            {
                ObjBOL.OrderValue = decimal.Parse(txtOrdervalue_Spec.Text);
            }

            if (txtExpectedClosureDate_Spec.Text != "")
            {
                ObjBOL.ExpectedClosureDate = Utility.ConvertDateFormat(txtExpectedClosureDate_Spec.Text);
            }

            if (txtClosedDate_Spec.Text != "")
            {
                ObjBOL.ClosedDate = Utility.ConvertDateFormat(txtClosedDate_Spec.Text);
            }

            if (ddlBDM_Spec.SelectedIndex > 0)
            {
                ObjBOL.BDMId = Int32.Parse(ddlBDM_Spec.SelectedValue);
            }

            string returnStatus = ObjBLL.Return_String(ObjBOL);
            if (returnStatus.Trim() == "ER01")
            {
                Utility.ShowMessage_Error(Page, "Spec already exists !");
                return;
            }

            if (returnStatus.Trim() == "ER02")
            {
                Utility.ShowMessage_Error(Page, Utility.ConcurrencyErrorMessage());
                return;
            }

            if (returnStatus.Trim().Contains("S01-"))
            {
                Utility.MaintainLogsSpecial(formName, operation, returnStatus.Trim().Replace("S01-", ""));
                Utility.ShowMessage_Success(Page, message);

                GetSpecGrid();
                btnCancelSpec_Click();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnCancelSpec_Click(object sender, EventArgs e)
    {
        btnCancelSpec_Click();
    }

    private void btnCancelSpec_Click()
    {
        try
        {
            txtSpecNo_Spec.Text = string.Empty;
            txtProjectName_Spec.Text = string.Empty;

            if (ddlCountry_Spec.Items.Count > 0)
            {
                ddlCountry_Spec.SelectedIndex = 0;
            }

            ddlState_Spec.Items.Clear();
            txtCity_Spec.Text = string.Empty;
            txtAddress_Spec.Text = string.Empty;

            if (ddlProjectStage_Spec.Items.Count > 0)
            {
                ddlProjectStage_Spec.SelectedIndex = 0;
            }

            if (ddlIndustry_Spec.Items.Count > 0)
            {
                ddlIndustry_Spec.SelectedIndex = 0;
            }

            if (ddlStatus_Spec.Items.Count > 0)
            {
                ddlStatus_Spec.SelectedIndex = 0;
            }

            if (ddlPriority_Spec.Items.Count > 0)
            {
                ddlPriority_Spec.SelectedIndex = 0;
            }

            txtEstimatedValue_Spec.Text = string.Empty;
            txtProbability_Spec.Text = string.Empty;

            if (ddlRiskLevel_Spec.Items.Count > 0)
            {
                ddlRiskLevel_Spec.SelectedIndex = 0;
            }

            txtCompetitor_Spec.Text = string.Empty;
            txtBusinessOpportunity_Spec.Text = string.Empty;
            txtCurrentIssue_Spec.Text = string.Empty;
            chkApprovedDrawing_Spec.Checked = false;
            chkOrderReceived_Spec.Checked = false;
            txtOrdervalue_Spec.Text = string.Empty;
            txtExpectedClosureDate_Spec.Text = string.Empty;
            txtClosedDate_Spec.Text = string.Empty;

            if (ddlBDM_Spec.Items.Count > 0)
            {
                ddlBDM_Spec.SelectedIndex = 0;
            }

            hfSpecId.Value = "-1";
            hfTimestamp_Spec.Value = "-1";
            btnAddSpec.Text = "Add Spec";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvSpecDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(gvSpecDetails.DataKeys[e.RowIndex].Values[0]);
            string TimeStamp = Convert.ToString(gvSpecDetails.DataKeys[e.RowIndex].Values[1]);
            ObjBOL.Operation = 21;
            ObjBOL.Id = ID;
            if (TimeStamp != "")
            {
                ObjBOL.Timestamp = long.Parse(TimeStamp);
            }
            string returnStatus = ObjBLL.Return_String(ObjBOL);
            if (returnStatus.Trim() == "ER01")
            {
                Utility.ShowMessage_Error(Page, Utility.ConcurrencyErrorMessage());
                return;
            }

            if (returnStatus != "")
            {
                Utility.MaintainLogsSpecial(formName, "Delete_Spec", ddlActivityHeaderList.SelectedValue);
                Utility.ShowMessage_Success(Page, "Spec Deleted Successfully !!");
                GetSpecGrid();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvSpecDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            int ID = Convert.ToInt32(gvSpecDetails.DataKeys[e.NewEditIndex].Values[0]);
            hfSpecId.Value = ID.ToString();
            ObjBOL.Operation = 20;
            ObjBOL.Id = ID;
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return;
            }

            DataRow dr = ds.Tables[0].Rows[0];

            Dictionary<string, Action<DataRow>> assignments = new Dictionary<string, Action<DataRow>>
            {
                { "SpecNo", d => txtSpecNo_Spec.Text = Convert.ToString(d["SpecNo"]) },
                { "ProjectName", d => txtProjectName_Spec.Text = Convert.ToString(d["ProjectName"]) },
                { "IndustryId", d =>
                    {
                        if (ddlIndustry_Spec.Items.FindByValue(Convert.ToString(d["IndustryId"])) != null)
                        {
                            ddlIndustry_Spec.SelectedValue = Convert.ToString(d["IndustryId"]);
                        }
                        else if(ddlIndustry_Spec.Items.Count > 0)
                        {
                            ddlIndustry_Spec.SelectedIndex = 0;
                        }
                    }
                },
                { "Timestamp", d => hfTimestamp_Spec.Value = Convert.ToString(d["Timestamp"]) },
                { "CountryID", d =>
                    {
                        if (ddlCountry_Spec.Items.FindByValue(Convert.ToString(d["CountryID"])) != null)
                        {
                            ddlCountry_Spec.SelectedValue = Convert.ToString(d["CountryID"]);
                            ddlCountry_Spec_SelectedIndexChanged();

                            if(ddlState_Spec.Items.FindByValue(Convert.ToString(d["StateID"])) != null)
                            {
                                 ddlState_Spec.SelectedValue = Convert.ToString(d["StateID"]);
                            }
                            else if(ddlState_Spec.Items.Count > 0)
                            {
                                ddlState_Spec.SelectedIndex = 0;
                            }
                        }
                        else if(ddlCountry_Spec.Items.Count > 0)
                        {
                            ddlCountry_Spec.SelectedIndex = 0;
                        }
                    }
                },
                { "City", d => txtCity_Spec.Text = Convert.ToString(d["City"]) },
                { "Address", d => txtAddress_Spec.Text = Convert.ToString(d["Address"]) },
                { "ProjectStageID", d =>
                    {
                        if (ddlProjectStage_Spec.Items.FindByValue(Convert.ToString(d["ProjectStageID"])) != null)
                        {
                            ddlProjectStage_Spec.SelectedValue = Convert.ToString(d["ProjectStageID"]);
                        }
                        else if(ddlProjectStage_Spec.Items.Count > 0)
                        {
                            ddlProjectStage_Spec.SelectedIndex = 0;
                        }
                    }
                },
                { "SpecStatusID", d =>
                    {
                        if (ddlStatus_Spec.Items.FindByValue(Convert.ToString(d["SpecStatusID"])) != null)
                        {
                            ddlStatus_Spec.SelectedValue = Convert.ToString(d["SpecStatusID"]);
                        }
                        else if(ddlStatus_Spec.Items.Count > 0)
                        {
                            ddlStatus_Spec.SelectedIndex = 0;
                        }
                    }
                },
                { "PriorityID", d =>
                    {
                        if (ddlPriority_Spec.Items.FindByValue(Convert.ToString(d["PriorityID"])) != null)
                        {
                            ddlPriority_Spec.SelectedValue = Convert.ToString(d["PriorityID"]);
                        }
                        else if(ddlPriority_Spec.Items.Count > 0)
                        {
                            ddlPriority_Spec.SelectedIndex = 0;
                        }
                    }
                },
                { "EstimatedValue", d => txtEstimatedValue_Spec.Text = Convert.ToString(d["EstimatedValue"]) },
                { "ProbabilityPercent", d => txtProbability_Spec.Text = Convert.ToString(d["ProbabilityPercent"]) },
                { "Competitor", d => txtCompetitor_Spec.Text = Convert.ToString(d["Competitor"]) },
                { "BusinessOpportunity", d => txtBusinessOpportunity_Spec.Text = Convert.ToString(d["BusinessOpportunity"]) },
                { "RiskLevelID", d =>
                    {
                        if (ddlRiskLevel_Spec.Items.FindByValue(Convert.ToString(d["RiskLevelID"])) != null)
                        {
                            ddlRiskLevel_Spec.SelectedValue = Convert.ToString(d["RiskLevelID"]);
                        }
                        else if(ddlRiskLevel_Spec.Items.Count > 0)
                        {
                            ddlRiskLevel_Spec.SelectedIndex = 0;
                        }
                    }
                },
                { "CurrentIssue", d => txtCurrentIssue_Spec.Text = Convert.ToString(d["CurrentIssue"]) },
                { "ApprovedDrawings", d => chkApprovedDrawing_Spec.Checked = Convert.ToBoolean(d["ApprovedDrawings"]) },
                { "OrderReceived", d => chkOrderReceived_Spec.Checked = Convert.ToBoolean(d["OrderReceived"]) },
                { "OrderValue", d => txtOrdervalue_Spec.Text = Convert.ToString(d["OrderValue"]) },
                { "ExpectedClosureDate", d => txtExpectedClosureDate_Spec.Text = Convert.ToString(d["ExpectedClosureDate"]) },
                { "ClosedDate", d => txtClosedDate_Spec.Text = Convert.ToString(d["ClosedDate"]) },
                { "OwnerUserID", d =>
                    {
                        if (ddlBDM_Spec.Items.FindByValue(Convert.ToString(d["OwnerUserID"])) != null)
                        {
                            ddlBDM_Spec.SelectedValue = Convert.ToString(d["OwnerUserID"]);
                        }
                        else if(ddlBDM_Spec.Items.Count > 0)
                        {
                            ddlBDM_Spec.SelectedIndex = 0;
                        }
                    }
                },
            };

            foreach (var assignment in assignments)
            {
                try
                {
                    assignment.Value(dr);
                }
                catch (Exception ex)
                {
                    Utility.AddEditException(ex, assignment.Key);
                }
            }

            btnAddSpec.Text = "Update Spec";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlActivityType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlActivityHeaderList.Items.Count > 0 && ddlActivityHeaderList.SelectedIndex > 0 && gvActivityParticipant.Rows.Count > 0)
        {
            Utility.ShowMessage_Error(Page, "Changing Type will delete the participants");
        }
        ddlActivityType_SelectedIndexChanged();
    }

    private void ddlActivityType_SelectedIndexChanged()
    {
        try
        {
            ddlParticipantType.Items.Clear();
            DataTable dt = ((DataTable)ViewState["ParticipantType"]).Copy();

            if (ddlActivityType.SelectedValue == "1")
            {
                lblParticipant.InnerText = "Consultant";

                dt.AsEnumerable().Where(r => r.Field<int>("id") != 1).ToList().ForEach(r => dt.Rows.Remove(r));
                dt.AcceptChanges();

                Utility.BindDropDownList(ddlParticipantType, dt);

                divDetails_Participant.Visible = true;
                divLocation_Participant.Visible = false;
                divMaterialUsed_Participant.Visible = false;
            }
            else if (ddlActivityType.SelectedValue == "2")
            {
                lblParticipant.InnerText = "Dealer";

                dt.AsEnumerable().Where(r => r.Field<int>("id") != 2).ToList().ForEach(r => dt.Rows.Remove(r));
                dt.AcceptChanges();

                Utility.BindDropDownList(ddlParticipantType, dt);

                divDetails_Participant.Visible = true;
                divLocation_Participant.Visible = false;
                divMaterialUsed_Participant.Visible = false;
            }
            else if (ddlActivityType.SelectedValue == "3")
            {
                lblParticipant.InnerText = "CE";

                Utility.BindDropDownList(ddlParticipantType, dt);

                divDetails_Participant.Visible = false;
                divLocation_Participant.Visible = true;
                divMaterialUsed_Participant.Visible = true;
            }
            else
            {
                lblParticipant.InnerText = "Participants";

                divDetails_Participant.Visible = true;
                divLocation_Participant.Visible = false;
                divMaterialUsed_Participant.Visible = false;
            }
            //ddlParticipantType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlFollowupType_Followup_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnNewSpec_Click(object sender, EventArgs e)
    {
        try
        {
            btnCancelSpec_Click();
            ObjBOL.Operation = 15;
            string returnStatus = ObjBLL.Return_String(ObjBOL);
            if (returnStatus.Trim() != "")
            {
                txtSpecNo_Spec.Text = returnStatus;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnNewFollowup_Click(object sender, EventArgs e)
    {
        try
        {
            btnCancelFollowup_Click();
            ObjBOL.Operation = 22;
            string returnStatus = ObjBLL.Return_String(ObjBOL);
            if (returnStatus.Trim() != "")
            {
                txtFollowupNo_Followup.Text = returnStatus;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlCountry_Spec_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCountry_Spec_SelectedIndexChanged();
    }

    private void ddlCountry_Spec_SelectedIndexChanged()
    {
        try
        {
            ddlState_Spec.Items.Clear();
            if (ddlCountry_Spec.SelectedIndex > 0)
            {
                ObjBOL.Operation = 16;
                ObjBOL.Id = Int32.Parse(ddlCountry_Spec.SelectedValue);
                DataSet ds = new DataSet();
                ds = ObjBLL.Return_DataSet(ObjBOL);
                Utility.BindDropDownList(ddlState_Spec, ds.Tables[0]);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}