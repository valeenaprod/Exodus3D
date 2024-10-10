using System;

namespace COP
{
    public class Observer<T> : IObserver<T>
  {
    public Observer(Action<T> action)
    {
      this.Action = action;
    }

    public Action<T> Action { get; set; }
  }
}