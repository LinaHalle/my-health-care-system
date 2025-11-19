-- INNEHÅLL: CREATE TABLE, datatyper, keys, views, index

CREATE DATABASE MyHealthCareSystem;
USE MyHealthCareSystem;

-- Tabell: Users
CREATE TABLE Users (
    UserID INT PRIMARY KEY AUTO_INCREMENT,
    SSN VARCHAR(12) NOT NULL UNIQUE, --Unique för att SSN är unikt. 
    PasswordHash NVARCHAR(100) NOT NULL, -- Krypterar i C#, bra för säkerhet. 
    FullName NVARCHAR(50) NOT NULL,
    Status INT NOT NULL,
    Role INT NOT NULL
    );
 
 
-- Tabell: Appointments
CREATE TABLE Appointments (
    AppointmentID INT PRIMARY KEY AUTO_INCREMENT,
    PatientID INT NOT NULL,
    PersonellID INT NOT NULL,
    Date DATETIME NOT NULL,
    DurationMinutes INT NOT NULL DEFAULT 60,
    Status INT NOT NULL,
    FOREIGN KEY (PatientID) REFERENCES Users(UserID),
    FOREIGN KEY (PersonellID) REFERENCES Users(UserID)
);

-- Tabell: JournalEntry
CREATE TABLE JournalEntries (
    JournalEntryID INT PRIMARY KEY AUTO_INCREMENT,
    PatientID INT NOT NULL,
    AuthorID INT NOT NULL,
    Date DATETIME  NOT NULL,
    Note TEXT NOT NULL,
    FOREIGN KEY (PatientID) REFERENCES Users(UserID),
    FOREIGN KEY (AuthorID) REFERENCES Users(UserID)
);

-- Tabell: Regions
CREATE TABLE Regions (
RegionID INT PRIMARY KEY, 
RegionName VARCHAR(100) NOT NULL
);

-- Tabell: Hospitals
CREATE TABLE Hospitals (
    HospitalID INT PRIMARY KEY AUTO_INCREMENT,
    Name NVARCHAR(100) NOT NULL,
    Region INT NOT NULL
FOREIGN KEY(Region) REFERENCES Regions(RegionID)
);

-- Tabell: Departments
CREATE TABLE Departments (
   DepartmentID INT PRIMARY KEY AUTO_INCREMENT,
    Name NVARCHAR(100) NOT NULL,
    HospitalID INT NOT NULL,
    FOREIGN KEY (HospitalID) REFERENCES Hospitals(HospitalID) 
    );

