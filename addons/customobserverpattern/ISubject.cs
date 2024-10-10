namespace COP
{
    public interface ISubject<T>
  {
    void Attach(IObserver<T> observer);
    void Detach(IObserver<T> observer);
    void OnNext(T value);
    }
}