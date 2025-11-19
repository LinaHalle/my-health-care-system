-- INNEHÅLL: ALL INSERT DATA
USE myhealthcaresystem;

-- SSN är UNIQUE
-- Status: 0=pending, 1=denied, 2=accepted
-- Role: 0=patient, 1=personell, 2=admin
INSERT INTO Users (SSN, PasswordHash, FullName, Status, Role) VALUES ('0000000000', '000', 'Lina Hallergren', 2, 2),
('1111111111', '111', 'David Puscas', 2, 1),
('5555555555', '555', 'Joel Sjövall', 2, 1),
('2222222222', '222', 'Gustav Fransson', 2, 0),
('3333333333', '333', 'Harris Novljakovic', 2, 0),
('4444444444', '444', 'Zomi Novljakovic', 2, 0),
('6666666666', '666', 'Anette Johansson', 2, 0),
('1000000001', 'adminpass', 'Anna Svensson', 2, 2),
('1000000002', 'adminpass', 'Erik Johansson', 2, 2),
('1000000003', 'adminpass', 'Maria Lindberg', 2, 2),
('1000000004', 'adminpass', 'Peter Nilsson', 2, 2),
('1000000005', 'adminpass', 'Sofia Karlsson', 2, 2),
('1000000006', 'adminpass', 'Lars Andersson', 2, 2),
('2000000001', 'adminpass', 'Karin Berg', 2, 2),
('2000000002', 'adminpass', 'Johan Ek', 2, 2),
('2000000003', 'adminpass', 'Eva Holm', 2, 2),
('2000000004', 'adminpass', 'Mats Lind', 2, 2),
('2000000005', 'adminpass', 'Nina Wall', 2, 2),
('2000000006', 'adminpass', 'Oskar Lund', 2, 2),
('3000000003', 'docpass', 'Emma Olsson', 2, 1),
('3000000004', 'docpass', 'Anna Nilsson', 2, 1),
('3000000005', 'docpass', 'Fredrik Lund', 2, 1),
('4000000001', 'pass123', 'Alice Berg', 2, 0),
('4000000002', 'pass123', 'Björn Eriksson', 2, 0),
('4000000003', 'pass123', 'Carina Holm', 2, 0),
('4000000004', 'pass123', 'Daniel Olsson', 2, 0),
('4000000005', 'pass123', 'Elin Svensson', 2, 0),
('4000000006', 'pass123', 'Fredrik Lind', 0, 0),
('4000000007', 'pass123', 'Gabriella Wall', 2, 0),
('4000000008', 'pass123', 'Henrik Lund', 1, 0),
('4000000009', 'pass123', 'Isabelle Nilsson', 2, 0),
('4000000010', 'pass123', 'Jonas Karlsson', 2, 0),
('4000000011', 'pass123', 'Katarina Andersson', 2, 0),
('4000000012', 'pass123', 'Lars Bergström', 2, 0),
('4000000013', 'pass123', 'Maria Olsson', 2, 0),
('4000000014', 'pass123', 'Niklas Svensson', 2, 0),
('4000000015', 'pass123', 'Olivia Lind', 0, 0),
('4000000016', 'pass123', 'Peter Holm', 2, 0),
('4000000017', 'pass123', 'Qasim Wall', 1, 0),
('4000000018', 'pass123', 'Rebecca Lund', 2, 0),
('4000000019', 'pass123', 'Stefan Nilsson', 2, 0),
('4000000020', 'pass123', 'Therese Karlsson', 2, 0),
('4000000021', 'pass123', 'Ulf Andersson', 2, 0),
('4000000022', 'pass123', 'Viktoria Berg', 2, 0),
('4000000023', 'pass123', 'William Olsson', 2, 0),
('4000000024', 'pass123', 'Xenia Svensson', 2, 0),
('4000000025', 'pass123', 'Yusuf Lind', 1, 0),
('4000000026', 'pass123', 'Zandra Holm', 2, 0),
('4000000027', 'pass123', 'Åke Wall', 2, 0),
('4000000028', 'pass123', 'Älva Lund', 2, 0),
('4000000029', 'pass123', 'Örjan Nilsson', 0, 0),
('4000000030', 'pass123', 'Adam Karlsson', 2, 0),
('5000000004', 'docpass', 'Anna Nilsson', 2, 1),
('5000000005', 'docpass', 'Fredrik Lund', 2, 1),
('5000000006', 'docpass', 'Cecilia Berg', 2, 1),
('5000000007', 'docpass', 'Magnus Svensson', 2, 1),
('5000000008', 'docpass', 'Sofia Lind', 2, 1),
('5000000009', 'docpass', 'Karin Holm', 2, 1),
('5000000010', 'docpass', 'Peter Wall', 2, 1),
('5000000011', 'docpass', 'Eva Lund', 2, 1),
('5000000012', 'docpass', 'Mats Nilsson', 2, 1),
('5000000013', 'docpass', 'Linda Karlsson', 2, 1),
('5000000014', 'docpass', 'Jonas Andersson', 2, 1),
('5000000015', 'docpass', 'Sara Berg', 2, 1),
('5000000016', 'docpass', 'Hanna Olsson', 2, 1),
('5000000017', 'docpass', 'Niklas Svensson', 2, 1),
('5000000018', 'docpass', 'Emma Lind', 2, 1),
('5000000019', 'docpass', 'Oskar Holm', 2, 1),
('5000000020', 'docpass', 'Lina Wall', 2, 1);

