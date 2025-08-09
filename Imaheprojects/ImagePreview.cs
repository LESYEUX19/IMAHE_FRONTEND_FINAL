using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components.Forms;

namespace Imahe // Make sure this namespace matches your project's namespace
{
    public class ImagePreview
    {
        public string ImageDataUrl { get; set; } = string.Empty;
        public ClassificationResult? ClassificationResult { get; set; }
        public bool IsProcessing { get; set; } = false;
        public bool HasError { get; set; } = false;
        public string FileName { get; set; } = string.Empty;
        public long FileSize { get; set; } = 0;
        public string ErrorMessage { get; set; } = string.Empty;
        public IBrowserFile? File { get; set; }
    }

    public class FaceDetail
    {
        [JsonPropertyName("reason")]
        public string Reason { get; set; } = string.Empty;
        [JsonPropertyName("count")]
        public int Count { get; set; } = 0;
    }

    public class ClassificationDetails
    {
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("sharpness")]
        public float? Sharpness { get; set; }
        [JsonPropertyName("exposure")]
        public float? Exposure { get; set; }
        [JsonPropertyName("face_details")]
        public List<FaceDetail>? FaceDetails { get; set; }
    }

    // FIXED: This class is now simpler to match the new backend logic.
    public class ClassificationResult
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("label")]
        public string Label { get; set; } = string.Empty;

        [JsonPropertyName("details")]
        public ClassificationDetails Details { get; set; } = new();

        public string GetDetailsSummary()
        {
            if (Details != null && !string.IsNullOrEmpty(Details.Message))
            {
                return Details.Message;
            }
            return $"Classification: {Label}";
        }
    }
}