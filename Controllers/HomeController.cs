using Historias_Clinicas_D.Data;
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
        private readonly HistoriasClinicasContext _context;

        public HomeController(ILogger<HomeController> logger, HistoriasClinicasContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Nosotros()
        {
            return View();
        }

 /*       public IActionResult CargarBD()
        {
            #region Pacientes

            Paciente pacienteUno = new Paciente()
            {
                ObraSocial = ObraSocial.GALENO,
                Nombre = "Fernando",
                Apellido = "Lopez",
                DNI = "37043674",
                Email = "fernando_lopez@gmail.com",
                UserName = "flopez"
            };

            _context.Add(pacienteUno);

            Paciente pacienteDos = new Paciente()
            {
                ObraSocial = ObraSocial.SWISS_MEDICAL,
                Nombre = "Antonio Luis",
                Apellido = "Tello",
                DNI = "32919178",
                Email = "antonio.tello@yahoo.com",
                UserName = "atello"
            };

            _context.Add(pacienteDos);

            Paciente pacienteTres = new Paciente()
            {
                ObraSocial = ObraSocial.MEDICUS,
                Nombre = "Luis Enrique",
                Apellido = "Mena",
                DNI = "3757545",
                Email = "luismena@outlook.com",
                UserName = "lmena"
            };

            _context.Add(pacienteTres);

            Paciente pacienteCuatro = new Paciente()
            {
                ObraSocial = ObraSocial.OSDE,
                Nombre = "Leopoldo",
                Apellido = "Mariscal",
                DNI = "6593902",
                Email = "mariscal._leopoldo@gmail.com",
                UserName = "lmariscal"
            };

            _context.Add(pacienteCuatro);

            Paciente pacienteCinco = new Paciente()
            {
                ObraSocial = ObraSocial.OSDE,
                Nombre = "Guillermo",
                Apellido = "Alcala",
                DNI = "17473304",
                Email = "guillermo_alcala@hotmail.com",
                UserName = "galcala"
            };

            _context.Add(pacienteCinco);

            #endregion

            #region Empleados

            Empleado empleadoUno = new Empleado()
            {
                Nombre = "Alejandro",
                Apellido = "Ponce",
                DNI = "39647489",
                Email = "aleponce@outlook.com",
                UserName = "aponce"
            };

            _context.Add(empleadoUno);

            Empleado empleadoDos = new Empleado()
            {
                Nombre = "Miriam",
                Apellido = "Escobar",
                DNI = "18600338",
                Email = "m.escobar@gmail.com",
                UserName = "mescobar"
            };

            _context.Add(empleadoDos);

            Empleado empleadoTres = new Empleado()
            {
                Nombre = "Carmen",
                Apellido = "Barrios",
                DNI = "17261727",
                Email = "carmen-barrios@yahoo.com",
                UserName = "cbarrios"
            };

            _context.Add(empleadoTres);

            Empleado empleadoCuatro = new Empleado()
            {
                Nombre = "Federico",
                Apellido = "Martínez",
                DNI = "23917753",
                Email = "fernando__martinez@live.com",
                UserName = "fmartinez"
            };

            _context.Add(empleadoCuatro);

            Empleado empleadoCinco = new Empleado()
            {
                Nombre = "Gerardo",
                Apellido = "Guzmán",
                DNI = "5960550",
                Email = "gerardoguzman@outlook.com",
                UserName = "gguzman"
            };

            _context.Add(empleadoCinco);

            #endregion

            #region Medicos

            Medico medicoUno = new Medico()
            {
                TipoMatricula = TipoMatricula.NACIONAL,
                Matricula = "368412",
                Especialidad = Especialidad.PEDIATRIA,
                Nombre = "Nicolas",
                Apellido = "Rivera",
                DNI = "41777762",
                Email = "nicolas.rivera@gmail.com",
                UserName = "nrivera"
            };

            _context.Add(medicoUno);

            Medico medicoDos = new Medico()
            {
                TipoMatricula = TipoMatricula.NACIONAL,
                Matricula = "368412",
                Especialidad = Especialidad.NEUMOLOGIA,
                Nombre = "Oscar",
                Apellido = "Herrero",
                DNI = "28321201",
                Email = "herrero_oscar@hotmail.com",
                UserName = "oherrero"
            };

            _context.Add(medicoDos);

            Medico medicoTres = new Medico()
            {
                TipoMatricula = TipoMatricula.PROVINCIAL,
                Matricula = "368412",
                Especialidad = Especialidad.INFECTOLOGIA,
                Nombre = "Bianca",
                Apellido = "Perez",
                DNI = "9154238",
                Email = "bianca._perez@outlook.com",
                UserName = "bperez"
            };

            _context.Add(medicoTres);

            Medico medicoCuatro = new Medico()
            {
                TipoMatricula = TipoMatricula.PROVINCIAL,
                Matricula = "368412",
                Especialidad = Especialidad.ENDOCRINOLOGIA,
                Nombre = "Marina",
                Apellido = "Cerdan",
                DNI = "6731568",
                Email = "marinacerdan@yahoo.com",
                UserName = "mcerdan"
            };

            _context.Add(medicoCuatro);

            Medico medicoCinco = new Medico()
            {
                TipoMatricula = TipoMatricula.NACIONAL,
                Matricula = "368412",
                Especialidad = Especialidad.NEUROLOGIA,
                Nombre = "Judit",
                Apellido = "Romero",
                DNI = "5975505",
                Email = "judit-romero@gmail.com",
                UserName = "jromero"
            };

            _context.Add(medicoCinco);

            #endregion

            #region Direcciones

            Direccion direccionUno = new Direccion()
            {
                Calle = "Montevideo",
                Numero = 248,
                CodigoPostal = "C1037ADA",
                Localidad = "Capital Federal",
                Provincia = Provincia.CAPITAL_FEDERAL,
                PersonaId = 1
            };

            _context.Add(direccionUno);

            Direccion direccionDos = new Direccion()
            {
                Calle = "Bartolomé Mitre",
                Numero = 2553,
                CodigoPostal = "C1039AAO",
                Localidad = "Capital Federal",
                Provincia = Provincia.CAPITAL_FEDERAL,
                PersonaId = 2
            };

            _context.Add(direccionDos);

            Direccion direccionTres = new Direccion()
            {
                Calle = "Av Meeks",
                Numero = 155,
                CodigoPostal = "B1832AHQ",
                Localidad = "Lomas de Zamora",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 3
            };

            _context.Add(direccionTres);

            Direccion direccionCuatro = new Direccion()
            {
                Calle = "Av. Roca",
                Numero = 1080,
                CodigoPostal = "B1870AOK",
                Localidad = "Avellaneda",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 4
            };

            _context.Add(direccionCuatro);

            Direccion direccionCinco = new Direccion()
            {
                Calle = "Paysandú",
                Numero = 5724,
                CodigoPostal = "B1743APT",
                Localidad = "Moreno",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 5
            };

            _context.Add(direccionCinco);

            Direccion direccionSeis = new Direccion()
            {
                Calle = "Ipiranga",
                Numero = 779,
                CodigoPostal = "B1609BQO",
                Localidad = "San Isidro",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 6
            };

            _context.Add(direccionSeis);

            Direccion direccionSiete = new Direccion()
            {
                Calle = "Nicolás Avellaneda",
                Numero = 3335,
                CodigoPostal = "B1636ASY",
                Localidad = "Vicente Lopez",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 7
            };

            _context.Add(direccionSiete);

            Direccion direccionOcho = new Direccion()
            {
                Calle = "Dr. Emilio Ravignani",
                Numero = 2049,
                CodigoPostal = "C1414CPU",
                Localidad = "Capital Federal",
                Provincia = Provincia.CAPITAL_FEDERAL,
                PersonaId = 8
            };

            _context.Add(direccionOcho);

            Direccion direccionNueve = new Direccion()
            {
                Calle = "Raúl Scalabrini Ortiz",
                Numero = 410,
                CodigoPostal = "C1414DNR",
                Localidad = "Capital Federal",
                Provincia = Provincia.CAPITAL_FEDERAL,
                PersonaId = 9
            };

            _context.Add(direccionNueve);

            Direccion direccionDiez = new Direccion()
            {
                Calle = "Av. Díaz Vélez",
                Numero = 3830,
                CodigoPostal = "C1200AAS",
                Localidad = "Capital Federal",
                Provincia = Provincia.CAPITAL_FEDERAL,
                PersonaId = 10
            };

            _context.Add(direccionDiez);

            Direccion direccionOnce = new Direccion()
            {
                Calle = "Venezuela",
                Numero = 1260,
                CodigoPostal = "C1095AAZ",
                Localidad = "Capital Federal",
                Provincia = Provincia.CAPITAL_FEDERAL,
                PersonaId = 11
            };

            _context.Add(direccionOnce);

            Direccion direccionDoce = new Direccion()
            {
                Calle = "Enrique Fernández",
                Numero = 2119,
                CodigoPostal = "B1824FCT",
                Localidad = "Lanús",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 12
            };

            _context.Add(direccionDoce);

            Direccion direccionTrece = new Direccion()
            {
                Calle = "Marcos Grigera",
                Numero = 532,
                CodigoPostal = "B1828HRK",
                Localidad = "Lomas de Zamora",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 13
            };

            _context.Add(direccionTrece);

            Direccion direccionCatorce = new Direccion()
            {
                Calle = "Cnel. Brandsen",
                Numero = 3263,
                CodigoPostal = "B1872FXY",
                Localidad = "Avellaneda",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 14
            };

            _context.Add(direccionCatorce);

            Direccion direccionQuince = new Direccion()
            {
                Calle = "Unamuno",
                Numero = 1516,
                CodigoPostal = "B1879FFN",
                Localidad = "Quilmes",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 15
            };

            _context.Add(direccionQuince);

            #endregion

            #region Telefonos

            Telefono telefonoUno = new Telefono()
            {
                Caracteristica = 11,
                Numero = 43824118,
                Tipo = TipoTelefono.PARTICULAR,
                PersonaId = 1
            };

            _context.Add(telefonoUno);

            Telefono telefonoDos = new Telefono()
            {
                Caracteristica = 11,
                Numero = 49547070,
                Tipo = TipoTelefono.PARTICULAR,
                PersonaId = 2
            };

            _context.Add(telefonoDos);

            Telefono telefonoTres = new Telefono()
            {
                Caracteristica = 11,
                Numero = 42928310,
                Tipo = TipoTelefono.PARTICULAR,
                PersonaId = 3
            };

            _context.Add(telefonoTres);

            Telefono telefonoCuatro = new Telefono()
            {
                Caracteristica = 11,
                Numero = 49432225,
                Tipo = TipoTelefono.LABORAL,
                PersonaId = 4
            };

            _context.Add(telefonoCuatro);

            Telefono telefonoCinco = new Telefono()
            {
                Caracteristica = 11,
                Numero = 47580113,
                Tipo = TipoTelefono.CELULAR,
                PersonaId = 5
            };

            _context.Add(telefonoCinco);

            Telefono telefonoSeis = new Telefono()
            {
                Caracteristica = 11,
                Numero = 48568526,
                Tipo = TipoTelefono.CELULAR,
                PersonaId = 6
            };

            _context.Add(telefonoSeis);

            Telefono telefonoSiete = new Telefono()
            {
                Caracteristica = 11,
                Numero = 45491325,
                Tipo = TipoTelefono.CELULAR,
                PersonaId = 7
            };

            _context.Add(telefonoSiete);

            Telefono telefonoOcho = new Telefono()
            {
                Caracteristica = 11,
                Numero = 43321083,
                Tipo = TipoTelefono.LABORAL,
                PersonaId = 8
            };

            _context.Add(telefonoOcho);

            Telefono telefonoNueve = new Telefono()
            {
                Caracteristica = 11,
                Numero = 46729947,
                Tipo = TipoTelefono.PARTICULAR,
                PersonaId = 9
            };

            _context.Add(telefonoNueve);

            Telefono telefonoDiez = new Telefono()
            {
                Caracteristica = 11,
                Numero = 41481125,
                Tipo = TipoTelefono.PARTICULAR,
                PersonaId = 10
            };

            _context.Add(telefonoDiez);

            Telefono telefonoOnce = new Telefono()
            {
                Caracteristica = 11,
                Numero = 43735991,
                Tipo = TipoTelefono.CELULAR,
                PersonaId = 11
            };

            _context.Add(telefonoOnce);

            Telefono telefonoDoce = new Telefono()
            {
                Caracteristica = 11,
                Numero = 44466121,
                Tipo = TipoTelefono.PARTICULAR,
                PersonaId = 12
            };

            _context.Add(telefonoDoce);

            Telefono telefonoTrece = new Telefono()
            {
                Caracteristica = 11,
                Numero = 43657600,
                Tipo = TipoTelefono.LABORAL,
                PersonaId = 13
            };

            _context.Add(telefonoTrece);

            Telefono telefonoCatorce = new Telefono()
            {
                Caracteristica = 11,
                Numero = 48728281,
                Tipo = TipoTelefono.CELULAR,
                PersonaId = 14
            };

            _context.Add(telefonoCatorce);

            Telefono telefonoQuince = new Telefono()
            {
                Caracteristica = 11,
                Numero = 45832427,
                Tipo = TipoTelefono.PARTICULAR,
                PersonaId = 15
            };

            _context.Add(telefonoQuince);

            #endregion

            _context.SaveChanges();

            return RedirectToAction("Index");
        }*/

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}