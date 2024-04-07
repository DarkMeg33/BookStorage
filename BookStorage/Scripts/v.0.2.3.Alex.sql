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

ALTER PROCEDURE [dbo].[Comment_Select] @commentId int = null, @bookId int = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		c.CommentId,
		c.Text,
		c.BookId,
		c.AuthorId,
		c.CreatedAt,
		u.Username as AuthorName,
		u.AvatarStorageReference as AuthorAvatarStorageReference
	FROM Comment c
		JOIN dbo.[User] u on u.UserId = c.AuthorId
	WHERE (BookId = @bookId OR @bookId is null) and
		(CommentId = @commentId or @commentId is null)
	ORDER BY CreatedAt desc
END
GO