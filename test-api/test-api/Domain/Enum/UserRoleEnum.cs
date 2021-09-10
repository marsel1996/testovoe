using System.ComponentModel;

namespace test_api.Domain.Enums
{
    public enum UserRoleEnum
    {
        [Description("Администратор")]
        Admin = 1,
        [Description("Менеджер")]
        Manager = 2,
        [Description("Сотрудник")]
        Worker = 3,
    }
}
