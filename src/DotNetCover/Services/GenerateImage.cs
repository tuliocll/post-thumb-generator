namespace DotNetCover;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Text;

public class GenerateImage
{
    private string _title;
    private string _author;
    private string _date;
    private List<string> _tags;


    public GenerateImage(string title, string? author, string? date, List<string> tags)
    {
        this._title = title;
        this._author = author ?? "";
        this._date = date ?? "";
        this._tags = tags;
    }

    public (byte[] content, string contentType) Create()
    {
        // Setup output format
        var contentType = "image/png";
        const int imageWidth = 1153;
        const int imageHeight = 621;

        System.Drawing.Imaging.Encoder myEncoder;
        EncoderParameter myEncoderParameter;
        EncoderParameters myEncoderParameters;
        ImageCodecInfo myImageCodecInfo;
        using var ms = new MemoryStream();

        // Create the image
        using Bitmap bitmap = new Bitmap(imageWidth, imageHeight);
        using Graphics graphics = Graphics.FromImage(bitmap);
        myEncoder = System.Drawing.Imaging.Encoder.Quality;

        // Create base image
        AddImage("Assets/Images/base.png", 0, 0, imageWidth, imageHeight, graphics);

        // Generate tags icons
        AddTags(this._tags, imageWidth, imageHeight, graphics);

        // Generate the title
        SolidBrush drawBrush = new SolidBrush(Color.FromArgb(61, 61, 61));
        AddText(this._title, 30, 90, imageWidth - 200, imageHeight - 200, graphics, drawBrush, 60, FontStyle.Bold);

        if (!this._author.Equals(""))
        {
            // Generate author and Date
            AddText(this._author + " - " + this._date, 80, imageHeight - 70, imageWidth - 200, imageHeight - 200, graphics, drawBrush, 20, FontStyle.Regular);

            // Create author image 
            AddImage("Assets/Images/profile.png", 20, imageHeight - 80, imageWidth, imageHeight, graphics);
        }

        // Save the bitmap as a PNG
        myEncoderParameters = new EncoderParameters(1);
        myEncoderParameter = new EncoderParameter(myEncoder, 100L);
        myEncoderParameters.Param[0] = myEncoderParameter;
        myImageCodecInfo = GetEncoderInfo(contentType);
        bitmap.Save(ms, myImageCodecInfo, myEncoderParameters);

        return (ms.ToArray(), contentType);

    }

    private static void AddImage(string imagePath, int x, int y, int width, int height, Graphics graph)
    {
        Image newImage = Image.FromFile(imagePath);

        RectangleF srcRect = new Rectangle(0, 0, width, height);
        Rectangle destRect1 = new Rectangle(x, y, width, height);
        GraphicsUnit units = GraphicsUnit.Pixel;

        graph.DrawImage(newImage, destRect1, srcRect, units);
    }

    private static void AddText(string text, int x, int y, int width, int height, Graphics graph, SolidBrush brush, int fontSize, FontStyle fontStyle)
    {
        PrivateFontCollection pfcoll = new PrivateFontCollection();
        // Loading custom font
        if (fontStyle == FontStyle.Bold)
        {
            pfcoll.AddFontFile(@"Assets\Fonts\Montserrat-Bold.ttf");
        }
        else
        {
            pfcoll.AddFontFile(@"Assets\Fonts\Montserrat-Regular.ttf");
        }

        FontFamily ff = pfcoll.Families[0];

        Font drawFont = new Font(ff, fontSize);
        StringFormat drawFormat = new StringFormat();
        Rectangle stringRect = new Rectangle(x, y, width, height);

        graph.DrawString(text, drawFont, brush, stringRect, drawFormat);
    }

    private static void AddTags(List<string> tags, int width, int height, Graphics graph)
    {
        int paddingRight = 30;
        // Max number of icons on the card
        int maxTags = 5;
        int index = 0;
        int lastPosition = (width - paddingRight);

        if (tags != null && tags.Count() > 0)
        {
            foreach (string tag in tags)
            {
                IconsSetup icons = new IconsSetup();
                Icon? icon = icons.searchIcon(tag);

                if (icon != null && index < maxTags)
                {
                    Console.WriteLine(icon.path);
                    lastPosition -= 70;
                    AddImage(icon.path, lastPosition, height - 80, width, height, graph);
                    Console.WriteLine(lastPosition);
                    index++;
                }
            }
        }
    }

    private static ImageCodecInfo GetEncoderInfo(String mimeType)
    {
        int j;
        ImageCodecInfo[] encoders;
        encoders = ImageCodecInfo.GetImageEncoders();
        for (j = 0; j < encoders.Length; ++j)
        {
            if (encoders[j].MimeType == mimeType)
                return encoders[j];
        }
        return null;
    }
}
