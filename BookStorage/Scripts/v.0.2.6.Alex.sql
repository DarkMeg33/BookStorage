ALTER PROCEDURE [dbo].[Book_Upsert] 
	@bookId int = null, 
	@title nvarchar(255), 
	@description nvarchar(max), 
	@coverStorageReference nvarchar(255) = null,
	@authorId int,
	@price numeric(12,2)
AS
BEGIN
	SET NOCOUNT ON;

	IF @bookId IS NULL
		BEGIN
			INSERT INTO [dbo].[Book]
			   ([Title]
			   ,[Description]
			   ,[AuthorId]
			   ,[Price])
			 VALUES
				   (@title
				   ,@description
				   ,@authorId
				   ,@price)

			SELECT @bookId = SCOPE_IDENTITY()
		END
	ELSE
		BEGIN
			UPDATE [dbo].[Book]
			SET [Title] = @title
				,[Description] = @description
				,[AuthorId] = @authorId
				,[Price] = @price
			WHERE BookId = @bookId
		END

	EXEC [Book_Select] @bookId, @currentUserId = @authorId
	RETURN @bookId
END
GO