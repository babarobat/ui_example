namespace Runtime.UIFrameworks.MVVM
{
    public interface IViewModelValue
    {
        void CleanUp();
        void Disconnect();
    }
}