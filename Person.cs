using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SummerPractice
{
    public enum HealthStatus { Healthy, Infected, Sick, Recovered, Dead }

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

        private int incubationPeriod;
        private int symptomsDuration;
        private int daysToGetSick;
        private int daysToGetRecovered;

        private bool triedToBeDead = false;

        public Person(int x, int y, int incubationPeriod, int symptomsDuration)
        {
            X = x;
            Y = y;
            daysToGetSick = this.incubationPeriod = incubationPeriod;
            daysToGetRecovered = this.symptomsDuration = symptomsDuration;
        }

        public void SetIncubationPeriod(int incubationPeriod)
        {
            int oldIncubationPeriod = this.incubationPeriod;
            int diff = oldIncubationPeriod - incubationPeriod;
            if (diff > 0)
                daysToGetSick += diff;
            else if (diff < 0)
            {
                daysToGetSick = 0;
                GetSick();
            }
            this.incubationPeriod = incubationPeriod;
        }

        public void SetSymptomsDuration(int symptomsDuration)
        { 
            int oldSymptomsDuration = this.symptomsDuration;
            int diff = oldSymptomsDuration - symptomsDuration;
            if (diff > 0)
                daysToGetRecovered += diff;
            else if (diff < 0)
            {
                daysToGetRecovered = 0;
                Recover();
            }
            this.symptomsDuration = symptomsDuration;
        }

        public void Infect()
        {
            Status = HealthStatus.Infected;
        }

        public void GetSick()
        {
            Status = HealthStatus.Sick;
        }

        public void Recover()
        {
            Status = HealthStatus.Recovered;
        }

        public void Die()
        {
            Status = HealthStatus.Dead;
        }

        public void InfectionExposure(double mortalityProbability)
        {
            if (Status == HealthStatus.Infected)
            {
                if ((--daysToGetSick) == 0)
                    GetSick();
            }
            if (Status == HealthStatus.Sick)
            {
                if (!triedToBeDead)
                {
                    double healthValue = StaticMethods.GetRandomDouble(0, 100);
                    if (healthValue <= mortalityProbability)
                    {
                        Die();
                        return;
                    }
                    else
                    {
                        triedToBeDead = true;
                    }
                }

                if ((--daysToGetRecovered) == 0)
                    Recover();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
