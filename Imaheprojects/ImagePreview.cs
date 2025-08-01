

using System.Text.Json;
using System.Text.Json.Serialization; // Required for [JsonPropertyName]

namespace Imahe
{
    /// <summary>
    /// This class represents a single image shown in the UI.
    /// </summary>
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

    /// <summary>
    /// This class holds the result from the Python API for one image.
    /// </summary>
    public class ClassificationResult
    {
        public string Status { get; set; } = "";
        public string Label { get; set; } = "";

        // This now uses the specific ClassificationDetails class, which is much safer.
        public ClassificationDetails? Details { get; set; }

        public string GetDetailsSummary()
        {
            if (Details == null) return "Analysis details not available.";

            try
            {
                switch (Label?.ToLower())
                {
                    case "bad":
                        string reason = Details.Reason ?? "Low quality";
                        return $"⚠️ {reason} (Sharpness: {Details.Sharpness:F1}, Exposure: {Details.Exposure:F1})";

                    case "good":
                        return $"✅ Good quality (Sharpness: {Details.Sharpness:F1}, Exposure: {Details.Exposure:F1})";

                    case "duplicate":
                        return $"🔄 {Details.Message ?? "Duplicate detected"}";

                    case "closed eye":
                        return $"👁️ {Details.Message ?? "Closed eye detected"}";

                    case "error":
                        return $"❌ {Details.Message ?? "An unknown error occurred."}";

                    default:
                        return "Image analyzed";
                }
            }
            catch
            {
                return "Could not read analysis details.";
            }
        }
    }

    /// <summary>
    /// THIS IS THE MISSING PIECE.
    /// This class defines what 'ClassificationDetails' is. The compiler was
    /// looking for this and could not find it.
    /// </summary>
    public class ClassificationDetails
    {
        [JsonPropertyName("sharpness")]
        public double Sharpness { get; set; }

        [JsonPropertyName("exposure")]
        public double Exposure { get; set; }

        [JsonPropertyName("reason")]
        public string? Reason { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}