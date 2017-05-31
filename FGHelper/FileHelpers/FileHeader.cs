using System.Collections.Generic;

namespace FGHelper.FileHelpers
{
    /// <summary>
    /// Object to hold table file header values.  The text file for a table can have a JSON
    /// text string on the first line to dictate how the file is processed.
    /// </summary>
    internal class FileHeader
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public List<string> Columns { get; set; }
        public char Separator { get; set; }
    }
}