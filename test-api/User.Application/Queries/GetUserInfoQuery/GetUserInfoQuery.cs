using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NHibernate;
using test_api.Configuration;
using test_api.Domain.Model;

namespace test_api.Queries.GetUserInfoQuery
{
    public class GetUserInfoQuery : IRequest<GetUserInfoDto>
    {
        public long Id { get; set; }
    }

    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, GetUserInfoDto>
    {
        public GetUserInfoQueryHandler()
        {
        }

        public async Task<GetUserInfoDto> Handle(GetUserInfoQuery query, CancellationToken cancellationToken)
        {
            ISession session = SessionFactory.OpenSession;

            using (session)
            {
                var user = session.Get<User>(query.Id);

                return user != null
                    ? new GetUserInfoDto() { 
                        Email = user.Email,
                        FullName = $"{user.SurName} {user.FirstName}",
                        Phone = user.Phone,
                    }
                    : null;
            }
        }
    }
}
