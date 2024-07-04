using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummerPractice
{
    class Field
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
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    Grid[i * RowsCount + j] = new Person(i, j);
                }
            }
        }
    }
}
