using System.ComponentModel.DataAnnotations;

namespace ProjetoCrudWEB.Models;

public class Aluno : IEntidade
{
    [Range(1, 999999999, ErrorMessage = "Matrícula só pode ter 9 digitos e não pode ser igual ou menor que 0.")]
    [Required(ErrorMessage = "Matrícula não pode ser vazio.")]
    public int Matricula { get; set; }

    [RegularExpression(@"^[a-zA-Zà-úÀ-Ú""'\s-]*$", ErrorMessage = "Nome só pode conter letras ou espaços.")]
    [StringLength(99, MinimumLength = 1, ErrorMessage = "Nome não pode ter mais de mais de 100 caracteres.")]
    [Required(ErrorMessage = "Nome não pode ser nulo.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Por favor selecione uma opção válida.")]
    public EnumeradorSexo Sexo { get; set; }

    [Display(Name = "Data de Nascimento")]
    [DataType(DataType.Date, ErrorMessage = "Selecione uma data válida")]
    [Required(ErrorMessage = "Data de nascimento não pode ser vazio.")]
    public DateTime Nascimento { get; set; }

    [RegularExpression(@"^[0-9\.-]*$", ErrorMessage = "Cpf só pode conter letras, '.' ou '-'.")]
    [StringLength(14, ErrorMessage = "Cpf não pode ter mais de 14 caracteres.")]
    public string Cpf { get; set; }

    public Aluno(int matricula, string nome, EnumeradorSexo sexo, DateTime nascimento, string cpf)
    {
        Matricula = matricula;
        Nome = nome;
        Sexo = sexo;
        Nascimento = nascimento;
        Cpf = cpf;
    }

    public override bool Equals(object? obj)
    {
        return obj is Aluno aluno &&
               Matricula == aluno.Matricula &&
               Nome == aluno.Nome &&
               Cpf == aluno.Cpf &&
               Nascimento == aluno.Nascimento &&
               Sexo == aluno.Sexo;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Matricula, Nome, Cpf, Nascimento, Sexo);
    }

    public override string ToString()
    {
        return $"Matrícula: {Matricula}" +
               $"Nome: {Nome}" +
               $"Sexo: {Sexo}" +
               $"Data de Nascimento: {Nascimento}" +
               $"Cpf: {Cpf}";
    }
}