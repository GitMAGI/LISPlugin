namespace IBLL.DTO
{
    public class PazienteDTO
    {
        public int? archivio { get; set; }
        public string nominativo { get; set; }
        public string coniugata { get; set; }
        public string sesso { get; set; }
        public string data_nascita { get; set; }
        public string luogo_nascita { get; set; }
        public string paese { get; set; }
        public string indirizzo { get; set; }
        public string comune { get; set; }
        public string cap { get; set; }
        public string prefisso { get; set; }
        public string telefono { get; set; }
        public string codice_fiscale { get; set; }
        public string paternita { get; set; }
        public string maternita { get; set; }
        public string stato_civile { get; set; }
        public string professione { get; set; }
        public string documento { get; set; }
        public string luogo_documento { get; set; }
        public string data_documento { get; set; }
        public string domicilio { get; set; }
        public string comune_domicilio { get; set; }
        public string responsabile { get; set; }
        public string indirizzo_resp { get; set; }
        public string comune_resp { get; set; }
        public string telefono_resp { get; set; }
        public int? curante { get; set; }
        public int? seriale { get; set; }
        public string apazext { get; set; }
        public string email { get; set; }
        public string cellulare { get; set; }
        public string tessera_cee { get; set; }
        public string data_cert { get; set; }
        public string data_creazione { get; set; }
        public bool? Export { get; set; }
        public double? dovuto { get; set; }
        public bool? speciale { get; set; }
        public double? dovuto_privato { get; set; }
        public double? dovuto_assicurato { get; set; }
        public string note { get; set; }
        public byte[] hash { get; set; }
        public string dt_agg { get; set; }
        public string citta_nascita { get; set; }
        public string citta_residenza { get; set; }
    }
}
