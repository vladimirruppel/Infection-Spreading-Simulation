namespace SummerPractice
{
    public enum HealthStatus { Healthy, Infected, Recovered, Dead }

    public class Person
    {
        public HealthStatus Status { get; set; } = HealthStatus.Healthy;
        public int X { get; }
        public int Y { get; }

        public Person(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Infect()
        {
            if (Status == HealthStatus.Healthy)
            {
                Status = HealthStatus.Infected;
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
    }
}
