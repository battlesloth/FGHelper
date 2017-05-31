using System;
using System.Collections.Generic;
using System.IO;
using FGHelper.FantasyGroundsObjects;
using FGHelper.FileHelpers;

namespace FGHelper.Projects
{
    /// <summary>
    /// Runs project files to generate Fanatsy Ground XML data
    /// </summary>
    public static class ProjectRunner
    {
        /// <summary>
        /// Runs projects that use files on disk to populate Fanatsy Grounds data.  This version currently supports Tables only.
        /// </summary>
        /// <param name="project"></param>
        /// <param name="sourceFilePath"></param>
        /// <returns></returns>
        public static Project RunFileBasedProject(Project project, string sourceFilePath)
        {
            project.Tables = new List<IFantasyGroundsObject>();

            var files = new DirectoryInfo(sourceFilePath).GetFiles();

            foreach (var fileInfo in files)
            {
                Console.WriteLine($"Processing {fileInfo.Name}.");

                if (fileInfo.Name.ToLower().StartsWith("ruleset"))
                {
                    project.Rulesets = FileParser.ParseRulesetFile(fileInfo);
                }
                else if (fileInfo.Extension == ".txt")
                {
                    project.Tables.Add(FileParser.ParseTableFile(fileInfo, project.Name));
                }
                else if (fileInfo.Name.ToLower() == "thumbnail.png")
                {
                    //Add thumbnail
                    project.ThumbnailPath = fileInfo.FullName;
                }
            }

            if (project.Tables.Count > 0)
            {
                project.Entries.Add("table");
            }

            if (project.Rulesets.Count == 0)
            {
                project.Rulesets.Add("Any");
            }

            var helper = new FantasyGroundsXmlHelper();

            project.DefinitionXml = helper.GenerateDefinitionXml(project);
            project.DatabaseXml = helper.GenerateDbXml(project.Version, project.Release, project);

            return project;
        }

        /// <summary>
        /// Runs projects that are prepopulated with Fanatsy Grounds data.  This vesrion currently supports Tables and Story Templates only.
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static Project RunProject(Project project)
        {
            if (project.Tables.Count > 0)
            {
                project.Entries.Add("table");
            }

            if (project.Rulesets.Count == 0)
            {
                project.Rulesets.Add("Any");
            }

            var helper = new FantasyGroundsXmlHelper();

            project.DefinitionXml = helper.GenerateDefinitionXml(project);
            project.DatabaseXml = helper.GenerateDbXml(project.Version, project.Release, project);

            return project;
        }
    }
}
