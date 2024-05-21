using OpenHtmlToPdf;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace StepikCourseDownloader
{
    internal class PdfHelpers
    {
        public static byte[] ConvertHtmlToPdf(string html)
        {
            return Pdf
                .From(html)
                .EncodedWith("utf-8")
                .OfSize(PaperSize.A4)
                .Content();
        }

        public static void MergePdfFiles(string[] fileNames, string outputFileName)
        {
            using (var outputDocument = new PdfDocument())
            {
                foreach (var file in fileNames)
                {
                    var inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);
                    var count = inputDocument.PageCount;

                    for (var i = 0; i < count; i++)
                    {
                        PdfPage page = inputDocument.Pages[i];
                        outputDocument.AddPage(page);
                    }
                }

                outputDocument.Save(outputFileName);
            }
        }
    }
}
