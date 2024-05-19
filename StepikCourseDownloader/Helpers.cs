namespace StepikCourseDownloader
{
    internal class Helpers
    {
        public static string RemoveInvalidChars(string filename)
        {
            filename = filename.Replace("/", ", ").Replace("\"", "");

            return string.Concat(filename.Split(Path.GetInvalidFileNameChars()));
        }
    }
}
