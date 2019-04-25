using System;
using System.Reactive.Linq;
using DynamicData;
using ReactiveUI;

namespace SafeguardAddin
{

    public class AddinViewModel : ReactiveObject
    {

        public AddinViewModel()
        {
            var authenticate =
                ReactiveCommand.Create(DoAuthenticate, this.WhenAny(x => x.IsAuthenticated, x => !x.Value));
            AuthenticateCommand = authenticate;

            var logout = ReactiveCommand.Create(DoLogout, this.WhenAnyValue(x => x.IsAuthenticated));
            LogoutCommand = logout;

            var obs = Reports.Connect();
            Reports.Edit(inner =>
            {
                inner.Add(new Report {Name = "User Entitlements"});
                inner.Add(new Report {Name = "Yesterday's Access Requests"});
                inner.Add(new Report {Name = "Some BullCrap"});
                inner.Add(new Report {Name = "Dan's stuff"});
            });
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

        private DynamicData.SourceList<Report> _reports = new SourceList<Report>();
        public DynamicData.SourceList<Report> Reports
        {
            get => _reports; 
            set => this.RaiseAndSetIfChanged(ref _reports, value); 
        }

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