using BlazorApp1.Components;
using BlazorApp1.Data;
using BlazorApp1.Model;
using BlazorDateRangePicker;
using ChartJs.Blazor.ChartJS.Common.Time;
using ChartJs.Blazor.ChartJS.LineChart;
using ChartJs.Blazor.ChartJS.PieChart;
using ChartJs.Blazor.Charts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlazorApp1.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        protected AlertService AlertService {get; set;}

        protected DateTimeOffset? StartDate { get; set; } = DateTime.Today.AddMonths(-1);
        protected DateTimeOffset? EndDate { get; set; } = DateTime.Today.AddDays(1).AddTicks(-1);


        //SIGNALR
        protected string Message { get; set; }
        protected string Log { get; set; }
        protected string Sender { get; set; }
        protected string BroadcastMessage { get; set; }
        protected List<string> Messages = new List<string>();

        protected string LastTsStatistics { get; set; }
        protected string LastTsAlerts { get; set; }

        protected List<Alert> Alerts;

        protected bool IsConnected => hubConnection.State == HubConnectionState.Connected;

        protected TwinEntity Twin { get; set; }


        private HubConnection hubConnection;

        protected override async Task OnInitializedAsync()
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5001/NotificationHub")
                .Build();

            hubConnection.On<string, Event>("UpdateTwin", (action, eventArg) =>
            {
                Twin.DoAction(action, eventArg);
                StateHasChanged();
            });
            hubConnection.On<string, Event>("UpdateAlerts", (action, eventArg) =>
            {
                LastTsAlerts = DateTime.Now.ToString("dddd, dd MMMM yyyy - HH:mm:ss");
                AlertService.DoAction(action, eventArg);
                StateHasChanged();
            });
            hubConnection.On<string, Event>("UpdateTelemetry", (action, eventArg) =>
            {
                lineChartComponent.DoAction(action, eventArg);
                _lineChartJs.Update();
                StateHasChanged();
            });
            hubConnection.On<string, Event>("UpdateStatistics", (action, eventArg) =>
            {
                LastTsStatistics = DateTime.Now.ToString("dddd, dd MMMM yyyy - HH:mm:ss");
                pieChartComponent.DoAction(action, eventArg);
                _pieChartJs.Update();
                StateHasChanged();
            });


            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var encodedMsg = $"{user}: {message}";
                Message = encodedMsg;
                Messages.Add(encodedMsg);
                StateHasChanged();
            });

            await hubConnection.StartAsync();
        }

        protected Task BroadcastAsync() { 
            return hubConnection.SendAsync("SendMessage", Sender, BroadcastMessage);
        }

        protected void OnRangeSelect(DateRange range)
        {
        }

        protected LineConfig _lineConfig;
        protected ChartJsLineChart _lineChartJs;
        protected LineDataset<TimeTuple<int>> _tempDataSet;

        protected PieConfig _pieconfig;
        protected ChartJsPieChart _pieChartJs;

        protected PieChartComponent pieChartComponent { get; set; }
        protected LineChartComponent lineChartComponent { get; set; }

        protected override void OnInitialized()
        {
            pieChartComponent = new PieChartComponent();
            lineChartComponent = new LineChartComponent();

            LastTsStatistics = DateTime.Now.ToString("dddd, dd MMMM yyyy - HH:mm:ss");
            LastTsAlerts = DateTime.Now.ToString("dddd, dd MMMM yyyy - HH:mm:ss");

            _pieconfig = pieChartComponent.config;
            _lineConfig = lineChartComponent.config;
            Twin = new TwinEntity();
            Twin.Init();
            Alerts = AlertService.GetAlertsAsync();
        }

        public void AlertAcknowledge(string alertCode)
        {
            AlertService.Delete(alertCode);
            Console.WriteLine("Button was clicked!");
        }

    }
}
