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

        public Field(int columnsCount, int rowsCount)
        {
            ColumnsCount = columnsCount;
            RowsCount = rowsCount;
            Grid = new Person[RowsCount * ColumnsCount];
            InitializeField();
        }

        private void InitializeField()
        {
            for (int i = 0; i < ColumnsCount; i++)
            {
                for (int j = 0; j < RowsCount; j++)
                {
                    Grid[j * ColumnsCount + i] = new Person(i, j);
                }
            }

            // инфицировать одного случайного человека
            int randomX = StaticMethods.GetRandomInt(0, ColumnsCount - 1);
            int randomY = StaticMethods.GetRandomInt(0, RowsCount - 1);
            Grid[randomY * ColumnsCount + randomX].Infect();
        }
    }
}
