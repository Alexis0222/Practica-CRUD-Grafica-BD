using Microsoft.AspNetCore.Mvc.Rendering;

namespace PracticaMVCCore.Models.ViewModels.cs
{
    public class EmpleadoVM
    {
        public Empleado oEmpleado { get; set; }
        public List<SelectListItem> oListaCargo { get; set; }
    }
}
