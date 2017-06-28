using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace RulesetChecker
{
    static class Program
    {
        static int errors = 0;

        static int Main(string[] args)
        {
            if (args.Length < 1 || !File.Exists(args[0]))
            {
                Console.WriteLine("Usage: {0} <RULESET.TXT> [<text or text file> ...]", Path.GetFileName(typeof(Program).Assembly.Location));
                return 2;
            }

            try
            {
                var total = Stopwatch.StartNew();

                var rulesetLoad = Stopwatch.StartNew();
                var ruleset = LoadRuleset(args[0]);
                rulesetLoad.Stop();

                int ruleHotNo = 0;
                long ruleHotMs = 0;

                var textsParsed = new Stopwatch();
                int texts = 0;
                for (int i = 1; i < args.Length; i++ )
                {
                    foreach (string text in ReadText(args[i]))
                    {
                        Console.Error.WriteLine("INPUT : {0}", text);
                        string res = text;
                        List<int> appliedRules = new List<int>();
                        textsParsed.Start();

                        foreach (var rule in ruleset)
                        {
                            var ruleApplied = Stopwatch.StartNew();
                            string source = res;
                            res = Regex.Replace(source, rule.Value.Item1, rule.Value.Item2);
                            ruleApplied.Stop();

                            if (!source.Equals(res, StringComparison.Ordinal))
                                appliedRules.Add(rule.Key);

                            if (ruleApplied.ElapsedMilliseconds > ruleHotMs)
                            {
                                ruleHotNo = rule.Key;
                                ruleHotMs = ruleApplied.ElapsedMilliseconds;
                            }
                        }

                        textsParsed.Stop();
                        texts++;

                        Console.Error.WriteLine("OUTPUT: {0}", res);
                        Console.Error.Write("RULES :");
                        if (appliedRules.Count > 0)
                        {
                            foreach (int r in appliedRules)
                                Console.Error.Write(" #{0}", r);
                            Console.Error.WriteLine();
                        }
                        else
                        {
                            Console.Error.WriteLine(" none");
                        }
                        Console.Error.WriteLine();
                    }
                }

                total.Stop();
                Console.Write("Total: {0} ms, {1} errors; {2} rules load: {3} ms",
                    total.ElapsedMilliseconds, errors,
                    ruleset.Count, rulesetLoad.ElapsedMilliseconds);
                if (texts > 0)
                    Console.Write("; {0} texts parsed: {1} ms", texts, textsParsed.ElapsedMilliseconds);
                Console.WriteLine();
                if (ruleHotNo > 0)
                {
                    Console.WriteLine("Hot rule #{0} /{1}/ --> \"{2}\": {3} ms",
                        ruleHotNo, ruleset[ruleHotNo].Item1, ruleset[ruleHotNo].Item2, ruleHotMs);
                }

                return (errors > 0 ? 1 : 0);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Check failed due to error: " + ex.ToString());
                return 2;
            }
        }

        static IEnumerable<string> ReadText(string text)
        {
            if (!File.Exists(text))
            {
                yield return text;
            }
            else
            {
                using (StreamReader file = new StreamReader(text, Encoding.Default, true))
                {
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                        // Skip empty strings
                        if (string.IsNullOrWhiteSpace(line))
                            continue;
                        yield return line;
                    }
                }
            }
        }

        static IDictionary<int, Tuple<string, string>> LoadRuleset(string fileName)
        {
            var ruleset = new SortedList<int, Tuple<string, string>>();

            using (StreamReader file = new StreamReader(fileName, Encoding.UTF8, true))
            {
                string line;
                int lineNo = 0;
                while ((line = file.ReadLine()) != null)
                {
                    lineNo++;

                    // Ignore comments and empty lines
                    if (line.Length == 0 || line[0] == '#' || line[0] == '[')
                        continue;

                    // TODO Actually there are allowed extra whitespaces and a modifier [imsx] inside and
                    // search/replace delimiters could be not only '/' char but any non-space symbol:
                    // https://www.west.com/wp-content/uploads/2015/10/Nuance-Developer-Guide-for-Vocalizer.pdf
                    if (line[0] != '/')
                    {
                        // TODO Search starting '/' char?
                        if (!line.StartsWith("language", StringComparison.OrdinalIgnoreCase) &&
                            !line.StartsWith("charset", StringComparison.OrdinalIgnoreCase))
                        {
                            errors++;
                            Console.Error.WriteLine("ERROR: Incorrect rule at line #{0}: {1}", lineNo, line);
                        }
                        continue;
                    }

                    // Separate regular expression and replacement text
                    int i = line.IndexOf("/ --> \"", StringComparison.Ordinal);
                    if (i < 1)
                    {
                        errors++;
                        Console.Error.WriteLine("ERROR: Invalid rule at line #{0}: {1}", lineNo, line);
                        continue;
                    }
                    int j = line.LastIndexOf('"');
                    if (j <= (i + 7))
                    {
                        errors++;
                        Console.Error.WriteLine("ERROR: Non-closed replace rule at line #{0}: {1}", lineNo, line);
                        continue;
                    }
                    else if (j + 1 < line.Length)
                    {
                        errors++;
                        Console.Error.WriteLine("WARN: Extra chars after replace rule at line #{0}: {1}", lineNo, line);
                    }

                    string search = line.Substring(1, i - 1);
                    // Replace double '\' chars and '\$' (RULESET.TXT specifics?)
                    string replace = line.Substring(i + 7, j - i - 7).Replace(@"\$", "$$").Replace(@"\\", @"\");

                    // Actually this syntax is dotNet-compatible:
                    //// Replace non-PCRE {N} substitutions
                    //// Actually only 1-9 substitution ref numbers are expected:
                    //replace = Regex.Replace(replace, @"\$\{(\d)\}", "$$$1");

                    // Check for valid syntax in both expressions
                    try
                    {
                        Regex.Replace("", search, replace);

                        ruleset.Add(lineNo, Tuple.Create(search, replace));
                    }
                    catch
                    {
                        errors++;
                        Console.Error.WriteLine("ERROR: Invalid regular expression in rule at line #{0}: {1}", lineNo, line);
                    }
                }

                return ruleset;
            }
        }
    }
}
