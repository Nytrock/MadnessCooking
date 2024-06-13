using System.Collections.Generic;

public interface IGraphable<TItem> {
    IEnumerable<TItem> NeedItems { get; }
    IEnumerable<TItem> NextItems { get; }
}
