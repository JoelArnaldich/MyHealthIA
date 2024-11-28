using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace MyHealthAI;

public partial class LiveCharts : ObservableObject
{
    public ISeries[] Series { get; set; } =
    {
    };
    public ISeries[] Series1 { get; set; } =
    {

    };
    public ISeries[] Series2 { get; set; } =
    {
    };
    public ISeries[] Series3 { get; set; } =
    {
    };
    public ISeries[] Series4 { get; set; } =
    {
    };

    public Axis[] YAxes { get; set; } =
    {
    };

    public Axis[] XAxes { get; set; } =
    {
    };
}
