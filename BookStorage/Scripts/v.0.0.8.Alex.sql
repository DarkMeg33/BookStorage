ALTER PROCEDURE [dbo].[Book_Select] @bookId int = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		b.BookId,
		b.Title,
		b.Description,
		b.Author,
		b.CreatedAt,
		u.Username as AuthorName
	FROM Book b
		JOIN dbo.[User] u on u.UserId = b.Author
	WHERE BookId = @bookId OR @bookId is null
	ORDER BY Title
END