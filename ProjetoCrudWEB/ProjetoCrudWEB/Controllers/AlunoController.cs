using Microsoft.AspNetCore.Mvc;
using ProjetoCrudWEB.Models;
using System.Threading.Tasks;

namespace ProjetoCrudWEB.Controllers;
public class AlunoController : Controller
{
    private readonly RepositorioAluno repositorio = new();
    public IActionResult Index(string stringPesquisa)
    {
        var alunoVM = new AlunoViewModel
        {
            Alunos = repositorio.GetAll().ToList()
        };

        if (!string.IsNullOrEmpty(stringPesquisa))
        {
            bool ehNumerico = int.TryParse(stringPesquisa, out _);
            if (ehNumerico)
            {
                alunoVM.Alunos = repositorio.GetByContendoNaMatricula(stringPesquisa).ToList();
                return View(alunoVM);
            }
            else
            {
                alunoVM.Alunos = repositorio.GetByContendoNoNome(stringPesquisa.ToLower()).ToList();
                return View(alunoVM);
            }
        }
        return View(alunoVM);
    }

    public IActionResult Adicionar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Adicionar(int matricula, string nome, EnumeradorSexo sexo, DateTime nascimento, string cpf)
    {
        string cpfFinal;
        if(cpf is null)
        {
            cpfFinal = "";
        }
        else
        {
            cpfFinal = (Cpf)(cpf.Replace(".",string.Empty).Replace("-",string.Empty));
        }
        Aluno novoAluno = new(matricula, nome, sexo, nascimento, cpfFinal);
        try
        {
            if (Utilidades.DadosSaoValidos(novoAluno, repositorio, 0))
            {
            }
        }
        catch (RegrasException ex)
        {
            ex.CopiarErrosPara(ModelState);
        }
        if (ModelState.IsValid)
        {
            repositorio.Add(novoAluno);
            return RedirectToAction(nameof(Index));
        }
        return View(novoAluno);
    }

    public IActionResult Editar(int id)
    {
        var aluno = repositorio.GetByMatricula(id);
        return View(aluno);
    } 

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(int matricula, string nome, EnumeradorSexo sexo, DateTime nascimento, string cpf, int id)
    {
        string cpfFinal;
        if (cpf is null)
        {
            cpfFinal = "";
        }
        else
        {
            cpfFinal = (Cpf)(cpf.Replace(".",string.Empty).Replace("-",string.Empty));
        }
        Aluno alunoEditado = new(matricula, nome, sexo, nascimento, cpfFinal);
        try
        {
            if (Utilidades.DadosSaoValidos(alunoEditado, repositorio, id))
            {
            }
        }
        catch (RegrasException ex)
        {
            ex.CopiarErrosPara(ModelState);
        }
        if (ModelState.IsValid)
        {
            repositorio.Update(alunoEditado);
            return RedirectToAction(nameof(Index));
        }
        return View(alunoEditado);
    } 

    public IActionResult Remover(int id)
    {
        var aluno = repositorio.GetByMatricula(id);
        return View(aluno);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remover(int id, bool notUsed)
    {
        var aluno = repositorio.GetByMatricula(id);
        repositorio.Remove(aluno);
        return RedirectToAction(nameof(Index));
    }
}