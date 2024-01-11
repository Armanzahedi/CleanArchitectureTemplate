using Ardalis.Specification;

namespace CA.Domain.Common.Specifications;


    public static class SpecificationBuilderExtensions
    {
        public static void Paginate<T>(this ISpecificationBuilder<T> query,
            int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            if (pageNumber > 1)
            {
                query = query.Skip((pageNumber - 1) * pageSize);
            }

            query
                .Take(pageSize);
        }
    }
