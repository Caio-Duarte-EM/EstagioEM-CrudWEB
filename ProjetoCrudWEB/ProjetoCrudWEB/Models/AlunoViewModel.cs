using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjetoCrudWEB.Models;

public class AlunoViewModel
{
    public List<Aluno> Alunos { get; set; }
    public string? StringPesquisa { get; set; }
}
