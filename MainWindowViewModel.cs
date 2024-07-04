using System.Windows;

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

        private int _fieldWidth = 20;
        public int FieldWidth
        {
            get => _fieldWidth;
            set
            {
                _fieldWidth = value;
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
            }
        }

        public MainWindowViewModel()
        {
            Field = new Field(FieldWidth, FieldHeight);
        }

        public RelayCommand ResetCommand => new RelayCommand(execute => Reset());
        public RelayCommand StartCommand => new RelayCommand(execute => Start());
        public RelayCommand StepCommand => new RelayCommand(execute => Step());

        private void Reset()
        {
            
        }
        private void Start()
        {
            
        }
        private void Step() 
        {
            
        }
    }
}
