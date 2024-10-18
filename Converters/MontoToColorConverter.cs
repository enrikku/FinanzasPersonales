using System.Globalization;

namespace FinanzasPersonales
{
    public class MontoToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal monto)
            {
                return monto < 0 ? Colors.Red : Colors.Green; // Cambia el color según el monto
            }
            return Colors.Transparent; // Color por defecto si el valor no es válido
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
