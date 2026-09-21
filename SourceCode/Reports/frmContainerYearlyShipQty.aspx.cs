using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BOLAERO;
using BLLAERO;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

public partial class Reports_frmContainerYearlyShipQty : System.Web.UI.Page
{
    BOLSearchContainer ObjBOL = new BOLSearchContainer();
    BLLManageSearchContainer ObjBLL = new BLLManageSearchContainer();

    BOLStockIn_New ObjBOL_1 = new BOLStockIn_New();
    BLLStockIn_New ObjBLL_1 = new BLLStockIn_New();
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
            ObjBOL_1.Operation = 1;
            ds = ObjBLL_1.Return_DataSet(ObjBOL_1);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Utility.BindDropDownList(ddlSource, ds.Tables[0]);
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = ReportDataZero();                
            rprt.Load(Server.MapPath("~/Reports/rptContainerShipQtyYearlyData.rpt"));
            if (dt.Rows.Count > 0)
            {
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];
                txtheader.Text = "Parts Ship Qty Details";
                rprt.SetDataSource(dt);                           
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            else
            {
                rprt.Load(Server.MapPath("~/Reports/rptNoDataFound.rpt"));
                TextObject txtheader = (TextObject)rprt.ReportDefinition.ReportObjects["txtHeader"];                
                rprt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, false, txtheader.Text);
            }
            
        }
        catch (Exception ex)
        {
            if(ex.ToString() != "Thread was being aborted.")
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
            clscon.Return_DT(dt, "EXEC [IV].[Get_ContainerYearlyShipQty] '" + ddlSource.SelectedValue + "'" );
            if (dt.Rows.Count > 0)
            {
                string basePath = Server.MapPath("~/INV_PartImages/");
                foreach (DataRow row in dt.Rows)
                {
                    string fileName = row["Partimage"].ToString(); // Adjust column name as needed
                    string fullPath = Path.Combine(basePath, fileName);

                    // Optional: Check if file exists
                    if (File.Exists(fullPath))
                        row["Partimage"] = fullPath;
                    else
                        row["Partimage"] = ""; // Or a default image path
                }
            }

        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return dt;
    }   
   
}