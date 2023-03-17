namespace CI_Platform.Entities.ViewModels
{
    public class MissionDocumentVM
    {
        public long MissionDocumentId { get; set; }

        public long MissionId { get; set; }

        public string? DocumentName { get; set; }

        public string? DocumentType { get; set; }

        public string? DocumentPath { get; set; }
    }
}
