using System;

namespace Mvb.Core.Components.BinderActions
{
    public interface IBinderActions
    {
        void Add(Action action);
        void Invoke();
    }
}