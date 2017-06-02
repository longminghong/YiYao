using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Reflection;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Diagnostics;

namespace MultiChartDemo
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {

        private Dictionary<string, object> values;

        public ViewModelBase()
        {
            this.values = new Dictionary<string, object>();
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        // Unfortunately, C# currently does not support property delegates, in other words, a reference to a property. 
        private string GetPropertyName(LambdaExpression lambda)
        {
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }
            var constantExpression = memberExpression.Expression as ConstantExpression;
            var propertyInfo = memberExpression.Member as PropertyInfo;
            return propertyInfo.Name;
        }

        protected void SetValue<T>(Expression<Func<T>> property, T value)
        {

            LambdaExpression lambda = property as LambdaExpression;

            if (lambda == null)
                throw new ArgumentException("Invalid view model property definition.");

            string propertyName = this.GetPropertyName(lambda);

            T existingValue = GetValueInternal<T>(propertyName);

            if (!object.Equals(existingValue, value))
            {
                this.values[propertyName] = value;
                this.OnPropertyChanged(propertyName);
            }
        }

        protected T GetValue<T>(Expression<Func<T>> property)
        {
            LambdaExpression lambda = property as LambdaExpression;

            if (lambda == null)
                throw new ArgumentException("Invalid view model property definition.");

            string propertyName = this.GetPropertyName(lambda);

            return GetValueInternal<T>(propertyName);

        }

        private T GetValueInternal<T>(string propertyName)
        {
            object value;
            if (values.TryGetValue(propertyName, out value))
                return (T)value;
            else
                return default(T);
        }
    }

}
