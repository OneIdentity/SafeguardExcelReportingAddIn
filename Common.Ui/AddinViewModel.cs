using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace Safeguard.Common.Ui
{

    public class AddinViewModel : ReactiveObject, ICanBeBusy
    {
        private readonly SourceList<Report> _reports = new SourceList<Report>();

        private ISafeguard Safeguard { get; }
        private IExcel Excel { get; }

        public AddinViewModel(ISafeguard safeguard, IExcel excel)
        {
            Safeguard = safeguard;
            Excel = excel;

            var canAuthenticate = this.WhenNotBusyAnd(
                this.WhenAnyValue(x => x.IsAuthenticated, x=>x.NetworkAddress)
                    .Select(x => !x.Item1 && !string.IsNullOrEmpty(x.Item2)));
            var authenticate = ReactiveCommand.Create(DoAuthenticate, canAuthenticate);
            AuthenticateCommand = authenticate;

            var canLogout = this.WhenNotBusyAnd(this.WhenAnyValue(x => x.IsAuthenticated));
            var logout = ReactiveCommand.Create(DoLogout, canLogout);
            LogoutCommand = logout;

            var canExecuteReport = this.WhenNotBusyAnd(this.WhenAnyValue(x => x.SelectedReport).Select(x => x != null));
            var execute = ReactiveCommand.CreateFromTask(DoExecute, canExecuteReport);
            ExecuteReportCommand = execute;

            _reports.Connect()
                .Bind(Reports)
                .Subscribe();

            safeguard.GetReports().ContinueWith(task =>
            {
                if (task.IsCompleted)
                {
                    _reports.Edit(inner =>
                    {
                        foreach (var report in task.Result)
                        {
                            inner.Add(new Report(report));
                        }
                    });
                }

                SelectedReport = Reports.First();
            });
        }

        public IReactiveCommand AuthenticateCommand { get; }
        public IReactiveCommand LogoutCommand { get; }
        public IReactiveCommand ExecuteReportCommand { get; }

        private bool _isAuthenticated = default(bool);
        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            set => this.RaiseAndSetIfChanged(ref _isAuthenticated, value);
        }

        private async Task DoAuthenticate()
        {
            using(this.SetBusy())
            {
                await Safeguard.Authenticate(NetworkAddress);
                IsAuthenticated = true;
            }
        }

        private async Task DoLogout()
        {
            using (this.SetBusy())
            {
                await Safeguard.Logout();
                IsAuthenticated = false;
            }
        }

        private async Task DoExecute()
        {
            if (SelectedReport == null) return;
            using (this.SetBusy())
            {
                await SelectedReport.Execute(Excel);
            }
        }

        public IObservableCollection<Report> Reports { get; } = new ObservableCollectionExtended<Report>();

        private Report _selectedReport = default(Report);
        public Report SelectedReport
        {
            get => _selectedReport; 
            set => this.RaiseAndSetIfChanged(ref _selectedReport, value); 
        }

        private bool _isBusy = default(bool);
        public bool IsBusy
        {
            get => _isBusy; 
            set => this.RaiseAndSetIfChanged(ref _isBusy, value); 
        }

        private string _networkAddress = default(string);
        public string NetworkAddress
        {
            get => _networkAddress; 
            set => this.RaiseAndSetIfChanged(ref _networkAddress, value); 
        }
    }

    public class Report : ReactiveObject
    {

        private readonly ISafeguardReport _report;

        public Report(ISafeguardReport report)
        {
            _report = report;
            Name = _report.Name;
            Description = _report.Description;
        }

        private string _name = default(string);
        public string Name
        {
            get => _name; 
            set => this.RaiseAndSetIfChanged(ref _name, value); 
        }

        private string _description = default(string);
        public string Description
        {
            get => _description; 
            set => this.RaiseAndSetIfChanged(ref _description, value); 
        }

        public async Task Execute(IExcel excel)
        {
            await _report.Execute(excel);
        }
    }
}