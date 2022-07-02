using System.Drawing;
using Blazored.Toast.Services;
using ChartJs.Blazor;
using ChartJs.Blazor.Common;
using ChartJs.Blazor.Common.Axes;
using ChartJs.Blazor.Common.Axes.Ticks;
using ChartJs.Blazor.Common.Enums;
using ChartJs.Blazor.Common.Handlers;
using ChartJs.Blazor.Common.Time;
using ChartJs.Blazor.Interop;
using ChartJs.Blazor.LineChart;
using ChartJs.Blazor.Util;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json.Linq;
using StreamingClient.Models;

namespace StreamingClient.Pages
{
    public partial class StreamingData : IAsyncDisposable
    {
        private HubConnection _hub;
        private CancellationTokenSource _cancellationTokenSource;
        private LineConfig _chartConfig;
        public Chart _lineChartJs;
        private IDataset<TimePoint> _data;
        private int _recordsReceived;
        [Inject] public IToastService ToastService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            SetupCancellation();
            InitializeChart();
            await InitializeSignalR();

        }

        private void SetupCancellation()
        {
            if (_cancellationTokenSource != null)
                _cancellationTokenSource = null;

            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void InitializeChart()
        {


            _chartConfig = new LineConfig
            {
                Options = new LineOptions
                {
                    Responsive = true,
                    Animation = new Animation() { Duration = 500, Easing = Easing.EaseInOutSine },
                    MaintainAspectRatio = true,
                    Title = new OptionsTitle
                    {
                        Display = true,
                        Text = "Product Price History"
                    },
                    Tooltips = new Tooltips
                    {
                        Mode = InteractionMode.Nearest,
                        Intersect = true
                    },
                    Hover = new Hover
                    {
                        Mode = InteractionMode.Nearest,
                        Intersect = true,
                    },
                    Scales = new Scales
                    {
                        XAxes = new List<CartesianAxis>
                        {
                            new TimeAxis
                            {
                                ScaleLabel = new ScaleLabel
                                {
                                    LabelString = "Date"
                                },
                                Ticks = new TimeTicks()
                                {
                                    Display = true,
                                    AutoSkip = true
                                },
                                Time = new TimeOptions
                                {
                                    TooltipFormat = "ll HH:mm",
                                    Unit = TimeMeasurement.Day,
                                    MinUnit = TimeMeasurement.Day,
                                    StepSize = 20
                                },
                                Bounds = ScaleBound.Ticks,
                                Offset = true,
                                Display = AxisDisplay.Auto,
                                Weight = 20,
                            }
                        },
                        YAxes = new List<CartesianAxis>
                        {
                            new LinearCartesianAxis
                            {
                                ScaleLabel = new ScaleLabel
                                {
                                    LabelString = "Value"
                                }
                            }
                        }
                    }
                }
            };

            _data = new LineDataset<TimePoint>()
            {
                BackgroundColor = ColorUtil.FromDrawingColor(Color.Green),
                BorderColor = ColorUtil.FromDrawingColor(Color.Green),
                Fill = FillingMode.Disabled,
                ShowLine = true,
                Label = "Price History Over Time",
                HoverBorderCapStyle = BorderCapStyle.Square
            };
        }


        private async Task InitializeSignalR()
        {

            _hub = new HubConnectionBuilder().WithUrl("http://localhost:5087/StreamingHub")
                .WithAutomaticReconnect()
                .Build();


            await _hub.StartAsync();
            ToastService.ShowSuccess("Signal R Connected!", "Success!");
        }

        private async Task UpdateDataChart(StreamingHubModel model)
        {

            _data.Add(new TimePoint(model.PriceTime, (double)model.PriceValue));

            await InvokeAsync(async () =>
            {
                _chartConfig.Data.Datasets.Add(_data);

                await _lineChartJs.Update();
                base.StateHasChanged();
            });


        }

        private async Task RequestPrices()
        {
            SetupCancellation();
            var streamResult = _hub.StreamAsync<StreamingHubModel>("RequestStream", _recordsReceived, cancellationToken: _cancellationTokenSource.Token);

            await foreach (var item in streamResult)
            {
                await UpdateDataChart(item);
                _recordsReceived++;
            }
        }

        private void PauseStreaming()
        {
            _cancellationTokenSource.Cancel();
        }

        private void ResetStreaming()
        {
            _cancellationTokenSource.Cancel();
            _chartConfig.Data.Datasets.Clear();
            _data.Clear();
            _recordsReceived = 0;
        }

        public async ValueTask DisposeAsync()
        {
            if (_hub is not null)
                await _hub.DisposeAsync();

            _cancellationTokenSource?.Dispose();
        }
    }
}
