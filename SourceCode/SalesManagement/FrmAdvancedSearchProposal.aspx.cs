using BLLAERO;
using BOLAERO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesManagement_FrmAdvancedSearchProposal : System.Web.UI.Page
{
    commonclass1 cls = new commonclass1();
    BOLProposalSearch ObjBOL = new BOLProposalSearch();
    BLLProposalSearch ObjBLL = new BLLProposalSearch();
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public class SearchFilters
    {
        // Date
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Price
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public decimal? EqualPrice { get; set; }

        // Status
        public string Status { get; set; }

        // Location
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        // Manager
        public string ProjectManager { get; set; }

        public int OrderFor { get; set; }
        public string Consultant { get; set; }
        public string Dealer { get; set; }
        public string SalesRep { get; set; }
        public string Industry { get; set; }
        public string PrimeSpec { get; set; }
        public string Model { get; set; }
        public string ProjectName { get; set; }
        public string PNumber { get; set; }
        public string JNumber { get; set; }
    }

    public string ParseSearch(string input)
    {
        input = input.ToLower();

        var f = new SearchFilters();

        ParseDate(ref input, f);
        ParsePrice(ref input, f);
        ParseManager(ref input, f);
        ParseStatus(ref input, f);
        ParseLocation(ref input, f);
        ParseOrderFor(ref input, f);
        ParseConsultant(ref input, f);
        ParseDealer(ref input, f);
        ParseSalesRep(ref input, f);
        ParseIndustry(ref input, f);
        ParsePrimeSpec(ref input, f);
        ParseModel(ref input, f);
        ParseProjectName(ref input, f);
        ParsePNumber(ref input, f);
        ParseJNumber(ref input, f);

        string whereClause = BuildWhereClause(f, input);
        return whereClause;
    }

    public string BuildWhereClause(SearchFilters f, string input)
    {
        StringBuilder where = new StringBuilder("");

        // DATE
        if (f.StartDate.HasValue)
        {
            where.Append(" AND proposalDate >= '" + f.StartDate.Value.ToString("MM/dd/yyyy") + "'");
        }

        if (f.EndDate.HasValue)
        {
            where.Append(" AND proposalDate <= '" + f.EndDate.Value.ToString("MM/dd/yyyy") + "'");
        }

        // PRICE
        if (f.MinPrice.HasValue)
        {
            where.Append(" AND Price >= " + f.MinPrice.Value.ToString());
        }

        if (f.MaxPrice.HasValue)
        {
            where.Append(" AND Price <= " + f.MaxPrice.Value.ToString());
        }

        if (f.EqualPrice.HasValue)
        {
            where.Append(" AND Price = " + f.EqualPrice.Value.ToString());
        }

        // STATUS
        if (!string.IsNullOrEmpty(f.Status))
        {
            where.Append(" AND tblProjects.JobID IS NULL ");
        }

        // LOCATION
        if (!string.IsNullOrEmpty(f.Country))
        {
            where.Append(" AND REPLACE(tblPFiles.Country, ' ', '') = '" + f.Country + "'");
        }

        if (!string.IsNullOrEmpty(f.State))
        {
            where.Append(" AND REPLACE(tblStates.State, ' ', '') = '" + f.State + "'");
        }

        if (!string.IsNullOrEmpty(f.City))
        {
            where.Append(" AND REPLACE(tblPFiles.City, ' ', '') = '" + f.City + "'");
        }

        // MANAGER
        if (f.ProjectManager.Trim() != "")
        {
            where.Append(" AND (ISNULL(tblEmployees.FirstName, '') + ISNULL(tblEmployees.LastName, '')) LIKE '%" + f.ProjectManager + "%'");
        }

        //ORDER BELONGS TO AEROWERKS, TRAGENFLEX
        if (f.OrderFor > 0)
        {
            where.Append(" AND tblPFiles.OrderBelongsTo = " + f.OrderFor);
        }

        if (!string.IsNullOrEmpty(f.Consultant))
        {
            where.Append(" AND REPLACE(tblConsultants.CompanyName, ' ', '') LIKE '%" + f.Consultant + "%'");
        }

        if (!string.IsNullOrEmpty(f.Dealer))
        {
            where.Append(" AND REPLACE(tblDealers.CompanyName, ' ', '') LIKE '%" + f.Dealer + "%'");
        }

        if (!string.IsNullOrEmpty(f.SalesRep))
        {
            where.Append(" AND (ISNULL(tblHobartListing.FirstName, '') + ISNULL(tblHobartListing.LastName, '')) LIKE '%" + f.SalesRep + "%'");
        }

        if (!string.IsNullOrEmpty(f.Industry))
        {
            where.Append(" AND tblIndustry.[name] LIKE '%" + f.Industry + "%'");
        }

        if (!string.IsNullOrEmpty(f.PrimeSpec))
        {
            where.Append(" AND tblCompetitor.CompetitorName LIKE '%" + f.PrimeSpec + "%'");
        }

        if (!string.IsNullOrEmpty(f.Model))
        {
            where.Append(" AND ma.Models LIKE '%" + f.Model + "%'");
        }

        if (!string.IsNullOrEmpty(f.ProjectName))
        {
            where.Append(" AND tblPFiles.ProjectName LIKE '%" + f.ProjectName + "%'");
        }

        if (!string.IsNullOrEmpty(f.PNumber))
        {
            where.Append(" AND tblPFiles.PNumber LIKE '%" + f.PNumber + "%'");
        }

        if (!string.IsNullOrEmpty(f.JNumber))
        {
            where.Append(" AND tblProjects.JobID LIKE '%" + f.JNumber + "%'");
        }


        return where.ToString();
    }

    private void ParseDate(ref string input, SearchFilters f)
    {
        try
        {
            if (TryParseBetween(ref input, f))
            {
                return;
            }

            if (TryParseKeyword(ref input, f))
            {
                return;
            }

            if (TryParseLast(ref input, f))
            {
                return;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private bool TryParseBetween(ref string input, SearchFilters f)
    {
        try
        {
            var match = Regex.Match(input, @"between\s+(.+?)\s+and\s+(.+)");

            if (!match.Success)
            {
                return false;
            }

            DateTime start;
            DateTime end;

            if (DateTime.TryParse(match.Groups[1].Value, out start) &&
                DateTime.TryParse(match.Groups[2].Value, out end))
            {
                f.StartDate = start;
                f.EndDate = end;

                // REMOVE
                input = Regex.Replace(input, Regex.Escape(match.Value), " ");

                return true;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return false;
    }

    private bool TryParseKeyword(ref string input, SearchFilters f)
    {
        try
        {
            var now = DateTime.Now;

            if (input.ToLower().Contains("last year"))
            {
                var start = new DateTime(now.Year - 1, 1, 1);
                var end = new DateTime(now.Year, 1, 1).AddTicks(-1);
                SetRange(f, start, end);

                input = Regex.Replace(input, @"\blast year\b", " ");

                return true;
            }

            else if (input.ToLower().Contains("last month"))
            {
                var firstDayCurrentMonth = new DateTime(now.Year, now.Month, 1);

                var start = firstDayCurrentMonth.AddMonths(-1);
                var end = firstDayCurrentMonth.AddTicks(-1);

                SetRange(f, start, end);

                input = Regex.Replace(input, @"\blast month\b", " ");

                return true;
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
        return false;
    }

    private bool TryParseLast(ref string input, SearchFilters f)
    {
        try
        {
            var match = Regex.Match(input,
                @"(last|past|previous)\s*(\d+)?\s*(day|week|month|year)s?");

            if (!match.Success) return false;

            int value = string.IsNullOrEmpty(match.Groups[2].Value)
                ? 1
                : int.Parse(match.Groups[2].Value);

            string unit = match.Groups[3].Value.ToLower();

            DateTime now = DateTime.Now;
            DateTime start;
            DateTime end;

            if (unit == "day")
            {
                start = now.AddDays(-value);
                end = now;
            }
            else if (unit == "week")
            {
                start = now.AddDays(-7 * value);
                end = now;
            }
            else if (unit == "month")
            {
                DateTime firstDayCurrentMonth = new DateTime(now.Year, now.Month, 1);

                start = firstDayCurrentMonth.AddMonths(-value);
                end = now;
            }
            else if (unit == "year")
            {
                DateTime firstDayCurrentYear = new DateTime(now.Year, 1, 1);

                start = firstDayCurrentYear.AddYears(-value);
                end = now;
            }
            else
            {
                return false;
            }

            SetRange(f, start, end);

            input = Regex.Replace(input, Regex.Escape(match.Value), " ");
            return true;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
            return false;
        }
    }

    private void SetRange(SearchFilters f, DateTime start, DateTime end)
    {
        f.StartDate = start;
        f.EndDate = end;
    }

    private void ParseStatus(ref string input, SearchFilters f)
    {
        try
        {
            var match = Regex.Match(input, @"\b(open)\b");

            if (match.Success)
            {
                f.Status = match.Value;

                input = Regex.Replace(input, @"\b(open)\b", " ");
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private void ParsePrice(ref string input, SearchFilters f)
    {
        try
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            input = input.ToLower();

            decimal value1;

            // equal
            Match equal = Regex.Match(input, @"(equal)\s*(\d+(\.\d+)?)");
            if (equal.Success)
            {
                f.EqualPrice = decimal.Parse(equal.Groups[2].Value, System.Globalization.CultureInfo.InvariantCulture);

                input = input.Replace(equal.Value, " ");
                return;
            }

            // UNDER / BELOW / LESS THAN
            Match under = Regex.Match(input, @"(below)\s*(\d+(\.\d+)?)");
            if (under.Success)
            {
                value1 = decimal.Parse(under.Groups[2].Value, System.Globalization.CultureInfo.InvariantCulture);
                f.MaxPrice = value1;

                input = input.Replace(under.Value, " ");
                return;
            }

            // ABOVE / MORE THAN / GREATER THAN
            Match above = Regex.Match(input, @"(above)\s*(\d+(\.\d+)?)");
            if (above.Success)
            {
                value1 = decimal.Parse(above.Groups[2].Value, System.Globalization.CultureInfo.InvariantCulture);
                f.MinPrice = value1;

                input = input.Replace(above.Value, " ");
                return;
            }

            return;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
            return;
        }
    }

    private void ParseManager(ref string input, SearchFilters f)
    {
        try
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            input = input.ToLower();
            f.ProjectManager = "";
            // "my"
            if (Regex.IsMatch(input, @"\bmy\b"))
            {
                f.ProjectManager = Utility.GetCurrentSession().EmployeeName;
                input = Regex.Replace(input, @"\bmy\b", " ");
                return;
            }

            // "pm name"
            Match pm = Regex.Match(input, @"\bpm\s+([a-z]+)");
            if (pm.Success)
            {
                f.ProjectManager = pm.Groups[1].Value.Trim();
                input = input.Replace(pm.Value, " ");
                return;
            }

            return;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
            return;
        }
    }

    private void ParseLocation(ref string input, SearchFilters f)
    {
        try
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            input = input.ToLower();

            // COUNTRY
            Match country = Regex.Match(input, @"\bcountry\s+([a-zA-Z]+)");
            if (country.Success)
            {
                string raw = country.Groups[1].Value.Trim().ToLower();
                f.Country = NormalizeCountry(raw);
                input = input.Replace(country.Value, " ");
            }

            // STATE
            Match state = Regex.Match(input, @"\bstate\s+([a-zA-Z]+)");

            if (state.Success)
            {
                f.State = state.Groups[1].Value.Trim();
                input = input.Replace(state.Value, " ");
            }

            // CITY
            Match city = Regex.Match(input, @"\bcity\s+([a-zA-Z]+)");
            if (city.Success)
            {
                f.City = city.Groups[1].Value.Trim();
                input = input.Replace(city.Value, " ");
            }

            return;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
            return;
        }
    }

    private void ParseOrderFor(ref string input, SearchFilters f)
    {
        try
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            input = input.ToLower();

            Match orderFor = Regex.Match(input, @"order\s*for\s*([a-zA-Z]+)", RegexOptions.IgnoreCase);
            if (orderFor.Success)
            {
                string company = orderFor.Groups[1].Value.ToLower();

                int value1 = 0;

                if (company.StartsWith("a"))
                {
                    value1 = 1;
                }
                else if (company.StartsWith("t"))
                {
                    value1 = 2;
                }

                f.OrderFor = value1;

                input = input.Replace(orderFor.Value, " ");
                return;
            }

            return;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
            return;
        }
    }

    private void ParseConsultant(ref string input, SearchFilters f)
    {
        try
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            input = input.ToLower();

            Match value = Regex.Match(input, @"consultant\s*([a-zA-Z]+)", RegexOptions.IgnoreCase);
            if (value.Success)
            {
                string company = value.Groups[1].Value.ToLower();

                f.Consultant = company;

                input = input.Replace(value.Value, " ");
                return;
            }

            return;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
            return;
        }
    }

    private void ParseDealer(ref string input, SearchFilters f)
    {
        try
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            input = input.ToLower();

            Match value = Regex.Match(input, @"dealer\s*([a-zA-Z]+)", RegexOptions.IgnoreCase);
            if (value.Success)
            {
                string company = value.Groups[1].Value.ToLower();

                f.Dealer = company;

                input = input.Replace(value.Value, " ");
                return;
            }

            return;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
            return;
        }
    }

    private void ParseSalesRep(ref string input, SearchFilters f)
    {
        try
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            input = input.ToLower();

            Match value = Regex.Match(input, @"rep \s*([a-zA-Z]+)", RegexOptions.IgnoreCase);
            if (value.Success)
            {
                string company = value.Groups[1].Value.ToLower();

                f.SalesRep = company;

                input = input.Replace(value.Value, " ");
                return;
            }

            return;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
            return;
        }
    }

    private void ParseIndustry(ref string input, SearchFilters f)
    {
        try
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            input = input.ToLower();

            Match value = Regex.Match(input, @"industry \s*([a-zA-Z]+)", RegexOptions.IgnoreCase);
            if (value.Success)
            {
                string company = value.Groups[1].Value.ToLower();

                f.Industry = company;

                input = input.Replace(value.Value, " ");
                return;
            }

            return;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
            return;
        }
    }

    private void ParsePrimeSpec(ref string input, SearchFilters f)
    {
        try
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            input = input.ToLower();

            Match value = Regex.Match(input, @"primespec \s*([a-zA-Z]+)", RegexOptions.IgnoreCase);
            if (value.Success)
            {
                string company = value.Groups[1].Value.ToLower();

                f.PrimeSpec = company;

                input = input.Replace(value.Value, " ");
                return;
            }

            return;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
            return;
        }
    }

    private void ParseModel(ref string input, SearchFilters f)
    {
        try
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            input = input.ToLower();

            Match value = Regex.Match(input, @"model \s*([a-zA-Z]+)", RegexOptions.IgnoreCase);
            if (value.Success)
            {
                string company = value.Groups[1].Value.ToLower();

                f.Model = company;

                input = input.Replace(value.Value, " ");
                return;
            }

            return;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
            return;
        }
    }

    private void ParseProjectName(ref string input, SearchFilters f)
    {
        try
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            input = input.ToLower();

            Match value = Regex.Match(input, @"pn \s*([a-zA-Z]+)", RegexOptions.IgnoreCase);
            if (value.Success)
            {
                string company = value.Groups[1].Value.ToLower();

                f.ProjectName = company;

                input = input.Replace(value.Value, " ");
                return;
            }

            return;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
            return;
        }
    }

    private void ParsePNumber(ref string input, SearchFilters f)
    {
        try
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            input = input.ToLower();

            Match value = Regex.Match(input, @"p# \s*([a-zA-Z0-9]+)", RegexOptions.IgnoreCase);
            if (value.Success)
            {
                string company = value.Groups[1].Value.ToLower();

                f.PNumber = company;

                input = input.Replace(value.Value, " ");
                return;
            }

            return;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
            return;
        }
    }

    private void ParseJNumber(ref string input, SearchFilters f)
    {
        try
        {
            if (string.IsNullOrEmpty(input))
            {
                return;
            }

            input = input.ToLower();

            Match value = Regex.Match(input, @"j# \s*([a-zA-Z0-9]+)", RegexOptions.IgnoreCase);
            if (value.Success)
            {
                string company = value.Groups[1].Value.ToLower();

                f.JNumber = company;

                input = input.Replace(value.Value, " ");
                return;
            }

            return;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
            return;
        }
    }

    private static string NormalizeCountry(string input)
    {
        if (input == "us" || input == "usa")
        {
            return "UnitedStates";
        }

        return System.Globalization.CultureInfo.InvariantCulture.TextInfo.ToTitleCase(input);
    }

    protected void btnSearchProposal_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtKeyword.Text.Trim() == "")
            {
                Utility.ShowMessage_Error(Page, "please enter keywords! ");
                return;
            }
            string whereClause = ParseSearch(txtKeyword.Text);
            DataSet ds = new DataSet();
            ObjBOL.Operation = 1;
            ObjBOL.SearchVar = whereClause;
            if (Utility.IsAuthorized())
            {
                ObjBOL.UserId = Utility.GetCurrentUser();
            }

            ObjBOL.UserInput = txtKeyword.Text;
            ds = ObjBLL.GetAdvancedProposalSearch(ObjBOL);
            int count = ds.Tables[0].Rows.Count;
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblRecordsCount.Text = "Total No. of Records:" + ds.Tables[0].Rows.Count.ToString();
                lblRecordsCount.Visible = true;

                gvProposalSearch.DataSource = ds;
                gvProposalSearch.DataBind();
                btnExportToExcel.Enabled = true;

                ViewState["dirState"] = ds.Tables[0];
                gvProposalSearch.Visible = true;               
            }
            else
            {
                lblRecordsCount.Text = "No Record Found.";
                lblRecordsCount.Visible = true;

                gvProposalSearch.DataSource = "";
                gvProposalSearch.DataBind();
                btnExportToExcel.Enabled = false;               
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void gvProposalSearch_Sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            DataTable dtrslt = (DataTable)ViewState["dirState"];
            if (dtrslt.Rows.Count > 0)
            {
                DataView dataView = new DataView(dtrslt);
                dataView.Sort = e.SortExpression + " " + ConvertSortDirectionToSql(e.SortDirection);
                gvProposalSearch.DataSource = dataView;
                gvProposalSearch.DataBind();
            }
            else
            {
                dtrslt.DefaultView.Sort = e.SortExpression + "DESC";
                gvProposalSearch.DataSource = dtrslt;
                gvProposalSearch.DataBind();
            }
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    private string GridViewSortDirection
    {
        get { return ViewState["SortDirection"] as string ?? "DESC"; }
        set { ViewState["SortDirection"] = value; }
    }

    private string ConvertSortDirectionToSql(SortDirection sortDirection)
    {
        switch (GridViewSortDirection)
        {
            case "ASC":
                GridViewSortDirection = "DESC";
                break;

            case "DESC":
                GridViewSortDirection = "ASC";
                break;
        }
        return GridViewSortDirection;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            txtKeyword.Text = string.Empty;
            gvProposalSearch.DataSource = string.Empty;
            gvProposalSearch.DataBind();
            lblRecordsCount.Text = string.Empty;
            lblRecordsCount.Visible = false;
            btnExportToExcel.Enabled = false;
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }

    protected void btnExportToExcel_Click(object sender, EventArgs e)
    {
        try
        {
            Utility.ExportToExcelGrid(gvProposalSearch, "Search Proposals");
        }
        catch (Exception ex)
        {
            Utility.AddEditException(ex);
        }
    }
}