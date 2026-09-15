using DeliveryApp.Dominio.Modulos.Clientes;
using DeliveryApp.Infraestrutura.Compartilhado.Orm;
using DeliveryApp.Infraestrutura.Orm;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Infraestrutura.Modulos.Clientes;

public sealed class RepositorioClienteEmOrm(
    DeliveryAppDbContext dbContext
) : RepositorioBaseEmOrm<Cliente>(dbContext), IRepositorioCliente
{
    public Task<bool> ExistePorIdAsync(Guid clienteId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExisteRegistroComCpfAsync(
        string cpf,
        CancellationToken cancellationToken = default
    )
    {
        return await registros.AnyAsync(r => r.Cpf == cpf, cancellationToken);
    }
}
