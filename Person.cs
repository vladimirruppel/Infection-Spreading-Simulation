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

        private int deathCheckDay;
        private bool triedToBeDead = false;

        public Person(int x, int y, int incubationPeriod, int symptomsDuration)
        {
            X = x;
            Y = y;
            daysToGetSick = this.incubationPeriod = incubationPeriod;
            daysToGetRecovered = this.symptomsDuration = symptomsDuration;
            deathCheckDay = StaticMethods.GetRandomInt(1, 7);
        }

        public void SetIncubationPeriod(int incubationPeriod)
        {
            int oldIncubationPeriod = this.incubationPeriod;
            int diff = incubationPeriod - oldIncubationPeriod;
            if (diff > 0)
                daysToGetSick += diff;
            else if (diff < 0)
            {
                if (Status == HealthStatus.Infected)
                {
                    daysToGetSick = 0;
                    GetSick();
                }
            }
            this.incubationPeriod = incubationPeriod;
        }

        public void SetSymptomsDuration(int symptomsDuration, double mortalityProbability)
        { 
            int oldSymptomsDuration = this.symptomsDuration;
            int diff = symptomsDuration - oldSymptomsDuration;
            if (diff > 0)
                daysToGetRecovered += diff;
            else if (diff < 0)
            {
                if (Status == HealthStatus.Sick)
                {
                    daysToGetRecovered = 0;
                    if (!triedToBeDead)
                        CheckDeath(mortalityProbability);
                    if (Status != HealthStatus.Dead)
                        Recover();
                }
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

        // человек умирает с определенной вероятностью
        private void CheckDeath(double mortalityProbability)
        {
            double healthValue = StaticMethods.GetRandomDouble(0, 100);
            if (healthValue <= mortalityProbability)
            {
                Die();
            }

            triedToBeDead = true;
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
                int thisDayNumber = symptomsDuration - daysToGetRecovered;
                if (thisDayNumber == deathCheckDay)
                {
                    CheckDeath(mortalityProbability);
                    if (Status == HealthStatus.Dead)
                        return;
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
