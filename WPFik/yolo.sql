USE Rekrutacja
GO IF OBJECT_ID('dbo.ValidControlNumber') IS NOT NULL DROP FUNCTION dbo.ValidControlNumber IF OBJECT_ID('dbo.IsValidPesel') IS NOT NULL DROP FUNCTION dbo.IsValidPesel IF OBJECT_ID('dbo.CheckPesel') IS NOT NULL DROP TRIGGER dbo.CheckPesel IF OBJECT_ID('dbo.IsValidEmail') IS NOT NULL DROP FUNCTION dbo.IsValidEmail IF OBJECT_ID('dbo.CheckEmail') IS NOT NULL DROP TRIGGER dbo.CheckEmail CREATE FUNCTION dbo.ValidControlNumber(@pesel NVARCHAR(11)) RETURNS BIT AS BEGIN
DECLARE @controlNumber INT = CAST(SUBSTRING(@pesel, 11, 1) AS INT)
DECLARE @sum INT = 0
DECLARE @i INT = 0
DECLARE @multipliers TABLE (multiplier INT)
DECLARE @digit INT -- delklarowanie mnożników cyfr
INSERT INTO @multipliers
VALUES (1),
    (3),
    (7),
    (9),
    (1),
    (3),
    (7),
    (9),
    (1),
    (3)
DECLARE multipliersCursor CURSOR FOR
SELECT multiplier
FROM @multipliers OPEN multipliersCursor FETCH NEXT
FROM multipliersCursor INTO @digit WHILE @@FETCH_STATUS = 0 BEGIN
SET @sum = @sum + @digit * CAST(SUBSTRING(@pesel, @i + 1, 1) AS INT)
SET @i = @i + 1 FETCH NEXT
FROM multipliersCursor INTO @digit
END CLOSE multipliersCursor DEALLOCATE multipliersCursor -- modulo 10
SET @sum = @sum % 10 IF @sum = 0
SET @sum = 10
SET @sum = 10 - @sum -- Jeśli suma 10 cyfr == jedynsatej cyfrze to jest prawidłowy
    IF @sum = @controlNumber RETURN 1 RETURN 0
END
GO CREATE FUNCTION dbo.IsValidPesel (@pesel NVARCHAR(11)) RETURNS BIT AS BEGIN
DECLARE @peselLength INT = LEN(@pesel);
DECLARE @isPeselNumeric BIT = ISNUMERIC(@pesel);
DECLARE @isControlNumberValid BIT = dbo.ValidControlNumber(@pesel);
IF @peselLength = 11
AND @isPeselNumeric = 1
AND @isControlNumberValid = 1 RETURN 1;
RETURN 0;
END
GO CREATE TRIGGER dbo.CheckPesel ON dbo.Użytkownicy FOR
INSERT AS BEGIN
DECLARE @PESEL VARCHAR(11)
SELECT @PESEL = PESEL
FROM inserted IF dbo.IsValidPesel(@PESEL) = 1 RETURN RAISERROR('Niepoprawny numer PESEL.', 16, 1);
ROLLBACK TRANSACTION;
END
GO CREATE FUNCTION dbo.IsValidEmail (@email NVARCHAR(255)) RETURNS BIT AS BEGIN
DECLARE @emailLength INT
DECLARE @atPosition INT
DECLARE @dotPosition INT
SET @emailLength = LEN(@email)
SET @atPosition = CHARINDEX('@', @email)
SET @dotPosition = CHARINDEX('.', @email) IF @atPosition > 1
    AND @dotPosition > @atPosition
    AND @dotPosition < @emailLength RETURN 1 RETURN 0
END CREATE TRIGGER dbo.CheckEmail ON dbo.Użytkownicy FOR
INSERT,
    UPDATE AS BEGIN
DECLARE @Email VARCHAR(255)
SELECT @Email = Email
FROM inserted IF dbo.IsValidEmail(@Email) = 1 RETURN RAISERROR('Niepoprawny adres email.', 16, 1);
ROLLBACK TRANSACTION;
END
INSERT INTO Użytkownicy (
        imię,
        nazwisko,
        PESEL,
        telefon,
        email,
        nr_dowodu,
        płeć,
        data_urodzenia
    )
VALUES (
        'Jan',
        'Kowalski',
        '12345678901',
        '123456789',
        'jan@wp.pl',
        'ABC123456',
        'Mężczyzna',
        '1990-01-01'
    )
INSERT INTO Użytkownicy (
        imię,
        nazwisko,
        PESEL,
        telefon,
        email,
        nr_dowodu,
        płeć,
        data_urodzenia
    )
VALUES (
        'Adrian',
        'Kowalski',
        '64042999928',
        '123456789',
        'piotr@o2.pl',
        'ABC123478',
        'Mężczyzna',
        '1990-01-05'
    )