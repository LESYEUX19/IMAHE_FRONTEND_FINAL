using System.Text.Json;

// Define the namespace to match your project
namespace Imahe
{
    public class ImagePreview
    {
        public string ImageDataUrl { get; set; } = "";
        public ClassificationResult? ClassificationResult { get; set; }
        public bool IsProcessing { get; set; }
        public bool HasError { get; set; }
        public string FileName { get; set; } = "";
        public long FileSize { get; set; }
        public string ErrorMessage { get; set; } = "";
    }

    public class ClassificationResult
    {
        public string Status { get; set; } = "";
        public string Label { get; set; } = "";
        public JsonElement Details { get; set; }

        public string GetDetailsString()
        {
            if (Details.ValueKind == JsonValueKind.String)
            {
                return Details.GetString() ?? "";
            }
            else if (Details.ValueKind == JsonValueKind.Object)
            {
                try
                {
                    return JsonSerializer.Serialize(Details, new JsonSerializerOptions { WriteIndented = true });
                }
                catch
                {
                    return Details.ToString();
                }
            }
            return Details.ToString();
        }

        public string GetDetailsSummary()
        {
            if (Details.ValueKind == JsonValueKind.Object)
            {
                try
                {
                    switch (Label.ToLower())
                    {
                        case "bad":
                            var badSummary = "";
                            if (Details.TryGetProperty("reason", out var reason))
                            {
                                badSummary = reason.GetString() ?? "Low quality image";
                            }
                            if (Details.TryGetProperty("sharpness", out var sharpness) &&
                                Details.TryGetProperty("exposure", out var exposure))
                            {
                                if (!string.IsNullOrEmpty(badSummary))
                                    badSummary += $" (Sharpness: {sharpness.GetDouble():F1}, Exposure: {exposure.GetDouble():F1})";
                                else
                                    badSummary = $"Sharpness: {sharpness.GetDouble():F1}, Exposure: {exposure.GetDouble():F1}";
                            }
                            return string.IsNullOrEmpty(badSummary) ? "Low quality image" : badSummary;
                        case "good":
                            if (Details.TryGetProperty("sharpness", out var goodSharpness) &&
                                Details.TryGetProperty("exposure", out var goodExposure))
                                return $"✅ Good quality (Sharpness: {goodSharpness.GetDouble():F1}, Exposure: {goodExposure.GetDouble():F1})";
                            return "✅ Good quality image";
                        case "duplicate":
                            if (Details.TryGetProperty("message", out var message))
                                return $"🔄 {message.GetString() ?? "Duplicate detected"}";
                            return "🔄 Duplicate detected";
                        case "closed eye":
                            return "👁️ Closed eye detected";
                        default:
                            return "Image analyzed";
                    }
                }
                catch
                {
                    return "Image analyzed";
                }
            }
            return Details.ToString();
        }
    }
}