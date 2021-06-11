using System.Collections.Generic;
using System.Linq.Expressions;

namespace Package.Extensions.Linq
{
    internal interface IExpressionCollection : IEnumerable<Expression>
    {
        void Fill();
    }
}