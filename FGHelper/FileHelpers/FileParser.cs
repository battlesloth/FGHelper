using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FGHelper.FantasyGroundsObjects;
using Newtonsoft.Json;

namespace FGHelper.FileHelpers
{
    /// <summary>
    /// Static class to assist with file operations for creating Fantasy Grounds .mod files
    /// </summary>
    public static class FileParser
    {
        /// <summary>
        /// Parses a text file from disk into a Table object that represents a Fantasy Grounds Table. If there is no table
        /// header present in the file, it assumes it is a single column table
        /// </summary>
        /// <param name="file"></param>
        /// <param name="projectName"></param>
        /// <returns></returns>
        public static Table ParseTableFile(FileInfo file, string projectName)
        {
            var table = new Table();
            var rows = new List<List<string>>();

            using (var reader = file.OpenText())
            {
                var first = reader.ReadLine();

                FileHeader header;
                var multiColumn = false;

                if (first.TryDeserialize(out header))
                {
                    table.Name = header.Name ?? file.Name.Replace(file.Extension, "");
                    table.Description = header.Description ?? "";
                    table.Columns = header.Columns ?? new List<string>{" "};
                    table.Category = header.Category ?? projectName;
                    if (table.Columns.Count > 1)
                    {
                        multiColumn = true;
                    }
                }
                else
                {
                    table.Name = file.Name.Replace(file.Extension, "");
                    table.Description = "";
                    table.Columns.Add(" ");
                    table.Category = projectName;
                    rows.Add(new List<string> {first});
                }
                

                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    rows.Add(multiColumn ? line.Split(header.Separator).ToList() : new List<string> {line});
                }
            }

            table.Rows = rows;

            return table;
        }

        /// <summary>
        /// Parses out the rulesets to apply to a .mod file. Expects a single ruleset per line in text file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static List<string> ParseRulesetFile(FileInfo file)
        {
            var result = new List<string>();
            using (var reader = file.OpenText())
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }

            return result;
        }
    }

    internal static class JsonExtensions
    {
        internal static bool TryDeserialize<T>(this string obj, out T result )
        {
            try
            {
                result = JsonConvert.DeserializeObject<T>(obj);
                return true;
            }
            catch (Exception e)
            {
                result = default(T);
                return false;
            }
        }
    }
}