namespace StepikCourseDownloader
{
    internal class HtmlHelpers
    {
        public static string AddStyle(string html, string style)
        {
            return $@"
                <html>
                <style>
                {style}
                </style>
                <body>
                {html}
                </body>
                </html>";
        }
    }
}
