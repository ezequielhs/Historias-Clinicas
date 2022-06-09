using Historias_Clinicas_D.Helpers;
using Historias_Clinicas_D.Models;
using Historias_Clinicas_D.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Historias_Clinicas_D.Data
{
    public class DbInitializer
    {
        private readonly HistoriasClinicasContext _context;
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<IdentityRole<int>> _rolManager;

        public DbInitializer(HistoriasClinicasContext context, UserManager<Persona> userManager, RoleManager<IdentityRole<int>> rolManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._rolManager = rolManager;
        }

        public void Seed()
        {
            GenerarRoles().Wait();
            GenerarPacientes().Wait();
            GenerarEmpleados().Wait();
            GenerarMedicos().Wait();
            GenerarEpisodios();
            GenerarEvoluciones();
            GenerarNotas();
            GenerarEpicrisis();
            GenerarDirecciones();
            GenerarTelefonos();
        }

        private async Task GenerarRoles()
        {
            if (_context.Roles.Count() == 0)
            {
                List<string> roles = new List<string>()
                {
                    Constantes.RolPaciente,
                    Constantes.RolMedico,
                    Constantes.RolEmpleado
                };

                foreach (string rol in roles)
                {
                    await _rolManager.CreateAsync(new IdentityRole<int>(rol));
                }
            }
        }

        private async Task GenerarPacientes()
        {
            if (_context.Pacientes.Count() == 0)
            {
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
            }
        }

        private async Task GenerarEmpleados()
        {
            if (_context.Empleados.Count() == 0)
            {
                List<Empleado> empleados = new List<Empleado>();

                empleados.Add(new Empleado()
                {
                    Nombre = "Alejandro",
                    Apellido = "Ponce",
                    DNI = "39647489",
                    Email = "aponce@sannicolas.com.ar",
                    UserName = Constantes.EmpleadoUserName
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
                        await _userManager.AddToRoleAsync(empleado, Constantes.RolEmpleado);
                    }
                }
            }
        }

        private async Task GenerarMedicos()
        {
            if(_context.Medicos.Count() == 0)
            {
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
                    UserName = Constantes.MedicoUserName
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
                        await _userManager.AddToRoleAsync(medico, Constantes.RolMedico);
                    }
                }
            } 
        }

        private void GenerarEpisodios()
        {
            if (_context.Episodios.Count() == 0)
            {
                List<Episodio> episodios = new List<Episodio>();
                List<Paciente> pacientes = _context.Pacientes.ToList();
                List<Empleado> empleados = _context.Empleados.ToList();

                episodios.Add(new Episodio
                {
                    Motivo = "Traumatismo en Pierna izquierda.",
                    Descripcion = "El paciente se encontraba andando en Skate y sufrió un accidente.",
                    PacienteId = pacientes[0].Id,
                    EmpleadoRegistraId = empleados[0].Id,
                    FechaYHoraAlta = Constantes.FechaActual,
                    FechaYHoraCierre = Constantes.FechaActual,
                    EstadoAbierto = Constantes.EstadoCerrado
                });

                episodios.Add(new Episodio
                {
                    Motivo = "Mareos y fuertes migrañias.",
                    Descripcion = "El paciente informa que experimenta migrañias constantes hace cuatro días, ademas de leves mareos.",
                    PacienteId = pacientes[1].Id,
                    EmpleadoRegistraId = empleados[1].Id,
                    FechaYHoraAlta = Constantes.FechaActual,
                    FechaYHoraCierre = Constantes.FechaActual,
                    EstadoAbierto = Constantes.EstadoCerrado
                });

                episodios.Add(new Episodio
                {
                    Motivo = "Herida de bala en el Hombro derecho.",
                    Descripcion = "El Oficial realizaba un operativo cuando fue atacado y recibio un disparo en el Hombro. No sufrio una gran perdida de sangre.",
                    PacienteId = pacientes[2].Id,
                    EmpleadoRegistraId = empleados[2].Id
                });

                episodios.Add(new Episodio
                {
                    Motivo = "Dolor agudo en la parte inferior derecha de la Espalda.",
                    Descripcion = "El paciente se encontraba en su casa cuando comenzo a experimentar un fuerte dolor su Espalda.",
                    PacienteId = pacientes[3].Id,
                    EmpleadoRegistraId = empleados[3].Id
                });

                episodios.Add(new Episodio
                {
                    Motivo = "Cuadro febril y dolores muscular en el Cuello.",
                    Descripcion = "El paciente declara que desde la madrugada del dia de la fecha posee fiebre y un dolor muscular sobre Cuello.",
                    PacienteId = pacientes[4].Id,
                    EmpleadoRegistraId = empleados[4].Id
                });

                _context.Episodios.AddRange(episodios);
                _context.SaveChanges();
            } 
        }

        private void GenerarEvoluciones()
        {
            if (_context.Evoluciones.Count() == 0)
            {
                List<Evolucion> evoluciones = new List<Evolucion>();
                List<Medico> medicos = _context.Medicos.ToList();
                List<Episodio> episodios = _context.Episodios.ToList();

                evoluciones.Add(new Evolucion
                {
                    MedicoId = medicos[4].Id,
                    EpisodioId = episodios[0].Id,
                    DescripcionAtencion = "Se examino realizo un control en la Pierna izquierda del paciente y se encuentra en estado optimo.",
                    FechaYHoraAlta = Constantes.FechaActual,
                    FechaYHoraCierre = Constantes.FechaActual,
                    EstadoAbierto = Constantes.EstadoCerrado
                });

                evoluciones.Add(new Evolucion
                {
                    MedicoId = medicos[7].Id,
                    EpisodioId = episodios[1].Id,
                    DescripcionAtencion = "Se realizo una terapia al paciente, la misma consistio en ejercicios y masajes terapeuticos.",
                    FechaYHoraAlta = Constantes.FechaActual,
                    FechaYHoraCierre = Constantes.FechaActual,
                    EstadoAbierto = Constantes.EstadoCerrado
                });

                evoluciones.Add(new Evolucion
                {
                    MedicoId = medicos[4].Id,
                    EpisodioId = episodios[2].Id,
                    DescripcionAtencion = "El paciente se encuentra mejorando favorablemente, sigue bajo control."
                });

                evoluciones.Add(new Evolucion
                {
                    MedicoId = medicos[6].Id,
                    EpisodioId = episodios[3].Id,
                    DescripcionAtencion = "Debido a la medicacion aplicada al paciente, este indica que el dolor a disminuido. Debe someterse a cirugia."
                });

                _context.Evoluciones.AddRange(evoluciones);
                _context.SaveChanges();
            }
        }

        private void GenerarNotas()
        {
            if (_context.Notas.Count() == 0)
            {
                List<Nota> notas = new List<Nota>();
                List<Evolucion> evoluciones = _context.Evoluciones.ToList();
                List<Empleado> empleados = _context.Empleados.ToList();

                notas.Add(new Nota
                {
                    EvolucionId = evoluciones[0].Id,
                    EmpleadoId = empleados[0].Id,
                    Mensaje = "No se deben mojar los vendajes hasta en tanto el medico encargado no los quite."
                });

                notas.Add(new Nota
                {
                    EvolucionId = evoluciones[1].Id,
                    EmpleadoId = empleados[1].Id,
                    Mensaje = "Los resultados de la ecografia muestran una cobertura liquida sobre el Riñon derecho, debe someterse a cirugia de extraccion de riñon."
                });

                notas.Add(new Nota
                {
                    EvolucionId = evoluciones[2].Id,
                    EmpleadoId = empleados[2].Id,
                    Mensaje = "Se extrajo de forma exitosa el proyectil en el Hombro derecho, luego del alta el paciente debe solicitar turno con Traumatologia."
                });

                notas.Add(new Nota
                {
                    EvolucionId = evoluciones[3].Id,
                    EmpleadoId = empleados[3].Id,
                    Mensaje = "El paciente debe presentarse al Centro Medico San Nicolas el dia Viernes 22 de Julio a las 19:00 hs."
                });

                _context.Notas.AddRange(notas);
                _context.SaveChanges();
            }
        }

        private void GenerarEpicrisis()
        {
            if (_context.Epicrisis.Count() == 0)
            {
                List<Epicrisis> epicrisis = new List<Epicrisis>();
                List<Medico> medicos = _context.Medicos.ToList();
                List<Episodio> episodios = _context.Episodios.ToList();

                epicrisis.Add(new Epicrisis
                {
                    Diagnostico = "El paciente posee un esguince leve en su Pierna izquierda producto de la caida que sufrio mientras patinaba en Skate.",
                    Recomendacion = "Se aplicaron los vendajes correspondientes, se recomienda al paciente reposo por 72 horas. Debe solicitar un turno con Traumatologia para el control de la lesion.",
                    EmpleadoId = medicos[4].Id,
                    EpisodioId = episodios[0].Id
                });

                epicrisis.Add(new Epicrisis
                {
                    Diagnostico = "Se examino al paciente pero no se encontro ningun traumatismo, se concluye que se encuentra bajo gran estres.",
                    Recomendacion = "Se indica al paciente que solicite turno con Kinesiología.",
                    EmpleadoId = medicos[5].Id,
                    EpisodioId = episodios[1].Id
                });

                _context.Epicrisis.AddRange(epicrisis);
                _context.SaveChanges();
            }
        }

        private void GenerarDirecciones()
        {
            if (_context.Direcciones.Count() == 0)
            {
                List<Direccion> direcciones = new List<Direccion>();
                List<Persona> personas = _context.Personas.ToList();

                direcciones.Add(new Direccion()
                {
                    Calle = "Montevideo",
                    Numero = 248,
                    CodigoPostal = "C1037ADA",
                    Localidad = "Capital Federal",
                    Provincia = Provincia.CAPITAL_FEDERAL,
                    PersonaId = personas[0].Id
                });

                direcciones.Add(new Direccion()
                {
                    Calle = "Bartolomé Mitre",
                    Numero = 2553,
                    CodigoPostal = "C1039AAO",
                    Localidad = "Capital Federal",
                    Provincia = Provincia.CAPITAL_FEDERAL,
                    PersonaId = personas[1].Id
                });

                direcciones.Add(new Direccion()
                {
                    Calle = "Av Meeks",
                    Numero = 155,
                    CodigoPostal = "B1832AHQ",
                    Localidad = "Lomas de Zamora",
                    Provincia = Provincia.BUENOS_AIRES,
                    PersonaId = personas[2].Id
                });

                direcciones.Add(new Direccion()
                {
                    Calle = "Av. Roca",
                    Numero = 1080,
                    CodigoPostal = "B1870AOK",
                    Localidad = "Avellaneda",
                    Provincia = Provincia.BUENOS_AIRES,
                    PersonaId = personas[3].Id
                });

                direcciones.Add(new Direccion()
                {
                    Calle = "Paysandú",
                    Numero = 5724,
                    CodigoPostal = "B1743APT",
                    Localidad = "Moreno",
                    Provincia = Provincia.BUENOS_AIRES,
                    PersonaId = personas[4].Id
                });

                direcciones.Add(new Direccion()
                {
                    Calle = "Ipiranga",
                    Numero = 779,
                    CodigoPostal = "B1609BQO",
                    Localidad = "San Isidro",
                    Provincia = Provincia.BUENOS_AIRES,
                    PersonaId = personas[5].Id
                });

                direcciones.Add(new Direccion()
                {
                    Calle = "Nicolás Avellaneda",
                    Numero = 3335,
                    CodigoPostal = "B1636ASY",
                    Localidad = "Vicente Lopez",
                    Provincia = Provincia.BUENOS_AIRES,
                    PersonaId = personas[6].Id
                });

                direcciones.Add(new Direccion()
                {
                    Calle = "Dr. Emilio Ravignani",
                    Numero = 2049,
                    CodigoPostal = "C1414CPU",
                    Localidad = "Capital Federal",
                    Provincia = Provincia.CAPITAL_FEDERAL,
                    PersonaId = personas[7].Id
                });

                direcciones.Add(new Direccion()
                {
                    Calle = "Raúl Scalabrini Ortiz",
                    Numero = 410,
                    CodigoPostal = "C1414DNR",
                    Localidad = "Capital Federal",
                    Provincia = Provincia.CAPITAL_FEDERAL,
                    PersonaId = personas[8].Id
                });

                direcciones.Add(new Direccion()
                {
                    Calle = "Av. Díaz Vélez",
                    Numero = 3830,
                    CodigoPostal = "C1200AAS",
                    Localidad = "Capital Federal",
                    Provincia = Provincia.CAPITAL_FEDERAL,
                    PersonaId = personas[9].Id
                });

                direcciones.Add(new Direccion()
                {
                    Calle = "Venezuela",
                    Numero = 1260,
                    CodigoPostal = "C1095AAZ",
                    Localidad = "Capital Federal",
                    Provincia = Provincia.CAPITAL_FEDERAL,
                    PersonaId = personas[10].Id
                });

                direcciones.Add(new Direccion()
                {
                    Calle = "Enrique Fernández",
                    Numero = 2119,
                    CodigoPostal = "B1824FCT",
                    Localidad = "Lanús",
                    Provincia = Provincia.BUENOS_AIRES,
                    PersonaId = personas[11].Id
                });

                direcciones.Add(new Direccion()
                {
                    Calle = "Marcos Grigera",
                    Numero = 532,
                    CodigoPostal = "B1828HRK",
                    Localidad = "Lomas de Zamora",
                    Provincia = Provincia.BUENOS_AIRES,
                    PersonaId = personas[12].Id
                });

                direcciones.Add(new Direccion()
                {
                    Calle = "Cnel. Brandsen",
                    Numero = 3263,
                    CodigoPostal = "B1872FXY",
                    Localidad = "Avellaneda",
                    Provincia = Provincia.BUENOS_AIRES,
                    PersonaId = personas[13].Id
                });

                direcciones.Add(new Direccion()
                {
                    Calle = "Unamuno",
                    Numero = 1516,
                    CodigoPostal = "B1879FFN",
                    Localidad = "Quilmes",
                    Provincia = Provincia.BUENOS_AIRES,
                    PersonaId = personas[14].Id
                });

                direcciones.Add(new Direccion()
                {
                    Calle = "José A. Cortejarena",
                    Numero = 1874,
                    CodigoPostal = "C1281AAB",
                    Localidad = "Capital Federal",
                    Provincia = Provincia.CAPITAL_FEDERAL,
                    PersonaId = personas[15].Id
                });

                direcciones.Add(new Direccion()
                {
                    Calle = "Salguero",
                    Numero = 553,
                    CodigoPostal = "B1751CDN",
                    Localidad = "La Matanza",
                    Provincia = Provincia.BUENOS_AIRES,
                    PersonaId = personas[16].Id
                });

                direcciones.Add(new Direccion()
                {
                    Calle = "Lisandro de la Torre",
                    Numero = 329,
                    CodigoPostal = "B1638",
                    Localidad = "Vicente López",
                    Provincia = Provincia.BUENOS_AIRES,
                    PersonaId = personas[17].Id
                });

                _context.Direcciones.AddRange(direcciones);
                _context.SaveChanges();
            }
        }

        private void GenerarTelefonos()
        {
            if (_context.Telefonos.Count() == 0)
            {
                List<Telefono> telefonos = new List<Telefono>();
                List<Persona> personas = _context.Personas.ToList();

                telefonos.Add(new Telefono()
                {
                    Caracteristica = 11,
                    Numero = 43824118,
                    Tipo = TipoTelefono.PARTICULAR,
                    PersonaId = personas[0].Id
                });

                telefonos.Add(new Telefono()
                {
                    Caracteristica = 11,
                    Numero = 49547070,
                    Tipo = TipoTelefono.PARTICULAR,
                    PersonaId = personas[1].Id
                });

                telefonos.Add(new Telefono()
                {
                    Caracteristica = 11,
                    Numero = 42928310,
                    Tipo = TipoTelefono.PARTICULAR,
                    PersonaId = personas[2].Id
                });

                telefonos.Add(new Telefono()
                {
                    Caracteristica = 11,
                    Numero = 49432225,
                    Tipo = TipoTelefono.LABORAL,
                    PersonaId = personas[3].Id
                });

                telefonos.Add(new Telefono()
                {
                    Caracteristica = 11,
                    Numero = 47580113,
                    Tipo = TipoTelefono.CELULAR,
                    PersonaId = personas[4].Id
                });

                telefonos.Add(new Telefono()
                {
                    Caracteristica = 11,
                    Numero = 48568526,
                    Tipo = TipoTelefono.CELULAR,
                    PersonaId = personas[5].Id
                });

                telefonos.Add(new Telefono()
                {
                    Caracteristica = 11,
                    Numero = 45491325,
                    Tipo = TipoTelefono.CELULAR,
                    PersonaId = personas[6].Id
                });

                telefonos.Add(new Telefono()
                {
                    Caracteristica = 11,
                    Numero = 43321083,
                    Tipo = TipoTelefono.LABORAL,
                    PersonaId = personas[7].Id
                });

                telefonos.Add(new Telefono()
                {
                    Caracteristica = 11,
                    Numero = 46729947,
                    Tipo = TipoTelefono.PARTICULAR,
                    PersonaId = personas[8].Id
                });

                telefonos.Add(new Telefono()
                {
                    Caracteristica = 11,
                    Numero = 41481125,
                    Tipo = TipoTelefono.PARTICULAR,
                    PersonaId = personas[9].Id
                });

                telefonos.Add(new Telefono()
                {
                    Caracteristica = 11,
                    Numero = 43735991,
                    Tipo = TipoTelefono.CELULAR,
                    PersonaId = personas[10].Id
                });

                telefonos.Add(new Telefono()
                {
                    Caracteristica = 11,
                    Numero = 44466121,
                    Tipo = TipoTelefono.PARTICULAR,
                    PersonaId = personas[11].Id
                });

                telefonos.Add(new Telefono()
                {
                    Caracteristica = 11,
                    Numero = 43657600,
                    Tipo = TipoTelefono.LABORAL,
                    PersonaId = personas[12].Id
                });

                telefonos.Add(new Telefono()
                {
                    Caracteristica = 11,
                    Numero = 48728281,
                    Tipo = TipoTelefono.CELULAR,
                    PersonaId = personas[13].Id
                });

                telefonos.Add(new Telefono()
                {
                    Caracteristica = 11,
                    Numero = 45832427,
                    Tipo = TipoTelefono.PARTICULAR,
                    PersonaId = personas[14].Id
                });

                telefonos.Add(new Telefono()
                {
                    Caracteristica = 11,
                    Numero = 46691020,
                    Tipo = TipoTelefono.PARTICULAR,
                    PersonaId = personas[15].Id
                });

                telefonos.Add(new Telefono()
                {
                    Caracteristica = 11,
                    Numero = 62104453,
                    Tipo = TipoTelefono.PARTICULAR,
                    PersonaId = personas[16].Id
                });

                telefonos.Add(new Telefono()
                {
                    Caracteristica = 11,
                    Numero = 42169988,
                    Tipo = TipoTelefono.CELULAR,
                    PersonaId = personas[17].Id
                });

                _context.Telefonos.AddRange(telefonos);
                _context.SaveChanges();
            }
        }
    }
}
