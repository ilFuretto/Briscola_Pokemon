===============================================================================
BRISCOLA POKEMON - DOCUMENTAZIONE DI PROGETTO
===============================================================================

Variante del gioco di carte Briscola con artwork Pokémon, realizzata in C# con
architettura client-server per il gioco in rete locale.


===============================================================================
REGOLAMENTO
===============================================================================

-------------------------------------------------------------------------------
OBIETTIVO
-------------------------------------------------------------------------------

Raccogliere più punti possibili aggiudicandosi le mani.
Il totale dei punti in gioco è 120.
Vince chi supera i 60 punti a fine partita.

-------------------------------------------------------------------------------
IL MAZZO
-------------------------------------------------------------------------------

Il mazzo è composto da 40 carte, suddivise in 4 semi da 10 carte ciascuno:

+----------+----------------+-----------+
| Seme     | Tipo Pokémon   | Colore    |
+----------+----------------+-----------+
| Coppe    | Acqua          | Blu       |
| Denari   | Normale        | Arancione |
| Bastoni  | Erba           | Verde     |
| Spade    | Lotta          | Rosso     |
+----------+----------------+-----------+

-------------------------------------------------------------------------------
VALORE DELLE CARTE
-------------------------------------------------------------------------------

Ogni carta ha un valore in punti secondo la tabella classica della Briscola:

+------------------+---------+-------+
| Carta            | Simbolo | Punti |
+------------------+---------+-------+
| Asso             | 1       | 11    |
| Tre              | 3       | 10    |
| Re               | K       | 4     |
| Donna            | Q       | 3     |
| Fante            | J       | 2     |
| 2, 4, 5, 6, 7    | -       | 0     |
+------------------+---------+-------+

-------------------------------------------------------------------------------
TEMA POKEMON
-------------------------------------------------------------------------------

Le carte con simbolo J, Q, K rappresentano linee evolutive a 3 stadi.
Asso e Tre rappresentano linee evolutive a 2 stadi.

+----------+-------------+-------------------+-------------+-------------+-------------------+
| Seme     | Tre (base)  | Asso (evoluzione) | J (stadio1) | Q (stadio2) | K (stadio finale) |
+----------+-------------+-------------------+-------------+-------------+-------------------+
| Coppe    | Psyduck     | Golduck           | Squirtle    | Wartortle   | Blastoise         |
| Denari   | Rattata     | Raticate          | Happiny     | Chansey     | Blissey           |
| Bastoni  | Oddish      | Gloom             | Bulbasaur   | Ivysaur     | Venusaur          |
| Spade    | Mankey      | Primeape          | Machop      | Machoke     | Machamp           |
+----------+-------------+-------------------+-------------+-------------+-------------------+

-------------------------------------------------------------------------------
SVOLGIMENTO DELLA PARTITA
-------------------------------------------------------------------------------

1) All'inizio vengono distribuite 3 carte a testa

2) Viene girata una carta a indicare il seme di briscola
   Questa carta viene rimessa in fondo al mazzo

3) Il Giocatore 1 apre la prima mano

4) A turno ogni giocatore gioca una carta dalla propria mano

5) Dopo ogni mano entrambi i giocatori pescano una carta dal mazzo
   (finché ci sono carte)

6) Chi vince la mano apre la successiva

-------------------------------------------------------------------------------
REGOLE PER VINCERE UNA MANO
-------------------------------------------------------------------------------

[1] Se viene giocata una briscola contro una carta di seme diverso
    -> Vince sempre la briscola

[2] Se entrambe le carte sono briscola
    -> Vince quella con il valore in punti più alto

[3] Se le carte hanno lo stesso seme
    -> Vince quella con il valore in punti più alto
    -> A parità di punti vince quella col numero più alto

[4] Se le carte hanno seme diverso e nessuna è briscola
    -> Vince chi ha aperto la mano

-------------------------------------------------------------------------------
FINE PARTITA
-------------------------------------------------------------------------------

La partita termina quando entrambi i giocatori hanno esaurito le carte in mano
e il mazzo è finito.

Viene mostrato un riepilogo con:
- Punti del Giocatore 1
- Punti del Giocatore 2
- Nome del vincitore


===============================================================================
CREDITI
===============================================================================

- Sprite Pokémon: PokéAPI Sprites (https://github.com/PokeAPI/sprites)
  -> gratuiti e open source

<<<<<<< HEAD
- Progetto realizzato in C# con .NET 8.0 e WinForms
=======
- Progetto realizzato in C# con .NET 8.0 e WinForms
>>>>>>> 232792b (miglioramenti grafici)
