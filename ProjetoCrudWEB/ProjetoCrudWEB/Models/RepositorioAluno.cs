using FirebirdSql.Data.FirebirdClient;
using System.Linq.Expressions;

namespace ProjetoCrudWEB.Models;

public class RepositorioAluno : RepositorioAbstrato<Aluno>
{
    public string caminhoConexao = @"DataSource=localhost; Port=3054; Database=D:\DBESTAGIO.FDB; username= SYSDBA; password = masterkey";
    public FbConnection? conn;

    public override void Add(Aluno aluno)
    {
        conn = new(caminhoConexao);
        conn.Open();
        FbCommand comandoAdd = new($"INSERT INTO TBALUNO (ALUMATRICULA, ALUNOME, ALUSEXO, ALUNASCIMENTO, ALUCPF) VALUES ({aluno.Matricula}, '{aluno.Nome}', {(int)aluno.Sexo}, '{aluno.Nascimento:dd.MM.yyyy}', '{aluno.Cpf}');", conn);
        comandoAdd.ExecuteNonQuery();
        conn.Close();
    }

    public override void Remove(Aluno aluno)
    {
        conn = new(caminhoConexao);
        conn.Open();
        FbCommand comandoRemove = new($"DELETE FROM TBALUNO WHERE ALUMATRICULA = {aluno.Matricula};", conn);
        comandoRemove.ExecuteNonQuery();
        conn.Close();
    }

    public override void Update(Aluno aluno)
    {
        conn = new(caminhoConexao);
        conn.Open();
        FbCommand comandoUpdate = new($"UPDATE TBALUNO SET ALUMATRICULA = {aluno.Matricula}," +
            $"ALUNOME = '{aluno.Nome}'," +
            $"ALUSEXO = {(int)aluno.Sexo}," +
            $"ALUNASCIMENTO = '{aluno.Nascimento:dd.MM.yyyy}'," +
            $"ALUCPF = '{aluno.Cpf}'" +
            $"WHERE ALUMATRICULA = {aluno.Matricula}; ", conn);
        comandoUpdate.ExecuteNonQuery();
        conn.Close();
    }

    public override IEnumerable<Aluno> GetAll()
    {
        conn = new(caminhoConexao);
        conn.Open();
        FbCommand comandoGetAll = new($"SELECT * FROM TBALUNO", conn);
        FbDataReader reader = comandoGetAll.ExecuteReader();
        List<Aluno> alunos = new();
        while (reader.Read())
        {
            Aluno aluno = new(
                Convert.ToInt32(reader["ALUMATRICULA"]),
                reader["ALUNOME"].ToString() ?? "",
                (EnumeradorSexo)reader["ALUSEXO"],
                Convert.ToDateTime(reader["ALUNASCIMENTO"]),
                reader["ALUCPF"].ToString() ?? ""
            );
            alunos.Add(aluno);
        }
        reader.Close();
        conn.Close();
        return alunos.OrderBy(aluno => aluno.Matricula);
    }

    public override IEnumerable<Aluno> Get(Expression<Func<Aluno, bool>> predicate)
    {
        return GetAll().Where(predicate.Compile());
    }

    public Aluno GetByMatricula(int matricula)
    {
        return Get(aluno => aluno.Matricula == matricula).First();
    }

    public IEnumerable<Aluno> GetByContendoNoNome(string parteDoNome)
    {
        return Get(aluno => aluno.Nome.ToLower().Contains(parteDoNome)).ToList();
    }

    public IEnumerable<Aluno> GetByContendoNaMatricula(string parteDaMatricula)
    {
        return Get(aluno => aluno.Matricula.ToString().Contains(parteDaMatricula)).ToList();
    }
}