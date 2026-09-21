using BLLAERO;
using BOLAERO;
using System;
using System.Data;

public partial class Mafsi_FrmMafsiRegionMaintenance : System.Web.UI.Page
{
    BOLMafsiRegionMaintenance ObjBOL = new BOLMafsiRegionMaintenance();
    BLLMafsiRegionMaintenance ObjBLL = new BLLMafsiRegionMaintenance();
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
                Utility.BindDropDownList(ddlMafsiRegion, ds.Tables[0]);
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
            if (txtRegionNo.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter Region No!! ");
                txtRegionNo.Focus();
                return false;
            }

            if (txtRegionName.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter Region Name!! ");
                txtRegionName.Focus();
                return false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    protected void ddlMafsiRegion_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMafsiRegion_SelectedIndexChanged();
    }

    private void ddlMafsiRegion_SelectedIndexChanged()
    {
        try
        {
            ObjBOL.Operation = 2;
            if (ddlMafsiRegion.SelectedIndex > 0)
            {
                ObjBOL.Id = Int32.Parse(ddlMafsiRegion.SelectedValue);
            }
            DataSet ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtRegionNo.Text = ds.Tables[0].Rows[0]["RegionNo"].ToString();
                txtRegionName.Text = ds.Tables[0].Rows[0]["RegionName"].ToString();
                txtSortOrder.Text = ds.Tables[0].Rows[0]["SortOrder"].ToString();

                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Status"]) == 1)
                {
                    rdoStatus.SelectedValue = "1";
                }
                else
                {
                    rdoStatus.SelectedValue = "0";
                }
                hfTimeStamp.Value = ds.Tables[0].Rows[0]["Timestamp"].ToString();
                btnSave.Text = "Update";
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
            if (ValidationCheck())
            {
                string message = "Record inserted successfully !!";
                string op = "save";
                ObjBOL.Operation = 3;//insert
                if (ddlMafsiRegion.SelectedIndex > 0)
                {
                    ObjBOL.Operation = 4;//update
                    ObjBOL.Id = Int32.Parse(ddlMafsiRegion.SelectedValue);
                    message = "Record updated successfully !!";
                    op = "update";
                    if (hfTimeStamp.Value != "-1")
                    {
                        ObjBOL.Timestamp = long.Parse(hfTimeStamp.Value);
                    }
                }

                ObjBOL.RegionNo = txtRegionNo.Text;
                ObjBOL.RegionName = txtRegionName.Text;
                if (txtSortOrder.Text.Trim() != "")
                {
                    ObjBOL.SortOrder = Int32.Parse(txtSortOrder.Text);
                }

                if (rdoStatus.SelectedValue == "1")
                {
                    ObjBOL.Status = true;
                }
                else
                {
                    ObjBOL.Status = false;
                }

                string returnStatus = ObjBLL.Return_String(ObjBOL);

                if (returnStatus.Trim() == "ER01")
                {
                    Utility.ShowMessage_Error(Page, "Region No already exists !!");
                    return;
                }

                if (returnStatus.Trim() == "ER02")
                {
                    Utility.ShowMessage_Error(Page, "Region Name already exists !!");
                    return;
                }

                if (returnStatus.Trim() == "ER03")
                {
                    Utility.ShowMessage_Error(Page, Utility.ConcurrencyErrorMessage());
                    return;
                }

                Utility.MaintainLogsSpecial("FrmMafsiRegionMaintenance.aspx", op, returnStatus);
                Utility.ShowMessage_Success(Page, message);
                BindControls();
                if (ddlMafsiRegion.Items.FindByValue(returnStatus.Trim()) != null)
                {
                    ddlMafsiRegion.SelectedValue = returnStatus.Trim();
                    ddlMafsiRegion_SelectedIndexChanged();
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
            txtRegionNo.Text = string.Empty;
            txtRegionName.Text = string.Empty;
            rdoStatus.SelectedValue = "1";
            ddlMafsiRegion.SelectedIndex = 0;
            txtSortOrder.Text = string.Empty;
            hfTimeStamp.Value = "-1";
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}