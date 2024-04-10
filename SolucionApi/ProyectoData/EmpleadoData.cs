using Microsoft.Extensions.Options;
using System.Data.SqlClient;
using System.Data;
using ProyectoModelo;
namespace ProyectoData
{
    public class EmpleadoData
    {
        private readonly ConnectionStrings _connection;

        public EmpleadoData(IOptions<ConnectionStrings> options)
        {
            _connection = options.Value;

        }


        public async Task<bool> Crear (Empleado objeto)
        {
            bool respuesta = true;
            using (var conexion = new SqlConnection(_connection.CadenaSql))
            {
                await conexion.OpenAsync ();
                SqlCommand cmd = new SqlCommand("sp_crearEmpleado", conexion);
                cmd.Parameters.AddWithValue("@NombreCompleto",objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@IdDepartamento",objeto.Departamento!.IdDepartamento);
                cmd.Parameters.AddWithValue("@Sueldo",objeto.sueldo);
                cmd.Parameters.AddWithValue("@FechaContrato",objeto.FechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await conexion.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync()> 0 ? true : false;


                }
                catch 
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }


        public async Task<List<Empleado>> Lista()
        {
            List<Empleado> empleado = new List<Empleado>();
            using (var conexion = new SqlConnection( _connection.CadenaSql))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_listadoEmpleados", conexion);
                
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader= await cmd.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        empleado.Add(new Empleado {
                            IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            sueldo = Convert.ToDecimal(reader["sueldo"]),
                            FechaContrato = reader["FechaContrato"].ToString(),
                            Departamento = new Departamento
                            {
                                IdDepartamento = Convert.ToInt32(reader["IdDepartamento"]),
                                Nombre = reader["Nombre"].ToString(),
                            }
                        });
                    }
                }
            }
            return empleado;
        }
        public async Task<bool> Editar(Empleado objeto)
        {
            bool result = true;
            using (var conexion = new SqlConnection(_connection.CadenaSql))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_editarEmpleado", conexion);
                cmd.Parameters.AddWithValue("@IdEmpleado", objeto.IdEmpleado);
                cmd.Parameters.AddWithValue("@NombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@IdDepartamento", objeto.Departamento);
                cmd.Parameters.AddWithValue("@Sueldo", objeto.sueldo);
                cmd.Parameters.AddWithValue("@FechaContrato", objeto.FechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await conexion.OpenAsync();
                    result = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;


                }
                catch (Exception ex)
                {
                    result = false;
                }
            }
            return result;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool result = true;
            using (var conexion = new SqlConnection(_connection.CadenaSql))
            {
                
                SqlCommand cmd = new SqlCommand("sp_eliminarEmpleado", conexion);
                cmd.Parameters.AddWithValue("@IdEmpleado", id);
                cmd.CommandType = CommandType.StoredProcedure;
                
                try
                {
                   await conexion.OpenAsync() ;
                   result = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;


                }
                catch (Exception ex)
                {
                    result = false;
                }
            }
            return result;
        }
    }
    }

