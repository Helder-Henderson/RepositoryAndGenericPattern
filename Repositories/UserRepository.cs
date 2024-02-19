using blog.Models;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace blog.repositories;

internal class UserRepository : Repository<User> {
    private readonly SqlConnection Connection;
    public UserRepository(SqlConnection sqlConnection) 
    :   base(sqlConnection) 
            => Connection = sqlConnection;
    
    public List<User> GetWithRoles() {
        var query = @"
            SELECT 
                [USER].*,
                [Role].*
            FROM
                [User]
                LEFT JOIN [UserRole] ON [UserRole].[UserId] = [User].[Id]
                LEFT JOIN [Role] ON [UserRole].[RoleId] = [Role].[Id]
            ";

        var users = new List<User>();

        var items = Connection.Query<User,Role,User>(
            query,
            (user,role) => {
            var usr = users.FirstOrDefault(x => x.Id == user.Id);
            if(usr is null) {
                usr=user;
                if(role != null)
                    usr.Roles.Add(role);
                users.Add(usr);
            } else {
                usr.Roles.Add(role);
            }

            return user;
        },
        splitOn: "Id");


        return users;
    }
}