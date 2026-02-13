CREATE Database MovieTicketBookingDB;

USE MovieTicketBookingDB;

CREATE TABLE Users (
    UserId INT IDENTITY PRIMARY KEY,
    Username VARCHAR(50) UNIQUE NOT NULL,
    Password VARCHAR(50) NOT NULL,
    Role VARCHAR(20) NOT NULL CHECK (Role IN ('Admin','Customer'))
);

Drop table if exists Movies;


CREATE TABLE Movies (
    MovieId INT PRIMARY KEY IDENTITY,
    Title VARCHAR(100) NOT NULL
);

Drop table if exists Shows;

CREATE TABLE Shows (
    ShowId INT IDENTITY PRIMARY KEY,
    MovieId INT NOT NULL,
    ShowTime TIME NOT NULL,
    Price DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_Shows_Movies FOREIGN KEY (MovieId)
        REFERENCES Movies(MovieId),

    CONSTRAINT UQ_Movie_ShowTime UNIQUE (MovieId, ShowTime)
);

Drop Table if exists Seats;

CREATE TABLE Seats (
    SeatId INT IDENTITY PRIMARY KEY,
    ShowId INT NOT NULL,
    SeatNumber INT NOT NULL,
    IsBooked BIT DEFAULT 0,

    CONSTRAINT FK_Seats_Shows FOREIGN KEY (ShowId)
        REFERENCES Shows(ShowId),

    CONSTRAINT UQ_Show_Seat UNIQUE (ShowId, SeatNumber)
);



