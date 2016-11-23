namespace TestCalling
{
    public class Trash
    {
        public static string DummyFunction(string esamidid)
        {
            string query = "select top 1 ";
            query = query + "REPLACE(REPLACE(ESAM.hl7_stato,'SENDING', 'NW'),'DELETING','CA') AS stathl7, ";
            query = query + "esamidid as esam_id, ";
            query = query + "episidid as idevento, ";
            query = query + "paziidid as paz, ";
            query = query + "CASE ";
            query = query + "WHEN (isnull(cast(esamurge as int),0) = 0) THEN 'R' ";
            query = query + "WHEN (isnull(cast(esamurge as int),0) = 1) THEN 'A' ";
            query = query + "WHEN (isnull(cast(esamurge as int),0) = 2) THEN 'S' END as presurge, ";
            query = query + "case when isnull(nazin.istat,100)<>100 then '999' + nazin.istat else cnas.istat end as istatN, ";
            query = query + "case when isnull(nazir.istat,100)<>100 then '999' + nazir.istat else cres.istat end as istatR, ";
            query = query + "isnull(cnaz.istat,'100') as istatC, ";
            query = query + "convert(datetime,episdain) as episdain_, ";
            query = query + "CONVERT(varchar,EPISDAIN,112) AS admitDate, ";
            query = query + "CONVERT(varchar,EPISDAIN,112) + '' + replace(CONVERT(varchar(5),EPISDAIN,108) ,':','') as admitDateTime, ";
            query = query + "case when episprec > 0 then 'C' when epistipo=1 then 'E' else 'R' end as admittype, ";
            query = query + "CONVERT(varchar,GETDATE(),112) + '' + replace(CONVERT(varchar(5),GETDATE(),108) ,':','') as dataev, ";

            query = query + "CASE WHEN (EPISCHIU = 1) THEN 'H' ";
            query = query + "WHEN EPISTIPO = 1 THEN  'I' ";
            query = query + "WHEN EPISTIPO = 3 THEN  'A' ";
            query = query + "WHEN EPISTIPO = 2 ";
            query = query + "THEN REPLACE(REPLACE(replace(replace(replace(cast(isnull(preotipo,5) as varchar), '5','I'),'2','D'),'1','P'), '0', 'Z'),'4','I') END  as tiporicovero, ";

            query = query + "CAST(PAZI.PAZIIDID AS varchar) + '-' + 'GESTREP' AS PAZIENTE, ";
            query = query + "PAZI.PAZIIDID AS PAZI_PAZIIDID, ";
            query = query + "isnull(PAZI.PAZIREPA, '') AS PAZI_PAZIREPA, ";
            query = query + "isnull(PAZI.PAZINOME, '') AS PAZI_PAZINOME, ";
            query = query + "isnull(PAZI.PAZICOGN, '') AS PAZI_PAZICOGN, ";
            query = query + "isnull(PAZI.PAZISESS, '') AS PAZI_PAZISESS, ";
            query = query + "isnull(PAZI.PAZICOFI, '') AS PAZI_PAZICOFI, ";
            query = query + "isnull(PAZI.PAZICOPS, '') AS PAZI_PAZICOPS, ";
            query = query + "isnull(PAZI.PAZICAPP, '') AS PAZI_PAZICAPS, ";
            query = query + "CONVERT(varchar,PAZI.PAZIDATA,112) AS PAZI_PAZIDATA, ";
            query = query + "isnull(PAZI.PAZITELE, '') AS PAZI_PAZITELE, ";
            query = query + "isnull(PAZI.PAZICOMU, '') AS PAZI_PAZICOMU, ";
            query = query + "isnull(PAZI.PAZIPROV, '') AS PAZI_PAZIPROV, ";
            query = query + "isnull(PAZI.PAZIVIAA, '') AS PAZI_PAZIVIAA, ";
            query = query + "isnull(PAZI.PAZIBRR1, '') AS PAZI_PAZIBRR1, ";
            query = query + "isnull(PAZI.PAZIBRR2, '') AS PAZI_PAZIBRR2, ";
            query = query + "isnull(PAZI.PAZIBRR3, '') AS PAZI_PAZIBRR3, ";
            query = query + "isnull(PAZI.PAZIBRR4, '') AS PAZI_PAZIBRR4, ";
            query = query + "isnull(PAZI.PAZIBRR5, '') AS PAZI_PAZIBRR5, ";
            query = query + "isnull(PAZI.PAZIREGI, '') AS PAZI_PAZIREGI, ";
            query = query + "isnull(PAZI.PAZIMEDI, '') AS PAZI_PAZIMEDI, ";
            query = query + "isnull(PAZI.PAZIASLL, '') AS PAZI_PAZIASLL, ";
            query = query + "isnull(PAZI.PAZISTCI, '') AS PAZI_PAZISTCI, ";
            query = query + "isnull(PAZI.PAZICOND, '') AS PAZI_PAZICOND, ";
            query = query + "isnull(PAZI.PAZIPOSI, '') AS PAZI_PAZIPOSI, ";
            query = query + "isnull(PAZI.PAZIRAMO, '') AS PAZI_PAZIRAMO, ";
            query = query + "isnull(PAZI.PAZITITO, '') AS PAZI_PAZITITO, ";
            query = query + "isnull(PAZI.PAZICAPP, '') AS PAZI_PAZICAPP, ";
            query = query + "isnull(PAZI.PAZICTNZ, '') AS PAZI_PAZICTNZ, ";
            query = query + "isnull(PAZI.PAZIRESI, '') AS PAZI_PAZIRESI, ";
            query = query + "isnull(PAZI.PAZICIRC, '') AS PAZI_PAZICIRC, ";
            query = query + "isnull(PAZI.PAZIMADR, '') AS PAZI_PAZIMADR, ";
            query = query + "isnull(PAZI.PAZIRELO, '') AS PAZI_PAZIRELO, ";
            query = query + "isnull(PAZI.PAZIISTI, '') AS PAZI_PAZIISTI, ";
            query = query + "isnull(PAZI.PAZIISCA, '') AS PAZI_PAZIISCA, ";
            query = query + "ISNULL(PAZI.PAZITEAM ,'') PAZI_PAZITEAM, ";

            // RESIDENZA
            query = query + "isnull(nazir.istat,100) AS nistatr, ";
            query = query + "isnull(nazir.nazione,'Italia') AS nazionerr, ";
            query = query + "'' AS capr, ";
            query = query + "isnull(cres.Istat, '') AS cistatr, ";
            query = query + "isnull(cres.Comune, '') AS cdescrr, ";
            query = query + "isnull(prres.descrizione, '') AS prdescrr, ";
            query = query + "isnull(prres.codice, '') AS prcodr, ";
            query = query + "isnull(PAZI.PAZIVIAA, '') AS indr, ";
            // NASCITA
            query = query + "isnull(nazin.istat,100) AS nistatn, ";
            query = query + "isnull(nazin.nazione,'Italia') AS nazionenn, ";
            query = query + "'' AS capn, ";
            query = query + "cnas.Istat AS cistatn, ";
            query = query + "cnas.Comune AS cdescrn, ";
            query = query + "prnas.descrizione AS prdescrn, ";
            query = query + "prnas.codice AS prcodn, ";
            // DOMICILIO
            query = query + "isnull(nazir.istat,100) AS nistatd, ";
            query = query + "isnull(nazir.nazione,'Italia') AS nazionedd, ";
            query = query + "'' AS capd, ";
            query = query + "isnull(cres.Istat, '') AS cistatd, ";
            query = query + "isnull(cres.Comune, '') AS cdescrd, ";
            query = query + "isnull(prres.descrizione, '') AS prdescrd, ";
            query = query + "isnull(prres.codice, '') AS prcodd, ";
            query = query + "isnull(PAZI.PAZIVIAA, '') AS indd, ";
            // INDIRIZZO REFERTI
            query = query + "isnull(nazir.istat,100) AS nistatrf, ";
            query = query + "isnull(nazir.nazione,'Italia') AS nazionerf, ";
            query = query + "'' AS caprf, ";
            query = query + "isnull(cres.Istat, '') AS cistatrf, ";
            query = query + "isnull(cres.Comune, '') AS cdescrrf, ";
            query = query + "isnull(prres.descrizione, '') AS prdescrrf, ";
            query = query + "isnull(prres.codice, '') AS prcodrf, ";
            query = query + "isnull(PAZI.PAZIVIAA, '') AS indrf, ";

            query = query + "isnull(asll.codice, 120102) AS asl_codice, ";
            query = query + "isnull(asll.descrizione, 'RM/B') AS asl_descr, ";

            query = query + "case when episintens=2 and repaidid in (17,62) then 150  when episintens=2 and repaidid = 11 then 151 else REPAIDID end AS REPAIDID, ";
            query = query + "isnull(repanome, '') as repanome, ";
            query = query + "isnull(repapsps, '') as repapsps, ";

            query = query + "ltrim(rtrim(perscogn)) as perscogn, ";
            query = query + "ltrim(rtrim(persnome)) as persnome, ";
            query = query + "'GR' + cast(persidid as varchar(10)) as persidid, ";
            query = query + "'' AS perscofi, ";

            query = query + "CONVERT(varchar,ESAMPREN,112) + '' + replace(CONVERT(varchar(5),ESAMPREN,108) ,':','') as dataPREN, ";
            query = query + "'' RADIOTIPO, ";
            query = query + "'' RADIODESC, ";

            query = query + "'' as PRESQUES ";

            //query = query + "* ";

            query = query + "from esam ";
            query = query + "inner join even on evenidid = ESAMEVEN ";
            query = query + "inner join epis on episidid = evenepis ";
            query = query + "inner join pazi on epispazi = paziidid ";
            query = query + "left outer join (select preotipo, evenepis epid from preo inner join even on evenidid = preoeven) pr on pr.epid = episidid ";
            query = query + "left outer join repa on evenreri = repaidid ";
            query = query + "left outer join pers on persidid = evenperi ";
            query = query + "left outer join listacomuniappo cnas on PAZICOMU = cnas.Comune ";
            query = query + "left outer join listacomuniappo cres on PAZIresi = cres.Comune ";
            query = query + "left outer join listacomuniappo cnaz on PAZIctnz = cnaz.Comune ";
            query = query + "left outer join province prnas on cnas.provincia = prnas.codice ";
            query = query + "left outer join province prres on cres.provincia = prres.codice ";
            query = query + "left outer join asll on replace (paziasll, 'roma', 'RM/B') = asll.descrizione ";
            query = query + "left outer join ";
            query = query + "(select istat, comune as nazione from listacomuniappo where provincia is null) nazir ";
            query = query + "on nazir.istat = cres.istat ";
            query = query + "left outer join ";
            query = query + "(select istat, comune as nazione from listacomuniappo where provincia is null) nazin ";
            query = query + "on nazin.istat = cnas.istat ";
            query = query + "where esamtipo = 0 ";
            query = query + "and ( isnull(hl7_stato,'')='SENDING' or isnull(hl7_stato,'')='DELETING' ) ";
            query = query + "and esamidid = " + esamidid;

            return query;
        }
    }
}
