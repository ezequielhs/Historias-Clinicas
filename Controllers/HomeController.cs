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
                Constantes.RolAdmin,
                Constantes.RolPaciente,
                Constantes.RolProfesional,
                Constantes.RolAdministrativo
            };

            foreach (string rol in roles)
            {
                await _rolManager.CreateAsync(new IdentityRole<int>(rol));
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
                UserName = Constantes.PacienteUserName
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
                var resultCreateUser = await _userManager.CreateAsync(paciente, Constantes.DefaultPassword);

                if (resultCreateUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(paciente, Constantes.RolPaciente);
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
                Email = "aponce@sannicolas.com.ar",
                UserName = Constantes.AdministrativoUserName
            });

            empleados.Add(new Empleado()
            {
                Nombre = "Miriam",
                Apellido = "Escobar",
                DNI = "18600338",
                Email = "mescobar@sannicolas.com.ar",
                UserName = "mescobar@sannicolas.com.ar"
            });

            empleados.Add(new Empleado()
            {
                Nombre = "Carmen",
                Apellido = "Barrios",
                DNI = "17261727",
                Email = "cbarrios@sannicolas.com.ar",
                UserName = "cbarrios@sannicolas.com.ar"
            });

            empleados.Add(new Empleado()
            {
                Nombre = "Federico",
                Apellido = "Martínez",
                DNI = "23917753",
                Email = "fmartinez@sannicolas.com.ar",
                UserName = "fmartinez@sannicolas.com.ar"
            });

            empleados.Add(new Empleado()
            {
                Nombre = "Gerardo",
                Apellido = "Guzmán",
                DNI = "5960550",
                Email = "gguzman@sannicolas.com.ar",
                UserName = "gguzman@sannicolas.com.ar"
            });

            foreach (Empleado empleado in empleados)
            {
                var resultCreateUser = await _userManager.CreateAsync(empleado, Constantes.DefaultPassword);

                if (resultCreateUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(empleado, Constantes.RolAdministrativo);
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
                Email = "nrivera@sannicolas.com.ar",
                UserName = Constantes.ProfesionalUserName
            });

            medicos.Add(new Medico()
            {
                TipoMatricula = TipoMatricula.NACIONAL,
                Matricula = "856923",
                Especialidad = Especialidad.NEUMOLOGIA,
                Nombre = "Oscar",
                Apellido = "Herrero",
                DNI = "28321201",
                Email = "oherreo@sannicolas.com.ar",
                UserName = "oherreo@sannicolas.com.ar"
            });

            medicos.Add(new Medico()
            {
                TipoMatricula = TipoMatricula.PROVINCIAL,
                Matricula = "741296",
                Especialidad = Especialidad.INFECTOLOGIA,
                Nombre = "Bianca",
                Apellido = "Perez",
                DNI = "9154238",
                Email = "bperez@sannicolas.com.ar",
                UserName = "bperez@sannicolas.com.ar"
            });

            medicos.Add(new Medico()
            {
                TipoMatricula = TipoMatricula.PROVINCIAL,
                Matricula = "258463",
                Especialidad = Especialidad.ENDOCRINOLOGIA,
                Nombre = "Marina",
                Apellido = "Cerdan",
                DNI = "6731568",
                Email = "mcerdan@sannicolas.com.ar",
                UserName = "mcerdan@sannicolas.com.ar"
            });

            medicos.Add(new Medico()
            {
                TipoMatricula = TipoMatricula.NACIONAL,
                Matricula = "200385",
                Especialidad = Especialidad.TRAUMATOLOGIA,
                Nombre = "Judit",
                Apellido = "Romero",
                DNI = "5975505",
                Email = "jromero@sannicolas.com.ar",
                UserName = "jromero@sannicolas.com.ar"
            });

            medicos.Add(new Medico()
            {
                TipoMatricula = TipoMatricula.NACIONAL,
                Matricula = "663877",
                Especialidad = Especialidad.MEDICINA_GENERAL,
                Nombre = "Martin",
                Apellido = "Gomez",
                DNI = "35620120",
                Email = "mgomez@sannicolas.com.ar",
                UserName = "mgomez@sannicolas.com.ar"
            });

            medicos.Add(new Medico()
            {
                TipoMatricula = TipoMatricula.NACIONAL,
                Matricula = "423695",
                Especialidad = Especialidad.NEFROLOGIA,
                Nombre = "Claudio",
                Apellido = "Fernandez",
                DNI = "34552631",
                Email = "cfernandez@sannicolas.com.ar",
                UserName = "cfernandez@sannicolas.com.ar"
            });

            medicos.Add(new Medico()
            {
                TipoMatricula = TipoMatricula.PROVINCIAL,
                Matricula = "362204",
                Especialidad = Especialidad.KINESIOLOGIA,
                Nombre = "Marta",
                Apellido = "Lirio",
                DNI = "18966210",
                Email = "mlirio@sannicolas.com.ar",
                UserName = "mlirio@sannicolas.com.ar"
            });

            foreach (Medico medico in medicos)
            {
                var resultCreateUser = await _userManager.CreateAsync(medico, Constantes.DefaultPassword);
                if (resultCreateUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(medico, Constantes.RolProfesional);
                }
            }

            #endregion

            #region Administrador

            Empleado admin = new Empleado()
            {
                Nombre = "Ezequiel",
                Apellido = "Hoyos",
                DNI = "39267310",
                Email = "ehoyos@sannicolas.com.ar",
                UserName = Constantes.AdminUserName
            };
            if (_context.Personas.FirstOrDefault(persona => persona.UserName == admin.UserName) == null)
            {
                var resultCreateUser = await _userManager.CreateAsync(admin, Constantes.DefaultPassword);

                if (resultCreateUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, Constantes.RolAdmin);
                }
            }

            #endregion

            #region Episodios

            List<Episodio> episodios = new List<Episodio>();

            episodios.Add(new Episodio
            {
                Id = 1,
                Motivo = "Traumatismo en Pierna izquierda.",
                Descripcion = "El paciente se encontraba andando en Skate y sufrió un accidente.",
                PacienteId = 1,
                EmpleadoRegistraId = 6
            });

            episodios.Add(new Episodio
            {
                Id = 2,
                Motivo = "Dolor agudo en la parte inferior derecha de la Espalda.",
                Descripcion = "El paciente se encontraba en su casa cuando comenzo a experimentar un fuerte dolor su Espalda.",
                PacienteId = 2,
                EmpleadoRegistraId = 7
            });

            episodios.Add(new Episodio
            {
                Id = 3,
                Motivo = "Herida de bala en el Hombro derecho.",
                Descripcion = "El Oficial realizaba un operativo cuando fue atacado y recibio un disparo en el Hombro. No sufrio una gran perdida de sangre.",
                PacienteId = 3,
                EmpleadoRegistraId = 8
            });

            episodios.Add(new Episodio
            {
                Id = 4,
                Motivo = "Mareos y fuertes migrañias.",
                Descripcion = "El paciente informa que experimenta migrañias constantes hace cuatro días, ademas de leves mareos.",
                PacienteId = 4,
                EmpleadoRegistraId = 9
            });

            episodios.Add(new Episodio
            {
                Id = 5,
                Motivo = "Cuadro febril y dolores muscular en el Cuello.",
                Descripcion = "El paciente declara que desde la madrugada del dia de la fecha posee fiebre y un dolor muscular sobre Cuello.",
                PacienteId = 5,
                EmpleadoRegistraId = 10
            });

            _context.Episodios.AddRange(episodios);

            #endregion

            #region Evoluciones

            List<Evolucion> evoluciones = new List<Evolucion>();

            evoluciones.Add(new Evolucion
            {
                Id = 1,
                MedicoId = 15,
                EpisodioId = 1,
                DescripcionAtencion = "Se examino realizo un control en la Pierna izquierda del paciente y se encuentra en estado optimo."
            });

            evoluciones.Add(new Evolucion
            {
                Id = 2,
                MedicoId = 17,
                EpisodioId = 2,
                DescripcionAtencion = "Debido a la medicacion aplicada al paciente, este indica que el dolor a disminuido. Debe someterse a cirugia."
            });

            evoluciones.Add(new Evolucion
            {
                Id = 3,
                MedicoId = 15,
                EpisodioId = 3,
                DescripcionAtencion = "El paciente se encuentra mejorando favorablemente, sigue bajo control."
            });

            evoluciones.Add(new Evolucion
            {
                Id = 4,
                MedicoId = 18,
                EpisodioId = 4,
                DescripcionAtencion = "Se realizo una terapia al paciente, la misma consistio en ejercicios y masajes terapeuticos."
            });

            evoluciones.Add(new Evolucion
            {
                Id = 5,
                MedicoId = 13,
                EpisodioId = 5,
                DescripcionAtencion = "Se receto la medicina correspondiente al paciente."
            });

            _context.Evoluciones.AddRange(evoluciones);

            #endregion

            #region Notas

            List<Nota> notas = new List<Nota>();

            notas.Add(new Nota
            {
                Id = 1,
                EvolucionId = 1,
                EmpleadoId = 6,
                Mensaje = "No se deben mojar los vendajes hasta en tanto el medico encargado no los quite."
            });

            notas.Add(new Nota
            {
                Id = 2,
                EvolucionId = 2,
                EmpleadoId = 7,
                Mensaje = "Los resultados de la ecografia muestran una cobertura liquida sobre el Riñon derecho, debe someterse a cirugia de extraccion de riñon."
            });

            notas.Add(new Nota
            {
                Id = 3,
                EvolucionId = 3,
                EmpleadoId = 8,
                Mensaje = "Se extrajo de forma exitosa el proyectil en el Hombro derecho, luego del alta el paciente debe solicitar turno con Traumatologia."
            });

            notas.Add(new Nota
            {
                Id = 4,
                EvolucionId = 4,
                EmpleadoId = 9,
                Mensaje = "El paciente debe presentarse al Centro Medico San Nicolas el dia Viernes 22 de Julio a las 19:00 hs."
            });

            notas.Add(new Nota
            {
                Id = 5,
                EvolucionId = 5,
                EmpleadoId = 10,
                Mensaje = "Debe tomar un comprimido de 'QURA PLUS 500 mg' durante la mañana y la noche ademas de 'Te Vick Vitapyrena Forte'."
            });

            _context.Notas.AddRange(notas);

            #endregion

            #region Epicrisis

            List<Epicrisis> epicrisis = new List<Epicrisis>();

            epicrisis.Add(new Epicrisis
            {
                Id = 1,
                Diagnostico = "El paciente posee un esguince leve en su Pierna izquierda producto de la caida que sufrio mientras patinaba en Skate.",
                Recomendacion = "Se aplicaron los vendajes correspondientes, se recomienda al paciente reposo por 72 horas. Debe solicitar un turno con Traumatologia para el control de la lesion.",
                MedicoId = 15,
                EpisodioId = 1
            });

            epicrisis.Add(new Epicrisis
            {
                Id = 2,
                Diagnostico = "Se examino la Espalda del paciente mediante radiografia, el resulto mostro que su Riñon derecho se encuentra en un tamaño anormal.",
                Recomendacion = "Se solicita con caracter urgente una ecografia para examinar los Riñones del paciente.",
                MedicoId = 16,
                EpisodioId = 2
            });

            epicrisis.Add(new Epicrisis
            {
                Id = 3,
                Diagnostico = "Se deriva de forma urgente al paciente a la sala de cirugias.",
                Recomendacion = "El paciente debe quedar internado por dos dias para control.",
                MedicoId = 15,
                EpisodioId = 3
            });

            epicrisis.Add(new Epicrisis
            {
                Id = 4,
                Diagnostico = "Se examino al paciente pero no se encontro ningun traumatismo, se concluye que se encuentra bajo gran estres.",
                Recomendacion = "Se indica al paciente que solicite turno con Kinesiología.",
                MedicoId = 16,
                EpisodioId = 4
            });

            epicrisis.Add(new Epicrisis
            {
                Id = 5,
                Diagnostico = "El paciente experimenta los sintomas tipicos de la gripe.",
                Recomendacion = "Se inidica al paciente la medicina correspondiente que debe solicitar en la farmacia, ver nota.",
                MedicoId = 13,
                EpisodioId = 5
            });

            _context.Epicrisis.AddRange(epicrisis);

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

            direcciones.Add(new Direccion()
            {
                Calle = "José A. Cortejarena",
                Numero = 1874,
                CodigoPostal = "C1281AAB",
                Localidad = "Capital Federal",
                Provincia = Provincia.CAPITAL_FEDERAL,
                PersonaId = 16
            });

            direcciones.Add(new Direccion()
            {
                Calle = "Salguero",
                Numero = 553,
                CodigoPostal = "B1751CDN",
                Localidad = "La Matanza",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 17
            });

            direcciones.Add(new Direccion()
            {
                Calle = "Lisandro de la Torre",
                Numero = 329,
                CodigoPostal = "B1638",
                Localidad = "Vicente López",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 18
            });

            direcciones.Add(new Direccion()
            {
                Calle = "Santiago del Estero",
                Numero = 782,
                CodigoPostal = "B1823CLJ",
                Localidad = "Lanus",
                Provincia = Provincia.BUENOS_AIRES,
                PersonaId = 19
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

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 46691020,
                Tipo = TipoTelefono.PARTICULAR,
                PersonaId = 16
            });

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 62104453,
                Tipo = TipoTelefono.PARTICULAR,
                PersonaId = 17
            });

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 42169988,
                Tipo = TipoTelefono.CELULAR,
                PersonaId = 18
            });

            telefonos.Add(new Telefono()
            {
                Caracteristica = 11,
                Numero = 50256001,
                Tipo = TipoTelefono.CELULAR,
                PersonaId = 19
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