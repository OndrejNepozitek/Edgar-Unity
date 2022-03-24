using System;

namespace Edgar.Unity
{
    /// <summary>
    /// Use this attribute to define a custom connection control for a given connection type.
    /// </summary>
    /// <remarks>
    /// The class that is marked with this attribute must inherit from <see cref="ConnectionControl"/>
    /// and have a parameterless constructor.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CustomConnectionControlAttribute : Attribute
    {
        /// <summary>
        /// Type of connection for which the custom control will be applied.
        /// </summary>
        public Type ConnectionType { get; }

        public CustomConnectionControlAttribute(Type connectionType)
        {
            ConnectionType = connectionType;
        }
    }
}