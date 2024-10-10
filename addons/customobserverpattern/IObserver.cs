using System;

namespace COP
{
    public interface IObserver<T>
  {
    Action<T> Action { get; set; }
  }
}