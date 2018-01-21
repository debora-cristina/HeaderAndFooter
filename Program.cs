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
                    try
                    {
                        // step 2
                        ITextEvents events = new ITextEvents();
                        PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, msReport);
                        pdfWriter.PageEvent = events;
                        events.Titulo = "Titulo Personalizado";
                        events.LogoCaminho = "C:/Users/Débora/Desktop/logo.jpg";



                        //open the stream 
                        pdfDoc.Open();

                        int contarLinhas = 0;


                        for (int j = 0; j < 36; j++)
                        {
                            Paragraph para = new Paragraph("Hello world. Checking Header Footer" + j);
                            para.Alignment = Element.ALIGN_CENTER;
                            pdfDoc.Add(para);
                            contarLinhas++;
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

 
