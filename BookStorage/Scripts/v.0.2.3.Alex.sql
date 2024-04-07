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

ALTER PROCEDURE [dbo].[Book_Select] @bookId int = null, @title nvarchar(255) = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		b.BookId,
		b.Title,
		b.Description,
		b.AuthorId,
		b.CreatedAt,
		b.CoverStorageReference,
		u.Username as AuthorName,
		u.AvatarStorageReference as AuthorAvatarStorageReference
	FROM Book b
		JOIN dbo.[User] u on u.UserId = b.AuthorId
	WHERE (BookId = @bookId OR @bookId is null) and
		(Title = @title OR @title is null)
	ORDER BY Title
END
GO