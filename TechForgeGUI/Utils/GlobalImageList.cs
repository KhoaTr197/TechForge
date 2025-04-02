using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TechForgeGUI.Utils
{
  public static class GlobalStatics
  {
    public static ImageList iconList = new ImageList() {
      ColorDepth = ColorDepth.Depth32Bit,
    };
    public static void SetUp(string path = "Resources")
    {
      SVGIconHandler iconHandler = new SVGIconHandler(path, new Size(16, 16));

      Dictionary<string, Bitmap> icons = iconHandler.ConvertToBitmap();

      foreach (KeyValuePair<string, Bitmap> icon in icons)
      {
        string name = icon.Key;
        Bitmap bitmap = icon.Value;
        iconList.Images.Add(name, bitmap);
      }
    }
  }
}
