USE [Master]
GO

CREATE DATABASE MVCApplicationFramework
GO

USE MVCApplicationFramework
GO

CREATE TABLE [dbo].[User]
(
	ID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	LoginName VARCHAR(50) NOT NULL UNIQUE,
	[Password] VARCHAR(255) NOT NULL,
	FirstName VARCHAR(50) NOT NULL,
	MiddleName VARCHAR(50),
	LastName VARCHAR(50) NOT NULL,
	ContactNumber VARCHAR(15) NOT NULL,
	CreatedDate DATETIME DEFAULT(GETDATE()) NOT NULL,
	CreatedBy INT NOT NULL,
	UpdatedDate DATETIME,
	UpdatedBy INT
)
GO

CREATE TABLE [dbo].[Role]
(
	ID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Name] VARCHAR(50) NOT NULL UNIQUE,
	CreatedDate DATETIME DEFAULT(GETDATE()) NOT NULL,
	CreatedBy INT NOT NULL,
	UpdatedDate DATETIME,
	UpdatedBy INT
)
GO
CREATE TABLE [dbo].[UserRole]
(
	ID INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	UserID INT REFERENCES [dbo].[User](ID) NOT NULL,
	RoleID INT REFERENCES [dbo].[Role](ID) NOT NULL,
	CreatedDate DATETIME DEFAULT(GETDATE()) NOT NULL,
	CreatedBy INT NOT NULL,
	UpdatedDate DATETIME,
	UpdatedBy INT,
	CONSTRAINT uk_UserRole_UserID_RoleID UNIQUE (UserID, RoleID)
)

GO
INSERT INTO [dbo].[User]
(
	LoginName, 
	[Password], 
	FirstName, 
	LastName, 
	ContactNumber,
	CreatedBy
)
VALUES
('adarsh.b@codearray.tech', '12345678', 'Adarsh', 'Bajpai', '9022775974', 0),
('nbirajadar5@gmail.com', '12345678','Nilesh', 'Yashvant','9594910921',0),
('Apoorv.Mengudale@gmail.com', '12345678','Apoorv', 'Mengudale','9022044731',0),
('jadhavmansi655@gmail.com', '12345678','Mansi', 'Jadhav','7506987590',0),
('dgala13jan@gmail.com', '12345678','Deegesh', 'Gala','7045869213',0),
('harshuranavat@gmail.com', '12345678','Harsh', 'Ranavat','8796442020',0),
('devikasatare26@gmail.com ', '12345678','Devika', 'Satare','9967507608',0),
('ramani.k@codearray.tech ', '12345678','Ramani', 'Kalyansundaram','7738785358',0)

DECLARE @CreatedByUserID INT,
		@AdminRoleID INT,
		@TraneeRoleID INT,
		@ReportViewerRoleID INT

SELECT @CreatedByUserID = Id FROM  [dbo].[User] WHERE LoginName = 'adarsh.b@codearray.tech'

INSERT [dbo].[Role]
(
	[Name],
	CreatedBy
)
VALUES
('Admin', @CreatedByUserID),
('Trainee', @CreatedByUserID),
('ReportViewer', @CreatedByUserID)


SELECT @AdminRoleID = ID FROM [dbo].[Role] WHERE [Name] = 'Admin'
SELECT @TraneeRoleID = ID FROM [dbo].[Role] WHERE [Name] = 'Trainee'
SELECT @ReportViewerRoleID = ID FROM [dbo].[Role] WHERE [Name] = 'ReportViewer'

INSERT INTO [dbo].[UserRole]
(
	UserID,
	RoleID,
	CreatedBy
)
VALUES
(@CreatedByUserID, @AdminRoleID, @CreatedByUserID),
((SELECT ID FROM [dbo].[User] WHERE LoginName = 'nbirajadar5@gmail.com'), @TraneeRoleID, @CreatedByUserID),
((SELECT ID FROM [dbo].[User] WHERE LoginName = 'Apoorv.Mengudale@gmail.com'), @TraneeRoleID, @CreatedByUserID),
((SELECT ID FROM [dbo].[User] WHERE LoginName = 'jadhavmansi655@gmail.com'), @TraneeRoleID, @CreatedByUserID),
((SELECT ID FROM [dbo].[User] WHERE LoginName = 'dgala13jan@gmail.com'), @TraneeRoleID, @CreatedByUserID),
((SELECT ID FROM [dbo].[User] WHERE LoginName = 'harshuranavat@gmail.com'), @TraneeRoleID, @CreatedByUserID),
((SELECT ID FROM [dbo].[User] WHERE LoginName = 'devikasatare26@gmail.com'), @TraneeRoleID, @CreatedByUserID),
((SELECT ID FROM [dbo].[User] WHERE LoginName = 'ramani.k@codearray.tech'), @ReportViewerRoleID, @CreatedByUserID)



-----
Select * from [User]
Select * from [Role]
Select * from [UserRole]


SELECT	u.ID,
		LoginName,
		[Password],
		FirstName,
		MiddleName,
		LastName,
		ContactNumber,
		r.[Name],
		u.CreatedDate,
		u.CreatedBy,
		u.UpdatedDate,
		u.UpdatedBy
FROM	[User] u
JOIN	[UserRole] ur ON ur.UserID = u.Id
JOIN	[Role] r ON r.ID = ur.RoleID
