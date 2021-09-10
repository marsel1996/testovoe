using System;
using FluentValidation;
using NHibernate;
using test_api.Configuration;
using test_api.Domain.Model;

namespace test_api.Application.UserApplication.Commands.SaveUserCommand
{
    public class UniqueUserEmailValidator : AbstractValidator<SaveUserCommand>
    {
        public UniqueUserEmailValidator()
        {
            RuleFor(command => command)
                .Custom((command, validationContext) =>
                {
                    if(string.IsNullOrEmpty(command.Email)) return;
                    ISession session = SessionFactory.OpenSession;

                    using (session)
                    {
                        var countOtherUserWithSameEmail = session.QueryOver<User>()
                            .Where((u) => u.Email != null && u.Email == command.Email)
                            .RowCount();
                        
                        if (countOtherUserWithSameEmail > 0)
                        {
                            validationContext.AddFailure("Уже существует пользователь с таким email.");
                        }
                    }
                });
        }
    }
}
