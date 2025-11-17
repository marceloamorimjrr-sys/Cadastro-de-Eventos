using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EventosApp.Models
{
    // Classe da Model que armazena os dados e a lógica de cálculo
    public class Evento : INotifyPropertyChanged
    {
        // Implementação básica de INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _nome;
        public string Nome
        {
            get => _nome;
            set
            {
                if (_nome != value)
                {
                    _nome = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _dataInicio = DateTime.Now;
        public DateTime DataInicio
        {
            get => _dataInicio;
            set
            {
                if (_dataInicio != value)
                {
                    _dataInicio = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DuracaoEmDias)); // Recalcula a duração
                    OnPropertyChanged(nameof(TimeSpanDuracao)); // Recalcula o TimeSpan
                }
            }
        }

        private DateTime _dataTermino = DateTime.Now.AddDays(1);
        public DateTime DataTermino
        {
            get => _dataTermino;
            set
            {
                if (_dataTermino != value)
                {
                    _dataTermino = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(DuracaoEmDias)); // Recalcula a duração
                    OnPropertyChanged(nameof(TimeSpanDuracao)); // Recalcula o TimeSpan
                }
            }
        }

        private int _participantes;
        public int Participantes
        {
            get => _participantes;
            set
            {
                if (_participantes != value)
                {
                    _participantes = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CustoTotal)); // Recalcula o custo total
                }
            }
        }

        private string _local;
        public string Local
        {
            get => _local;
            set
            {
                if (_local != value)
                {
                    _local = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _custoPorParticipante;
        public decimal CustoPorParticipante
        {
            get => _custoPorParticipante;
            set
            {
                if (_custoPorParticipante != value)
                {
                    _custoPorParticipante = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CustoTotal)); // Recalcula o custo total
                }
            }
        }

        // Propriedade calculada: Duração do evento em dias (double)
        public double DuracaoEmDias
        {
            get
            {
                // Garante que a data de término seja maior ou igual à de início para o cálculo
                if (DataTermino >= DataInicio)
                {
                    return (DataTermino - DataInicio).TotalDays;
                }
                return 0;
            }
        }
        
        // Propriedade calculada: Duração do evento como TimeSpan (para requisito)
        public TimeSpan TimeSpanDuracao
        {
            get
            {
                if (DataTermino >= DataInicio)
                {
                    return DataTermino - DataInicio;
                }
                return TimeSpan.Zero;
            }
        }

        // Propriedade calculada: Custo total do evento
        public decimal CustoTotal
        {
            get => Participantes * CustoPorParticipante;
        }
    }
}
