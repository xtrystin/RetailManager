CREATE PROCEDURE [dbo].[spUser_Insert]
	@Id nchar(128),
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@EmailAddress nvarchar(256)
AS
begin
	set nocount on;

	insert into dbo.[User](Id, FirstName, LastName, EmailAddress)
	VALUES (@Id, @FirstName, @LastName, @EmailAddress)
end
