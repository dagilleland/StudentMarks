USE [StudentMarks2]
GO

/****** Object:  Table [dbo].[Courses]    Script Date: 01/03/2015 11:36:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Courses](
	[Number] [nvarchar](10) NOT NULL,
	[Name] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO

USE [StudentMarks2]
GO

/****** Object:  Table [dbo].[Components]    Script Date: 01/03/2015 11:36:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Components](
	[Title] [nvarchar](50) NOT NULL,
	[Weight] [int] NOT NULL,
	[Number] [nvarchar](10) NOT NULL
) ON [PRIMARY]

GO


