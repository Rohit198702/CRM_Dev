using BLLAERO;
using BOLAERO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesManagement_FrmApprovedDwgAndFollowup : System.Web.UI.Page
{
    BOLApprovedDwgAndFollowup ObjBOL = new BOLApprovedDwgAndFollowup();
    BLLApprovedDwgAndFollowup ObjBLL = new BLLApprovedDwgAndFollowup();

    BOLManageDealerMember ObjBOLMember = new BOLManageDealerMember();
    BLLManageDealerMember ObjBLLMember = new BLLManageDealerMember();

    commonclass1 cls = new commonclass1();

    string formName = "ApprovedDwgAndFollowup";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetDates();
            BindControls();
            if (Utility.IsAuthorized())
            {
                txtLoginUser.Text = Utility.GetCurrentSession().EmployeeName;
            }
        }
    }

    private void SetDates()
    {
        try
        {
            txtFromDate.Text = DateTime.Now.Month + "/01/" + DateTime.Now.Year;
            txtToDate.Text = DateTime.Now.AddMonths(0).Month + "/" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(0).Month) + "/" + DateTime.Now.AddMonths(0).Year;
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
            ObjBOL.Operation = 8;
            ds = ObjBLL.Return_DataSet(ObjBOL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlProjectManager, ds.Tables[0]);
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlFollowupBy, ds.Tables[1]);
                if (Utility.IsAuthorized())
                {
                    if (ddlFollowupBy.Items.FindByValue(Utility.GetCurrentUser().ToString()) != null)
                    {
                        ddlFollowupBy.SelectedValue = Utility.GetCurrentUser().ToString();
                    }

                    ddlFollowupBy.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private bool ValidationCheck()
    {
        try
        {
            if (txtFromDate.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter From Date !");
                txtFromDate.Focus();
                return false;
            }

            //if (txtToDate.Text == "")
            //{
            //    Utility.ShowMessage_Error(Page, "Please enter To Date !");
            //    txtToDate.Focus();
            //    return false;
            //}
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    private bool ValidationCheckForCallHistory()
    {
        try
        {
            if (ddlFollowupBy.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Followup By !");
                ddlFollowupBy.Focus();
                modalForJob.Show();
                return false;
            }

            if (txtDateCalled.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter Date Called !");
                txtDateCalled.Focus();
                modalForJob.Show();
                return false;
            }

            //if (ddlContactName.SelectedIndex == 0)
            //{
            //    Utility.ShowMessage_Error(Page, "Please Select Contact Name !");
            //    ddlContactName.Focus();
            //    modalForJob.Show();
            //    return false;
            //}

            if (ddlCallHistoryStatus.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please Select Status !");
                ddlCallHistoryStatus.Focus();
                modalForJob.Show();
                return false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    private void Bind_Grid()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 1;
            ObjBOL.FromDate = Utility.ConvertDate(txtFromDate.Text);
            if (ddlProjectManager.SelectedIndex > 0)
            {
                ObjBOL.ProjectManagerId = Int32.Parse(ddlProjectManager.SelectedValue);
            }

            if (txtToDate.Text == "")
            {
                txtToDate.Text = DateTime.Now.ToShortDateString();
            }
            ObjBOL.ToDate = Utility.ConvertDate(txtToDate.Text);

            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvApprovedDwg.DataSource = ds.Tables[0];
                gvApprovedDwg.DataBind();
                ViewState["dirState"] = ds.Tables[0];
                lblRecordsCount.Text = "Total No. of Records:" + ds.Tables[0].Rows.Count.ToString() + ". Click on any row to add/view followups";
                lblRecordsCount.Visible = true;
                btnExportToExcel.Enabled = true;
            }
            else
            {
                gvApprovedDwg.DataSource = string.Empty;
                gvApprovedDwg.DataBind();
                ViewState["dirState"] = null;
                lblRecordsCount.Text = "No Record Found. Click on any row to add/view followups";
                lblRecordsCount.Visible = true;
                btnExportToExcel.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void SetTitle(string Title, string date, string projectManager)
    {
        try
        {
            lblJobIDTitleInModal.Text = Title;
            lblDwgAppDate.Text = date;
            lblProjectManager.Text = projectManager;
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

    private void LoadModal(string jobID)
    {
        try
        {
            if (jobID.Trim() != "")
            {
                hfJobIDTitleInModal.Value = jobID;
                BindBothModalGridViews(false);
                modalForJob.Show();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void BindBothModalGridViews(bool LoadCallHistoryOnly)
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 2;
            ObjBOL.JobId = hfJobIDTitleInModal.Value;
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (!LoadCallHistoryOnly)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvJobContacts.DataSource = ds.Tables[0];
                    gvJobContacts.DataBind();
                    //gvJobContacts.Rows[0].Visible = false;
                    EnableSaveFeature();
                }
                else
                {
                    gvJobContacts.DataSource = BindEmptyContact();
                    gvJobContacts.DataBind();
                    gvJobContacts.Rows[0].Visible = false;
                    DisableSaveFeature();
                }
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvCallHistory.DataSource = ds.Tables[1];
                gvCallHistory.DataBind();
            }
            else
            {
                gvCallHistory.DataSource = string.Empty;
                gvCallHistory.DataBind();
            }

            //BIND CONTACTNAME DROPDOWN
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlContactName, ds.Tables[0]);
                ddlContactName.SelectedIndex = 0;
            }
            else
            {
                ddlContactName.Items.Clear();
            }

            //BIND STATUS DROPDOWN
            if (ds.Tables[2].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlCallHistoryStatus, ds.Tables[2]);
                ddlCallHistoryStatus.SelectedIndex = 0;
            }
            else
            {
                ddlCallHistoryStatus.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void EnableSaveFeature()
    {
        try
        {
            btnSave.Enabled = true;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void DisableSaveFeature()
    {
        try
        {
            btnSave.Enabled = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private DataTable BindEmptyContact()
    {
        DataTable dtEmpty = new DataTable();
        try
        {
            dtEmpty.Columns.Add("ContactID", typeof(int));
            dtEmpty.Columns.Add("ContactName", typeof(string));
            dtEmpty.Columns.Add("FirstName", typeof(string));
            dtEmpty.Columns.Add("LastName", typeof(string));
            dtEmpty.Columns.Add("Title", typeof(string));
            dtEmpty.Columns.Add("Phone", typeof(string));
            dtEmpty.Columns.Add("Email", typeof(string));
            dtEmpty.Columns.Add("TimeStamp", typeof(bool));
            DataRow datatRow = dtEmpty.NewRow();
            dtEmpty.Rows.Add(datatRow);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dtEmpty;
    }


    protected void gvApprovedDwg_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Select")
            {
                GridViewRow clickedRow = gvApprovedDwg.Rows[Convert.ToInt32(e.CommandArgument)];
                Label lblJobID = (Label)clickedRow.FindControl("lblJobID");
                Label lblProjectName = (Label)clickedRow.FindControl("lblProjectName");
                Label lblPM = (Label)clickedRow.FindControl("lblProjectManager_Grid");
                Label lblDwgAppDate = (Label)clickedRow.FindControl("lblDwgAppDate_Grid");
                string var1 = lblJobID.Text;
                string var2 = lblProjectName.Text;
                string title = (var1 ?? "") + (var2 != "" ? ", " + var2 : "");
                LoadModal(lblJobID.Text);
                SetTitle(title, lblDwgAppDate.Text, lblPM.Text);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvApprovedDwg_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string lastCallDate = DataBinder.Eval(e.Row.DataItem, "LastCallDate") as string;

                if (!string.IsNullOrEmpty(lastCallDate))
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#d4edda");
                        cell.ForeColor = Color.Black;
                    }
                }
                else
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        cell.ForeColor = Color.Black;
                    }
                }

                e.Row.Attributes["onmouseover"] = "this.style.cursor='hand';this.style.cursor = 'Pointer'";
                e.Row.Attributes["onmouseout"] = "this.style.textDecoration='none';";
                e.Row.ToolTip = "Click to Add/View Call Logs";
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(this.gvApprovedDwg, "Select$" + e.Row.RowIndex);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvApprovedDwg_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable dtrslt = (DataTable)ViewState["dirState"];
            if (dtrslt.Rows.Count > 0)
            {
                DataView dataView = new DataView(dtrslt);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);
                gvApprovedDwg.DataSource = dataView;
                gvApprovedDwg.DataBind();
            }
            else
            {
                dtrslt.DefaultView.Sort = e.SortExpression + "DESC";
                gvApprovedDwg.DataSource = dtrslt;
                gvApprovedDwg.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        btnShow_Click();
    }

    private void btnShow_Click()
    {
        try
        {
            if (ValidationCheck())
            {
                Bind_Grid();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        try
        {
            SetDates();
            ddlProjectManager.SelectedIndex = 0;
            HfJObID.Value = "";
            gvApprovedDwg.DataSource = string.Empty;
            gvApprovedDwg.DataBind();
            gvExportToExcel.DataSource = string.Empty;
            gvExportToExcel.DataBind();
            lblRecordsCount.Text = string.Empty;
            lblRecordsCount.Visible = false;
            btnExportToExcel.Enabled = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 9;
            ObjBOL.FromDate = Utility.ConvertDate(txtFromDate.Text);
            if (ddlProjectManager.SelectedIndex > 0)
            {
                ObjBOL.ProjectManagerId = Int32.Parse(ddlProjectManager.SelectedValue);
            }

            if (txtToDate.Text == "")
            {
                txtToDate.Text = DateTime.Now.ToShortDateString();
            }
            ObjBOL.ToDate = Utility.ConvertDate(txtToDate.Text);

            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvExportToExcel.DataSource = ds.Tables[0];
                gvExportToExcel.DataBind();
                List<GridView> grids = new List<GridView>();
                List<string> gridNames = new List<string>();
                grids.Add(gvExportToExcel);
                gridNames.Add("");
                Utility.ExportGrids(grids, gridNames, Utility.ExportMode.SingleSheet, "Approved Dwg and Followup");
            }
            else
            {
                gvExportToExcel.DataSource = string.Empty;
                gvExportToExcel.DataBind();
                Utility.ShowMessage_Error(Page, "No Data Found!");
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }


    private int GetDealerId()
    {
        try
        {

            //hfJobIDTitleInModal.Value
            ObjBOL.Operation = 7;
            ObjBOL.JobId = hfJobIDTitleInModal.Value;
            string dealerId = ObjBLL.Return_String(ObjBOL);
            int returnValue = 0;
            Int32.TryParse(dealerId, out returnValue);
            return returnValue;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return 0;
    }

    private void ResetCallHistoryForm()
    {
        try
        {
            txtDateCalled.Text = string.Empty;
            txtContact.Text = string.Empty;
            txtNotes.Text = string.Empty;
            hfDetailID.Value = string.Empty;
            if (ddlContactName.Items.Count > 0)
            {
                ddlContactName.SelectedIndex = 0;
            }

            if (ddlFollowupBy.Items.Count > 0)
            {
                if (Utility.IsAuthorized())
                {
                    if (ddlFollowupBy.Items.FindByValue(Utility.GetCurrentUser().ToString()) != null)
                    {
                        ddlFollowupBy.SelectedValue = Utility.GetCurrentUser().ToString();
                    }
                    else
                    {
                        ddlFollowupBy.SelectedIndex = 0;
                    }
                }
            }

            if (ddlCallHistoryStatus.Items.Count > 0)
            {
                ddlCallHistoryStatus.SelectedIndex = 0;
            }

            chkNotifyThePM.Checked = false;
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetCallHistoryModal()
    {
        try
        {
            ResetCallHistoryForm();
            gvCallHistory.DataSource = string.Empty;
            gvCallHistory.DataBind();
            gvJobContacts.DataSource = string.Empty;
            gvJobContacts.DataBind();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvJobContacts_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Insert")
            {
                TextBox FooterFirstName = gvJobContacts.FooterRow.FindControl("txtFooterFirstName") as TextBox;
                TextBox FooterLastName = gvJobContacts.FooterRow.FindControl("txtFooterLastName") as TextBox;
                TextBox FooterTitle = gvJobContacts.FooterRow.FindControl("txtFooterTitle") as TextBox;
                TextBox FooterPhone = gvJobContacts.FooterRow.FindControl("txtFooterPhone") as TextBox;
                TextBox FooterEmail = gvJobContacts.FooterRow.FindControl("txtFooterEmail") as TextBox;

                if (FooterTitle.Text.Trim() == "")
                {
                    Utility.ShowMessage_Error(Page, "Please enter Position!");
                    FooterTitle.Focus();
                    modalForJob.Show();
                    return;
                }

                if (FooterFirstName.Text.Trim() == "")
                {
                    Utility.ShowMessage_Error(Page, "Please enter First Name!");
                    FooterFirstName.Focus();
                    modalForJob.Show();
                    return;
                }
                ObjBOLMember.Operation = 4;
                ObjBOLMember.DealerID = (short)GetDealerId();
                ObjBOLMember.Title = FooterTitle.Text;
                ObjBOLMember.FName = FooterFirstName.Text;
                ObjBOLMember.LName = FooterLastName.Text;
                ObjBOLMember.Phone = FooterPhone.Text;
                ObjBOLMember.email = FooterEmail.Text;

                string returnValue = ObjBLLMember.SaveDealerMember(ObjBOLMember);
                if (returnValue.Trim() == "ER01")
                {
                    Utility.ShowMessage_Success(Page, "Contact name already exists");
                    LoadModal(hfJobIDTitleInModal.Value.Trim());
                    return;
                }

                if (returnValue.Trim() != "")
                {
                    Utility.ShowMessage_Success(Page, returnValue);
                    Utility.MaintainLogsSpecial(formName, "Save-Contact", hfJobIDTitleInModal.Value.Trim());
                    ResetCallHistoryModal();
                    LoadModal(hfJobIDTitleInModal.Value.Trim());
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvJobContacts_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int ContactID = Convert.ToInt32(gvJobContacts.DataKeys[e.RowIndex].Values[0]);
            string TimeStamp = Convert.ToString(gvJobContacts.DataKeys[e.RowIndex].Values[1]);
            ObjBOLMember.Operation = 5;
            ObjBOLMember.ContactID = ContactID;
            ObjBOLMember.DealerID = (short)GetDealerId();
            string returnValue = "";
            returnValue = ObjBLLMember.DeleteDealerMember(ObjBOLMember);
            if (returnValue.Trim() != "")
            {
                if (returnValue.Trim() == "1")
                {
                    Utility.ShowMessage_Error(Page, "Contact cannot be deleted with existing call logs");
                    modalForJob.Show();
                }
                else
                {
                    Utility.ShowMessage_Success(Page, returnValue);
                    Utility.MaintainLogsSpecial(formName, "delete-Contact", hfJobIDTitleInModal.Value.Trim());
                    ResetCallHistoryModal();
                    LoadModal(hfJobIDTitleInModal.Value.Trim());
                }

            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvJobContacts_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvJobContacts.EditIndex = e.NewEditIndex;
            LoadModal(hfJobIDTitleInModal.Value.Trim());
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvJobContacts_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow row = gvJobContacts.Rows[e.RowIndex];
            ObjBOLMember.Operation = 6;
            ObjBOLMember.ContactID = Convert.ToInt32(gvJobContacts.DataKeys[e.RowIndex].Values[0]);
            string TimeStamp = Convert.ToString(gvJobContacts.DataKeys[e.RowIndex].Values[1]);
            if (TimeStamp != "")
            {
                ObjBOLMember.Timestamp = long.Parse(TimeStamp);
            }
            TextBox txtTitle = (row.FindControl("txtTitle") as TextBox);
            ObjBOLMember.Title = txtTitle.Text;
            if (txtTitle.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter Title. !");
                txtTitle.Focus();
                modalForJob.Show();
                return;
            }

            TextBox txtFName = (row.FindControl("txtFirstName") as TextBox);
            if (txtFName.Text == "")
            {
                Utility.ShowMessage_Error(Page, "Please Enter First Name. !");
                txtFName.Focus();
                modalForJob.Show();
                return;
            }
            ObjBOLMember.FName = txtFName.Text;
            ObjBOLMember.LName = (row.FindControl("txtLastName") as TextBox).Text;
            ObjBOLMember.Phone = (row.FindControl("txtPhone") as TextBox).Text;
            ObjBOLMember.email = (row.FindControl("txtEmail") as TextBox).Text;

            string returnValue = "";
            returnValue = ObjBLLMember.SaveDealerMember(ObjBOLMember);
            gvJobContacts.EditIndex = -1;
            if (returnValue.Trim() == "ER01")
            {
                Utility.ShowMessage_Success(Page, "Contact name already exists");
                LoadModal(hfJobIDTitleInModal.Value.Trim());
                return;
            }

            if (returnValue.Trim() == "ER02")
            {
                Utility.ShowMessage_Error(Page, Utility.ConcurrencyErrorMessage());
                LoadModal(hfJobIDTitleInModal.Value.Trim());
                return;
            }
            if (returnValue.Trim() != "")
            {
                Utility.ShowMessage_Success(Page, returnValue);
                Utility.MaintainLogsSpecial(formName, "update-Contact", hfJobIDTitleInModal.Value.Trim());
                ResetCallHistoryModal();
                LoadModal(hfJobIDTitleInModal.Value.Trim());
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvJobContacts_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvJobContacts.EditIndex = -1;
            LoadModal(hfJobIDTitleInModal.Value.Trim());
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }


    protected void btnClose_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ResetCallHistoryModal();
            modalForJob.Hide();
            btnShow_Click();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnAddContactRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            int dealerId = GetDealerId();
            if (dealerId > 0)
            {
                Session["Dealer"] = dealerId;
                Response.Redirect("~/ContactManagement/FrmDealers.aspx", false);
            }
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
            if (ValidationCheckForCallHistory())
            {
                ObjBOL.JobId = hfJobIDTitleInModal.Value.Trim();
                ObjBOL.DateCalled = Utility.ConvertDate(txtDateCalled.Text);
                if (ddlContactName.SelectedIndex > 0)
                {
                    ObjBOL.ContactID = Int32.Parse(ddlContactName.SelectedValue);
                    ObjBOL.Contact = ddlContactName.SelectedItem.Text;
                }

                if (ddlFollowupBy.SelectedIndex > 0)
                {
                    ObjBOL.FollowupBy = Int32.Parse(ddlFollowupBy.SelectedValue);
                }

                if (ddlCallHistoryStatus.SelectedIndex > 0)
                {
                    ObjBOL.StatusId = Int32.Parse(ddlCallHistoryStatus.SelectedValue);
                }
                ObjBOL.Notes = txtNotes.Text;
                if (Utility.IsAuthorized())
                {
                    ObjBOL.LoginUserId = Utility.GetCurrentUser();
                }

                if (btnSave.Text == "Save")
                {
                    ObjBOL.Operation = 3;
                }
                else if (btnSave.Text == "Update")
                {
                    ObjBOL.Operation = 6;
                    if (hfTimeStampCH.Value != "-1")
                    {
                        ObjBOL.TimeStampCH = long.Parse(hfTimeStampCH.Value);
                    }
                    ObjBOL.ID = Int32.Parse(hfDetailID.Value);
                }
                string returnValue = ObjBLL.Return_String(ObjBOL);
                if (returnValue.Trim() == "erts")
                {
                    Utility.ShowMessage_Error(Page, Utility.ConcurrencyErrorMessage());
                    LoadModal(hfJobIDTitleInModal.Value.Trim());
                    return;
                }
                if (returnValue.Trim() != "")
                {
                    Utility.ShowMessage_Success(Page, returnValue);
                    Utility.MaintainLogsSpecial(formName, btnSave.Text, hfJobIDTitleInModal.Value.Trim());

                    bool notifyPM = chkNotifyThePM.Checked;
                    if (notifyPM)
                    {
                        NotifyPM_EmailEntry();
                    }
                    ResetCallHistoryModal();
                    LoadModal(hfJobIDTitleInModal.Value.Trim());
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetContactFooter()
    {
        try
        {
            TextBox FooterTitle = gvJobContacts.FooterRow.FindControl("txtFooterTitle") as TextBox;
            TextBox FooterFirstName = gvJobContacts.FooterRow.FindControl("txtFooterFirstName") as TextBox;
            TextBox FooterLastName = gvJobContacts.FooterRow.FindControl("txtFooterLastName") as TextBox;
            TextBox FooterPhone = gvJobContacts.FooterRow.FindControl("txtFooterPhone") as TextBox;
            TextBox FooterEmail = gvJobContacts.FooterRow.FindControl("txtFooterEmail") as TextBox;

            FooterTitle.Text = string.Empty;
            FooterFirstName.Text = string.Empty;
            FooterLastName.Text = string.Empty;
            FooterPhone.Text = string.Empty;
            FooterEmail.Text = string.Empty;
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
            ResetCallHistoryForm();
            ResetContactFooter();
            modalForJob.Show();
            hfTimeStampCH.Value = "-1";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvCallHistory_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(gvCallHistory.DataKeys[e.RowIndex].Values[0]);
            string TimeStampCH = Convert.ToString(gvCallHistory.DataKeys[e.RowIndex].Values[1]);
            ObjBOL.Operation = 4;
            ObjBOL.ID = ID;
            if (TimeStampCH != "")
            {
                ObjBOL.TimeStampCH = long.Parse(TimeStampCH);
            }
            string returnValue = ObjBLL.Return_String(ObjBOL);
            if (returnValue.Trim() == "erts")
            {
                Utility.ShowMessage_Error(Page, Utility.ConcurrencyErrorMessage());
                LoadModal(hfJobIDTitleInModal.Value.Trim());
                return;
            }
            if (returnValue.Trim() != "")
            {
                Utility.ShowMessage_Success(Page, returnValue);
                Utility.MaintainLogsSpecial(formName, "Delete-call", hfJobIDTitleInModal.Value.Trim());
                LoadModal(hfJobIDTitleInModal.Value.Trim());
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvCallHistory_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            modalForJob.Show();
            BindBothModalGridViews(true);
            DataSet ds = new DataSet();
            ObjBOL.Operation = 5;
            ObjBOL.ID = Int32.Parse(gvCallHistory.DataKeys[e.NewEditIndex].Values[0].ToString());
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                hfDetailID.Value = ds.Tables[0].Rows[0]["ID"].ToString();
                txtDateCalled.Text = ds.Tables[0].Rows[0]["DateCalled"].ToString();
                txtContact.Text = ds.Tables[0].Rows[0]["Contact"].ToString();
                if (ddlContactName.Items.FindByValue(ds.Tables[0].Rows[0]["ContactID"].ToString()) != null)
                {
                    ddlContactName.SelectedValue = ds.Tables[0].Rows[0]["ContactID"].ToString();
                }
                else
                {
                    if (ddlContactName.Items.Count > 0)
                    {
                        ddlContactName.SelectedIndex = 0;
                    }
                }

                if (ddlCallHistoryStatus.Items.FindByValue(ds.Tables[0].Rows[0]["StatusId"].ToString()) != null)
                {
                    ddlCallHistoryStatus.SelectedValue = ds.Tables[0].Rows[0]["StatusId"].ToString();
                }
                else
                {
                    if (ddlCallHistoryStatus.Items.Count > 0)
                    {
                        ddlCallHistoryStatus.SelectedIndex = 0;
                    }
                }

                if (ddlFollowupBy.Items.FindByValue(ds.Tables[0].Rows[0]["FollowupBy"].ToString()) != null)
                {
                    ddlFollowupBy.SelectedValue = ds.Tables[0].Rows[0]["FollowupBy"].ToString();
                }
                else
                {
                    if (ddlFollowupBy.Items.Count > 0)
                    {
                        ddlFollowupBy.SelectedIndex = 0;
                    }
                }

                txtNotes.Text = ds.Tables[0].Rows[0]["Notes"].ToString();
                hfTimeStampCH.Value = ds.Tables[0].Rows[0]["TimeStamp"].ToString();
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered (used for export to excel)*/
    }

    protected void btnReportRedirect_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Reports/FrmApprovedDwgAndFollowupReport.aspx", false);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvExportToExcel_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string lastCallDate = DataBinder.Eval(e.Row.DataItem, "Date Called") as string;

                if (!string.IsNullOrEmpty(lastCallDate))
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        cell.BackColor = ColorTranslator.FromHtml("#d4edda");
                        cell.ForeColor = Color.Black;
                    }
                }
                else
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        cell.ForeColor = Color.Black;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void NotifyPM_EmailEntry()
    {
        try
        {
            if (true)
            {
                List<MailAddress> sendList = new List<MailAddress>();
                List<MailAddress> ccList = new List<MailAddress>();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                ObjBOL.Operation = 10;
                ObjBOL.JobId = hfJobIDTitleInModal.Value.Trim();
                if (ddlFollowupBy.SelectedIndex > 0)
                {
                    ObjBOL.FollowupBy = Int32.Parse(ddlFollowupBy.SelectedValue);
                }
                ds = ObjBLL.Return_DataSet(ObjBOL);
                dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    string email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null;
                    string name = row["DisplayName"] != DBNull.Value ? row["DisplayName"].ToString() : null;

                    if (!string.IsNullOrEmpty(email))
                    {
                        sendList.Add(new MailAddress(email, name));
                    }
                }

                dt.Clear();
                dt = ds.Tables[1];
                foreach (DataRow row in dt.Rows)
                {
                    string email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null;
                    string name = row["DisplayName"] != DBNull.Value ? row["DisplayName"].ToString() : null;

                    if (!string.IsNullOrEmpty(email))
                    {
                        ccList.Add(new MailAddress(email, name));
                    }
                }
                string bdmEmail = ds.Tables[2].Rows[0][0].ToString();
                NotifyPM_PrepareEmailAndSend(sendList, ccList, bdmEmail);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void NotifyPM_PrepareEmailAndSend(List<MailAddress> sendList, List<MailAddress> ccList, string bdmEmail)
    {
        try
        {
            if (true)
            {
                string Do_Not_Reply = "[Please do not reply to this message. Replies to this message are routed to an unmonitored mailbox]";
                string Subject = lblJobIDTitleInModal.Text;
                string concernedPerson = "the concerned person";
                string Message = string.Empty;
                Message += "<!doctype html><html lang='en'><head><meta charset='utf-8'><meta name='viewport' content='width=device-width, initial-scale=1'>";
                Message += "<title>Approved Dwg and Followup</title></head>";
                Message += "<body>";
                Message += "<table cellpadding='0' cellspacing='0' style='border-collapse:collapse;width:100%;font-family:Calibri;font-size:1.15rem'>";
                Message += "<tr><td>";
                Message += "<table border='1' cellpadding='8' cellspacing='0' style='border-collapse:collapse;width:100%;max-width:580px;margin:0 auto;border-color:#ddd'>";

                Message += "<tr><td colspan='2'>";
                Message += "<h2 style='margin:0;font-size:1.15rem'>Hello,</h2>";
                Message += "<p style='margin-top:5px'>You will find the details of the <b>Approved Dwg and Followup</b> below.</p>";
                Message += "</td></tr>";

                Message += "<tr><td colspan='2'>";
                Message += "<h1 style='font-size:1.65rem;margin:.3rem 0 0;color:#000;text-align:center'>Approved Dwg Followups</h1>";
                Message += "</td></tr>";

                if (ddlFollowupBy.SelectedIndex > 0)
                {
                    Message += "<tr><td style='width:1%;white-space:nowrap'>Followup By</td><td style='font-weight:600;width:99%'>" + ddlFollowupBy.SelectedItem.Text;
                    concernedPerson = ddlFollowupBy.SelectedItem.Text;
                    if (bdmEmail.Trim() != "")
                    {
                        concernedPerson += " at " + bdmEmail;
                    }
                    Message += "</td></tr>";

                }

                if (txtDateCalled.Text.Trim() != "")
                {
                    Message += "<tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Date Called</td><td style='font-weight:600;width:99%'>" + txtDateCalled.Text + "</td></tr>";
                }

                if (ddlCallHistoryStatus.SelectedIndex > 0)
                {
                    Message += "<tr><td style='width:1%;white-space:nowrap'>Status</td><td style='font-weight:600;width:99%'>" + ddlCallHistoryStatus.SelectedItem.Text + "</td></tr>";
                }

                if (txtNotes.Text.Trim() != "")
                {
                    Message += "<tr style='background:#efefef'><td style='width:1%;white-space:nowrap'>Notes</td><td style='font-weight:600;width:99%'>" + System.Web.HttpUtility.HtmlEncode(txtNotes.Text) + "</td></tr>";
                }

                Message += "<tr><td colspan='2'>";

                Message += "If you have any questions or concerns regarding the above information, please contact " + concernedPerson + ".<br /><br />";
                Message += "Thanks<br />";
                //Message += "<strong>" +  + "</strong><br />";
                Message += "</td></tr>";

                Message += "<tr><td colspan='2' style='color:red'>" + Do_Not_Reply + "</td></tr>";

                Message += "</table>";
                Message += "</td></tr>";
                Message += "</table>";
                Message += "</body></html>";

                NotifyPM_SendEmail(Message, Subject, sendList, ccList);
                sendList.Clear();
                ccList.Clear();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void NotifyPM_SendEmail(String Message, String Subject, List<MailAddress> sendToList, List<MailAddress> ccList)
    {
        try
        {
            if (sendToList.Count > 0)
            {
                MailMessage message = new MailMessage(new MailAddress(Utility.Email(), "CRM"), sendToList[0]);
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

                System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["Host"], 587);
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential(ConfigurationManager.AppSettings["FromMail"], ConfigurationManager.AppSettings["Password"]);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicCredential1;
                client.Send(message);
                Message = string.Empty;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}