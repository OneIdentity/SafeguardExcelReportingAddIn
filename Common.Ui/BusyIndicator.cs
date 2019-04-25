using System;
using System.Windows;
using System.Windows.Controls;

namespace Safeguard.Common.Ui
{
    public class BusyIndicator : ContentControl
    {
        static BusyIndicator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyIndicator), new FrameworkPropertyMetadata(typeof(BusyIndicator)));
        }

        public static readonly DependencyProperty IsBusyProperty = DependencyProperty.Register(
            "IsBusy",
            typeof(bool),
            typeof(BusyIndicator),
            new PropertyMetadata(default(bool)));

        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        public static readonly DependencyProperty BusyMessageProperty = DependencyProperty.Register(
            "BusyMessage",
            typeof(string),
            typeof(BusyIndicator),
            new PropertyMetadata("Please wait..."));

        public string BusyMessage
        {
            get { return (string)GetValue(BusyMessageProperty); }
            set { SetValue(BusyMessageProperty, value); }
        }
    }
}
