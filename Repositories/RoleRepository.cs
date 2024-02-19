using blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace blog.repositories;

internal class RoleRepository {
    private readonly SqlConnection Connection;
    public RoleRepository(SqlConnection sqlConnection) => Connection = sqlConnection;
    
    public IEnumerable<Role> Get() => Connection.GetAll<Role>();

    public Role GetById(int id) => Connection.Get<Role>(id);    

    public void Insert(Role role) => Connection.Insert<Role>(role);

    public void Delete(Role role) => Connection.Delete<Role>(role);
}