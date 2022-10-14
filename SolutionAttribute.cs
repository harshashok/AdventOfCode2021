using System;

namespace AdventOfCode2021
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class SolutionAttribute : Attribute
    {
        public string Name { get; set; }
        public string Soln { get; set; }

        public SolutionAttribute(string name, string soln)
        {
            this.Name = name;
            this.Soln = soln;
        }
    }
}

