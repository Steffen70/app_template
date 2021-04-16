using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.CodeAnalysis;

namespace Generator
{
    public class CodeBuilder
    {
        public string Code { get; set; }
        public CodeBuilder(AdditionalText file, CancellationToken cancellationToken)
        {
            Code = file.GetText(cancellationToken).ToString();
        }

        private static readonly Regex re = new Regex(@"%(\w+)%", RegexOptions.Compiled);
        public void Replace(Dictionary<string, string> args)
        => Code = re.Replace(Code, match => args[match.Groups[1].Value]);
    }
}