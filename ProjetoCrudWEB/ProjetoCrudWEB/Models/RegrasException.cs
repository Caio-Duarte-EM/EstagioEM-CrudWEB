using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq.Expressions;
using System.Web.Mvc;
using ModelStateDictionary = Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace ProjetoCrudWEB.Models;

public class RegrasException : Exception
{
    protected IList<ViolacaoDeRegra> _erros = new List<ViolacaoDeRegra>();
    private readonly Expression<Func<object, object>> _objeto = x => x;

    public IEnumerable<ViolacaoDeRegra> Erros { get { return _erros; } }

    internal void AdicionarErroAoModelo(string mensagem)
    {
        _erros.Add(new ViolacaoDeRegra { Propriedade = _objeto, Mensagem = mensagem });
    }
}

public class RegrasException<TModelo> : RegrasException
{
    internal void AdicionarErroPara<TPropriedade>(Expression<Func<TModelo, TPropriedade>> propriedade, string mensagem)
    {
        _erros.Add(new ViolacaoDeRegra { Propriedade = propriedade, Mensagem = mensagem });
    }
}

public class ViolacaoDeRegra
{
    public LambdaExpression? Propriedade { get; internal set; }
    public string Mensagem { get; internal set; }
}

public static class ExtensoesDeRegrasException
{
    public static void CopiarErrosPara(this RegrasException ex, ModelStateDictionary modelState, string? prefixo = null)
    {
        prefixo = string.IsNullOrWhiteSpace(prefixo) ? "" : prefixo + ".";

        foreach (var erro in ex.Erros)
        {
            var propriedade = ExpressionHelper.GetExpressionText(erro.Propriedade);
            var chave = string.IsNullOrWhiteSpace(propriedade) ? "" : prefixo + propriedade;
            modelState.AddModelError(chave, erro.Mensagem);
        }
    }
}