using IBLL.DTO;
using System.Collections.Generic;

namespace ILISPlugin
{
    public interface ILIS
    {
        string ScheduleNewRequest(RichiestaLISDTO esam, List<AnalisiDTO> anals, ref string errorString);
        MirthResponseDTO SubmitNewRequest(string richid, ref string errorString);

        List<RisultatoDTO> Check4Results(string analid);
        List<AnalisiDTO> Check4Analysis(string richid);
        List<RichiestaLISDTO> Check4Exams(string evenid);
        RefertoDTO Check4Report(string richid);
        List<LabelDTO> Check4Labels(string richid);

        MirthResponseDTO CancelRequest(string richid, ref string errorString);
        bool CheckIfCancelingIsAllowed(string richid, ref string errorString);

        List<RisultatoDTO> RetrieveResults(string richid, ref string errorString);
    }
}
