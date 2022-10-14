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
            //AssInfo();
            cleanCall("Day3", "2");
            //TODO : use attributes to find the soln methods and execute reflectively. (from DisplayAttributes)
            //TODO : (extension) Add problem description to the comments and reflectively print it out with options

            // Display information about each module of this assembly.

            //Console.WriteLine("Day 1 - Part 1 : {0}",  new Day1().solve());
            //Console.WriteLine("Day 1 - Part 2 : {0}",  new Day1Part2().solve());
            //Console.WriteLine("Day 2 - Part 1 : {0}",  new Day2().solve());
            //Console.WriteLine("Day 2 - Part 2 : {0}",  new Day2Part2().solve());
            //Console.WriteLine("Day 3 - Part  : {0}", new Day3().solvePart2());
        }

        // Display a formatted string indented by the specified amount.
        public static void Display(Int32 indent, string format, params object[] param)
        {
            Console.Write(new string(' ', indent * 2));
            Console.WriteLine(format, param);
        }

        // Displays the custom attributes applied to the specified member.
        public static void DisplayAttributes(Int32 indent, MemberInfo mi)
        {
            // Get the set of custom attributes; if none exist, just return.
            object[] attrs = mi.GetCustomAttributes(false);
            if (attrs.Length == 0) { return; }

            // Display the custom attributes applied to this member.
            Display(indent + 1, "Attributes:");
            foreach (object o in attrs)
            {
                Display(indent + 2, "{0}", o.ToString());
                if(o.GetType() == typeof(AdventOfCode2021.SolutionAttribute))
                {
                    SolutionAttribute k = (SolutionAttribute)o;
                    Display(indent + 3, "Attrib_Name: " + k.Name);
                    Display(indent + 3, "Attrib_Soln: " + k.Soln);
                }

            }
        }

        public static void Result(string day, string part = "1")
        {
            int indent = 0;
            Console.WriteLine("Solving : Day{0} - Part{1}", day, part);
            Display(indent, "Assemblies Count={0}", AppDomain.CurrentDomain.GetAssemblies().Length.ToString());

        }

        public static void AssInfo()
        {
            // This variable holds the amount of indenting that
            // should be used when displaying each line of information.
            Int32 indent = 0;
            // Display information about the EXE assembly.
            Assembly a = typeof(Program).Assembly;
            Display(indent, "Assembly identity={0}", a.FullName);
            Display(indent + 1, "Codebase={0}", a.Location);

            // Display the set of assemblies our assemblies reference.

            //Display(indent, "Referenced assemblies:");
            //foreach (AssemblyName an in a.GetReferencedAssemblies())
            //{
            //    Display(indent + 1, "Name={0}, Version={1}, Culture={2}, PublicKey token={3}", an.Name, an.Version, an.CultureInfo.Name, (BitConverter.ToString(an.GetPublicKeyToken())));
            //}
            //Display(indent, "");

            // Display information about each assembly loading into this AppDomain.
            Display(indent, "Assemblies Count={0}", AppDomain.CurrentDomain.GetAssemblies().Length.ToString());
            foreach (Assembly b in AppDomain.CurrentDomain.GetAssemblies())
            {
                Display(indent, "Assembly: {0}", b);

                //// Display information about each module of this assembly.
                //foreach (Module m in b.GetModules(true))
                //{
                //    Display(indent + 1, "Module: {0}", m.Name);
                //}

                // Display information about each type exported from this assembly.

                indent += 1;
                foreach (Type t in b.GetExportedTypes())
                {
                    if (t.ToString().Contains("AdventOfCode2021"))
                    {
                        Display(0, "");
                        Display(indent, "Type: {0}", t);

                        //For each type, show its members & their custom attributes.

                        indent += 1;
                        foreach (MemberInfo mi in t.GetMembers())
                        {
                            Display(indent, "Member: {0}", mi.Name);
                            DisplayAttributes(indent, mi);

                            // If the member is a method, display information about its parameters.

                            if (mi.MemberType == MemberTypes.Method)
                            {
                                foreach (ParameterInfo pi in ((MethodInfo)mi).GetParameters())
                                {
                                    Display(indent + 1, "Parameter: Type={0}, Name={1}", pi.ParameterType, pi.Name);
                                }
                            }

                            // If the member is a property, display information about the property's accessor methods.
                            if (mi.MemberType == MemberTypes.Property)
                            {
                                foreach (MethodInfo am in ((PropertyInfo)mi).GetAccessors())
                                {
                                    Display(indent + 1, "Accessor method: {0}", am);
                                }
                            }
                        }
                        indent -= 1;
                    }
                }
            }

        }

        public static void cleanCall(string day, string part)
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

            Console.WriteLine("Result : {0}", returnValueType.ToString());
        }

    }
 }



