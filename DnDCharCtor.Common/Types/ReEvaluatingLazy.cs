namespace DnDCharCtor.Common.Types;

public class ReEvaluatingLazy<T>
{
    private readonly Func<T> _valueFactory;

    private Lazy<T> _lazy;

    public ReEvaluatingLazy(Func<T> valueFactory)
    {
        _valueFactory = valueFactory ?? throw new ArgumentNullException(nameof(valueFactory));
        _lazy = new Lazy<T>(valueFactory);
    }

    public T Value => _lazy.Value;

    public void ReEvaluate()
    {
        _lazy = new Lazy<T>(_valueFactory);
    }
}
