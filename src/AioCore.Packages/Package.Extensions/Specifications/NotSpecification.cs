using Package.Extensions.Linq;

namespace Package.Extensions.Specifications
{
    public class NotSpecification<T> : Specification<T>
    {
        public NotSpecification(ISpecification<T> spec) : base(spec.Predicate.Not())
        {
        }

    }
}
