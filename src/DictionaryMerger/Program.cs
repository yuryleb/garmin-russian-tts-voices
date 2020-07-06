using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DictionaryMerger
{
    static class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 3 || !File.Exists(args[0]) ||
                (args[1] != "-c" && args[1] != "-f"))
            {
                Console.WriteLine("Usage: {0} <DICTIONARY.VOC> (-c|-f) <output dictionary file>", Path.GetFileName(typeof(Program).Assembly.Location));
                Console.WriteLine("\t-c\tCapitalize words from source dictionary and write to <output dictionary file>");
                Console.WriteLine("\t-f\tFilter out words from source dictionary inside <output dictionary file>");
                return 2;
            }

            try
            {
                var source = ReadDictionary(args[0]);

                switch (args[1])
                {
                    case "-c":
                        using (var file = new StreamWriter(args[2], false, Encoding.UTF8))
                        {
                            foreach (var d in source)
                            {
                                var key = new StringBuilder();
                                key.Append(char.ToUpper(d.Key[0]));
                                if (d.Key.Length > 1)
                                {
                                    int sep = d.Key.IndexOf('-', 1);
                                    if (sep > 0)
                                        key.Append(d.Key, 1, sep).Append(char.ToUpper(d.Key[sep + 1])).Append(d.Key.Substring(sep + 2));
                                    else
                                        key.Append(d.Key.Substring(1));
                                }
                                if (key.Length < 19)
                                    key.Append(' ', 19 - key.Length);
                                file.WriteLine(key.ToString() + " //" + d.Value);
                            }
                        }
                        break;
                    case "-f":
                        var target = File.ReadAllLines(args[2], Encoding.UTF8);
                        using (var file = new StreamWriter(args[2], false, Encoding.UTF8))
                        {
                            foreach(var line in target)
                            {
                                // Detect '//' separator
                                int sep = line.IndexOf(" //", StringComparison.Ordinal);
                                if (sep > 0)
                                {
                                    // Skip the same word from source dictionary
                                    var word = line.Substring(0, sep).TrimEnd();
                                    if (source.ContainsKey(word))
                                        continue;
                                }
                                file.WriteLine(line);
                            }
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Process failed due to error: " + ex.ToString());
                return 1;
            }

            return 0;
        }

        static IDictionary<string,string> ReadDictionary(string fileName)
        {
            var dic = new Dictionary<string, string>(StringComparer.Ordinal);
            using (var file = new StreamReader(fileName, Encoding.UTF8, true))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    // Detect '//' separator
                    int sep = line.IndexOf(" //", StringComparison.Ordinal);
                    if (sep < 0)
                        continue;
                    dic.Add(line.Substring(0, sep).TrimEnd(), line.Substring(sep + 3));
                }
            }
            return dic;
        }
    }
}
