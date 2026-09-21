using System;
using BOLAERO;
using BLLAERO;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
/// <summary>
///  Proposal Form (06 December 2018) Rohit Kumar
/// </summary>
public partial class Administration_FrmDepartment : System.Web.UI.Page
{
    BOLManageDepartment ObjBOL = new BOLManageDepartment();
    BLLManageDepartment ObjBLL = new BLLManageDepartment();

    // Page load event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_Controls();           
        }

    }
    /// <summary>
    /// Prepare drop down list ddlCity
    /// </summary>
    // Bind all dropdownlist here
    private void Bind_Controls()
    {
        DataSet ds = new DataSet();
        ObjBOL.operation = 1;
        ds = ObjBLL.GetDepartment(ObjBOL);
        if (ds.Tables[0].Rows.Count > 0)
        {
            Utility.BindDropDownList(ddlDepartment, ds.Tables[0]);
        }
    }
    /// <summary>
    /// Mandetory fields check
    /// </summary>
    /// <returns></returns>
    private Boolean ValidationCheck()
    {
        if (txtDeptName.Text.Trim() == "")
        {         
            Utility.ShowMessage_Error(Page, "Please Enter Department Name. !");
            txtDeptName.Focus();
            return false;
        }

        return true;
    }
    /// <summary>
    /// clear controls
    /// </summary>
    // Reset all controls
    private void Reset()
    {
        try
        {           
            ddlDepartment.SelectedIndex = 0;
            txtDeptName.Text = string.Empty;
            lblMsg.Text = "";
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
    /// <summary>
    /// Save infromation if mandetory fields
    /// entered in page controls
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    // Save data
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ValidationCheck() == true)
        {
            string msg = "";
            if (ddlDepartment.SelectedIndex > 0)
            {
                ObjBOL.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);                
            }
            else
            {
                ObjBOL.DepartmentID = 0;
            }
            ObjBOL.operation = 2;
            if(txtDeptName.Text.Trim() != "")
            {
                ObjBOL.DeptName = txtDeptName.Text.Trim();
            }            

            msg = ObjBLL.SaveDepartment(ObjBOL);
            if (msg.Trim() == "ER01")
            {
                Utility.ShowMessage_Error(this, Utility.ConcurrencyErrorMessage());
                return;
            }

            if (msg.Trim() == "ER02")
            {
                Utility.ShowMessage_Error(this, "Department already exists !!");
                return;
            }

            if (ddlDepartment.SelectedIndex > 0)
            {                
                Utility.ShowMessage_Success(this, "Record updated successfully!!");
                Utility.MaintainLogsSpecial("FrmDepartment.aspx", "Update", ddlDepartment.SelectedValue);
                Bind_Controls();
                Reset();
            }
            else
            {                
                Utility.ShowMessage_Success(this, "Record inserted successfully!!");
                Utility.MaintainLogsSpecial("FrmDepartment.aspx", "Save", msg);
                Bind_Controls();
                Reset();
            }
        }
    }
    /// <summary>
    /// Cancel information
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    // Cancel command
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Reset();
    }
    /// <summary>
    /// Cancel all the information
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDepartment.SelectedIndex > 0)
            {           
                DataSet ds = new DataSet();
                ObjBOL.operation = 3;
                ObjBOL.DepartmentID = Convert.ToInt32(ddlDepartment.SelectedValue);
                ds = ObjBLL.GetDepartment(ObjBOL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtDeptName.Text = Convert.ToString(ds.Tables[0].Rows[0]["Dept"]);
                    lblMsg.Text = "";
                    btnSave.Text = "Update";
                }
            }
            else
            {
                Reset();
            }
        }

        catch (Exception ex)
        {            
            Utility.AddEditException(ex);            
        }
    }    
}