CREATE PROCEDURE [dbo].[Comment_Upsert]
	@commentId int = null,
	@bookId int,
	@authorId int,
	@text nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Comment]
           ([Text]
           ,[AuthorId]
           ,[BookId]
           ,[CreatedAt])
     VALUES
           (@text
           ,@authorId
           ,@bookId
           ,GETUTCDATE())

	SELECT @commentId = SCOPE_IDENTITY()

	EXEC [Comment_Select] @commentId
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
		u.Username as AuthorName
	FROM Comment c
		JOIN dbo.[User] u on u.UserId = c.AuthorId
	WHERE (BookId = @bookId OR @bookId is null) and
		(CommentId = @commentId or @commentId is null)
	ORDER BY CreatedAt desc
END