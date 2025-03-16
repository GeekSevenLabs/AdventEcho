using GeekSevenLabs.AdventEcho.Domain.Shared.Enums;
using GeekSevenLabs.Utilities.Documents;

namespace GeekSevenLabs.AdventEcho.Domain.People;

public record DocumentVo : ValueObject
{

    public DocumentVo(DocumentType type, string number)
    {
        Type = type;
        Number = number;
        
        Throw.When.True(Type is DocumentType.Cpf && CadastroPessoaFisica.IsValid(number), "CPF inválido");
    }
    
    public string Number { get; set; }
    public DocumentType Type { get; set; }
}