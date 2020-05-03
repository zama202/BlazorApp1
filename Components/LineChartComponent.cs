using BlazorApp1.Model;
using ChartJs.Blazor.ChartJS.Common.Axes;
using ChartJs.Blazor.ChartJS.Common.Axes.Ticks;
using ChartJs.Blazor.ChartJS.Common.Enums;
using ChartJs.Blazor.ChartJS.Common.Handlers;
using ChartJs.Blazor.ChartJS.Common.Properties;
using ChartJs.Blazor.ChartJS.Common.Time;
using ChartJs.Blazor.ChartJS.LineChart;
using ChartJs.Blazor.ChartJS.MixedChart;
using ChartJs.Blazor.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Components
{
    public class LineChartComponent
    {

        public LineConfig config { get; set; }
        public LineDataset<TimeTuple<int>> dataset { get; set; }

        public LineOptions options { get; set; }
        protected void InitComponent() {

            options = new LineOptions
            {
                Responsive = true,
                Title = new OptionsTitle
                {
                    Display = false,
                    Text = ""
                },
                Legend = new Legend
                {
                    Position = Position.Right,
                    Labels = new LegendLabelConfiguration
                    {
                        UsePointStyle = true
                    }
                },
                Tooltips = new Tooltips
                {
                    Mode = InteractionMode.Nearest,
                    Intersect = false
                },
                Scales = new Scales
                {
                    xAxes = new List<CartesianAxis>
                    {
                        new TimeAxis
                        {
                            Distribution = TimeDistribution.Linear,
                            Ticks = new TimeTicks
                            {
                                Source = TickSource.Data
                            },
                            Time = new TimeOptions
                            {
                                Unit = TimeMeasurement.Millisecond,
                                Round = TimeMeasurement.Millisecond,
                                TooltipFormat = "DD.MM.YYYY HH:mm:ss:SSS",
                                DisplayFormats = TimeDisplayFormats.DE_CH
                            },
                            ScaleLabel = new ScaleLabel
                            {
                                LabelString = "Time"
                            }
                        }
                    }
                },
                Hover = new LineOptionsHover
                {
                    Intersect = true,
                    Mode = InteractionMode.Y
                }
            };

            

            config = new LineConfig();
        }


        public LineChartComponent () {

            InitComponent();
            config.Options = options;

            dataset = new LineDataset<TimeTuple<int>>
            {
                BackgroundColor = ColorUtil.RandomColorString(),
                BorderColor = ColorUtil.RandomColorString(),
                Label = "Temperature",
                Fill = false,
                BorderWidth = 2,
                PointRadius = 3,
                PointBorderWidth = 1,
                SteppedLine = SteppedLine.False
            };
            //Initial retrieve of Data
            //var forecasts = await _forecastService.GetForecastAsync(DateTime.Now, 2);
            List<TimeTuple<int>> e = new List<TimeTuple<int>>();
            var rand = new Random();
            e.ToList();
            for (int i = 0; i < 50; i++)
                e.Add(new TimeTuple<int>(new Moment(DateTime.Now.AddSeconds(-300 * i)), rand.Next(10, 100)));
            dataset.AddRange(e);

            //_tempDataSet.AddRange(forecasts.Select(p => new TimeTuple<int>(new Moment(p.Date), p.TemperatureC)));
            config.Data.Datasets.Add(dataset);
        }

        public void DoAction(string action, Event eventArgs)
        {
            if (action.Equals("add"))
            {
                if (eventArgs.Contains("Label") && eventArgs.Contains("Value") && eventArgs.Contains("Timestamp")) {
                    dataset = new LineDataset<TimeTuple<int>>
                    {
                        BackgroundColor = ColorUtil.RandomColorString(),
                        BorderColor = ColorUtil.RandomColorString(),
                        Label = eventArgs.Get("Label"),
                        Fill = false,
                        BorderWidth = 2,
                        PointRadius = 3,
                        PointBorderWidth = 1,
                        SteppedLine = SteppedLine.False
                    };
                    List<TimeTuple<int>> e = new List<TimeTuple<int>>();
                    var rand = new Random();
                    e.ToList();
                    for (int i = 0; i < 50; i++)
                        e.Add(new TimeTuple<int>(new Moment(DateTime.Now.AddSeconds(-300 * i)), rand.Next(50,100)));
                    dataset.AddRange(e);
                    dataset.Id = eventArgs.Get("Label");
                    config.Data.Datasets.Add(dataset);
                }
                
            }
            if (action.Equals("replace"))
            {
                if (eventArgs.Contains("Label") && eventArgs.Contains("Value") && eventArgs.Contains("Timestamp"))
                {
                    HashSet<IMixableDataset<object>> datasets = config.Data.Datasets;
                    for (int i = 0; i < datasets.Count; i++)
                    {
                        if (datasets.ElementAt(i).Id.Equals(eventArgs.Get("Label")) )
                            config.Data.Datasets.Remove(datasets.ElementAt(i));
                    }

                    dataset = new LineDataset<TimeTuple<int>>
                    {
                        BackgroundColor = ColorUtil.RandomColorString(),
                        BorderColor = ColorUtil.RandomColorString(),
                        Label = eventArgs.Get("Label"),
                        Fill = false,
                        BorderWidth = 2,
                        PointRadius = 3,
                        PointBorderWidth = 1,
                        SteppedLine = SteppedLine.False
                    };
                    List<TimeTuple<int>> e = new List<TimeTuple<int>>();
                    var rand = new Random();
                    e.ToList();
                    for (int i = 0; i < 10; i++)
                        e.Add(new TimeTuple<int>(new Moment(DateTime.Now.AddSeconds(-10000 * i)), rand.Next(50, 100)));
                    dataset.AddRange(e);
                    dataset.Id = eventArgs.Get("Label");
                    config.Data.Datasets.Add(dataset);
                }

            }


            //if (action.Equals("replace"))
            //{
            //    //First remove old label and piece of cake
            //    if (eventArgs.Contains("Label"))
            //    {
            //        int index = -1;
            //        List<string> labels = config.Data.Labels;
            //        for (int i = 0; i < labels.Count; i++)
            //        {
            //            if (eventArgs.Get("Label").Equals(labels.ElementAt(i)))
            //                index = i;
            //        }

            //        if (index > -1)
            //        {
            //            config.Data.Labels.RemoveAt(index);
            //            dataset.Data.RemoveAt(index);
            //        }
            //    }

            //    //then re-add
            //    if (eventArgs.Contains("Label"))
            //        config.Data.Labels.Add(eventArgs.Get("Label"));
            //    if (eventArgs.Contains("Value"))
            //    {
            //        var dataset = config.Data.Datasets.LastOrDefault();
            //        if (dataset != null)
            //            dataset.Data.Add(Convert.ToInt32(eventArgs.Get("Value")));
            //        dataset.BackgroundColor = dataset.BackgroundColor.IndexedValues.Append(ColorUtil.RandomColorString()).ToArray();
            //    }

            //}
        }

    }
}
