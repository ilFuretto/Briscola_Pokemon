using System.Net;

Dictionary<string, int> mappa = new Dictionary<string, int>();

// COPPE = tipo Acqua
mappa["Coppe_1"] = 55;  // Golduck
mappa["Coppe_2"] = 60;  // Poliwag
mappa["Coppe_3"] = 54;  // Psyduck
mappa["Coppe_4"] = 72;  // Tentacool
mappa["Coppe_5"] = 73;  // Tentacruel
mappa["Coppe_6"] = 86;  // Seel
mappa["Coppe_7"] = 87;  // Dewgong
mappa["Coppe_8"] = 7;   // Squirtle  (Fante)
mappa["Coppe_9"] = 8;   // Wartortle (Donna)
mappa["Coppe_10"] = 9;   // Blastoise (Re)

// DENARI = tipo Normale
mappa["Denari_1"] = 20;  // Raticate
mappa["Denari_2"] = 133; // Eevee
mappa["Denari_3"] = 19;  // Rattata
mappa["Denari_4"] = 52;  // Meowth
mappa["Denari_5"] = 53;  // Persian
mappa["Denari_6"] = 108; // Lickitung
mappa["Denari_7"] = 143; // Snorlax
mappa["Denari_8"] = 440; // Happiny  (Fante)
mappa["Denari_9"] = 113; // Chansey  (Donna)
mappa["Denari_10"] = 242; // Blissey  (Re)

// BASTONI = tipo Erba
mappa["Bastoni_1"] = 44;  // Gloom
mappa["Bastoni_2"] = 69;  // Bellsprout
mappa["Bastoni_3"] = 43;  // Oddish
mappa["Bastoni_4"] = 70;  // Weepinbell
mappa["Bastoni_5"] = 102; // Exeggcute
mappa["Bastoni_6"] = 103; // Exeggutor
mappa["Bastoni_7"] = 114; // Tangela
mappa["Bastoni_8"] = 1;   // Bulbasaur (Fante)
mappa["Bastoni_9"] = 2;   // Ivysaur   (Donna)
mappa["Bastoni_10"] = 3;   // Venusaur  (Re)

// SPADE = tipo Lotta
mappa["Spade_1"] = 57;  // Primeape
mappa["Spade_2"] = 106; // Hitmonlee
mappa["Spade_3"] = 56;  // Mankey
mappa["Spade_4"] = 107; // Hitmonchan
mappa["Spade_5"] = 62;  // Poliwrath
mappa["Spade_6"] = 236; // Tyrogue
mappa["Spade_7"] = 237; // Hitmontop
mappa["Spade_8"] = 66;  // Machop    (Fante)
mappa["Spade_9"] = 67;  // Machoke   (Donna)
mappa["Spade_10"] = 68;  // Machamp   (Re)

string cartella = "immagini";
Directory.CreateDirectory(cartella);

Console.WriteLine("Download immagini Pokemon in corso...");
Console.WriteLine("Verranno scaricate 40 immagini nella cartella 'immagini'");
Console.WriteLine("");

WebClient web = new WebClient();
int contatore = 0;

foreach (string nomeCarta in mappa.Keys)
{
    int idPokemon = mappa[nomeCarta];
    string url = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/" + idPokemon + ".png";
    string percorsoFile = Path.Combine(cartella, nomeCarta + ".png");

    try
    {
        web.DownloadFile(url, percorsoFile);
        contatore++;
        Console.WriteLine("[" + contatore + "/40] Scaricato: " + nomeCarta + ".png");
    }
    catch (Exception ex)
    {
        Console.WriteLine("ERRORE su " + nomeCarta + ": " + ex.Message);
    }
}

Console.WriteLine("");
Console.WriteLine("Download completato! " + contatore + " immagini scaricate.");
Console.WriteLine("Premi un tasto per chiudere...");
Console.ReadKey();