CREATE TABLE SearchWebPages
(
       [Id]                       INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
       [Title]                    NVARCHAR(1000) NOT NULL,
       [Description]				  NVARCHAR(MAX) NOT NULL,
       [Category]                 NVARCHAR(500) NULL,
       [Link]                     VARCHAR(1000) NOT NULL,
       [Keywords]                 NVARCHAR(1000) NULL,
       [Modified]                 ROWVERSION,
       [Deleted]                  BIT NOT NULL DEFAULT(0)
)


drop PROC procAddSearchWebPages

CREATE PROC procAddSearchWebPages(@title NVARCHAR(1000), @description NVARCHAR(MAX), @link VARCHAR(1000), @category NVARCHAR(500), @keywords NVARCHAR(1000))
AS
SET NOCOUNT ON
UPDATE SearchWebPages SET [Title]=@title, [Description]=@description, [Category]=@category, [Keywords]=@keywords WHERE [Link]= @link;
IF @@ROWCOUNT = 0 
       INSERT SearchWebPages([Title], [Description], [Link], [Category], [Keywords]) VALUES (@title, @description, @link, @category, @keywords);

CREATE INDEX idxLink ON SearchWebPages(Link)

INSERT SearchWebPages([Title],[Description],[Link],[Category],[Keywords],[Deleted])
       SELECT [Title],[Title],[Link],@keywords,0 FROM WebPages
