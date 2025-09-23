using AspNet_MVC.Models.Entidades;
using Dapper;
using Models.Data;

public class AmbulatoriosRepository : AbstractRepository<Ambulatorios>
{
    private readonly DapperContext context;

    public AmbulatoriosRepository(DapperContext _context)
    {
        this.context = _context;
    }

    public override void Atualizar(Ambulatorios model)
    {
        string query = @"UPDATE Ambulatorios SET
            nroa = @nroa,
            andar = @andar,
            capacidade = @capacidade
        WHERE nroa = @nroa
        ";

        using (var connection = context.CretateConnection())
        {
            connection.ExecuteScalar(query, model);
        }
    }

    public override Ambulatorios? Buscar(int id)
    {
        string query = @"SELECT * FROM Ambulatorios
            WHERE nroa = @nroa
        ";

        using (var connection = context.CretateConnection())
        {
            return connection.QueryFirstOrDefault<Ambulatorios>(query, new { nroa = id });
        }
    }

    public override List<Ambulatorios> BuscarTodos()
    {
        string query = "SELECT * FROM Ambulatorios";

        using (var connection = context.CretateConnection())
        {
            return connection.Query<Ambulatorios>(query).ToList();
        }
    }

    public override void Excluir(Ambulatorios model)
    {
        string query = @"DELETE FROM Ambulatorios
            WHERE nroa = @nroa ";

        using (var connection = context.CretateConnection())
        {
            connection.Execute(query, new { nroa = model.nroa });
        }
      
    }

    public override void Salvar(Ambulatorios model)
    {
        string query_id = @"SELECT MAX(nroa) AS nroa FROM Ambulatorios";

        string query = @"INSERT INTO Ambulatorios(nroa, andar, capacidade)
            VALUES (@nroa, @andar, @capacidade)
        ";

        using (var connection = context.CretateConnection())
        {
            var max_id = connection.QueryFirst<int>(query_id);
            model.nroa = max_id + 1;
            connection.ExecuteScalar(query, model); 
        }
    }
}