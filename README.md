# MovieAPIBackend

Lösningen går ut på att vi använder denna backend för att koppla upp oss mot The Movie DataBase (TMDB) för att hämta filmdata.
Vi skapar en singleton/cache som innehåller regionsspecifika översättningar för olika genres. Eftersom TMDB-förfrågningar ger oss id'n och vi kan vilja skicka tillbaka genrernas namn istället för endast ID'n. Detta besparar oss från att göra ett API-call per anrop till vår backend.
Controllern utnyttjar cachen via dependency injection (DI). Varje anrop mot TMDB har en kort kommentar för att auto-generera beskrivningar i Swagger.
Eftersom controllers instansieras vid behov så injicerar vi även Configuration (appsettings.json) så att vi kan komma åt t.ex. API-key mot TMDB, detta gör att vi kan ändra nyckel under drift utan att behöva ta ned tjänsten.

För att testa API:et, kör projektet i debugläge och gå mot "/swagger/index.html" för att komma åt Swagger UI.
