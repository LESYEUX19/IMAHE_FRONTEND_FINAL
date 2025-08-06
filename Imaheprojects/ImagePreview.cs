using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Imahe // Make sure this namespace matches your project's namespace
{
    // No changes needed to ImagePreview, FaceDetail, or ClassificationDetails classes
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
        public string Reason { get; set; } = string.Empty;
        public int Count { get; set; } = 0;
    }

    public class ClassificationDetails
    {
        public string? Message { get; set; }
        public float? Sharpness { get; set; }
        public float? Exposure { get; set; }
        [JsonPropertyName("face_details")]
        public List<FaceDetail>? FaceDetails { get; set; }
    }

    // --- START OF THE FIX ---
    // The only changes are inside the GetDetailsSummary() method below
    public class ClassificationResult
    {
        public string Status { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public ClassificationDetails Details { get; set; } = new();

        public string GetDetailsSummary()
        {
            if (Details == null)
            {
                return "No details available.";
            }

            switch (Label?.ToLower())
            {
                case "good":
                    return $"Image quality is good. (Sharpness: {Details.Sharpness:F2}, Exposure: {Details.Exposure:F2})";

                case "bad":
                    return $"Image has poor quality. (Sharpness: {Details.Sharpness:F2}, Exposure: {Details.Exposure:F2})";

                // This is the corrected logic.
                // We now handle "flagged" and "closed eye" as two separate cases.
                case "flagged":
                    // For "Flagged", we ALWAYS show the generic message, regardless of the underlying reason.
                    return "Image was flagged for manual review.";

                case "closed eye":
                    // For "Closed Eye", we show the specific details.
                    if (Details.FaceDetails != null && Details.FaceDetails.Count > 0)
                    {
                        var faceDetail = Details.FaceDetails.First();
                        var reason = faceDetail.Reason?.Replace("_", " ") ?? "unknown issue";
                        var plural = faceDetail.Count > 1 ? "s" : "";
                        return $"Detected {faceDetail.Count} face{plural} with {reason}.";
                    }
                    // This is a fallback in case the details are missing for some reason.
                    return "Image has a closed eye issue.";

                case "duplicate":
                    return Details.Message ?? "Duplicate image detected.";

                case "error":
                    return Details.Message ?? "An unknown error occurred.";

                default:
                    return Details.Message ?? "No classification summary available.";
            }
        }
    }
    // --- END OF THE FIX ---
}