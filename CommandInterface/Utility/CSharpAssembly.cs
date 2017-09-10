using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.Reflection;


namespace CommandInterface.Utility
{
    public class ScriptExecutionException : Exception
    {
        public ScriptExecutionException(string fileName, Microsoft.CodeAnalysis.Emit.EmitResult emitResult) 
            : base(MakeMessage(fileName, emitResult))
        { }

        private static string MakeMessage(string fileName, Microsoft.CodeAnalysis.Emit.EmitResult emitResult)
        {
            var errorMessages = new StringBuilder();
            foreach (var diag in emitResult.Diagnostics)
            {
                errorMessages.AppendLine($"  - {diag.Id}");
                errorMessages.AppendLine($"    {diag.GetMessage()}");
                errorMessages.AppendLine($"    {diag.Location}");
            }

            var sb = new StringBuilder();
            sb.AppendLine(
                $@"ScriptExecutionException:
{errorMessages.ToString()}
");
            return sb.ToString();
        }
    }

    public static class CSharpAssembly
    {
        public static ReturnType ExecuteScriptFromFile<ReturnType>(FileInfo fileInfo)
        {
            if(!fileInfo.Exists)
            {
                throw new ScriptNotFoundException();
            }

            var text = File.ReadAllText(fileInfo.FullName);
            var syntaxTree = CSharpSyntaxTree.ParseText(text);
            CSharpCompilation compilation = CSharpCompilation.Create(
                "ScriptAssembly",
                new[] { syntaxTree },
                new[] {
                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(CommandManager).Assembly.Location)
                },
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            using (var dll = new MemoryStream())
            using (var pdb = new MemoryStream())
            {
                var emitResult = compilation.Emit(dll, pdb);
                if (emitResult.Success)
                {
                    var assembly = Assembly.Load(dll.ToArray(), pdb.ToArray());
                    var types = assembly.GetTypes();
                    var targetClass = types[0];
                    var targetMethods = targetClass.GetMethods();
                    var methodBase = targetClass.GetMethod("Main", BindingFlags.Static | BindingFlags.Public);

                    return (ReturnType)methodBase.Invoke(null, null);
                }
                else
                {
                    throw new ScriptExecutionException("None", emitResult);
                }
            }
        }
    }
}
