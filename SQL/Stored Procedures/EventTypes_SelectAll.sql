-- =============================================
-- Author: David Dickson
-- Create date: MM/DD/YEAR
-- Description: SelectAll for dbo.EventType Lookup Table
-- Code Reviewer: Redacted

-- MODIFIED BY: Author
-- MODIFIED DATE: MM/DD/YEAR
-- Code Reviewer:
-- Note:
-- =============================================
    
    
ALTER proc [dbo].[EventTypes_SelectAll]


AS

/* -----TEST CODE-----


Execute [dbo].[EventTypes_SelectAll]
					

*/-----END TEST CODE-----

BEGIN
	
		SELECT [Id]
			  ,[Name]
		  FROM [dbo].[EventTypes]




END
