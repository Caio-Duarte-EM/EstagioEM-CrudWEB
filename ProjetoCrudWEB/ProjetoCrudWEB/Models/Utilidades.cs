namespace ProjetoCrudWEB.Models;

public abstract class Utilidades
{
    public static bool ValorCpfEhValido(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
        {
            return false;
        }

        if (cpf.Length != 11)
        {
            return false;
        }

        foreach (char c in cpf)
        {
            if (!char.IsNumber(c))
            {
                return false;
            }
        }

        if (cpf.Equals("00000000000") ||
        cpf.Equals("11111111111") ||
        cpf.Equals("22222222222") ||
        cpf.Equals("33333333333") ||
        cpf.Equals("44444444444") ||
        cpf.Equals("55555555555") ||
        cpf.Equals("66666666666") ||
        cpf.Equals("77777777777") ||
        cpf.Equals("88888888888") ||
        cpf.Equals("99999999999"))
        {
            return false;
        }

        int somaDigitos = 0;
        int multiplicacaoDigitos;
        for (int i = 0; i < 9; i++)
        {
            multiplicacaoDigitos = int.Parse(cpf[i].ToString()) * (i + 1);
            somaDigitos += multiplicacaoDigitos;
        }

        int primeiroDigitoVerificador = somaDigitos % 11;
        if (primeiroDigitoVerificador == 10)
        {
            primeiroDigitoVerificador = 0;
        }
        if (primeiroDigitoVerificador != int.Parse(cpf[9].ToString()))
        {
            return false;
        }

        somaDigitos = 0;
        for (int i = 0; i < 10; i++)
        {
            multiplicacaoDigitos = int.Parse(cpf[i].ToString()) * (i);
            somaDigitos += multiplicacaoDigitos;
        }

        int segundoDigitoVerificador = somaDigitos % 11;
        if (segundoDigitoVerificador == 10)
        {
            segundoDigitoVerificador = 0;
        }
        if (segundoDigitoVerificador != int.Parse(cpf[10].ToString()))
        {
            return false;
        }

        return true;
    }

    public static string FormateCpf(string cpf)
    {
        return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
    }

    public static bool DadosSaoValidos(Aluno aluno, RepositorioAluno repositorio, int id = -1)
    {
        var regrasException = new RegrasException<Aluno>();

        int matricula = aluno.Matricula;
        if (id != matricula)
        {
            foreach (var alunoTeste in repositorio.GetAll())
            {
                if (alunoTeste.Matricula == matricula)
                {
                    regrasException.AdicionarErroPara(x => x.Matricula, "Já existe um aluno com essa matrícula.");
                    throw regrasException;
                }
            }
        }

        DateTime dataAtual = DateTime.Now;
        var DataDeNascimento = aluno.Nascimento;
        if (DataDeNascimento.Year < 1900 || DataDeNascimento > dataAtual)
        {
            regrasException.AdicionarErroPara(x => x.Nascimento, "Insira uma data entre 01/01/1900 e a data de hoje.");
            throw regrasException;
        }

        string CpfTeste = aluno.Cpf.Replace(".", string.Empty).Replace("-", string.Empty);
        if (aluno.Cpf == string.Empty)
        {
            return true;
        }
        else if ((Cpf)CpfTeste == "invalido")
        {
            regrasException.AdicionarErroPara(x => x.Cpf, "Esse CPF não é válido.");
            throw regrasException;
        }
        else
        {
            foreach (Aluno alunoTeste in repositorio.GetAll())
            {
                if (alunoTeste.Cpf == aluno.Cpf && alunoTeste.Matricula != matricula)
                {
                    regrasException.AdicionarErroPara(x => x.Cpf, "Já existe um aluno com esse CPF.");
                    throw regrasException;
                }
            }
        }
        return true;
    }
}