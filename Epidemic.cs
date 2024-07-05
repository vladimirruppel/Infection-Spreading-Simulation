namespace SummerPractice
{
    public class Epidemic
    {
        private int spreadRadius;
        private int contactsPerDay;
        private double infectionProbability;
        private double mortalityProbability;

        public Epidemic(int spreadRadius, int contactsPerDay, double infectionProbability, double mortalityProbability)
        {
            this.spreadRadius = spreadRadius;
            this.contactsPerDay = contactsPerDay;
            this.infectionProbability = infectionProbability;
            this.mortalityProbability = mortalityProbability;
        }

        public void SetSpreadRadius(int spreadRadius)
        {
            this.spreadRadius = spreadRadius;
        }
        public void SetContactsPerDay(int contactsPerDay)
        {
            this.contactsPerDay = contactsPerDay;
        }
        public void SetInfectionProbability(double infectionProbability)
        {
            this.infectionProbability = infectionProbability;
        }
        public void SetMortalityProbability(double mortalityProbability)
        {
            this.mortalityProbability = mortalityProbability;
        }

        public void SpreadInfection(Field field)
        {
            // заражение небольных соседей в радиусе распространения
            List<Person> infectedPeople = field.Grid
                .Where(person => person.Status == HealthStatus.Infected || person.Status == HealthStatus.Sick)
                .ToList();
            foreach (Person infectedPerson in infectedPeople)
            {
                List<Person> neighbors = GetNeighbors(field, infectedPerson);
                foreach (Person neighbor in neighbors) 
                {
                    // заразить здоровых соседей с какой-то вероятностью
                    if (neighbor.Status == HealthStatus.Healthy) 
                    {
                        double neighborImmunityValue = StaticMethods.GetRandomDouble(0, 100);
                        // если иммунитет соседа сильнее (больше по значению) вероятности заражения
                        if (neighborImmunityValue <= infectionProbability) // то заразить соседа
                            neighbor.Infect();
                    }
                }
            }

            // воздействие инфекции на зараженных людей (отнять очки жизней)
            infectedPeople.ForEach(infectedPerson => infectedPerson.InfectionExposure(mortalityProbability));
        }

        // функция, в рандомном порядке получающая соседей указанного человека, учитывающая радиус распространения и кол-во контактов в день
        // при этом, в списке могут быть уже инфицированные люди, но мертвых людей там не будет
        private List<Person> GetNeighbors(Field field, Person person)
        {
            List<Person> neighbors = new List<Person>();
            int startX = Math.Max(0, person.X - spreadRadius);
            int endX = Math.Min(field.ColumnsCount - 1, person.X + spreadRadius);
            int startY = Math.Max(0, person.Y - spreadRadius);
            int endY = Math.Min(field.RowsCount - 1, person.Y + spreadRadius);

            // получить всех соседей человека в радиусе распространения
            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    if (x != person.X || y != person.Y)
                    {
                        neighbors.Add(field.Grid[y * field.ColumnsCount + x]);
                    }
                }
            }

            // удаление мертвых людей из списка
            neighbors = neighbors.Where(person => person.Status != HealthStatus.Dead).ToList();

            // сортировка людей в рандомном порядке
            Random random = new Random();
            neighbors = neighbors.OrderBy(_ => random.Next()).ToList();

            // отсечение списка людей до кол-ва контактов в день
            int peopleToRemoveCount = neighbors.Count - contactsPerDay;
            if (peopleToRemoveCount > 0)
            {
                neighbors.RemoveRange(contactsPerDay, peopleToRemoveCount);
            }

            return neighbors;
        }
    }
}
