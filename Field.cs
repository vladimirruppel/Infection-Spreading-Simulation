namespace SummerPractice
{
    public class Field : PropertyChangedBase
    {
        private Person[] _grid;
        public Person[] Grid
        {
            get => _grid;
            set
            {
                _grid = value;
                OnPropertyChanged();
            }
        }

        private int _columnsCount;
        public int ColumnsCount
        {
            get => _columnsCount;
            set
            {
                _columnsCount = value;
                OnPropertyChanged();
            }
        }
        private int _rowsCount;
        public int RowsCount
        {
            get => _rowsCount;
            set
            {
                _rowsCount = value;
                OnPropertyChanged();
            }
        }

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

        public void SetColumnsCount(int columnsCount, int incubationPeriod, int symptomsDuration)
        {
            int oldColumnsCount = ColumnsCount;
            int diff = columnsCount - oldColumnsCount;

            // новая сетка с новым количеством столбцов
            Person[] Grid = new Person[RowsCount * columnsCount];
            if (diff > 0)
            {
                for (int row = 0; row < RowsCount; row++)
                {
                    // перенести старые столбцы
                    for (int column = 0; column < oldColumnsCount; column++)
                    {
                        Grid[row * columnsCount + column] = this.Grid[row * oldColumnsCount + column];
                    }
                    // добавить новые столбцы с новыми людьми (их кол-во равно diff)
                    for (int column = oldColumnsCount; column < columnsCount; column++)
                    {
                        Grid[row * columnsCount + column] = new Person(column, row, incubationPeriod, symptomsDuration);
                    }

                }
            }
            else if (diff < 0)
            {
                // убрать diff последних столбцов
                for (int row = 0; row < RowsCount; row++)
                {
                    // просто перенос columnsCount старых столбцов в новую сетку
                    for (int column = 0; column < columnsCount; column++)
                    {
                        Grid[row * columnsCount + column] = this.Grid[row * oldColumnsCount + column];
                    }
                } 
            }

            this.Grid = Grid;
            this.ColumnsCount = columnsCount;
        }

        public void SetRowsCount(int rowsCount, int incubationPeriod, int symptomsDuration) 
        {
            int oldRowsCount = RowsCount;
            int diff = rowsCount - oldRowsCount;

            Person[] Grid = new Person[rowsCount * ColumnsCount];
            if (diff > 0)
            {
                for (int column = 0; column < ColumnsCount; column++)
                {
                    // перенести старые строки
                    for (int row = 0; row < oldRowsCount; row++)
                    {
                        Grid[row * ColumnsCount + column] = this.Grid[row * ColumnsCount + column];
                    }
                    // добавить новые строки
                    for (int row = oldRowsCount; row < rowsCount; row++)
                    {
                        Grid[row * ColumnsCount + column] = new Person(column, row, incubationPeriod, symptomsDuration);
                    }
                }
            }
            else if (diff < 0)
            {
                // убрать diff последних строк
                for (int column = 0; column < ColumnsCount; column++)
                {
                    // просто перенести старые строки
                    for (int row = 0; row < rowsCount; row++)
                    {
                        Grid[row * ColumnsCount + column] = this.Grid[row * ColumnsCount + column];
                    }
                }
            }

            this.Grid = Grid;
            this.RowsCount = rowsCount;
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

        public bool AreThereSickPeople()
        {
            return Grid.Where(person => person.Status == HealthStatus.Infected || person.Status == HealthStatus.Sick).Count() > 0;
        }
    }
}