-- INSERT DATA: Permissions 
-- Alla permissions får ett PermissionID 1-18 
INSERT INTO Permissions (Name) VALUES
('AddUser'), --1
('AddPermission'), --2
('AddLocation'), --3
('AssignAdminRegion'), --4
('ViewPermissions'), --5
('ViewJournal'), --6
('WriteJournal'), --7
('ViewAppointments'), --8
('AcceptOrDenyUser'), --9
('ViewMyJournal'), --10
('RequestAppointment'), --11
('RegisterAppointment'), --12
('AcceptOrDenyAppointments'), --13
('ModifyAppointments'), --14
('ViewMySchedule'), --15
('ViewHospitalSchedule'), --16
('Logout'), --17
('Quit'); --18


-- INSERT DATA: UserPermissions
-- Endast valt att ge permission till en admin, en personell och en patient pga tidsåtkomst, kommer skala upp vid senare tillfälle
-- Lina Admin
INSERT INTO UserPermissions (UserID, PermissionID) VALUES
(1, 1), -- Adduser
(1, 2), -- Addpermission
(1, 3), -- AddLocation
(1, 4), -- AssignAdminRegion
(1, 5), -- ViewPermission
(1, 9), -- AcceptOrDenyUser
(1, 16), -- ViewHospitalSchedule
(1, 17), -- Logout
(1, 18); -- Quit

-- David Staff
INSERT INTO UserPermissions (UserID, PermissionID) VALUES
(2, 13), -- AcceptOrDenyAppointments
(2, 12), -- RegisterAppointment
(2, 14), -- ModifyAppointments
(2, 7), -- WriteJournal
(2, 6), -- ViewJournal
(2, 8), -- ViewAppointments
(2, 15), -- ViewMySchedule
(2, 16), --ViewHospitalSchedule
(2, 17), -- Logout
(2, 18); -- Quit

-- Gustav Patient
INSERT INTO UserPermissions (UserID, PermissionID) VALUES
(4, 11), -- RequestAppointment
(4, 10), -- ViewMyJournal
(4, 8), -- ViewAppointments
(4, 17), -- Logout
(4, 18); -- Quit

-- INSERT DATA: Regions
INSERT INTO Regions (RegionID, RegionName)
VALUES 
(0, 'Region Skåne'),
(1, 'Region Uppsala'),
(2, 'Region Norbotten'),
(3, 'Region Västerbotten'),
(4, 'Region Stockholm'),
(5, 'Region Halland');

