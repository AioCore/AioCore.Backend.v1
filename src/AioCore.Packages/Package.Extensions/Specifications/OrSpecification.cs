using Package.Extensions.Linq;

namespace Package.Extensions.Specifications
{
    public class OrSpecification<T> : Specification<T>
    {
        public OrSpecification(ISpecification<T> left, ISpecification<T> right) : base(left.Predicate.OrElse(right.Predicate))
        {
        }
    }
}
