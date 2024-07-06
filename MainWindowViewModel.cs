﻿using System.Windows;

namespace SummerPractice
{
    class MainWindowViewModel : ViewModelBase
    {
        private Field _field;
        public Field Field
        {
            get => _field;
            set
            {
                _field = value;
                OnPropertyChanged();
            }
        }

        private Epidemic epidemic;

        private int _fieldWidth = 20;
        public int FieldWidth
        {
            get => _fieldWidth;
            set
            {
                _fieldWidth = value;
                Field.SetColumnsCount(value, IncubationPeriod, SymptomsDuration);
                OnPropertyChanged(nameof(Field));
                OnPropertyChanged();
                OnPropertyChanged(nameof(GridWidth));
            }
        }

        private int _fieldHeight = 20;
        public int FieldHeight
        {
            get => _fieldHeight;
            set
            {
                _fieldHeight = value;
                Field.SetRowsCount(value, IncubationPeriod, SymptomsDuration);
                OnPropertyChanged(nameof(Field));
                OnPropertyChanged();
                OnPropertyChanged(nameof(GridHeight));
            }
        }

        public int GridWidth => FieldWidth * 20;
        public int GridHeight => FieldHeight * 20;

        private int _spreadRadius;
        public int SpreadRadius
        {
            get => _spreadRadius;
            set
            {
                _spreadRadius = value;
                OnPropertyChanged();
                epidemic.SetSpreadRadius(SpreadRadius);
            }
        }

        private int _contactsPerDay;
        public int ContactsPerDay
        {
            get => _contactsPerDay;
            set
            {
                _contactsPerDay = value;
                OnPropertyChanged();
                epidemic.SetContactsPerDay(ContactsPerDay);
            }
        }

        private double _infectionProbability;
        public double InfectionProbability
        {
            get => _infectionProbability;
            set
            {
                _infectionProbability = value;
                OnPropertyChanged();
                epidemic.SetInfectionProbability(InfectionProbability);
            }
        }

        private int _incubationPeriod;
        public int IncubationPeriod
        {
            get => _incubationPeriod;
            set
            {
                _incubationPeriod = value;
                OnPropertyChanged();
                Field.UpdateIncubationPeriod(value);
                OnPropertyChanged(nameof(Field));
            }
        }

        private int _symptomsDuration;
        public int SymptomsDuration
        {
            get => _symptomsDuration;
            set
            {
                _symptomsDuration = value;
                OnPropertyChanged();
                Field.UpdateSymptomsDuration(value, MortalityProbability);
                OnPropertyChanged(nameof(Field));
            }
        }

        private double _mortalityProbability;
        public double MortalityProbability
        {
            get => _mortalityProbability;
            set
            {
                _mortalityProbability = value;
                OnPropertyChanged();
                epidemic.SetMortalityProbability(MortalityProbability);
            }
        }

        public MainWindowViewModel()
        {
            Reset();

            SpreadRadius = 5;
            ContactsPerDay = 10;
            InfectionProbability = 40;
            IncubationPeriod = 5;
            SymptomsDuration = 6;
            MortalityProbability = 20;

            Reset();
        }

        public RelayCommand ResetCommand => new RelayCommand(execute => Reset());
        public RelayCommand StartCommand => new RelayCommand(execute => Start());
        public RelayCommand StepCommand => new RelayCommand(execute => Step());

        public RelayCommand TestCommand => new RelayCommand(execute => Test());

        private void Reset()
        {
            Field = new Field(FieldWidth, FieldHeight, IncubationPeriod, SymptomsDuration);
            epidemic = new Epidemic(SpreadRadius, ContactsPerDay, InfectionProbability, MortalityProbability);
        }
        private void Start()
        {
            
        }
        private void Step() 
        {
            epidemic.SpreadInfection(Field);
            OnPropertyChanged(nameof(Field));
        }

        private void Test()
        {
            FieldWidth = 10;
            //FieldHeight = 10;
        }
    }
}
