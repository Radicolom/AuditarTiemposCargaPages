using Microsoft.EntityFrameworkCore;

namespace AuditarApi.Models.Dtos
{
	/// <summary>
	/// Objeto de Transferencia de Datos (DTO) que contiene los resultados procesados
	/// del análisis de Google PageSpeed Insights.
	/// </summary>
	public class PageSpeedResultDto
	{
		public string AnalyzedUrl { get; set; }
		public int PerformanceScore { get; set; }

		// --- Core Web Vitals y Métricas Modernas ---
		public Metric FirstContentfulPaint { get; set; }
		public Metric LargestContentfulPaint { get; set; }
		public Metric CumulativeLayoutShift { get; set; }
		public Metric SpeedIndex { get; set; }

		// --- Métricas de Tiempo para tu Base de Datos (en milisegundos) ---
		public int? PageLoadTimeMs { get; set; }
		public int? TimeToFirstByteMs { get; set; }
		public int? DomProcessingTimeMs { get; set; }
	}

	/// <summary>
	/// Representa una métrica individual con su título, valor visible y puntuación.
	/// Esta es la versión actualizada.
	/// </summary>
	public class Metric
	{
		public string Title { get; set; }        // Ej: "First Contentful Paint"
		public string DisplayValue { get; set; } // Ej: "1.2 s"
		public double Score { get; set; }        // Ej: 95.0
	}
}