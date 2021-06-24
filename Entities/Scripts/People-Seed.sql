IF EXISTS (
        SELECT  *
        FROM    People
    )
    BEGIN
        DELETE
        FROM    People;

        DBCC CHECKIDENT ('People', RESEED, 0);
    END
    GO

INSERT INTO People (FirstName, LastName, Created, LastModified) VALUES
    ('Luke','Skywalker', GETUTCDATE(), GETUTCDATE()),
    ('Han','Solo', GETUTCDATE(), GETUTCDATE());

SELECT  *
FROM    People;