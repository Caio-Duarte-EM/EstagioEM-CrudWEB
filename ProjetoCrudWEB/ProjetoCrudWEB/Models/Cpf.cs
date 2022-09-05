namespace ProjetoCrudWEB.Models;

public readonly struct Cpf
{
    public readonly string Valor;
    private Cpf(string possivelCPF)
    {
        Valor = Utilidades.ValorCpfEhValido(possivelCPF) ? Utilidades.FormateCpf(possivelCPF) : "invalido";
    }

    public static implicit operator string(Cpf cpf) => cpf.Valor;
    public static explicit operator Cpf(string v) => new(v);
}