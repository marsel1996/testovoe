using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NHibernate;
using test_api.Configuration;
using test_api.Domain.Model;

namespace test_api.Commands.RemoveUser
{
    public class RemoveUserCommand : IRequest<Unit>
    {
        public long Id { get; set; }
    }

    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, Unit>
    {
        public RemoveUserCommandHandler() 
        {

        }

        public async Task<Unit> Handle(RemoveUserCommand command, CancellationToken cancellationToken)
        {
            ISession session = SessionFactory.OpenSession;

            using (var transaction = session.Transaction)
            {
                transaction.Begin();
                var user = session.Get<User>(command.Id);
                session.Delete(user);
                transaction.Commit();
            }
            return Unit.Value;
        }
    }
}
