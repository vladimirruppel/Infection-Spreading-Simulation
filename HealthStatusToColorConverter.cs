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
                    return Brushes.IndianRed;
                case HealthStatus.Sick:
                    return Brushes.Red;
                case HealthStatus.Recovered:
                    return Brushes.Blue;
                case HealthStatus.Dead:
                    return Brushes.Black;
                default:
                    return Brushes.Yellow;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}