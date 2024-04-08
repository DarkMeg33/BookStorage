CREATE TABLE [dbo].[UserReadingSettings](
	[UserReadingSettingsId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FontSize] int NOT NULL,
	[ThemeMode] nvarchar(255) NOT NULL
 CONSTRAINT [PK_UserReadingSettings_UserReadingSettingsId] PRIMARY KEY CLUSTERED 
(
	[UserReadingSettingsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_User] UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[UserReadingSettings]  ADD  CONSTRAINT [FK_UserReadingSettings_User] FOREIGN KEY(UserId)
REFERENCES [dbo].[User] ([UserId])
GO

CREATE PROCEDURE [dbo].[UserReadingSettings_Select] @userReadingSettingsId int = null, @userId int = null
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		urs.UserReadingSettingsId,
		urs.FontSize,
		urs.ThemeMode
	FROM UserReadingSettings urs
		JOIN dbo.[User] u on u.UserId = urs.UserId and u.UserId = @userId
	WHERE (UserReadingSettingsId = @userReadingSettingsId or @userReadingSettingsId = null) OR 
		(urs.UserId = @userId or @userId = null)
END
GO

CREATE PROCEDURE [dbo].[UserReadingSettings_Upsert] 
	@userReadingSettingsId int = null, 
	@fontSize int, 
	@themeMode nvarchar(255),
	@userId int
AS
BEGIN
	SET NOCOUNT ON;

	IF @userReadingSettingsId IS NULL
		BEGIN
			INSERT INTO [dbo].[UserReadingSettings]
			   ([FontSize]
			   ,[ThemeMode]
			   ,[UserId])
			 VALUES
				   (@fontSize
				   ,@themeMode
				   ,@userId)

			SELECT @userReadingSettingsId = SCOPE_IDENTITY()
		END
	ELSE
		BEGIN
			UPDATE [dbo].[UserReadingSettings]
			SET [FontSize] = @fontSize
				,[ThemeMode] = @themeMode
				,[UserId] = @userId
			WHERE UserReadingSettingsId = @userReadingSettingsId
		END

	EXEC [UserReadingSettings_Select] @userReadingSettingsId
END
GO