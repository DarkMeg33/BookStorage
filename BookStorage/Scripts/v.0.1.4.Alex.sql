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
		u.Username as AuthorName
	FROM Book b
		JOIN dbo.[User] u on u.UserId = b.AuthorId
	WHERE (BookId = @bookId OR @bookId is null) and
		(Title = @title OR @title is null)
	ORDER BY Title
END
GO

ALTER TABLE [dbo].[Book] add CONSTRAINT [IX_Title] unique nonclustered ([Title] asc)
GO