
--> Login
CREATE OR ALTER PROCEDURE sp_Login
    @username VARCHAR(50),
    @password VARCHAR(100)
AS
BEGIN
    SELECT UserId, Username, Role
    FROM Users
    WHERE Username = @username
      AND PasswordHash = @password;
END;
Go
-->Register
CREATE PROC sp_RegisterUser
@username VARCHAR(50),
@password VARCHAR(50),
@role VARCHAR(25)
AS
IF EXISTS (SELECT 1 FROM Users WHERE Username=@username)
    THROW 50002, 'User already exists', 1

INSERT INTO Users VALUES (@username, @password, @role)
GO
--> Add Movie
CREATE PROCEDURE sp_AddMovie
    @title NVARCHAR(100)
AS
BEGIN
    INSERT INTO Movies (Title)
    VALUES (@title);
END;
GO
--> Update Movie
CREATE PROCEDURE sp_UpdateMovie
    @movieId INT,
    @title NVARCHAR(100)
AS
BEGIN
    UPDATE Movies
    SET Title = @title
    WHERE MovieId = @movieId;
END;
GO
--> Delete Movie
CREATE PROCEDURE sp_DeleteMovie
    @movieId INT
AS
BEGIN
    DELETE FROM Movies WHERE MovieId = @movieId;
END;
GO
--> Get Movie List
CREATE PROCEDURE sp_GetMovies
AS
BEGIN
    SELECT MovieId, Title FROM Movies ORDER BY Title;
END;
GO
--> Add Show
CREATE PROCEDURE sp_AddShow
    @movieId INT,
    @showTime TIME,
    @price DECIMAL(10,2)
AS
BEGIN
    INSERT INTO Shows (MovieId, ShowTime, Price)
    VALUES (@movieId, @showTime, @price);

    -- Auto-create 50 seats per show
    DECLARE @showId INT = SCOPE_IDENTITY();
    DECLARE @i INT = 1;

    WHILE @i <= 25
    BEGIN
        INSERT INTO Seats (ShowId, SeatNumber)
        VALUES (@showId, @i);
        SET @i += 1;
    END
END;
GO
--> Update Show
CREATE OR ALTER PROCEDURE sp_UpdateShow
    @showId INT,
    @showTime TIME,
    @price DECIMAL(10,2)
AS
BEGIN
    UPDATE Shows
    SET ShowTime = @showTime,
        Price = @price
    WHERE ShowId = @showId;
END
GO
--> Delete show
CREATE OR ALTER PROCEDURE sp_DeleteShow
    @showId INT
AS
BEGIN
    DELETE FROM Shows
    WHERE ShowId = @showId;
END
GO

--> Get Show By Movie
CREATE PROCEDURE sp_GetShowsByMovie
    @movieId INT
AS
BEGIN
    SELECT ShowId, ShowTime, Price
    FROM Shows
    WHERE MovieId = @movieId
    ORDER BY ShowTime;
END;
GO
--> Get Available Seats
CREATE PROCEDURE sp_GetAvailableSeats
    @showId INT
AS
BEGIN
    SELECT SeatNumber
    FROM Seats
    WHERE ShowId = @showId AND IsBooked = 0
    ORDER BY SeatNumber;
END;
GO

CREATE PROCEDURE sp_BookSeat
    @userId INT,
    @showId INT,
    @seatNumber INT,
    @price DECIMAL(10,2)
AS
BEGIN
    IF EXISTS (
        SELECT 1 FROM Seats
        WHERE ShowId = @showId
          AND SeatNumber = @seatNumber
          AND IsBooked = 1
    )
    BEGIN
        RAISERROR ('Seat already booked', 16, 1);
        RETURN;
    END

    UPDATE Seats
    SET IsBooked = 1
    WHERE ShowId = @showId
      AND SeatNumber = @seatNumber;

    INSERT INTO Bookings (UserId, ShowId, SeatNumber, TotalPrice)
    VALUES (@userId, @showId, @seatNumber, @price);
END;
GO
--> Get user Booking
CREATE PROCEDURE sp_GetUserBookings
    @userId INT
AS
BEGIN
    SELECT 
        b.BookingId,
        m.Title,
        s.ShowTime,
        b.SeatNumber,
        b.TotalPrice,
        b.BookingDate
    FROM Bookings b
    JOIN Shows s ON b.ShowId = s.ShowId
    JOIN Movies m ON s.MovieId = m.MovieId
    WHERE b.UserId = @userId
    ORDER BY b.BookingDate DESC;
END;
GO
-->Updated Get user Booking
ALTER PROCEDURE sp_GetUserBookings
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        b.BookingId,
        m.Title,
        s.ShowTime,
        bs.SeatNumber,
        bs.Price,
        b.TotalAmount,
        b.BookingDate
    FROM Bookings b
    INNER JOIN BookingSeats bs ON b.BookingId = bs.BookingId
    INNER JOIN Shows s ON b.ShowId = s.ShowId
    INNER JOIN Movies m ON s.MovieId = m.MovieId
    WHERE b.UserId = @UserId
    ORDER BY b.BookingDate DESC, b.BookingId;
END;
GO

--> Updated BOOK seats
CREATE PROCEDURE sp_BookSeats
    @UserId INT,
    @ShowId INT,
    @SeatNumbers NVARCHAR(MAX), -- Example: '10,11,12'
    @Price DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        DECLARE @SeatCount INT;
        DECLARE @TotalAmount DECIMAL(10,2);

        -- Count seats
        SELECT @SeatCount = COUNT(*)
        FROM STRING_SPLIT(@SeatNumbers, ',');

        SET @TotalAmount = @SeatCount * @Price;
        --Update seat status
        UPDATE Seats
        SET IsBooked = 1
        WHERE ShowId = @showId
          AND SeatNumber = @seatNumber;
        -- Insert main booking
        INSERT INTO Bookings (UserId, ShowId, TotalAmount)
        VALUES (@UserId, @ShowId, @TotalAmount);

        DECLARE @BookingId INT = SCOPE_IDENTITY();

        -- Insert each seat
        INSERT INTO BookingSeats (BookingId, ShowId, SeatNumber, Price)
        SELECT @BookingId,
               @ShowId,
               CAST(value AS INT),
               @Price
        FROM STRING_SPLIT(@SeatNumbers, ',');

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO



