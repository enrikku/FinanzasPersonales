using FinanzasPersonales.DB;
using SQLite;

namespace FinanzasPersonales
{
    public static class VariablesGlobales
    {
        public static DatabaseService db { get; set; }
        public static string RUTA_DB = Path.Combine(FileSystem.AppDataDirectory, "FinanzasPersonales.db");
    }
}
