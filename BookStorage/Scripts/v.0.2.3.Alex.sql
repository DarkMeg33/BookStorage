ALTER PROCEDURE [dbo].[UserProfile_Update] 
	@userId int,
	@username nvarchar(255), 
	@email nvarchar(255), 
	@password nvarchar(255),
	@avatarStorageReference nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[User]
	SET [Email] = @email
		,[Username] = @username
		,[Password] = @password
		,[AvatarStorageReference] = @avatarStorageReference
	WHERE UserId = @userId

	EXEC [User_SelectById] @userId
END
GO