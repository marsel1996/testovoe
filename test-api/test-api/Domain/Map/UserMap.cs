using FluentNHibernate.Mapping;
using test_api.Domain.Enums;
using test_api.Domain.Model;

namespace test_api.Domain.Map
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("test_user");

            Id(x => x.Id, "ID").GeneratedBy.Native();
            Map(x => x.FirstName, "first_name").Not.Nullable();
            Map(x => x.SurName, "sur_name").Not.Nullable();
            Map(x => x.Phone, "phone").Not.Nullable();
            Map(x => x.Email, "email").Nullable();
            Map(x => x.Password, "Password").Not.Nullable();
            Map(x => x.Role, "role").Not.Nullable()
                .CustomType<UserRoleEnum>();
        }
    }
}
