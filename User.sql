-- tabela User:
CREATE TABLE dbo.[User]
(
  userName NVARCHAR(32) NOT NULL,
  password CHAR(64) NOT NULL,
  CONSTRAINT user_pk PRIMARY KEY (userName)
);

-- procedury składowane:
-- UserSqlDB.Add
CREATE PROCEDURE [dbo].[UserCreate]
( @pUserName NVARCHAR(32),
  @pPassword CHAR(64) )
AS
  INSERT INTO [User] (userName, password)
  VALUES (@pUserName, @pPassword);

-- UserSqlDB.GetPassword
CREATE PROCEDURE [dbo].[UserGetPassword]
( @pUserName NVARCHAR(32) )
AS
  SELECT password
  FROM [User]
  WHERE userName = @pUserName;

-- UserSqlDB.UpdatePassword
CREATE PROCEDURE [dbo].[UserUpdatePassword]
( @pUserName VARCHAR(32),
  @pPassword CHAR(64) )
AS
  UPDATE [User]
  SET password = @pPassword
  WHERE userName = @pUserName;

-- UserSqlDB.Delete
CREATE PROCEDURE [dbo].[UserDelete]
( @pUserName VARCHAR(32) )
AS
  DELETE FROM [User]
  WHERE userName = @pUserName;

-- UserSqlDB.Exists
CREATE PROCEDURE [dbo].[UserExists]
( @pUserName NVARCHAR(32) )
AS
  SELECT 1
  FROM [User]
  WHERE userName = @pUserName;
