using BLLAERO;
using BOLAERO;
using System;
using System.Data;

public partial class ContactManagement_FrmInstallers : System.Web.UI.Page
{
    BOLInstallers ObjBOL = new BOLInstallers();
    BLLInstallers ObjBLL = new BLLInstallers();

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
            DataSet ds = new DataSet();
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlInstallerLookupList, ds.Tables[0]);
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

    protected void ddlInstallerLookupList_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlInstallerLookupList_SelectedIndexChanged();
    }

    private void ddlInstallerLookupList_SelectedIndexChanged()
    {
        try
        {
            if (ddlInstallerLookupList.SelectedIndex > 0)
            {
                btnSaveDetail.Enabled = true;
                btnSave.Text = "Update";
                ObjBOL.Operation = 5;
                ObjBOL.ID = Int32.Parse(ddlInstallerLookupList.SelectedValue);
                DataSet ds = ObjBLL.Return_DataSet(ObjBOL);
                if (ds.Tables[0].Rows.Count == 1)
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    txtCompanyName.Text = dr["CompanyName"].ToString();
                    txtAddress.Text = dr["Address"].ToString();

                    if (ddlCountry.Items.FindByValue(dr["CountryId"].ToString()) != null)
                    {
                        ddlCountry.SelectedValue = dr["CountryId"].ToString();
                        ddlCountry_SelectedIndexChanged();

                        if (ddlState.Items.FindByValue(dr["StateId"].ToString()) != null)
                        {
                            ddlState.SelectedValue = dr["StateId"].ToString();
                        }
                        else
                        {
                            if (ddlState.Items.Count > 0)
                            {
                                ddlState.SelectedIndex = 0;
                            }
                        }
                    }
                    else
                    {
                        ddlCountry.SelectedIndex = 0;
                    }

                    txtCity.Text = dr["City"].ToString();
                    txtZipCode.Text = dr["ZipCode"].ToString();
                    txtBankDetails.Text = dr["BankDetails"].ToString();
                    hfTimeStamp.Value = dr["TimeStamp"].ToString();
                    if (ddlStatus.Items.FindByValue(dr["StatusId"].ToString()) != null)
                    {
                        ddlStatus.SelectedValue = dr["StatusId"].ToString();
                    }
                    else
                    {
                        ddlStatus.SelectedValue = "0";
                    }
                }

                BindContacts();
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        btnSave_Click();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            btnCancel_Click();
            hfTimeStamp.Value = "-1";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }       
    }

    private void btnCancel_Click()
    {
        try
        {
            if (ddlInstallerLookupList.Items.Count > 0)
            {
                ddlInstallerLookupList.SelectedIndex = 0;
            }
            txtCompanyName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            ddlCountry.SelectedIndex = 0;
            ddlState.Items.Clear();
            txtCity.Text = string.Empty;
            txtZipCode.Text = string.Empty;
            txtBankDetails.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            btnSave.Text = "Save";
            btnSaveDetail.Enabled = false;
            gvDetail.DataSource = string.Empty;
            gvDetail.DataBind();
            btnCancelDetail_Click();
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
            if (ddlCountry.SelectedIndex > 0)
            {
                ObjBOL.Operation = 2;
                ObjBOL.CountryID = Int32.Parse(ddlCountry.SelectedValue);
                DataSet ds = ObjBLL.Return_DataSet(ObjBOL);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    Utility.BindDropDownList(ddlState, ds.Tables[0]);
                }
                else
                {
                    ddlState.Items.Clear();
                }
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

    private bool ValidationCheck()
    {
        try
        {
            if (txtCompanyName.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter company name !");
                txtCompanyName.Focus();
                return false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    private void btnSave_Click()
    {
        try
        {
            if (ValidationCheck())
            {
                string message = "Record inserted successfully !!";
                string opType = "save";
                if (ddlInstallerLookupList.SelectedIndex > 0)
                {
                    ObjBOL.Operation = 4;
                    ObjBOL.ID = Int32.Parse(ddlInstallerLookupList.SelectedValue);
                    if(hfTimeStamp.Value != "-1" && hfTimeStamp.Value.Trim() != "")
                    {
                        ObjBOL.TimeStamp = long.Parse(hfTimeStamp.Value);
                    }                    
                    message = "Record updated successfully !!";
                    opType = "update";
                }
                else
                {
                    ObjBOL.Operation = 3;
                }

                ObjBOL.CompanyName = txtCompanyName.Text;
                ObjBOL.Address = txtAddress.Text;

                if (ddlCountry.SelectedIndex > 0)
                {
                    ObjBOL.CountryID = Int32.Parse(ddlCountry.SelectedValue);
                }

                if (ddlState.SelectedIndex > 0)
                {
                    ObjBOL.StateID = Int32.Parse(ddlState.SelectedValue);
                }

                ObjBOL.City = txtCity.Text;
                ObjBOL.ZipCode = txtZipCode.Text;
                ObjBOL.BankDetails = txtBankDetails.Text;
                ObjBOL.StatusID = Int32.Parse(ddlStatus.SelectedValue);
                string returnStatus = ObjBLL.Return_String(ObjBOL);
                if(returnStatus.Trim() == "erts")
                {
                    Utility.ShowMessage_Error(Page, Utility.ConcurrencyErrorMessage());
                    return;
                }
                if (returnStatus.Trim() == "")
                {
                    return;
                }

                if (returnStatus.Trim() == "ER01")
                {
                    Utility.ShowMessage_Error(Page, "Installer already exists !!");
                    return;
                }

                Utility.ShowMessage_Success(Page, message);
                Utility.MaintainLogsSpecial("FrmInstallers.aspx", opType, returnStatus);

                BindControls();
                ddlInstallerLookupList.SelectedValue = returnStatus;
                ddlInstallerLookupList_SelectedIndexChanged();
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
            if (ddlInstallerLookupList.SelectedIndex > 0)
            {
                ObjBOL.Operation = 6;
                ObjBOL.ID = Int32.Parse(ddlInstallerLookupList.SelectedValue);
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

    protected void btnSaveDetail_Click(object sender, EventArgs e)
    {
        btnSaveDetail_Click();
    }

    private bool ValidationCheckDetail()
    {
        try
        {
            if (ddlInstallerLookupList.SelectedIndex == 0)
            {
                Utility.ShowMessage_Error(Page, "Please select Installer !!");
                return false;
            }

            if (txtFirstName.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "Please enter First Name !");
                txtFirstName.Focus();
                return false;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    private void btnSaveDetail_Click()
    {
        try
        {
            if (ValidationCheckDetail())
            {
                ObjBOL.Operation = 7;
                ObjBOL.ID = Int32.Parse(ddlInstallerLookupList.SelectedValue);
                string message = "Record inserted successfully !!";
                string opType = "save-contact";
                if (txtId.Text.Trim().Length > 0)
                {
                    ObjBOL.Operation = 8;
                    ObjBOL.ID = Int32.Parse(txtId.Text);
                    if(hfTimeStampDetail.Value != "-1")
                    {
                        ObjBOL.TimeStampDetail = long.Parse(hfTimeStampDetail.Value);
                    }                    
                    message = "Record updated successfully !!";
                    opType = "update-contact";
                }
                ObjBOL.Title = txtTitle.Text;
                ObjBOL.FirstName = txtFirstName.Text;
                ObjBOL.LastName = txtLastName.Text;
                ObjBOL.Extension = txtExtension.Text;
                ObjBOL.Phone = txtPhone.Text;
                ObjBOL.Cell = txtCell.Text;
                ObjBOL.Email = txtEmail.Text;

                string returnStatus = ObjBLL.Return_String(ObjBOL);
                if(returnStatus.Trim() == "erts")
                {
                    Utility.ShowMessage_Error(Page, Utility.ConcurrencyErrorMessage());
                    return;
                }
                if (returnStatus.Trim().Length > 0)
                {
                    Utility.ShowMessage_Success(Page, message);
                    Utility.MaintainLogsSpecial("FrmInstallers.aspx", opType, returnStatus);
                    btnCancelDetail_Click();
                    BindContacts();
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnCancelDetail_Click(object sender, EventArgs e)
    {
        try
        {
            btnCancelDetail_Click();
            hfTimeStampDetail.Value = "-1";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }        
    }

    private void btnCancelDetail_Click()
    {
        try
        {
            txtId.Text = string.Empty;
            txtTitle.Text = string.Empty;
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtExtension.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtCell.Text = string.Empty;
            txtEmail.Text = string.Empty;
            btnSaveDetail.Text = "Save Contact";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvDetail_RowDeleting(object sender, System.Web.UI.WebControls.GridViewDeleteEventArgs e)
    {
        try
        {
            int ID = Convert.ToInt32(gvDetail.DataKeys[e.RowIndex].Values[0]);
            string TimeStamp = Convert.ToString(gvDetail.DataKeys[e.RowIndex].Values[1]);
            ObjBOL.Operation = 10;
            ObjBOL.ID = ID;
            if (TimeStamp != "")
            {
                ObjBOL.TimeStampDetail = long.Parse(TimeStamp);
            }
            string returnStatus = ObjBLL.Return_String(ObjBOL);
            if (returnStatus.Trim() == "erts")
            {
                Utility.ShowMessage_Error(Page, Utility.ConcurrencyErrorMessage());
                return;
            }
            if (returnStatus.Trim() == "")
            {
                //Utility.ShowMessage_Error(Page, "Task already sent to Nesting !!");
                return;
            }

            Utility.ShowMessage_Success(Page, "record Deleted Successfully !!");
            Utility.MaintainLogsSpecial("FrmInstallers.aspx", "Delete-contact", ddlInstallerLookupList.SelectedValue);
            btnCancelDetail_Click();
            BindContacts();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvDetail_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            int ID = Convert.ToInt32(gvDetail.DataKeys[e.NewEditIndex].Values[0]);
            string TimeStamp= Convert.ToString(gvDetail.DataKeys[e.NewEditIndex].Values[1]);
            txtId.Text = ID.ToString();
            ObjBOL.Operation = 9;
            ObjBOL.ID = ID;
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                txtTitle.Text = row["Title"].ToString();
                txtFirstName.Text = row["FirstName"].ToString();
                txtLastName.Text = row["LastName"].ToString();
                txtExtension.Text = row["Extension"].ToString();
                txtPhone.Text = row["Phone"].ToString();
                txtCell.Text = row["Cell"].ToString();
                txtEmail.Text = row["Email"].ToString();
                hfTimeStampDetail.Value= row["TimeStamp"].ToString();
                btnSaveDetail.Text = "Update Contact";
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}