-- Tabell: Departments - Users M:M only about personell, avdelningarna har flera anställda och anställda kan vara tilldelade flera avdelningar
CREATE TABLE DepartmentPersonell (
    DepartmentID INT NOT NULL,
    UserID INT NOT NULL,
    PRIMARY KEY (DepartmentID, UserID),
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

-- Tabell: Departments - Users M:M only about patients, avdelningarna kan ha många patienter och patienterna kan vara på flera avdelnignar. 
CREATE TABLE DepartmentPatients (
	 DepartmentID INT NOT NULL,
    UserID INT NOT NULL,
    PRIMARY KEY (DepartmentID, UserID),
    FOREIGN KEY (DepartmentID) REFERENCES Departments(DepartmentID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
    
-- Tabell: Region - Users 1:M, 1 region med flera admins 
CREATE TABLE RegionAdmins (
	Region INT NOT NULL,
    UserID INT NOT NULL, 
    PRIMARY KEY (Region, UserID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
    );

-- Tabell: Hospital - Users 1:M, Ett sjukhus men flera admins
CREATE TABLE HospitalAdmins (
    HospitalID INT NOT NULL,
    UserID INT NOT NULL,
    PRIMARY KEY (HospitalID, UserID),
    FOREIGN KEY (HospitalID) REFERENCES Hospitals(HospitalID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);
    
-- Tabell: Permissions 
CREATE TABLE Permissions (
PermissionID INT PRIMARY KEY AUTO_INCREMENT,
Name NVARCHAR(50) NOT NULL UNIQUE
);

-- Tabell: UsersPermission M:M
CREATE TABLE UserPermissions (
UserID INT NOT NULL,
PermissionID INT NOT NULL,
PRIMARY KEY (UserID, PermissionID),
FOREIGN KEY (UserID) REFERENCES Users(UserID),
FOREIGN KEY (PermissionID) REFERENCES Permissions(PermissionID)
);

-------------- RAPPORTVYER ---------------
-- Visar hur många bokningar varje personal har varje dag, bra för schemaläggning och arbetsbelastning 
CREATE VIEW AppointmentsPersonell AS
SELECT u.FullName AS Personell, DATE(a.Date) AS Day, COUNT(a.AppointmentID) AS Bookings
FROM Appointments AS a
INNER JOIN Users as u ON u.UserID = a.PersonellID
GROUP BY u.FullName, DATE(a.Date);

-- En view som visar alla bokningar som är pending, bra för filtering när personal ska godkänna/neka 
CREATE VIEW PendingAppointments AS
SELECT p.FullName AS Patient, s.FullName AS Personell, DATE(a.Date) AS AppointmentDate
FROM Appointments AS a
INNER JOIN Users AS p ON p.UserID = PatientID
INNER JOIN Users AS s ON s.UserID = PersonellID
WHERE a.Status = 0;


--------------- FÖRENKLADE VYER ---------------
-- Kopplar sjukhus, avdelningar och ansvarig personal. Bra för att kolla personaltäckning
CREATE VIEW HospitalDepartmentPersonell AS
SELECT h.Name AS Hospital, d.Name AS Department, u.FullName AS Personell
FROM Departments as d
LEFT JOIN Hospitals AS h ON h.HospitalID = d.HospitalID
LEFT JOIN DepartmentPersonell AS dp ON dp.DepartmentID = d.DepartmentID
LEFT JOIN Users AS u ON u.UserID = dp.UserID
ORDER BY h.Name;

-- Visar hur många patienter som finns på varje avdelning och sjuhus, bra om man måste skicka patienter till annat sjukhus eller avdelning 
CREATE VIEW PatientsCountDepartmentHospital AS
SELECT h.Name AS Hospital, d.Name AS Department, COUNT(u.UserID) AS patientcount
FROM Hospitals AS h
LEFT JOIN Departments AS d ON d.HospitalID = h.HospitalID
LEFT JOIN DepartmentPatients AS dp ON dp.DepartmentID = d.DepartmentID
LEFT JOIN Users AS u ON u.UserID = dp.UserID
GROUP BY h.HospitalID, d.DepartmentID
ORDER BY h.Name, d.Name;

-- Visar namn på patienter som har journalanteckningar, författare, från vilket sjukhus och anteckningen. 
CREATE VIEW JournalWithHospitals AS
 SELECT p.FullName AS Patient, a.FullName AS Author, h.Name AS Hospital, j.Note
 FROM JournalEntries AS j
 INNER JOIN Users AS p ON p.UserID = j.PatientID
 INNER JOIN Users AS a ON a.UserID = j.AuthorID
 INNER JOIN Departmentpatients AS dp ON dp.UserID = j.PatientID
 INNER JOIN Departments AS d ON d.DepartmentID = dp.DepartmentID
 INNER JOIN Hospitals AS h ON h.HospitalID = d.HospitalID
 ORDER BY Patient;

-- Kallar på alla views via denna metoden
SELECT * FROM VIEW_Name;

-------------- INDEXERING --------------------
-- Som ett uppslagsregister i en telefonbok. 

-- Denna kolumn indexeras eftersom vi ofta söker och sorterar bokningar efter datum.
CREATE INDEX idx_appointments_date ON Appointments(Date);

-- För att möjliggöra en snabbare sätt för en läkare att se alla sina bokningar eller för admin att göra schema 
CREATE INDEX idx_appointments_personellID ON Appointments(PersonellID);






