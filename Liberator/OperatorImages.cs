using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;

namespace Liberator;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
public class OperatorImages
{

    internal OperatorImages()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static ResourceManager ResourceManager
    {
        get
        {
            if (OperatorImages._resourceManager == null)
            {
                OperatorImages._resourceManager = new ResourceManager("OperatorImages1", typeof(OperatorImages).Assembly);
            }
            return OperatorImages._resourceManager;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static CultureInfo Culture
    {
        get
        {
            return OperatorImages._culture;
        }
        set
        {
            OperatorImages._culture = value;
        }
    }

    public static Bitmap Attackers => (Bitmap)OperatorImages.ResourceManager.GetObject("attackers", OperatorImages._culture);

    public static Bitmap Defenders => (Bitmap)OperatorImages.ResourceManager.GetObject("defenders", OperatorImages._culture);

    public static Bitmap FbiSwat => (Bitmap)OperatorImages.ResourceManager.GetObject("fbi_swat", OperatorImages._culture);

    public static Bitmap Gign => (Bitmap)OperatorImages.ResourceManager.GetObject("gign", OperatorImages._culture);

    public static Bitmap Gsg9 => (Bitmap)OperatorImages.ResourceManager.GetObject("gsg9", OperatorImages._culture);

    public static Icon Icon => (Icon)OperatorImages.ResourceManager.GetObject("icon", OperatorImages._culture);

    public static Bitmap NoOperator => (Bitmap)OperatorImages.ResourceManager.GetObject("no_operator", OperatorImages._culture);

    public static Bitmap PlaceholderCard => (Bitmap)OperatorImages.ResourceManager.GetObject("placeholder_card", OperatorImages._culture);

    public static Bitmap RecruitFull => (Bitmap)OperatorImages.ResourceManager.GetObject("recruit_full", OperatorImages._culture);

    public static Bitmap Sas => (Bitmap)OperatorImages.ResourceManager.GetObject("sas", OperatorImages._culture);

    public static Bitmap Spetsnaz => (Bitmap)OperatorImages.ResourceManager.GetObject("spetsnaz", OperatorImages._culture);

    private static ResourceManager _resourceManager;

    private static CultureInfo _culture;
}
