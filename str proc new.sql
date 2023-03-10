USE [EcomByAdoNet]
GO
/****** Object:  StoredProcedure [dbo].[get_ProductCartID]    Script Date: 20-02-2023 18:11:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[get_ProductCartID]
@mensGuid uniqueidentifier
as
begin
		select ProductCartId from ProductATC where MensGuid = @mensGuid
end


USE [EcomByAdoNet]
GO
/****** Object:  StoredProcedure [dbo].[sp_AddToCart]    Script Date: 20-02-2023 18:11:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[sp_AddToCart]
@ProductCartId uniqueidentifier,
@MensGuid uniqueidentifier,
@UserId uniqueidentifier,
@Quantity int,
@Date Date
as
begin
declare @count int
select @count = COUNT(*) from ProductATC Where MensGuid = @MensGuid AND UserId = @UserId

if(@count = 0 or @count = null)
begin
insert into ProductATC values(
@ProductCartId,
@MensGuid,
@UserId,
@Quantity,
@Date
)
select 1;
end
else
begin
     Update ProductATC Set
	 Quantity = @Quantity + Quantity where MensGuid = @MensGuid
select 1;
end
end

USE [EcomByAdoNet]
GO
/****** Object:  StoredProcedure [dbo].[sp_Delete_Productbyall]    Script Date: 20-02-2023 18:11:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[sp_Delete_Productbyall]
@ProductCartId uniqueidentifier
as
begin
	delete from ProductATC where ProductCartId = @ProductCartId
	select 1;
end


USE [EcomByAdoNet]
GO
/****** Object:  StoredProcedure [dbo].[Sp_DeleteManbyId]    Script Date: 20-02-2023 18:12:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[Sp_DeleteManbyId]
@identifier uniqueidentifier
as 
begin
   delete from Mens where Identifier = @identifier
   
   
   
   select 1;
end

USE [EcomByAdoNet]
GO
/****** Object:  StoredProcedure [dbo].[sp_GetQuantity]    Script Date: 20-02-2023 18:12:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[sp_GetQuantity]
@MensGuid uniqueidentifier
as
begin
   select Quantity from ProductATC where MensGuid = @MensGuid 
end

USE [EcomByAdoNet]
GO
/****** Object:  StoredProcedure [dbo].[sp_UpdateMens]    Script Date: 20-02-2023 18:12:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER proc [dbo].[sp_UpdateMens]
@id bigint,
@identifier uniqueidentifier,
@Name nvarchar(MAX),
@Image nvarchar(max),
@Rate nvarchar(Max),
@category int,
@userId uniqueidentifier
as
begin
   update Mens Set 
   CName =iif(isnull(@Name,'') <> '', @Name, CName),
   CImage = iif(isnull(@Image,'') <> '', @Image, CImage),
   CRate = iif(isnull( @Rate,'') <> '',  @Rate, CRate),
   Category = iif(isnull( @category,'') <> '',  @category, Category)
   where Identifier = @identifier
end