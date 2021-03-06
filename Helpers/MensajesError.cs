namespace Historias_Clinicas_D.Helpers
{
    public static class MensajesError
    {
        public const string ErrRequired = "El campo {0} es requerido";
        public const string ErrMaximo = "La longitud maxima de este campo es de {1} caracteres";
        public const string ErrMinimo = "La longitud minima de este campo es de {1} caracteres";
        public const string ErrDataTypeDateTime = "Por favor ingresar una fecha valida en el campo {0}";
        public const string ErrNoValido = "El campo {0} no es válido";
        public const string ErrMinMax = "El campo {0} necesita un minimo de {2} caracteres y un maximo de {1}";
        public const string ErrMaxMin = "El campo {0} necesita un minimo de {1} caracteres y un maximo de {2}";
        public const string ErrRangoMinMax = "El campo {0} debe estar comprendido entre {1} y {2}";
        public const string ErrCampoEnUso = "El {0} ya se encuentra registrado";
        public const string ErrMatriculaEnUso = "La Matrícula ya se encuentra registrada";
        public const string ErrLogIn = "Inicio de sesión Invalido";
        public const string ErrPasswordMissmatching = "Las Contraseñas no coinciden";
        public const string ErrCurrentPassword = "La Contraseña Actual es incorrecta";
        public const string ErrEvolucionesPendientes = "El Episodio no se puede cerrar, tiene evoluciones pendientes sin cerrar";
    }
}
