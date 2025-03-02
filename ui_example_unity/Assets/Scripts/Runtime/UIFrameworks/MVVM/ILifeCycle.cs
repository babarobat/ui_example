namespace Runtime.UIFrameworks.MVVM
{
    public interface ILifeCycle : IInit, ITick, ICleanUp
    {
        void TurnOn();
        void TurnOff();
    }
}