using System.Data.Entity.Infrastructure.Pluralization;

namespace CAI.ConsoleTestGround.Modules
{
    using System;
    using System.Text.RegularExpressions;

    public class NLPModule
    {
        public void Start()
        {
            var input = "torment";

            var pattern = @"^(.*?)(ing|ly|ed|ious|ies|ive|es|s|ment)?$";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            //MatchCollection matches = rgx.Matches(input);

            //if (matches.Count > 0)
            //{
            //    Console.WriteLine("{0} ({1} matches):", input, matches.Count);
            //    foreach (Match match in matches)
            //        Console.WriteLine("   " + match.Value);
            //}

            var r = Regex.Replace(input, pattern, "");
            Console.WriteLine(r);
        }
    }
}
