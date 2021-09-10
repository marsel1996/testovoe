using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NHibernate;
using test_api.Configuration;

namespace test_api.Application.UserApplication.Commands.RemoveUser
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
                var user = session.Get<Domain.Model.User>(command.Id);
                session.Delete(user);
                transaction.Commit();
            }
            return Unit.Value;
        }
    }
}
