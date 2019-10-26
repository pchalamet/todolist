using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Services
{

    public class Todo
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
    }

    public interface ITodosDbService
    {
        IEnumerable<Todo> Load();
        void Add(Todo todo);
        void Remove(Todo todo);
    }

    public class StubDbService : ITodosDbService
    {
        public StubDbService(string cnString)
        {
            
        }

        public void Add(Todo todo)
        {
        }

        public IEnumerable<Todo> Load()
        {
            return new Todo[] { new Todo { Id = Guid.NewGuid(), Text = "Toto" } };
        }

        public void Remove(Todo todo)
        {
        }
    }

    public class TodosDbService : ITodosDbService
    {
        private readonly string cnString;

        public TodosDbService(string cnString)
        {
            this.cnString = cnString;
        }

        public IEnumerable<Todo> Load()
        {
            using var cn = new Microsoft.Data.SqlClient.SqlConnection(cnString);
            return cn.Query<Todo>("select id, text from Todos").ToArray();
        }

        public void Add(Todo todo)
        {
            using var cn = new Microsoft.Data.SqlClient.SqlConnection(cnString);
            cn.ExecuteScalar("insert into Todos (id, text) values (@Id, @Text)", todo);
        }

        public void Remove(Todo todo)
        {
            using var cn = new Microsoft.Data.SqlClient.SqlConnection(cnString);
            cn.ExecuteScalar("delete Todos where id = @Id", todo);
        }

    }
}
