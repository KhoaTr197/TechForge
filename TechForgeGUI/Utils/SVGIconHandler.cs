using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Svg;
using System.Drawing;

namespace TechForgeGUI.Utils
{
  //Handle SVG to Bitmap for Icon-Purpose
  public class SVGIconHandler
  {
    public string FolderPath { get; set; }
    public Size Size { get; set; }
    public SVGIconHandler(string folderPath, Size size = default) {
      FolderPath = folderPath;
      Size = size;
    }
    public Dictionary<string, Bitmap> ConvertToBitmap()
    {
      if (!Directory.Exists(FolderPath))
        throw new DirectoryNotFoundException($"Resources folder not found: {FolderPath}");

      Dictionary<string, Bitmap> results = new Dictionary<string, Bitmap>();

      // Get all SVG files from the folder
      string[] svgFiles = Directory.GetFiles(FolderPath, "*.svg");
      foreach (string svgFile in svgFiles)
      {
        // Load and create Bitmap from SVG
        Bitmap bitmap = SvgDocument.Open(svgFile).Draw(Size.Width, Size.Height);

        string svgName = Path.GetFileNameWithoutExtension(svgFile);
        results.Add(svgName, bitmap);
      }

      return results;
    }
  }
}
