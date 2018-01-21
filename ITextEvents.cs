using iTextSharp.text.pdf; 
using iTextSharp.text; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HeaderAndFooter
{
    class ITextEvents : PdfPageEventHelper
    {
        private String titulo;
        private String logoCaminho;
        // This is the contentbyte object of the writer
        PdfContentByte cb;

        // we will put the final number of pages in a template
        PdfTemplate headerTemplate, footerTemplate;

        // this is the BaseFont we are going to use for the header / footer
        BaseFont bf = null;

        // This keeps track of the creation time
        DateTime PrintTime = DateTime.Now;


        #region Fields
        private string _header;
        #endregion

        #region Properties
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }

        public string Titulo { get => titulo; set => titulo = value; }
        public string LogoCaminho { get => logoCaminho; set => logoCaminho = value; }




        #endregion


        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            try
            {
                PrintTime = DateTime.Now;
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                cb = writer.DirectContent;
                headerTemplate = cb.CreateTemplate(100, 100);
                footerTemplate = cb.CreateTemplate(50, 50);
            }
            catch (DocumentException de)
            {
                //handle exception here
            }
            catch (System.IO.IOException ioe)
            {
                //handle exception here
            }
        }

        public override void OnEndPage(iTextSharp.text.pdf.PdfWriter writer, iTextSharp.text.Document document)
        {
            base.OnEndPage(writer, document);

            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

            iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

       
            Phrase p1Header = new Phrase(Titulo, baseFontNormal);

            //Create PdfTable object
            //PdfPTable pdfTab = new PdfPTable(3);

            //We will have to create separate cells to include image logo and 2 separate strings
            //Row 1
            PdfPCell pdfCell1 = new PdfPCell(createImageCell(logoCaminho));
            //PdfPCell pdfCell2 = new PdfPCell(p1Header);
           // PdfPCell pdfCell3 = new PdfPCell();
            String text = "Page " + writer.PageNumber + " of ";


            
            //Add paging to footer
            {
                cb.BeginText();
                cb.SetFontAndSize(bf, 12);
                cb.SetTextMatrix(document.PageSize.GetRight(180), document.PageSize.GetBottom(20));
                cb.ShowText(text);
                cb.EndText();
                float len = bf.GetWidthPoint(text, 12);
                cb.AddTemplate(footerTemplate, document.PageSize.GetRight(180) + len, document.PageSize.GetBottom(20));
            }

            //set the alignment of all three cells and set border to 0
            pdfCell1.HorizontalAlignment = Element.ALIGN_LEFT;


            pdfCell1.Border = 0;

            PdfPTable table = new PdfPTable(8);
             //le.WidthPercentage = 100;
             table.AddCell(pdfCell1);
             table.AddCell(createCell("Board", 1, 2, PdfPCell.BOX));
             table.AddCell(createCell("Month and Year of Passing", 1, 2, PdfPCell.BOX));
             table.AddCell(createCell("", 1, 1, PdfPCell.ALIGN_TOP));
             table.AddCell(createCell("Marks", 2, 1, PdfPCell.ALIGN_TOP));
             table.AddCell(createCell("Percentage", 1, 2, PdfPCell.BOX));
             table.AddCell(createCell("Class / Grade", 1, 2, PdfPCell.BOX));
             table.AddCell(createCell("", 1, 1, PdfPCell.BOX));
             table.AddCell(createCell("Obtained", 1, 1, PdfPCell.BOX));
             table.AddCell(createCell("Out of", 1, 1, PdfPCell.BOX));
             table.AddCell(createCell("12th / I.B. Diploma", 1, 1, PdfPCell.BOX));
             table.AddCell(createCell("", 1, 1, PdfPCell.BOX));
             table.AddCell(createCell("", 1, 1, PdfPCell.BOX));
             table.AddCell(createCell("Aggregate (all subjects)", 1, 1, PdfPCell.BOX));
             table.AddCell(createCell("", 1, 1, PdfPCell.BOX));
             table.AddCell(createCell("", 1, 1, PdfPCell.BOX));
             table.AddCell(createCell("", 1, 1, PdfPCell.BOX));
             table.AddCell(createCell("", 1, 1, PdfPCell.BOX));
            

             //add all three cells into PdfTable
             pdfCell1.Colspan = 1;
             pdfCell1.FixedHeight = 50;
            // table.AddCell(pdfCell1);
            // pdfTab.AddCell(pdfCell2);            
            // pdfTab.AddCell(pdfCell3);


             table.TotalWidth = document.PageSize.Width - 80f;
             table.WidthPercentage = 70;    


             table.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);


        }

        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);

            headerTemplate.BeginText();
            headerTemplate.SetFontAndSize(bf, 12);
            headerTemplate.SetTextMatrix(0, 0);
            headerTemplate.ShowText((writer.PageNumber).ToString());
            headerTemplate.EndText();

            footerTemplate.BeginText();
            footerTemplate.SetFontAndSize(bf, 12);
            footerTemplate.SetTextMatrix(0, 0);
            footerTemplate.ShowText((writer.PageNumber).ToString());
            footerTemplate.EndText();


        }

        public static PdfPCell createImageCell(String path) {
                Image img = Image.GetInstance(path);
                PdfPCell cell = new PdfPCell(img, true);



            return cell;
       }


        public PdfPCell createCell(String content, int colspan, int rowspan, int border)
        {
            iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

            iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

            PdfPCell cell = new PdfPCell(new Phrase(content, baseFontNormal));
            cell.Colspan = colspan;
            cell.Rowspan = rowspan;
            cell.Border = border;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            return cell;
        }
    }
}

