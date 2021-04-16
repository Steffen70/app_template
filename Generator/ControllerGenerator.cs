using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace Generator
{
    [Generator]
    public class ControllerGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            if (!(context.SyntaxReceiver is AttributeSyntaxReceiver receiver))
                return;

            var txtFiles = context.AdditionalFiles
                .Where(at => at.Path.EndsWith(".txt"));

            var compilation = context.Compilation;

            var generateControllerSymbol = compilation
                .GetTypeByMetadataName(typeof(GenerateControllerAttribute).FullName);

            var tableSymbol = compilation
                .GetTypeByMetadataName(typeof(TableAttribute).FullName);

            foreach (var classDeclaration in receiver.CandidateClasses)
            {
                var model = compilation.GetSemanticModel(classDeclaration.SyntaxTree);
                ITypeSymbol classSymbol = model.GetDeclaredSymbol(classDeclaration) as ITypeSymbol;

                var attributes = classSymbol.GetAttributes();

                var generateControllerAttr = attributes
                    .FirstOrDefault(a => a.AttributeClass.Equals(generateControllerSymbol, SymbolEqualityComparer.Default));

                var tableAttr = attributes
                    .FirstOrDefault(a => a.AttributeClass.Equals(tableSymbol, SymbolEqualityComparer.Default));

                if (generateControllerAttr is null || tableAttr is null)
                    continue;

                var dtoType = generateControllerAttr.NamedArguments.First(a => a.Key == "Dto").Value.Value as Type;
                var tablename = tableAttr.NamedArguments.First(a => a.Key == "Name").Value.Value as string;

                var args = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    {"Entity", classDeclaration.Identifier.Text},
                    {"Dto", dtoType.Name},
                    {"Table", tablename}
                };

                foreach (AdditionalText file in txtFiles)
                {
                    var codeBuilder = new CodeBuilder(file, context.CancellationToken);

                    codeBuilder.Replace(args);

                    var fileName = Path.GetFileNameWithoutExtension(file.Path);

                    context.AddSource($"{args["Entity"]}{fileName}", SourceText.From(codeBuilder.Code, Encoding.UTF8));
                }
            }
        }


        public void Initialize(GeneratorInitializationContext context)
        => context.RegisterForSyntaxNotifications(() => new AttributeSyntaxReceiver());

    }
}