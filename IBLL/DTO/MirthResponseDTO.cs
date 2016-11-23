using System;
using System.Collections.Generic;

namespace IBLL.DTO
{
    public class MirthResponseDTO
    {
        public bool Errored { get; set; }
        public bool Accepted { get; set; }
        public bool Refused { get; set; }
        public string ACKCode { get; set; }
        public string ACKDesc { get; set; }
        public string MsgID { get; set; }
        public string ERRMsg { get; set; }

        public List<ORCStatus> ORCStatus { get; set; }

        public List<LabelDTO> Labes { get; set; }
    }
    public class ORCStatus
    {
        public string EsamID { get; set; }
        public string AnalID { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        
    }
}
