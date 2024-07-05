using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerPractice
{
    public class Field
    {
        public Person[] Grid { get; }
        public int ColumnsCount { get; }
        public int RowsCount { get; }

        public Field(int columnsCount, int rowsCount, int incubationPeriod, int symptomsDuration)
        {
            ColumnsCount = columnsCount;
            RowsCount = rowsCount;
            Grid = new Person[RowsCount * ColumnsCount];
            InitializeField(incubationPeriod, symptomsDuration);
        }

        private void InitializeField(int incubationPeriod, int symptomsDuration)
        {
            for (int i = 0; i < ColumnsCount; i++)
            {
                for (int j = 0; j < RowsCount; j++)
                {
                    Grid[j * ColumnsCount + i] = new Person(i, j, incubationPeriod, symptomsDuration);
                }
            }

            // инфицировать одного случайного человека
            int randomX = StaticMethods.GetRandomInt(0, ColumnsCount - 1);
            int randomY = StaticMethods.GetRandomInt(0, RowsCount - 1);
            Grid[randomY * ColumnsCount + randomX].Infect();
        }

        public void UpdateIncubationPeriod(int incubationPeriod)
        {
            Grid
                .Where(person => person.Status != HealthStatus.Dead || person.Status != HealthStatus.Recovered)
                .ToList()
                .ForEach(person => person.SetIncubationPeriod(incubationPeriod));
        }

        public void UpdateSymptomsDuration(int symptomsDuration, double mortalityProbability)
        {
            Grid
                .Where(person => person.Status != HealthStatus.Dead || person.Status != HealthStatus.Recovered)
                .ToList()
                .ForEach(person => person.SetSymptomsDuration(symptomsDuration, mortalityProbability));
        }
    }
}
