using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace Safeguard.Common.Ui
{
    public interface ICanBeBusy
    {
        bool IsBusy { get; set; }
    }

    public static class BusyExtensions
    {
        public static IDisposable SetBusy(this ICanBeBusy busy)
        {
            busy.IsBusy = true;
            return Disposable.Create(() => busy.IsBusy = false);
        }

        public static IObservable<bool> WhenNotBusy(this ICanBeBusy busy)
        {
            return busy.WhenAnyValue(x => x.IsBusy).Select(x=>!x);
        }

        public static IObservable<bool> WhenBusy(this ICanBeBusy busy)
        {
            return busy.WhenAnyValue(x => x.IsBusy);
        }

        public static IObservable<bool> WhenNotBusyAnd(this ICanBeBusy busy, IObservable<bool> predicate)
        {
            return WhenNotBusy(busy).CombineLatest(predicate, (a, b) => a && b);
        }

        public static IObservable<bool> WhenBusyAnd(this ICanBeBusy busy, IObservable<bool> predicate)
        {
            return WhenBusy(busy).CombineLatest(predicate, (a, b) => a && b);
        }
    }
}
