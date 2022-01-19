//#if ENABLE_VSTU
//using System;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Xml.Linq;

//using UnityEngine;
//using UnityEditor;

//using SyntaxTree.VisualStudio.Unity.Bridge;

//[InitializeOnLoad]
//public class ProjectFileHook
//{
//    // necessary for XLinq to save the xml project file in utf8
//    class Utf8StringWriter : StringWriter
//    {
//        public override Encoding Encoding
//        {
//            get { return Encoding.UTF8; }
//        }
//    }

//    static ProjectFileHook()
//    {
//        ProjectFilesGenerator.ProjectFileGeneration += (string name, string content) =>
//        {
//            // parse the document and make some changes
//            var document = XDocument.Parse(content);

//            var assemblyName = Path.GetFileNameWithoutExtension(name);
//            var rootNamespace = document.Descendants().First(x => x.Name.LocalName == "RootNamespace");

//            if (assemblyName == "ProceduralLevelGenerator.Unity")
//            {
//                rootNamespace.Value = "ProceduralLevelGenerator.Unity";
//            }

//            if (assemblyName == "ProceduralLevelGenerator.Unity.Editor")
//            {
//                rootNamespace.Value = "ProceduralLevelGenerator.Unity";
//            }

//            if (assemblyName == "ProceduralLevelGenerator.Unity.Examples")
//            {
//                rootNamespace.Value = "ProceduralLevelGenerator.Unity";
//            }

//            if (assemblyName == "ProceduralLevelGenerator.Unity.Tests")
//            {
//                rootNamespace.Value = "ProceduralLevelGenerator.Unity";
//            }

//            document.Root.Add(new XComment($"{assemblyName},{name}"));

//            // save the changes using the Utf8StringWriter
//            var str = new Utf8StringWriter();
//            document.Save(str);

//            return str.ToString();
//        };
//    }
//}
//#endif

