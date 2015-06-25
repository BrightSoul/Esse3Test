using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESSE3Test.ESSE3;
using System.Xml.Serialization;
using System.IO;
using System.Xml;


namespace ESSE3Test
{

    class Program
    {



        static void Main(string[] args)
        {



            var client = new Esse3WsClient();

            Console.Write("Username [guest]: ");
            var username = Console.ReadLine();

            Console.Write("Password [guest]: ");
            var password = Console.ReadLine();

            string sid = null;
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                //altrimenti eseguo il login
                int risultato;
                risultato = client.fn_dologin(username, password, ref sid);

                if (risultato == (int)CodiciESSE3.Ok)
                {
                    Console.WriteLine(string.Format("Login effettuato come {0}", username));
                }
                else
                {
                    Console.WriteLine(string.Format("Si è verificato un errore durante il login: {0}", (CodiciESSE3)risultato));
                    sid = null;
                }
            }

            if (string.IsNullOrEmpty(sid))
            {
                //Se non ho indicato alcun username e password, faccio finta di essere l'utente guest
                //e mi imposto questo session id, che mi verrebbe comunque restituito dall'operazione fn_dologin
                //se passassi guest come username e guest come password.
                Console.WriteLine("Effettuo le richieste al servizio come utente guest.");
                sid = "SESSIONIDGUEST";
            }

            Console.WriteLine();
            Console.Write("Premi un tasto per continuare...");
            Console.ReadLine();

            //ANNO ACCADEMICO
            /*string retrieve = "GET_CURR_AA";
            string param = "tipo_data_rif_cod=DR_CARR";
            string xml = null;
            client.fn_retrieve_xml(sid, retrieve, param, ref xml);
            //Console.WriteLine(xml);*/

            #region CORSI
            string retrieve = "CDS_FACOLTA";
            string param = "aa_id=2014;tipo_corso='L1','L2','LC5','LC6','M1','M2'";
            string xml = null;
            client.fn_retrieve_xml(sid, retrieve, param, ref xml);
            // Console.WriteLine(xml);

            ElencoCorsi corsi = null;
            var serializer = new XmlSerializer(typeof(ElencoCorsi));
            using (var reader = new StringReader(xml))
            {
                corsi = serializer.Deserialize(reader) as ElencoCorsi;
            }
            #endregion

            #region FACOLTA
            retrieve = "FACOLTA";
            param = "";
            xml = null;
            client.fn_retrieve_xml(sid, retrieve, param, ref xml);


            ElencoFacoltà fac = null;
            serializer = new XmlSerializer(typeof(ElencoFacoltà));
            using (var reader = new StringReader(xml))
            {
                fac = serializer.Deserialize(reader) as ElencoFacoltà;
            }
            #endregion


            #region VISUALIZZAZIONE DEI CORSI RAGGRUPPATI PER FACOLTA
            //Raggruppo i corsi che ho ottenuto per IdFacoltà. Il risultato lo trasformo in un dizionario
            //la cui chiave è, appunto, l'IdFacoltà
            var gruppi = corsi.Corsi.GroupBy(corso => corso.IdFacoltà).ToDictionary(gruppo => gruppo.Key);
            foreach (var item in fac.Facoltà)
            {
                //Non stato il nome della facoltà se non ha corsi associati
                if (!gruppi.ContainsKey(item.Id)) continue;
                Console.WriteLine(item.Nome);


                var corsiDiQuestaFacoltà = gruppi[item.Id];
                foreach (var corso in corsiDiQuestaFacoltà)
                {
                    //Stampo il nome del corso con un rientro
                    Console.WriteLine("\t" + corso.Nome);
                }
            }
            #endregion
            Console.ReadLine();

            #region LOGOUT
            client.fn_dologout(sid);
            #endregion
        }

    }
}
