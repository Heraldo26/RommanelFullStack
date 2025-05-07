
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'RommanelDB')
BEGIN
    CREATE DATABASE RommanelDB;
    PRINT 'Database RommanelDB created.'
END
ELSE
BEGIN
    PRINT 'Database RommanelDB already exists.'
END
