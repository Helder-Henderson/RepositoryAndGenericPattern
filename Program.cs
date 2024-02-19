using System.Data;
using System.Diagnostics.Tracing;
using System.Reflection.Metadata;
using blog.Models;
using blog.repositories;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

const string CONNECTION_STRING = @"";

var connection = new SqlConnection(CONNECTION_STRING);

ReadUserWithRoles();


#region SELECT
static void ReadUsers(SqlConnection connection) {
    var repository = new Repository<User>(connection);
    var users = repository.Get();

    foreach(var user in users) 
        Console.WriteLine(user.Name);
}

static void ReadRoles(SqlConnection connection) {
    var repository = new RoleRepository(connection);
    var roles = repository.Get();

    foreach(var role in roles) 
        Console.WriteLine(role.Name);
}

static void ReadTags(SqlConnection connection) {
    var repository = new Repository<Tag>(connection);
    var tags = repository.Get();

    foreach(var tag in tags) 
        Console.WriteLine(tag.Name);
}

static void ReadUser(int id) {
    using (var connection = new SqlConnection(CONNECTION_STRING)) {
        var user = connection.Get<User>(id);

        Console.WriteLine(user.Name);
    }
}

static void ReadUserWithRoles() {
    using (var connection = new SqlConnection(CONNECTION_STRING)) {
        var repository = new UserRepository(connection);
        var items = repository.GetWithRoles();

        foreach(var user in items) {
            Console.WriteLine(user.Name);
        }
    }
}
#endregion

static void CreateUser() {
      var user = new User() {
            Bio="TesteBio",
            Email="Email@email.com.br",
            Slug="/Teste",
            PasswordHash="Hash",
            Name="Testeeee",
            Image="Imagemaqui"
        };

    using (var connection = new SqlConnection(CONNECTION_STRING)) {
        connection.Insert<User>(user);
        Console.WriteLine("Cadastro realizado com sucesso");
    }
}


static void UpdateUser(int id) {
      var user = new User() {
            Id = id,
            Bio="TesteUpdateBio",
            Email="updated@email.com.br",
            Slug="/Teste",
            PasswordHash="Hash",
            Name="Testeeee",
            Image="Imagemaqui"
        };

    using (var connection = new SqlConnection(CONNECTION_STRING)) {
        connection.Update<User>(user);
        Console.WriteLine("Atualizacao realizada com sucesso");
    }
}


static void DeleteUser(int id) {
    
    using (var connection = new SqlConnection(CONNECTION_STRING)) {
        var user = connection.Get<User>(id);
        connection.Delete<User>(user);
        Console.WriteLine("Deletado com sucesso");
    }
}