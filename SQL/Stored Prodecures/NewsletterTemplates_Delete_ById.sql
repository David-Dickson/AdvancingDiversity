-- =============================================
-- Author:	David Dickson
-- Create date: MM/DD/YEAR
-- Description:	Delete_ById for Newsletter Template
-- Code Reviewer: Redacted


-- MODIFIED BY: author
-- MODIFIED DATE: MM/DD/YEAR
-- Code Reviewer: 
-- Note: 
-- =============================================

ALTER proc [dbo].[NewsletterTemplates_Delete_ById]
	@Id int
	   
as

/* ----TEST CODE----

Declare @Id int = 54

Select *
	From [dbo].[NewsletterTemplates]
	Where Id = @Id

Execute [dbo].[NewsletterTemplates_Delete_ById] @Id

Select *
	From [dbo].[NewsletterTemplates]
	Where Id = @Id

*/
Begin


DELETE FROM [dbo].[NewsletterTemplates]
      WHERE Id = @Id

End

