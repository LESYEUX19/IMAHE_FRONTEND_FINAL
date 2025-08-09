// In AppState.cs
namespace Imahe; // Ensure this namespace matches your project

// FIXED: Added 'partial' keyword for MAUI/WinRT compatibility.
public partial class AppState : IDisposable
{
    public bool IsProcessing { get; private set; }

    public CancellationTokenSource? ProcessingCancellationTokenSource { get; private set; }

    public event Action? OnChange;

    public void SetIsProcessing(bool isProcessing)
    {
        if (isProcessing)
        {
            // Create a new cancellation source for the new process
            ProcessingCancellationTokenSource = new CancellationTokenSource();
        }
        else
        {
            // Clean up the old source when processing is done
            ProcessingCancellationTokenSource?.Cancel();
            ProcessingCancellationTokenSource?.Dispose();
            ProcessingCancellationTokenSource = null;
        }

        IsProcessing = isProcessing;
        NotifyStateChanged();
    }

    // FIXED: Stray colon was removed from here.

    public void Dispose()
    {
        ProcessingCancellationTokenSource?.Dispose();
        // FIXED: Added this line as requested by the compiler for best practices.
        GC.SuppressFinalize(this);
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}