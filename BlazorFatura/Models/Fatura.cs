namespace BlazorFatura.Models
{
    public class Fatura
    {
        public int idFatura { get; set; }
        public string NomeFornecedor { get; set; }
        public string NumeroFatura { get; set; }
        public DateTime DataDocumento { get; set; }
        public decimal ValorTotal { get; set; }
        public bool Ativo { get; set; }
        public string OriginalFileName { get; set; } // Novo campo para nome do arquivo PDF
        public byte[] OriginalFileNameBytes { get; set; } // Novo campo para dados do arquivo PDF em bytes
    }
}
