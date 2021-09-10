using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NHibernate;
using test_api.Configuration;

namespace test_api.Application.UserApplication.Queries.GetUserListQuery
{
    public class GetUserListQuery : IRequest<GetUserListItemDto[]>
    {
        public string Pattern { get; set; }
    }

    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, GetUserListItemDto[]>
    {
        public async Task<GetUserListItemDto[]> Handle(GetUserListQuery query, CancellationToken cancellationToken)
        {
            ISession session = SessionFactory.OpenSession;

            using(session)
            {
                var users = session.QueryOver<Domain.Model.User>().List();
                if (!string.IsNullOrEmpty(query.Pattern))
                {
                    users = users.Where(u => u.Email.Contains(query.Pattern) ||
                                               u.FirstName.Contains(query.Pattern) ||
                                               u.SurName.Contains(query.Pattern)).ToArray();
                }

                return users
                    .Select(u => u.MapToDto())
                    .ToArray();
            }
        }
    }
}
