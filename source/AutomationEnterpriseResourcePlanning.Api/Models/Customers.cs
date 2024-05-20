using AutomationEnterpriseResourcePlanning.Api.Models.Abstractions;

namespace AutomationEnterpriseResourcePlanning.Api.Models;

public class Customers : Entity
{
    public string RazaoSocial { get; private set; }
    public string NomeFantasia { get; private set; }
    public string CpfCnpj { get; private set; }

    private const string RazaoSocialEmptyMessage = "Razão Social não pode ser vazia.";
    private const string NomeFantasiaEmptyMessage = "Nome Fantasia não pode ser vazio.";
    private const string CpfCnpjEmptyMessage = "CPF/CNPJ não pode ser vazio.";
    public Customers(string razaoSocial, string nomeFantasia, string cpfCnpj)
    {
        RazaoSocial = !string.IsNullOrWhiteSpace(razaoSocial) ? razaoSocial : throw new ArgumentNullException(nameof(razaoSocial), RazaoSocialEmptyMessage);
        NomeFantasia = !string.IsNullOrWhiteSpace(nomeFantasia) ? nomeFantasia : throw new ArgumentNullException(nameof(nomeFantasia), NomeFantasiaEmptyMessage);
        CpfCnpj = !string.IsNullOrWhiteSpace(cpfCnpj) ? cpfCnpj : throw new ArgumentNullException(nameof(cpfCnpj), CpfCnpjEmptyMessage);
    }
}
