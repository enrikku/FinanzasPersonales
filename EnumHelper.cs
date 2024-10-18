namespace FinanzasPersonales;
public static class EnumHelper
{
    public static List<string> ObtenerTiposTransaccion()
    {
        return Enum.GetNames(typeof(TipoTransaccion)).ToList();
    }
}