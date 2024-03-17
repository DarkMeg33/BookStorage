CREATE PROCEDURE [dbo].[UserProfile_Update] 
	@userId int,
	@username nvarchar(255), 
	@email nvarchar(255), 
	@password nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[User]
	SET [Email] = @email
		,[Username] = @username
		,[Email] = @password
	WHERE UserId = @userId

	EXEC [User_SelectById] @userId
END