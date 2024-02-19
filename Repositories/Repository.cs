using blog.Models;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace blog.repositories;

internal class Repository<TModel> where TModel : class {
    private readonly SqlConnection Connection;
    public Repository(SqlConnection sqlConnection) => Connection = sqlConnection;
    
    public IEnumerable<TModel> Get() => Connection.GetAll<TModel>();

    public TModel GetById(int id) => Connection.Get<TModel>(id);    

    public void Insert(TModel model) => Connection.Insert<TModel>(model);

    public void Delete(TModel model) => Connection.Delete<TModel>(model);
}