using System.Configuration;

namespace DataAccessLayer
{
    public partial class LISDAL : IDAL.ILISDAL
    {
        public static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string HLTDesktopConnectionString = ConfigurationManager.ConnectionStrings["HltDesktop"].ConnectionString;
        public string GRConnectionString = ConfigurationManager.ConnectionStrings["GR"].ConnectionString;
        public string CCConnectionString = ConfigurationManager.ConnectionStrings["CC"].ConnectionString;

        public string AnalisiTabName = ConfigurationManager.AppSettings["tbn_analisi"];
        public string LabelTabName = ConfigurationManager.AppSettings["tbn_label"];
        public string RichiestaLISTabName = ConfigurationManager.AppSettings["tbn_richiestalis"];
        public string PazienteTabName = ConfigurationManager.AppSettings["tbn_paziente"];
        public string EventoTabName = ConfigurationManager.AppSettings["tbn_evento"];
        public string EpisodioTabName = ConfigurationManager.AppSettings["tbn_episodio"];
        public string RisultatoGrezzoTabName = ConfigurationManager.AppSettings["tbn_anretrash"];
        public string RisultatoTabName = ConfigurationManager.AppSettings["tbn_anre"];
        public string RefertoTabName = ConfigurationManager.AppSettings["tbn_refe"];
        public string RepartoTabName = ConfigurationManager.AppSettings["tbn_repa"];
        public string PrestazioneTabName = ConfigurationManager.AppSettings["tbn_pres"];
    }
}
