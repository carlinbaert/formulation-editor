using System.Runtime.CompilerServices;

namespace FormulationEditor.WPF.BusinessModel
{
    public class BusinessModelBase<T> : PropertyChangedBase
    {
        public BusinessModelBase(T model)
        {
            Model = model;
        }

        public T Model { get; }

        protected virtual void SetValue<TValue>(TValue value, [CallerMemberName]string propertyName = null)
        {
            typeof(T).GetProperty(propertyName).SetValue(Model, value);
            NotifyPropertyChanged(propertyName);
        }

        protected virtual TValue GetValue<TValue>([CallerMemberName]string propertyName = null)
        {
            return (TValue)typeof(T).GetProperty(propertyName).GetValue(Model);
        }
    }
}
