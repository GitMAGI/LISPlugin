using System.Collections.Generic;

namespace IBLL
{
    public interface ILISBLL
    {
        DTO.PazienteDTO GetPazienteById(string id);

        DTO.EpisodioDTO GetEpisodioById(string id);
        DTO.EpisodioDTO AddEpisodio(DTO.EpisodioDTO data);
        DTO.EpisodioDTO UpdateEpisodio(DTO.EpisodioDTO data);

        DTO.EventoDTO GetEventoById(string id);
        DTO.EventoDTO AddEvento(DTO.EventoDTO data);
        DTO.EventoDTO UpdateEvento(DTO.EventoDTO data);
        int DeleteEventoById(string evendid);

        List<DTO.RichiestaLISDTO> GetRichiesteLISByEven(string episid);
        DTO.RichiestaLISDTO GetRichiestaLISById(string richidid);
        DTO.RichiestaLISDTO AddRichiestaLIS(DTO.RichiestaLISDTO esam);
        DTO.RichiestaLISDTO UpdateRichiestaLIS(DTO.RichiestaLISDTO esam);
        int DeleteRichiestaLISById(string esamidid);

        List<DTO.AnalisiDTO> GetAnalisisByRichiesta(string richidid);
        DTO.AnalisiDTO GetAnalisiById(string analidid);
        List<DTO.AnalisiDTO> GetAnalisisByIds(List<string> analidids);
        DTO.AnalisiDTO UpdateAnalisi(DTO.AnalisiDTO data);
        DTO.AnalisiDTO AddAnalisi(DTO.AnalisiDTO data);
        List<DTO.AnalisiDTO> AddAnalisis(List<DTO.AnalisiDTO> data);
        int DeleteAnalisiById(string analidid);
        int DeleteAnalisiByRichiesta(string richidid);

        List<DTO.LabelDTO> GetLabelsByRichiesta(string richidid);
        DTO.LabelDTO GetLabelById(string labeidid);
        DTO.LabelDTO UpdatLabel(DTO.LabelDTO data);
        DTO.LabelDTO AddLabel(DTO.LabelDTO data);
        List<DTO.LabelDTO> AddLabels(List<DTO.LabelDTO> data);
        int DeleteLabelById(string labeidid);

        List<DTO.RisultatoDTO> GetRisultatiByEsamAnalId(string id);
        List<DTO.RisultatoDTO> GetRisultatiByAnalId(string id);
        List<DTO.RisultatoDTO> AddRisultati(List<DTO.RisultatoDTO> data);
        int DeleteRisultatiByIdAnalisi(string analid);

        DTO.MirthResponseDTO ORLParser(string raw);
        string SendMirthRequest(string richidid);
        List<DTO.LabelDTO> StoreLabels(List<DTO.LabelDTO> labes);
        int ChangeHL7StatusAndMessageAll(string richidid, string presidid, string hl7_stato, string hl7_msg = null);
        List<DTO.AnalisiDTO> ChangeHL7StatusAndMessageAnalisis(List<string> analidids, string hl7_stato, string hl7_msg = null);
        DTO.RichiestaLISDTO ChangeHL7StatusAndMessageRichiestaLIS(string richidid, string hl7_stato, string hl7_msg = null);
        DTO.PrestazioneDTO ChangeHL7StatusAndMessagePrestazione(string presidid, string hl7_stato, string hl7_msg = null);
        bool ValidateEsam(DTO.RichiestaLISDTO esam, ref string errorString);
        bool ValidatePres(DTO.PrestazioneDTO pres, ref string errorString);
        bool ValidateAnals(List<DTO.AnalisiDTO> anals, ref string errorString);

        DTO.RefertoDTO GetRefertoByEsamId(string id);
        DTO.RefertoDTO GetRefertoById(string id);

        DTO.RepartoDTO GetRepartoById(string id);
        DTO.RepartoDTO GetRepartoByNome(string nome);

        DTO.PrestazioneDTO GetPrestazioneById(string presidid);
        DTO.PrestazioneDTO GetPrestazioneByEvento(string evenidid);
        DTO.PrestazioneDTO AddPrestazione(DTO.PrestazioneDTO data);
        DTO.PrestazioneDTO UpdatePrestazione(DTO.PrestazioneDTO data);
        int DeletePrestazioneById(string presidid);
    }
}