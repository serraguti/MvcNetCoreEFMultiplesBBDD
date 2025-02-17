using Microsoft.EntityFrameworkCore;
using MvcNetCoreEFMultiplesBBDD.Data;
using MvcNetCoreEFMultiplesBBDD.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

#region VISTAS Y PROCEDIMIENTOS
/*
create or replace view V_EMPLEADOS
as
       select EMP.EMP_NO as IDEMPLEADO
       , EMP.APELLIDO, EMP.OFICIO
       , EMP.SALARIO, DEPT.DNOMBRE AS DEPARTAMENTO
       , DEPT.LOC AS LOCALIDAD
       from EMP
       inner join DEPT
       on EMP.DEPT_NO=DEPT.DEPT_NO;
create or replace procedure SP_ALL_VEMPLEADOS
(p_cursor_empleados out sys_refcursor)
as
begin
  open p_cursor_empleados for
  select * from V_EMPLEADOS;
end;
 */
#endregion

namespace MvcNetCoreEFMultiplesBBDD.Repositories
{
    public class RepositoryEmpleadosOracle : IRepositoryEmpleados
    {
        private HospitalContext context;

        public RepositoryEmpleadosOracle(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<EmpleadoView>> GetEmpleadosAsync()
        {
            string sql = "begin ";
            sql += " SP_ALL_VEMPLEADOS (:p_cursor_empleados);";
            sql += " end;";
            OracleParameter pamCursor = new OracleParameter();
            pamCursor.ParameterName = "p_cursor_empleados";
            pamCursor.Value = null;
            pamCursor.Direction = ParameterDirection.Output;
            //DEBEMOS INDICAR EL TIPO DE DATO DE ORACLE CURSOR
            pamCursor.OracleDbType = OracleDbType.RefCursor;
            var consulta = this.context.EmpleadosView
                .FromSqlRaw(sql, pamCursor);
            return await consulta.ToListAsync();
        }

        public async Task<EmpleadoView> FindEmpleadoAsync(int idEmpleado)
        {
            var consulta = from datos in this.context.EmpleadosView
                           where datos.IdEmpleado == idEmpleado
                           select datos;
            var data = await consulta.ToListAsync();
            return data.First();
        }
    }
}
