namespace StepikCourseDownloader
{
    public class Program
    {
        static async Task Main()
        {
            var htmlStyle = File.ReadAllText(@"styles\style.css");

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
            var summary = course["summary"].ToString();
            summary = HtmlHelpers.AddStyle(summary, htmlStyle);
            var summaryPdf = PdfHelpers.ConvertHtmlToPdf(summary);
            File.WriteAllBytes($"{courseTitle}\\Краткое содержимое курса.pdf", summaryPdf);

            await Console.Out.WriteLineAsync($"Описание курса");
            var description = course["description"].ToString();
            description = HtmlHelpers.AddStyle(description, htmlStyle);
            var descriptionPdf = PdfHelpers.ConvertHtmlToPdf(description);
            File.WriteAllBytes($"{courseTitle}\\Описание курса.pdf", descriptionPdf);

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
                            lessonHTML = HtmlHelpers.AddStyle(lessonHTML, htmlStyle);
                            var lessonPdf = PdfHelpers.ConvertHtmlToPdf(lessonHTML);
                            File.WriteAllBytes($"{sectionDir}\\{sectionNum}.{unitNum}.{stepNum} {lessonTitle}.pdf", lessonPdf);

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