alter table [Book] add CoverStorageReference nvarchar(255) null
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
		u.Username as AuthorName
	FROM Book b
		JOIN dbo.[User] u on u.UserId = b.AuthorId
	WHERE (BookId = @bookId OR @bookId is null) and
		(Title = @title OR @title is null)
	ORDER BY Title
END
GO

ALTER PROCEDURE [dbo].[Book_Upsert] 
	@bookId int = null, 
	@title nvarchar(255), 
	@description nvarchar(255), 
	@coverStorageReference nvarchar(255) = null,
	@authorId int
AS
BEGIN
	SET NOCOUNT ON;

	IF @bookId IS NULL
		BEGIN
			INSERT INTO [dbo].[Book]
			   ([Title]
			   ,[Description]
			   ,[AuthorId])
			 VALUES
				   (@title
				   ,@description
				   ,@authorId)

			SELECT @bookId = SCOPE_IDENTITY()
		END
	ELSE
		BEGIN
			UPDATE [dbo].[Book]
			SET [Title] = @title
				,[Description] = @description
				,[AuthorId] = @authorId
			WHERE BookId = @bookId
		END

	EXEC [Book_Select] @bookId
	RETURN @bookId
END
GO

create PROCEDURE [dbo].[BookCover_Update] 
	@bookId int = null,  
	@storageReference nvarchar(255)
AS
BEGIN
	SET NOCOUNT ON;
	
	UPDATE [dbo].[Book]
		SET [CoverStorageReference] = @storageReference
	WHERE BookId = @bookId

END
GO