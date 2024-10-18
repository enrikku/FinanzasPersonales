using System.Globalization;

namespace FinanzasPersonales;
public class FechaToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime fecha)
        {
            return fecha.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        return string.Empty; // Devuelve una cadena vacía si el valor no es un DateTime
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
