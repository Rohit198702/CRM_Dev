using BLLAERO;
using BOLAERO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class NACUFS_FrmDesignation : System.Web.UI.Page
{
    BOLManageDesg ObjBOL = new BOLManageDesg();
    BLLManageDesg ObjBLL = new BLLManageDesg();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Control_Bind();

            if (string.IsNullOrEmpty(Request.QueryString["Designation"]) == false)

            {
                ddlDesg.SelectedItem.Text = Request.QueryString["Designation"];
                txtName.Text = Request.QueryString["Designation"];
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

    private void Control_Bind()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.operation = 1;
            ds = ObjBLL.GetDesg(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlDesg, ds.Tables[0]);
            }
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
            if (txtName.Text == "")
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "window", "alert('Please Enter Designation. !');", true);
                Utility.ShowMessage_Error(Page, "Please Enter Designation. !");
                txtName.Focus();
                return false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    private void Controls_Reset()
    {
        try
        {
            HfTimestamp.Value = "-1";
            ddlDesg.SelectedIndex = 0;
            txtName.Text = string.Empty;
            lblMsg.Text = "";
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Controls_Reset();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidationCheck() == true)
            {
                string msg = "";
                if (ddlDesg.SelectedIndex > 0)
                {
                    ObjBOL.id = Convert.ToInt32(ddlDesg.SelectedValue);
                }
                else
                {
                    ObjBOL.id = 0;
                }
                ObjBOL.operation = 2;
                ObjBOL.DesgName = txtName.Text;
                ObjBOL.Timestamp = long.Parse(HfTimestamp.Value);

                msg = ObjBLL.SaveDesg(ObjBOL);

                if (msg.Trim() == "ER01")
                {
                    Utility.ShowMessage_Error(this, Utility.ConcurrencyErrorMessage());
                    return;
                }

                if (msg.Trim() == "ER02")
                {
                    Utility.ShowMessage_Error(this, "Record already exists!! ");
                    return;
                }

                if (ddlDesg.SelectedIndex > 0)
                {
                    hfCusId.Value = ddlDesg.SelectedValue;
                    Utility.ShowMessage_Success(this, "Record updated successfully!!. ");
                    Utility.MaintainLogsSpecial("frmDesignation.aspx", "Update", ddlDesg.SelectedValue);
                    Control_Bind();
                    Controls_Reset();
                }
                else
                {
                    hfCusId.Value = msg;
                    Utility.ShowMessage_Success(this, "Record save successfully!!. ");
                    Utility.MaintainLogsSpecial("frmDesignation.aspx", "save", msg);
                    Control_Bind();
                    Controls_Reset();
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlDesg_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlDesg.SelectedIndex > 0)
            {
                hfCusId.Value = ddlDesg.SelectedValue;
                DataSet ds = new DataSet();
                ObjBOL.operation = 3;
                ObjBOL.id = Convert.ToInt32(ddlDesg.SelectedValue);
                ds = ObjBLL.GetDesg(ObjBOL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    HfTimestamp.Value = Convert.ToString(ds.Tables[0].Rows[0]["Timestamp"]).ToString();
                    txtName.Text = Convert.ToString(ds.Tables[0].Rows[0]["DesgName"]);
                    lblMsg.Text = "";
                    btnSave.Text = "Update";
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}
