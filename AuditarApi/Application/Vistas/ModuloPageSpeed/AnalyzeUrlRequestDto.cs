using System.ComponentModel.DataAnnotations;

namespace AuditarApi.Models.Dtos
{
    public class AnalyzeUrlRequestDto
    {
        [Required]
        [Url]
        public string Url { get; set; }
        [Required]
        public int Id { get; set; }
    }
}
