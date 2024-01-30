using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Decompiler
{
    internal class FunctionModEntry
    {
        public string? Name = null;
        public Types.TypeInfo? ReturnType = null;
        public Dictionary<int, string> ParamNames = new();
        public Dictionary<int, Types.TypeInfo> ParamTypes = new();

        public FunctionModEntry()
        {
        }
    }

    internal class FunctionDB
    {
        private readonly Dictionary<uint, FunctionModEntry> Entries = new();
        private readonly Dictionary<uint, FunctionModEntry> Mk2Entries = new();

        public FunctionDB()
        {
            var file = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                "functions.db");

            var lines = File.Exists(file)
                ? File.ReadAllLines(file)
                : Encoding.Default.GetString(Properties.Resources.Functions).Trim().Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            foreach (var line in lines)
            {
                if (line.StartsWith("#") || line.Length == 0)
                    continue;

                var tokens = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                Dictionary<uint, FunctionModEntry> entryDict;

                if (tokens[0].StartsWith("^"))
                {
                    tokens[0] = tokens[0][1..];
                    entryDict = Mk2Entries;
                }
                else
                {
                    entryDict = Entries;
                }

                if (!uint.TryParse(tokens[0].Replace("0x", ""), System.Globalization.NumberStyles.HexNumber, null, out var hash))
                    throw new FileFormatException("Cannot parse function hash; " + line);

                if (!entryDict.ContainsKey(hash))
                    entryDict[hash] = new();

                tokens[1] = tokens[1].ToUpper();

                switch (tokens[1])
                {
                    case "NAME":
                        entryDict[hash].Name = tokens[2];
                        break;
                    case "RETURN":
                        entryDict[hash].ReturnType = Types.GetFromName(tokens[2]);
                        break;
                    case "ARG_NAME":
                        entryDict[hash].ParamNames.Add(int.Parse(tokens[2]), tokens[3]);
                        break;
                    case "ARG_TYPE":
                        entryDict[hash].ParamTypes.Add(int.Parse(tokens[2]), Types.GetFromName(tokens[3]));
                        break;
                    default:
                        throw new FileFormatException("Unknown field");
                }
            }
        }

        public void Apply(Function func, FunctionModEntry entry)
        {
            if (entry.Name != null)
            {
                func.SetName(entry.Name);
            }

            if (entry.ReturnType != null)
            {
                func.HintReturnType(ref entry.ReturnType.GetContainer());
                func.ReturnType.SealType();
            }

            foreach (var p in entry.ParamNames)
            {
                func.GetFrameVar((uint)p.Key).Name = p.Value;
            }

            foreach (var p in entry.ParamTypes)
            {
                func.GetFrameVar((uint)p.Key).HintType(ref p.Value.GetContainer());
                func.GetFrameVar((uint)p.Key).DataType.SealType();
            }
        }

        public void Visit(Function func)
        {
            var hash = func.Hash;

            if (Entries.TryGetValue(hash, out var entry))
            {
                Apply(func, entry);
            }

            var mk2hash = func.Mk2Hash;

            if (Mk2Entries.TryGetValue(mk2hash, out var mk2Entry))
            {
                Apply(func, mk2Entry);
            }
        }
    }
}
