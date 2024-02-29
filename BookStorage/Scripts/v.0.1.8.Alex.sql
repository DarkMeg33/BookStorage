CREATE TABLE [dbo].[Chapter](
	[ChapterId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[BookId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
 CONSTRAINT [PK_Chapter_ChapterId] PRIMARY KEY CLUSTERED 
(
	[ChapterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY])
GO

ALTER TABLE [dbo].[Chapter] ADD  CONSTRAINT [DF_Chapter_CreatedAt]  DEFAULT (getdate()) FOR [CreatedAt]
GO

ALTER TABLE [dbo].[Chapter]  ADD  CONSTRAINT [FK_Chapter_Book] FOREIGN KEY([BookId])
REFERENCES [dbo].[Book] ([BookId])
GO

CREATE PROCEDURE [dbo].[Chapter_Select] @chapterId int = null, @bookId int = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		c.ChapterId,
		c.Title,
		c.Content,
		c.BookId,
		c.CreatedAt
	FROM Chapter c
	WHERE (BookId = @bookId OR @bookId is null) and
		(ChapterId = @chapterId OR @chapterId is null)
	ORDER BY ChapterId
END
GO

CREATE PROCEDURE [dbo].[Chapter_SelectMetadata] @chapterId int = null, @bookId int = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		c.ChapterId,
		c.Title,
		c.CreatedAt
	FROM Chapter c
	WHERE (ChapterId = @chapterId OR @chapterId is null) and
		(BookId = @bookId OR @bookId is null)
	ORDER BY ChapterId
END
GO

CREATE PROCEDURE [dbo].[Chapter_Insert] 
	@chapterId int = null, 
	@title nvarchar(255), 
	@content nvarchar(max), 
	@bookId int
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Chapter]
		([Title]
		,[Content]
		,[BookId])
		VALUES
			(@title
			,@content
			,@bookId)

	SELECT @chapterId = SCOPE_IDENTITY()

	EXEC [Chapter_Select] @chapterId
END
GO