namespace DotNetCover;
using System.IO;

public class IconsSetup
{
    public List<Icon> icons = new List<Icon>();

    public IconsSetup()
    {
        getIconsFromPath();
    }

    public Icon? searchIcon(string name)
    {
        if (name.Equals(""))
        {
            return null;
        }

        Icon? result = this.icons.Find(icon => icon.name.Equals(name + ".png"));

        return result;
    }

    private void getIconsFromPath()
    {
        try
        {
            string current = Directory.GetCurrentDirectory().ToString();
            string assetsPath = current + @"\Assets\Images\techs";
            string[] dirs = Directory.GetFiles(assetsPath, "*.png");
            Console.WriteLine("{0} Icons found.", dirs.Length);
            foreach (string dir in dirs)
            {
                string fileName = Path.GetFileName(dir);
                this.icons.Add(new Icon(fileName, dir));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The process failed: {0}", e.ToString());
        }
    }

}
