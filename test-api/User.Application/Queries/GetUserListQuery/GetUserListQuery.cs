using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NHibernate;
using test_api.Configuration;
using test_api.Domain.Model;

namespace test_api.Queries.GetUserListQuery
{
    public class GetUserListQuery : IRequest<GetUserListItemDto[]>
    {
    }

    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, GetUserListItemDto[]>
    {
        public GetUserListQueryHandler()
        {
        }

        public async Task<GetUserListItemDto[]> Handle(GetUserListQuery query, CancellationToken cancellationToken)
        {
            ISession session = SessionFactory.OpenSession;

            using(session)
            {
                var users = session.QueryOver<User>()
                    .List();

                return users
                    .Select(u => u.MapToDto())
                    .ToArray();
            }
        }
    }
}
