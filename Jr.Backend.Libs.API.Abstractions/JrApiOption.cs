namespace Jr.Backend.Libs.API.Abstractions
{
    public class JrApiOption : IJrApiOption
    {
        public int MajorVersion { get; set; } = 1;
        public int MinorVersion { get; set; } = 0;

        public string HeaderApiVersionReader { get; set; } = "x-api-version";
        public string GroupNameFormat { get; set; } = "'v'VVV";

        public string Title { get; set; } = "Api Title";
        public int DefaultVersion { get; set; } = 1;
        public string Email { get; set; } = "";
        public string Uri { get; set; } = "";

        public string Description { get; set; } = "";
    }
}