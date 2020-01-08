using System;

public class AppState
{
    public event Action OnChange;

    public void RefreshState()
    {
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}