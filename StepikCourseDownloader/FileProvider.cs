namespace StepikCourseDownloader
{
    public class FileProvider
    {
        public static string Read(string filepath)
        {
            var fileText = string.Empty;

            if (File.Exists(filepath))
            {
                fileText = File.ReadAllText(filepath);
            }

            return fileText;
        }
    }
}