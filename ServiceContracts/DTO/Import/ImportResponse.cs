namespace ServiceContracts.DTO
{
    public class ImportResponse
    {
        public bool Success { get; set; }

        public int ImportedCount { get; set; }

        public int SkippedCount { get; set; }

        public List<string> Errors { get; set; } = new();
    }
}