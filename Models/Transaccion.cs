using SQLite;

namespace FinanzasPersonales.Models;
public class Transaccion
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Descripcion { get; set; }
    public decimal Monto { get; set; }
    public DateTime Fecha { get; set; }
    public string Categoria { get; set; }  // Ej: "Alimentación", "Transporte"
    public bool EsIngreso { get; set; }  // true si es un ingreso, false si es un gasto
}