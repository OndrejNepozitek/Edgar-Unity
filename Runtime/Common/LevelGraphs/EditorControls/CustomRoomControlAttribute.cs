using System;

namespace Edgar.Unity
{
    /// <summary>
    /// Use this attribute to define a custom room control for a given room type.
    /// </summary>
    /// <remarks>
    /// The class that is marked with this attribute must inherit from <see cref="RoomControl"/>
    /// and have a parameterless constructor.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CustomRoomControlAttribute : Attribute
    {
        /// <summary>
        /// Type of room for which the custom control will be applied.
        /// </summary>
        public Type RoomType { get; }

        public CustomRoomControlAttribute(Type roomType)
        {
            RoomType = roomType;
        }
    }
}