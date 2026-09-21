using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BOLAERO;
using BLLAERO;
using System.Data;
using System.Net.Mail;
using System.Text;
using System.Configuration;

public partial class SalesManagement_FrmSalesActivity_V1 : System.Web.UI.Page
{
    BOLSalesActivity_V1 ObjBOL = new BOLSalesActivity_V1();
    BLLSalesActivity_V1 ObjBLL = new BLLSalesActivity_V1();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Utility.IsAuthorized())
            {
                Bind_Controls();
                Bind_BDM();
            }

        }
    }

    private void Bind_BDM()
    {
        try
        {
            ddlBDM.SelectedValue = Utility.GetCurrentUser().ToString();
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
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddllookupActivityType, ds.Tables[0]);
                Utility.BindDropDownList(ddlActivityType, ds.Tables[0]);
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlCompetitor, ds.Tables[1]);
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlTypeofContact, ds.Tables[2]);
            }
            if (ds.Tables[3].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlStatus, ds.Tables[3]);
            }
            if (ds.Tables[4].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlSessionType, ds.Tables[4]);
            }
            if (ds.Tables[5].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlSessionConsultant, ds.Tables[5]);
            }
            if (ds.Tables[6].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlStakeHolderTier, ds.Tables[6]);
                Utility.BindDropDownList(ddlTargetTier, ds.Tables[6]);
            }
            if (ds.Tables[7].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlBDM, ds.Tables[7]);
            }
            if (ds.Tables[8].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlLocPlatform, ds.Tables[8]);
            }
            if (ds.Tables[9].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlMaterialsUsed, ds.Tables[9]);
            }
            if (ds.Tables[10].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlLevelofInterest, ds.Tables[10]);
            }
            if (ds.Tables[11].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlProjectType, ds.Tables[11]);
            }
            if (ds.Tables[12].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlProjectStage, ds.Tables[12]);
            }
            if (ds.Tables[13].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlSpecStatus, ds.Tables[13]);
            }
            if (ds.Tables[14].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlConsultantRep, ds.Tables[14]);
                if (ddlConsultantRep.Items.Count > 0)
                {
                    ddlConsultantRep.SelectedIndex = 0;
                }
                Utility.BindDropDownList(ddlOriRep, ds.Tables[14]);
                if (ddlOriRep.Items.Count > 0)
                {
                    ddlOriRep.SelectedIndex = 0;
                }
                Utility.BindDropDownList(ddlDestRep, ds.Tables[14]);
                if (ddlDestRep.Items.Count > 0)
                {
                    ddlDestRep.SelectedIndex = 0;
                }
            }
            if (ds.Tables[15].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlProjectManager, ds.Tables[15]);
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

    private void BindTier(string ActivityID, string StakeHolder)
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 4;
            ObjBOL.SalesActivityID = Int32.Parse(ActivityID);
            ObjBOL.StakeHolderID = Int32.Parse(StakeHolder);
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlStakeHolderTier.SelectedValue = ds.Tables[0].Rows[0]["TierClassification"].ToString();
            }
            else
            {
                if (ddlStakeHolderTier.Items.Count > 0)
                {
                    ddlStakeHolderTier.SelectedIndex = 0;
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void BindStakeHolders(string ActivityTypeID)
    {
        try
        {
            if (ActivityTypeID == "1" || ActivityTypeID == "2")
            {
                DataSet ds = new DataSet();
                ObjBOL.Operation = 3;
                ObjBOL.SalesActivityID = Int32.Parse(ActivityTypeID);
                ds = ObjBLL.Return_DataSet(ObjBOL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Utility.BindDropDownList(ddlStakeHolder, ds.Tables[0]);
                    if (ddlStakeHolder.Items.Count > 0)
                    {
                        ddlStakeHolder.SelectedIndex = 0;
                    }
                }
                else
                {
                    if (ddlStakeHolder.Items.Count > 0)
                    {
                        ddlStakeHolder.Items.Clear();
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }


    private void BindContacts()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 2;
            ObjBOL.SalesActivityID = Int32.Parse(ddllookupActivityType.SelectedValue);
            ObjBOL.StakeHolderID = Int32.Parse(ddlStakeHolder.SelectedValue);
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlResponsiblePerson, ds.Tables[0]);
                if (ddlResponsiblePerson.Items.Count > 0)
                {
                    ddlResponsiblePerson.SelectedIndex = 0;
                }
            }
            else
            {
                if (ddlResponsiblePerson.Items.Count > 0)
                {
                    ddlResponsiblePerson.Items.Clear();
                }
            }

        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetActivityDivs()
    {
        try
        {
            divCommonContent.Visible = false;
            divCESession.Visible = false;
            divStakeHolder.Visible = false;
            divSpecProtection.Visible = false;
            divShowButtons.Visible = false;
            divgvStakeHolder.Visible = false;
            DivCommonReps.Visible = false;
            divMain.Visible = false;
            divShowButtons.Visible = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetActivityType()
    {
        try
        {
            Reset();
            if (ddllookupActivityType.SelectedValue == "1")
            {
                lblActivityTypeHeading.Text = "Consultant Visit Details";
                lblStakeHolderHeading.InnerText = "Consultant*";
                divStakeHolder.Visible = true;
                divCommonContent.Visible = true;
                divgvStakeHolder.Visible = true;
                DivCommonReps.Visible = true;
                divMain.Visible = true;
                divShowButtons.Visible = true;
            }
            else if (ddllookupActivityType.SelectedValue == "2")
            {
                lblActivityTypeHeading.Text = "Dealer Visit Details";
                lblStakeHolderHeading.InnerText = "Dealer*";
                divStakeHolder.Visible = true;
                divCommonContent.Visible = true;
                divgvStakeHolder.Visible = true;
                DivCommonReps.Visible = true;
                divMain.Visible = true;
                divShowButtons.Visible = true;
            }
            else if (ddllookupActivityType.SelectedValue == "3")
            {
                lblCESessionHeading.Text = "CE Session Details";
                divCESession.Visible = true;
                divgvStakeHolder.Visible = true;
                DivCommonReps.Visible = true;
                divMain.Visible = true;
                divShowButtons.Visible = true;
            }
            else if (ddllookupActivityType.SelectedValue == "4")
            {
                divSpecProtection.Visible = true;
                divgvStakeHolder.Visible = true;
                DivCommonReps.Visible = true;
                divMain.Visible = true;
                divShowButtons.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void SearchPNumberButton_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Utility.ReturnProposals(23, txtPNumber.Text);
            if (dt.Rows.Count > 0)
            {
                FillJobID(txtPNumber.Text);
                txtProjectName.Text = Convert.ToString(dt.Rows[0]["ProjectName"]);
            }
            else
            {
                Utility.ShowMessage_Error(Page, "P# not found");
                txtProjectName.Text = "";
                txtPNumber.Text = "";
                txtJobID.Text = "";
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void SearchJobNoButton_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Utility.ReturnProjects(27, txtJobID.Text);
            if (dt.Rows.Count > 0)
            {
                FillProposalID(txtJobID.Text);
                txtProjectName.Text = Convert.ToString(dt.Rows[0]["ProjectName"]);
            }
            else
            {
                Utility.ShowMessage_Error(Page, "Job# not found");
                txtProjectName.Text = "";
                txtJobID.Text = "";
                txtPNumber.Text = "";
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }


    private void FillJobID(string PNumber)
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 5;
            ObjBOL.PNumber = PNumber;
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtJobID.Text = ds.Tables[0].Rows[0]["JobID"].ToString();
            }
            else
            {
                txtJobID.Text = String.Empty;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void FillProposalID(string JobID)
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 6;
            ObjBOL.JobID = JobID;
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtPNumber.Text = ds.Tables[0].Rows[0]["ProposalID"].ToString();
            }
            else
            {
                txtPNumber.Text = String.Empty;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddllookupActivityType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (Utility.IsAuthorized())
            {
                int EmployeeID = Utility.GetCurrentUser();
                txtRefNumber.Text = String.Empty;
                txtActivityDate.Text = String.Empty;
                ResetActivityType();
                if (ddllookupActivityType.SelectedIndex > 0)
                {
                    BindStakeHolders(ddllookupActivityType.SelectedValue);
                    Bind_BDM();
                    BindGridStakeHolderDetails(Convert.ToString(EmployeeID), ddllookupActivityType.SelectedValue);
                    btnAdd.Enabled = true;
                }
                else
                {
                    ResetActivityType();
                }
            }

        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }


    private void Bind_DummyDataSelection(string ActivityTypeID)
    {
        try
        {
            if (ActivityTypeID == "1")
            {
                ddlStakeHolder.SelectedValue = "1";
                BindTier(ddllookupActivityType.SelectedValue, ddlStakeHolder.SelectedValue);
                txtEstimatedValue.Text = "14356";
                ddlCompetitor.SelectedValue = "1";
                txtDate.Text = "06/17/2026";
                txtTask.Text = "Task Reason Added";
                ddlTypeofContact.SelectedValue = "1";
                txtPNumber.Text = "P211221";
                FillJobID(txtPNumber.Text);

            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlStakeHolder_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlStakeHolder.SelectedIndex > 0)
            {
                BindTier(ddllookupActivityType.SelectedValue, ddlStakeHolder.SelectedValue);
                BindContacts();
            }
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
            ResetActivityDivs();
            ResetStakeHolderDetails();
            ResetCommonModule();
            ResetCESession();
            ResetSpecProtections();
            ResetReps();

        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }


    private void ResetStakeholder()
    {
        try
        {
            txtRefNumber.Text = String.Empty;
            txtActivityDate.Text = String.Empty;
            if (ddllookupActivityType.Items.Count > 0)
            {
                ddllookupActivityType.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetStakeHolderDetails()
    {
        try
        {
            if (ddlActivityType.Items.Count > 0)
            {
                ddlActivityType.SelectedIndex = 0;
            }
            if (ddlStakeHolder.Items.Count > 0)
            {
                ddlStakeHolder.SelectedIndex = 0;
            }
            if (ddlStakeHolderTier.Items.Count > 0)
            {
                ddlStakeHolderTier.SelectedIndex = 0;
            }
            txtEstimatedValue.Text = String.Empty;
            if (ddlCompetitor.Items.Count > 0)
            {
                ddlCompetitor.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetCommonModule()
    {
        try
        {
            txtDate.Text = String.Empty;
            txtTask.Text = String.Empty;
            if (ddlTypeofContact.Items.Count > 0)
            {
                ddlTypeofContact.SelectedIndex = 0;
            }
            txtPNumber.Text = "";
            txtJobID.Text = String.Empty;
            txtProjectName.Text = String.Empty;
            if (ddlResponsiblePerson.Items.Count > 0)
            {
                ddlResponsiblePerson.Items.Clear();
            }
            txtDeadline.Text = String.Empty;
            txtRegIndustryUpdates.Text = String.Empty;
            if (ddlStatus.Items.Count > 0)
            {
                ddlStatus.SelectedIndex = 0;
            }
            txtNextFollowupDate.Text = String.Empty;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetReps()
    {
        try
        {
            if (ddlConsultantRep.SelectedIndex > 0)
            {
                ddlConsultantRep.SelectedIndex = 0;
            }
            if (ddlOriRep.SelectedIndex > 0)
            {
                ddlOriRep.SelectedIndex = 0;
            }
            if (ddlDestRep.SelectedIndex > 0)
            {
                ddlDestRep.SelectedIndex = 0;
            }
            if (ddlProjectManager.Items.Count > 0)
            {
                ddlProjectManager.SelectedIndex = 0;
            }
            chkNotifywithemail.Checked = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetCESession()
    {
        try
        {
            txtSessionNumber.Text = String.Empty;
            txtSessionName.Text = String.Empty;
            if (ddlSessionType.Items.Count > 0)
            {
                ddlSessionType.SelectedIndex = 0;
            }
            txtSessionDate.Text = String.Empty;
            txtDuration.Text = String.Empty;
            if (ddlSessionConsultant.Items.Count > 0)
            {
                ddlSessionConsultant.SelectedIndex = 0;
            }
            txtCompany.Text = String.Empty;
            txtNoofAttendees.Text = String.Empty;
            if (ddlTargetTier.Items.Count > 0)
            {
                ddlTargetTier.SelectedIndex = 0;
            }
            if (ddlBDM.Items.Count > 0)
            {
                ddlBDM.SelectedValue = "263";
            }
            if (ddlLocPlatform.Items.Count > 0)
            {
                ddlLocPlatform.SelectedIndex = 0;
            }
            if (ddlMaterialsUsed.Items.Count > 0)
            {
                ddlMaterialsUsed.SelectedIndex = 0;
            }
            rdbFollowuprequired.SelectedValue = "1";
            txtFollowupDescription.Text = String.Empty;
            if (ddlLevelofInterest.Items.Count > 0)
            {
                ddlLevelofInterest.SelectedIndex = 0;
            }
            rdbPotentialProject.SelectedValue = "1";

        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetSpecProtections()
    {
        try
        {
            txtProjectNum.Text = String.Empty;
            txtSpecProjectName.Text = String.Empty;
            if (ddlProjectType.Items.Count > 0)
            {
                ddlProjectType.SelectedIndex = 0;
            }
            if (ddlProjectStage.Items.Count > 0)
            {
                ddlProjectStage.SelectedIndex = 0;
            }
            rdbCESession.SelectedValue = "1";
            rdbConsultantVisit.SelectedValue = "1";
            txtConsultantName.Text = String.Empty;
            txtDealerName.Text = String.Empty;
            if (ddlBDMOwnership.Items.Count > 0)
            {
                ddlBDMOwnership.SelectedIndex = 0;
            }
            if (ddlRegion.Items.Count > 0)
            {
                ddlRegion.SelectedIndex = 0;
            }
            if (ddlSpecStatus.Items.Count > 0)
            {
                ddlSpecStatus.SelectedIndex = 0;
            }
            txtDateLogged.Text = String.Empty;
            rdbLoggedInCRM.SelectedValue = "1";
            txtLastFollowupDate.Text = String.Empty;
            txtNextAction.Text = String.Empty;
            txtNextActionDate.Text = String.Empty;
            txtSpecEstimatedValue.Text = String.Empty;
            txtSpecCompetitor.Text = String.Empty;
            txtProbabilityPercentage.Text = String.Empty;
            rdbApprovedDrawings.SelectedValue = "1";
            rdbOrderReceived.SelectedValue = "1";
            txtDateClosed.Text = String.Empty;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void BindGridStakeHolderDetails(string EmployeeID, string activityId)
    {
        try
        {
            DataTable dtGrid = new DataTable();
            dtGrid.Columns.Add("RefNumber");
            dtGrid.Columns.Add("ActivityDate");
            dtGrid.Columns.Add("ActivityTypeID");
            //13     
            dtGrid.Columns.Add("Stakeholder");
            dtGrid.Columns.Add("Tier");
            dtGrid.Columns.Add("EstimatedValue");
            dtGrid.Columns.Add("Competetor");
            dtGrid.Columns.Add("StakeHolderVisitDate");
            dtGrid.Columns.Add("Task");
            dtGrid.Columns.Add("TypeofContact");
            dtGrid.Columns.Add("PNumber");
            dtGrid.Columns.Add("JNumber");
            dtGrid.Columns.Add("ProjectName");
            dtGrid.Columns.Add("ContactPerson");
            dtGrid.Columns.Add("ActionRequired");
            dtGrid.Columns.Add("RegionalIndustryUpdates");
            dtGrid.Columns.Add("StakeHolderStatus");
            dtGrid.Columns.Add("NextFollowUpDate");
            //CE Session 16   
            dtGrid.Columns.Add("SessionNumber");
            dtGrid.Columns.Add("SessionName");
            dtGrid.Columns.Add("SessionType");
            dtGrid.Columns.Add("SessionDate");
            dtGrid.Columns.Add("Duration");
            dtGrid.Columns.Add("Consultant");
            dtGrid.Columns.Add("Company");
            dtGrid.Columns.Add("NoofAttendees");
            dtGrid.Columns.Add("TargetTier");
            dtGrid.Columns.Add("BDM");
            dtGrid.Columns.Add("LocationPlatform");
            dtGrid.Columns.Add("MaterialsUsed");
            dtGrid.Columns.Add("Followuprequired");
            dtGrid.Columns.Add("Followupdescription");
            dtGrid.Columns.Add("levelofinterest");
            dtGrid.Columns.Add("potentialprojectidentified");
            //Spec Protection 22
            dtGrid.Columns.Add("SpecProjectNumber");
            dtGrid.Columns.Add("SpecProjectName");
            dtGrid.Columns.Add("SpecProjectType");
            dtGrid.Columns.Add("SpecProjectStage");
            dtGrid.Columns.Add("SpecCESession");
            dtGrid.Columns.Add("SpecConsultantVisit");
            dtGrid.Columns.Add("SpecConsultantName");
            dtGrid.Columns.Add("SpecDealerName");
            dtGrid.Columns.Add("SpecBDMOwnership");
            dtGrid.Columns.Add("SpecRegion");
            dtGrid.Columns.Add("SpecStatus");
            dtGrid.Columns.Add("SpecDateLogged");
            dtGrid.Columns.Add("SpecLoggedInCRM");
            dtGrid.Columns.Add("SpecLastFollowupdate");
            dtGrid.Columns.Add("SpecNextAction");
            dtGrid.Columns.Add("SpecNextActionDate");
            dtGrid.Columns.Add("SpecEstimatedValue");
            dtGrid.Columns.Add("SpecCompetitor");
            dtGrid.Columns.Add("SpecProbPerc");
            dtGrid.Columns.Add("SpecAppDwgs");
            dtGrid.Columns.Add("SpecOrderReceived");
            dtGrid.Columns.Add("SpecDateClosed");
            dtGrid.Columns.Add("ConsultantRep");
            dtGrid.Columns.Add("DestinationRep");
            dtGrid.Columns.Add("OriginationRep");
            dtGrid.Columns.Add("SalesManager");


            dtGrid.Rows.Add("REF2610001", "06/16/2026", "1", "3Pm Design, Colorado, United States", "1/Strategic",
                "$ 14,560", "Aerowerks", "06/15/2026", "Task Description", "Phone Call", "P211221", "J213731",
                "Nazareth Hospital, Philadelphia, Pennsylvania, United States,#P211221", "Mon", "Send Quotation",
                "Regional Industry Updates", "In Progress", "06/30/2026",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "",
                 "", "", "", "", "1", "1", "1", "263"
                 );


            dtGrid.Rows.Add("REF2610002", "06/17/2026", "2", "Aramark Culinary & Design Solutions, Philadelphia, Pennsylvania, United States",
                "2/Growth", "$ 25,000", "Aerowerks", "06/15/2026", "Task Description", "Phone Call", "P211221", "J213731",
                "Nazareth Hospital, Philadelphia, Pennsylvania, United States,#P211221", "Bill Binder", "Send Drawings",
                "Regional Industry Updates", "In Progress", "06/30/2026",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "",
                 "", "", "", "", "1", "1", "1", "263"
                );

            dtGrid.Rows.Add("REF2610003", "06/18/2026", "3", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "SE2610001", "Training", "In Person", "06/16/2026", "30 Min", "3Pm Design, Colorado, United States",
                "XYZ Company", "15", "1/Strategic", "Rohit Sharma", "Teams", "PPT", "Yes", "Send Product Designs",
                "High", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "1", "1", "1", "263");

            dtGrid.Rows.Add("REF2610004", "06/19/2026", "4", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
                "PR2610001", "Google", "Healthcare", "Concept", "Yes", "Yes", "Consultant 1",
                "Dealer 1", "Owner 1", "Region 1", "Identified", "06/30/2026", "Yes", "06/30/ 2026", "Action 1", "06/30/2026",
                "$ 1,60,000", " Competitor 1", "", "Yes", "Yes", "07/31/2026", "1", "1", "1", "263"
                );

            DataRow[] rows = dtGrid.Select("SalesManager = '" + EmployeeID.Replace("'", "''") + "' AND ActivityTypeID = '" + activityId.Replace("'", "''") + "'");

            DataTable dtFiltered = dtGrid.Clone();

            foreach (DataRow row in rows)
            {
                dtFiltered.ImportRow(row);
            }

            ViewState["ActivityData"] = dtGrid;
            ViewState["ActivityFilter"] = dtFiltered;
            gvActivityDetail.DataSource = dtFiltered;
            gvActivityDetail.DataBind();

        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidationCheckStakeHolder() == true)
            {
                if(ddlProjectManager.SelectedIndex>0)
                {
                    if(chkNotifywithemail.Checked==true)
                    {
                        Utility.ShowMessage_Success(Page, "Email Sent !");
                    }
                }
                //Prepare_Email();
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
            txtRefNumber.Text = String.Empty;
            txtActivityDate.Text = String.Empty;
            ResetStakeHolderDetails();
            ResetCommonModule();
            ResetCESession();
            ResetSpecProtections();
            ResetReps();
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private bool ValidationCheckStakeHolder()
    {
        try
        {
            if (txtRefNumber.Text == String.Empty)
            {
                Utility.ShowMessage_Error(Page, "Please Generate Ref # !");
                txtRefNumber.Focus();
                return false;
            }
            if (txtActivityDate.Text == String.Empty)
            {
                Utility.ShowMessage_Error(Page, "Please Enter Activity Date !");
                txtActivityDate.Focus();
                return false;
            }
            if (ddlActivityType.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Activity Type !");
                ddllookupActivityType.Focus();
                return false;
            }
            if (ddlStakeHolder.SelectedIndex == 0)
            {
                if (ddllookupActivityType.SelectedValue == "1")
                {
                    Utility.ShowMessage_Error(Page, "Please Select Consultant !");
                    ddlStakeHolder.Focus();
                    return false;
                }
                else if (ddllookupActivityType.SelectedValue == "2")
                {
                    Utility.ShowMessage_Error(Page, "Please Select Dealer !");
                    ddlStakeHolder.Focus();
                    return false;
                }

            }
            if (ddllookupActivityType.SelectedValue == "1" || ddllookupActivityType.SelectedValue == "2")
            {
                if (txtDate.Text == String.Empty)
                {
                    Utility.ShowMessage_Error(Page, "Please Enter Date !");
                    txtDate.Focus();
                    return false;
                }
                if (txtTask.Text == String.Empty)
                {
                    Utility.ShowMessage_Error(Page, "Please Enter Task !");
                    txtTask.Focus();
                    return false;
                }
                if (ddlTypeofContact.SelectedIndex == 0)
                {
                    Utility.ShowMessage_Error(Page, "Please Select Type of Contact");
                    ddlTypeofContact.Focus();
                    return false;
                }
                if (ddlStatus.SelectedIndex == 0)
                {
                    Utility.ShowMessage_Error(Page, "Please Select Status !");
                    ddlStatus.Focus();
                    return false;
                }
            }
            if (ddllookupActivityType.SelectedValue == "3")
            {
                if (txtSessionNumber.Text.Trim() == String.Empty)
                {
                    Utility.ShowMessage_Error(Page, "Please Enter Session Number !");
                    txtSessionNumber.Focus();
                    return false;
                }
                if (txtSessionName.Text.Trim() == String.Empty)
                {
                    Utility.ShowMessage_Error(Page, "Please Enter Session Name !");
                    txtSessionName.Focus();
                    return false;
                }
                if (ddlSessionType.SelectedIndex == 0)
                {
                    Utility.ShowMessage_Error(Page, "Please Select Session Type !");
                    ddlSessionType.Focus();
                    return false;
                }
                if (ddlSessionConsultant.SelectedIndex == 0)
                {
                    Utility.ShowMessage_Error(Page, "Please Select Consultant !");
                    ddlSessionConsultant.Focus();
                    return false;
                }
                if (ddlTargetTier.SelectedIndex == 0)
                {
                    Utility.ShowMessage_Error(Page, "Please Select Target Tier !");
                    ddlTargetTier.Focus();
                    return false;
                }
            }
            if (ddllookupActivityType.SelectedValue == "4")
            {
                if (txtProjectNum.Text == String.Empty)
                {
                    Utility.ShowMessage_Error(Page, "Please Enter Project Number !");
                    txtProjectNum.Focus();
                    return false;
                }
                if (txtSpecProjectName.Text == String.Empty)
                {
                    Utility.ShowMessage_Error(Page, "Please Enter Project Name !");
                    txtProjectName.Focus();
                    return false;
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }


    private string GenerateRefNumber(DataTable dtActivity)
    {
        string year = DateTime.Now.ToString("yy");

        if (dtActivity == null || dtActivity.Rows.Count == 0)
            return "REF" + year + "100001";

        int maxNo = dtActivity.AsEnumerable()
            .Where(r => !string.IsNullOrEmpty(r["RefNumber"].ToString()))
            .Select(r =>
            {
                string refNo = r["RefNumber"].ToString();
                return Convert.ToInt32(refNo.Substring(5));
            }).Max();



        return "REF" + year + (maxNo + 1);
    }

    private string GenerateSessionNumber(DataTable dtActivity)
    {
        string year = DateTime.Now.ToString("yy");

        if (dtActivity == null || dtActivity.Rows.Count == 0)
            return "SE" + year + "100001";

        int maxNo = dtActivity.AsEnumerable()
            .Where(r => !string.IsNullOrEmpty(r["SessionNumber"].ToString()))
            .Select(r =>
            {
                string sessionNo = r["SessionNumber"].ToString();
                return Convert.ToInt32(sessionNo.Substring(5));
            }).Max();



        return "SE" + year + "10000" + (maxNo + 1);
    }

    private string GenerateProjectNumber(DataTable dtActivity)
    {
        string year = DateTime.Now.ToString("yy");

        if (dtActivity == null || dtActivity.Rows.Count == 0)
            return "PR" + year + "100001";

        int maxNo = dtActivity.AsEnumerable()
            .Where(r => !string.IsNullOrEmpty(r["SpecProjectNumber"].ToString()))
            .Select(r =>
            {
                string ProjectNo = r["SpecProjectNumber"].ToString();
                return Convert.ToInt32(ProjectNo.Substring(5));
            }).Max();



        return "PR" + year + "10000" + (maxNo + 1);
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["ActivityData"] != null)
            {
                DataTable dtActivity = (DataTable)ViewState["ActivityData"];
                if (dtActivity.Rows.Count > 0)
                {
                    string RefNumber = GenerateRefNumber(dtActivity);
                    if (RefNumber != "")
                    {
                        txtRefNumber.Text = RefNumber;
                    }
                    else
                    {
                        txtRefNumber.Text = String.Empty;
                    }

                }


            }
            if (ViewState["ActivityFilter"] != null)
            {
                DataTable dtActivityFilter = (DataTable)ViewState["ActivityFilter"];
                if (ddllookupActivityType.SelectedValue == "3")
                {
                    string SessionNumber = GenerateSessionNumber(dtActivityFilter);
                    if (SessionNumber != "")
                    {
                        txtSessionNumber.Text = SessionNumber;
                    }
                    else
                    {
                        txtSessionNumber.Text = String.Empty;
                    }
                }
                else if (ddllookupActivityType.SelectedValue == "4")
                {
                    string projectNumber = GenerateProjectNumber(dtActivityFilter);
                    if (projectNumber != "")
                    {
                        txtProjectNum.Text = projectNumber;
                    }
                    else
                    {
                        txtProjectNum.Text = String.Empty;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        try
        {
            txtRefNumber.Text = String.Empty;
            txtActivityDate.Text = String.Empty;
            if (ddllookupActivityType.SelectedIndex > 0)
            {
                ddllookupActivityType.SelectedIndex = 0;
            }
            Reset();
            ResetActivityHistory();
            gvActivityDetail.DataSource = "";
            gvActivityDetail.DataBind();
            gvFollowups.DataSource = "";
            gvFollowups.DataBind();
            Modal_Followup.Hide();
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetActivityHistory()
    {
        try
        {
            if (ddlActivityHistoryFollowUpStatus.Items.Count > 0)
            {
                ddlActivityHistoryFollowUpStatus.SelectedIndex = 0;
            }
            txtActivityHistoryFollowUpDate.Text = String.Empty;
            txtActivityHistoryNextActionDate.Text = String.Empty;
            txtActivityHistoryOwner.Text = String.Empty;
            rdbCRMUpdated.SelectedValue = "1";
            rdbTeamsPosted.SelectedValue = "1";
            btnAddActivityHistory.Text = "Add";
            Modal_Followup.Show();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvActivityDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvActivityDetail.Rows[rowIndex];
            string activityType = ((Label)row.FindControl("lblActivityType")).Text;
            string stakeHolder = ((Label)row.FindControl("lblStakeHolderName")).Text;
            string sessionNo = ((Label)row.FindControl("lblSessionNumber")).Text;
            string Consultant = ((Label)row.FindControl("lblConsultant")).Text;
            string projectNo = ((Label)row.FindControl("lblSpecProjectNumber")).Text;
            string projectName = ((Label)row.FindControl("lblSpecProjectName")).Text;
            if (e.CommandName == "EditActivity")
            {
                string refNo = ((Label)row.FindControl("lblRefNumber")).Text;
                string ActivityDate = ((Label)row.FindControl("lblActivityDate")).Text;

                string Tier =
                    ((Label)row.FindControl("lblTier")).Text;
                string estValue =
                   ((Label)row.FindControl("lblEstimatedValue")).Text;
                string Competitor =
                   ((Label)row.FindControl("lblCompetetor")).Text;
                string StakeHolderDate =
                   ((Label)row.FindControl("lblDate")).Text;
                string Task =
                   ((Label)row.FindControl("lblTask")).Text;
                string TypeofContact =
                   ((Label)row.FindControl("lblTypeofContact")).Text;
                string PNumber =
                   ((Label)row.FindControl("lblPNumber")).Text;
                string JNumber =
                   ((Label)row.FindControl("lblJNumber")).Text;
                string ProjectName =
                   ((Label)row.FindControl("lblProjectName")).Text;
                string ContactPerson =
                   ((Label)row.FindControl("lblContactPerson")).Text;
                string actionRequired =
                   ((Label)row.FindControl("lblActionRequired")).Text;
                string RegionalIndUpdates =
                   ((Label)row.FindControl("lblRegionalIndustryUpdates")).Text;
                string status =
                   ((Label)row.FindControl("lblStakeHolderStatus")).Text;
                string nextFollowUpDate =
                   ((Label)row.FindControl("lblStakeNextFollwupdate")).Text;
                //CE Session                
                string sessionName = ((Label)row.FindControl("lblSessionName")).Text;
                string sessionType = ((Label)row.FindControl("lblSessionType")).Text;
                string sessionDate = ((Label)row.FindControl("lblSessionDate")).Text;
                string Duration = ((Label)row.FindControl("lblDuration")).Text;

                string Company = ((Label)row.FindControl("lblCompany")).Text;
                string NoofAttendees = ((Label)row.FindControl("lblNoofAttendees")).Text;
                string TargetTier = ((Label)row.FindControl("lblTargetTier")).Text;
                string BDM = ((Label)row.FindControl("lblBDM")).Text;
                string location = ((Label)row.FindControl("lblLocationPlatform")).Text;
                string MaterialUsed = ((Label)row.FindControl("lblMaterialsUsed")).Text;
                string FollowupReq = ((Label)row.FindControl("lblFollowuprequired")).Text;
                string FollowupDesc = ((Label)row.FindControl("lblFollowupdescription")).Text;
                string levelofInterest = ((Label)row.FindControl("lbllevelofinterest")).Text;
                string PotProjectIndentified = ((Label)row.FindControl("lblpotentialprojectidentified")).Text;

                //Spec Protection                
                string projectType = ((Label)row.FindControl("lblSpecProjectType")).Text;
                string projectStage = ((Label)row.FindControl("lblSpecProjectStage")).Text;
                string SpecCESession = ((Label)row.FindControl("lblSpecCESession")).Text;
                string consultantVisit = ((Label)row.FindControl("lblSpecConsultantVisit")).Text;
                string consultantName = ((Label)row.FindControl("lblSpecConsultantName")).Text;
                string dealerName = ((Label)row.FindControl("lblSpecDealerName")).Text;
                string BDMOwnership = ((Label)row.FindControl("lblSpecBDMOwnership")).Text;
                string region = ((Label)row.FindControl("lblSpecRegion")).Text;
                string specStatus = ((Label)row.FindControl("lblSpecStatus")).Text;
                string specDateLogged = ((Label)row.FindControl("lblSpecDateLogged")).Text;
                string specLoggedInCRM = ((Label)row.FindControl("lblSpecLoggedInCRM")).Text;
                string specLastFollowUpDate = ((Label)row.FindControl("lblSpecLastFollowupdate")).Text;
                string specNextAction = ((Label)row.FindControl("lblSpecNextAction")).Text;
                string specNextActiondate = ((Label)row.FindControl("lblSpecNextActionDate")).Text;
                string specEstValue = ((Label)row.FindControl("lblSpecEstimatedValue")).Text;
                string specCompetitor = ((Label)row.FindControl("lblSpecCompetitor")).Text;
                string probability = ((Label)row.FindControl("lblSpecProbPerc")).Text;
                string appDrawings = ((Label)row.FindControl("lblSpecAppDwgs")).Text;
                string orderReceived = ((Label)row.FindControl("lblSpecOrderReceived")).Text;
                string specDateClosed = ((Label)row.FindControl("lblSpecDateClosed")).Text;

                string constRep = ((Label)row.FindControl("lblConsultantRep")).Text;
                string destRep = ((Label)row.FindControl("lblDestRep")).Text;
                string origRep = ((Label)row.FindControl("lblOrigRep")).Text;

                txtRefNumber.Text = refNo;
                txtActivityDate.Text = ActivityDate;
                if (ddllookupActivityType.SelectedIndex > 0)
                {
                    ddlActivityType.SelectedValue = activityType;
                }
                if (ddllookupActivityType.SelectedValue == "1")
                {
                    ddlStakeHolder.SelectedValue = "392";
                }
                else if (ddllookupActivityType.SelectedValue == "2")
                {
                    ddlStakeHolder.SelectedValue = "1";
                }
                BindContacts();
                txtEstimatedValue.Text = estValue;
                ddlCompetitor.SelectedValue = "1";
                txtDate.Text = StakeHolderDate;
                txtTask.Text = Task;
                ddlTypeofContact.SelectedValue = "2";
                txtPNumber.Text = PNumber;
                txtJobID.Text = JNumber;
                txtProjectName.Text = ProjectName;
                if (ddllookupActivityType.SelectedValue == "1")
                {
                    ddlResponsiblePerson.SelectedValue = "448";
                    ddlStakeHolderTier.SelectedValue = "1";
                }
                else if (ddllookupActivityType.SelectedValue == "2")
                {
                    ddlResponsiblePerson.SelectedValue = "494";
                    ddlStakeHolderTier.SelectedValue = "2";
                }
                txtDeadline.Text = actionRequired;
                txtRegIndustryUpdates.Text = RegionalIndUpdates;
                ddlStatus.SelectedValue = "1";
                txtNextFollowupDate.Text = nextFollowUpDate;
                txtSessionNumber.Text = sessionNo;
                txtSessionName.Text = sessionName;
                ddlSessionType.SelectedValue = "1";
                txtSessionDate.Text = sessionDate;
                txtDuration.Text = Duration;
                ddlSessionConsultant.SelectedValue = "392";
                txtCompany.Text = Company;
                txtNoofAttendees.Text = NoofAttendees;
                ddlTargetTier.SelectedValue = "1";
                ddlBDM.SelectedValue = "263";
                ddlLocPlatform.SelectedValue = "1";
                ddlMaterialsUsed.SelectedValue = "1";
                rdbFollowuprequired.SelectedValue = "1";
                txtFollowupDescription.Text = FollowupDesc;
                ddlLevelofInterest.SelectedValue = "1";
                rdbPotentialProject.SelectedValue = "1";
                txtProjectNum.Text = projectNo;
                txtSpecProjectName.Text = projectName;
                ddlProjectType.SelectedValue = "1";
                ddlProjectStage.SelectedValue = "1";
                rdbCESession.SelectedValue = "1";
                rdbConsultantVisit.SelectedValue = "1";
                txtConsultantName.Text = consultantName;
                txtDealerName.Text = dealerName;
                ddlBDMOwnership.SelectedValue = "1";
                ddlSpecStatus.SelectedValue = "1";
                txtDateLogged.Text = specDateLogged;
                rdbLoggedInCRM.SelectedValue = "1";
                ddlRegion.SelectedValue = "1";
                txtLastFollowupDate.Text = specLastFollowUpDate;
                txtNextAction.Text = specNextAction;
                txtNextActionDate.Text = specNextActiondate;
                txtSpecEstimatedValue.Text = specEstValue;
                txtSpecCompetitor.Text = specCompetitor;
                txtProbabilityPercentage.Text = probability;
                rdbApprovedDrawings.SelectedValue = "1";
                rdbOrderReceived.SelectedValue = "1";
                txtDateClosed.Text = specDateClosed;
                ddlConsultantRep.SelectedValue = "1";
                ddlDestRep.SelectedValue = "1";
                ddlOriRep.SelectedValue = "1";
                btnSave.Text = "Update";
            }
            if (e.CommandName == "Followup")
            {
                if (activityType == "1")
                {
                    lblActivity.Text = "Consultant- " + stakeHolder;
                }
                else if (activityType == "2")
                {
                    lblActivity.Text = "Dealer- " + stakeHolder;
                }
                else if (activityType == "3")
                {
                    lblActivity.Text = sessionNo + " " + Consultant;
                }
                else if (activityType == "4")
                {
                    lblActivity.Text = projectNo + " " + projectName;
                }

                ResetActivityHistory();
                BindFollowupGrid(activityType);
                Modal_Followup.Show();
            }
            if (e.CommandName == "DeleteActivity")
            {
                BindFollowupGrid(activityType);
                DataTable dtFollowUpData = new DataTable();
                dtFollowUpData = (DataTable)ViewState["ActivityHistory"];
                if (dtFollowUpData.Rows.Count > 0)
                {
                    Utility.ShowMessage_Error(Page, "Activity History exists !");
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }


    private void BindFollowupGrid(string activityType)
    {
        try
        {
            DataTable dtActivityHistoryGrid = new DataTable();
            dtActivityHistoryGrid.Columns.Add("ActivityTypeID");
            dtActivityHistoryGrid.Columns.Add("Status");
            dtActivityHistoryGrid.Columns.Add("LastFollowupDate");
            dtActivityHistoryGrid.Columns.Add("NextAction");
            dtActivityHistoryGrid.Columns.Add("Owner");
            dtActivityHistoryGrid.Columns.Add("CRMUpdated");
            dtActivityHistoryGrid.Columns.Add("TeamsPosted");

            dtActivityHistoryGrid.Rows.Add("1", "New", "04/06/2026", "04/06/2026", "Ed", "Yes", "No");
            dtActivityHistoryGrid.Rows.Add("1", "New", "04/14/2026", "04/22/2026", "Ed", "Yes", "No");

            dtActivityHistoryGrid.Rows.Add("3", "New", "04/06/2026", "04/06/2026", "Ed", "Yes", "No");
            dtActivityHistoryGrid.Rows.Add("3", "New", "04/14/2026", "04/22/2026", "Ed", "Yes", "No");

            dtActivityHistoryGrid.Rows.Add("4", "New", "04/06/2026", "04/06/2026", "Ed", "Yes", "No");
            dtActivityHistoryGrid.Rows.Add("4", "New", "04/14/2026", "04/22/2026", "Ed", "Yes", "No");

            DataRow[] rows = dtActivityHistoryGrid.Select("ActivityTypeID = '" + activityType + "'");

            DataTable dtFiltered = dtActivityHistoryGrid.Clone();

            foreach (DataRow activityHistoryRow in rows)
            {
                dtFiltered.ImportRow(activityHistoryRow);
            }
            ViewState["ActivityHistory"] = dtFiltered;

            gvFollowups.DataSource = dtFiltered;
            gvFollowups.DataBind();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    protected void gvActivityDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                int activityTypeId = Convert.ToInt32(ddllookupActivityType.SelectedValue);

                if (activityTypeId == 1)
                {
                    e.Row.Cells[0].Text = "Consultant";
                }
                else if (activityTypeId == 2)
                {
                    e.Row.Cells[0].Text = "Dealer";
                }
                gvActivityDetail.Columns[3].Visible = (activityTypeId == 1 || activityTypeId == 2);
                gvActivityDetail.Columns[4].Visible = (activityTypeId == 1 || activityTypeId == 2);
                gvActivityDetail.Columns[5].Visible = (activityTypeId == 1 || activityTypeId == 2);
                gvActivityDetail.Columns[6].Visible = (activityTypeId == 1 || activityTypeId == 2);
                gvActivityDetail.Columns[7].Visible = (activityTypeId == 1 || activityTypeId == 2);
                gvActivityDetail.Columns[8].Visible = (activityTypeId == 1 || activityTypeId == 2);
                gvActivityDetail.Columns[9].Visible = (activityTypeId == 1 || activityTypeId == 2);
                gvActivityDetail.Columns[13].Visible = (activityTypeId == 1 || activityTypeId == 2);
                gvActivityDetail.Columns[16].Visible = (activityTypeId == 1 || activityTypeId == 2);
                gvActivityDetail.Columns[17].Visible = (activityTypeId == 1 || activityTypeId == 2);
                gvActivityDetail.Columns[18].Visible = (activityTypeId == 3);
                gvActivityDetail.Columns[19].Visible = (activityTypeId == 3);
                gvActivityDetail.Columns[20].Visible = (activityTypeId == 3);
                gvActivityDetail.Columns[21].Visible = (activityTypeId == 3);
                gvActivityDetail.Columns[22].Visible = (activityTypeId == 3);
                gvActivityDetail.Columns[23].Visible = (activityTypeId == 3);
                gvActivityDetail.Columns[24].Visible = (activityTypeId == 3);
                gvActivityDetail.Columns[25].Visible = (activityTypeId == 3);
                gvActivityDetail.Columns[26].Visible = (activityTypeId == 3);
                gvActivityDetail.Columns[27].Visible = (activityTypeId == 3);
                gvActivityDetail.Columns[28].Visible = (activityTypeId == 3);
                //gvActivityDetail.Columns[29].Visible = (activityTypeId == 3);
                gvActivityDetail.Columns[30].Visible = (activityTypeId == 3);
                // gvActivityDetail.Columns[31].Visible = (activityTypeId == 3);
                gvActivityDetail.Columns[32].Visible = (activityTypeId == 3);
                gvActivityDetail.Columns[33].Visible = (activityTypeId == 3);
                gvActivityDetail.Columns[34].Visible = (activityTypeId == 4);
                gvActivityDetail.Columns[35].Visible = (activityTypeId == 4);
                gvActivityDetail.Columns[36].Visible = (activityTypeId == 4);
                gvActivityDetail.Columns[37].Visible = (activityTypeId == 4);
                gvActivityDetail.Columns[38].Visible = (activityTypeId == 4);
                gvActivityDetail.Columns[39].Visible = (activityTypeId == 4);
                gvActivityDetail.Columns[40].Visible = (activityTypeId == 4);
                gvActivityDetail.Columns[41].Visible = (activityTypeId == 4);
                gvActivityDetail.Columns[42].Visible = (activityTypeId == 4);
                gvActivityDetail.Columns[43].Visible = (activityTypeId == 4);
                gvActivityDetail.Columns[44].Visible = (activityTypeId == 4);
                gvActivityDetail.Columns[45].Visible = (activityTypeId == 4);
                gvActivityDetail.Columns[46].Visible = (activityTypeId == 4);

            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnAddActivityHistory_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnCancelActivityHistory_Click(object sender, EventArgs e)
    {
        try
        {
            ResetActivityHistory();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvFollowups_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "EditFollowup")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvFollowups.Rows[rowIndex];
                string status = ((Label)row.FindControl("lblActHisStatus")).Text;
                string lastFollowupDate = ((Label)row.FindControl("lblActHisFollowupdate")).Text;
                string nextAction = ((Label)row.FindControl("lblActHisNextAction")).Text;
                string owner = ((Label)row.FindControl("lblActHisOwner")).Text;
                string crmUpdated = ((Label)row.FindControl("lblActHisCRMUpdated")).Text;
                string teamsPosted = ((Label)row.FindControl("lblActHisTeamsUpdated")).Text;

                ddlActivityHistoryFollowUpStatus.SelectedValue = "1";
                txtActivityHistoryFollowUpDate.Text = lastFollowupDate;
                txtActivityHistoryNextActionDate.Text = nextAction;
                txtActivityHistoryOwner.Text = owner;
                rdbCRMUpdated.SelectedValue = "1";
                rdbTeamsPosted.SelectedValue = "1";
                btnAddActivityHistory.Text = "Update";
                Modal_Followup.Show();
            }
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

        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void Send_SalesActivityPMEmail()
    {
        try
        {
            if (Utility.SalesActivityPMEmailSwitch())
            {
                Prepare_Email();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void Prepare_Email()
    {
        try
        {

            string RefNo = string.Empty;
            DateTime activityDate;
            string formatActivityDate = string.Empty;
            string activityType = string.Empty;
            string stakeholder = string.Empty;
            string tier = string.Empty;
            DateTime stakeholderdate;
            string formatstakeholderdate = string.Empty;
            string task = string.Empty;
            string typeofcontact = string.Empty;
            string pnumber = string.Empty;
            string jnumber = string.Empty;
            string projectname = string.Empty;
            string contactperson = string.Empty;
            DateTime nextfollowupdate;
            string formatnextfollowupdate = string.Empty;
            string projectManager = string.Empty;
            string salesManager = string.Empty;
            //CE Session
            string sessionNo = string.Empty;
            string sessionName = string.Empty;
            string sessionType = string.Empty;
            DateTime sessionDate;
            string formatsessiondate = string.Empty;
            string sessionConsultant = string.Empty;
            string sessionBDM = string.Empty;
            string sessionTargetTier = string.Empty;
            string sessionlocplatform = string.Empty;
            string sessionfollowuprequired = string.Empty;
            string sessionlevelofinterest = string.Empty;
            //spec protection
            string specprojectno = string.Empty;
            string specprojectname = string.Empty;
            string specprojecttype = string.Empty;
            string specprojectstage = string.Empty;
            string speccesession = string.Empty;
            string specconsultantname = string.Empty;
            string specdealername = string.Empty;
            DateTime specdatelogged;
            string formatspecdatelogged = string.Empty;
            DateTime specfollowupdatelogged;
            string formatspecfollowupdatelogged = string.Empty;
            string estimatedvalue = string.Empty;
            string specappdwgs = string.Empty;
            string specorderreceived = string.Empty;
            DateTime dateclosed;
            string formatspecdateclosed = string.Empty;


            if (txtRefNumber.Text != "")
            {
                RefNo = txtRefNumber.Text;
            }
            if (DateTime.TryParse(txtActivityDate.Text, out activityDate))
            {
                formatActivityDate = activityDate.ToString("MMMM dd, yyyy");
            }
            if (ddlActivityType.SelectedIndex > 0)
            {
                activityType = ddlActivityType.SelectedItem.Text;
            }
            if (ddlStakeHolder.SelectedIndex > 0)
            {
                stakeholder = ddlStakeHolder.SelectedItem.Text;
            }
            if (ddlStakeHolderTier.SelectedIndex > 0)
            {
                tier = ddlStakeHolderTier.SelectedItem.Text;
            }
            if (DateTime.TryParse(txtDate.Text, out stakeholderdate))
            {
                formatstakeholderdate = stakeholderdate.ToString("MMMM dd, yyyy");
            }
            if (txtTask.Text != "")
            {
                task = txtTask.Text;
            }
            if (ddlTypeofContact.SelectedIndex > 0)
            {
                typeofcontact = ddlTypeofContact.SelectedItem.Text;
            }
            if (txtPNumber.Text.Trim() != "")
            {
                pnumber = txtPNumber.Text.Trim();
            }
            if (txtJobID.Text.Trim() != "")
            {
                jnumber = txtJobID.Text.Trim();
            }
            if (txtProjectName.Text != "")
            {
                projectname = txtProjectName.Text;
            }
            if (ddlResponsiblePerson.Items.Count > 0)
            {
                contactperson = ddlResponsiblePerson.SelectedItem.Text;
            }
            if (DateTime.TryParse(txtNextFollowupDate.Text, out nextfollowupdate))
            {
                formatnextfollowupdate = nextfollowupdate.ToString("MMMM dd, yyyy");
            }
            if (ddlProjectManager.SelectedIndex > 0)
            {
                projectManager = ddlProjectManager.SelectedItem.Text;
            }
            if (Utility.IsAuthorized())
            {
                salesManager = Utility.GetCurrentSession().EmployeeName;
            }

            //CE Session
            if (txtSessionNumber.Text != "")
            {
                sessionNo = txtSessionNumber.Text;
            }
            if (txtSessionName.Text != "")
            {
                sessionName = txtSessionName.Text;
            }
            if (ddlSessionType.SelectedIndex > 0)
            {
                sessionType = ddlSessionType.SelectedItem.Text;
            }
            if (DateTime.TryParse(txtSessionDate.Text, out sessionDate))
            {
                formatsessiondate = sessionDate.ToString("MMMM dd, yyyy");
            }
            if (ddlSessionConsultant.SelectedIndex > 0)
            {
                sessionConsultant = ddlSessionConsultant.SelectedItem.Text;
            }
            if (ddlBDM.SelectedIndex > 0)
            {
                sessionBDM = ddlBDM.SelectedItem.Text;
            }
            if (ddlTargetTier.SelectedIndex > 0)
            {
                sessionTargetTier = ddlTargetTier.SelectedItem.Text;
            }
            if (ddlLocPlatform.SelectedIndex > 0)
            {
                sessionlocplatform = ddlLocPlatform.SelectedItem.Text;
            }
            if (rdbFollowuprequired.SelectedValue == "1")
            {
                sessionfollowuprequired = "Yes";
            }
            else if (rdbFollowuprequired.SelectedValue == "2")
            {
                sessionfollowuprequired = "No";
            }
            if (ddlLevelofInterest.SelectedIndex > 0)
            {
                sessionlevelofinterest = ddlLevelofInterest.SelectedItem.Text;
            }
            if(txtProjectNum.Text != "")
            {
                specprojectno = txtProjectNum.Text;
            }
            if(txtSpecProjectName.Text != "")
            {
                specprojectname = txtSpecProjectName.Text;
            }
            if(ddlProjectType.SelectedIndex>0)
            {
                specprojecttype = ddlProjectType.SelectedItem.Text;
            }
            if(ddlProjectStage.SelectedIndex>0)
            {
                specprojectstage = ddlProjectStage.SelectedItem.Text;
            }
            if(rdbCESession.SelectedValue=="1")
            {
                speccesession = "Yes";
            }
            else
            {
                speccesession = "No";
            }
            if(txtConsultantName.Text != "")
            {
                specconsultantname = txtConsultantName.Text;
            }
            if(txtDealerName.Text != "")
            {
                specdealername = txtDealerName.Text;
            }
            if(DateTime.TryParse(txtDateLogged.Text, out specdatelogged))
            {
                formatspecdatelogged=specdatelogged.ToString("MMMM dd, yyyy");
            }
            if(DateTime.TryParse(txtLastFollowupDate.Text, out specfollowupdatelogged))
            {
                formatspecfollowupdatelogged=specfollowupdatelogged.ToString("MMMM dd, yyyy");
            }
            if(txtSpecEstimatedValue.Text != "")
            {
                estimatedvalue = txtSpecEstimatedValue.Text;
            }
            if(rdbApprovedDrawings.SelectedValue=="1")
            {
                specappdwgs = "Yes";
            }
            else
            {
                specappdwgs = "No";
            }
            if(rdbOrderReceived.SelectedValue=="1")
            {
                specorderreceived = "Yes";
            }
            else
            {
                specorderreceived = "No";
            }
            if(DateTime.TryParse(txtDateClosed.Text, out dateclosed))
            {
                formatspecdateclosed= dateclosed.ToString("MMMM dd, yyyy");
            }
            string Message = string.Empty;
            Message += "<!doctype><html lang='en'><head><meta charset = 'utf-8'><meta name = 'viewport' content = 'width=device-width, initial-scale=1'> ";
            Message += " <title> Sales Activity </title></head><body><table cellpadding='0' cellspacing='0' style='border-collapse:collapse;width:100%;font-family:Calibri;font-size:1.15rem'> ";
            Message += " <tr><td><table border='1' cellpadding='8' cellspacing='0' style='border-collapse:collapse;width:100%;max-width:580px;margin:0 auto;border-color:#ddd'> ";
            Message += " <tr><td colspan='2'><h2 style='margin:0;font-size:1.15rem'> Hi " + projectManager + ",</h2> ";
            Message += " <p style = 'margin-top:5px'>Please review the attached Sales Activity module screens for <strong>" + activityType + "</strong>.<br/>";
            Message += " </td ></tr><tr><td colspan='2'><div style = 'width:80px;margin:0 auto'> ";
            Message += " <svg version='1.1' xmlns:xlink='http://www.w3.org/1999/xlink' id = 'Capa_1' enable-background='new 0 0 512 512' viewBox='0 0 512 512' style='width:100%;height:100%;' xmlns='http://www.w3.org/2000/svg'><g><g><g><path d='m369.4 76.49v401.05c0 3.26-.45 6.41-1.3 9.4-4.09 14.46-17.39 25.06-33.16 25.06h-239.78c-19.03 0-34.45-15.43-34.45-34.46v-401.05c0-19.03 15.42-34.46 34.45-34.46h239.78c19.03 0 34.46 15.43 34.46 34.46z' fill = '#a1412b' /><path d = 'm132.73 394.716v60.854c0 17.33 14.04 31.37 31.37 31.37h204c.85-2.99 1.3-6.14 1.3-9.4v-82.824z' fill = '#7f392c' /><path d = 'm334.941 42.034h-239.78c-19.031 0-34.455 15.424-34.455 34.455v401.053c0 19.031 15.424 34.455 34.455 34.455h239.781c19.031 0 34.455-15.424 34.455-34.455v-401.053c-.001-19.031-15.424-34.455-34.456-34.455zm-2.143 433.365h-235.494v-396.767h235.494z' fill= '#db765a' /> ";
            Message += " <path d = 'm369.4 394.72v82.82c0 3.26-.45 6.41-1.3 9.4h-204c-9.8 0-18.56-4.49-24.31-11.54h193.01v-80.68z' fill='#b55434' /><path d = 'm95.161 42.034c-19.029 0-34.455 15.426-34.455 34.455v219.149c0 10.106 8.193 18.299 18.299 18.299 10.106 0 18.299-8.193 18.299-18.299v-217.006h41.798c10.106 0 18.299-8.193 18.299-18.299 0-10.106-8.193-18.299-18.299-18.299z' fill='#f2886b' /><path d = 'm97.304 223.144v-82.649c0-10.106-8.193-18.299-18.299-18.299-10.106 0-18.299 8.193-18.299 18.299v82.649c0 10.106 8.193 18.299 18.299 18.299 10.106 0 18.299-8.193 18.299-18.299z' fill='#f79a7c' /></g><g><path d = 'm289.014 246.714v204.031h-124.915c-17.325 0-31.37-14.045-31.37-31.37v-162.204c0-5.775 4.682-10.457 10.457-10.457z' fill='#d8d4c9' /><path d='m289.01 246.71v204.04h-124.91c-11.88 0-22.22-6.6-27.54-16.34 26.46-35.27 42.13-79.09 42.13-126.57 0-21.26-3.14-41.78-8.99-61.13z' fill='#b5b1a4' /><path d='m195.728 419.376v-408.919c0-5.775 4.682-10.457 10.457-10.457h234.652c5.775 0 10.457 4.682 10.457 10.457v408.919c0 17.325-14.045 31.37-31.37 31.37h-255.565c17.325 0 31.369-14.045 31.369-31.37z' fill='#f1eee0' /><path d='m213.1 252.167c-9.593 0-17.37-7.777-17.37-17.37v-175.393c0-9.593 7.777-17.37 17.37-17.37 9.593 0 17.37 7.777 17.37 17.37v175.392c.001 9.594-7.776 17.371-17.37 17.371z' fill='#f9f8f2' /> ";
            Message += " <path d = 'm451.29 10.46v408.92c0 17.32-14.04 31.37-31.37 31.37h-99.7c62.77-106.75 98.77-231.12 98.77-363.91 0-29.39-1.76-58.37-5.19-86.84h27.04c5.77 0 10.45 4.68 10.45 10.46z' fill='#e8e4d8' /><path d='m195.73 344.78h255.56v21.67h-255.56z' fill='#ffc751' /><path d='m327.904 344.78h-93.578c-5.984 0-10.835 4.851-10.835 10.835 0 5.984 4.851 10.835 10.835 10.835h93.578c5.984 0 10.835-4.851 10.835-10.835 0-5.984-4.851-10.835-10.835-10.835z' fill='#ffe059' /> ";
            Message += " <path d = 'm451.29 10.46v70.63h-255.56v-70.63c0-5.78 4.68-10.46 10.46-10.46h234.65c5.77 0 10.45 4.68 10.45 10.46z' fill='#ffc751' /> ";
            Message += " <path d = 'm451.29 10.46v70.63h-32.32c-.22-27.42-1.96-54.48-5.17-81.09h27.04c5.77 0 10.45 4.68 10.45 10.46z' fill='#ffaf40' /></g></g><g><g> ";
            Message += " <g fill = '#8f8b81'><path d = 'm251.009 132.32h-18.706c-4.948 0-8.959-4.011-8.959-8.959 0-4.948 4.011-8.959 8.959-8.959h18.706c4.948 0 8.959 4.011 8.959 8.959-.001 4.948-4.012 8.959-8.959 8.959z' /> ";
            Message += " <path d = 'm306.009 160.899h-73.706c-4.948 0-8.959-4.011-8.959-8.959 0-4.948 4.011-8.959 8.959-8.959h73.706c4.948 0 8.959 4.011 8.959 8.959-.001 4.948-4.012 8.959-8.959 8.959z' /></g></g><g><g> ";
            Message += " <path d = 'm421.837 139.355c1.226-.808 1.226-2.601 0-3.409-3.164-2.084-6.168-4.51-8.957-7.278s-5.233-5.749-7.333-8.889c-.814-1.217-2.621-1.217-3.435 0-2.1 3.14-4.544 6.121-7.333 8.889s-5.793 5.193-8.957 7.278c-1.226.808-1.226 2.601 0 3.409 3.164 2.084 6.168 4.51 8.957 7.278s5.233 5.749 7.333 8.889c.814 1.217 2.621 1.217 3.435 0 2.1-3.14 4.544-6.121 7.333-8.889s5.793-5.194 8.957-7.278z' fill='#ffaf40' /></g></g></g><g><g> ";
            Message += " <g fill = '#8f8b81'><path d='m251.009 208.021h-18.706c-4.948 0-8.959-4.011-8.959-8.959 0-4.948 4.011-8.959 8.959-8.959h18.706c4.948 0 8.959 4.011 8.959 8.959-.001 4.948-4.012 8.959-8.959 8.959z' /> ";
            Message += " <path d = 'm306.009 236.6h-73.706c-4.948 0-8.959-4.011-8.959-8.959 0-4.948 4.011-8.959 8.959-8.959h73.706c4.948 0 8.959 4.011 8.959 8.959-.001 4.948-4.012 8.959-8.959 8.959z' /></g></g><g><g> ";
            Message += " <path d = 'm421.837 215.056c1.226-.808 1.226-2.601 0-3.409-3.164-2.084-6.168-4.51-8.957-7.278s-5.233-5.749-7.333-8.889c-.814-1.217-2.621-1.217-3.435 0-2.1 3.14-4.544 6.121-7.333 8.889s-5.793 5.193-8.957 7.278c-1.226.808-1.226 2.601 0 3.409 3.164 2.084 6.168 4.51 8.957 7.278s5.233 5.749 7.333 8.889c.814 1.217 2.621 1.217 3.435 0 2.1-3.14 4.544-6.121 7.333-8.889 2.789-2.769 5.793-5.194 8.957-7.278z' fill='#ffaf40' /></g></g></g><g><g><g> ";
            Message += " <path d = 'm251.009 283.722h-18.706c-4.948 0-8.959-4.011-8.959-8.959 0-4.948 4.011-8.959 8.959-8.959h18.706c4.948 0 8.959 4.011 8.959 8.959-.001 4.948-4.012 8.959-8.959 8.959z' fill='#8f8b81' /> ";
            Message += " <path d = 'm306.009 312.301h-73.706c-4.948 0-8.959-4.011-8.959-8.959 0-4.948 4.011-8.959 8.959-8.959h73.706c4.948 0 8.959 4.011 8.959 8.959-.001 4.948-4.012 8.959-8.959 8.959z' fill='#8f8b81' /><g fill='#b5b1a4'> ";
            Message += " <path d = 'm259.14 294.383h-16.15c-4.948 0-8.959 4.011-8.959 8.959 0 4.948 4.011 8.959 8.959 8.959h16.149c4.948 0 8.959-4.011 8.959-8.959.001-4.948-4.01-8.959-8.958-8.959z' /> ";
            Message += " <path d = 'm259.14 218.682h-16.15c-4.948 0-8.959 4.011-8.959 8.959 0 4.948 4.011 8.959 8.959 8.959h16.149c4.948 0 8.959-4.011 8.959-8.959.001-4.948-4.01-8.959-8.958-8.959z' /> ";
            Message += " <path d = 'm259.14 142.981h-16.15c-4.948 0-8.959 4.011-8.959 8.959 0 4.948 4.011 8.959 8.959 8.959h16.149c4.948 0 8.959-4.011 8.959-8.959.001-4.948-4.01-8.959-8.958-8.959z' /></g> ";
            Message += " <path d = 'm351.009 418.538h-118.706c-4.948 0-8.959-4.011-8.959-8.959 0-4.948 4.011-8.959 8.959-8.959h118.706c4.948 0 8.959 4.011 8.959 8.959-.001 4.948-4.012 8.959-8.959 8.959z' fill='#8f8b81' /> ";
            Message += " <path d = 'm281.558 409.579c0 4.948 4.011 8.959 8.959 8.959h28.472c4.948 0 8.959-4.011 8.959-8.959 0-4.948-4.011-8.959-8.959-8.959h-28.472c-4.948.001-8.959 4.012-8.959 8.959z' fill = '#b5b1a4' /></g></g><g><g> ";
            Message += " <path d = 'm421.837 290.757c1.226-.808 1.226-2.601 0-3.409-3.164-2.084-6.168-4.51-8.957-7.278s-5.233-5.749-7.333-8.889c-.814-1.217-2.621-1.217-3.435 0-2.1 3.14-4.544 6.121-7.333 8.889s-5.793 5.193-8.957 7.278c-1.226.808-1.226 2.601 0 3.409 3.164 2.084 6.168 4.51 8.957 7.278s5.233 5.749 7.333 8.889c.814 1.217 2.621 1.217 3.435 0 2.1-3.14 4.544-6.121 7.333-8.889s5.793-5.194 8.957-7.278z' fill='#229bff' /></g></g><g><g> ";
            Message += " <path d= 'm421.837 410.49c1.226-.808 1.226-2.601 0-3.409-3.164-2.084-6.168-4.51-8.957-7.278s-5.233-5.749-7.333-8.889c-.814-1.217-2.621-1.217-3.435 0-2.1 3.14-4.544 6.121-7.333 8.889s-5.793 5.193-8.957 7.278c-1.226.808-1.226 2.601 0 3.409 3.164 2.084 6.168 4.51 8.957 7.278s5.233 5.749 7.333 8.889c.814 1.217 2.621 1.217 3.435 0 2.1-3.14 4.544-6.121 7.333-8.889s5.793-5.194 8.957-7.278z' fill='#229bff' /></g></g></g> ";
            Message += " <path d = 'm375.049 58.537h-103.076c-5.523 0-10-4.477-10-10v-18.822c0-5.523 4.477-10 10-10h103.076c5.523 0 10 4.477 10 10v18.822c0 5.523-4.477 10-10 10z' fill='#f1eee0' /> ";
            Message += " <path d = 'm451.29 344.78v21.67h-88.71c3.04-7.16 5.95-14.39 8.75-21.67z' fill='#ffaf40' /> ";
            Message += " <path d = 'm230.47 59.4v21.69h-34.74v-21.69c0-9.59 7.78-17.37 17.37-17.37 4.8 0 9.14 1.95 12.28 5.09s5.09 7.48 5.09 12.28z' fill='#ffe059' /></g></svg></div> ";
            Message += " <h1 style ='font-size:1.65rem;margin:.3rem 0 0;color:#000;text-align:center'>Sales Activity</h1> </td></tr>";
            Message += " <tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Ref Number</td><td style='font-weight:600;width:99%'> " + RefNo + "</td></tr>";
            Message += " <tr><td style='width:1%;white-space:nowrap'> Activity Date </td><td style='font-weight:600;width:99%'>" + formatActivityDate + "</td></tr>";
            Message += " <tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Activity Type</td><td style='font-weight:600;width:99%'> " + activityType + "</td></tr>";
            if (ddllookupActivityType.SelectedValue == "1" || ddlActivityType.SelectedValue == "2")
            {
                if (ddllookupActivityType.SelectedValue == "1")
                {
                    Message += " <tr><td style='width:1%;white-space:nowrap'> Consultant </td><td style='font-weight:600;width:99%'>" + stakeholder + "</td></tr>";
                }
                else if (ddllookupActivityType.SelectedValue == "2")
                {
                    Message += " <tr><td style='width:1%;white-space:nowrap'> Dealer </td><td style='font-weight:600;width:99%'>" + stakeholder + "</td></tr>";
                }
                Message += " <tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Tier</td><td style='font-weight:600;width:99%'> " + tier + "</td></tr>";
                Message += " <tr><td style='width:1%;white-space:nowrap'> Date </td><td style='font-weight:600;width:99%'>" + formatstakeholderdate + "</td></tr>";
                Message += " <tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Task</td><td style='font-weight:600;width:99%'> " + task + "</td></tr>";
                Message += " <tr><td style='width:1%;white-space:nowrap'> Type of Contact </td><td style='font-weight:600;width:99%'>" + typeofcontact + "</td></tr>";
                Message += " <tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>P-Number</td><td style='font-weight:600;width:99%'> " + pnumber + "</td></tr>";
                Message += " <tr><td style='width:1%;white-space:nowrap'> Job Number</td><td style='font-weight:600;width:99%'>" + jnumber + "</td></tr>";
                Message += " <tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Project Name</td><td style='font-weight:600;width:99%'> " + projectname + "</td></tr>";
                Message += " <tr><td style='width:1%;white-space:nowrap'> Contact Person </td><td style='font-weight:600;width:99%'>" + contactperson + "</td></tr>";
                Message += " <tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Next Follow-up Date</td><td style='font-weight:600;width:99%'> " + formatnextfollowupdate + "</td></tr>";

            }
            else if (ddllookupActivityType.SelectedValue == "3")
            {
                Message += " <tr><td style='width:1%;white-space:nowrap'> Session Number </td><td style='font-weight:600;width:99%'>" + sessionNo + "</td></tr>";
                Message += " <tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Session Name</td><td style='font-weight:600;width:99%'> " + sessionName + "</td></tr>";
                Message += " <tr><td style='width:1%;white-space:nowrap'> Session Type </td><td style='font-weight:600;width:99%'>" + sessionType + "</td></tr>";
                Message += " <tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Date</td><td style='font-weight:600;width:99%'> " + formatsessiondate + "</td></tr>";
                Message += " <tr><td style='width:1%;white-space:nowrap'> Consultant </td><td style='font-weight:600;width:99%'>" + sessionConsultant + "</td></tr>";
                Message += " <tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Target Tier</td><td style='font-weight:600;width:99%'> " + sessionTargetTier + "</td></tr>";
                Message += " <tr><td style='width:1%;white-space:nowrap'> BDM </td><td style='font-weight:600;width:99%'>" + sessionBDM + "</td></tr>";
                Message += " <tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Location/Platform</td><td style='font-weight:600;width:99%'> " + sessionlocplatform + "</td></tr>";
                Message += " <tr><td style='width:1%;white-space:nowrap'> Follow-up Required</td><td style='font-weight:600;width:99%'>" + sessionfollowuprequired + "</td></tr>";
                Message += " <tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Level of Interest</td><td style='font-weight:600;width:99%'> " + sessionlevelofinterest + "</td></tr>";
            }
            else if (ddllookupActivityType.SelectedValue == "4")
            {
                Message += " <tr><td style='width:1%;white-space:nowrap'> Project Number </td><td style='font-weight:600;width:99%'>" + specprojectno + "</td></tr>";
                Message += " <tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Project Name</td><td style='font-weight:600;width:99%'> " + specprojectname + "</td></tr>";
                Message += " <tr><td style='width:1%;white-space:nowrap'> Project Type </td><td style='font-weight:600;width:99%'>" + specprojecttype + "</td></tr>";
                Message += " <tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Project Stage</td><td style='font-weight:600;width:99%'> " + specprojectstage + "</td></tr>";
                Message += " <tr><td style='width:1%;white-space:nowrap'> CE Session </td><td style='font-weight:600;width:99%'>" + speccesession + "</td></tr>";
                Message += " <tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Consultant Name</td><td style='font-weight:600;width:99%'> " + specconsultantname + "</td></tr>";
                Message += " <tr><td style='width:1%;white-space:nowrap'> Dealer Name </td><td style='font-weight:600;width:99%'>" + specdealername + "</td></tr>";
                Message += " <tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Date Logged</td><td style='font-weight:600;width:99%'> " + formatspecdatelogged + "</td></tr>";
                Message += " <tr><td style='width:1%;white-space:nowrap'> Follow-up Date </td><td style='font-weight:600;width:99%'>" + formatspecfollowupdatelogged + "</td></tr>";
                Message += " <tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Estimated Value</td><td style='font-weight:600;width:99%'> " + estimatedvalue + "</td></tr>";
                Message += " <tr><td style='width:1%;white-space:nowrap'> App Drawings </td><td style='font-weight:600;width:99%'>" + specappdwgs + "</td></tr>";
                Message += " <tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Order Received</td><td style='font-weight:600;width:99%'> " + specorderreceived + "</td></tr>";
                Message += " <tr><td style='width:1%;white-space:nowrap'> Date Closed </td><td style='font-weight:600;width:99%'>" + formatspecdateclosed + "</td></tr>";
            }
            Message += " <tr><td colspan = '2'>Kindly provide your feedback, comments, or approval at your earliest convenience.. <br /><br /> ";
            Message += " Thanks, <br/ > <strong> " + salesManager + " </strong> <br /> ";
            Message += " </td></tr></table></td></tr></table></body></html> ";
            List<MailAddress> sendToList = new List<MailAddress>();
            List<MailAddress> ccList = new List<MailAddress>();
            sendToList.Add(new MailAddress("ruth@aero-werks.com", "Ruth"));
            ccList.Add(new MailAddress("rk83314@gmail.com", "Rohit Kumar"));
            string Subject = "Sales Activity - " + activityType;
            Send_Email(Message, Subject, sendToList, ccList);
            sendToList.Clear();
            ccList.Clear();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void Send_Email(String Message, String Subject, List<MailAddress> sendToList, List<MailAddress> ccList)
    {
        try
        {
            if (sendToList.Count > 0)
            {
                MailMessage message = new MailMessage(new MailAddress(Utility.Email(), Utility.EmailDisplayName()), sendToList[0]);
                string mailbody = Message;
                message.Subject = Subject;
                message.Body = mailbody;                

                foreach (var sendto in sendToList)
                {
                    if (!message.To.Contains(sendto))
                    {
                        message.To.Add(sendto);
                    }
                }
                foreach (var cc in ccList)
                {
                    if (!message.CC.Contains(cc))
                    {
                        message.CC.Add(cc);
                    }

                }
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                // SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp
                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["Host"], 587);
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromMail"], ConfigurationManager.AppSettings["Password"]);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicCredential1;
                client.Send(message);
                Message = string.Empty;
                Utility.ShowMessage_Success(Page, "Email Sent Successfully!!");
            }

        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}