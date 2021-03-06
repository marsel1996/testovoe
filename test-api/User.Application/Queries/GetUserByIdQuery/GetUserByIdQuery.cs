using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NHibernate;
using test_api.Configuration;
using test_api.Domain.Model;

namespace test_api.Queries.GetUserByIdQuery
{
    public class GetUserByIdQuery : IRequest<GetUserByIdDto>
    {
        public long Id { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, GetUserByIdDto>
    {
        public async Task<GetUserByIdDto> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            ISession session = SessionFactory.OpenSession;

            using (session)
            {
                var user = session.Get<User>(query.Id);

                return new GetUserByIdDto()
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    Phone = user.Phone,
                    RoleId = (long) user.Role,
                    SurName = user.SurName,
                };
            }
        }
    }
}
