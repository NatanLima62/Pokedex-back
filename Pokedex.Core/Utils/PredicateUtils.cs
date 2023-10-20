using System.Linq.Expressions;

namespace Pokedex.Core.Utils;

public static class PredicateUtils
{
    private static class Predicate<T>
    {
        public static readonly Expression<Func<T, bool>> TrueExpression = item => true;
    }
    public static Expression<Func<T, bool>> True<T>() { return Predicate<T>.TrueExpression; }
}