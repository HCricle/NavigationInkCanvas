using NaiveInkCanvas.Controls;

namespace NaiveInkCanvas.Background.Models.Defs
{
    public interface IImgBackground
    {
        BackgroundBorder BorderControl { get; }
        string Name { get; }
    }
}