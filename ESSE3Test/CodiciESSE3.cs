using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESSE3Test
{
    public enum CodiciESSE3 : int
    {
        Ok = 1,
        Errore = -1,
        AutenticazioneFallita = 1003,
        CreazioneComponenteFallita = 1004,
        ConnessioneAlDbFallita = 1007,
        UtenteDisabilitato = 1110,
        PasswordDaImpostare = 1112,
        UserIdNonValido = 1116,
        GruppoUtenteNonAbilitato = 1119,
        ErroreLDAP = 1126,
        PasswordScaduta = 1130
    }
}
