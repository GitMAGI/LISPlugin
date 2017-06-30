using System;

namespace IDAL.VO
{
    public class RichiestaLISVO
    {
        public int? esamidid { get; set; }
        public int? esameven { get; set; }
        public DateTime? esamdapr { get; set; }
        public DateTime? esamorpr { get; set; }
        public short? esamurge { get; set; }
        public string esamrout { get; set; }
        public string esamesec { get; set; }
        public int? esamtipo { get; set; }
        public DateTime? esampren { get; set; }
        public int? esamrico { get; set; }
        public int? esamconf { get; set; }
        public string esamdmod { get; set; }
        public string hl7_stato { get; set; }
        public string hl7_msg { get; set; }
    }
}
