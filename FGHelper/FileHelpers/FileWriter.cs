using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Xml;
using FGHelper.Projects;

namespace FGHelper.FileHelpers
{
    public static class FileWriter
    {

        /// <summary>
        /// Writes .mod file to the specified output file path
        /// </summary>
        /// <param name="project"></param>
        /// <param name="outputFilePath"></param>
        /// <returns>Full path of mod file</returns>
        public static string WriteModFile(Project project, string outputFilePath)
        {
            var xmlSettings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                Encoding = Encoding.GetEncoding("iso-8859-1"),
                NewLineOnAttributes = false
            };


            if (!string.IsNullOrEmpty(project.ThumbnailPath))
            {
                string outPath = Path.Combine(outputFilePath, project.FileName, "thumbnail.png");
                File.Copy(project.ThumbnailPath, outPath);
            }


            var defFile = File.Create($"{outputFilePath}\\{project.FileName}\\definition.xml");

            Console.WriteLine("Writing definition file.");

            using (var stream = new StreamWriter(defFile))
            {
                using (var writer = XmlWriter.Create(stream, xmlSettings))
                {
                    project.DefinitionXml.Save(writer);
                }
            }
            

            var dbFile = File.Create($"{outputFilePath}\\{project.FileName}\\db.xml");
            Console.WriteLine("Writing db file.");

            using (var stream = new StreamWriter(dbFile))
            {
                using (var writer = XmlWriter.Create(stream, xmlSettings))
                {
                    project.DatabaseXml.Save(writer);
                }
            }

            Console.WriteLine("Zipping files.");

            if (File.Exists($"{outputFilePath}\\{project.FileName}.zip"))
            {
                File.Delete($"{outputFilePath}\\{project.FileName}.zip");
            }


            ZipFile.CreateFromDirectory($"{outputFilePath}\\{project.FileName}",
                $"{outputFilePath}\\{project.FileName}.zip");

            if (File.Exists($"{outputFilePath}\\{project.FileName}.mod"))
            {
                File.Delete($"{outputFilePath}\\{project.FileName}.mod");
            }

            File.Move($"{outputFilePath}\\{project.FileName}.zip",
                $"{outputFilePath}\\{project.FileName}.mod");

            Console.WriteLine("Cleaning up temp files.");

            Directory.Delete($"{outputFilePath}\\{project.FileName}", true);
            File.Delete($"{outputFilePath}\\{project.FileName}.zip");

            return $"{outputFilePath}\\{project.FileName}.mod";
        }
    }
}
