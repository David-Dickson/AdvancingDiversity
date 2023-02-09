CREATE TABLE [dbo].[Newsletters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TemplateId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[CoverPhoto] [nvarchar](255) NULL,
	[DateToPublish] [datetime2](7) NULL,
	[DateToExpire] [datetime2](7) NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Newsletters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Newsletters] ADD  CONSTRAINT [DF_Newsletters_DateCreated]  DEFAULT (getutcdate()) FOR [DateCreated]

ALTER TABLE [dbo].[Newsletters] ADD  CONSTRAINT [DF_Newsletters_DateModified]  DEFAULT (getutcdate()) FOR [DateModified]

ALTER TABLE [dbo].[Newsletters]  WITH CHECK ADD  CONSTRAINT [FK_Newsletters_NewsletterTemplates] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[NewsletterTemplates] ([Id])

ALTER TABLE [dbo].[Newsletters] CHECK CONSTRAINT [FK_Newsletters_NewsletterTemplates]

ALTER TABLE [dbo].[Newsletters]  WITH CHECK ADD  CONSTRAINT [FK_Newsletters_Users] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Users] ([Id])

ALTER TABLE [dbo].[Newsletters] CHECK CONSTRAINT [FK_Newsletters_Users]
