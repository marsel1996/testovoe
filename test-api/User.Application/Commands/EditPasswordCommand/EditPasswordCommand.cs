using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace User.Application.Commands.EditPasswordCommand
{
    public class EditPasswordCommand : IRequest<Unit>
    {
        public long Id { get; set; }
        public string Password { get; set; }
    }

    public class EditPasswordCommandHandler : IRequestHandler<EditPasswordCommand, Unit>
    {
        public async Task<Unit> Handle(EditPasswordCommand command, CancellationToken cancellationToken)
        {
            ISession session = SessionFactory.OpenSession;
            var hash = Md5Hash.CreateMD5(command.Password);

            using (var transaction = session.Transaction)
            {
                transaction.Begin();
                var user = session.Get<User>(command.Id);
                user.Password = hash;
                session.Save(user);
                transaction.Commit();
            }
            return Unit.Value;
        }
    }
}
