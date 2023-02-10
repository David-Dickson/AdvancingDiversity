-- =============================================
-- Author: David Dickson
-- Create date: 6/10/2022
-- Description: NewsletterSubscriptions Select By Search
-- Code Reviewer: Redacted

-- MODIFIED BY: Author
-- MODIFIED DATE: MM/DD/YYYY
-- Code Reviewer:
-- Note:
-- =============================================


ALTER PROC [dbo].[NewsletterSubscriptions_SelectBy_Search]
		@PageIndex int
		,@PageSize int
		,@Query nvarchar(255)

AS

/*-----------TEST CODE----------------
			
	SELECT *
	FROM [dbo].[NewsletterSubScriptions]

	DECLARE @PageIndex int = 0
		,@PageSize int = 10
		,@Query nvarchar(255) = 'Test06@gmail.com'

	EXECUTE [dbo].[NewsletterSubscriptions_SelectBy_Search]
		@PageIndex
		,@PageSize
		,@Query

------------------------------------------------------------------*/

BEGIN

	DECLARE @offset int = @PageIndex * @PageSize

	SELECT [Id]
		,[Email]
		,[IsSubscribed]
		,[DateCreated]
		,[DateModified]
		,TotalCount = COUNT(1) OVER()
		
	FROM [dbo].[NewsletterSubscriptions]
	
		WHERE (Email LIKE '%' + @Query + '%')

		ORDER BY Id

		OFFSET @offset ROWS

		FETCH NEXT @PageSize ROWS ONLY

END
