using System.Collections.Generic;

namespace FGHelper.FantasyGroundsObjects
{
    /// <summary>
    /// Object to represent Fantasy Grounds table data
    /// </summary>
    public class Table : IFantasyGroundsObject
    {
        public string Class => "tables";

        /// <summary>
        /// Table name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Catagory that table will be located within Fantasy Grounds table dialog
        /// </summary>
        public string Category { get; set; }
        
        /// <summary>
        /// Table description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///Displayed notes for the table
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Table column headers
        /// </summary>
        public List<string> Columns { get; set; }

        /// <summary>
        /// Table rows
        /// </summary>
        public List<List<string>> Rows { get; set; }

        public Table()
        {
            Columns = new List<string>();
            Rows = new List<List<string>>();
        }

        public string GetName()
        {
            return Name;
        }

        public string GetClass()
        {
            return Class;
        }

        public string GetCategory()
        {
            return Category;
        }
    }
}