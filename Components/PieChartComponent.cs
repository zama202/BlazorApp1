using BlazorApp1.Model;
using ChartJs.Blazor.ChartJS.Common.Enums;
using ChartJs.Blazor.ChartJS.Common.Handlers;
using ChartJs.Blazor.ChartJS.Common.Properties;
using ChartJs.Blazor.ChartJS.PieChart;
using ChartJs.Blazor.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp1.Components
{
    public class PieChartComponent
    {
        public PieConfig config { get; set; }
        public PieDataset dataset { get; set; }
        public PieOptions options { get; set; }

        protected List<string> Labels { get; set; }
        protected List<double> Values { get; set; }


        public PieChartComponent()
        {
            InitComponent();
            config.Options = options;

            //Initial Service CallBack
            config.Data.Labels.AddRange(new[] { "reject001", "reject002", "reject003", "reject004", "reject005" });
            dataset.Data.AddRange(new double[] { 40, 12, 36, 7, 28 });

            config.Data.Datasets.Add(dataset);
        }


        public void DoAction(string action, Event eventArgs)
        {
            if (action.Equals("add"))
            {
                if (eventArgs.Contains("Label"))
                    config.Data.Labels.Add(eventArgs.Get("Label"));
                if (eventArgs.Contains("Value"))
                {
                    var dataset = config.Data.Datasets.LastOrDefault();
                    if (dataset != null)
                        dataset.Data.Add(Convert.ToInt32(eventArgs.Get("Value")));
                    dataset.BackgroundColor = dataset.BackgroundColor.IndexedValues.Append(ColorUtil.RandomColorString()).ToArray();
                }
            }

            if (action.Equals("replace"))
            {
                //First remove old label and piece of cake
                if (eventArgs.Contains("Label"))
                {
                    int index = -1;
                    List<string> labels = config.Data.Labels;
                    for (int i = 0; i < labels.Count; i++)
                    {
                        if (eventArgs.Get("Label").Equals(labels.ElementAt(i)))
                            index = i;
                    }

                    if (index > -1)
                    {
                        config.Data.Labels.RemoveAt(index);
                        dataset.Data.RemoveAt(index);
                    }
                }

                //then re-add
                if (eventArgs.Contains("Label"))
                    config.Data.Labels.Add(eventArgs.Get("Label"));
                if (eventArgs.Contains("Value"))
                {
                    var dataset = config.Data.Datasets.LastOrDefault();
                    if (dataset != null)
                        dataset.Data.Add(Convert.ToInt32(eventArgs.Get("Value")));
                    dataset.BackgroundColor = dataset.BackgroundColor.IndexedValues.Append(ColorUtil.RandomColorString()).ToArray();
                }

            }
        }

        protected void InitComponent()
        {
            options = new PieOptions
            {
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
                Responsive = true,
                Animation = new ArcAnimation
                {
                    AnimateRotate = true,
                    AnimateScale = true
                }
            };
            dataset = new PieDataset
            {
                BackgroundColor = new[] { ColorUtil.RandomColorString(), ColorUtil.RandomColorString(), ColorUtil.RandomColorString(), ColorUtil.RandomColorString(), ColorUtil.RandomColorString() },
                BorderWidth = 0,
                HoverBackgroundColor = ColorUtil.RandomColorString(),
                HoverBorderColor = ColorUtil.RandomColorString(),
                HoverBorderWidth = 1,
                BorderColor = "#ffffff",
            };
            config = new PieConfig();

        }

    }
}
