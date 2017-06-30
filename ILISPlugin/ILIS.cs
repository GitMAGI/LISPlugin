using IBLL.DTO;
using System.Collections.Generic;

namespace ILISPlugin
{
    public interface ILIS
    {
        string ScheduleNewRequest(EventoDTO even, PrestazioneDTO pres, RichiestaLISDTO esam, List<AnalisiDTO> anals, ref string errorString);
        MirthResponseDTO SubmitNewRequest(string richid, ref string errorString);

        List<RisultatoDTO> Check4Results(string analid);
        List<AnalisiDTO> Check4Analysis(string richid);
        List<RichiestaLISDTO> Check4Richs(string evenid);
        RefertoDTO Check4Report(string richid);
        List<LabelDTO> Check4Labels(string richid);

        RichiestaLISDTO RetrieveRich(string richid);
        AnalisiDTO RetrieveAnal(string anal);
        EventoDTO RetrieveEven(string evenid);
        PrestazioneDTO RetrievePresByEven(string evenid);
        PrestazioneDTO RetrievePres(string presid);
        RepartoDTO RetrieveRepa(string repaid);
        RepartoDTO RetrieveRepaByNome(string repanome);
        PazienteDTO RetrievePazi(string paziid);

        MirthResponseDTO CancelRequest(string richid, ref string errorString);
        bool CheckIfCancelingIsAllowed(string richid, ref string errorString);

        //List<RisultatoDTO> RetrieveResults(string richid, ref string errorString);
        List<RisultatoDTO> RetrieveResults(string richid_, ref string errorString, bool? forceUpdating = null);
    }
}
