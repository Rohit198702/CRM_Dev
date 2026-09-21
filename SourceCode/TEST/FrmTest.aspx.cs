using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TEST_FrmTest : System.Web.UI.Page
{
    commonclass1 cls = new commonclass1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SyncCalendarEvent();
        }
    }

    private void SyncCalendarEvent()
    {
        try
        {
            DataSet ds = new DataSet();
            cls.Return_DS(ds, "EXEC aero_ManageOutlookCalendarData 1, 'J244598' ");

            if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                throw new Exception("Outlook calendar configuration not found.");
            }

            //if (Convert.ToInt32(ds.Tables[0].Rows[0]["IsEnabled"]) == 0)
            //{
            //    return;
            //}

            DataRow row = ds.Tables[0].Rows[0];

            string tenantId = row["TenantId"] == DBNull.Value ? "" : row["TenantId"].ToString().Trim();
            string clientId = row["ClientId"] == DBNull.Value ? "" : row["ClientId"].ToString().Trim();
            string clientSecret = row["ClientSecret"] == DBNull.Value ? "" : row["ClientSecret"].ToString().Trim();

            if (tenantId == "" || clientId == "" || clientSecret == "")
            {
                throw new Exception("Outlook calendar configuration is incomplete.");
            }

            System.Net.ServicePointManager.SecurityProtocol = (System.Net.SecurityProtocolType)3072;

            string accessToken = Utility.GetGraphAccessToken(tenantId, clientId, clientSecret);

            StringBuilder result = new StringBuilder();
            string email = ds.Tables[1].Rows[0]["Email"].ToString();
            string calendarId = ds.Tables[1].Rows[0]["CalendarId"].ToString();

            CalendarFolderInfo obj = GetPublicFolderCalendar(accessToken, email);
            lblInfo.Text = obj == null ? "Calendar not found." : obj.ToString();
        }
        catch (WebException ex)
        {
            Utility.AddEditException(ex);
        }
    }

    public CalendarFolderInfo GetPublicFolderCalendar(string accessToken, string email)
    {
        ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2013_SP1);
        service.Credentials = new OAuthCredentials(accessToken);
        service.Url = new Uri("https://outlook.office365.com/EWS/Exchange.asmx");

        service.ImpersonatedUserId = new ImpersonatedUserId(ConnectingIdType.SmtpAddress, email);

        FolderView view = new FolderView(100);
        view.PropertySet = new PropertySet(BasePropertySet.IdOnly, FolderSchema.DisplayName, FolderSchema.FolderClass);

        Folder calendarFolder = FindPublicFolderCalendar(service, WellKnownFolderName.PublicFoldersRoot, "Shipping Schedule", view);

        if (calendarFolder == null)
        {
            return null;
        }

        return new CalendarFolderInfo
        {
            Id = calendarFolder.Id.UniqueId,
            DisplayName = calendarFolder.DisplayName,
            FolderClass = calendarFolder.FolderClass            
        };
    }

    private Folder FindPublicFolderCalendar(ExchangeService service, FolderId parentFolderId, string calendarName, FolderView view)
    {
        FindFoldersResults folders = service.FindFolders(parentFolderId, view);

        foreach (Folder folder in folders)
        {
            if (string.Equals(folder.DisplayName, calendarName, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(folder.FolderClass, "IPF.Appointment", StringComparison.OrdinalIgnoreCase))
            {
                return folder;
            }

            Folder found = FindPublicFolderCalendar(service, folder.Id, calendarName, view);

            if (found != null)
            {
                return found;
            }
        }

        return null;
    }
}

public class CalendarFolderInfo
{
    private string _id;
    private string _displayName;
    private string _folderClass;
    private int _totalCount;
    private int _childFolderCount;

    public string Id
    {
        get { return _id; }
        set { _id = value; }
    }

    public string DisplayName
    {
        get { return _displayName; }
        set { _displayName = value; }
    }

    public string FolderClass
    {
        get { return _folderClass; }
        set { _folderClass = value; }
    }

    public int TotalCount
    {
        get { return _totalCount; }
        set { _totalCount = value; }
    }

    public int ChildFolderCount
    {
        get { return _childFolderCount; }
        set { _childFolderCount = value; }
    }

    public override string ToString()
    {
        return "Id: " + Id + "<br/>" +
               "Name: " + DisplayName + "<br/>" +
               "Folder Class: " + FolderClass + "<br/>";
    }
}