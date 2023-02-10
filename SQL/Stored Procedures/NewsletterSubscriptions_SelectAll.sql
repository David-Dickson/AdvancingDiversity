--===========================================================================================
-- Author: David Dickson
-- Create date: MM/DD/YEAR
-- Description: [dbo].[NewsletterSubscriptions_SelectAll] for [dbo].[NewsletterSubscriptions]
-- Code Reviewer: Redacted

-- Modified by: author
-- Modified date: MM/DD/YEAR
-- Code Reviewer: 
-- Note:
--============================================================================================


ALTER PROC [dbo].[NewsletterSubscriptions_SelectAll]
		@PageIndex int
		,@PageSize int

AS

/*------------------TEST CODE--------------------

	DECLARE @PageIndex int = 0
		,@PageSize int = 7

	EXECUTE [dbo].[NewsletterSubscriptions_SelectAll]
		@PageIndex
		,@PageSize

-----------------------------------------*/

BEGIN

	DECLARE @offset int = @PageIndex * @PageSize

	SELECT [Id]
		,[Email]
		,[IsSubscribed]
		,[DateCreated]
		,[DateModified] 
		,TotalCount = COUNT(1) OVER()
		
	FROM [dbo].[NewsletterSubscriptions]
	ORDER By Id
	
	OFFSET @offset ROWS
	FETCH NEXT @PageSize ROWS ONLY

END
