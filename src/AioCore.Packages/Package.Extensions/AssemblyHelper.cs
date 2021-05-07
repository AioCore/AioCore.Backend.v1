using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Package.Extensions
{
    public static class AssemblyHelper
    {
        private static Assembly[] _assemblies;
        private static Type[] _exportTypes;
        private static readonly object Locker = new();

        public static IEnumerable<Assembly> Assemblies
        {
            get
            {
                if (_assemblies != null) return _assemblies;
                lock (Locker)
                {
                    _assemblies = GetAssemblies().ToArray();
                }
                return _assemblies;
            }
        }

        public static IEnumerable<Type> ExportTypes
        {
            get
            {
                if (_exportTypes != null) return _exportTypes;
                lock (Locker)
                {
                    _exportTypes = Assemblies.SelectMany(asm => asm.GetExportedTypes().Where(t => !t.IsAbstract && !t.IsInterface)).ToArray();
                }
                return _exportTypes;
            }
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            var assembly = Assembly.GetEntryAssembly();
            yield return assembly;
            if (assembly is null) yield break;
            foreach (var asm in assembly.GetReferencedAssemblies().Where(t => t.FullName.StartsWith("AioCore")))
                yield return Assembly.Load(asm);
        }
    }
}