-- INSERT DATA: Hospitals
-- Regions(RegionID) refererar till Region 
INSERT INTO Hospitals (Name, Region) VALUES
('Skånes universitetssjukhus', 0),
('Akademiska sjukhuset Uppsala', 1),
('Sunderby sjukhus', 2),
('Lycksele lasarett', 3),
('Danderyds sjukhus', 4),
('Kungsbacka sjukhus', 5);

-- INSERT DATA: Departments
INSERT INTO Departments (Name, HospitalID) VALUES
('Emergency Department', 1),
('General Medicine', 1),
('General Surgery', 1),
('Intensive Care Unit (ICU)', 1),
('Radiology', 1),
('Emergency Department', 2),
('General Medicine', 2),
('General Surgery', 2),
('Intensive Care Unit (ICU)', 2),
('Radiology', 2),
('Emergency Department', 3),
('General Medicine', 3),
('General Surgery', 3),
('Intensive Care Unit (ICU)', 3),
('Radiology', 3),
('Emergency Department', 4),
('General Medicine', 4),
('General Surgery', 4),
('Intensive Care Unit (ICU)', 4),
('Radiology', 4),
('Emergency Department', 5),
('General Medicine', 5),
('General Surgery', 5),
('Intensive Care Unit (ICU)', 5),
('Radiology', 5),
('General Medicine', 6),
('General Surgery', 6),
('Intensive Care Unit (ICU)', 6),
('Radiology', 6);

INSERT INTO RegionAdmins (Region, UserID) VALUES
-- 0 = skåne, 1 = uppsala, 2 = Norrbotten, 3 = Västerbotten, 4 = Stockholm, 5 = Halland 
(0, 1), 
(0, 9),
(1, 10),
(2, 12), 
(3, 13),
(4, 14),
(5, 15);

INSERT INTO HospitalAdmins (HospitalID, UserID) VALUES
-- 1 = Skånes universitetssjukhus, 2 = Akademiska sjukhsuet uppsala, 3 = Sunderby sjuhus, 4 = Lycksele lasarett, 5 = Danderyds sjukhus, 6 = Kungsbacka sjukhus
(1, 11), 
(2, 16),
(3, 17),
(4, 18),
(5, 19),
(6, 20);

INSERT INTO DepartmentPersonell(DepartmentID, UserID) VALUES
(1, 2),
(1, 23),
(1, 24),
(2, 3),   -- Skånes universitetssjukhus
(2, 25)
(7, 2),
(8, 24),                 
(9, 25),				
(10, 23),					
(11, 3),					
							
(12, 57),					
(13, 57),  -- Akademiska sjukhuset uppsala 
(14, 58),					
(15, 59),							
(16, 59),

(17, 60),						
(18, 61),  -- Sunderby sjukhus 
(19, 61),						
(20, 62),
(21, 60),

(37, 63),
(38, 63),  -- Lycksele Lasarett
(39, 64),
(40, 64),
(41, 65),

(42, 66),
(42, 67),
(43, 68),
(44, 68),  -- Danderyds sjukhus 
(45, 66),
(45, 69),
(46, 70),

(47, 71),
(48, 72), -- Kungsbacka sjukhus
(49, 73),
(50, 71);

INSERT INTO DepartmentPatients (DepartmentID, UserID) VALUES
-- Skånes Universitetssjukhus 
(10, 4), (10, 5), (10, 6), (10, 8),
(8, 26), (8, 27),

-- Akademiska sjukhuset uppsala 
(15, 28), (15, 29), (15, 30), (15, 31),
(13, 32), (13, 33),

-- Sunderby sjukhus
(20, 34), (20, 35), (20, 36), (20, 37),
(18, 38), (18, 39),

-- Lycksele Lasarett
(40, 40), (40, 41), (40, 42), (40, 43),
(38, 44), (38, 45),

-- Danderyds sjukhus 
(45, 46), (45, 47), (45, 48), (45, 49),
(43, 50),

-- Kungsbacka sjukhus 
(49, 51), (49, 52), (49, 53), (49, 54),
(47, 55);

