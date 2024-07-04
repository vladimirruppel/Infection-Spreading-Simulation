using System.Windows.Data;
using System.Windows.Media;

namespace SummerPractice
{
    public class HealthStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch ((HealthStatus)value)
            {
                case HealthStatus.Healthy:
                    return Brushes.White;
                case HealthStatus.Infected:
                    return Brushes.Red;
                case HealthStatus.Dead:
                    return Brushes.Black;
                default:
                    return Brushes.Blue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}