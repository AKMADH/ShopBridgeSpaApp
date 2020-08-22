-----------------database table creation file---------------
USE [ShopBridge]
GO

/****** Object:  Table [dbo].[Hotel]    Script Date: 8/19/2020 1:49:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Hotel](
	[Mhid] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[HotelImage] [varchar](255) NOT NULL,
	[Description] [text] NULL,
	[price] [decimal](10, 2) NULL,
	[IsActive] [bit] NULL,
	[CreatedBy] [varchar](255) NULL,
	[Createdate] [date] NULL,
	[LastModifiedBy] [varchar](255) NULL,
	[LastModifiedDate] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Mhid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO




-------------------------DeleteHotelDetails_V1 Stored Procedure--------------
USE [ShopBridge]
GO
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create proc [dbo].[DeleteHotelByMhid_V1]         
(        
@Mhid int       
)        
as        
begin        
DELETE FROM Hotel WHERE Mhid=@Mhid;    
end
GO
----------------------------------------GetAllHotels_V1 Stored Procedure-----------------
Create proc [dbo].[GetAllHotels_V1]
as
begin
select [Mhid],
[Name]
      ,[HotelImage]
      ,[Description]
      ,[price]
      
  FROM  Hotel where isActive=1
 end
 -------------------------------------------------GetHotelByMhid_V1  Stored Procedure-----------------
 Create Proc [dbo].[GetHotelByMhid_V1](@Mhid int) 
as
 begin
  select
[Mhid]
      ,[Name]
      ,[HotelImage]
      ,[Description]
      ,[price] from Hotel where Mhid=@Mhid    
 end  
---------------------------------------------------InsertHotelDetails_V1 Stored Procedure-------------------
CREATE Proc [dbo].[InsertHotelDetails_V1]
(
@Mhid int out,
@Name varchar(50),
@Description text,
@HotelImage varchar(255),
@Price int

)
as
begin
insert into Hotel(
        [Name]
      ,[HotelImage]
      ,[Description]
      ,[price]
      ,[IsActive]
      ,[CreatedBy]
      ,[Createdate]
)
values
(
 @Name
,@HotelImage
,@Description,
@Price,
1,
'Admin'
,GetDate()
)

set @Mhid=SCOPE_IDENTITY()
end
---------------------------------------------- UpdateHotelDetails_V1  Stored Procedure---------------
Create proc [dbo].[UpdateHotelDetails_V1](  
@Mhid int,
@name varchar(255),
@description varchar(255),
@hotelImage varchar(200),
@price int
)  
as  
begin  
Update Hotel 
set  

Name=@name,
HotelImage = @hotelImage,
 price=@price,
 Description=@description,
 LastModifiedDate=getdate()

where Mhid= @Mhid  
  
end  