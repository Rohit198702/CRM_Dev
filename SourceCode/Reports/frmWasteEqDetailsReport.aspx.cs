using BLLAERO;
using BOLAERO;
using System;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;

public partial class Reports_frmWasteEqDetailsReport : System.Web.UI.Page
{
    BOLWasteEq_New ObjBOL = new BOLWasteEq_New();
    BLLWasteEq_New ObjBLL = new BLLWasteEq_New();
    commonclass1 clscon = new commonclass1();
    ReportDocument rprt = new ReportDocument();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                BindControls();
            }
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
            ObjBOL.Operation = 1;
            ds = ObjBLL.Return_DataSet(ObjBOL);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlTrackingNumber, ds.Tables[0]);
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlProject, ds.Tables[1]);
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                Utility.BindDropDownListAll(ddlManufacturer, ds.Tables[2]);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private bool IsNullOrWhiteSpace(string value)
    {
        return value == null || value.Trim().Length == 0;
    }

    private bool ValidationCheck()
    {
        try
        {
            if (ddlFilterDate.SelectedValue == "1")
            {
                if ((!IsNullOrWhiteSpace(txtEstDeliveryDateFrom.Text) && IsNullOrWhiteSpace(txtEstDeliveryDateTo.Text)) ||
                (IsNullOrWhiteSpace(txtEstDeliveryDateFrom.Text) && !IsNullOrWhiteSpace(txtEstDeliveryDateTo.Text)))
                {
                    Utility.ShowMessage_Error(Page, "Please Select Both From And To Est. Delivery Date !");
                    ShowHideDateDivs();
                    return false;
                }
            }

            if(ddlFilterDate.SelectedValue == "2")
            {
                if ((!IsNullOrWhiteSpace(txtReqByShopDateFrom.Text) && IsNullOrWhiteSpace(txtReqByShopDateTo.Text)) ||
                (IsNullOrWhiteSpace(txtReqByShopDateFrom.Text) && !IsNullOrWhiteSpace(txtReqByShopDateTo.Text)))
                {
                    Utility.ShowMessage_Error(Page, "Please Select Both From And To Required By Shop Date !");
                    ShowHideDateDivs();
                    return false;
                }
            }
            if(ddlFilterDate.SelectedValue=="3")
            {
                if ((!IsNullOrWhiteSpace(txtShipDateFrom.Text) && IsNullOrWhiteSpace(txtShipDateTo.Text)) ||
                (IsNullOrWhiteSpace(txtShipDateFrom.Text) && !IsNullOrWhiteSpace(txtShipDateTo.Text)))
                {
                    Utility.ShowMessage_Error(Page, "Please Select Both From And To Ship Date !");
                    ShowHideDateDivs();
                    return false;
                }
            }
            
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return true;
    }

    protected void ddlManufacturer_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlManufacturer_SelectedIndexChanged_Event();
    }

    protected void ddlWasteEq_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlWasteEq_SelectedIndexChanged_Event();
    }

    protected void btnExportToPDF_Click(object sender, EventArgs e)
    {
        try
        {
            GenerateReport();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void HideDivs()
    {
        try
        {
            EstDeli.Style["display"] = "none";
            divReqByShop.Style["display"] = "none";
            divShipDate.Style["display"] = "none";
            txtEstDeliveryDateFrom.Text = String.Empty;
            txtEstDeliveryDateTo.Text = String.Empty;
            txtReqByShopDateFrom.Text = String.Empty;
            txtReqByShopDateTo.Text = String.Empty;
            txtShipDateFrom.Text = String.Empty;
            txtShipDateTo.Text = String.Empty;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ShowHideDateDivs()
    {
        try
        {
            HideDivs();
            if (ddlFilterDate.SelectedValue=="1")
            {
                EstDeli.Style["display"] = "block";
            }
            else if (ddlFilterDate.SelectedValue == "2")
            {
                divReqByShop.Style["display"] = "block";
            }
            else if (ddlFilterDate.SelectedValue == "3")
            {
                divShipDate.Style["display"] = "block";
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
            if (ddlTrackingNumber.Items.Count > 0)
            {
                ddlTrackingNumber.SelectedIndex = 0;
            }

            if (ddlProject.Items.Count > 0)
            {
                ddlProject.SelectedIndex = 0;
            }

            if (ddlManufacturer.Items.Count > 0)
            {
                ddlManufacturer.SelectedIndex = 0;
            }

            if (ddlWasteEq.Items.Count > 0)
            {
                ddlWasteEq.SelectedIndex = 0;
            }

            if (ddlAccessory.Items.Count > 0)
            {
                ddlAccessory.SelectedIndex = 0;
            }

            if (ddlUsedFromStock.Items.Count > 0)
            {
                ddlUsedFromStock.SelectedIndex = 0;
            }

            txtEstDeliveryDateFrom.Text = string.Empty;
            txtEstDeliveryDateTo.Text = string.Empty;

            txtReqByShopDateFrom.Text = string.Empty;
            txtReqByShopDateTo.Text = string.Empty;
            ClearWasteEqList();
            ClearAccessoryList();
            HideDivs();
            if (ddlFilterDate.Items.Count > 0)
            {
                ddlFilterDate.SelectedIndex = 0;
            }
            txtShipDateFrom.Text = String.Empty;
            txtShipDateTo.Text = String.Empty;
            rdbAssessoryReq.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private string PrepareQuery()
    {
        try
        {
            string query = string.Empty;
            query = " SELECT MAIN.id, MAIN.trackingno AS TrackingNo, MAIN.JobID, MAIN.requestondate AS RequestedByShop, CONVERT(VARCHAR, MAIN.acc_recieved, 101) AS ReceivedDate, MAIN.ServiceProvider, ";
            query += " LJ_1.makername AS MakerName, LJ_2.WasteEqName, LJ_3.acc_name AS AccName, MAIN.usedfromstock AS UsedFromStock, CONVERT(VARCHAR, MAIN.estimatdeliverydate, 101) AS EstDeliveryDate, ";
            query += " CONVERT(VARCHAR, PR.ShipDate, 101) AS ShipDate, CASE WHEN [MAIN].AccessoryRequired=1 Then 'Yes' ELSE 'No' End AS [AccRequired], ";
            query += " CONVERT(VARCHAR, MAIN.requestondate, 101) AS RequiredByShop, MAIN.remarks   ";
            query += " FROM tblWasteEquMain MAIN ";
            query += " LEFT JOIN tblAcc_Makers LJ_1 ON LJ_1.id = MAIN.makerid ";
            query += " LEFT JOIN tblWasteEq LJ_2 ON LJ_2.id = MAIN.eqid ";
            query += " LEFT JOIN tblAccessories LJ_3 ON LJ_3.id = MAIN.accid ";
            query += " LEFT JOIN tblProjects PR ON PR.JobID=MAIN.JobID ";
            query += " WHERE MAIN.id IS NOT NULL ";
            if (ddlTrackingNumber.SelectedIndex > 0)
            {
                query += " AND MAIN.TrackingNo = '" + ddlTrackingNumber.SelectedValue + "' ";
            }

            if (ddlProject.SelectedIndex > 0)
            {
                query += " AND MAIN.JobID = '" + ddlProject.SelectedValue + "' ";
            }

            if (ddlManufacturer.SelectedIndex > 0)
            {
                query += " AND MAIN.MakerID = '" + ddlManufacturer.SelectedValue + "' ";
            }

            if (ddlWasteEq.SelectedIndex > 0)
            {
                query += " AND MAIN.EqID = '" + ddlWasteEq.SelectedValue + "' ";
            }

            if (ddlAccessory.SelectedIndex > 0)
            {
                query += " AND MAIN.AccID = '" + ddlAccessory.SelectedValue + "' ";
            }

            if (ddlUsedFromStock.SelectedIndex > 0)
            {
                query += " AND MAIN.UsedFromStock = '" + ddlUsedFromStock.SelectedValue + "' ";
            }

            if(rdbAssessoryReq.SelectedValue == "0" || rdbAssessoryReq.SelectedValue == "1")
            {
                query += " AND MAIN.AccessoryRequired = '" + rdbAssessoryReq.SelectedValue + "' ";
            }

            if (txtEstDeliveryDateFrom.Text.Trim() != "" && txtEstDeliveryDateTo.Text.Trim() != "")
            {
                query += " AND MAIN.EstimatDeliveryDate >= '" + txtEstDeliveryDateFrom.Text + "' ";
                query += " AND MAIN.EstimatDeliveryDate <= '" + txtEstDeliveryDateTo.Text + "' ";
            }

            if (txtReqByShopDateFrom.Text.Trim() != "" && txtReqByShopDateTo.Text.Trim() != "")
            {
                query += " AND MAIN.RequestOnDate >= '" + txtReqByShopDateFrom.Text + "' ";
                query += " AND MAIN.RequestOnDate <= '" + txtReqByShopDateTo.Text + "' ";
            }

            if (txtShipDateFrom.Text.Trim() != "" && txtShipDateTo.Text.Trim() != "")
            {
                query += " AND PR.[ShipDate] >= '" + txtShipDateFrom.Text + "' ";
                query += " AND PR.[ShipDate] <= '" + txtShipDateTo.Text + "' ";
            }

            return query;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return string.Empty;
    }

    private DataTable ReportData()
    {
        DataTable dt = new DataTable();
        try
        {
            string query = PrepareQuery();
            if (query.Length > 1)
            {
                clscon.Return_DT(dt, query);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }

    private void GenerateReport()
    {
        try
        {
            if (!ValidationCheck())
            {
                return;
            }
            DataTable dt = ReportData();
            rprt.Load(Server.MapPath("~/Reports/rptWasteEqDetailsReport.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Waste Equipment Detail Report ";
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Waste Equipment Detail Report ";
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        finally
        {
            rprt.Close();
            rprt.Dispose();
        }
    }

    private void GenerateReport_Excel()
    {
        try
        {
            if (!ValidationCheck())
            {
                return;
            }
            DataTable dt = ReportData();
            rprt.Load(Server.MapPath("~/Reports/rptWasteEqDetailsReport.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Waste Equipment Detail Report ";
                rprt.SetDataSource(dt);
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Waste Equipment Detail Report ";
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.Excel, Response, false, txtheader.Text);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        finally
        {
            rprt.Close();
            rprt.Dispose();
        }
    }

    private void ddlManufacturer_SelectedIndexChanged_Event()
    {
        try
        {
            ClearWasteEqList();
            ClearAccessoryList();
            if (ddlManufacturer.SelectedIndex > 0)
            {
                DataTable dt = new DataTable();
                ObjBOL.Operation = 2;
                ObjBOL.ManufacturerId = Int32.Parse(ddlManufacturer.SelectedValue);
                dt = ObjBLL.Return_DataSet(ObjBOL).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Utility.BindDropDownListAll(ddlWasteEq, dt);
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ddlWasteEq_SelectedIndexChanged_Event()
    {
        try
        {
            ClearAccessoryList();
            if (ddlWasteEq.SelectedIndex > 0)
            {
                DataTable dt = new DataTable();
                ObjBOL.Operation = 3;
                ObjBOL.WasteEqId = Int32.Parse(ddlWasteEq.SelectedValue);
                dt = ObjBLL.Return_DataSet(ObjBOL).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    Utility.BindDropDownListAll(ddlAccessory, dt);
                }
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ClearWasteEqList()
    {
        try
        {
            ddlWasteEq.Items.Clear();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ClearAccessoryList()
    {
        try
        {
            ddlAccessory.Items.Clear();
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnGenerateExcel_Click(object sender, EventArgs e)
    {
        GenerateReport_Excel();
    }
}