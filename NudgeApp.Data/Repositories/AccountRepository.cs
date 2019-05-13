namespace NudgeApp.Data.Repositories
{
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class AccountRepository : Repository<AccountEntity>, IAccountRepository
    {
        public AccountRepository(INudgeDbContext context) : base(context) { }
    }
}
