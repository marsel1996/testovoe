using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NHibernate;
using test_api.Configuration;
using test_api.Domain.Enums;

namespace test_api.Application.UserApplication.Commands.EditUserCommand
{
    public class EditUserCommand : IRequest<Unit>
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long Role { get; set; }
    }

    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, Unit>
    {
        public EditUserCommandHandler() 
        {

        }

        public async Task<Unit> Handle(EditUserCommand command, CancellationToken cancellationToken)
        {
            ISession session = SessionFactory.OpenSession;

            using (var transaction = session.Transaction)
            {
                transaction.Begin();
                var user = session.Get<Domain.Model.User>(command.Id);

                user.Email = command.Email;
                user.FirstName = command.FirstName;
                user.Phone = command.Phone;
                user.SurName = command.SurName;
                user.Role = (UserRoleEnum) command.Role;

                session.SaveOrUpdate(user);
                transaction.Commit();
            }
            return Unit.Value;
        }
    }
}
