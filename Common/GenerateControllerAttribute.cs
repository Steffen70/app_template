using System;

namespace Common
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class GenerateControllerAttribute : Attribute
    {
        public Type Dto { get; set; }
        public GenerateControllerAttribute(Type dto)
        {
            Dto = dto;
        }
    }
}