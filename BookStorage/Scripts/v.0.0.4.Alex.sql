CREATE TABLE Book(
	BookId int IDENTITY(1, 1) NOT NULL,
	Title nvarchar(255) NOT NULL,
	Description nvarchar(255) NOT NULL,
	Author int NULL,
	CreatedAt datetime NOT NULL
	CONSTRAINT PK_Book_BookId PRIMARY KEY CLUSTERED(
		BookId ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
)

ALTER TABLE Book
ADD CONSTRAINT DF_Book_Created
DEFAULT (GETDATE()) FOR CreatedAt;

CREATE PROCEDURE Book_Select @bookId int = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		b.BookId,
		b.Title,
		b.Description,
		b.Author,
		b.CreatedAt
	FROM Book b
	WHERE BookId = @bookId OR @bookId is null
	ORDER BY Title
END
GO

CREATE PROCEDURE Book_Upsert 
	@bookId int = null, 
	@title nvarchar(255), 
	@description nvarchar(255), 
	@author int
AS
BEGIN
	SET NOCOUNT ON;

	IF @bookId IS NULL
		BEGIN
			INSERT INTO [dbo].[Book]
			   ([Title]
			   ,[Description]
			   ,[Author])
			 VALUES
				   (@title
				   ,@description
				   ,@author)

			SELECT @bookId = SCOPE_IDENTITY()
		END
	ELSE
		BEGIN
			UPDATE [dbo].[Book]
			SET [Title] = @title
				,[Description] = @description
				,[Author] = @author
			WHERE BookId = @bookId
		END

	EXEC [Book_Select] @bookId
	RETURN @bookId
END
GO