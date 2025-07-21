namespace Imahe; // Ensure this namespace matches your project

public class AppState : IDisposable
{
    public bool IsProcessing { get; private set; }

    // ✅ NEW: The CancellationTokenSource is now shared globally.
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
            ProcessingCancellationTokenSource?.Dispose();
            ProcessingCancellationTokenSource = null;
        }

        IsProcessing = isProcessing;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();

    public void Dispose()
    {
        ProcessingCancellationTokenSource?.Dispose();
    }
}