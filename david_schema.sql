
--create database ithealth

--use ithealth

CREATE TABLE Patients (
    Id INT IDENTITY(1,1) NOT NULL,
    FullName NVARCHAR(150) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT PK_Patients PRIMARY KEY (Id)
);
GO

CREATE TABLE Orders (
    Id INT IDENTITY(1,1) NOT NULL,
    PatientId INT NOT NULL,
    AttentionDate DATETIME2 NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    CONSTRAINT PK_Orders PRIMARY KEY (Id),
    CONSTRAINT FK_Orders_Patients 
        FOREIGN KEY (PatientId) 
        REFERENCES Patients(Id)
        ON DELETE CASCADE
);
GO

CREATE TABLE Exams (
    Id INT IDENTITY(1,1) NOT NULL,
    Code NVARCHAR(50) NOT NULL UNIQUE,
    Name NVARCHAR(150) NOT NULL,
    CONSTRAINT PK_Exams PRIMARY KEY (Id)
);
GO

CREATE TABLE OrderExams (
    OrderId INT NOT NULL,
    ExamId INT NOT NULL,
    CONSTRAINT PK_OrderExams PRIMARY KEY (OrderId, ExamId),
    CONSTRAINT FK_OrderExams_Orders 
        FOREIGN KEY (OrderId)
        REFERENCES Orders(Id)
        ON DELETE CASCADE,
    CONSTRAINT FK_OrderExams_Exams 
        FOREIGN KEY (ExamId)
        REFERENCES Exams(Id)
        ON DELETE CASCADE
);
GO

INSERT INTO Patients(FullName) VALUES ('Antonio')
INSERT INTO Exams(Code,Name) VALUES ('H01','Hemograma'),('GLU2','Glucosa')
INSERT INTO Orders(PatientId,AttentionDate) VALUES (1,SYSDATETIME()),(2,SYSDATETIME())
INSERT INTO OrderExams (OrderId,ExamId) VALUES (1,1),(1,2)










