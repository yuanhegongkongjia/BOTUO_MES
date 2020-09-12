USE [sumin]
GO

/****** Object:  Table [dbo].[SM_T_PROCESS_COLLECT]    Script Date: 2019/7/30 9:58:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SM_T_PROCESS_COLLECT](
	[PKId] [nvarchar](50) NOT NULL,
	[PLCAddress] [nvarchar](50) NULL,
	[Line] [nvarchar](50) NULL,
	[Position] [nvarchar](50) NULL,
	[ParamName] [nvarchar](50) NULL,
	[CollectValue] [nvarchar](50) NULL,
	[CollectTime] [datetime] NULL,
	[Remark1] [nvarchar](50) NULL,
	[Remark2] [nvarchar](50) NULL,
	[Remark3] [nvarchar](50) NULL,
	[Remark4] [nvarchar](50) NULL,
	[Remark5] [nvarchar](50) NULL,
	[CreateUser] [nvarchar](50) NULL,
	[CreateTime] [datetime] NULL,
	[ParamType] [nvarchar](50) NULL,
 CONSTRAINT [PK_SM_T_PROCESS_COLLECT] PRIMARY KEY CLUSTERED 
(
	[PKId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


