namespace CaminhoLivre.Compartilhado.Exceptions;
public abstract class DomainException(string mensagem) : Exception(mensagem)
{
}

// Exceções genéricas de infraestrutura/protocolo HTTP
public class NotFoundException(string mensagem) : DomainException(mensagem)
{
}

public class ConflictException(string mensagem) : DomainException(mensagem)
{
}

public class BusinessRuleException(string mensagem) : DomainException(mensagem)
{
}