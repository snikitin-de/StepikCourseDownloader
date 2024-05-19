
namespace StepikCourseDownloader
{
    public class Program
    {
        static async Task Main()
        {
            var apiHost = "https://stepik.org";
            var authUrl = apiHost + "/oauth2/token/";

            var config = YamlSerializer.Read<Configuration>("config.yaml");
            var auth = new Authorization(authUrl, config.ClientId, config.ClientSecret);
            var token = await auth.GetToken();
            var fetch = new Fetch(apiHost, token);

            await Console.Out.WriteLineAsync("Введите идентификатор курса:");
            var courseId = Convert.ToInt32(Console.ReadLine());
            var course = await fetch.FetchObject("course", courseId);
            var courseTitle = course["title"].ToString();

            if (!Directory.Exists(courseTitle))
            {
                Directory.CreateDirectory(courseTitle);
            }

            await Console.Out.WriteLineAsync($"\nСкачивание курса \"{courseTitle}\"...");

            await Console.Out.WriteLineAsync($"Краткое содержимое курса");
            File.WriteAllText($"{courseTitle}\\Краткое содержимое курса.html", course["summary"].ToString());
            await Console.Out.WriteLineAsync($"Описание курса");
            File.WriteAllText($"{courseTitle}\\Описание курса.html", course["description"].ToString());

            var sections = await fetch.FetchObjects("section", course["sections"]);
            var sectionNum = 1;

            foreach (var section in sections)
            {
                var unitIds = section["units"];
                var units = await fetch.FetchObjects("unit", unitIds);
                var sectionTitle = section["title"].ToString().Trim();
                var unitNum = 1;

                string sectionDir = $"{courseTitle}\\{sectionNum} {sectionTitle}";

                if (!Directory.Exists(sectionDir))
                {
                    Directory.CreateDirectory(sectionDir);
                }

                await Console.Out.WriteLineAsync($"{sectionNum} {sectionTitle}");

                foreach (var unit in units)
                {
                    var lessonNum = unit["lesson"];
                    var lesson = await fetch.FetchObject("lesson", lessonNum);
                    var lessonTitle = Helpers.RemoveInvalidChars(lesson["title"].ToString());

                    var stepIds = lesson["steps"];
                    var steps = await fetch.FetchObjects("step", stepIds);
                    var stepNum = 1;

                    await Console.Out.WriteLineAsync($"{sectionNum}.{unitNum} {lessonTitle}");

                    foreach (var step in steps)
                    {
                        if (step["block"]["name"].ToString() == "text")
                        {
                            var lessonHTML = step["block"]["text"].ToString();

                            File.WriteAllText($"{sectionDir}\\{sectionNum}.{unitNum}.{stepNum} {lessonTitle}.html", lessonHTML);

                            stepNum++;
                        }
                    }

                    unitNum++;
                }

                sectionNum++;
            }
        }
    }
}