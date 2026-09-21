using BLLAERO;
using BOLAERO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DailyPurchase_FrmDailyPurchaseDepartment : System.Web.UI.Page
{
    BOLDailyPurchaseDepartment ObjBOL = new BOLDailyPurchaseDepartment();
    BLLDailyPurchaseDepartment ObjBLL = new BLLDailyPurchaseDepartment();

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
            ObjBOL.Operation = 1;
            DataSet ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlDepartment, ds.Tables[0]);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDepartment_SelectedIndexChanged();
    }

    private void ddlDepartment_SelectedIndexChanged()
    {
        try
        {
            if (ddlDepartment.SelectedIndex > 0)
            {
                DataSet ds = new DataSet();
                ObjBOL.Operation = 2;
                ObjBOL.Id = Convert.ToInt32(ddlDepartment.SelectedValue);
                ds = ObjBLL.Return_DataSet(ObjBOL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    HfTimestamp.Value = ds.Tables[0].Rows[0]["Timestamp"].ToString();
                    txtName.Text = Convert.ToString(ds.Tables[0].Rows[0]["Department"]);
                    btnSave.Text = "Update";
                }
            }
            else
            {
                btnCancel_Click();
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
                Utility.ShowMessage_Error(Page, "Please Enter Department. !");
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidationCheck())
            {
                //string msg = "";
                if (ddlDepartment.SelectedIndex > 0)
                {
                    ObjBOL.Operation = 4;
                    ObjBOL.Id = Convert.ToInt32(ddlDepartment.SelectedValue);
                    if (HfTimestamp.Value.Trim() != "")
                    {
                        ObjBOL.Timestamp = long.Parse(HfTimestamp.Value);
                    }
                }
                else
                {
                    ObjBOL.Operation = 3;
                }

                ObjBOL.Department = txtName.Text;

                string returnStatus = ObjBLL.Return_String(ObjBOL);
                if (returnStatus.Trim() == "")
                {                    
                        Utility.ShowMessage_Error(this, "Database error occured! ");
                        return;                    
                }

                if (returnStatus.Trim() == "ER01")
                {
                    Utility.ShowMessage_Error(this, Utility.ConcurrencyErrorMessage());
                    return;
                }

                if (returnStatus.Trim() == "ER02")
                {
                    Utility.ShowMessage_Error(this, "Department already exists !!");
                    return;
                }

                if (ddlDepartment.SelectedIndex > 0)
                {                    
                    Utility.ShowMessage_Success(this, "Record updated successfully!!");
                    Utility.MaintainLogsSpecial("FrmDailyPurchaseDepartment.aspx", "Update", ddlDepartment.SelectedValue);

                    btnCancel_Click();
                    BindControls();
                    if (ddlDepartment.Items.FindByValue(returnStatus.Trim()) != null)
                    {
                        ddlDepartment.SelectedValue = returnStatus.Trim();
                        ddlDepartment_SelectedIndexChanged();
                    }
                }
                else
                {
                    Utility.ShowMessage_Success(this, "Record inserted successfully!!");
                    Utility.MaintainLogsSpecial("FrmDailyPurchaseDepartment.aspx", "Save", returnStatus);
                    btnCancel_Click();
                    BindControls();

                    if (ddlDepartment.Items.FindByValue(returnStatus.Trim()) != null)
                    {
                        ddlDepartment.SelectedValue = returnStatus.Trim();
                        ddlDepartment_SelectedIndexChanged();
                    }
                }
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
            ddlDepartment.SelectedIndex = 0;
            txtName.Text = string.Empty;
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}