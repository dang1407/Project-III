using Dapper;
using WebAPI.Application;
using WebAPI.Domain;

namespace WebAPI.Infrastructure
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<User?> FindAccountAsync(string username, string password)
        {
            string sql = "SELECT * FROM Account WHERE UserName = @UserName";
            var param = new DynamicParameters();
            param.Add("UserName", username);     
            var user = await Uow.Connection.QuerySingleOrDefaultAsync<User>(sql, param);
            return user;
        }
    }
}
