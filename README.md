# FitSammen – Distribueret Holdbookingsystem

Dette var vores hovedprojekt på 3. semester på UCN. Målet var at bygge en skalerbar løsning til fitnesskæder, der kunne håndtere alt fra medlemsbooking til administration.

##  Arkitektur
Vi valgte en 5-lags N-tier arkitektur for at sikre en klar separation af ansvarsområder. Det betyder i praksis, at vores REST API (.NET Core) fungerer som den centrale hjerne, som både vores web- og desktopklienter taler med.

- RESTful API (.NET Core): Vores centrale server, der håndterer al forretningslogik og sikrer, at data serveres ensartet via JSON.
- Webklient (ASP.NET MVC): Udviklet til medlemmerne, så de nemt kan booke og afmelde hold direkte i browseren.
- Desktopklient (WinForms): En dedikeret applikation til personalet, der kræver et hurtigt værktøj til at administrere hold og brugere.
- Data Access Layer (ADO.NET): Vi valgte at arbejde tæt på databasen med ADO.NET for at have fuld kontrol over vores queries og performance.
- Database (MSSQL): Hele databasedelen kører i en Docker-container, hvilket gjorde det nemt for os i gruppen at sikre, at vi alle arbejdede på nøjagtig samme    miljø.

I udviklingsprocessen dykkede vi ned i nogle af de mere komplekse udfordringer ved distribuerede systemer:

- Håndtering af samtidighed (Concurrency): En af vores største prioriteter var at undgå "overbooking". Vi implementerede databasetransaktioner og styrede        isolation-levels for at sikre, at ACID-principperne blev overholdt, selv når mange brugere booker samtidigt.
- Sikkerhed: Vi beskyttede vores API-endpoints med JWT (JSON Web Tokens). Til brugerne implementerede vi hashing og salting af passwords (ud fra BCrypt-         princippet) for at sikre, at persondata aldrig ligger usikret i databasen.
- Testbar kode: For at sikre stabilitet løbende i projektet, skrev vi unit- og integrationstests i xUnit, især omkring vores kritiske booking-logik.

##  Tech Stack
* **Sprog:** C# (.NET)
* **API:** RESTful API
* **Database:** Microsoft SQL Server (MSSQL) via ADO.NET
* **Sikkerhed:** JWT (JSON Web Tokens), Hashing & Salting (BCrypt/PBKDF2-princip)
* **Containere:** Docker
* **Testing:** xUnit til unit- og integrationstest


https://github.com/user-attachments/assets/a99ce3bf-1fe6-434a-a4e1-6aaac359834d

