using System.Collections.Generic;
using System.Xml.Linq;
using FGHelper.FantasyGroundsObjects;

namespace FGHelper.Projects
{
    /// <summary>
    /// Object that represents the details needed for the creation of a Fantasy Grounds .mod file
    /// </summary>
    public class Project
    {
        /// <summary>
        /// Name of module to be displayed in Fantasy Grounds
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// File name of .mod file
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Catagory to be displayed in Fantasy Grounds
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        ///  Author to be displayed in Fantasy Grounds
        /// </summary>
        public string Author { get; set; }


        /// <summary>
        /// Minimum version of Fantasy Grounds that is supported
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// Minimum release of Fantasy Grounds that is supported
        /// </summary>
        public string Release { get; set; }

        /// <summary>
        /// Output xml for .mod definition file
        /// </summary>
        public XDocument DefinitionXml { get; set; }
        /// <summary>
        /// Output xml for .mod database file
        /// </summary>
        public XDocument DatabaseXml { get; set; }

        /// <summary>
        /// Path to thumbnail image
        /// </summary>
        public string ThumbnailPath { get; set; }

        /// <summary>
        /// Rulesets in Fantasy Grounds that are supported
        /// </summary>
        public List<string> Rulesets { get; set; }

        /// <summary>
        /// Type of entires in the .mod file.  Currently only tables are supported by this library
        /// </summary>
        public List<string> Entries { get; set; }

        /// <summary>
        /// List of Story Templates for this .mod file
        /// </summary>
        public List<IFantasyGroundsObject> StoryTemplates { get; set; }

        /// <summary>
        /// List of Tables for this .mod file
        /// </summary>
        public List<IFantasyGroundsObject> Tables { get; set; }

        public Project()
        {
            Rulesets = new List<string>();
            Entries = new List<string>();
            StoryTemplates = new List<IFantasyGroundsObject>();
            Tables = new List<IFantasyGroundsObject>();
        }

    }
}
