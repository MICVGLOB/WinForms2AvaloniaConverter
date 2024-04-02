using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

using System.Windows.Forms;

namespace WAConverter
{
    public partial class XamlConverter
    {
        #region Create application from template

        private void CreateAppFromTemplate(string rootNamespace)
        {
            CreateAppFromTemplateCore(rootNamespace, "App.axaml", controlsVersion, avaloniaVersion);
            CreateAppFromTemplateCore(rootNamespace, "App.axaml.cs", controlsVersion, avaloniaVersion);
            CreateAppFromTemplateCore(rootNamespace, "ConvertedAvalonia.csproj", controlsVersion, avaloniaVersion);
            CreateAppFromTemplateCore(rootNamespace, "MainWindow.axaml", controlsVersion, avaloniaVersion);
            CreateAppFromTemplateCore(rootNamespace, "MainWindow.axaml.cs", controlsVersion, avaloniaVersion);
            CreateAppFromTemplateCore(rootNamespace, "nuget.config", controlsVersion, avaloniaVersion);
            CreateAppFromTemplateCore(rootNamespace, "Program.cs", controlsVersion, avaloniaVersion);

            var packageName = $"Eremex.Avalonia.Controls.{controlsVersion}.nupkg";
            try
            {
                var templateName = Assembly.GetAssembly(typeof(WAForm))
                    .GetManifestResourceNames()
                    .Single(x => x.EndsWith(packageName));
                var resource = Assembly.GetAssembly(typeof(WAForm)).GetManifestResourceStream(templateName);
                if (resource != null)
                {
                    var dir = Directory.GetCurrentDirectory() + "\\Converted\\packages\\";
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    using (FileStream fs = new FileStream(dir + packageName, FileMode.Create))
                        resource.CopyTo(fs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void CreateAppFromTemplateCore(string namespaceName, string templateName, string controlsVersion, string avaloniaVersion)
        {
            try
            {
                var template = LoadTemplate(templateName);
                template = template.Replace("@NamespaceName@", namespaceName);
                template = template.Replace("@AvaloniaVersion@", avaloniaVersion);
                template = template.Replace("@ControlsVersion@", controlsVersion);

                var dir = Directory.GetCurrentDirectory() + "\\Converted";
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                var fileFullName = dir + "\\" + templateName;
                File.WriteAllText(fileFullName, template);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region Create markup and C# view model files from template

        private void GenerateCodeFile(Control target, string rootTypeName, string namespaceName)
        {
            var vmClass = rootTypeName + "ViewModel";

            try
            {
                var baseType = target is Form ? "Window" : "UserControl";
                var templateName = "CodeFileTemplate.cs";
                var template = LoadTemplate(templateName)
                    .Replace("@BaseClass@", baseType)
                    .Replace("@TypeClass@", rootTypeName)
                    .Replace("@NamespaceName@", namespaceName)
                    .Replace("@ViewModelClass@", vmClass);

                var dir = Directory.GetCurrentDirectory() + "\\Converted";
                var fileFullName = dir + "\\" + rootTypeName + ".axaml.cs";
                File.WriteAllText(fileFullName, template);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void GenerateViewModelFile(Control target, string rootTypeName, string namespaceName)
        {
            var vmClass = rootTypeName + "ViewModel";

            try
            {
                var baseType = target is Form ? "Window" : "UserControl";
                var templateName = "ViewModelClassTemplate.cs";
                var template = LoadTemplate(templateName)
                    .Replace("@BaseClass@", baseType)
                    .Replace("@TypeClass@", rootTypeName)
                    .Replace("@NamespaceName@", namespaceName)
                    .Replace("@ViewModelClass@", vmClass)
                    .Replace("@Fields@", GenerateFields(target))
                    .Replace("@FieldsInitialization@", GenerateFieldsInitialization(vmClass))
                    .Replace("@Methods@", GenerateMethods());

                var dir = Directory.GetCurrentDirectory() + "\\Converted";
                var fileFullName = dir + "\\" + vmClass + ".cs";
                File.WriteAllText(fileFullName, template);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region Utils
        private string LoadTemplate(string resourceTemplateName)
        {
            var templateName = Assembly.GetAssembly(typeof(WAForm))
                .GetManifestResourceNames()
                .Single(x => x.EndsWith(resourceTemplateName));

            var resource = Assembly.GetAssembly(typeof(WAForm)).GetManifestResourceStream(templateName);
            if (resource == null)
                return null;
            using (StreamReader reader = new StreamReader(resource))
                return reader.ReadToEnd();
        }

        private string GenerateFieldsInitialization(string viewModelClassName)
        {
            var b = new StringBuilder();
            foreach (var fi in Fields)
            {
            }
            return b.ToString();
        }
        #endregion
    }
}
