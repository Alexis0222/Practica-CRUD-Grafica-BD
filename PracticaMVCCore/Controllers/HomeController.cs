using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using PracticaMVCCore.Models;
using PracticaMVCCore.Models.ViewModels;
using System.Diagnostics;
using PracticaMVCCore.Models.ViewModels.cs;

namespace PracticaMVCCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly PracticaCoreContext _dbContext;

        public HomeController(PracticaCoreContext context)
        {
            _dbContext = context;
        }

        public IActionResult ResumenEmpleadosCargoDona()
        {


            List<Empleado> list = new List<Empleado>();
            List<CargoVM> listVM = new List<CargoVM>();
            int programador = 0, disenador = 0, Analista = 0;
            list = _dbContext.Empleados.Include(c => c.oCargo).ToList();
            foreach (Empleado empleado in list)
            {
                if (empleado.oCargo.Descripcion.Equals("Programador"))
                {
                    programador++;
                }
                else if (empleado.oCargo.Descripcion.Equals("Disenador"))
                {
                    disenador++;
                }
                else
                {
                    Analista++;
                }
            }
            listVM.Add(new CargoVM() { Cargo = "programador", Cantidad = programador });
            listVM.Add(new CargoVM() { Cargo = "Diseñador", Cantidad = disenador });
            listVM.Add(new CargoVM() { Cargo = "Analista", Cantidad = Analista });
            return StatusCode(StatusCodes.Status200OK, listVM);
        }
        public IActionResult ResumenEmpleadosCargoPie()
        {


            List<Empleado> list = new List<Empleado>();
            List<CargoVM> listVM = new List<CargoVM>();
            int programador = 0, disenador = 0, Analista = 0;
            list = _dbContext.Empleados.Include(c => c.oCargo).ToList();
            foreach (Empleado empleado in list)
            {
                if (empleado.oCargo.Descripcion.Equals("Programador"))
                {
                    programador++;
                }
                else if (empleado.oCargo.Descripcion.Equals("Disenador"))
                {
                    disenador++;
                }
                else
                {
                    Analista++;
                }
            }
            listVM.Add(new CargoVM() { Cargo = "programador", Cantidad = programador });
            listVM.Add(new CargoVM() { Cargo = "Diseñador", Cantidad = disenador });
            listVM.Add(new CargoVM() { Cargo = "Analista", Cantidad = Analista });
            return StatusCode(StatusCodes.Status200OK, listVM);
        }

        public IActionResult Index()
        {
            List<Empleado> list = new List<Empleado>();
            list = _dbContext.Empleados.Include(c => c.oCargo).ToList();
            return View(list);

        }
        [HttpGet]
        public IActionResult Empleado_Detalle(int idEmpleado)
        {
            EmpleadoVM oempleadoVM = new EmpleadoVM()
            {
                oEmpleado = new Empleado(),
                oListaCargo = _dbContext.Cargos.Select(cargo => new SelectListItem()
                {
                    Text = cargo.Descripcion,
                    Value = cargo.IdCargo.ToString()
                }).ToList()
            };
            if (idEmpleado != 0)
            {
                oempleadoVM.oEmpleado = _dbContext.Empleados.Find(idEmpleado);
            }
            return View(oempleadoVM);
        }
        [HttpPost]
        public IActionResult Empleado_Detalle(EmpleadoVM oEmpleadoVM)
        {
            if (oEmpleadoVM.oEmpleado.IdEmpleado == 0)
            {
                _dbContext.Empleados.Add(oEmpleadoVM.oEmpleado);
            }
            else
            {
                _dbContext.Empleados.Update(oEmpleadoVM.oEmpleado);
            }
            _dbContext.SaveChanges();

            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Eliminar(int idEmpleado)
        {
            Empleado oEmpleado = _dbContext.Empleados.Include(c => c.oCargo).Where(c => c.IdEmpleado == idEmpleado).FirstOrDefault();

            return View(oEmpleado);
 
        }
        [HttpPost]
        public IActionResult Eliminar(Empleado oEmpleado)
        {
            _dbContext.Empleados.Remove(oEmpleado);
            _dbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}