using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Comparer
{
    public class Equality<T>
    {
        public static IEqualityComparer<T> CreateComparer(Func<T, T, bool> equalsExpression)
        {
            return new CommonEqualityComparer(equalsExpression, null);
        }

        public static IEqualityComparer<T> CreateComparer(Func<T, T, bool> equalsExpression, Func<T, int> getHashCodeExpression)
        {
            return new CommonEqualityComparer(equalsExpression, getHashCodeExpression);
        }

        private class CommonEqualityComparer : IEqualityComparer<T>
        {
            private Func<T, T, bool> _equalsExpression;
            private Func<T, int> _getHashCodeExpression;

            public CommonEqualityComparer(Func<T, T, bool> equalsExpression, Func<T, int> getHashCodeExpression)
            {
                _equalsExpression = equalsExpression;
                _getHashCodeExpression = getHashCodeExpression;
            }

            public bool Equals(T x, T y)
            {
                return _equalsExpression(x, y);
            }

            public int GetHashCode(T obj)
            {
                if (_getHashCodeExpression == null)
                {
                    return 0;
                }

                return _getHashCodeExpression(obj);
            }
        }
    }
}
