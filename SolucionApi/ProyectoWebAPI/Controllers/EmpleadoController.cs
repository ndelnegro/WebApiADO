using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProyectoData;
using ProyectoModelo;

namespace ProyectoWebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly EmpleadoData _empleadoData;

        public EmpleadoController(EmpleadoData empleadoData)
        {
            _empleadoData = empleadoData;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Empleado> list = await _empleadoData.Lista();
            return StatusCode(StatusCodes.Status200OK, list);
        }
        
        [HttpPost]
        public async Task<ActionResult> CrearEmpleado([FromBody] Empleado empleado)
        {   
            bool respuesta = await _empleadoData.Crear(empleado);
            return StatusCode(StatusCodes.Status200OK, new { isSucces = respuesta });

        }

        [HttpPut]
        public async Task<ActionResult> EditarEmpleado([FromBody] Empleado empleado)
        {
            bool respuesta = await _empleadoData.Editar(empleado);
            return StatusCode(StatusCodes.Status200OK, new { isSucces = respuesta });

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarEmpleado(int id)
        {
            bool respuesta = await _empleadoData.Eliminar(id);
            return StatusCode(StatusCodes.Status200OK, new { isSucces = respuesta });

        }


    }
}
