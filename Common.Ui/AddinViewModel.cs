using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Security.Cryptography.X509Certificates;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace Safeguard.Common.Ui
{

    public class AddinViewModel : ReactiveObject
    {
        private readonly SourceList<Report> _reports = new SourceList<Report>();

        public AddinViewModel()
        {
            var authenticate = ReactiveCommand.Create(DoAuthenticate, 
                this.WhenAnyValue(x => x.IsAuthenticated).Select(x=>!x));
            AuthenticateCommand = authenticate;

            var logout = ReactiveCommand.Create(DoLogout, 
                this.WhenAnyValue(x => x.IsAuthenticated));
            LogoutCommand = logout;

            _reports.Connect()
                .Bind(Reports)
                .Subscribe();

            _reports.Edit(inner =>
            {
                inner.Add(new Report {Name = "User Entitlements"});
                inner.Add(new Report {Name = "Yesterday's Access Requests"});
                inner.Add(new Report {Name = "Some BullCrap"});
                inner.Add(new Report {Name = "Dan's stuff"});
            });
            SelectedReport = Reports.First();
        }

        public IReactiveCommand AuthenticateCommand { get; }
        public IReactiveCommand LogoutCommand { get; }

        private bool _isAuthenticated = default(bool);
        public bool IsAuthenticated
        {
            get => _isAuthenticated;
            set => this.RaiseAndSetIfChanged(ref _isAuthenticated, value);
        }

        private void DoAuthenticate()
        {
            // Whatever we need to do here

            IsAuthenticated = true;
        }

        private void DoLogout()
        {
            // Whatever we need to do here

            IsAuthenticated = false;
        }

        
        public IObservableCollection<Report> Reports { get; } = new ObservableCollectionExtended<Report>();

        private Report _selectedReport = default(Report);
        public Report SelectedReport
        {
            get => _selectedReport; 
            set => this.RaiseAndSetIfChanged(ref _selectedReport, value); 
        }



    }

    public class Report : ReactiveObject
    {
        private string _name = default(string);
        public string Name
        {
            get => _name; 
            set => this.RaiseAndSetIfChanged(ref _name, value); 
        }
    }
}