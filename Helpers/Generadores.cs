using System.Security.Claims;

namespace Historias_Clinicas_D.Helpers
{
    public static class Generadores
    {
        private static readonly Random random = new Random();
        private static readonly string caracteres = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static int GetId(ClaimsPrincipal user)
        {
            return Int32.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        public static string GetNewLegajoEmpleado(int largo)
        {
            return GetRandomString(caracteres, largo);
        }

        private static string GetRandomString(string caracteres, int largo)
        {
            return new string(Enumerable.Repeat(caracteres, largo).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
