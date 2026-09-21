using System;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Globalization;

public class InvoicePdfBuilder
{
    //==========================
    // Private Variables
    //==========================

    private Document document;
    private PdfWriter writer;

    private Font TitleFont;
    private Font HeaderFont;
    private Font NormalFont;
    private Font BoldFont;
    private Font SmallFont;

    //==========================
    // Constructor
    //==========================

    public InvoicePdfBuilder(HttpResponse response, string fileName)
    {
        response.Clear();
        response.Buffer = true;
        response.ClearHeaders();
        response.ClearContent();
        response.Cache.SetCacheability(HttpCacheability.NoCache);
        response.ContentType = "application/pdf";
        response.AddHeader("content-disposition", "inline; filename=" + fileName + ".pdf");

        document = new Document(PageSize.A4, 25, 25, 20, 20);

        writer = PdfWriter.GetInstance(document, response.OutputStream);

        document.Open();

        LoadFonts();
    }

    //==========================
    // Load Fonts
    //==========================

    private void LoadFonts()
    {
        TitleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
        HeaderFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 11);
        BoldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
        NormalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
        SmallFont = FontFactory.GetFont(FontFactory.HELVETICA, 8);
    }

    //==================================================
    // Helper Method 1
    //==================================================

    private PdfPCell BlankCell()
    {
        PdfPCell cell = new PdfPCell(new Phrase(""));

        cell.Border = Rectangle.NO_BORDER;

        return cell;
    }

    //==================================================
    // Helper Method 2
    //==================================================

    private PdfPCell Cell(string text, Font font)
    {
        PdfPCell cell = new PdfPCell(new Phrase(text, font));

        cell.Padding = 5;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.HorizontalAlignment = Element.ALIGN_RIGHT;

        return cell;
    }
    private PdfPCell NumberCellWithDoller(decimal amount, Font font)
    {
        PdfPCell cell = new PdfPCell(
            new Phrase("$" + amount.ToString("N2", CultureInfo.GetCultureInfo("en-US")), font));

        cell.Padding = 5;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.HorizontalAlignment = Element.ALIGN_RIGHT;

        return cell;
    }
    private PdfPCell NumberCell(decimal amount, Font font)
    {
        PdfPCell cell = new PdfPCell(
            new Phrase(amount.ToString("N2", CultureInfo.GetCultureInfo("en-US")), font));

        cell.Padding = 5;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.HorizontalAlignment = Element.ALIGN_RIGHT;

        return cell;
    }
    //==================================================
    // Helper Method 3
    //==================================================

    private PdfPCell Cell(string text, Font font, int colspan)
    {
        PdfPCell cell = new PdfPCell(new Phrase(text, font));

        cell.Colspan = colspan;
        cell.Padding = 5;

        return cell;
    }

    //==================================================
    // Helper Method 4
    //==================================================

    private PdfPCell HeaderCell(string text)
    {
        PdfPCell cell = new PdfPCell(new Phrase(text, BoldFont));

        cell.HorizontalAlignment = Element.ALIGN_CENTER;
        cell.VerticalAlignment = Element.ALIGN_MIDDLE;
        cell.BackgroundColor = new BaseColor(233, 236, 239);
        cell.Padding = 5;

        return cell;
    }

    //==================================================
    // Helper Method 5
    //==================================================

    public void Close(HttpResponse response)
    {
        document.Close();

        response.Flush();
        HttpContext.Current.ApplicationInstance.CompleteRequest();
    }


    public void AddHeader(string invoiceNo, string invoiceDate, string invoicePeriod)
    {
        //===========================
        // Invoice Title
        //===========================

        Paragraph title = new Paragraph("INVOICE", TitleFont);
        title.Alignment = Element.ALIGN_CENTER;
        title.SpacingAfter = 20;

        document.Add(title);

        //===========================
        // Company + Invoice Details
        //===========================

        PdfPTable table = new PdfPTable(2);

        table.WidthPercentage = 100;
        table.SetWidths(new float[] { 70, 30 });
        table.SpacingAfter = 15;

        //===========================
        // LEFT COLUMN
        //===========================

        PdfPCell left = new PdfPCell();
        left.Border = Rectangle.NO_BORDER;

        left.AddElement(new Paragraph("Smart Conveyer Design Solutions Pvt. Ltd.", HeaderFont));
        left.AddElement(new Paragraph("Plot #82, Industrial Area", NormalFont));
        left.AddElement(new Paragraph("Phase-9, Mohali, Punjab, India", NormalFont));
        left.AddElement(new Paragraph("Phone : +91 172 5053287", NormalFont));

        table.AddCell(left);

        //===========================
        // RIGHT COLUMN
        //===========================

        PdfPCell right = new PdfPCell();
        right.Border = Rectangle.NO_BORDER;

        Paragraph p = new Paragraph();
        p.Alignment = Element.ALIGN_RIGHT;
        p.Add(new Chunk("Invoice Number : ", BoldFont));
        p.Add(new Chunk(invoiceNo, NormalFont));

        right.AddElement(p);

        right.AddElement(new Paragraph(" "));

        Paragraph d = new Paragraph();
        d.Alignment = Element.ALIGN_RIGHT;
        d.Add(new Chunk("Date : ", BoldFont));
        d.Add(new Chunk(invoiceDate, NormalFont));

        right.AddElement(d);

        table.AddCell(right);

        document.Add(table);

        //===========================
        // Customer
        //===========================

        Paragraph customer = new Paragraph();

        customer.SpacingBefore = 10;

        customer.Add(new Chunk("Customer :\n", BoldFont));
        customer.Add(new Chunk("Aerowerks Inc\n", NormalFont));
        customer.Add(new Chunk("6625 Millcreek, Mississauga\n", NormalFont));
        customer.Add(new Chunk("Ontario, L5N 5M4\n", NormalFont));
        customer.Add(new Chunk("Phone : 905 363 6999", NormalFont));

        document.Add(customer);

        //===========================
        // Invoice Period
        //===========================

        Paragraph period = new Paragraph(
            "Invoice Period : " + invoicePeriod,
            BoldFont);

        period.Alignment = Element.ALIGN_CENTER;
        period.SpacingBefore = 15;
        period.SpacingAfter = 15;

        document.Add(period);
    }

    public void AddInvoiceDetails(string qty1, string qty2, string qty3, string awscharges, string supportchanges, string total1, string total2, string total3, string total4, string total5, string grandTotal, string rate1, string rate2, string rate3)
    {
        PdfPTable table = new PdfPTable(5);

        table.WidthPercentage = 100;
        table.SpacingBefore = 10;

        table.SetWidths(new float[] { 10, 49, 10, 15, 25 });
        table.HeaderRows = 1;

        //---------------- Header ----------------

        table.AddCell(HeaderCell("S.No."));
        table.AddCell(HeaderCell("Project Details"));
        table.AddCell(HeaderCell("Qty"));
        table.AddCell(HeaderCell("Rate"));
        table.AddCell(HeaderCell("Total Amount (US Dollers)"));

        //---------------- Row 1 ----------------

        table.AddCell(Cell("1", NormalFont));

        table.AddCell(LeftCell(
            "Technical Drawings for Proposal",
            NormalFont));

        table.AddCell(Cell(qty1, NormalFont));

        table.AddCell(Cell(rate1, NormalFont));

        table.AddCell(NumberCell(Convert.ToDecimal(total1), NormalFont));

        //---------------- Row 2 ----------------

        table.AddCell(Cell("2", NormalFont));

        table.AddCell(LeftCell("Job Drawings", NormalFont));

        table.AddCell(Cell(qty2, NormalFont));

        table.AddCell(Cell(rate2, NormalFont));

        table.AddCell(NumberCell(Convert.ToDecimal(total2), NormalFont));

        //---------------- Row 3 ----------------

        table.AddCell(Cell("3", NormalFont));

        table.AddCell(LeftCell("Fabrication Drawings", NormalFont));

        table.AddCell(Cell(qty3, NormalFont));

        table.AddCell(Cell(rate3, NormalFont));

        table.AddCell(NumberCell(Convert.ToDecimal(total3), NormalFont));

        //---------------- Row 4 ----------------

        table.AddCell(Cell("4", NormalFont));

        table.AddCell(LeftCell("On Call Advisory Support", NormalFont));

        table.AddCell(Cell("", NormalFont));

        table.AddCell(Cell("", NormalFont));

        table.AddCell(NumberCell(Convert.ToDecimal("1000"), NormalFont));

        //---------------- Row 5 ----------------
        table.AddCell(Cell("5", NormalFont));

        decimal awsAmount = Convert.ToDecimal(awscharges);
        decimal supportAmount = Convert.ToDecimal(supportchanges);

        string awsDetails =
            "AWS\n" +
            "(DB & Cloud Storage Charges: $" + awsAmount.ToString("0.00") +
            "\nSupport Charges: $" + supportAmount.ToString("0.00") + ")";

        table.AddCell(LeftCell(awsDetails, NormalFont));

        table.AddCell(Cell("", NormalFont));

        table.AddCell(Cell("", NormalFont));

        decimal awsTotal = awsAmount + supportAmount;

        table.AddCell(NumberCell(awsTotal, NormalFont));


        //---------------- Grand Total ----------------

        PdfPCell totalCell =
            new PdfPCell(new Phrase("TOTAL", BoldFont));

        totalCell.Colspan = 4;
        totalCell.PaddingRight = 10;

        table.AddCell(totalCell);

        table.AddCell(NumberCellWithDoller(Convert.ToDecimal(grandTotal), BoldFont));

        //---------------- Amount in Words ----------------

        PdfPCell words =
            new PdfPCell(
                new Phrase(
                    "US Dollars : " + NumberToWords.Convert(Convert.ToDecimal(grandTotal)),
                    BoldFont));

        words.Colspan = 5;
        words.HorizontalAlignment = Element.ALIGN_CENTER;

        table.AddCell(words);

        document.Add(table);
    }

    private PdfPCell LeftCell(string text, Font font)
    {
        PdfPCell cell = Cell(text, font);
        cell.HorizontalAlignment = Element.ALIGN_LEFT;
        return cell;
    }

    public static class NumberToWords
    {
        private static string[] ones = {
        "", "One", "Two", "Three", "Four", "Five", "Six", "Seven",
        "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen",
        "Fourteen", "Fifteen", "Sixteen", "Seventeen",
        "Eighteen", "Nineteen"
    };

        private static string[] tens = {
        "", "", "Twenty", "Thirty", "Forty", "Fifty",
        "Sixty", "Seventy", "Eighty", "Ninety"
    };

        public static string Convert(decimal amount)
        {
            long number = (long)Math.Floor(amount);

            return ConvertNumber(number) + " Only";
        }

        private static string ConvertNumber(long number)
        {
            if (number == 0)
                return "Zero";

            if (number < 20)
                return ones[number];

            if (number < 100)
                return tens[number / 10] +
                       ((number % 10 > 0) ? " " + ones[number % 10] : "");

            if (number < 1000)
                return ones[number / 100] + " Hundred" +
                       ((number % 100 > 0) ? " " + ConvertNumber(number % 100) : "");

            if (number < 1000000)
                return ConvertNumber(number / 1000) + " Thousand" +
                       ((number % 1000 > 0) ? " " + ConvertNumber(number % 1000) : "");

            if (number < 1000000000)
                return ConvertNumber(number / 1000000) + " Million" +
                       ((number % 1000000 > 0) ? " " + ConvertNumber(number % 1000000) : "");

            return ConvertNumber(number / 1000000000) + " Billion" +
                   ((number % 1000000000 > 0) ? " " + ConvertNumber(number % 1000000000) : "");
        }
    }
    public void AddSignature(string imagePath)
    {
        PdfPTable table = new PdfPTable(1);
        table.WidthPercentage = 100;
        table.SpacingBefore = 20f;

        PdfPCell cell = new PdfPCell();
        cell.Border = Rectangle.NO_BORDER;
        cell.PaddingTop = 2f;
        cell.PaddingBottom = 2f;
        cell.UseAscender = true;
        cell.UseDescender = true;

        // Company Name
        Paragraph p = new Paragraph("For Smart Conveyer Design Solutions Pvt. Ltd.", NormalFont);
        p.Alignment = Element.ALIGN_LEFT;
        p.SetLeading(14f, 0f);
        p.SpacingAfter = 5f;
        cell.AddElement(p);

        // Signature Image
        if (System.IO.File.Exists(imagePath))
        {
            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
            img.ScaleAbsolute(180f, 70f);
            img.Alignment = Element.ALIGN_LEFT;
            img.SpacingBefore = 2f;
            img.SpacingAfter = 5f;

            cell.AddElement(img);
        }

        // Signatory
        p = new Paragraph("Prateek Saurabh Kalsi", BoldFont);
        p.Alignment = Element.ALIGN_LEFT;
        p.SetLeading(14f, 0f);
        p.SpacingAfter = 2f;
        cell.AddElement(p);

        // Designation
        p = new Paragraph("Manager-Engineering", NormalFont);
        p.Alignment = Element.ALIGN_LEFT;
        p.SetLeading(14f, 0f);
        p.SpacingAfter = 15f;
        cell.AddElement(p);

        // Approval Text
        p = new Paragraph(
            "The above Invoice is as per approval of Balbir Singh (President of Aerowerks Inc)",
            BoldFont);
        p.Alignment = Element.ALIGN_CENTER;
        p.SetLeading(14f, 0f);

        cell.AddElement(p);

        table.AddCell(cell);
        document.Add(table);
    }
}