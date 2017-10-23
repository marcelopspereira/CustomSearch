CREATE TABLE SearchWebPages
(
	[Id]				INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[Title]				NVARCHAR(1000) NOT NULL,
	[Description]		NVARCHAR(MAX) NOT NULL,
	[Category]			NVARCHAR(500) NULL,
	[Link]				VARCHAR(1000) NOT NULL,
	[Modified]			ROWVERSION,
	[Deleted]			BIT NOT NULL
)

CREATE PROC procAddSearchWebPages(@title NVARCHAR(1000), @description NVARCHAR(MAX), @link VARCHAR(1000))
AS
SET NOCOUNT ON
UPDATE SearchWebPages SET [Title]=@title, [Description]=@description WHERE [Link]= @link;
IF @@ROWCOUNT = 0 
	INSERT SearchWebPages([Title], [Description], [Link]) VALUES (@title, @description, @link);
