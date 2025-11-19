RAPPORT - MY HEALTH CARE SYSTEM

Jag har valt att skapa en databas för mitt konsolbaserade Health Care System som jag kodat i C#. Systemet omfattas av etniteter såsom
användare, bokningar, journalanteckningar, regioner, sjukhus, avdelningar samt permissions som kontrollerar vad de olika användarna kan
göra i applikationen. Alla användare tilldelas en roll, Admin, patient eller personal. Rollerna används endast för filtrering av
användare men de mesta grundas i vilka permissions de olika användarna har och det är det som ger dig olika behörigheter inte rollerna.
Jag har valt att utforma det på detta sätt för att skapa felxibilitet i systemet så att även en läkare kan söka vård eller att någon som
är patient kan bli anställd. Informationen som databasen behöver hantera är användarinformation, journalantecknignar, bokningar samt
kopplingarna mellan patient/personal– bokningar/journaler, platser – användare mm.

I databasen har jag tabellerna:
Users – som sparar användare med UserID(PK), SSN, PasswordHash, Fullname, Status(approved, pending, denied), Role(Patient, Personell,
Admin).
Appointments – som sparar bokningar med AppointmentID(PK), PatientID(FK), PersonellID(FK), Date, DurationMinutes(default 60 min), Status
(approved, pending, denied).
JournalEntries – Som sparar Journalanteckningar med JournalEntryID(PK), PatientID(FK), AuthorID(FK), Date, Note.
Regions – som sparar regioner med RegionID(PK), Name.
Hospitals – som sparar sjukhus inom regionerna med HospitalID(PK), Name, RegionID(FK).
Departments – som sparar avdelningarna på sjukhusen med DepartmentID(PK), Name, HospitalID(FK).
Permissions – som sparar behörigheter med PermissionID och Name

Kopplingstabeller:
RegionAdmins som håller koll på vilken användare som är Admin för en hel region genom RegionID(PK) och UserID(PK).
HospitalAdmins som tilldelar admins till sjukhus genom HospitalID(PK), UserID(PK).
DepartmentPatients och DepartmentPersonell tilldelar patienter och personal till avdelningar på sjukhusen genom HospitalID(PK) och UserID
(PK).
UsersPermissions som sparar behörigheterna för användarna genom UserID(PK) och PermissionID(PK).
Relationerna i databasen är utformade för att spegla verksamhetens logik. En användare kan exempelvis ha flera bokningar och
journalanteckningar (1:M) medan sjukhs och avdelningar följer en hierarkisk struktur. För att hantera mer komplexa kopplingar som mellan
användare och behörigheter används M : M relationer med kopplingstabeller.

Min databas följer principerna för normalisering upp till 3NF. Det innebär i korthet att informationen är uppdelad på ett sätt som gör
den tydlig, utan att något sparas i onödan.
Första steget handlar om att varje kolumn bara innehåller ett värde, exempel ett namnfält innehåller bara ett namn,
Andra steget betyder att varje tabell bara innehåller information som hör ihop med dess primary key, exempelvis tabellen UserPermission
innehåller bara information som rör användare och deras rättigheter.
Tredje steget handlar om att kolumner inte ska bero på varandra utan bara på tabellens nyckel, Exempelvis i tabellen Appointments beror
alla värden (datum, status osv.) på själva bokningen, inte på patienten eller personalen.
På det här sättet blir databasen lättare att hålla ordning på, snabbare att söka i och mindre risk att innehålla motsägande information.

I mina centrala kolumner har jag valt att alla har en INT PRIMARY KEY, detta för att undvika duplicering och för att enklare göra
kopplingtabeller där jag endast använder deras ID. Jag har valt att lägga till namn för det mesta för att skapa en tydlighet så man
håller koll på alla id:s och istället kan söka på namn på exempelvis användare, regioner och avdelningar. Jag loggar Journalanteckningar
och bokningar med date så man kan filtera på datum och göra de enklare för schemaläggning, ha koll på arbetsbelastning och loggar.
I varje tabell har jag använt mig av NOT NULL på alla VARCHAR värden för att jag vill att all data ska vara ifylld för att undvika att
det saknas information om en användare eller i en bokning. För personnummer (SSN) är den unique för att ge systemet verklighetsbaserat då
alla personnummer är unika. Jag referar även alla Foreign keys till primary keys där de behövs för att göra kopplingarna konsekventa och
tydliga.

Sammanfattningsvis har arbetet med databasen gett mig en tydlig och praktisk förståelse för kursens innehåll, särskilt normalisering och
relationsbyggande. Genom att tillämpa dessa principer har jag kunnat skapa en stabil, flexibel och logisk databas för mitt Health Care
System. Jag har lärt mig mycket under kursens gång, både teoretiskt och genom att utforma ett verklighetsnära system. Nästa steg blir att
koppla samman min applikation med databasen och få hela lösningen att fungera som en helhet.
