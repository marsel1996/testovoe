using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NHibernate;
using test_api.Configuration;
using test_api.Domain.Enums;
using test_api.Helpers;

namespace test_api.Application.UserApplication.Commands.SaveUserCommand
{
    public class SaveUserCommand : IRequest<Unit>
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public long Role { get; set; }
    }

    public class SaveUserCommandHandler : IRequestHandler<SaveUserCommand, Unit>
    {
        public async Task<Unit> Handle(SaveUserCommand command, CancellationToken cancellationToken)
        {
            ISession session = SessionFactory.OpenSession;

            using (var transaction = session.Transaction)
            {
                transaction.Begin();
                var user = new Domain.Model.User()
                {
                    Email = command.Email,
                    FirstName = command.FirstName,
                    Phone = command.Phone,
                    SurName = command.SurName,
                    Role = (UserRoleEnum) command.Role,
                    Password = Md5Hash.CreateMD5(command.Password),
                };
                session.Save(user);
                transaction.Commit();
            }
            return Unit.Value;
        }
    }
}
