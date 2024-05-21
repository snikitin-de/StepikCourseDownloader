using OpenHtmlToPdf;

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
    }
}
