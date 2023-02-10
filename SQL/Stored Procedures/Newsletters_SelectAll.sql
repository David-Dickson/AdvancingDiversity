-- =============================================
    -- Author: David Dickson
    -- Create date: MM/DD/YEAR
    -- Description: SelectAll Pagination for the given PageIndex and PageSized of Newsletters
    -- Code Reviewer: Redacted

    -- MODIFIED BY: Author
    -- MODIFIED DATE: MM/DD/YEAR
    -- Code Reviewer:
    -- Note:
-- ==============================================

ALTER   PROC [dbo].[Newsletters_SelectAll]
		@PageIndex int
		,@PageSize int


AS


/*------ TEST CODE ------
	
	DECLARE @PageIndex int = 0
		,@PageSize int = 10

	EXECUTE [dbo].[Newsletters_SelectAll]
		@PageIndex
		,@PageSize

        SELECT * 
		FROM [dbo].[Newsletters]
			
	SELECT * 
		FROM [dbo].[NewsletterTemplates]

	SELECT * 
		FROM [dbo].[Users]


------ END  TEST CODE -------
*/


BEGIN


	DECLARE @offset int = @PageIndex * @PageSize
		

	SELECT [Id]
		  ,TemplateId
		  ,[Name]
		  ,[CoverPhoto]
		  ,[DateToPublish]
		  ,[DateToExpire]
		  ,[DateCreated]
		  ,[DateModified]
		  ,CreatedBy
		  ,TotalCount = COUNT(1) OVER()
	FROM [db]o.[Newsletters]

		  ORDER BY Id DESC
		  OFFSET @offset ROWS
		  FETCH NEXT @PageSize ROWS Only 

END
