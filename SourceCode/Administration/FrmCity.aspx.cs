using System;
using BOLAERO;
using BLLAERO;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
/// <summary>
///  Proposal Form (06 December 2018) Rohit Kumar
/// </summary>
public partial class Administration_FrmCity : System.Web.UI.Page
{
    BOLManageCity ObjBOL = new BOLManageCity();
    BLLManageCity ObjBLL = new BLLManageCity();

    // Page load event
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_Controls();

            if (string.IsNullOrEmpty(Request.QueryString["city"]) == false)
            {
                ddlCity.SelectedItem.Text = Request.QueryString["city"];
                txtName.Text = Request.QueryString["city"];
            }
            if (Session["PNumber"] != null)
            {
                btnBack.Enabled = true;
            }
            else
            {
                btnBack.Enabled = false;
            }
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
        ds = ObjBLL.GetCity(ObjBOL);
        if (ds.Tables[0].Rows.Count > 0)
        {
            Utility.BindDropDownList(ddlCity, ds.Tables[0]);
        }
    }
    /// <summary>
    /// Mandetory fields check
    /// </summary>
    /// <returns></returns>
    private Boolean ValidationCheck()
    {
        if (txtName.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "alert('Please Enter City Name. !');", true);
            Utility.ShowMessage_Error(Page, "Please Enter City Name. !");
            txtName.Focus();
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
            HfTimestamp.Value = "-1";
            ddlCity.SelectedIndex = 0;
            txtName.Text = string.Empty;
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
            if (ddlCity.SelectedIndex > 0)
            {
                ObjBOL.CityID = Convert.ToInt32(ddlCity.SelectedValue);
                if (HfTimestamp.Value.Trim() != "")
                {
                    ObjBOL.TimeStamp = long.Parse(HfTimestamp.Value);
                }
            }
            else
            {
                ObjBOL.CityID = 0;
            }
            ObjBOL.operation = 2;
            ObjBOL.CityName = txtName.Text;

            msg = ObjBLL.SaveCity(ObjBOL);
            if (msg.Trim() == "ER01")
            {
                Utility.ShowMessage_Error(this, Utility.ConcurrencyErrorMessage());
                return;
            }

            if (msg.Trim() == "ER02")
            {
                Utility.ShowMessage_Error(this, "City already exists !!");
                return;
            }

            if (ddlCity.SelectedIndex > 0)
            {
                hfCusId.Value = ddlCity.SelectedValue;
                Utility.ShowMessage_Success(this, "Record updated successfully!!");
                Utility.MaintainLogsSpecial("FrmCity.aspx", "update", ddlCity.SelectedValue);
                Bind_Controls();
                Reset();
            }
            else
            {
                hfCusId.Value = msg;
                Utility.ShowMessage_Success(this, "Record inserted successfully!!");
                Utility.MaintainLogsSpecial("FrmCity.aspx", "Save", msg);
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
            if (ddlCity.SelectedIndex > 0)
            {
                hfCusId.Value = ddlCity.SelectedValue;
                DataSet ds = new DataSet();
                ObjBOL.operation = 3;
                ObjBOL.CityID = Convert.ToInt32(ddlCity.SelectedValue);
                ds = ObjBLL.GetCity(ObjBOL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    HfTimestamp.Value = ds.Tables[0].Rows[0]["Timestamp"].ToString();
                    txtName.Text = Convert.ToString(ds.Tables[0].Rows[0]["CityName"]);
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
            lblMsg.Text = ex.ToString();
            //Utility.ShowMessage(Page, ex.ToString());
            //throw ex;
        }
    }
    /// <summary>
    /// Back to the Proposal Page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/SalesManagement/FrmProposals.aspx");
    }
}