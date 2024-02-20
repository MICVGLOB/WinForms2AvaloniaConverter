using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;

namespace WAConverter
{
    internal static class ResXCleaner
    {
        static HashSet<string> ignoredProperties = new HashSet<string>()
        {
            "Type",
            "Name",
            "Parent",
            "ZOrder"
        };

        static bool IsPng(XElement element)
        {
            var localName = element.Name.LocalName;
            if (localName != "data")
                return false;
            var type = element.Attribute(XName.Get("type", ""));
            if (type == null)
                return false;
            if (type.Value.Split(',').First() != "System.Drawing.Bitmap")
                return false;
            return true;
        }
        static void ExtractPng(XElement element, string fName)
        {
            byte[] data = Convert.FromBase64String(element.Value);
            var directory = Path.GetDirectoryName(Path.GetFullPath(fName)) + "/";
            int index = 0;
            var elementName = element.Attribute(XName.Get("name", "")).Value;
            string fileName;
            do
            {
                if (index == 0)
                    fileName = elementName + ".png";
                else
                    fileName = elementName + $"({index}).png";
                index++;
            } while (File.Exists(directory + fileName));
            using (var stream = new MemoryStream(data, 0, data.Length))
            {
                Image image = Image.FromStream(stream);
                try
                {
                    image.Save(directory + fileName, ImageFormat.Png);
                }
                catch { }
            }
        }
        static bool IsWindowTitle(XElement element)
        {
            var elementName = element.Attribute(XName.Get("name", ""))?.Value;
            return elementName == "$this.Text";
        }
        static void UpdateWindowTitle(XElement element, string fileName)
        {
            element.Attribute(XName.Get("name", "")).Value = "Title";
        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static void CleanupFile(string fileName)
        {
            var file = XDocument.Load(fileName);
            foreach (var element in file.XPathSelectElements("/root/*").ToArray())
            {
                /*if (IsDXSvgImage(element)) // Uncomment for DevExpress SVG-image extraction
                  {
                   ExtractSVGImage(element, fileName);
                   element.Remove();
                  }*/
                if (IsPng(element))
                {
                    ExtractPng(element, fileName);
                    element.Remove();
                }
                if (IsWindowTitle(element))
                {
                    UpdateWindowTitle(element, fileName);
                }
                else if (ShouldRemoveElement(element))
                {
                    try
                    {
                        if (element.Parent != null)
                            element.Remove();
                    }
                    catch { }
                }
            }
            SetFileReadAccess(fileName, false);
            file.Save(fileName);
        }
        public static void SetFileReadAccess(string FileName, bool setReadOnly)
        {
            FileInfo fInfo = new FileInfo(FileName);
            fInfo.IsReadOnly = setReadOnly;
        }
        /*        private static void ExtractSVGImage(XElement element, string sourceFileName) // Uncomment for DevExpress SVG-image extraction
                {
                    const string xmlHeaderMark = "<?xml";
                    const string svgEndMark = "</svg>";
                    const string svgImagePropertySuffix = ".SvgImage";

                    var value = Encoding.UTF8.GetString(Convert.FromBase64String(element.Value));
                    int startIndex = value.IndexOf(xmlHeaderMark);
                    int endIndex = value.IndexOf(svgEndMark) + svgEndMark.Length;
                    var svgImageData = value.Substring(startIndex, endIndex - startIndex);

                    var elementName = element.Attribute(XName.Get("name", "")).Value;
                    if (elementName.EndsWith(svgImagePropertySuffix))
                        elementName = elementName.Substring(0, elementName.Length - svgImagePropertySuffix.Length);
                    var directory = Path.GetDirectoryName(Path.GetFullPath(sourceFileName)) + "/";
                    int index = 0;
                    string fileName;
                    do
                    {
                        if (index == 0)
                            fileName = elementName + ".svg";
                        else
                            fileName = elementName + $"({index}).svg";
                        index++;
                    } while (File.Exists(directory + fileName));
                    File.WriteAllText(directory + fileName, svgImageData);
                }

                private static bool IsDXSvgImage(XElement element)
                {
                    var localName = element.Name.LocalName;
                    if (localName != "data")
                        return false;
                    var type = element.Attribute(XName.Get("type", ""));
                    if (type == null)
                        return false;
                    if (type.Value.Split(',').First() != "DevExpress.Utils.Svg.SvgImage")
                        return false;

                    var mimeType = element.Attribute(XName.Get("mimetype", ""))?.Value;
                    if (mimeType != "application/x-microsoft.net.object.bytearray.base64")
                        throw new Exception("Unexpected MimeType for SVGImage: " + mimeType);
                    return true;
                }
        */
        static bool ShouldRemoveElement(XElement element)
        {
            var localName = element.Name.LocalName;
            switch (localName)
            {
                case "assembly":
                    return true; //remove all assembly aliases
                case "data":
                    if (element.Attribute(XName.Get("type", "")) != null)
                        return true;
                    if (element.Attribute(XName.Get("mimetype", "")) != null)
                        return true;
                    var name = element.Attribute(XName.Get("name", ""));

                    var propertyName = name?.Value?.Split('.').LastOrDefault();
                    if (propertyName != null)
                        return ignoredProperties.Contains(propertyName);
                    break;
                case "metadata":
                    return true;
                default:
                    break;
            }
            return false;
        }
        public static bool IsDesignerHosted(this Control control)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return true;

            Control ctrl = control;
            while (ctrl != null)
            {
                if ((ctrl.Site != null) && ctrl.Site.DesignMode)
                    return true;
                ctrl = ctrl.Parent;
            }
            return false;
        }
    }
}