INSERT INTO Appointments (PatientID, PersonellID, Date, DurationMinutes, Status) VALUES
(4, 2, '2025-11-12 10:00', 60, 0),
(5, 3, '2025-11-13 14:00', 60, 2),
(6, 2, '2025-11-14 09:00', 60, 1),
(4, 2, '2025-11-12 09:00', 60, 1),
(5, 2, '2025-11-12 10:00', 60, 0),
(6, 3, '2025-11-13 13:00', 60, 1),
(8, 3, '2025-11-14 15:00', 60, 2),
(8, 3, '2025-12-12 15:00', 60, 2),
(4, 3, '2025-11-15 09:00', 60, 1),
(5, 2, '2025-11-16 11:00', 60, 0),
(4, 23, '2025-11-18 10:00', 60, 1),
(5, 3, '2025-11-18 11:00', 60, 1),  -- Bokningar Skånes universitetssjukhus
(6, 2, '2025-11-20 08:00', 60, 0),
(8, 24, '2025-11-21 09:00', 60, 1),

(28, 57, '2025-11-21 14:00', 60, 1),
(29, 58, '2025-11-24 14:00', 60, 2),
(29, 58, '2025-11-25 08:00', 60, 1),  -- Bokningar Akademiska uppsala 
(30, 57, '2025-11-25 08:00', 60, 1),

(34, 60, '2025-11-26 10:00', 60, 0),
(36, 60, '2025-11-27 11:00', 60, 1),  -- Bokningar Sunderbyn
(39, 62, '2025-11-27 13:00', 60, 1),

(40, 63, '2025-11-27 15:00', 60, 2),
(41, 63, '2025-11-29 13:00', 60, 1),  -- Bokningar Lycksele Lasarett
(45, 63, '2025-12-01 08:00', 60, 1),

(46, 67, '2025-12-01 09:00', 60, 0),
(48, 69, '2025-12-02 09:00', 60, 1),
(49, 70, '2025-12-03 10:00', 60, 1),   -- Danderyds Sjukhus 
(50, 70, '2025-12-04 11:00', 60, 2),

(52, 73, '2025-12-04 07:00', 60, 1),
(53, 72, '2025-12-04 09:00', 60, 1),   -- Kungsbacka Sjukhus 
(53, 72, '2025-12-11 15:00', 60, 0);

INSERT INTO JournalEntries (PatientId, AuthorID, Date, Note) VALUES
(6, 2, '2025-11-03 09:45', 'Initial consultation')
(5, 3, '2025-11-02 11:30', 'Follop-up on test results'),
(4, 2, '2025-11-01 10:15', 'Routine checkup'),
(4, 23, '2025-11-17 15:00', 'Initial consultation'),
(8, 24, '2025-11-14 13:45', 'Did MRA-scan waiting for results'),               -- Skånes universitetssjuhus
(26, 23, '2025-11-11 15:00', 'Fracture in rib, had pain while coughing'),

(28, 57, '2025-11-14 15:00', 'Check up on test-results'),
(28, 57, '2025-10-08 10:15', 'Initial consultation'),                             -- Akademiska 
(33, 59, '2025-10-09 15:30', 'Cancer testing negative, check up set 1 year'),

(34, 60, '2025-11-01 08:45', 'MRA-scan, negative results'),
(39, 62, '2025-11-03 15:20', 'Appendicitis, emergency surgery'),           -- Synderbyn 

(4, 64, '2025-09-20 11:50', 'Routine check-up, recovery good'),
(4, 65, '2025-09-17 09:00', 'Pain in knee, gave painkillers, calls if worse'),    -- Lycksele lasarett

(47, 69, '2025-10-16 15:15', 'Removing stitches, healed wound'),
(47, 69, '2025-09-22 14:00', 'Came in with big wound L.arm, 7 stitches'),         -- danderyds 

(51, 72, '2025-11-22 13:00', 'Broken L.arm, Cast and bandage for 7 weeks'),
(53, 73, '2025-09-23 14:15', 'Open wound on hand, 3 stitches, remove in 2 weeks');     -- Kungsbackas 