namespace MassiveDynamic.Configs
{
    public class UploadConfig
    {
        public const string Section = "Upload";

        public int MaxFileSizeMB { get; set; }

        public string[] AllowedExtensions { get; set; }
    }
}
