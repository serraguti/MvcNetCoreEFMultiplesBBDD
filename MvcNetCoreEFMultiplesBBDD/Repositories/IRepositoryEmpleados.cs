using MvcNetCoreEFMultiplesBBDD.Models;

namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    public interface IRepositoryEmpleados
    {
        Task<List<EmpleadoView>> GetEmpleadosAsync();
        Task<EmpleadoView> FindEmpleadoAsync(int idEmpleado);
    }
}
