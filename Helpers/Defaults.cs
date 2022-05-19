namespace Historias_Clinicas_D.Helpers
{
    public static class Defaults
    {
        public const string RolAdmin = "Administrador";
        public const string RolPaciente = "Paciente";
        public const string RolMedico = "Medico";
        public const string RolEmpleado = "Empleado";
        public const string AdminUserName = "admin@admin.com";
        public const string AdminPassword = "Admin.1234";
        public const string EmpleadoPassword = "Empleado.1234";
        public const string PacientePassword = "Paciente.1234";
        public const string MedicoPassword = "Medico.1234";
        public const bool EstadoAbierto = true;
        public const string SinTelefono = "No hay un Teléfono asociado";
        public static DateTime FechaActual = DateTime.Now;
    }
}
