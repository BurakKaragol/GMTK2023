using System;

namespace MrLule.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ConsoleCommandAttribute : Attribute
    {
        public string code;
        public string[] parameters;
        public bool getAll;

        public ConsoleCommandAttribute(string code)
        {
            this.code = code;
            this.parameters = null;
        }

        public ConsoleCommandAttribute(string code, string[] parameters, bool getAll = false)
        {
            this.code = code;
            this.parameters = parameters;
            this.getAll = getAll;
        }
    }
}
