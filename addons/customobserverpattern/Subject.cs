using System.Collections.Generic;

namespace COP
{
  internal class Subject<T> : ISubject<T>
  {
    private List<IObserver<T>> observers = new List<IObserver<T>>();

    public void Attach(IObserver<T> observer) => this.observers.Add(observer);

    public void Detach(IObserver<T> observer) => this.observers.Remove(observer);

    public void OnNext(T value)
    {
      foreach (var observer in observers)
      {
        observer.Action(value);
      }
    }

  }
}