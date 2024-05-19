using YamlDotNet.Serialization;

namespace StepikCourseDownloader
{
    public static class YamlSerializer
    {
        public static T Read<T>(string filepath)
        {
            var deserializer = new DeserializerBuilder().Build();

            return deserializer.Deserialize<T>(FileProvider.Read(filepath));
        }
    }
}