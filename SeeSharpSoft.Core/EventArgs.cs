using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeeSharpSoft
{
    public delegate void EventHandler<TSender, TEventArgs>(TSender sender, TEventArgs e) where TEventArgs : EventArgs where TSender : new();

    public class EventArgs<T> : EventArgs
    {
        public static EventArgs<T> create(T value) {
            return new EventArgs<T>(value);
        }

        public T Value { set; get; }

        public EventArgs(T value)
            : base()
        {
            Value = value;
        }
    }

    public class ChangeEventArgs<T> : EventArgs
    {
        public T OldValue { set; get; }
        public T NewValue { set; get; }

        public ChangeEventArgs(T oldValue, T newValue)
            : base()
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
