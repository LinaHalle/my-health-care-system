-- INNEHÅLL: Alla queries 

Uppgift 1: 
-- UPDATE DATA: Uppdatera User (Anette Johansson) från status pending (0) till accepterad (2)
-- SELECT * FROM USERS; visar att Anette har UserID = 8 
USE myhealthcaresystem;

UPDATE Users
SET Status = 2
WHERE UserID= 8;

Uppgift 2: 
-- Radera specifik data 
-- Radera journalentries som har patientID = 6
USE myhealthcaresystem;

DELETE FROM JournalEntries WHERE PatientID = 6;


UPPGIFT 3: 
-- Anvädning av LIMIT och OFFSET
-- visa de 10 senaste(längst bort i tiden) bokningarna i tabellen Appointments, 
-- hoppa över de 2 första 
USE myhealthcaresystem;

SELECT * FROM Appointments
ORDER BY Date DESC
LIMIT 10
OFFSET 2;

Uppgift 4:
-- Användning av DATE och COUNT
-- Räkna antal bokningar per personell och gruppera dem per dag 
USE myhealthcaresystem;

SELECT u.FullName AS Personell, DATE(a.Date) AS Day, COUNT(a.AppointmentID) AS Appointments
FROM Users AS u
INNER JOIN Appointments AS a ON a.PersonellID = u.UserID
GROUP BY Personell, DATE(Date);

Uppgift 5:
-- Fritext sökning LIKE
-- Sök upp alla users som har L någostans i namnet
USE myhealthcaresystem; 

SELECT * FROM Users WHERE FullName LIKE "%l%"
ORDER BY FullName;

Uppgift 6:
-- En join som innefattar minst tre tabeller 
-- visa alla journalanteckningar tillsammans med: patientens namn, författarens namn
-- och vilket sjukhus patienten tillhör
USE myhealthcaresystem;

SELECT p.FullName AS Patient, a.FullName AS Author, h.Name AS Hospital, j.Note
FROM JournalEntries AS j
INNER JOIN Users AS p ON p.UserID = j.PatientID
INNER JOIN Users AS a ON a.UserID = j.AuthorID
INNER JOIN Departmentpatients AS dp ON dp.UserID = j.PatientID
INNER JOIN Departments AS d ON d.DepartmentID = dp.DepartmentID
INNER JOIN Hospitals AS h ON h.HospitalID = d.HospitalID
ORDER BY Patient;

Uppgift 7: 
-- Skapa en view: finns i schema.sql 

Uppgift 8:
-- Rapport som använder HAVING
-- visa antalet bokningar per personell, men bara de som har fler än 2 bokning
USE myhealthcaresystem;

SELECT u.FullName AS Personell, COUNT(a.AppointmentID) AS Bookings
FROM Users AS u
LEFT JOIN Appointments AS a ON a.PersonellID = u.UserID
GROUP BY Personell
HAVING Bookings >2;

Uppgift 9:
-- En Boolesk/CASE
-- visa om en bokning är idag, i framtiden eller redan har passerat, med hjälp av en CASE-sats 
-- som skapar en kolumn som heter BookingStatus
USE myhealthcaresystem;
SELECT u.FullName AS Patient, a.Date, 
CASE
WHEN DATE(a.Date) = CURDATE() THEN 'Today'
WHEN a.Date > NOW() THEN 'Upcoming'
ELSE 'Completed'
END AS BookingStatus
FROM Appointments AS a
INNER JOIN Users as u ON u.UserID = a.PatientID;


Uppgift 10:
-- Stored Procedure
-- gör en stored procedure med att lägga in en appointment
USE myhealthcaresystem;
DELIMITER //

CREATE PROCEDURE AddAppointment (
IN p_PatientID INT,
IN p_PersonellID INT,
IN p_Date DATETIME,
IN p_DurationMinutes INT,
IN p_Status INT
)
BEGIN 
INSERT INTO Appointments(PatientID, PersonellID, Date, DurationMinutes, Status)
VALUES (p_PatientID, p_PersonellID, p_Date, p_DurationMinutes, p_Status);
SELECT 'Appointment successfully created!' AS Message;
END //

DELIMITER ; -- viktigt med mellanslag däremellan annars funkar de inte 
CALL AddAppointment(8, 3, '2025-11-12 15:00:00', 60, 1);
-- Efter booking får jag meddelandet : Appointment successfully created!

DATAMODELL & NORMALISERING 

Motivera tabellindelning 
Vi gjorde ett grupparbete i OOP där vi skapade ett konsolbaserat health care-system. Efter kursens slut tog jag inspiration från det 
och utvecklade en egen version enligt mina egna idéer. Det är det systemet jag har valt att bygga en databas till.
I min kod finns klasserna User, Appointment, Journal, Region, Hospital, Department och Permission och därför valde jag att låta databasen
följa samma struktur. Genom att dela upp informationen i tydliga entiteter blir det lättare att undvika duplicering, hålla datan
konsekvent och skapa flexibla relationer mellan tabellerna.
Varje tabell representerar en egen del av verksamheten exempelvis en användare, en bokning eller en avdelning, vilket gör systemet
enklare att underhålla och utöka. Denna struktur följer också principerna för normalisering och leder till en effektivare och mer skalbar
databas.

