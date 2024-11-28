using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;
using System.Reflection.Emit;

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
    public Axis[] YAxes1 { get; set; } =
{
    };
    public Axis[] YAxes2 { get; set; } =
{
    };
    public Axis[] YAxes3 { get; set; } =
{
    };
    public Axis[] YAxes4 { get; set; } =
{
    };

    public Axis[] XAxes { get; set; } =
    {
    };

    public Axis[] XAxes1 { get; set; } =
    {
            new Axis
            {
  
            }
        };


    public ISeries[] Series5 { get; set; } =
    {
        new LineSeries<double>
        {

        }
    };
    public LabelVisual Title { get; set; } =
        new LabelVisual
        {

        };
}

