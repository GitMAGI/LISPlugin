using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestBLL
{
    class Program
    {
        static void Main(string[] args)
        {
            DataAccessLayer.LISDAL dal = new DataAccessLayer.LISDAL();
            BusinessLogicLayer.LISBLL bll = new BusinessLogicLayer.LISBLL(dal);

            IBLL.DTO.EpisodioDTO epis = bll.GetEpisodioById("697798");
        }
    }
}