Identifiera primary keys och foreign keys 
Varje tabell i databasen har en primärnyckel som unikt identifierar varje rad. För tabeller som representerar enskilda entiteter, såsom
Users, Appointments och Hospitals, används ett ID-fält med AUTO_INCREMENT. Jag har valt att strukturera databasen på så vis för att 
förenkla filtrering, sökbarhet och för att göra systemet flexibelt, så att man kan ändra data i tabellerna men ändå ha samma ID. 
I kopplingstabellerna, till exempel UsersPermission och DepartmentPersonell, används sammansatta nycklar för att undvika dubbletter och
beskriva relationer mellan flera tabeller.

_______________________________________________________________________________________
|TABELL              |PRIMARY KEYS	      | FOREIGN KEYS
_______________________________________________________________________________________
|Users	             |UserID	          |    -
_______________________________________________________________________________________
|Appointments        |AppointmentID       | PatientID, PersonellID (Users(UserID))
_______________________________________________________________________________________
|JournalEntries	     |JournalEntryID	  | PatientID, AuthorID (Users(UserID))
_______________________________________________________________________________________
|Hospitals           |HospitalID	      |    -
_______________________________________________________________________________________
|Departments	     |DepartmentID	      | Hospitals(HospitalID)
_______________________________________________________________________________________
|DepartmentPersonell |DepartmentID, UserID|	Departments(DepartmentID), Users(UserID)
_______________________________________________________________________________________
|DepartmentPatients	 |DepartmentID, UserID|	Departments(DepartmentID), Users(UserID)
_______________________________________________________________________________________
|RegionAdmins	     |Region, UserID      |	Users(UserID)
_______________________________________________________________________________________
|Permissions	     |PermissionID	      |    -
_______________________________________________________________________________________
|UsersPermissions	 |UserID, PermissionID|	Users(UserID), Permissions(PermissionID)
_______________________________________________________________________________________



BESKRIVNING AV RELATIONER
________________________________________________________________________________________________________________________________________
|Relation	                     |Typ	|Beskrivning
________________________________________________________________________________________________________________________________________
|Users <-> Appointments	         |1 - M |En Patient kan ha flera bokningar och en anställd kan vara kopplad till flera bokningar
________________________________________________________________________________________________________________________________________
|Users <-> JournalEntries        |1 - M |En patient kan ha flera journalanteckningar och en författare kan skriva i flera journaler
________________________________________________________________________________________________________________________________________
|Hospitals <-> Departments       |1 - M |Ett sjukhus kan ha flera avdelningar 
________________________________________________________________________________________________________________________________________
|Departments <-> Users(Personell)|M - M |Personal kan arbeta på flera avdelningar och en avdelning kan ha flera anställda
________________________________________________________________________________________________________________________________________
|Departments <-> Users(patient)  |M - M |En patient kan vara kopplad till en eller flera avdelningar, en avdelning kan ha många patienter
________________________________________________________________________________________________________________________________________
|Regions <-> Users(Admins)       |1 - M |En region kan ha flera admins men varje admin tillhör endast en region
________________________________________________________________________________________________________________________________________
|Hospitals <-> Users(Admins)     |1 - M |Ett sjukhus kan ha flera admins men admins är endast på ett sjuhus
________________________________________________________________________________________________________________________________________
|Users <-> Permissions           |M - M |En användare kan ha flera behörigheter och en behörighet kan tilldelas flera användare
________________________________________________________________________________________________________________________________________

Relationerna i databasen är utformade för att spegla verksamhetens logik. En användare kan exempelvis ha flera bokningar och
journalanteckningar medan sjukhs och avdelningar följer en hierarkisk struktur. För att hantera mer komplexa kopplingar som mellan
användare och behörigheter används M:M relationer med kopplingstabeller.

Motivering 3NF
Min databas följer principerna för normalisering upp till 3NF. Det innebär i korthet att informationen är uppdelad på ett sätt som gör
den tydlig, utan att något sparas i onödan. 
Första steget handlar om att varje kolumn bara innehåller ett värde, exempel ett namnfält innehåller bara ett namn, 
Andra steget betyder att varje tabell bara innehåller information som hör ihop med dess primary key, exempelvis tabellen UserPermission
innehåller bara information som rör användare och deras rättigheter. 
Tredje steget handlar om att kolumner inte ska bero på varandra utan bara på tabellens nyckel, Exempelvis i tabellen Appointments beror
alla värden datum, status etc. på själva bokningen, inte på patienten eller personalen.
På det här sättet blir databasen lättare att hålla ordning på, snabbare att söka i och mindre risk att innehålla motsägande information.

Relationer och Kopplingstabeller: 
•	UsersPermission:  kopplar Users <-> Permissions
•	DepartmentPersonell:  kopplar Departments <-> Users(Personell)
•	DepartmentPatients:  kopplar Departments <-> Users(patients)
•	RegionAdmins: kopplar Region <-> Users(admins)
•	HospitalAdmins: kopplar Hospital <-> Users(admins)
Varje tabell fungerar som en kopplingstabell mellan två entiteter och hanterar M:M-relationen.


En rapport med HAVING där grupperade resultat filtreras
-- Kollar avdelningar som endast har 0-1 personal så admin vet vilka avdelningar som behöver uppbackning 
USE myhealthcaresystem;
SELECT d.Name AS Department, h.Name AS Hospital, COUNT(dp.UserID) AS NumPersonell
FROM DepartmentPersonell AS dp
INNER JOIN Departments AS d ON dp.DepartmentID = d.DepartmentID
INNER JOIN Hospitals AS h ON d.HospitalID = h.HospitalID
GROUP BY d.DepartmentID
HAVING COUNT(dp.UserID) <= 1;
