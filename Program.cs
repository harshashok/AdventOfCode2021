namespace AdventOfCode2021
{
    using System;
    using System.Linq;
    using AdventOfCode2021.Puzzles;
    using System.Reflection;
    class Program
    {
        static void Main(string[] args)
        {
            InvokeSolution("Day6", "2");

            //var t = Assembly.GetEntryAssembly();
            //Console.WriteLine(t.ToString());
        }

        public static void InvokeSolution(string day, string part)
        {
            var method = AppDomain.CurrentDomain.GetAssemblies()
                            .Select(x => x)
                            .Where(x => x.GetName().Name == "AdventOfCode2021")
                            .FirstOrDefault()                                   //Assembly
                            .GetTypes()
                            .Where(x => x.Name.StartsWith(day))                 //Types
                            .SelectMany(x => x.GetMethods())                    //Methods
                            .Where(x => x.IsDefined(typeof(SolutionAttribute)))
                            .Select(x => x)
                            .Where(y => y.GetCustomAttributesData().FirstOrDefault().ConstructorArguments[0].Value.Equals(day)
                                    && y.GetCustomAttributesData().FirstOrDefault().ConstructorArguments[1].Value.Equals(part))
                            .FirstOrDefault();

            object declaringClass = Activator.CreateInstance(method.DeclaringType);
            object returnValueType = Activator.CreateInstance(method.ReturnType);

            returnValueType = method.Invoke(declaringClass, null);

            Console.WriteLine("Result {0} - Part{1} : {2}", day, part, returnValueType.ToString());
        }
    }
 }



