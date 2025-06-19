namespace MessageFlow.Frontend.Services.Authentication
{
    public class SessionExpiredNotifier
    {
        public event Action? OnSessionExpired;

        public void Trigger()
        {
            OnSessionExpired?.Invoke();
        }
    }
}