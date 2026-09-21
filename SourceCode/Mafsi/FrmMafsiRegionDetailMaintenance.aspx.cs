using BLLAERO;
using BOLAERO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Mafsi_FrmMafsiRegionDetailMaintenance : System.Web.UI.Page
{
    BOLMafsiRegionDetailMaintenance ObjBOL = new BOLMafsiRegionDetailMaintenance();
    BLLMafsiRegionDetailMaintenance ObjBLL = new BLLMafsiRegionDetailMaintenance();
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
                Utility.BindDropDownList(ddlMafsiRegion_Lookup, ds.Tables[0]);
                Utility.BindDropDownList(ddlMafsiRegion, ds.Tables[0]);
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlCountry, ds.Tables[1]);
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
            if (ddlMafsiRegion.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please select Region !! ");
                ddlMafsiRegion.Focus();
                return false;
            }

            if (ddlCountry.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please select country !! ");
                ddlCountry.Focus();
                return false;
            }

            if (ddlState.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please select State !! ");
                ddlState.Focus();
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
                string message = "Record inserted successfully !!";
                string op = "save";
                ObjBOL.Operation = 5;//insert               
                if (hfTimeStamp.Value != "-1")
                {
                    ObjBOL.Operation = 6;//update
                    ObjBOL.Id = Int32.Parse(hfId.Value);
                    message = "Record updated successfully !!";
                    op = "update";
                    if (hfTimeStamp.Value != "-1")
                    {
                        ObjBOL.Timestamp = long.Parse(hfTimeStamp.Value);
                    }
                }

                if (ddlMafsiRegion.SelectedIndex > 0)
                {
                    ObjBOL.RegionId = Int32.Parse(ddlMafsiRegion.SelectedValue);
                }

                if (ddlState.SelectedIndex > 0)
                {
                    ObjBOL.StateId = Int32.Parse(ddlState.SelectedValue);
                }

                ObjBOL.City = txtCity.Text;
                if (rdoStatus.SelectedValue == "1")
                {
                    ObjBOL.Status = true;
                }
                else
                {
                    ObjBOL.Status = false;
                }

                string returnStatus = ObjBLL.Return_String(ObjBOL);

                if (returnStatus.Trim() == "ER03")
                {
                    Utility.ShowMessage_Error(Page, Utility.ConcurrencyErrorMessage());
                    return;
                }

                Utility.MaintainLogsSpecial("FrmMafsiRegionDetailMaintenance.aspx", op, returnStatus);
                Utility.ShowMessage_Success(Page, message);
                returnStatus = ddlMafsiRegion.SelectedValue;
                btnCancel_Click();
                if (ddlMafsiRegion_Lookup.Items.FindByValue(returnStatus.Trim()) != null)
                {
                    ddlMafsiRegion_Lookup.SelectedValue = returnStatus.Trim();
                    ddlMafsiRegion_Lookup_SelectedIndexChanged();
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
            ddlState.Items.Clear();
            ddlMafsiRegion_Lookup.SelectedIndex = 0;
            ddlMafsiRegion.SelectedIndex = 0;
            ddlCountry.SelectedIndex = 0;
            txtCity.Text = string.Empty;
            rdoStatus.SelectedValue = "1";
            hfTimeStamp.Value = "-1";
            hfId.Value = "-1";
            gvDetail.DataSource = string.Empty;
            gvDetail.DataBind();
            btnSave.Text = "Save";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlMafsiRegion_Lookup_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMafsiRegion_Lookup_SelectedIndexChanged();
    }

    private void ddlMafsiRegion_Lookup_SelectedIndexChanged()
    {
        try
        {
            ddlMafsiRegion.SelectedValue = ddlMafsiRegion_Lookup.SelectedValue;

            ObjBOL.Operation = 3;
            ObjBOL.Id = Int32.Parse(ddlMafsiRegion_Lookup.SelectedValue);
            DataSet ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDetail.DataSource = ds.Tables[0];
                gvDetail.DataBind();
            }
            else
            {
                gvDetail.DataSource = string.Empty;
                gvDetail.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCountry_SelectedIndexChanged();
    }

    private void ddlCountry_SelectedIndexChanged()
    {
        try
        {
            if (ddlCountry.SelectedIndex == 0)
            {
                ddlState.Items.Clear();
                return;
            }

            DataSet ds = new DataSet();
            ObjBOL.Operation = 2;
            if (ddlCountry.SelectedIndex > 0)
            {
                ObjBOL.Id = Int32.Parse(ddlCountry.SelectedValue);
            }
            ds = ObjBLL.Return_DataSet(ObjBOL);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlState, ds.Tables[0]);
            }
            else
            {
                ddlState.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            int ID = Convert.ToInt32(gvDetail.DataKeys[e.NewEditIndex].Values[0]);
            hfId.Value = ID.ToString();
            ObjBOL.Operation = 4;
            ObjBOL.Id = ID;
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                if (ddlMafsiRegion.Items.FindByValue(row["MafsiRegionId"].ToString()) != null)
                {
                    ddlMafsiRegion.SelectedValue = row["MafsiRegionId"].ToString();
                }

                if (ddlCountry.Items.FindByValue(row["CountryID"].ToString()) != null)
                {
                    ddlCountry.SelectedValue = row["CountryID"].ToString();
                    ddlCountry_SelectedIndexChanged();
                    if (ddlState.Items.FindByValue(row["StateId"].ToString()) != null)
                    {
                        ddlState.SelectedValue = row["StateId"].ToString();
                    }
                }

                if (Convert.ToInt32(ds.Tables[0].Rows[0]["Status"]) == 1)
                {
                    rdoStatus.SelectedValue = "1";
                }
                else
                {
                    rdoStatus.SelectedValue = "0";
                }

                txtCity.Text = row["City"].ToString();

                hfTimeStamp.Value = row["Timestamp"].ToString();
                btnSave.Text = "Update";
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvDetail_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(gvDetail.DataKeys[e.RowIndex].Values[0]);
            string TimeStamp = Convert.ToString(gvDetail.DataKeys[e.RowIndex].Values[1]);
            ObjBOL.Operation = 6;
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

            Utility.MaintainLogsSpecial("FrmMafsiRegionDetailMaintenance.aspx", "Delete", ddlMafsiRegion_Lookup.SelectedValue);
            Utility.ShowMessage_Success(Page, "Record deleted successfully!! ");
            returnStatus = ddlMafsiRegion_Lookup.SelectedValue;
            btnCancel_Click();
            if (ddlMafsiRegion_Lookup.Items.FindByValue(returnStatus.Trim()) != null)
            {
                ddlMafsiRegion_Lookup.SelectedValue = returnStatus.Trim();
                ddlMafsiRegion_Lookup_SelectedIndexChanged();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

}