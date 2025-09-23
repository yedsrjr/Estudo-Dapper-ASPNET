using Dapper;
using Models.Entidades;

namespace Models.Data;

public class FuncionariosRepository : AbstractRepository<Funcionarios>
{
    private readonly DapperContext context;
    public FuncionariosRepository(DapperContext _context)
    {
        this.context = _context;
    }
    public override void Atualizar(Funcionarios model)
    {
        string query = @"UPDATE Funcionarios SET
            nome = @nome,
            idade = @idade,
            CPF = @CPF,
            cidade = @cidade, 
            salario = @salario
        WHERE codf = @codf";

        using (var connection = context.CretateConnection())
        {
            connection.ExecuteScalar(query, model);
        }
    }

    public override Funcionarios? Buscar(int id)
    {
        string query = "SELECT * FROM Funcionarios where codf = @id";

        using (var connection = context.CretateConnection())
        {
            return connection.QueryFirstOrDefault<Funcionarios>(query, new { id});
        }
    }

    public override List<Funcionarios> BuscarTodos()
    {
        string query = "SELECT * FROM Funcionarios";

        using (var connection = context.CretateConnection())
        {
            return connection.Query<Funcionarios>(query).ToList();
        }
    }

    public override void Excluir(Funcionarios model)
    {
       string query = "DELETE FROM Funcionarios where codf = @id";

        using (var connection = context.CretateConnection())
        {
            connection.Execute(query, new {id = model.codf });
        }
    }

    public override void Salvar(Funcionarios model)
    {
        string queryID = "SELECT MAX(codf) codf FROM Funcionarios";

        string query = @"INSERT INTO Funcionarios(codf, nome, idade, CPF, cidade, salario)
            VALUES (@codf, @nome, @idade, @CPF, @cidade, @salario)";

        using (var connection = context.CretateConnection())
        {
            var maxID = connection.QueryFirst<int>(queryID);
            model.codf = maxID + 1;
            connection.ExecuteScalar(query, model);
        }
    }
}