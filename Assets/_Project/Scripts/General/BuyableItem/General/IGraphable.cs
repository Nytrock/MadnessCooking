using System.Collections.Generic;

namespace MadnessCooking.General {
    public interface IGraphable<TItem> {
        IEnumerable<TItem> NeedItems { get; }
        IEnumerable<TItem> NextItems { get; }
    }
}
