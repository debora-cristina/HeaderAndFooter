using System;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Drawing;
using System.IO;

namespace HeaderAndFooter
{
    class Program
    {
        public static object MessageBox { get; private set; }

        static void Main(string[] args)
        {
            CreatePDF();
        }

        static void CreatePDF()
        {
            string fileName = string.Empty;

            DateTime fileCreationDatetime = DateTime.Now;


            fileName = string.Format("{0}.pdf", fileCreationDatetime.ToString(@"yyyyMMdd") + "_" + fileCreationDatetime.ToString(@"HHmmss"));

            string pdfPath = "output.pdf";

            using (FileStream msReport = new FileStream(pdfPath, FileMode.Create))
            {
                //step 1
                using (Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 140f, 10f))
                {
                    // step 2
                    ITextEvents events = new ITextEvents();
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, msReport);
                    pdfWriter.PageEvent = events;
                    events.Titulo = "Titulo Personalizado";
                    events.LogoCaminho = "C:/Users/Débora/Desktop/logo.jpg";
                    try
                    {
                      

                        //open the stream 
                        pdfDoc.Open();

                        int contarLinhas = 0;


                        for (int j = 0; j < 36; j++)
                        {
                            PdfPTable table = new PdfPTable(8);
                            //le.WidthPercentage = 100;
                            PdfPCell pdfCell1 = new PdfPCell(events.createImageCell("C:/Users/Débora/Desktop/logo.jpg"));
                            table.AddCell(pdfCell1);
                            table.AddCell(events.createCell("Board", 1, 2, PdfPCell.BOX));
                            table.AddCell(events.createCell("Month and Year of Passing", 1, 2, PdfPCell.BOX));
                            table.AddCell(events.createCell("", 1, 1, PdfPCell.ALIGN_TOP));
                            table.AddCell(events.createCell("Marks", 2, 1, PdfPCell.ALIGN_TOP));
                            table.AddCell(events.createCell("Percentage", 1, 2, PdfPCell.BOX));
                            table.AddCell(events.createCell("Class / Grade", 1, 2, PdfPCell.BOX));
                            table.AddCell(events.createCell("", 1, 1, PdfPCell.BOX));
                            table.AddCell(events.createCell("Obtained", 1, 1, PdfPCell.BOX));
                            table.AddCell(events.createCell("Out of", 1, 1, PdfPCell.BOX));
                            table.AddCell(events.createCell("12th / I.B. Diploma", 1, 1, PdfPCell.BOX));
                            table.AddCell(events.createCell("", 1, 1, PdfPCell.BOX));
                            table.AddCell(events.createCell("", 1, 1, PdfPCell.BOX));
                            table.AddCell(events.createCell("Aggregate (all subjects)", 1, 1, PdfPCell.BOX));
                            table.AddCell(events.createCell("", 1, 1, PdfPCell.BOX));
                            table.AddCell(events.createCell("", 1, 1, PdfPCell.BOX));
                            table.AddCell(events.createCell("", 1, 1, PdfPCell.BOX));
                            table.AddCell(events.createCell("", 1, 1, PdfPCell.BOX));


                            //add all three cells into PdfTable
                            pdfCell1.Colspan = 1;
                            pdfCell1.FixedHeight = 50;
                            // table.AddCell(pdfCell1);
                            // pdfTab.AddCell(pdfCell2);            
                            // pdfTab.AddCell(pdfCell3);

                            contarLinhas++;


                            table.TotalWidth = pdfDoc.PageSize.Width - 80f;
                            table.WidthPercentage = 70;


                            table.WriteSelectedRows(0, -1, 40, pdfDoc.PageSize.Height - 200, pdfWriter.DirectContent);
                            if (contarLinhas == 36 / 2)
                            {
                                pdfDoc.NewPage();
                            }
                        }


                        pdfDoc.Close();

                        System.Diagnostics.Process.Start("output.pdf");

                    }
                    catch (Exception ex)
                    {
                        //handle exception
                    }

                    finally
                    {


                    }
                }

            }
        }


    }

}

 
