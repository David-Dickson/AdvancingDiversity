-- =============================================
-- Author:	David Dickson
-- Create date: MM/DD/YEAR
-- Description:	Delete_ById for Newsletter Template
-- Code Reviewer: REdacted


-- MODIFIED BY: author
-- MODIFIED DATE: MM/DD/YEAR
-- Code Reviewer: 
-- Note: 
-- =============================================

ALTER PROC [dbo].[NewsletterTemplates_Delete_ById]
	@Id int
	   
AS

/* ----TEST CODE----

DECLARE @Id int = 54

SELECT *
	FROM [dbo].[NewsletterTemplates]
	WHERE Id = @Id

EXECUTE [dbo].[NewsletterTemplates_Delete_ById] @Id

SELECT *
	FROM [dbo].[NewsletterTemplates]
	WHERE Id = @Id

*/


BEGIN


DELETE FROM [dbo].[NewsletterTemplates]
      WHERE Id = @Id

END
