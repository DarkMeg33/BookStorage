 alter table [BookStorage].[dbo].[User] add AvatarStorageReference nvarchar(255) null
 go

 ALTER PROCEDURE [dbo].[User_SelectById] @userId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT u.[UserId],
		u.[Username],
		u.[Email],
		u.[Password],
		u.[AvatarStorageReference],
		u.[CreatedAt]
	FROM [dbo].[User] u
	WHERE u.UserId = @userId
	ORDER BY u.Username
END
go

create PROCEDURE [dbo].[UserAvatar_Update] 
	@userId int,
	@avatarStorageReference nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE [dbo].[User]
	SET AvatarStorageReference = @avatarStorageReference
	WHERE UserId = @userId
END
GO