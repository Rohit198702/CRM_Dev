using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BOLAERO;
using BLLAERO;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using OfficeOpenXml;
using System.IO;
using OfficeOpenXml.Style;

public partial class Reports_frmReqReport : System.Web.UI.Page
{
    BOLRequisition ObjBOL = new BOLRequisition();
    BLLRequisition ObjBLL = new BLLRequisition();
    ReportDocument rprt = new ReportDocument();
    commonclass1 clscon = new commonclass1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Bind_Controls();
        }
    }

    private void Bind_Controls()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 1;
            ds = ObjBLL.GetControlsData(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlPreparedByList, ds.Tables[0]);
            }
            BindRequisitions();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }    

    protected void btnCancel_Click1(object sender, EventArgs e)
    {
        try
        {
            ddlPreparedByList.SelectedIndex = 0;
            if (ddlReq.Items.Count > 0)
            {
                ddlReq.Items.Clear();
            }
            BindRequisitions();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ReportDataZero();
            rprt.Load(Server.MapPath("~/Reports/rptGenerateRequisition.rpt"));
            if (dt.Rows.Count > 0)
            {
                string reqNo = ddlReq.SelectedItem.Text;
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, reqNo);
            }
            else
            {
                Utility.ShowMessage_Error(Page, "No Matching Data Found !!");
            }
        }
        catch (Exception ex)
        {
            if (ex.Message != "Thread was being aborted.")
            {
                Utility.AddEditException(ex);
            }
        }
        finally
        {
            rprt.Close();
            rprt.Dispose();
        }
    }

    private DataTable ReportDataZero()
    {
        DataTable dt = new DataTable();
        try
        {
            clscon.Return_DT(dt, "EXEC [IV].[INV_GenerateRequisition] '" + ddlReq.SelectedValue + "'");
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    protected void ddlPreparedByList_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPreparedByList.SelectedIndex > 0)
            {
                BindPreparedByRequisitions();

            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    public void BindPreparedByRequisitions()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 27;
            if (ddlPreparedByList.SelectedIndex > 0)
            {
                ObjBOL.LoginUserId = Convert.ToInt32(ddlPreparedByList.SelectedValue);
                ds = ObjBLL.GetControlsData(ObjBOL);
                if (ds.Tables[0].Rows.Count > 0)
                {                   
                    ClearDropdown(ddlReq);
                    Utility.BindDropDownList(ddlReq, ds.Tables[0]);
                    if (ddlReq.Items.Count > 0)
                    {
                        ddlReq.SelectedIndex = 0;
                    }
                }
                else
                {
                    if (ddlReq.Items.Count > 0)
                    {
                        ddlReq.Items.Clear();
                    }
                }
            }
            else
            {
                if (ddlReq.Items.Count > 0)
                {
                    ddlReq.Items.Clear();
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void BindRequisitions()
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 26;
            ObjBOL.LoginUserId = Utility.GetCurrentSession().EmployeeID;
            ds = ObjBLL.GetControlsData(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {               
                ClearDropdown(ddlReq);
                Utility.BindDropDownList(ddlReq, ds.Tables[0]);
                if (ddlReq.Items.Count > 0)
                {
                    ddlReq.SelectedIndex = 0;
                }
            }
            else
            {
                if (ddlReq.Items.Count > 0)
                {
                    ddlReq.Items.Clear();
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ClearDropdown(DropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            ddl.SelectedValue = null;
            ddl.Text = null;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }

    }

    protected void ddlReq_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            ObjBOL.Operation = 28;
            ObjBOL.Reqid =Convert.ToInt32(ddlReq.SelectedValue);
            ds = ObjBLL.GetControlsData(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlPreparedByList.SelectedValue = ds.Tables[0].Rows[0]["PreparedBy"].ToString();
            }
            else
            {
                ddlPreparedByList.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnGenerateExcel_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ReportDataZero();
            rprt.Load(Server.MapPath("~/Reports/rptGenerateRequisition.rpt"));
            if (dt.Rows.Count > 0)
            {
                string reqNo = ddlReq.SelectedItem.Text;
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, reqNo);
            }
            else
            {
                Utility.ShowMessage_Error(Page, "No Matching Data Found !!");
            }
        }
        catch (Exception ex)
        {
            if (ex.Message != "Thread was being aborted.")
            {
                Utility.AddEditException(ex);
            }
        }
        finally
        {
            rprt.Close();
            rprt.Dispose();
        }
    }
}