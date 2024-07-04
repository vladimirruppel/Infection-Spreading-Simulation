using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SummerPractice
{
    public enum HealthStatus { Healthy, Infected, Recovered, Dead }

    public class Person : INotifyPropertyChanged
    {
        private HealthStatus _status = HealthStatus.Healthy;
        public HealthStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public int X { get; }
        public int Y { get; }
        public double HealthPoints;
        private double healthReducingStep;

        public Person(int x, int y)
        {
            X = x;
            Y = y;

            HealthPoints = StaticMethods.GetRandomDouble(80, 100);
            healthReducingStep = StaticMethods.GetRandomDouble(3, 14);
        }

        public void Infect()
        {
            if (Status == HealthStatus.Healthy)
            {
                Status = HealthStatus.Infected;
            }
        }

        public void InfectionExposure()
        {
            if (Status == HealthStatus.Infected)
            {
                HealthPoints -= healthReducingStep;
                if (HealthPoints <= 0)
                    Die();
            }
        }

        public void Recover()
        {
            if (Status == HealthStatus.Infected)
            {
                Status = HealthStatus.Recovered;
            }
        }

        public void Die()
        {
            if (Status == HealthStatus.Infected)
            {
                Status = HealthStatus.Dead;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
