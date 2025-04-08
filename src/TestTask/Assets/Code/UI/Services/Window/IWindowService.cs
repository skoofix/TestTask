namespace Code.UI.Services.Window
{
    public interface IWindowService
    {
        void Open(WindowId windowId);
        void Close(WindowId windowId);
    }
}