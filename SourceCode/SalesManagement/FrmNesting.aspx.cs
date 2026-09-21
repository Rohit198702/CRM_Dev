using BLLAERO;
using BOLAERO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesManagement_FrmNesting : System.Web.UI.Page
{
    string formName = "FrmNesting.aspx";
    BOLProjectsFabricationAndNestingTasks ObjBOL_FabricationAndNestingTasks = new BOLProjectsFabricationAndNestingTasks();
    BLLProjectsFabricationAndNestingTasks_V1 ObjBLL_FabricationAndNestingTasks = new BLLProjectsFabricationAndNestingTasks_V1();

    BOLManageITWProjects ObjBOL = new BOLManageITWProjects();
    BLLManageITWProjects ObjBLL = new BLLManageITWProjects();

    BOLGaylordProjects ObjBOL_Gaylord = new BOLGaylordProjects();
    BLLGaylordProjects ObjBLL_Gaylord = new BLLGaylordProjects();
    commonclass1 cls = new commonclass1();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResetNesting_Aero();
            BindControls();
            ddlNestingFor_SelectedIndexChanged();
        }
    }

    private void BindControls()
    {
        try
        {
            DataTable dt = new DataTable();
            cls.Return_DT(dt, "EXEC Get_NestingReport 1");
            if (dt.Rows.Count > 0)
            {
                Utility.BindDropDownListWithoutFiller(ddlNestingFor, dt);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void txtSearchJobID_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtSearchJobID.Text != "")
            {
                SyncTextbox("NAME", txtSearchJobID.Text);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void txtSearchJobID_TextChanged_Event()
    {
        try
        {
            if (txtSearchJobID.Text.Trim() != "")
            {
                ObjBOL_FabricationAndNestingTasks.Operation = 14;
                ObjBOL_FabricationAndNestingTasks.JobId = txtSearchJobID.Text;
                DataSet ds = ObjBLL_FabricationAndNestingTasks.Return_DataSet(ObjBOL_FabricationAndNestingTasks);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    GetNestingTasks();
                }
                else
                {
                    txtSearchJobID.Text = string.Empty;
                    ResetNestingTaskGrid();
                }
            }
            else
            {
                txtSearchJobID.Text = string.Empty;
                ResetNesting_Aero();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private DataSet BindNestingGridControls_Data()
    {
        DataSet ds = new DataSet();
        try
        {
            ObjBOL_FabricationAndNestingTasks.Operation = 8;
            ds = ObjBLL_FabricationAndNestingTasks.Return_DataSet(ObjBOL_FabricationAndNestingTasks);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return ds;
    }

    private void BindNestingGridControls()
    {
        try
        {
            DataSet ds = BindNestingGridControls_Data();
            if (ds.Tables[0].Rows.Count > 0)
            {
                DropDownList ddlTaskType_FooterNesting = (DropDownList)gvNestingTasks.FooterRow.FindControl("ddlTaskType_FooterNesting");
                Utility.BindDropDownList(ddlTaskType_FooterNesting, ds.Tables[0]);
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                DropDownList ddlProjectEngineer_FooterNesting = (DropDownList)gvNestingTasks.FooterRow.FindControl("ddlProjectEngineer_FooterNesting");
                Utility.BindDropDownList(ddlProjectEngineer_FooterNesting, ds.Tables[1]);
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                DropDownList ddlJobID_FooterNesting = (DropDownList)gvNestingTasks.FooterRow.FindControl("ddlJobID_FooterNesting");
                Utility.BindDropDownList(ddlJobID_FooterNesting, ds.Tables[2]);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void BindNestingGridControls(DropDownList ddl1, DropDownList ddl2)
    {
        try
        {
            DataSet ds = BindNestingGridControls_Data();
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddl1, ds.Tables[0]);
            }
            else
            {
                ddl1.Items.Clear();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddl2, ds.Tables[1]);
            }
            else
            {
                ddl2.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private DataTable EmptyDT_NestingTasks()
    {
        DataTable dtEmpty = new DataTable();
        try
        {
            dtEmpty.Columns.Add("Id", typeof(int));
            dtEmpty.Columns.Add("TaskNumber", typeof(string));
            dtEmpty.Columns.Add("NatureOfTask", typeof(string));
            dtEmpty.Columns.Add("AssignedFrom", typeof(string));
            dtEmpty.Columns.Add("TaskType", typeof(int));
            dtEmpty.Columns.Add("ProjectEngineer", typeof(int));
            dtEmpty.Columns.Add("StartDate", typeof(DateTime));
            dtEmpty.Columns.Add("EndDate", typeof(DateTime));
            dtEmpty.Columns.Add("SentDate", typeof(DateTime));
            dtEmpty.Columns.Add("Status", typeof(int));
            dtEmpty.Columns.Add("SentToProduction", typeof(string));

            DataRow datatRow = dtEmpty.NewRow();
            dtEmpty.Rows.Add(datatRow);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dtEmpty;
    }

    private void ResetAerowerksNesting()
    {
        try
        {
            ResetNesting_Aero();
            txtSearchJobID.Text = string.Empty;
            txtSearchPName.Text = string.Empty;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetITWNesting()
    {
        btnCancel_Click();
    }

    private void ResetCaddy()
    {
        try
        {
            gvCaddyNestingTasks.DataSource = string.Empty;
            gvCaddyNestingTasks.DataBind();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetNesting_Aero()
    {
        try
        {
            gvNestingTasks.DataSource = string.Empty;
            gvNestingTasks.DataBind();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ResetNestingTaskGrid()
    {
        try
        {
            gvNestingTasks.DataSource = EmptyDT_NestingTasks();
            gvNestingTasks.DataBind();
            gvNestingTasks.Rows[0].Visible = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void GetNestingTasks()
    {
        try
        {
            ObjBOL_FabricationAndNestingTasks.Operation = 7;
            ObjBOL_FabricationAndNestingTasks.JobId = txtSearchJobID.Text;
            DataSet ds = ObjBLL_FabricationAndNestingTasks.Return_DataSet(ObjBOL_FabricationAndNestingTasks);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvNestingTasks.DataSource = ds.Tables[0];
                gvNestingTasks.DataBind();
            }
            else
            {
                ResetNestingTaskGrid();
            }
            BindNestingGridControls();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvNestingTasks_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int id = Int32.Parse(gvNestingTasks.DataKeys[e.RowIndex].Values[0].ToString());

            ObjBOL_FabricationAndNestingTasks.Operation = 13;
            ObjBOL_FabricationAndNestingTasks.ID = id;
            string returnStatus = ObjBLL_FabricationAndNestingTasks.Return_String(ObjBOL_FabricationAndNestingTasks);

            if (returnStatus.Trim() == "ER01")
            {
                Utility.ShowMessage_Error(Page, "Task already sent to Production !!");
                return;
            }

            if (returnStatus.Trim() == "ER02")
            {
                Utility.ShowMessage_Error(Page, "Task forwarded from Fabrication. Cannot be deleted !!");
                return;
            }

            if (returnStatus.Trim() != "")
            {
                Utility.ShowMessage_Success(Page, "Task Deleted Successfully !!");
                Utility.MaintainLogsSpecial(formName, "Delete " + ddlNestingFor.SelectedValue, returnStatus);
            }

            //GetNestingTasks();
            GetAllAerowerksTasks();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvNestingTasks_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvNestingTasks.EditIndex = e.NewEditIndex;
            //GetNestingTasks();
            GetAllAerowerksTasks();

            DataSet ds = new DataSet();
            int id = Int32.Parse(gvNestingTasks.DataKeys[e.NewEditIndex].Values[0].ToString());

            ObjBOL_FabricationAndNestingTasks.Operation = 9;
            ObjBOL_FabricationAndNestingTasks.ID = id;
            ds = ObjBLL_FabricationAndNestingTasks.Return_DataSet(ObjBOL_FabricationAndNestingTasks);
            if (ds.Tables[0].Rows.Count > 0)
            {

                DataRow row = ds.Tables[0].Rows[0];
                //if (Boolean.Parse(row["SentToProduction"].ToString()))
                //{
                //    Utility.ShowMessage_Error(Page, "Task already sent to Production !!");
                //    gvNestingTasks.EditIndex = -1;
                //    GetNestingTasks();
                //    return;
                //}

                Label lblTaskNumber_EditNesting = gvNestingTasks.Rows[e.NewEditIndex].FindControl("lblTaskNumber_EditNesting") as Label;
                lblTaskNumber_EditNesting.Text = row["TaskNumber"].ToString();


                DropDownList ddlNatureOfTask_EditNesting = gvNestingTasks.Rows[e.NewEditIndex].FindControl("ddlNatureOfTask_EditNesting") as DropDownList;
                if (ddlNatureOfTask_EditNesting.Items.FindByValue(row["NatureOfTask"].ToString()) != null)
                {
                    ddlNatureOfTask_EditNesting.SelectedValue = row["NatureOfTask"].ToString();
                }
                else
                {
                    if (ddlNatureOfTask_EditNesting.Items.Count > 0)
                    {
                        ddlNatureOfTask_EditNesting.SelectedIndex = 0;
                    }
                }

                DropDownList ddlAssignedFrom_EditNesting = gvNestingTasks.Rows[e.NewEditIndex].FindControl("ddlAssignedFrom_EditNesting") as DropDownList;
                if (ddlAssignedFrom_EditNesting.Items.FindByValue(row["AssignedFrom"].ToString()) != null)
                {
                    ddlAssignedFrom_EditNesting.SelectedValue = row["AssignedFrom"].ToString();
                }
                else
                {
                    if (ddlAssignedFrom_EditNesting.Items.Count > 0)
                    {
                        ddlAssignedFrom_EditNesting.SelectedIndex = 0;
                    }
                }

                //if (row["AssignedFrom"].ToString() == "F")
                //{
                ddlAssignedFrom_EditNesting.Enabled = false;
                ddlNatureOfTask_EditNesting.Enabled = false;
                //}
                //else
                //{
                //    ddlAssignedFrom_EditNesting.Enabled = true;
                //    ddlNatureOfTask_EditNesting.Enabled = true;
                //}

                DropDownList ddlTaskType_EditNesting = gvNestingTasks.Rows[e.NewEditIndex].FindControl("ddlTaskType_EditNesting") as DropDownList;
                DropDownList ddlProjectEngineer_EditNesting = gvNestingTasks.Rows[e.NewEditIndex].FindControl("ddlProjectEngineer_EditNesting") as DropDownList;

                BindNestingGridControls(ddlTaskType_EditNesting, ddlProjectEngineer_EditNesting);

                if (ddlTaskType_EditNesting.Items.FindByValue(row["TaskType"].ToString()) != null)
                {
                    ddlTaskType_EditNesting.SelectedValue = row["TaskType"].ToString();
                }
                else
                {
                    if (ddlTaskType_EditNesting.Items.Count > 0)
                    {
                        ddlTaskType_EditNesting.SelectedIndex = 0;
                    }
                }

                if (ddlProjectEngineer_EditNesting.Items.FindByValue(row["ProjectEngineer"].ToString()) != null)
                {
                    ddlProjectEngineer_EditNesting.SelectedValue = row["ProjectEngineer"].ToString();
                }
                else
                {
                    if (ddlProjectEngineer_EditNesting.Items.Count > 0)
                    {
                        ddlProjectEngineer_EditNesting.SelectedIndex = 0;
                    }
                }

                TextBox txtStartDate_EditNesting = gvNestingTasks.Rows[e.NewEditIndex].FindControl("txtStartDate_EditNesting") as TextBox;
                txtStartDate_EditNesting.Text = row["StartDate"].ToString();

                TextBox txtEndDate_EditNesting = gvNestingTasks.Rows[e.NewEditIndex].FindControl("txtEndDate_EditNesting") as TextBox;
                txtEndDate_EditNesting.Text = row["EndDate"].ToString();

                TextBox txtSentDate_EditNesting = gvNestingTasks.Rows[e.NewEditIndex].FindControl("txtSentDate_EditNesting") as TextBox;
                txtSentDate_EditNesting.Text = row["SentDate"].ToString();

                DropDownList ddlStatus_EditNesting = gvNestingTasks.Rows[e.NewEditIndex].FindControl("ddlStatus_EditNesting") as DropDownList;
                if (ddlStatus_EditNesting.Items.FindByValue(row["Status"].ToString()) != null)
                {
                    ddlStatus_EditNesting.SelectedValue = row["Status"].ToString();
                }
                else
                {
                    if (ddlStatus_EditNesting.Items.Count > 0)
                    {
                        ddlStatus_EditNesting.SelectedIndex = 0;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvNestingTasks_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Send")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int id = Int32.Parse(gvNestingTasks.DataKeys[rowIndex].Values[0].ToString());

                ObjBOL_FabricationAndNestingTasks.Operation = 12;
                ObjBOL_FabricationAndNestingTasks.ID = id;
                ObjBOL_FabricationAndNestingTasks.AssistedBy = Utility.GetCurrentUser();
                string returnStatus = ObjBLL_FabricationAndNestingTasks.Return_String(ObjBOL_FabricationAndNestingTasks);

                if (returnStatus.Trim() == "ER01")
                {
                    Utility.ShowMessage_Error(Page, "Task already sent to Production !!");
                    return;
                }

                if (returnStatus.Trim() == "ER02")
                {
                    Utility.ShowMessage_Error(Page, "Please enter Sent Date !!");
                    return;
                }

                if (returnStatus.Trim() == "ER03")
                {
                    Utility.ShowMessage_Error(Page, "Not Authorized !!");
                    return;
                }

                if (returnStatus.Trim() != "")
                {
                    Utility.ShowMessage_Success(Page, "Task Sent to Production Successfully !!");
                    Utility.MaintainLogsSpecial(formName, "Sent " + ddlNestingFor.SelectedValue, id.ToString());
                    //GetNestingTasks();
                    GetAllAerowerksTasks();
                }
            }

            else if (e.CommandName == "Insert")
            {
                GridViewRow row = gvNestingTasks.FooterRow;

                ObjBOL_FabricationAndNestingTasks.Operation = 11;
                DropDownList ddlJobID_FooterNesting = row.FindControl("ddlJobID_FooterNesting") as DropDownList;
                DropDownList ddlNatureOfTask_FooterNesting = row.FindControl("ddlNatureOfTask_FooterNesting") as DropDownList;
                DropDownList ddlAssignedFrom_FooterNesting = row.FindControl("ddlAssignedFrom_FooterNesting") as DropDownList;

                DropDownList ddlTaskType_FooterNesting = row.FindControl("ddlTaskType_FooterNesting") as DropDownList;
                DropDownList ddlProjectEngineer_FooterNesting = row.FindControl("ddlProjectEngineer_FooterNesting") as DropDownList;
                DropDownList ddlStatus_FooterNesting = row.FindControl("ddlStatus_FooterNesting") as DropDownList;

                if (ddlJobID_FooterNesting.SelectedIndex == 0)
                {
                    Utility.ShowMessage_Error(Page, "Please select Job !");
                    return;
                }

                //if (txtSearchJobID.Text.Length < 6)
                //{
                //    Utility.ShowMessage_Error(Page, "Please select Job !");
                //    return;
                //}

                if (ddlNatureOfTask_FooterNesting.SelectedIndex == 0)
                {
                    Utility.ShowMessage_Error(Page, "Please select Nature of Task !");
                    return;
                }

                if (ddlAssignedFrom_FooterNesting.SelectedIndex == 0)
                {
                    Utility.ShowMessage_Error(Page, "Please select Assigned From !");
                    return;
                }

                //ObjBOL_FabricationAndNestingTasks.TaskNumber = (row.FindControl("lblTaskNumber_FooterNesting") as Label).Text;
                //ObjBOL_FabricationAndNestingTasks.JobId = txtSearchJobID.Text;
                ObjBOL_FabricationAndNestingTasks.JobId = ddlJobID_FooterNesting.SelectedValue;
                ObjBOL_FabricationAndNestingTasks.NatureOfTask = ddlNatureOfTask_FooterNesting.SelectedValue;
                ObjBOL_FabricationAndNestingTasks.AssignedFrom = ddlAssignedFrom_FooterNesting.SelectedValue;
                if (ddlTaskType_FooterNesting.SelectedIndex > 0)
                {
                    ObjBOL_FabricationAndNestingTasks.TaskType = Int32.Parse(ddlTaskType_FooterNesting.SelectedValue);
                }

                if (ddlProjectEngineer_FooterNesting.SelectedIndex > 0)
                {
                    ObjBOL_FabricationAndNestingTasks.ProjectEngineer = Int32.Parse(ddlProjectEngineer_FooterNesting.SelectedValue);
                }

                ObjBOL_FabricationAndNestingTasks.StartDate = Utility.ConvertDate((row.FindControl("txtStartDate_FooterNesting") as TextBox).Text);
                ObjBOL_FabricationAndNestingTasks.EndDate = Utility.ConvertDate((row.FindControl("txtEndDate_FooterNesting") as TextBox).Text);
                ObjBOL_FabricationAndNestingTasks.SentDate = Utility.ConvertDate((row.FindControl("txtSentDate_FooterNesting") as TextBox).Text);

                if (ddlStatus_FooterNesting.SelectedIndex > 0)
                {
                    ObjBOL_FabricationAndNestingTasks.Status = Int32.Parse(ddlStatus_FooterNesting.SelectedValue);
                }

                string returnStatus = ObjBLL_FabricationAndNestingTasks.Return_String(ObjBOL_FabricationAndNestingTasks);

                if (returnStatus.Trim() != "")
                {
                    Utility.MaintainLogsSpecial(formName, "Add " + ddlNestingFor.SelectedValue, returnStatus);
                    Utility.ShowMessage_Success(Page, "Task inserted successfully !!");
                    //GetNestingTasks();
                    GetAllAerowerksTasks();
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvNestingTasks_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvNestingTasks.EditIndex = -1;
            //GetNestingTasks();
            GetAllAerowerksTasks();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvNestingTasks_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow row = gvNestingTasks.Rows[e.RowIndex];
            ObjBOL_FabricationAndNestingTasks.Operation = 10;
            DropDownList ddlNatureOfTask_EditNesting = row.FindControl("ddlNatureOfTask_EditNesting") as DropDownList;
            DropDownList ddlAssignedFrom_EditNesting = row.FindControl("ddlAssignedFrom_EditNesting") as DropDownList;

            DropDownList ddlTaskType_EditNesting = row.FindControl("ddlTaskType_EditNesting") as DropDownList;
            DropDownList ddlProjectEngineer_EditNesting = row.FindControl("ddlProjectEngineer_EditNesting") as DropDownList;
            DropDownList ddlStatus_EditNesting = row.FindControl("ddlStatus_EditNesting") as DropDownList;

            if (ddlNatureOfTask_EditNesting.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please select Nature of Task !");
                return;
            }

            if (ddlAssignedFrom_EditNesting.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please select Assigned From !");
                return;
            }
            string taskId = gvNestingTasks.DataKeys[e.RowIndex].Values["id"].ToString();

            ObjBOL_FabricationAndNestingTasks.ID = Int32.Parse(taskId);
            ObjBOL_FabricationAndNestingTasks.JobId = txtSearchJobID.Text;
            ObjBOL_FabricationAndNestingTasks.TaskNumber = (row.FindControl("lblTaskNumber_EditNesting") as Label).Text;
            ObjBOL_FabricationAndNestingTasks.NatureOfTask = ddlNatureOfTask_EditNesting.SelectedValue;
            ObjBOL_FabricationAndNestingTasks.AssignedFrom = ddlAssignedFrom_EditNesting.SelectedValue;
            if (ddlTaskType_EditNesting.SelectedIndex > 0)
            {
                ObjBOL_FabricationAndNestingTasks.TaskType = Int32.Parse(ddlTaskType_EditNesting.SelectedValue);
            }

            if (ddlProjectEngineer_EditNesting.SelectedIndex > 0)
            {
                ObjBOL_FabricationAndNestingTasks.ProjectEngineer = Int32.Parse(ddlProjectEngineer_EditNesting.SelectedValue);
            }

            ObjBOL_FabricationAndNestingTasks.StartDate = Utility.ConvertDate((row.FindControl("txtStartDate_EditNesting") as TextBox).Text);
            ObjBOL_FabricationAndNestingTasks.EndDate = Utility.ConvertDate((row.FindControl("txtEndDate_EditNesting") as TextBox).Text);
            ObjBOL_FabricationAndNestingTasks.SentDate = Utility.ConvertDate((row.FindControl("txtSentDate_EditNesting") as TextBox).Text);

            if (ddlStatus_EditNesting.SelectedIndex > 0)
            {
                ObjBOL_FabricationAndNestingTasks.Status = Int32.Parse(ddlStatus_EditNesting.SelectedValue);
            }
            ObjBOL_FabricationAndNestingTasks.Timestamp = long.Parse(gvNestingTasks.DataKeys[e.RowIndex].Values["Timestamp"].ToString());

            string returnStatus = ObjBLL_FabricationAndNestingTasks.Return_String(ObjBOL_FabricationAndNestingTasks);

            if (returnStatus.Trim() == "ER01")
            {
                Utility.ShowMessage_Error(Page, "Task already sent to Production !!");
                return;
            }

            if (returnStatus.Trim() == "ER02")
            {
                Utility.ShowMessage_Error(this, Utility.ConcurrencyErrorMessage());
                return;
            }

            if (returnStatus.Trim() != "")
            {
                Utility.MaintainLogsSpecial(formName, "Update " + ddlNestingFor.SelectedValue, returnStatus);
                Utility.ShowMessage_Success(Page, "Task updated successfully !!");
                gvNestingTasks.EditIndex = -1;
                //GetNestingTasks();
                GetAllAerowerksTasks();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlNestingFor_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlNestingFor_SelectedIndexChanged();
    }

    private void ddlNestingFor_SelectedIndexChanged()
    {
        try
        {
            if (ddlNestingFor.SelectedValue == "1")
            {
                divAero.Visible = true;
                divITW.Visible = false;
                divGaylord.Visible = false;
                divCaddy.Visible = false;
                ResetITWNesting();
                //Get_AeroProjects();
                GetAllAerowerksTasks();
            }
            else if (ddlNestingFor.SelectedValue == "2")
            {
                divAero.Visible = false;
                divITW.Visible = true;
                divGaylord.Visible = false;
                divCaddy.Visible = false;
                ResetAerowerksNesting();
                GetAllITWTasks();
            }
            else if (ddlNestingFor.SelectedValue == "3")
            {
                divGaylord.Visible = true;
                divAero.Visible = false;
                divITW.Visible = false;
                divCaddy.Visible = false;
                ResetAerowerksNesting();
                GetAllGaylordTasks();
            }
            else if (ddlNestingFor.SelectedValue == "4")
            {
                divCaddy.Visible = true;
                divGaylord.Visible = false;
                divAero.Visible = false;
                divITW.Visible = false;
                ResetAerowerksNesting();
                ResetITWNesting();
                ResetCaddy();
            }
            else
            {
                ResetAerowerksNesting();
                divGaylord.Visible = false;
                divAero.Visible = false;
                divITW.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnNestingScheduleReport_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Reports/FrmNestingSchedule.aspx", false);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void SearchPNameButton_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSearchPName.Text != "")
            {
                string output = txtSearchPName.Text;
                int openTagEndPosition = output.IndexOf("#");
                output = output.Substring(openTagEndPosition + 1);
                txtSearchJobID.Text = string.Empty;
                SyncTextbox("NUM", output);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void SyncTextbox(string type, string text)
    {
        try
        {
            if (type != "")
            {
                DataTable dt = new DataTable();
                if (type == "NUM")
                {
                    dt = Utility.ReturnProjects(26, text);
                    if (dt.Rows.Count > 0)
                    {
                        string value = Convert.ToString(dt.Rows[0]["ProjectName"]);
                        txtSearchJobID.Text = value.Substring(0, value.IndexOf(','));
                        txtSearchJobID_TextChanged_Event();
                    }
                    else
                    {
                        txtSearchJobID.Text = string.Empty;
                        txtSearchPName.Text = string.Empty;
                        Utility.ShowMessage_Error(Page, "J# not Found");
                    }
                }
                else
                {
                    dt = Utility.ReturnProjects(25, text);
                    if (dt.Rows.Count > 0)
                    {
                        txtSearchPName.Text = Convert.ToString(dt.Rows[0]["ProjectName"]);
                        txtSearchJobID_TextChanged_Event();
                    }
                    else
                    {
                        txtSearchJobID.Text = "";
                        txtSearchPName.Text = "";
                        Utility.ShowMessage_Error(Page, "J# not Found");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnProjectName_ITW_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtProjectName_ITW.Text != "")
            {
                SyncLookups_ITW("NAME");
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void SearchJobId_ITW_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSearchJobId_ITW.Text != "")
            {
                SyncLookups_ITW("NUMBER");
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void SyncLookups_ITW(string lookupSelector)
    {
        try
        {
            ObjBOL.Operation = 7;
            if (lookupSelector.ToUpper() == "NAME")
            {
                ObjBOL.ProjectName = txtProjectName_ITW.Text.Split(',')[0];
            }
            else if (lookupSelector.ToUpper() == "NUMBER")
            {
                ObjBOL.JobID = txtSearchJobId_ITW.Text.Split(',')[0];
            }

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds = ObjBLL.Return_DataSet(ObjBOL);
            dt = ds.Tables[0];
            int count = dt.Rows.Count;
            if (count > 0)
            {
                if (dt.Rows[0]["JobID"].ToString() != "")
                {
                    txtSearchJobId_ITW.Text = dt.Rows[0]["JobID"].ToString();
                }
                else
                {
                    txtSearchJobId_ITW.Text = dt.Rows[0]["PONumber"].ToString();
                }

                FetchInfo(ds);
                txtProjectName_ITW.Text = dt.Rows[0]["ProjectName"].ToString();
            }
            else
            {
                ResetITWNesting();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void FetchInfo(DataSet ds)
    {
        try
        {
            DataTable dt = ds.Tables[0];
            Dictionary<string, Action<DataRow>> assignments = new Dictionary<string, Action<DataRow>>
            {
                { "RefId", d => txtRefId.Text = d["RefId"].ToString() },
                { "NestingStatusId", d =>
                    {
                       if(ddlNestingStatus.Items.FindByValue(d["NestingStatusId"].ToString()) != null)
                        {
                            ddlNestingStatus.SelectedValue = d["NestingStatusId"].ToString();
                        }
                        else
                        {
                            if(ddlNestingStatus.Items.Count > 0)
                            {
                                ddlNestingStatus.SelectedIndex = 0;
                            }
                        }
                    }
                },
                { "NestingStartDate", d => txtNestingStartDate.Text = d["NestingStartDate"].ToString() },
                { "NestingEndDate", d => txtNestingEndDate.Text = d["NestingEndDate"].ToString() },
                { "SentDate", d => txtNestingSentDate.Text = d["SentDate"].ToString() },
                { "SentToProduction", d => chkSendToProduction.Checked = bool.Parse(d["SentToProduction"].ToString()) }
            };

            foreach (var assignment in assignments)
            {
                try
                {
                    assignment.Value(dt.Rows[0]);
                }
                catch (Exception ex)
                {
                    Utility.AddEditException(ex, assignment.Key);
                }
            }

            btnAdd.Text = "Update";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtRefId.Text.Trim() != "")
            {
                ObjBOL.Operation = 25;
                ObjBOL.RefId = txtRefId.Text;
                ObjBOL.NestingStartDate = Utility.ConvertDate(txtNestingStartDate.Text);
                ObjBOL.NestingEndDate = Utility.ConvertDate(txtNestingEndDate.Text);
                ObjBOL.SentDate = Utility.ConvertDate(txtNestingSentDate.Text);
                if (ddlNestingStatus.SelectedIndex > 0)
                {
                    ObjBOL.NestingStatusId = Int32.Parse(ddlNestingStatus.SelectedValue);
                }
                ObjBOL.SendToProduction = chkSendToProduction.Checked;

                string returnStatus = ObjBLL.Return_String(ObjBOL);

                if (returnStatus.Trim() == "S")
                {
                    Utility.MaintainLogsSpecial(formName, "update", txtRefId.Text);
                    Utility.ShowMessage_Success(Page, "Record updated successfully !!");
                    btnCancel_Click();
                    GetAllITWTasks();
                }
            }
            else
            {
                Utility.ShowMessage_Error(Page, "Please select Job !");
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
            txtSearchJobId_ITW.Text = string.Empty;
            txtProjectName_ITW.Text = string.Empty;
            txtRefId.Text = string.Empty;
            txtNestingStartDate.Text = string.Empty;
            txtNestingEndDate.Text = string.Empty;
            txtNestingSentDate.Text = string.Empty;
            ddlNestingStatus.SelectedIndex = 0;
            chkSendToProduction.Checked = false;
            //btnAdd.Text = "Save";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private DataTable ReportData_SubReport1()
    {
        DataTable dt = new DataTable();
        try
        {
            string query = "EXEC Get_NestingSchedule 2 ";
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
            string query = "EXEC Get_NestingSchedule 3 ";
            cls.Return_DT(dt, query);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    private void GetAllAerowerksTasks()
    {
        try
        {
            DataTable dt = new DataTable();
            cls.Return_DT(dt, "EXEC [aero_ForecastingAndNestingTasks_V1] 7");
            if (dt.Rows.Count > 0)
            {
                gvNestingTasks.DataSource = dt;
                gvNestingTasks.DataBind();
                BindNestingGridControls();
            }
            else
            {
                gvNestingTasks.DataSource = string.Empty;
                gvNestingTasks.DataBind();
            }            
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void GetAllITWTasks()
    {
        try
        {
            DataTable dt = new DataTable();
            cls.Return_DT(dt, "EXEC [aero_ForecastingAndNestingTasks_V1] 15");
            if (dt.Rows.Count > 0)
            {
                gvITWNesting.DataSource = dt;
                gvITWNesting.DataBind();
            }
            else
            {
                gvITWNesting.DataSource = string.Empty;
                gvITWNesting.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void GetAllGaylordTasks()
    {
        try
        {
            DataTable dt = new DataTable();
            cls.Return_DT(dt, "EXEC [GL_ManageProjects] 26");
            if (dt.Rows.Count > 0)
            {
                gvGaylord.DataSource = dt;
                gvGaylord.DataBind();
            }
            else
            {
                gvGaylord.DataSource = string.Empty;
                gvGaylord.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void Get_AeroProjects()
    {
        try
        {
            DataTable dt1 = ReportData_SubReport1();

            if (dt1.Rows.Count > 0)
            {
                gvAerowerksNesting.DataSource = dt1;
                gvAerowerksNesting.DataBind();
            }
            else
            {
                gvAerowerksNesting.DataSource = string.Empty;
                gvAerowerksNesting.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvAerowerksNesting_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Open")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                string JobId = gvAerowerksNesting.DataKeys[rowIndex].Value.ToString();
                txtSearchJobID.Text = JobId;
                SyncTextbox("NAME", txtSearchJobID.Text);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvITWNesting_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //if (e.CommandName == "Open")
            //{
            //    int rowIndex = Convert.ToInt32(e.CommandArgument);

            //    string PO = gvITWNesting.DataKeys[rowIndex].Value.ToString();
            //    txtSearchJobId_ITW.Text = PO;
            //    SyncLookups_ITW("NUMBER");
            //}

            if (e.CommandName == "Send")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string refId = gvITWNesting.DataKeys[rowIndex].Values[0].ToString();

                ObjBOL_FabricationAndNestingTasks.Operation = 17;
                ObjBOL_FabricationAndNestingTasks.JobId = refId;
                ObjBOL_FabricationAndNestingTasks.ID = Int32.Parse(gvITWNesting.DataKeys[rowIndex].Values["ShipmentId"].ToString());
                ObjBOL_FabricationAndNestingTasks.AssistedBy = Utility.GetCurrentUser();
                string returnStatus = ObjBLL_FabricationAndNestingTasks.Return_String(ObjBOL_FabricationAndNestingTasks);

                if (returnStatus.Trim() == "ER01")
                {
                    Utility.ShowMessage_Error(Page, "Task already sent to Production !!");
                    return;
                }

                if (returnStatus.Trim() == "ER02")
                {
                    Utility.ShowMessage_Error(Page, "Please enter Sent Date !!");
                    return;
                }

                if (returnStatus.Trim() == "ER03")
                {
                    Utility.ShowMessage_Error(Page, "Not Authorized !!");
                    return;
                }

                if (returnStatus.Trim() != "")
                {
                    Utility.ShowMessage_Success(Page, "Task Sent to Production Successfully !!");
                    Utility.MaintainLogsSpecial(formName, "Sent " + ddlNestingFor.SelectedValue, refId);
                    GetAllITWTasks();
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvITWNesting_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvITWNesting.EditIndex = e.NewEditIndex;
            GetAllITWTasks();

            DataSet ds = new DataSet();
            string refId = gvITWNesting.DataKeys[e.NewEditIndex].Values[0].ToString();

            ObjBOL.Id = Int32.Parse(gvITWNesting.DataKeys[e.NewEditIndex].Values["ShipmentId"].ToString());
            ObjBOL_FabricationAndNestingTasks.Operation = 16;
            ObjBOL_FabricationAndNestingTasks.JobId = refId;
            ds = ObjBLL_FabricationAndNestingTasks.Return_DataSet(ObjBOL_FabricationAndNestingTasks);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                TextBox txtNestingStartDate_EditNesting = gvITWNesting.Rows[e.NewEditIndex].FindControl("txtStartDate_EditNesting") as TextBox;
                txtNestingStartDate_EditNesting.Text = row["NestingStartDate"].ToString();

                TextBox txtNestingEndDate_EditNesting = gvITWNesting.Rows[e.NewEditIndex].FindControl("txtEndDate_EditNesting") as TextBox;
                txtNestingEndDate_EditNesting.Text = row["NestingEndDate"].ToString();

                TextBox txtSentDate_EditNesting = gvITWNesting.Rows[e.NewEditIndex].FindControl("txtSentDate_EditNesting") as TextBox;
                txtSentDate_EditNesting.Text = row["SentDate"].ToString();

                DropDownList ddlStatus_EditNesting = gvITWNesting.Rows[e.NewEditIndex].FindControl("ddlStatus_EditNesting") as DropDownList;
                if (ddlStatus_EditNesting.Items.FindByValue(row["NestingStatusId"].ToString()) != null)
                {
                    ddlStatus_EditNesting.SelectedValue = row["NestingStatusId"].ToString();
                }
                else
                {
                    if (ddlStatus_EditNesting.Items.Count > 0)
                    {
                        ddlStatus_EditNesting.SelectedIndex = 0;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvITWNesting_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvITWNesting.EditIndex = -1;
            GetAllITWTasks();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvITWNesting_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow row = gvITWNesting.Rows[e.RowIndex];
            DropDownList ddlStatus_EditNesting = row.FindControl("ddlStatus_EditNesting") as DropDownList;

            string refId = gvITWNesting.DataKeys[e.RowIndex].Values["RefId"].ToString();

            if (refId.Trim() != "")
            {
                ObjBOL.Operation = 25;
                ObjBOL.RefId = refId;
                ObjBOL.Id = Int32.Parse(gvITWNesting.DataKeys[e.RowIndex].Values["ShipmentId"].ToString());
                ObjBOL.NestingStartDate = Utility.ConvertDate((row.FindControl("txtStartDate_EditNesting") as TextBox).Text);
                ObjBOL.NestingEndDate = Utility.ConvertDate((row.FindControl("txtEndDate_EditNesting") as TextBox).Text);
                ObjBOL.SentDate = Utility.ConvertDate((row.FindControl("txtSentDate_EditNesting") as TextBox).Text);
                if (ddlStatus_EditNesting.SelectedIndex > 0)
                {
                    ObjBOL.NestingStatusId = Int32.Parse(ddlStatus_EditNesting.SelectedValue);
                }
                ObjBOL.Timestamp = long.Parse(gvITWNesting.DataKeys[e.RowIndex].Values["Timestamp"].ToString());

                string returnStatus = ObjBLL.Return_String(ObjBOL);

                if (returnStatus.Trim() == "ER01")
                {
                    Utility.ShowMessage_Error(this, Utility.ConcurrencyErrorMessage());
                    return;
                }

                if (returnStatus.Trim() == "ER02")
                {
                    Utility.ShowMessage_Error(this, "Database error occured !");
                    return;
                }

                if (returnStatus.Trim() == "S")
                {
                    Utility.MaintainLogsSpecial(formName, "update-ITW", refId);
                    Utility.ShowMessage_Success(Page, "Record updated successfully !!");
                    gvITWNesting.EditIndex = -1;
                    //btnCancel_Click();
                    GetAllITWTasks();
                }
            }
            else
            {
                Utility.ShowMessage_Error(Page, "Please select Job !");
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvGaylord_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Send")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                string Id = gvGaylord.DataKeys[rowIndex].Values[0].ToString();
                GridViewRow row = gvGaylord.Rows[rowIndex];

                ObjBOL_Gaylord.Operation = 29;
                ObjBOL_Gaylord.Id = Int32.Parse(Id);
                ObjBOL_Gaylord.WorkOrder = (row.FindControl("lblWorkOrder") as Label).Text;
                ObjBOL_Gaylord.LoginUserID = Utility.GetCurrentUser();
                string returnStatus = ObjBLL_Gaylord.Return_String(ObjBOL_Gaylord);

                if (returnStatus.Trim() == "ER01")
                {
                    Utility.ShowMessage_Error(Page, "Task already sent to Production !!");
                    return;
                }

                if (returnStatus.Trim() == "ER02")
                {
                    Utility.ShowMessage_Error(Page, "Please enter Sent Date !!");
                    return;
                }

                if (returnStatus.Trim() == "ER03")
                {
                    Utility.ShowMessage_Error(Page, "Not Authorized !!");
                    return;
                }

                if (returnStatus.Trim() != "")
                {
                    Utility.ShowMessage_Success(Page, "Task Sent to Production Successfully !!");
                    Utility.MaintainLogsSpecial(formName, "Sent " + ddlNestingFor.SelectedValue, Id);
                    GetAllGaylordTasks();
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvGaylord_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvGaylord.EditIndex = e.NewEditIndex;
            GetAllGaylordTasks();

            DataSet ds = new DataSet();
            string Id = gvGaylord.DataKeys[e.NewEditIndex].Values[0].ToString();
            GridViewRow row1 = gvGaylord.Rows[e.NewEditIndex];

            ObjBOL_Gaylord.Operation = 27;
            ObjBOL_Gaylord.Id = Int32.Parse(Id);
            ObjBOL_Gaylord.WorkOrder = (row1.FindControl("lblWorkOrder") as Label).Text;
            ds = ObjBLL_Gaylord.Return_DataSet(ObjBOL_Gaylord);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                TextBox txtNestingStartDate_EditNesting = gvGaylord.Rows[e.NewEditIndex].FindControl("txtStartDate_EditNesting") as TextBox;
                txtNestingStartDate_EditNesting.Text = row["NestingStartDate"].ToString();

                TextBox txtNestingEndDate_EditNesting = gvGaylord.Rows[e.NewEditIndex].FindControl("txtEndDate_EditNesting") as TextBox;
                txtNestingEndDate_EditNesting.Text = row["NestingEndDate"].ToString();

                TextBox txtSentDate_EditNesting = gvGaylord.Rows[e.NewEditIndex].FindControl("txtSentDate_EditNesting") as TextBox;
                txtSentDate_EditNesting.Text = row["NestingSentDate"].ToString();

                DropDownList ddlStatus_EditNesting = gvGaylord.Rows[e.NewEditIndex].FindControl("ddlStatus_EditNesting") as DropDownList;
                if (ddlStatus_EditNesting.Items.FindByValue(row["NestingStatus"].ToString()) != null)
                {
                    ddlStatus_EditNesting.SelectedValue = row["NestingStatus"].ToString();
                }
                else
                {
                    if (ddlStatus_EditNesting.Items.Count > 0)
                    {
                        ddlStatus_EditNesting.SelectedIndex = 0;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvGaylord_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvGaylord.EditIndex = -1;
            GetAllGaylordTasks();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvGaylord_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            GridViewRow row = gvGaylord.Rows[e.RowIndex];
            DropDownList ddlStatus_EditNesting = row.FindControl("ddlStatus_EditNesting") as DropDownList;

            string Id = gvGaylord.DataKeys[e.RowIndex].Values["Id"].ToString();

            if (Id.Trim() != "")
            {
                ObjBOL_Gaylord.Operation = 28;
                ObjBOL_Gaylord.Id = Int32.Parse(Id);
                ObjBOL_Gaylord.WorkOrder = (row.FindControl("lblWorkOrder") as Label).Text;
                ObjBOL_Gaylord.NestingStartDate = Utility.ConvertDate((row.FindControl("txtStartDate_EditNesting") as TextBox).Text);
                ObjBOL_Gaylord.NestingEndDate = Utility.ConvertDate((row.FindControl("txtEndDate_EditNesting") as TextBox).Text);
                ObjBOL_Gaylord.NestingSentDate = Utility.ConvertDate((row.FindControl("txtSentDate_EditNesting") as TextBox).Text);
                if (ddlStatus_EditNesting.SelectedIndex > 0)
                {
                    ObjBOL_Gaylord.NestingStatusId = Int32.Parse(ddlStatus_EditNesting.SelectedValue);
                }
                ObjBOL_Gaylord.Timestamp = long.Parse(gvGaylord.DataKeys[e.RowIndex].Values["Timestamp"].ToString());

                string returnStatus = ObjBLL_Gaylord.Return_String(ObjBOL_Gaylord);
                if (returnStatus.Trim() == "ER01")
                {
                    Utility.ShowMessage_Error(this, Utility.ConcurrencyErrorMessage());
                    return;
                }

                if (returnStatus.Trim() == "S")
                {
                    Utility.MaintainLogsSpecial(formName, "update-gaylord", Id);
                    Utility.ShowMessage_Success(Page, "Record updated successfully !!");
                    gvGaylord.EditIndex = -1;
                    //btnCancel_Click();
                    GetAllGaylordTasks();
                }
            }
            else
            {
                Utility.ShowMessage_Error(Page, "Please select PO !");
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnNestingReport_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Reports/FrmNestingReport.aspx", false);
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}