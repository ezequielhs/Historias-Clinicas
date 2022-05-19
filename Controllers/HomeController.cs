using Historias_Clinicas_D.Data;
using Historias_Clinicas_D.Helpers;
using Historias_Clinicas_D.Models;
using Historias_Clinicas_D.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Historias_Clinicas_D.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<IdentityRole<int>> _rolManager;
        private readonly HistoriasClinicasContext _context;
        

        public HomeController(ILogger<HomeController> logger, UserManager<Persona> userManager, RoleManager<IdentityRole<int>> rolManager, HistoriasClinicasContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _rolManager = rolManager;
            _context = context;
        }

        public IActionResult Index()
        {
            if (this.User.IsInRole(Defaults.RolPaciente))
            {
                int idPaciente = int.Parse(_userManager.GetUserId(this.User));
                return RedirectToAction("Details", "Pacientes", new { id = idPaciente });
            }

            return View();
        }

        public IActionResult Nosotros()
        {
            return View();
        }

        public async Task<IActionResult> CargarBD()
        {
            #region Roles

            List<string> roles = new List<string>() 
            { 
                Defaults.RolAdmin,
                Defaults.RolPaciente,
                Defaults.RolMedico,
                Defaults.RolEmpleado
            };

            foreach (string rol in roles)
            {
                await _rolManager.CreateAsync(new IdentityRole<int>(rol));
            }

            #endregion

            #region Administrador

            Persona admin = new Persona()
            {
                Nombre = "Ezequiel",
                Apellido = "Hoyos",
                DNI = "39267310",
                Email = Defaults.AdminUserName,
                UserName = Defaults.AdminUserName
            };
            if (_context.Personas.FirstOrDefault(persona => persona.UserName == admin.UserName) == null)
            {
                var resultCreateUser = await _userManager.CreateAsync(admin, Defaults.AdminPassword);

                if (resultCreateUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, Defaults.RolAdmin);
                }
            }

            #endregion

            #region Pacientes

            List<Paciente> pacientes = new List<Paciente>();

            pacientes.Add(new Paciente()
            {
                ObraSocial = ObraSocial.GALENO,
                Nombre = "Fernando",
                Apellido = "Lopez",
                DNI = "37043674",
                Email = "fernando_lopez@gmail.com",
                UserName = "fernando_lopez@gmail.com"
            });

            pacientes.Add(new Paciente()
            {
                ObraSocial = ObraSocial.SWISS_MEDICAL,
                Nombre = "Antonio Luis",
                Apellido = "Tello",
                DNI = "32919178",
                Email = "antonio.tello@yahoo.com",
                UserName = "antonio.tello@yahoo.com"
            });

            pacientes.Add(new Paciente()
            {
                ObraSocial = ObraSocial.MEDICUS,
                Nombre = "Luis Enrique",
                Apellido = "Mena",
                DNI = "3757545",
                Email = "luismena@outlook.com",
                UserName = "luismena@outlook.com"
            });

            pacientes.Add(new Paciente()
            {
                ObraSocial = ObraSocial.OSDE,
                Nombre = "Leopoldo",
                Apellido = "Mariscal",
                DNI = "6593902",
                Email = "mariscal._leopoldo@gmail.com",
                UserName = "mariscal._leopoldo@gmail.com"
            });

            pacientes.Add(new Paciente()
            {
                ObraSocial = ObraSocial.OSDE,
                Nombre = "Guillermo",
                Apellido = "Alcala",
                DNI = "17473304",
                Email = "guillermo_alcala@hotmail.com",
                UserName = "guillermo_alcala@hotmail.com"
            });

            foreach (Paciente paciente in pacientes)
            {
                var resultCreateUser = await _userManager.CreateAsync(paciente, Defaults.PacientePassword);

                if (resultCreateUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(paciente, Defaults.RolPaciente);
                }
            }

            #endregion

            #region Empleados

            List<Empleado> empleados = new List<Empleado>();

            empleados.Add(new Empleado()
            {
                Nombre = "Alejandro",
                Apellido = "Ponce",
                DNI = "39647489",
                Email = "aleponce@outlook.com",
                UserName = "aleponce@outlook.com"
            });

            empleados.Add(new Empleado()
            {
                Nombre = "Miriam",
                Apellido = "Escobar",
                DNI = "18600338",
                Email = "m.escobar@gmail.com",
                UserName = "m.escobar@gmail.com"
            });

            empleados.Add(new Empleado()
            {
                Nombre = "Carmen",
                Apellido = "Barrios",
                DNI = "17261727",
                Email = "carmen-barrios@yahoo.com",
                UserName = "carmen-barrios@yahoo.com"
            });

            empleados.Add(new Empleado()
            {
                Nombre = "Federico",
                Apellido = "Martínez",
                DNI = "23917753",
                Email = "fernando__martinez@live.com",
                UserName = "fernando__martinez@live.com"
            });

            empleados.Add(new Empleado()
            {
                Nombre = "Gerardo",
                Apellido = "Guzmán",
                DNI = "5960550",
                Email = "gerardoguzman@outlook.com",
                UserName = "gerardoguzman@outlook.com"
            });

            foreach (Empleado empleado in empleados)
            {
                var resultCreateUser = await _userManager.CreateAsync(empleado, Defaults.EmpleadoPassword);
                if (resultCreateUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(empleado, Defaults.RolEmpleado);
                }
            }

            #endregion

            #region Medicos

            List<Medico> medicos = new List<Medico>();

            medicos.Add(new Medico()
            {
                TipoMatricula = TipoMatricula.NACIONAL,
                Matricula = "368412",
                Especialidad = Especialidad.PEDIATRIA,
                Nombre = "Nicolas",
                Apellido = "Rivera",
                DNI = "41777762",
                Email = "nicolas.rivera@gmail.com",
                UserName = "nicolas.rivera@gmail.com"
            });

            medicos.Add(new Medico()
            {
                TipoMatricula = TipoMatricula.NACIONAL,
                Matricula = "368412",
                Especialidad = Especialidad.NEUMOLOGIA,
                Nombre = "Oscar",
                Apellido = "Herrero",
                DNI = "28321201",
                Email = "herrero_oscar@hotmail.com",
                UserName = "nicolas.rivera@gmail.com"
            });

            medicos.Add(new Medico()
            {
                TipoMatricula = TipoMatricula.PROVINCIAL,
                Matricula = "368412",
                Especialidad = Especialidad.INFECTOLOGIA,
                Nombre = "Bianca",
                Apellido = "Perez",
                DNI = "9154238",
                Email = "bianca._perez@outlook.com",
                UserName = "bianca._perez@outlook.com"
            });

            medicos.Add(new Medico()
            {
                TipoMatricula = TipoMatricula.PROVINCIAL,
                Matricula = "368412",
                Especialidad = Especialidad.ENDOCRINOLOGIA,
                Nombre = "Marina",
                Apellido = "Cerdan",
                DNI = "6731568",
                Email = "marinacerdan@yahoo.com",
                UserName = "marinacerdan@yahoo.com"
            });

            medicos.Add(new Medico()
            {
                TipoMatricula = TipoMatricula.NACIONAL,
                Matricula = "368412",
                Especialidad = Especialidad.NEUROLOGIA,
                Nombre = "Judit",
                Apellido = "Romero",
                DNI = "5975505",
                Email = "judit-romero@gmail.com",
                UserName = "judit-romero@gmail.com"
            });

            foreach (Medico medico in medicos)
            {
                var resultCreateUser = await _userManager.CreateAsync(medico, Defaults.MedicoPassword);
                if (resultCreateUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(medico, Defaults.RolMedico);
                }
            }

            #endregion

            #region Direcciones

            List<Direccion> direcciones = new List<Direccion>();

            direcciones.Add(new Direccion()
            {
                Calle = "Montevideo",
                Numero = 248,
                CodigoPostal = "C1037ADA",
                Localidad = "Capital Federal",
                Provincia = Provincia.CAPITAL_FEDERAL,
                PersonaId = 1
            });

            direcciones.Add(new Direccion()
            {
                Calle = "Bartolomé Mitre",
                Numero = 2553,
                CodigoPostal = "C1039AAO",
                Localidad = "Capital Federal",
                Provincia = Provincia.CAPITAL_FEDERAL,
                PersonaId = 2
            });

            direcciones.Add(new Direccion()
            {
                Calle = "Av Meeks",
                Numero = 155,
                CodigoPostal = "B1832AHQ",
                Localidad = "Lomas de Zamora",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 3
            });

            direcciones.Add(new Direccion()
            {
                Calle = "Av. Roca",
                Numero = 1080,
                CodigoPostal = "B1870AOK",
                Localidad = "Avellaneda",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 4
            });

            direcciones.Add(new Direccion()
            {
                Calle = "Paysandú",
                Numero = 5724,
                CodigoPostal = "B1743APT",
                Localidad = "Moreno",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 5
            });

            direcciones.Add(new Direccion()
            {
                Calle = "Ipiranga",
                Numero = 779,
                CodigoPostal = "B1609BQO",
                Localidad = "San Isidro",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 6
            });

            direcciones.Add(new Direccion()
            {
                Calle = "Nicolás Avellaneda",
                Numero = 3335,
                CodigoPostal = "B1636ASY",
                Localidad = "Vicente Lopez",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 7
            });

            direcciones.Add(new Direccion()
            {
                Calle = "Dr. Emilio Ravignani",
                Numero = 2049,
                CodigoPostal = "C1414CPU",
                Localidad = "Capital Federal",
                Provincia = Provincia.CAPITAL_FEDERAL,
                PersonaId = 8
            });

            direcciones.Add(new Direccion()
            {
                Calle = "Raúl Scalabrini Ortiz",
                Numero = 410,
                CodigoPostal = "C1414DNR",
                Localidad = "Capital Federal",
                Provincia = Provincia.CAPITAL_FEDERAL,
                PersonaId = 9
            });

            direcciones.Add(new Direccion()
            {
                Calle = "Av. Díaz Vélez",
                Numero = 3830,
                CodigoPostal = "C1200AAS",
                Localidad = "Capital Federal",
                Provincia = Provincia.CAPITAL_FEDERAL,
                PersonaId = 10
            });

            direcciones.Add(new Direccion()
            {
                Calle = "Venezuela",
                Numero = 1260,
                CodigoPostal = "C1095AAZ",
                Localidad = "Capital Federal",
                Provincia = Provincia.CAPITAL_FEDERAL,
                PersonaId = 11
            });

            direcciones.Add(new Direccion()
            {
                Calle = "Enrique Fernández",
                Numero = 2119,
                CodigoPostal = "B1824FCT",
                Localidad = "Lanús",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 12
            });

            direcciones.Add(new Direccion()
            {
                Calle = "Marcos Grigera",
                Numero = 532,
                CodigoPostal = "B1828HRK",
                Localidad = "Lomas de Zamora",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 13
            });

            direcciones.Add(new Direccion()
            {
                Calle = "Cnel. Brandsen",
                Numero = 3263,
                CodigoPostal = "B1872FXY",
                Localidad = "Avellaneda",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 14
            });

            direcciones.Add(new Direccion()
            {
                Calle = "Unamuno",
                Numero = 1516,
                CodigoPostal = "B1879FFN",
                Localidad = "Quilmes",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 15
            });

            _context.Direcciones.AddRange(direcciones);

            #endregion

            #region Telefonos

            List<Telefono> telefonos = new List<Telefono>();

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 43824118,
                Tipo = TipoTelefono.PARTICULAR,
                PersonaId = 1
            });

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 49547070,
                Tipo = TipoTelefono.PARTICULAR,
                PersonaId = 2
            });

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 42928310,
                Tipo = TipoTelefono.PARTICULAR,
                PersonaId = 3
            });

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 49432225,
                Tipo = TipoTelefono.LABORAL,
                PersonaId = 4
            });

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 47580113,
                Tipo = TipoTelefono.CELULAR,
                PersonaId = 5
            });

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 48568526,
                Tipo = TipoTelefono.CELULAR,
                PersonaId = 6
            });

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 45491325,
                Tipo = TipoTelefono.CELULAR,
                PersonaId = 7
            });

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 43321083,
                Tipo = TipoTelefono.LABORAL,
                PersonaId = 8
            });

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 46729947,
                Tipo = TipoTelefono.PARTICULAR,
                PersonaId = 9
            });

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 41481125,
                Tipo = TipoTelefono.PARTICULAR,
                PersonaId = 10
            });

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 43735991,
                Tipo = TipoTelefono.CELULAR,
                PersonaId = 11
            });

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 44466121,
                Tipo = TipoTelefono.PARTICULAR,
                PersonaId = 12
            });

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 43657600,
                Tipo = TipoTelefono.LABORAL,
                PersonaId = 13
            });

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 48728281,
                Tipo = TipoTelefono.CELULAR,
                PersonaId = 14
            });

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 45832427,
                Tipo = TipoTelefono.PARTICULAR,
                PersonaId = 15
            });

            _context.Telefonos.AddRange(telefonos);

            #endregion

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}