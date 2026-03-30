namespace backend.Entities;

    public class EventoEntity
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public DateTime Data { get; set; }

        public TimeSpan Horario { get; set; }

        public int CapacidadeMaxima { get; set; }

        public decimal Valor { get; set; }

        public string Setor { get; set; }

        public TimeSpan DuracaoAproximada { get; set; }

        public int LimitePorPessoa { get; set; }
    }
